using ABM.Helper;
using ABM.Properties;
using ABM.Services;
using System;
using System.Windows.Forms;

namespace ABM.LogIn
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            lblErrorMsg.Text = string.Empty;
            lblCountAttempts.Text = string.Empty;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string msj = string.Empty;

            if (loginValido(ref msj))
            {
                string password = EncryptHelper.Sha256Encrypt(txtPass.Text);
                string userName = txtUser.Text;

                DTO.LogIn login = UserServices.LoginUser(userName, password);

                if (login.LoginSuccess)
                {
                    if (login.Usuario.Roles.Count > 1)
                    {
                        var seleccionRol = new SelectRol { User = login.Usuario };
                        seleccionRol.ShowDialog();
                    }
                    else
                    {
                        var menuDialog = new Menu.MainMenu { User = login.Usuario };
                        menuDialog.ShowDialog();
                    }
                }
                else
                {
                    lblErrorMsg.Text = login.ErrorMessage;

                    if (login.Usuario != null && !login.Usuario.IsActive)
                    {
                        lblCountAttempts.Text = string.Empty;
                    }
                    else
                    {
                        if (login.Usuario != null)
                            lblCountAttempts.Text = Resources.IntentosRestantes + (3 - login.Usuario.CountFailedAttempts);
                    }
                }
            }
            else
                MessageBox.Show(msj);
        }

        private bool loginValido(ref string msj)
        {
            ValidationServices v = new ValidationServices();

            v.Validations.Add(new ValidationServices.Validation { condition = !string.IsNullOrEmpty(txtUser.Text), msj = "Debe ingresar el usuario." });
            v.Validations.Add(new ValidationServices.Validation { condition = !string.IsNullOrEmpty(txtPass.Text), msj = "Debe ingresar la contraseña." });

            return v.validate(ref msj);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llblToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newUserDialog = new ABM_User.SaveUser();
            newUserDialog.ShowDialog();
        }
    }
}
