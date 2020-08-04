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
            EncryptHelper eh = new EncryptHelper();

            if (loginValido(ref msj))
            {
                string password = eh.Sha256Encrypt(txtPass.Text.Trim());
                string userName = txtUser.Text;

                DTO.LogIn login = UserServices.LoginUser(userName, password);

                if (login.LoginSuccess)
                {
                    if (login.Usuario.Roles.Count > 1)
                    {
                        var seleccionRol = new SelectRol(login.Usuario);
                        seleccionRol.ShowDialog();
                    }
                    else
                    {
                        login.Usuario.RolActivo = login.Usuario.Roles[0];
                        var menuDialog = new Menu.MainMenu(login.Usuario);
                        menuDialog.ShowDialog();
                    }
                    Close();
                }
                else
                {
                    lblErrorMsg.Text = login.ErrorMessage;
                    txtPass.Text = string.Empty;

                    if (login.Usuario != null)
                        lblCountAttempts.Text = Resources.IntentosRestantes + (3 - login.Usuario.CountFailedAttempts);
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
            txtUser.Text = string.Empty;
            txtPass.Text = string.Empty;
            lblErrorMsg.Text = string.Empty;
            lblCountAttempts.Text = string.Empty;

            var newUserDialog = new ABM_User.SaveUser(true);
            newUserDialog.ShowDialog();
        }
    }
}
