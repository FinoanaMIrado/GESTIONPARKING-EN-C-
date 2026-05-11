using Google.Protobuf.WellKnownTypes;
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
    public partial class add : Form
    {
        private int numeroPlace;
        private ParkingPage parent;
        public add(ParkingPage p, int num)
        {
            InitializeComponent();
            numeroPlace = num;
            parent = p;
            
        }

        private void txtNumClient_TextChanged(object sender, EventArgs e)
        {

        }
        private void ajouter_Click(object sender, EventArgs e)
        {
            try
            {
                var BD = ConnexionBD.Instance();

                if (!BD.IsConnect())
                {
                    MessageBox.Show("Connexion échouée");
                    return;
                }

                string queryClient = "INSERT INTO client(nom, prenom,Adresse) VALUES(@nom,@prenom,@adresse)";
                MySqlCommand cmdClient = new MySqlCommand(queryClient, BD.GetConnection());
                cmdClient.Parameters.AddWithValue("@nom", NomCli.Text);
                cmdClient.Parameters.AddWithValue("@prenom", PrenomCli.Text);
                cmdClient.ExecuteNonQuery();

                long idClient = cmdClient.LastInsertedId;

                string queryVehicule = "INSERT INTO vehicule(matricule, idClient) VALUES(@mat,@idc)";
                MySqlCommand cmdVehicule = new MySqlCommand(queryVehicule, BD.GetConnection());
                cmdVehicule.Parameters.AddWithValue("@mat", textImmat.Text);
                cmdVehicule.Parameters.AddWithValue("@idc", idClient);
                cmdVehicule.ExecuteNonQuery();

                long idVehicule = cmdVehicule.LastInsertedId;

                string queryPlace = "INSERT INTO place(numero, idVehicule) VALUES(@num,@idv)";
                MySqlCommand cmdPlace = new MySqlCommand(queryPlace, BD.GetConnection());
                cmdPlace.Parameters.AddWithValue("@num", numeroPlace);
                cmdPlace.Parameters.AddWithValue("@idv", idVehicule);
                cmdPlace.ExecuteNonQuery();

                MessageBox.Show("Ajout réussi");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

    }
}
