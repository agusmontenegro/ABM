namespace ABM.LogIn
{
    partial class SelectRol
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
            this.cboRoles = new System.Windows.Forms.ComboBox();
            this.lblSelectRol = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboRoles
            // 
            this.cboRoles.FormattingEnabled = true;
            this.cboRoles.Location = new System.Drawing.Point(219, 32);
            this.cboRoles.Name = "cboRoles";
            this.cboRoles.Size = new System.Drawing.Size(267, 21);
            this.cboRoles.TabIndex = 0;
            // 
            // lblSelectRol
            // 
            this.lblSelectRol.AutoSize = true;
            this.lblSelectRol.Location = new System.Drawing.Point(12, 35);
            this.lblSelectRol.Name = "lblSelectRol";
            this.lblSelectRol.Size = new System.Drawing.Size(201, 13);
            this.lblSelectRol.TabIndex = 1;
            this.lblSelectRol.Text = "Seleccione rol con el cual desea ingresar";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(361, 74);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(125, 34);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "Ingresar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // SelectRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 132);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblSelectRol);
            this.Controls.Add(this.cboRoles);
            this.Name = "SelectRol";
            this.Text = "Seleccione algún rol";
            this.Load += new System.EventHandler(this.SelectRol_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRoles;
        private System.Windows.Forms.Label lblSelectRol;
        private System.Windows.Forms.Button btnAccept;
    }
}