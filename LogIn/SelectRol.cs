using ABM.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ABM.LogIn
{
    public partial class SelectRol : Form
    {
        public User User { get; set; }

        public SelectRol(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void SelectRol_Load(object sender, EventArgs e)
        {
            List<Rol> roles = new List<Rol>(User.Roles);

            cboRoles.DataSource = roles;
            cboRoles.DisplayMember = "Description";
            cboRoles.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

        }
    }
}