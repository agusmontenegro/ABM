using ABM.ABM_User;
using ABM.DTO;
using ABM.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ABM.Menu
{
    public partial class MainMenu : Form
    {
        public User User { get; set; }

        public MainMenu(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            lblRol.Text = "Perfil: " + User.RolActivo.Description;
            btnABMUser.Enabled = User.RolActivo.Funcionalities.Any(f => f.Description.Equals(Resources.UsuarioBusqueda, StringComparison.CurrentCultureIgnoreCase));
        }

        private void btnABMUser_Click(object sender, EventArgs e)
        {
            var ABMUserDialog = new UserSearch(User);
            ABMUserDialog.ShowDialog();
        }
    }
}