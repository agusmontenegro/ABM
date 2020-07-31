namespace ABM.LogIn
{
    partial class LogIn
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
            this.btnIn = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblCountAttempts = new System.Windows.Forms.Label();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.llblToRegister = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(35, 187);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(126, 39);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "Ingresar";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(212, 187);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(126, 39);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblCountAttempts
            // 
            this.lblCountAttempts.AutoSize = true;
            this.lblCountAttempts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountAttempts.Location = new System.Drawing.Point(32, 155);
            this.lblCountAttempts.Name = "lblCountAttempts";
            this.lblCountAttempts.Size = new System.Drawing.Size(89, 13);
            this.lblCountAttempts.TabIndex = 2;
            this.lblCountAttempts.Text = "CountAttempts";
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrorMsg.Location = new System.Drawing.Point(32, 142);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(72, 13);
            this.lblErrorMsg.TabIndex = 3;
            this.lblErrorMsg.Text = "ErrorMessage";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(32, 93);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(61, 13);
            this.lblPass.TabIndex = 4;
            this.lblPass.Text = "Contraseña";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(32, 38);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(43, 13);
            this.lblUser.TabIndex = 5;
            this.lblUser.Text = "Usuario";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(35, 109);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(303, 20);
            this.txtPass.TabIndex = 1;
            this.txtPass.PasswordChar = '*';
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(35, 54);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(303, 20);
            this.txtUser.TabIndex = 0;
            // 
            // llblToRegister
            // 
            this.llblToRegister.AutoSize = true;
            this.llblToRegister.Location = new System.Drawing.Point(32, 229);
            this.llblToRegister.Name = "llblToRegister";
            this.llblToRegister.Size = new System.Drawing.Size(60, 13);
            this.llblToRegister.TabIndex = 8;
            this.llblToRegister.TabStop = true;
            this.llblToRegister.Text = "Registrarse";
            this.llblToRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblToRegister_LinkClicked);
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 262);
            this.Controls.Add(this.llblToRegister);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.lblCountAttempts);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnIn);
            this.Name = "LogIn";
            this.Text = "Ingreso al sistema";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblCountAttempts;
        private System.Windows.Forms.Label lblErrorMsg;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.LinkLabel llblToRegister;
    }
}