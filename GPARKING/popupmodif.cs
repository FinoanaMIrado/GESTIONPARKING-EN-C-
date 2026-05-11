using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GPARKING
{
    public partial class popupmodif : Form
    {
        private int codeCli;
        public popupmodif(int id = 0)
        {
            InitializeComponent();
            this.codeCli = id;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var DB = ConnexionBD.Instance().GetConnection();
            if (DB.State == System.Data.ConnectionState.Closed) DB.Open();

            MySqlTransaction transaction = DB.BeginTransaction();

            try
            {
                string queryClient = @"UPDATE client SET 
                                nom = @nom, 
                                prenom = @prenom, 
                                adresse = @adr 
                              WHERE codeCli = @id";

                using (MySqlCommand cmdClient = new MySqlCommand(queryClient, DB, transaction))
                {
                    cmdClient.Parameters.AddWithValue("@nom", NomCli.Text.Trim());
                    cmdClient.Parameters.AddWithValue("@prenom", PrenomCli.Text.Trim());
                    cmdClient.Parameters.AddWithValue("@adr", Adresse.Text.Trim());
                    cmdClient.Parameters.AddWithValue("@id", this.codeCli);
                    cmdClient.ExecuteNonQuery();
                }

                string queryVehicule = @"UPDATE vehicule SET  
                                 marque = @marque, 
                                 couleur = @coul 
                               WHERE codeCli = @id";

                using (MySqlCommand cmdVehicule = new MySqlCommand(queryVehicule, DB, transaction))
                {
                    //cmdVehicule.Parameters.AddWithValue("@immat", textImmat.Text.Trim());
                    cmdVehicule.Parameters.AddWithValue("@marque", Marque.Text.Trim());
                    cmdVehicule.Parameters.AddWithValue("@coul", couleur.Text.Trim());
                    cmdVehicule.Parameters.AddWithValue("@id", this.codeCli);
                    cmdVehicule.ExecuteNonQuery();
                }

                transaction.Commit();
                transaction.Dispose();

                MessageBox.Show("Client mis à jour !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Erreur lors de la mise à jour : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textImmat_Load(object sender, EventArgs e)
        {
            var BD = ConnexionBD.Instance().GetConnection();

            string query = "SELECT * FROM client INNER JOIN vehicule ON client.codeCli = vehicule.codeCli  WHERE client.codeCli = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, BD))
            {
                cmd.Parameters.AddWithValue("@id", this.codeCli);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtNumClient.Text = reader["CodeCli"].ToString();
                        textImmat.Text = reader["matricule"].ToString();
                        NomCli.Text = reader["nom"].ToString();
                        PrenomCli.Text = reader["prenom"].ToString();
                        Adresse.Text = reader["adresse"].ToString();
                        Marque.Text = reader["marque"].ToString();
                        couleur.Text = reader["couleur"].ToString();

                    }
                }
            }
        }

    }
}
