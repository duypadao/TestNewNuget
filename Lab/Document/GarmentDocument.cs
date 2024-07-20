using PDFCreator.Lab.Component;
using PDFCreator.Lab.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data.Common;
using System.Net.Http.Headers;

public class GarmentDocument : IDocument
{
    public ReportInfo report { get; }
    public static class GarmentColor
    {
        public static string LabelTextColor = "#16365C";
        public static class LabelBackGroundColor
        {
            public static string Applicant = "#FFFFFF";
            public static string Report = "#F2F2F2";
        }
        public static string ValueTextColor = Colors.Grey.Darken2.ToString();
        public static string ValueBackGroundColor = Colors.White.ToString();
    }
    public GarmentDocument(ReportInfo reportInfo)
    {
        report = reportInfo;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(5);
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Calibri"));
            page.Margin(15);
            CreateHeader(page);
            CreateContent(page);
        });
    }
    void CreateHeader(PageDescriptor page)
    {
        byte[] logoImage = File.ReadAllBytes("D:\\Darius folder\\TestNewNuget\\Image\\logo.png");
        page.Header()
            //.Background(Colors.Grey.Darken2)
            .BorderBottom(0.5f)
            .BorderColor(Colors.Black)
            .Container()
            .TranslateY(-2)
            .BorderBottom(0.5f)
            .BorderColor(Colors.Black)
            .Row(row =>
            {
                row.ConstantItem(100).Image(logoImage);
                row.RelativeItem(2)
                   //.Background(Colors.Red.Accent4)
                   .Text(text =>
                   {
                       text.Span(report.ReportName);
                       text.DefaultTextStyle(x => x.FontSize(27).Bold());
                       text.AlignCenter();
                   });
                row.RelativeItem(1)
                   .PaddingBottom(5)
                   .AlignBottom()
                   .AlignRight()
                   .Column(column =>
                   {
                       column.Item().Text($"Report No: {report.ReportNo}").Bold();
                       column.Item().Text($"Received Date: {report.RecveicedDate}");
                       column.Item().Text($"Report Date: {report.ReportDate}");
                   });
            });
    }

    void CreateContent(PageDescriptor page)
    {
        page.Content()
            //.Background(Colors.Green.Darken1)
            .Column(column =>
            {
                AddSection(column, "1. REPORT INFORMATION");
                AddApplicationInfo(column);
                AddReportInformation(column);
                AddSection(column, "2. SUMMARY PAGE");
                AddSummaryTable(column);
            });
    }
    void AddSection(ColumnDescriptor column, string value)
    {
        var sectionBGColor = "#16365C";
        column
            .Item()
            .PaddingTop(15)
            .Background(sectionBGColor)
            .PaddingHorizontal(5)
            .Text(value)
            .FontSize(14)
            .FontColor(Colors.White);
    }

    void AddApplicationInfo(ColumnDescriptor column)
    {
        var applicationInfomation = new (string, string)[]
        {
            ("Department:", report.Department),
            ("Applicant:", report.Applicant),
            ("Email:", report.Email),
            ("Tel:", report.Tel),
        };
        column
            .Item()
            .Row(row =>
            {
                row.RelativeItem(4)
                   .PaddingVertical(5)
                   .Column(col =>
                   {
                       col.Spacing(2);
                       col.Item().Text("APPLICANT INFORMATION").Bold().FontSize(14);
                       foreach (var (label, value) in applicationInfomation)
                       {
                           col.Item()
                              .Component(new DataLabelComponent(label,
                                                                value,
                                                                GarmentColor.LabelTextColor,
                                                                GarmentColor.LabelBackGroundColor.Applicant,
                                                                GarmentColor.ValueTextColor,
                                                                GarmentColor.ValueBackGroundColor)
                              );
                       }
                   });
                row.ConstantItem(150)
                   .PaddingVertical(2.5f)
                   .PaddingLeft(5)
                   .Border(0.5f)
                   .Padding(2)
                   .Border(0.5f)
                   .PaddingVertical(5)
                   .PaddingHorizontal(10)
                   .Column(col =>
                   {
                       col.Spacing(2);
                       col.Item().Text("Overall Rating").AlignCenter().Bold().Underline();
                       col.Item().Component(new RatingLabelComponent(report.OverallRating));
                   });
            });
    }


    void AddReportInformation(ColumnDescriptor column)
    {
        var reportInformation = new (string, string)[]
        {
            ("Stage", report.Stage),
            ("Customer", report.Customer),
            ("InternalStyle", report.InternalStyle),
            ("Color/ RM Color", report.Color),
            ("Size", report.Size),
            ("Internal Order", report.InternalOrder),
            ("SO", report.SO),
            ("Actual Fiber Content", report.ActualFiberContent),
            ("Submitted Carre Instruction(s).", report.Instruction),
            ("Country of Origin", report.Country),
            ("Factory Name", report.Factory),
            ("Number of Sample Submitted for Testing", report.SampleNumber),
            ("Season", report.Season),
            ("Sample Description", report.SampleDescription),
            ("Reporter", report.Reporter),
            ("Checker", report.Checker),
            ("Date of testing", report.TestingDate),
            ("Remark", report.Remark)
        };
        column.Item()
            .PaddingTop(5)
            .Text("REPORT INFORMATION")
            .Bold()
            .FontSize(14);
        column.Item()
              .Row(row =>
              {
                  var halfCount = reportInformation.Length / 2;                  
                  AddColumnData(row,reportInformation.Take(halfCount));
                  AddColumnData(row, reportInformation.Skip(halfCount));
              });
    }
    void AddColumnData(RowDescriptor row, IEnumerable<(string, string)> data)
    {
        row.RelativeItem()
            .Column(col =>
            {
                foreach (var (label, value) in data)
                {
                    col.Item()
                       .Height(30)
                       .Component(new DataLabelComponent(label, 
                                                         value,
                                                         GarmentColor.LabelTextColor,
                                                         GarmentColor.LabelBackGroundColor.Report,
                                                         GarmentColor.ValueTextColor,
                                                         GarmentColor.ValueBackGroundColor)
                       );
                }
        });
    }
    void AddSummaryTable(ColumnDescriptor column)
    {
        var tableFormat = new List<TableFormat>()
        {
            new TableFormat() {ColSpan = 3, Align = Align.Left},
            new TableFormat() {ColSpan = 3, Align = Align.Left},
            new TableFormat() {ColSpan = 1, Align = Align.Center},
        };
        column.Item().Component(new TableComponent<TestResult>(report.TestResults, tableFormat));
    }
}
