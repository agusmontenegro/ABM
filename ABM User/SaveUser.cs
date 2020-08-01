using ABM.DTO;
using ABM.Helper;
using ABM.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace ABM.ABM_User
{
    public partial class SaveUser : Form
    {
        public bool register { get; set; }

        public SaveUser(bool register = false)
        {
            InitializeComponent();
            this.register = register;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msj = string.Empty;
            DataBaseHelper db = new DataBaseHelper(ConfigurationManager.AppSettings["connectionString"]);

            if (usuarioValido(ref msj, db))
            {
                User newUser = new User
                {
                    CountFailedAttempts = 0,
                    Email = txtMail.Text.Trim(),
                    IsActive = true,
                    Password = EncryptHelper.Sha256Encrypt(txtPassword.Text.Trim()),
                    UserName = txtUsername.Text.Trim(),
                    Roles = new List<Rol>()
                };

                using (db.Connection)
                {
                    db.BeginTransaction();
                    if (register) //Chequeo si se está registrando un usuario nuevo
                    {
                        UserServices.RegisterNewUser(newUser, db);
                        MessageBox.Show("Usuario registrado exitosamente.");
                        Close();
                    }
                }
            }
            else
                MessageBox.Show(msj);
        }

        private bool usuarioValido(ref string msj, DataBaseHelper db)
        {
            ValidationServices v = new ValidationServices();

            using (db.Connection)
            {
                if (!string.IsNullOrEmpty(txtUsername.Text))
                {
                    db.BeginTransaction();
                    User usuario = UserServices.GetUsuarioByUserName(txtUsername.Text.Trim(), db);
                    v.Validations.Add(new ValidationServices.Validation { condition = usuario == null, msj = "Ya existe un usuario con ese nombre de usuario." });
                }
                else
                    v.Validations.Add(new ValidationServices.Validation { condition = false, msj = "Debe ingresar el nombre de usuario." });

                v.Validations.Add(new ValidationServices.Validation { condition = !string.IsNullOrEmpty(txtPassword.Text), msj = "Debe ingresar la contraseña." });
                v.Validations.Add(new ValidationServices.Validation { condition = !string.IsNullOrEmpty(txtRepeatPassword.Text), msj = "Debe repetir la contraseña." });
                v.Validations.Add(new ValidationServices.Validation { condition = !string.IsNullOrEmpty(txtMail.Text), msj = "Debe ingresar el correo electrónico." });
                v.Validations.Add(new ValidationServices.Validation { condition = txtPassword.Text.ToUpper() == txtRepeatPassword.Text.ToUpper(), msj = "Las contraseñas no coinciden." });
                v.Validations.Add(new ValidationServices.Validation { condition = txtMail.Text.Contains("@"), msj = "Formato de correo electrónico inválido." });

            }

            return v.validate(ref msj);
        }
    }
}