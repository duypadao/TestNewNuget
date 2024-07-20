using PDFCreator.Lab;
using PDFCreator.Lab.Component;
using PDFCreator.Lab.Model;
using PDFCreator.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Data.Common;
using System.Net.WebSockets;

static class Program
{
    [Obsolete]
    static void Main(string[] arg)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var report = ReportDataSource.GenerateRandomReportInfo();
        var document = new GarmentDocument(report);
        document.ShowInPreviewer();
    }
    
}