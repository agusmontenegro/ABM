using ABM.ABM_User;
using ABM.DTO;
using ABM.Properties;
using ABM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ABM.Menu
{
    public partial class MainMenu : Form
    {
        public User User { get; set; }

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            List<Rol> roles = new List<Rol>(RolesServices.GetAllData());

            #region habilitacionSeccionABM
            Rol rolAdmin = roles.Find(rol => rol.Description.Equals(Resources.Administrativo, StringComparison.CurrentCultureIgnoreCase));

            #endregion

            #region habilitacionSecciones

            if (User.RolActivo != null)
            {
                btnABMUser.Enabled = User.RolActivo.Funcionalities.Any(f => f.Description.Equals(btnABMUser.Text, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                btnABMUser.Enabled = User.Roles.First().Funcionalities.Any(f => f.Description.Equals(btnABMUser.Text, StringComparison.CurrentCultureIgnoreCase));
            }
            #endregion
        }

        private void btnABMUser_Click(object sender, EventArgs e)
        {
            var ABMUserDialog = new UserSearch { User = User };
            ABMUserDialog.ShowDialog();
        }
    }
}
