using System;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GPARKING
{
    public class FactureGenerator
    {
        public static string GenerateFacture(
            string numFact,
            decimal montant,
            DateTime dateHeure,
            string numTicket)
        {
            try
            {
                // Dossier

                string folder = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Factures"
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                // Fichier

                string fileName =
                    $"Facture_{numFact}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                string filePath = Path.Combine(folder, fileName);

                // PDF

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A5);

                        page.Margin(20);

                        page.Content().Column(col =>
                        {
                            // Titre

                            col.Item()
                                .AlignCenter()
                                .Text("FACTURE PARKING")
                                .Bold()
                                .FontSize(18);

                            // Ligne

                            col.Item()
                                .PaddingVertical(10)
                                .LineHorizontal(1);

                            // Informations

                            col.Item()
                                .Text($"Num Facture : {numFact}");

                            col.Item()
                                .Text($"Num Ticket : {numTicket}");

                            // Ligne

                            col.Item()
                                .PaddingVertical(5)
                                .LineHorizontal(1);

                            // Date sortie

                            col.Item()
                                .Text($"Date Heure : {dateHeure:dd/MM/yyyy HH:mm:ss}");

                            // Montant

                            col.Item()
                                .Text($"Montant : {montant:0.00} Ar");

                            // Message

                            col.Item()
                                .PaddingTop(15)
                                .AlignCenter()
                                .Text("Merci pour votre paiement")
                                .Italic();
                        });
                    });
                });

                // Génération

                document.GeneratePdf(filePath);

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Erreur génération facture PDF : {ex.Message}"
                );
            }
        }
    }
}