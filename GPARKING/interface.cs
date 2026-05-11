using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GPARKING
{    // Change this:

    public partial class InterfaceForm : Form
    {
        public InterfaceForm()
        {
            InitializeComponent();
        }


        private void ResetMenu()
        {
            parkingbtn.FillColor = Color.FromArgb(25, 45, 120);
            clientbtn.FillColor = Color.FromArgb(25, 45, 120);
            statistiquebtn.FillColor = Color.FromArgb(25, 45, 120);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Initialize with default parking page
            parkingbtn_Click(null, null);

            // Set button styles with icons
            UpdateButtonIcons();
            ApplyThemeColors();
        }

        private void UpdateButtonIcons()
        {
            // Set text with icons using Unicode symbols
            parkingbtn.Text = "🅿️  PARKING";
            clientbtn.Text = "👤  CLIENT";
            statistiquebtn.Text = "📊  STATISTIQUE";
            deconnectbtn.Text = "⏻  DÉCONNECTER";
        }

        private void ApplyThemeColors()
        {
            // Professional color scheme
            guna2CustomGradientPanel1.FillColor = Color.FromArgb(25, 45, 120);
            guna2CustomGradientPanel1.FillColor2 = Color.FromArgb(15, 30, 90);
            guna2CustomGradientPanel1.FillColor3 = Color.FromArgb(25, 45, 120);
            guna2CustomGradientPanel1.FillColor4 = Color.FromArgb(15, 30, 90);

            panel1.BackColor = Color.FromArgb(20, 40, 100);

            // Set button default colors
            parkingbtn.FillColor = Color.FromArgb(25, 45, 120);
            clientbtn.FillColor = Color.FromArgb(25, 45, 120);
            statistiquebtn.FillColor = Color.FromArgb(25, 45, 120);
            deconnectbtn.FillColor = Color.FromArgb(220, 53, 69);

            // Hover effects
            parkingbtn.HoverState.FillColor = Color.FromArgb(66, 106, 193);
            clientbtn.HoverState.FillColor = Color.FromArgb(66, 106, 193);
            statistiquebtn.HoverState.FillColor = Color.FromArgb(66, 106, 193);
            deconnectbtn.HoverState.FillColor = Color.FromArgb(255, 73, 87);
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ResetMenu();
            clientbtn.FillColor = Color.FromArgb(66, 106, 193);
            LoadPage(new ClientPage());
        }

        private void parkingbtn_Click(object sender, EventArgs e)
        {
            ResetMenu();
            parkingbtn.FillColor = Color.FromArgb(66, 106, 193);
            LoadPage(new ParkingPage());
        }

        private void statistiquebtn_Click(object sender, EventArgs e)
        {
            ResetMenu();
            statistiquebtn.FillColor = Color.FromArgb(66, 106, 193);
            LoadPage(new StatistiquePage());
        }
        //foction loadpage manano affichage eo am panel droite eo
        private void LoadPage(UserControl page)
        {
            panelMain.Controls.Clear();   // vider
            page.Dock = DockStyle.Fill;   // prendre toute la place
            panelMain.Controls.Add(page); // afficher
        }



        private void deconnectbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show
                (
                    "Voulez-vous se deconnecter ?",
                    "Confirmation",
                     MessageBoxButtons.OKCancel,
                     MessageBoxIcon.Question
                 );

            if (result == DialogResult.OK)
            {
                login f1 = new login();
                f1.Show();
                this.Hide();

            }
            else
            {

            }


        }



        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
