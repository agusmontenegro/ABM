﻿using ABM.DTO;
using ABM.Helper;
using ABM.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ABM.Services
{
    class UserServices
    {
        public static DTO.LogIn LoginUser(string userName, string password)
        {

            DataBaseHelper db = new DataBaseHelper(ConfigurationManager.AppSettings["connectionString"]);

            using (db.Connection)
            {
                db.BeginTransaction();

                User usuario = GetUsuarioByUserName(userName, db);

                DTO.LogIn login = new DTO.LogIn();
                if (usuario == null)
                {
                    login.LoginSuccess = false;
                    MessageBox.Show(Resources.UsuarioNoExiste);
                }
                else
                {
                    if (!usuario.IsActive)
                    {
                        if (usuario.CountFailedAttempts >= 3)
                        {
                            login.Usuario = usuario;
                            login.LoginSuccess = false;
                            MessageBox.Show(Resources.UsuarioBloqueado + Resources.ContactarAdministrador);
                        }
                        else
                        {
                            login.Usuario = usuario;
                            login.LoginSuccess = false;
                            MessageBox.Show(Resources.UsuarioDeshabilitado + "\n" + Resources.ContactarAdministrador);
                        }
                    }
                    else if (usuario.Password.Equals(password))
                    {
                        ResetCountLogin(userName, db);

                        usuario.CountFailedAttempts = 0;
                        login.Usuario = usuario;
                        login.LoginSuccess = true;
                    }
                    else
                    {
                        usuario.CountFailedAttempts = (int)IncrementCountLogin(userName, db);

                        login.Usuario = usuario;
                        login.LoginSuccess = false;
                        MessageBox.Show(Resources.ContraseñaIncorrecta);

                        if (usuario.CountFailedAttempts >= 3)
                            BloqUser(userName, db);
                    }
                }

                db.EndConnection();

                return login;
            }
        }

        public static void BloqUser(string userName, DataBaseHelper db)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter userNameParameter = new SqlParameter("@UserName", SqlDbType.NVarChar);
            userNameParameter.Value = userName;

            parameters.Add(userNameParameter);

            db.ExecInstruction(DataBaseHelper.ExecutionType.NonQuery, "MASTERDBA.SP_BloqUser", parameters);
        }

        public static object IncrementCountLogin(string userName, DataBaseHelper db)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter userNameParameter = new SqlParameter("@UserName", SqlDbType.NVarChar);
            userNameParameter.Value = userName;

            parameters.Add(userNameParameter);

            return db.ExecInstruction(DataBaseHelper.ExecutionType.Scalar, "MASTERDBA.SP_IncrementCountLogin", parameters);
        }

        public static void ResetCountLogin(string userName, DataBaseHelper db)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter userNameParameter = new SqlParameter("@UserName", SqlDbType.NVarChar);
            userNameParameter.Value = userName;

            parameters.Add(userNameParameter);

            db.ExecInstruction(DataBaseHelper.ExecutionType.NonQuery, "MASTERDBA.SP_ResetCountLogin", parameters);
        }

        public static User GetUsuarioByUserName(string userName, DataBaseHelper db)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter userNameParameter = new SqlParameter("@username", SqlDbType.NVarChar);
            userNameParameter.Value = userName;

            parameters.Add(userNameParameter);

            DataTable res = db.GetDataAsTable("dbo.SP_GetUsuarioByUserName", parameters);
            if (res.Rows.Count != 0)
            {
                User usuario = new User();
                foreach (DataRow row in res.Rows)
                {
                    usuario.Id = Convert.ToInt32(row["user_id"]);
                    usuario.UserName = Convert.ToString(row["user_username"]);
                    usuario.Password = Convert.ToString(row["user_password"]);
                    usuario.CountFailedAttempts = Convert.ToByte(row["user_count_failed_attempts"]);
                    usuario.IsActive = Convert.ToBoolean(row["user_active"]);
                }

                usuario.Roles = GetRolesUsuario(usuario.Id, db);

                return usuario;
            }
            else return null;
        }

        public static List<Rol> GetRolesUsuario(int idUsuario, DataBaseHelper db)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter idUsuarioParameter = new SqlParameter("@IdUsuario", SqlDbType.Int);
            idUsuarioParameter.Value = idUsuario;

            parameters.Add(idUsuarioParameter);

            DataTable res = db.GetDataAsTable("MASTERDBA.SP_GetRolesUsuario", parameters);
            List<Rol> roles = new List<Rol>();
            List<Rol> allRoles = new List<Rol>(RolesServices.GetAllData());
            foreach (DataRow row in res.Rows)
            {
                var idRol = Convert.ToInt32(row["IdRol"]);

                roles.Add(allRoles.Find(x => x.Id == idRol));
            }

            return roles;
        }

        public static void RegisterNewUser(User newUser, DataBaseHelper db)
        {
            using (db.Connection)
            {
                db.BeginTransaction();

                InsertUser(newUser, db);
                newUser.Roles.Add(RolesServices.GetRolByDescription(Resources.Supervisor, db));

                foreach (Rol rol in newUser.Roles)
                {
                    InsertUsuarioRol(newUser.Id, rol.Id, db);
                }

                db.EndConnection();
            }
        }

        private static void InsertUsuarioRol(int idUsuario, int idRol, DataBaseHelper db)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter idUsuarioParameter = new SqlParameter("@userId", SqlDbType.Int);
            idUsuarioParameter.Value = idUsuario;

            SqlParameter idRolParameter = new SqlParameter("@rolId", SqlDbType.Int);
            idRolParameter.Value = idRol;

            parameters.Add(idUsuarioParameter);
            parameters.Add(idRolParameter);

            db.ExecInstruction(DataBaseHelper.ExecutionType.NonQuery, "MASTERDBA.SP_InsertUsuarioRol", parameters);
        }

        private static void InsertUser(User newUser, DataBaseHelper db)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter userNameParameter = new SqlParameter("@username", SqlDbType.NVarChar);
            userNameParameter.Value = newUser.UserName;

            SqlParameter passEncrParameter = new SqlParameter("@password", SqlDbType.NVarChar);
            passEncrParameter.Value = EncryptHelper.Sha256Encrypt(newUser.Password);

            SqlParameter cantIntFallidosParameter = new SqlParameter("@countFailedAttemps", SqlDbType.Int);
            cantIntFallidosParameter.Value = newUser.CountFailedAttempts;

            SqlParameter activoParameter = new SqlParameter("@isActive", SqlDbType.Bit);
            activoParameter.Value = true;

            SqlParameter mailParameter = new SqlParameter("@mail", SqlDbType.NVarChar);
            activoParameter.Value = newUser.Email;

            parameters.Add(userNameParameter);
            parameters.Add(passEncrParameter);
            parameters.Add(cantIntFallidosParameter);
            parameters.Add(activoParameter);
            parameters.Add(mailParameter);

            newUser.Id = Convert.ToInt32(db.ExecInstruction(DataBaseHelper.ExecutionType.Scalar, "dbo.SP_InsertUser", parameters));
        }
    }
}
