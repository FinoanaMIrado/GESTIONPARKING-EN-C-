using System;
using System.Windows.Forms;
using QuestPDF.Infrastructure;

namespace GPARKING
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // license QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            ApplicationConfiguration.Initialize();
            Application.Run(new login());
        }
    }
}