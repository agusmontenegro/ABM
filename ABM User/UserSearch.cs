using ABM.DTO;
using ABM.Helper;
using ABM.Properties;
using ABM.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ABM.ABM_User
{
    public partial class UserSearch : Form
    {
        public User User { get; set; }

        public UserSearch(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void UserSearch_Load(object sender, EventArgs e)
        {
            btnSearch.Enabled = User.RolActivo.Funcionalities.Any(f => f.Description.Equals(Resources.UsuarioBusqueda, StringComparison.CurrentCultureIgnoreCase));
            btnNew.Enabled = User.RolActivo.Funcionalities.Any(f => f.Description.Equals(Resources.UsuarioAlta, StringComparison.CurrentCultureIgnoreCase));
            btnEdit.Enabled = User.RolActivo.Funcionalities.Any(f => f.Description.Equals(Resources.UsuarioBaja, StringComparison.CurrentCultureIgnoreCase));
            btnDelete.Enabled = User.RolActivo.Funcionalities.Any(f => f.Description.Equals(Resources.UsuarioModificacion, StringComparison.CurrentCultureIgnoreCase));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = string.Empty;
            txtMail.Text = string.Empty;
            chkIsActive.Checked = false;
            lblResult.Text = "Resultado:";
            grdResult.DataSource = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            DataBaseHelper db = new DataBaseHelper(ConfigurationManager.AppSettings["connectionString"]);

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                SqlParameter usernameParameter = new SqlParameter("@username", SqlDbType.NVarChar);
                usernameParameter.Value = txtUsername.Text.Trim();
                parameters.Add(usernameParameter);
            }

            if (string.IsNullOrEmpty(txtMail.Text))
            {
                SqlParameter mailParameter = new SqlParameter("@mail", SqlDbType.NVarChar);
                mailParameter.Value = txtMail.Text.Trim();
                parameters.Add(mailParameter);
            }

            SqlParameter activeParameter = new SqlParameter("@isActive", SqlDbType.Bit);
            activeParameter.Value = chkIsActive.Checked;
            parameters.Add(activeParameter);

            List<User> users = UserServices.Search(parameters, db);
        }
    }
}
