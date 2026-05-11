namespace GPARKING
{
    partial class login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelLogin = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            lblLoginTitle = new Label();
            guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            lblUsername = new Label();
            txtnom = new Guna.UI2.WinForms.Guna2TextBox();
            lblPassword = new Label();
            btnShowPassword = new Guna.UI2.WinForms.Guna2Button();
            txtmdp = new Guna.UI2.WinForms.Guna2TextBox();
            lblForgot = new LinkLabel();
            btnLogin = new Guna.UI2.WinForms.Guna2GradientButton();
            lblWelcome = new Label();
            label1 = new Label();
            panelLeft = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).BeginInit();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.White;
            panelLogin.BorderRadius = 30;
            panelLogin.Controls.Add(lblLoginTitle);
            panelLogin.Controls.Add(guna2CirclePictureBox1);
            panelLogin.Controls.Add(lblUsername);
            panelLogin.Controls.Add(txtnom);
            panelLogin.Controls.Add(lblPassword);
            panelLogin.Controls.Add(btnShowPassword);
            panelLogin.Controls.Add(txtmdp);
            panelLogin.Controls.Add(lblForgot);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.CustomizableEdges = customizableEdges8;
            panelLogin.Location = new Point(450, 0);
            panelLogin.Name = "panelLogin";
            panelLogin.ShadowDecoration.CustomizableEdges = customizableEdges9;
            panelLogin.Size = new Size(450, 600);
            panelLogin.TabIndex = 1;
            // 
            // lblLoginTitle
            // 
            lblLoginTitle.AutoSize = true;
            lblLoginTitle.BackColor = Color.Transparent;
            lblLoginTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLoginTitle.ForeColor = Color.FromArgb(52, 152, 219);
            lblLoginTitle.Location = new Point(111, 144);
            lblLoginTitle.Name = "lblLoginTitle";
            lblLoginTitle.Size = new Size(215, 30);
            lblLoginTitle.TabIndex = 0;
            lblLoginTitle.Text = "AUTHENTIFICATION";
            lblLoginTitle.Click += lblLoginTitle_Click;
            // 
            // guna2CirclePictureBox1
            // 
            guna2CirclePictureBox1.Image = (Image)resources.GetObject("guna2CirclePictureBox1.Image");
            guna2CirclePictureBox1.ImageRotate = 0F;
            guna2CirclePictureBox1.Location = new Point(126, 0);
            guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            guna2CirclePictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CirclePictureBox1.Size = new Size(172, 158);
            guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2CirclePictureBox1.TabIndex = 8;
            guna2CirclePictureBox1.TabStop = false;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.ForeColor = Color.FromArgb(64, 64, 64);
            lblUsername.Location = new Point(60, 198);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(114, 19);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Nom d'utilisateur";
            // 
            // txtnom
            // 
            txtnom.BorderRadius = 15;
            txtnom.CustomizableEdges = customizableEdges2;
            txtnom.DefaultText = "";
            txtnom.DisabledState.BorderColor = Color.FromArgb(226, 226, 226);
            txtnom.DisabledState.FillColor = Color.FromArgb(240, 240, 240);
            txtnom.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtnom.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtnom.FocusedState.BorderColor = Color.FromArgb(52, 152, 219);
            txtnom.Font = new Font("Segoe UI", 10F);
            txtnom.ForeColor = Color.Black;
            txtnom.HoverState.BorderColor = Color.FromArgb(52, 152, 219);
            txtnom.Location = new Point(56, 236);
            txtnom.Name = "txtnom";
            txtnom.PlaceholderText = "Entrez votre nom d'utilisateur";
            txtnom.SelectedText = "";
            txtnom.ShadowDecoration.CustomizableEdges = customizableEdges3;
            txtnom.Size = new Size(330, 40);
            txtnom.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.Location = new Point(56, 305);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(92, 19);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mot de passe";
            // 
            // btnShowPassword
            // 
            btnShowPassword.CustomizableEdges = customizableEdges4;
            btnShowPassword.FillColor = Color.Transparent;
            btnShowPassword.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnShowPassword.ForeColor = Color.FromArgb(52, 152, 219);
            btnShowPassword.Location = new Point(338, 342);
            btnShowPassword.Name = "btnShowPassword";
            btnShowPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnShowPassword.Size = new Size(48, 20);
            btnShowPassword.TabIndex = 9;
            btnShowPassword.Text = "👁️";
            btnShowPassword.Click += btnShowPassword_Click;
            // 
            // txtmdp
            // 
            txtmdp.BorderRadius = 15;
            txtmdp.CustomizableEdges = customizableEdges4;
            txtmdp.DefaultText = "";
            txtmdp.DisabledState.BorderColor = Color.FromArgb(226, 226, 226);
            txtmdp.DisabledState.FillColor = Color.FromArgb(240, 240, 240);
            txtmdp.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtmdp.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtmdp.FocusedState.BorderColor = Color.FromArgb(52, 152, 219);
            txtmdp.Font = new Font("Segoe UI", 10F);
            txtmdp.ForeColor = Color.Black;
            txtmdp.HoverState.BorderColor = Color.FromArgb(52, 152, 219);
            txtmdp.Location = new Point(56, 332);
            txtmdp.Name = "txtmdp";
            txtmdp.PasswordChar = '•';
            txtmdp.PlaceholderText = "Entrez votre mot de passe";
            txtmdp.SelectedText = "";
            txtmdp.ShadowDecoration.CustomizableEdges = customizableEdges5;
            txtmdp.Size = new Size(330, 40);
            txtmdp.TabIndex = 4;
            // 
            // lblForgot
            // 
            lblForgot.AutoSize = true;
            lblForgot.BackColor = Color.Transparent;
            lblForgot.Font = new Font("Segoe UI", 9F);
            lblForgot.LinkColor = Color.FromArgb(52, 152, 219);
            lblForgot.Location = new Point(268, 409);
            lblForgot.Name = "lblForgot";
            lblForgot.Size = new Size(118, 15);
            lblForgot.TabIndex = 6;
            lblForgot.TabStop = true;
            lblForgot.Text = "Mot de passe oublié?";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Transparent;
            btnLogin.BorderRadius = 15;
            btnLogin.CustomizableEdges = customizableEdges6;
            btnLogin.DisabledState.BorderColor = Color.FromArgb(191, 191, 191);
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(38, 38, 38);
            btnLogin.FillColor = Color.Blue;
            btnLogin.FillColor2 = Color.FromArgb(41, 128, 185);
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.HoverState.FillColor = Color.FromArgb(41, 128, 185);
            btnLogin.HoverState.FillColor2 = Color.FromArgb(30, 100, 150);
            btnLogin.Location = new Point(60, 486);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.Color = Color.FromArgb(52, 152, 219);
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges7;
            btnLogin.ShadowDecoration.Depth = 8;
            btnLogin.ShadowDecoration.Enabled = true;
            btnLogin.Size = new Size(330, 45);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "CONNEXION";
            btnLogin.Click += btnLogin_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(23, 144);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(409, 59);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "GESTION PARKING";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Font = new Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(175, 284);
            label1.Name = "label1";
            label1.Size = new Size(78, 88);
            label1.TabIndex = 1;
            label1.Text = "P";
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.Transparent;
            panelLeft.BorderRadius = 20;
            panelLeft.Controls.Add(label1);
            panelLeft.Controls.Add(lblWelcome);
            customizableEdges10.BottomLeft = false;
            customizableEdges10.TopLeft = false;
            panelLeft.CustomizableEdges = customizableEdges10;
            panelLeft.FillColor = Color.Blue;
            panelLeft.FillColor2 = Color.FromArgb(41, 128, 185);
            panelLeft.FillColor3 = Color.FromArgb(41, 128, 185);
            panelLeft.FillColor4 = Color.FromArgb(52, 152, 219);
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.ShadowDecoration.CustomizableEdges = customizableEdges11;
            panelLeft.Size = new Size(450, 600);
            panelLeft.TabIndex = 0;
            // 
            // login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 600);
            ControlBox = false;
            Controls.Add(panelLogin);
            Controls.Add(panelLeft);
            FormBorderStyle = FormBorderStyle.None;
            Name = "login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GPARKING - Connexion";
            Load += Form1_Load;
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).EndInit();
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2CustomGradientPanel panelLogin;
        private Label lblLoginTitle;
        private Label lblUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtnom;
        private Label lblPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtmdp;
        private Guna.UI2.WinForms.Guna2Button btnShowPassword;
        private LinkLabel lblForgot;
        private Guna.UI2.WinForms.Guna2GradientButton btnLogin;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Label lblWelcome;
        private Label label1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel panelLeft;
    }
}
