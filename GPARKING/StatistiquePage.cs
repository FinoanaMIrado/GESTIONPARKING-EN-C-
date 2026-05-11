using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ScottPlot;
using ScottPlot.WinForms;

namespace GPARKING
{
    public partial class StatistiquePage : UserControl
    {
        private FormsPlot formsPlotBar;
        private FormsPlot formsPlotPie;
        private Panel statsPanel;
        private Panel chartsContainer;
        private System.Windows.Forms.Label headerTitle;
        private System.Windows.Forms.Label headerSubtitle;

        // Palette de couleurs centralisée
        private static class Palette
        {
            public static readonly System.Drawing.Color BgSurface = System.Drawing.Color.FromArgb(245, 245, 247);
            public static readonly System.Drawing.Color BgCard = System.Drawing.Color.White;
            public static readonly System.Drawing.Color TextPrimary = System.Drawing.Color.FromArgb(30, 30, 32);
            public static readonly System.Drawing.Color TextMuted = System.Drawing.Color.FromArgb(130, 130, 140);
            public static readonly System.Drawing.Color BorderLight = System.Drawing.Color.FromArgb(220, 220, 225);

            public static readonly System.Drawing.Color Red = System.Drawing.Color.FromArgb(197, 48, 48);
            public static readonly System.Drawing.Color Green = System.Drawing.Color.FromArgb(39, 103, 73);
            public static readonly System.Drawing.Color Blue = System.Drawing.Color.FromArgb(24, 95, 165);
            public static readonly System.Drawing.Color Purple = System.Drawing.Color.FromArgb(85, 60, 154);
            public static readonly System.Drawing.Color Teal = System.Drawing.Color.FromArgb(15, 110, 86);
        }

        public StatistiquePage()
        {
            InitializeComponent();
            
            this.BackColor = Palette.BgSurface;
            this.Padding = new Padding(100);

            BuildLayout();
            LoadDataAndCharts();
        }

        private void BuildLayout()
        {
            // En-tête
            headerSubtitle = new System.Windows.Forms.Label
            {
                Text = "Gestion des parkings",
                Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Regular),
                ForeColor = Palette.TextMuted,
                AutoSize = true,
                Location = new Point(0, 0)
            };

            headerTitle = new System.Windows.Forms.Label
            {
                Text = "Statistiques",
                Font = new System.Drawing.Font("Segoe UI", 18f, System.Drawing.FontStyle.Regular),
                ForeColor = Palette.TextPrimary,
                AutoSize = true,
                Location = new Point(0, 20)
            };

            panelContent.Controls.Add(headerSubtitle);
            panelContent.Controls.Add(headerTitle);

            // Zone KPI
            statsPanel = new Panel
            {
                Size = new Size(1330, 100),
                Location = new Point(0, 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panelContent.Controls.Add(statsPanel);

            // Zone graphiques
            chartsContainer = new Panel
            {
                Size = new Size(1330, 340),
                Location = new Point(0, 170),
                BackColor = System.Drawing.Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            formsPlotBar = new FormsPlot
            {
                Size = new Size(630, 320),
                Location = new Point(0, 0)
            };
            StylePlotPanel(formsPlotBar, "Places par statut");

            formsPlotPie = new FormsPlot
            {
                Size = new Size(630, 320),
                Location = new Point(660, 0)
            };
            StylePlotPanel(formsPlotPie, "Répartition de l'occupation");

            chartsContainer.Controls.Add(formsPlotBar);
            chartsContainer.Controls.Add(formsPlotPie);
            panelContent.Controls.Add(chartsContainer);
        }

        // Applique un fond carte avec bordure subtile au FormsPlot
        private void StylePlotPanel(FormsPlot plot, string title)
        {
            plot.BackColor = Palette.BgCard;
            plot.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#FFFFFF");
            plot.Plot.DataBackground.Color = ScottPlot.Color.FromHex("#FFFFFF");

            plot.Plot.Axes.Color(ScottPlot.Color.FromHex("#828284"));
            plot.Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#808080");
            plot.Plot.Title(title, size: 13);
        }

        private Panel CreateKpiCard(string label, string value, System.Drawing.Color accentColor, int x)
        {
            var card = new Panel
            {
                Size = new Size(190, 72),
                Location = new Point(x, 0),
                BackColor = Palette.BgCard
            };
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var pen = new Pen(Palette.BorderLight, 1f);
                g.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                using var accentBrush = new SolidBrush(accentColor);
                g.FillRectangle(accentBrush, 0, 0, 4, card.Height);
            };

            var lblLabel = new System.Windows.Forms.Label
            {
                Text = label,
                Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Regular),
                ForeColor = Palette.TextMuted,
                AutoSize = false,
                Size = new Size(178, 18),
                Location = new Point(12, 14),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblValue = new System.Windows.Forms.Label
            {
                Text = value,
                Font = new System.Drawing.Font("Segoe UI", 18f, System.Drawing.FontStyle.Regular),
                ForeColor = accentColor,
                AutoSize = false,
                Size = new Size(178, 32),
                Location = new Point(12, 32),
                TextAlign = ContentAlignment.MiddleLeft
            };

            card.Controls.Add(lblLabel);
            card.Controls.Add(lblValue);
            return card;
        }

        private void LoadDataAndCharts()
        {
            try
            {
                var BD = ConnexionBD.Instance();
                if (!BD.IsConnect())
                {
                    MessageBox.Show("Erreur de connexion à la base de données.");
                    return;
                }

                var conn = BD.GetConnection();
                int totalPlaces = 24;
                //int totalPlaces = 24 - ExecScalar(conn, "SELECT COUNT(*) FROM place");
                int occupiedPlaces = ExecScalar(conn,
                    "SELECT COUNT(*) FROM place WHERE Etat IN ('occupée','occupee')");
                int freePlaces = totalPlaces - occupiedPlaces;
                int totalVehicles = ExecScalar(conn, "SELECT COUNT(*) FROM vehicule");
                int occupiedSpots = ExecScalar(conn,
                    "SELECT COUNT(DISTINCT NumPlace) FROM place WHERE NumPlace IS NOT NULL");
                int rate = totalPlaces > 0
                    ? (int)Math.Round(occupiedPlaces * 100.0 / totalPlaces)
                    : 0;

                // KPI cards
                statsPanel.Controls.Clear();
                var cards = new (string Label, string Value, System.Drawing.Color Color)[]
                {
                    ("Places occupées",   occupiedPlaces.ToString(), Palette.Red),
                    ("Places libres",     freePlaces.ToString(),     Palette.Blue),
                    //("Parkings occupés",  occupiedSpots.ToString(),  Palette.Green),
                    //("Véhicules",         totalVehicles.ToString(),  Palette.Purple),
                    ("Total places",      totalPlaces.ToString(),    Palette.TextPrimary),
                    ("Taux d'occupation", $"{rate}%",               Palette.Teal),
                };
                for (int i = 0; i < cards.Length; i++)
                    statsPanel.Controls.Add(
                        CreateKpiCard(cards[i].Label, cards[i].Value, cards[i].Color, i * 205));

                // Graphiques
                if (occupiedPlaces > 0 || freePlaces > 0)
                    DrawCharts(new[] { "Occupées", "Libres" },
                               new double[] { occupiedPlaces, freePlaces });
                else
                    DrawCharts(new[] { "Aucune donnée" }, new double[] { 1 });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement : " + ex.Message);
            }
        }

        private static int ExecScalar(MySqlConnection conn, string sql)
        {
            using var cmd = new MySqlCommand(sql, conn);
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        private void DrawCharts(string[] labels, double[] values)
        {
            // --- Bar chart ---
            formsPlotBar.Plot.Clear();
            var bars = formsPlotBar.Plot.Add.Bars(values);
            var barColors = new[]
            {
                ScottPlot.Color.FromHex("#C53030"),
                ScottPlot.Color.FromHex("#6495ED"),
                ScottPlot.Color.FromHex("#185FA5"),
                ScottPlot.Color.FromHex("#553C9A"),
                ScottPlot.Color.FromHex("#0F6E56"),
            };
            for (int i = 0; i < bars.Bars.Count && i < barColors.Length; i++)
            {
                bars.Bars[i].FillColor = barColors[i];
                bars.Bars[i].LineWidth = 0;
            }
            formsPlotBar.Plot.YLabel("Nombre de places");
            formsPlotBar.Plot.XLabel("Statut");
            formsPlotBar.Refresh();

            // --- Pie chart ---
            formsPlotPie.Plot.Clear();
            var pie = formsPlotPie.Plot.Add.Pie(values);
            for (int i = 0; i < pie.Slices.Count && i < barColors.Length; i++)
                pie.Slices[i].FillColor = barColors[i];
            formsPlotPie.Refresh();
        }

        private void panelContent_Paint(object sender, PaintEventArgs e) { }
    }
}