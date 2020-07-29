namespace ABM.Menu
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRol = new System.Windows.Forms.Label();
            this.btnABMUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Location = new System.Drawing.Point(54, 21);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(36, 13);
            this.lblRol.TabIndex = 0;
            this.lblRol.Text = "Perfil: ";
            // 
            // btnABMUser
            // 
            this.btnABMUser.Location = new System.Drawing.Point(57, 54);
            this.btnABMUser.Name = "btnABMUser";
            this.btnABMUser.Size = new System.Drawing.Size(161, 24);
            this.btnABMUser.TabIndex = 1;
            this.btnABMUser.Text = "ABM Usuario";
            this.btnABMUser.UseVisualStyleBackColor = true;
            this.btnABMUser.Click += new System.EventHandler(this.btnABMUser_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 146);
            this.Controls.Add(this.btnABMUser);
            this.Controls.Add(this.lblRol);
            this.Name = "MainMenu";
            this.Text = "Menú principal";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Button btnABMUser;
    }
}