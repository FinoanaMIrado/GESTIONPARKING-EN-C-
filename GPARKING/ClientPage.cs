using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace GPARKING
{
    public partial class ClientPage : UserControl
    {
        public ClientPage()
        {
            InitializeComponent();
            ChargerDonnee();
            var BD = ConnexionBD.Instance();

            if (!BD.IsConnect())
            {
                MessageBox.Show("Erreur de connexion à la base de données");
            }
        }
        private void recherche_btn_Click(object sender, EventArgs e)
        {
            string tediavina = rech_val.Text.Trim();
            //ChargerDonnee(tediavina);
        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void modif_btn_Click(object sender, EventArgs e)
        {
            string valueVals = vals.Text.Trim();
            if (!string.IsNullOrEmpty(valueVals))
            {
                if (!int.TryParse(valueVals, out int mod))
                {
                    MessageBox.Show("Le CodeCli doit être un nombre entier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var DB = ConnexionBD.Instance().GetConnection();
                if (DB.State == System.Data.ConnectionState.Closed) DB.Open();

                string query = "SELECT * FROM client WHERE codeCli = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, DB))
                {
                    cmd.Parameters.AddWithValue("@id", mod);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Ce client n'existe pas", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (popupmodif p = new popupmodif(mod))
                {
                    if (p.ShowDialog() == DialogResult.OK)
                    {
                        ChargerDonnee();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer le CodeCli (ID) du client à modifier.", "Attention",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Paye_Click(object sender, EventArgs e)
        {
            string valueVals = vals.Text.Trim();
            if (!string.IsNullOrEmpty(valueVals))
            {
                if (!int.TryParse(valueVals, out int mod))
                {
                    MessageBox.Show("Le CodeCli doit être un nombre entier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var DB = ConnexionBD.Instance().GetConnection();
                if (DB.State == System.Data.ConnectionState.Closed) DB.Open();

                string query = "SELECT * FROM client WHERE codeCli = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, DB))
                {
                    cmd.Parameters.AddWithValue("@id", mod);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Ce client n'existe pas", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (popuppaimaka p = new popuppaimaka(mod))
                {

                    if (p.ShowDialog() == DialogResult.OK)
                    {
                        ChargerDonnee();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer le CodeCli (ID) du client qui effectue le paiement", "Attention",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void ChargerDonnee()
        {
            var BD = ConnexionBD.Instance();

            if (BD.IsConnect())
            {
                string requete = @"
            SELECT 
            c.CodeCli AS 'Numero Client',
            c.Nom AS 'Nom',
            c.Prenom AS 'Prénom',
            c.Adresse AS 'Adresse',
            v.Matricule AS 'Matricule',
            v.Marque AS 'Marque',
            v.Couleur AS 'Couleur',
            COALESCE(t.NumPlace, '') AS 'Numéro Place'
        FROM CLIENT c
        LEFT JOIN VEHICULE v ON c.CodeCli = v.CodeCli
        LEFT JOIN TICKET t ON v.Matricule = t.Matricule 
        ORDER BY c.CodeCli;";
                try
                {
                    MySqlConnection connexion = BD.GetConnection();

                    if (connexion.State == System.Data.ConnectionState.Closed)
                        connexion.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(requete, connexion);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    Tableau_affichage.DataSource = dt;

                    // Vérification du nombre de lignes chargées
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Aucune donnée trouvée dans la base de données.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur de chargement : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Impossible de se connecter à la base");
            }
        }

        private void suppr_btn_Click(object sender, EventArgs e)
        {
            string zayy = vals.Text.Trim();

            if (string.IsNullOrEmpty(zayy))
            {
                MessageBox.Show("Veuillez entrer le CodeCli (ID) du client à supprimer.", "Attention",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(zayy, out int idClient))
            {
                MessageBox.Show("Erreur : Le CodeCli doit être un nombre entier.", "Erreur de format",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer le client dont l'ID est {idClient} ?\n\n" +
                "Cette action est irréversible.",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var BD = ConnexionBD.Instance();

                if (!BD.IsConnect())
                {
                    MessageBox.Show("Erreur de connexion à la base de données.");
                    return;
                }

                MySqlConnection conn = BD.GetConnection();

                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();

                //  matricules du client
                string getMatriculesSql = "SELECT Matricule FROM vehicule WHERE CodeCli = @id";
                MySqlCommand cmdGetMatricules = new MySqlCommand(getMatriculesSql, conn);
                cmdGetMatricules.Parameters.AddWithValue("@id", idClient);

                List<string> matricules = new List<string>();
                using (MySqlDataReader reader = cmdGetMatricules.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matricules.Add(reader["Matricule"].ToString());
                    }
                }

                //  Suppr tickets liés aux matricules
                if (matricules.Count > 0)
                {
                    foreach (string matricule in matricules)
                    {
                        string deleteTicketSql = "DELETE FROM ticket WHERE Matricule = @mat";
                        MySqlCommand cmdDeleteTicket = new MySqlCommand(deleteTicketSql, conn);
                        cmdDeleteTicket.Parameters.AddWithValue("@mat", matricule);
                        cmdDeleteTicket.ExecuteNonQuery();
                    }
                }

                // Récupérer  NumPlace client
                string getPlacesSql = "SELECT NumPlace FROM place WHERE Matricule IN (SELECT Matricule FROM vehicule WHERE CodeCli = @id)";
                MySqlCommand cmdGetPlaces = new MySqlCommand(getPlacesSql, conn);
                cmdGetPlaces.Parameters.AddWithValue("@id", idClient);

                List<int> numPlaces = new List<int>();
                using (MySqlDataReader reader = cmdGetPlaces.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        numPlaces.Add(Convert.ToInt32(reader["NumPlace"]));
                    }
                }

                //  Suppr places liées
                if (numPlaces.Count > 0)
                {
                    foreach (int numPlace in numPlaces)
                    {
                        string deletePlaceSql = "DELETE FROM place WHERE NumPlace = @np";
                        MySqlCommand cmdDeletePlace = new MySqlCommand(deletePlaceSql, conn);
                        cmdDeletePlace.Parameters.AddWithValue("@np", numPlace);
                        cmdDeletePlace.ExecuteNonQuery();
                    }
                }

                // Suppr véhicules
                string deleteVehiculeSql = "DELETE FROM vehicule WHERE CodeCli = @id";
                MySqlCommand cmdDeleteVehicule = new MySqlCommand(deleteVehiculeSql, conn);
                cmdDeleteVehicule.Parameters.AddWithValue("@id", idClient);
                cmdDeleteVehicule.ExecuteNonQuery();

                // Suppr client
                string deleteClientSql = "DELETE FROM client WHERE CodeCli = @id";
                MySqlCommand cmdDeleteClient = new MySqlCommand(deleteClientSql, conn);
                cmdDeleteClient.Parameters.AddWithValue("@id", idClient);
                int result = cmdDeleteClient.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Client et ses données associées supprimés avec succès !", "Succès",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    vals.Clear();
                    vals.Focus();

                    ChargerDonnee();
                }
                else
                {
                    MessageBox.Show("Aucun client trouvé avec l'ID : " + idClient, "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erreur SQL : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur inattendue : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tableau_affichage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void recherche_btn_Click_1(object sender, EventArgs e)
        {
            string Recherche = rech_val.Text.Trim();

            if (string.IsNullOrEmpty(Recherche))
            {
                MessageBox.Show("Veuillez entrer le nom du client ou le vehicule à rechercher! ", "Attention",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /* try
             {
                 var BD = ConnexionBD.Instance();
                 if (!BD.IsConnect()) return;

                 string sql = "SELECT client.CodeCli, client.Nom, vehicule.Immatriculation " +
                      "FROM client " +
                      "LEFT JOIN vehicule ON client.CodeCli = vehicule.CodeCli " +
                      "WHERE client.Nom = @r OR vehicule.matricule = @r ";

                 using (MySqlCommand cmd = new MySqlCommand(sql))

                 {
                     cmd.Parameters.AddWithValue("@r", Recherche);

                     using (MySqlDataReader reader = cmd.ExecuteReader())

                     {
                         if (reader.Read())
                         {
                             MySqlConnection connexion = BD.GetConnection();
                             MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connexion);
                             DataTable dt = new DataTable();

                             adapter.Fill(dt);

                             Tableau_affichage.DataSource = dt;
                         }
                         else MessageBox.Show("Non trouvé")
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Erreur lors de la recherche : {ex.Message}");
             }*/
            try
            {
                var BD = ConnexionBD.Instance();
                if (!BD.IsConnect()) return;


                MySqlConnection connexion = BD.GetConnection();

                string sql = "SELECT client.CodeCli, client.Nom, vehicule.Matricule " +
                             "FROM client " +
                             "LEFT JOIN vehicule ON client.CodeCli = vehicule.CodeCli " +
                             "WHERE client.Nom = @r OR vehicule.Matricule = @r";


                using (MySqlCommand cmd = new MySqlCommand(sql, connexion))
                {
                    cmd.Parameters.AddWithValue("@r", Recherche);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt); // L'adapter gère l'ouverture/fermeture si nécessaire

                        if (dt.Rows.Count > 0)
                        {
                            Tableau_affichage.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Aucun résultat trouvé.");
                            Tableau_affichage.DataSource = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche : {ex.Message}");
            }
        }
    }
    
}
