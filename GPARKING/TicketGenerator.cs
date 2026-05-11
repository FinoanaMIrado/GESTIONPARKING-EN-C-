using System;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GPARKING
{
    public class TicketGenerator
    {
        public static string GenerateTicket(
            string numTicket,
            DateTime heureEntree,
            string matricule,
            int numPlace,
            string codeCli)
        {
            try
            {
                // dossier tickets
                string ticketsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tickets");

                if (!Directory.Exists(ticketsFolder))
                    Directory.CreateDirectory(ticketsFolder);

                // nom fichier pdf
                string fileName = $"Ticket_{numTicket}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(ticketsFolder, fileName);

                // creation document pdf
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A6);
                        page.Margin(20);

                        page.Content().Column(col =>
                        {
                            col.Item().AlignCenter().Text("TICKET DE PARKING")
                                .Bold().FontSize(18);

                            col.Item().PaddingVertical(10).LineHorizontal(1);

                            col.Item().Text($"Num Ticket : {numTicket}");
                            col.Item().Text($"Code Client: {codeCli}");
                            col.Item().Text($"Matricule  : {matricule}");
                            col.Item().Text($"N° Place   : {numPlace}");

                            col.Item().PaddingVertical(5).LineHorizontal(1);

                            col.Item().Text($"Heure Entrée : {heureEntree:dd/MM/yyyy HH:mm:ss}");

                            col.Item().PaddingTop(15).AlignCenter()
                                .Text("Merci de votre visite")
                                .Italic();
                        });
                    });
                });

                // generation pdf
                document.GeneratePdf(filePath);

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur génération ticket PDF: {ex.Message}");
            }
        }
    }
}