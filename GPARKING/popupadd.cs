using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GPARKING
{
    public partial class popupadd : Form
    {
        private int numeroPlace;
        private ParkingPage parent;

        public popupadd(ParkingPage p, int num)
        {
            InitializeComponent();
            numeroPlace = num;
            parent = p;
        }

        private void ajouter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NomCli.Text) ||
                string.IsNullOrWhiteSpace(PrenomCli.Text) ||
                string.IsNullOrWhiteSpace(textImmat.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }

            try
            {
                var BD = ConnexionBD.Instance();

                if (!BD.IsConnect())
                {
                    MessageBox.Show("Connexion échouée");
                    return;
                }

                MySqlConnection conn = BD.GetConnection();

                // vérifier si déjà occupée
                string checkPlace = "SELECT Etat FROM place WHERE NumPlace=@num";
                MySqlCommand cmdCheck = new MySqlCommand(checkPlace, conn);
                cmdCheck.Parameters.AddWithValue("@num", numeroPlace);

                var etat = cmdCheck.ExecuteScalar();

                if (etat != null && etat.ToString() == "Occupée")
                {
                    MessageBox.Show("Place déjà occupée !");
                    return;
                }

                // CLIENT
                string clientSql = "INSERT INTO client(Nom, Prenom, Adresse) VALUES(@n,@p,@a)";
                MySqlCommand cmdClient = new MySqlCommand(clientSql, conn);
                cmdClient.Parameters.AddWithValue("@n", NomCli.Text.Trim());
                cmdClient.Parameters.AddWithValue("@p", PrenomCli.Text.Trim());
                cmdClient.Parameters.AddWithValue("@a", Adresse.Text.Trim());
                cmdClient.ExecuteNonQuery();

                long idClient = cmdClient.LastInsertedId;
                // VEHICULE ca va nambarany
                string vehiculeSql = "INSERT INTO vehicule(Matricule, Marque, Couleur, CodeCli) VALUES(@m,@ma,@c,@id)";
                MySqlCommand cmdVehicule = new MySqlCommand(vehiculeSql, conn);

                cmdVehicule.Parameters.AddWithValue("@m", textImmat.Text.Trim().ToUpper());
                cmdVehicule.Parameters.AddWithValue("@ma", Marque.Text.Trim());
                cmdVehicule.Parameters.AddWithValue("@c", couleur.Text.Trim());
                cmdVehicule.Parameters.AddWithValue("@id", idClient);

                cmdVehicule.ExecuteNonQuery();


                string updatePlace =
                    "UPDATE place SET Etat='Occupée', Matricule=@mat WHERE NumPlace=@num";

                MySqlCommand cmdPlace = new MySqlCommand(updatePlace, conn);
                cmdPlace.Parameters.AddWithValue("@mat", textImmat.Text.Trim().ToUpper());
                cmdPlace.Parameters.AddWithValue("@num", numeroPlace);

                int rows = cmdPlace.ExecuteNonQuery();

                // place n'existe pas
                if (rows == 0)
                {
                    string insertPlace =
                        "INSERT INTO place(NumPlace, Etat, Matricule) VALUES(@num,'Occupée',@mat)";

                    MySqlCommand cmdInsert = new MySqlCommand(insertPlace, conn);
                    cmdInsert.Parameters.AddWithValue("@num", numeroPlace);
                    cmdInsert.Parameters.AddWithValue("@mat", textImmat.Text.Trim().ToUpper());
                    cmdInsert.ExecuteNonQuery();
                }

                parent.SetEtatPlace(numeroPlace, "Occupée");

                // TICKET - Insérer dans la table ticket
                DateTime heureEntree = DateTime.Now;

                string ticketSql = "INSERT INTO ticket(HeureEntree,Matricule, NumPlace, CodeCli) VALUES(@he,@m, @np, @cc)";
                MySqlCommand cmdTicket = new MySqlCommand(ticketSql, conn);
                cmdTicket.Parameters.AddWithValue("@he", heureEntree);
                cmdTicket.Parameters.AddWithValue("@m", textImmat.Text.Trim().ToUpper());
                cmdTicket.Parameters.AddWithValue("@np", numeroPlace);
                cmdTicket.Parameters.AddWithValue("@cc", idClient);

                cmdTicket.ExecuteNonQuery();
                long numTicket = cmdTicket.LastInsertedId;

                // Générer le ticket PDF
                try
                {
                    TicketGenerator.GenerateTicket(
                        numTicket.ToString(),
                        heureEntree,
                      
                        textImmat.Text.Trim().ToUpper(),
                        numeroPlace,
                        idClient.ToString()
                    );

                    MessageBox.Show("Ajout réussi et ticket généré");
                }
                catch (Exception ticketEx)
                {
                    MessageBox.Show($"Ajout réussi mais erreur lors de la génération du ticket: {ticketEx.Message}");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
        private void annuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void couleur_TextChanged(object sender, EventArgs e)
        {

        }
    }
}