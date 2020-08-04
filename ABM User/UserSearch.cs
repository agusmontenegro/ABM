using ABM.DTO;
using ABM.Helper;
using ABM.Properties;
using ABM.Services;
using System;
using System.Configuration;
using System.Data;
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
            string query = "select user_id, user_username, user_count_failed_attempts, user_active from dbo.Users ";
            DataBaseHelper db = new DataBaseHelper(ConfigurationManager.AppSettings["connectionString"]);

            if (!string.IsNullOrEmpty(txtUsername.Text))
                query += "where user_username like '" + txtUsername.Text + "' ";

            if (!string.IsNullOrEmpty(txtMail.Text))
            {
                if (!query.Contains("where"))
                    query += "where user_mail like '" + txtMail.Text + "' ";
                else
                    query += "and user_mail like '" + txtMail.Text + "' ";
            }

            if (!query.Contains("where"))
                query += "where user_active = " + (chkIsActive.Checked ? "1" : "0");
            else
                query += "and user_active = " + (chkIsActive.Checked ? "1" : "0");

            DataSet users = UserServices.Search(query, db);

            grdResult.ReadOnly = true;
            grdResult.DataSource = users.Tables[0];
        }
    }
}
