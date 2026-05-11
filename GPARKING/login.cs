using System;
using System.Drawing;
using System.Windows.Forms;

namespace GPARKING
{
    public partial class login : Form
    {
        private bool isPasswordVisible = false;

        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "GPARKING - Connexion";
            this.BackColor = Color.White;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nom = txtnom.Text.Trim();
            string mdp = txtmdp.Text.Trim();

            var BD = ConnexionBD.Instance();

            BD.Server = "localhost";
            BD.NomDB = "gestionparking";
            BD.NomUser = "root";
            BD.Mdp = "";

            if (BD.IsConnect())
            {
                bool ok = BD.Verifie(nom, mdp);

                if (ok)
                {
                    //MessageBox.Show("Connexion réussie");

                    InterfaceForm f = new InterfaceForm();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nom ou mot de passe incorrect");
                }

                BD.Close();
            }
            else
            {
                MessageBox.Show("Impossible de se connecter à la base");
            }
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                txtmdp.PasswordChar = '\0';
                btnShowPassword.Text = "Masquer";
            }
            else
            {
                txtmdp.PasswordChar = '*';
                btnShowPassword.Text = "Afficher";
            }
        }

        private void lblLoginTitle_Click(object sender, EventArgs e)
        {

        }
    }
}