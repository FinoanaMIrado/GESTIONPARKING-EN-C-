using MySql.Data.MySqlClient;

using MySql.Data.MySqlClient;
using System;

namespace GPARKING
{
    public class ConnexionBD
    {
        public static ConnexionBD instance;

        public string Server { get; set; }
        public string NomDB { get; set; }
        public string NomUser { get; set; }
        public string Mdp { get; set; }

        private MySqlConnection Connection;

       
        public static ConnexionBD Instance()
        {
            if (instance == null)
                instance = new ConnexionBD();

            return instance;
        }

        
        public bool IsConnect()
        {
            try
            {
                string connStr =
                    $"server={Server};database={NomDB};uid={NomUser};pwd={Mdp};";

                Connection = new MySqlConnection(connStr);
                Connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erreur connexion : " + ex.Message);
                return false;
            }
        }

        // Vérification login
        public bool Verifie(string nom, string mdp)
        {   
            try
            {
                string query =
                    "SELECT COUNT(*) FROM utilisateur WHERE nomUser=@nom AND md=@mdp";

                using (MySqlCommand cmd = new MySqlCommand(query, Connection))
                {
                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@mdp", mdp);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erreur SQL : " + ex.Message);
                return false;
            }
        }

        // Fermeture BD
        public void Close()
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }
        public MySqlConnection GetConnection()
        {
            return Connection;
        }
    }



}