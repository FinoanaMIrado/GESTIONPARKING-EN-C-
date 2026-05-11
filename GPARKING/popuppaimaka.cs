using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GPARKING
{
    public partial class popuppaimaka : Form
    {
        private string vals;

        public popuppaimaka(int val = 0)
        {
            InitializeComponent();
            date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            vals = val.ToString();

            var con = ConnexionBD.Instance().GetConnection();

            if (con.State != ConnectionState.Open)
                con.Open();

            string sql =
                "SELECT NumTicket FROM ticket " +
                "JOIN client ON ticket.CodeCli = client.CodeCli " +
                "WHERE client.CodeCli = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", val);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id.Text = reader["NumTicket"].ToString();
                    }
                }
            }

            con.Close();
        }

        private void no_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pay_btn_Click(object sender, EventArgs e)
        {
            var BD = ConnexionBD.Instance();
            MySqlConnection con = BD.GetConnection();

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                string sql =
                    "SELECT HeureEntree FROM ticket WHERE NumTicket = @a";

                DateTime entre;

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@a", int.Parse(id.Text));

                    var heureResult = cmd.ExecuteScalar();

                    if (heureResult == null || heureResult == DBNull.Value)
                    {
                        MessageBox.Show("Ticket introuvable !");
                        return;
                    }

                    entre = Convert.ToDateTime(heureResult);
                }

                DateTime sortie = DateTime.Now;

                TimeSpan difference = sortie - entre;

                int minutesTotales = (int)difference.TotalMinutes;

                if (minutesTotales < 0)
                {
                    MessageBox.Show("Erreur de date !");
                    return;
                }

                double tarifParHeure = 800;

                double montant =
                    (minutesTotales / 60.0) * tarifParHeure;

                string paiement =
                    "INSERT INTO paiement(DatePaiement, NumTicket) " +
                    "VALUES(@daty, @mimero)";

                long numPaiement = 0;

                using (MySqlCommand cmdpay =
                    new MySqlCommand(paiement, con))
                {
                    cmdpay.Parameters.AddWithValue("@daty", sortie);

                    cmdpay.Parameters.AddWithValue(
                        "@mimero",
                        int.Parse(id.Text)
                    );

                    cmdpay.ExecuteNonQuery();

                    numPaiement = cmdpay.LastInsertedId;
                }

                string insertFacture =
                    "INSERT INTO facture(Montant, DateHeure, NumPaiement) " +
                    "VALUES(@montant, @dateheure, @numpaiement)";

                long numFact = 0;

                using (MySqlCommand cmdFact =
                    new MySqlCommand(insertFacture, con))
                {
                    cmdFact.Parameters.AddWithValue("@montant", montant);

                    cmdFact.Parameters.AddWithValue(
                        "@dateheure",
                        sortie
                    );

                    cmdFact.Parameters.AddWithValue(
                        "@numpaiement",
                        numPaiement
                    );

                    cmdFact.ExecuteNonQuery();

                    numFact = cmdFact.LastInsertedId;
                }

                string cheminPdf =
                    FactureGenerator.GenerateFacture(
                        numFact.ToString(),
                        Convert.ToDecimal(montant),
                        sortie,
                        id.Text
                    );

                Montaka.Text = montant.ToString("0.00");

                MessageBox.Show(
                    "Paiement enregistré !" +
                    "\nMontant : " +
                    montant.ToString("0.00") +
                    " Ar" +
                    "\n\nFacture générée !" +
                    "\n\n" +
                    cheminPdf
                );

                string zayy = vals;

                if (string.IsNullOrEmpty(zayy))
                {
                    MessageBox.Show(
                        "CodeCli vide.",
                        "Attention",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (!int.TryParse(zayy, out int idClient))
                {
                    MessageBox.Show(
                        "CodeCli invalide.",
                        "Erreur",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                string getMatriculesSql =
                    "SELECT Matricule FROM vehicule " +
                    "WHERE CodeCli = @id";

                MySqlCommand cmdGetMatricules =
                    new MySqlCommand(getMatriculesSql, con);

                cmdGetMatricules.Parameters.AddWithValue(
                    "@id",
                    idClient
                );

                List<string> matricules = new List<string>();

                using (MySqlDataReader reader =
                    cmdGetMatricules.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matricules.Add(
                            reader["Matricule"].ToString()
                        );
                    }
                }

                foreach (string matricule in matricules)
                {
                    string deleteTicketSql =
                        "DELETE FROM ticket WHERE Matricule = @mat";

                    MySqlCommand cmdDeleteTicket =
                        new MySqlCommand(deleteTicketSql, con);

                    cmdDeleteTicket.Parameters.AddWithValue(
                        "@mat",
                        matricule
                    );

                    cmdDeleteTicket.ExecuteNonQuery();
                }

                string getPlacesSql =
                    "SELECT NumPlace FROM place " +
                    "WHERE Matricule IN " +
                    "(SELECT Matricule FROM vehicule " +
                    "WHERE CodeCli = @id)";

                MySqlCommand cmdGetPlaces =
                    new MySqlCommand(getPlacesSql, con);

                cmdGetPlaces.Parameters.AddWithValue(
                    "@id",
                    idClient
                );

                List<int> numPlaces = new List<int>();

                using (MySqlDataReader reader =
                    cmdGetPlaces.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        numPlaces.Add(
                            Convert.ToInt32(reader["NumPlace"])
                        );
                    }
                }

                foreach (int numPlace in numPlaces)
                {
                    string deletePlaceSql =
                        "DELETE FROM place WHERE NumPlace = @np";

                    MySqlCommand cmdDeletePlace =
                        new MySqlCommand(deletePlaceSql, con);

                    cmdDeletePlace.Parameters.AddWithValue(
                        "@np",
                        numPlace
                    );

                    cmdDeletePlace.ExecuteNonQuery();
                }

                string deleteVehiculeSql =
                    "DELETE FROM vehicule WHERE CodeCli = @id";

                MySqlCommand cmdDeleteVehicule =
                    new MySqlCommand(deleteVehiculeSql, con);

                cmdDeleteVehicule.Parameters.AddWithValue(
                    "@id",
                    idClient
                );

                cmdDeleteVehicule.ExecuteNonQuery();

                string deleteClientSql =
                    "DELETE FROM client WHERE CodeCli = @id";

                MySqlCommand cmdDeleteClient =
                    new MySqlCommand(deleteClientSql, con);

                cmdDeleteClient.Parameters.AddWithValue(
                    "@id",
                    idClient
                );

                int deleteResult =
                    cmdDeleteClient.ExecuteNonQuery();

                if (deleteResult > 0)
                {
                    MessageBox.Show(
                        "Suppression réussie !",
                        "Succès",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Client introuvable.",
                        "Info",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    "Erreur SQL : " + ex.Message,
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erreur : " +
                    ex.Message +
                    "\n\n" +
                    ex.StackTrace
                );
            }
            finally
            {
                con.Close();
            }

            this.Close();
        }
    }
}