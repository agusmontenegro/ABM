using ABM.DTO;
using ABM.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ABM.Services
{
    class RolesServices
    {
        public static List<Rol> GetAllData()
        {
            DataBaseHelper db = new DataBaseHelper(ConfigurationManager.AppSettings["connectionString"]);

            using (db.Connection)
            {
                db.BeginTransaction();

                List<Rol> roles = GetRoles(db);

                db.EndConnection();

                return roles;
            }
        }

        public static List<Rol> GetRoles(DataBaseHelper db)
        {
            List<Rol> roles = new List<Rol>();

            DataTable res = db.GetDataAsTable("MASTERDBA.SP_GetRoles");

            foreach (DataRow row in res.Rows)
            {
                var rol = new Rol
                {
                    Id = Convert.ToInt32(row["role_id"]),
                    Description = Convert.ToString(row["role_description"]),
                    IsActive = Convert.ToBoolean(row["role_active"]),
                };

                rol.Funcionalities = GetRolFuncionalities(rol.Id, db);

                roles.Add(rol);
            }

            return roles;
        }

        public static List<Funcionality> GetRolFuncionalities(int Id, DataBaseHelper db)
        {
            List<Funcionality> funcionalidades = new List<Funcionality>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter idRolParameter = new SqlParameter("@role_id", SqlDbType.Int);
            idRolParameter.Value = Id;

            parameters.Add(idRolParameter);

            DataTable res = db.GetDataAsTable("MASTERDBA.SP_GetRolFuncionalities", parameters);
            List<Funcionality> allActiveFuncionalities = new List<Funcionality>(GetFuncionalities(db));
            foreach (DataRow row in res.Rows)
            {
                var idFuncionalidad = Convert.ToInt32(row["IdFuncionalidad"]);

                funcionalidades.Add(allActiveFuncionalities.Find(x => x.Id == idFuncionalidad));
            }

            return funcionalidades;
        }

        public static Rol GetRolByDescription(string description, DataBaseHelper db)
        {
            Rol rol = new Rol();
            rol.Funcionalities = new List<Funcionality>();

            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter descriptionRolParameter = new SqlParameter("@role_description", SqlDbType.Int);
            descriptionRolParameter.Value = description;

            parameters.Add(descriptionRolParameter);

            DataTable res = db.GetDataAsTable("MASTERDBA.SP_GetRolByDescription", parameters);

            foreach (DataRow row in res.Rows)
            {
                rol.Id = Convert.ToInt32(row["role_id"]);
                rol.Description = Convert.ToString(row["role_description"]);
                rol.Funcionalities = GetRolFuncionalities(rol.Id, db);
            }

            return rol;
        }

        private static List<Funcionality> GetFuncionalities(DataBaseHelper db)
        {
            List<Funcionality> funcionalidades = new List<Funcionality>();

            DataTable res = db.GetDataAsTable("MASTERDBA.SP_GetFuncionalities");

            foreach (DataRow row in res.Rows)
            {
                var funcionalidad = new Funcionality
                {
                    Id = Convert.ToInt32(row["func_id"]),
                    Description = Convert.ToString(row["func_description"]),
                };

                funcionalidades.Add(funcionalidad);
            }

            return funcionalidades;
        }
    }
}