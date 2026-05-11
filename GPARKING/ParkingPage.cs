using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Guna.UI2.WinForms;

namespace GPARKING
{
    public partial class ParkingPage : UserControl
    {
        public ParkingPage()
        {
            InitializeComponent();
            this.Load += ParkingPage_Load!;
        }

        private void ParkingPage_Load(object? sender, EventArgs e)
        {
            ChargerEtatPlaces();
        }

        private void afficheo_ary(Guna2Button btn, int numero)
        {
            try
            {
                //if (PlaceOccupee(numero))
                //{
                //    MessageBox.Show("Place déjà occupée !");
                //    return;
                //}

                popupadd p = new popupadd(this, numero);
                p.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        public void SetEtatPlace(int numero, string etat)
        {
            var btn = this.Controls.Find("p" + numero, true).FirstOrDefault() as Control;

            if (btn != null)
            {
                string e = etat.Trim().ToLower();

                if (e == "occupée" || e == "occupee")
                {
                    //btn.BackColor = Color.Red;
                    btn.Enabled = false;
                }
                else
                {
                    //btn.BackColor = Color.Green;
                    btn.Enabled = true;
                }
            }
        }

        private bool PlaceOccupee(int numero)
        {
            var BD = ConnexionBD.Instance();

            if (!BD.IsConnect()) return true;

            MySqlConnection conn = BD.GetConnection();

            string sql = "SELECT Etat FROM place WHERE NumPlace=@num";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", numero);

            var result = cmd.ExecuteScalar();


            if (result == null)
                return false;

            string etat = result.ToString().Trim().ToLower();

            return etat == "occupée";
        }

        private void ChargerEtatPlaces()
        {
            var BD = ConnexionBD.Instance();


            if (!BD.IsConnect()) return;

            MySqlConnection conn = BD.GetConnection();

            string sql = "SELECT NumPlace, Etat FROM place";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int num = Convert.ToInt32(reader["NumPlace"]);
                string? etat = reader["Etat"]?.ToString();

                if (etat != null)
                {
                    SetEtatPlace(num, etat);
                }
            }

            reader.Close();
        }

        private void p1_Click(object sender, EventArgs e) { afficheo_ary(p1, 1); }
        private void p2_Click(object sender, EventArgs e) { afficheo_ary(p2, 2); }
        private void p3_Click(object sender, EventArgs e) { afficheo_ary(p3, 3); }
        private void p4_Click(object sender, EventArgs e) { afficheo_ary(p4, 4); }
        private void p5_Click(object sender, EventArgs e) { afficheo_ary(p5, 5); }
        private void p6_Click(object sender, EventArgs e) { afficheo_ary(p6, 6); }
        private void p7_Click(object sender, EventArgs e) { afficheo_ary(p7, 7); }
        private void p8_Click(object sender, EventArgs e) { afficheo_ary(p8, 8); }
        private void p9_Click(object sender, EventArgs e) { afficheo_ary(p9, 9); }
        private void p10_Click(object sender, EventArgs e) { afficheo_ary(p10, 10); }
        private void p11_Click(object sender, EventArgs e) { afficheo_ary(p11, 11); }
        private void p12_Click(object sender, EventArgs e) { afficheo_ary(p12, 12); }
        private void p13_Click(object sender, EventArgs e) { afficheo_ary(p13, 13); }
        private void p14_Click(object sender, EventArgs e) { afficheo_ary(p14, 14); }
        private void p15_Click(object sender, EventArgs e) { afficheo_ary(p15, 15); }
        private void p16_Click(object sender, EventArgs e) { afficheo_ary(p16, 16); }
        private void p17_Click(object sender, EventArgs e) { afficheo_ary(p17, 17); }
        private void p18_Click(object sender, EventArgs e) { afficheo_ary(p18, 18); }
        private void p19_Click(object sender, EventArgs e) { afficheo_ary(p19, 19); }
        private void p20_Click(object sender, EventArgs e) { afficheo_ary(p20, 20); }
        private void p21_Click(object sender, EventArgs e) { afficheo_ary(p21, 21); }
        private void p22_Click(object sender, EventArgs e) { afficheo_ary(p22, 22); }
        private void p23_Click(object sender, EventArgs e) { afficheo_ary(p23, 23); }
        private void p24_Click(object sender, EventArgs e) { afficheo_ary(p24, 24); }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}