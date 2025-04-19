using PDFCreator.Lab.Component;
using PDFCreator.Lab.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using static GarmentDocument.GarmentColor;
using static QuestPDF.Helpers.Colors;

public class GarmentDocument : IDocument
{
    public ReportInfo report { get; }
    public byte[] logoImage = File.ReadAllBytes("D:\\Darius folder\\TestNewNuget\\Image\\logo.png");
    byte[] signature = File.ReadAllBytes("D:\\Darius folder\\TestNewNuget\\Image\\Signature.png");

    public float scale = 0.6f;
    public (string, string)[] applicationInfomation { get; }
    public (string, string)[] reportInformation { get; }
    public (string, string)[] methodResult { get; }
    //public (string, string)[] result { get; }

    private string[] policyWarning = 
    {
        "Any copying or replication of this report to or for any other person or entity, or use of our name or trademark, is permitted only with our prior written permission. This report sets forth our findings solely with respect to the test samples identified herein.",
        "The results set forth in this report are not indicative or representative of the quality or characteristics of the lot from which a test sample was taken or any similar or identical product unless specifically and expressly noted.",
        "You have 60 days from date of issuance of this report to notify us of any material error or omission caused by our negligence or if you require measurement uncertainty."
    };
    private string[] companyInfo = 
    {
        "Regina Miracle International VietNam Co. Ltd",
        "No.9, East West Road, VSIP Hai Phong Township, Industrial and Service Park Duong Quan Commune, Thuy Nguyen District, Hai Phong City, Vietnam.",
        "Tel: 02256.263.282 - Fax: 02252.299.080 - Website: Reginamiracle.com"
    };
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
        public static string SectionBackGroundColor = "#16365C";
    }
    public GarmentDocument(ReportInfo reportInfo)
    {
        report = reportInfo;
        applicationInfomation = new (string, string)[]
        {
                ("Department:", report.Department),
                ("Applicant:", report.Applicant),
                ("Email:", report.Email),
                ("Tel:", report.Tel),
        };
        reportInformation = new (string, string)[]
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
        methodResult = new (string, string)[]
        {
            ("Washing Method:", report.WashingMethod),
            ("Temp℃:", report.TempC),
            ("Dry Method:", report.DryMethod),
            ("Cycle(s):", report.Cycle),
        };
        //result = new (string, string)[]
        //{
        //    ("Color change:", report.ColorChange),
        //    ("General Appearance:", report.GeneralAppearance),
        //};
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(5);
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Calibri"));
            page.Margin(15);
            CreateHeader(page);
            CreateContent(page);
            CreateFooter(page);
        });
    }
    void CreateHeader(PageDescriptor page)
    {
        page.Header()
            //.Background(Colors.Grey.Darken2)
            .Scale(scale)
            .BorderBottom(0.5f)
            .BorderColor(Colors.Black)
            .Container()
            .TranslateY(-2)
            .BorderBottom(0.5f)
            .BorderColor(Colors.Black)
            .Row(row =>
            {
                row.ConstantItem(100).Image(logoImage);
                row.RelativeItem(4)
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
                       column.Item().Row(r =>
                       {
                           r.ConstantItem(50).Text("Report No:").Bold().AlignLeft();
                           r.ConstantItem(90).Text("2502410150581").Bold().AlignRight();
                       });
                       column.Item().Row(r =>
                       {
                           r.ConstantItem(70).Text("Received Date:").AlignLeft();
                           r.ConstantItem(70).Text(report.RecveicedDate).AlignRight();
                       });
                       column.Item().Row(r =>
                       {
                           r.ConstantItem(60).Text("Report Date:").AlignLeft();
                           r.ConstantItem(80).Text(report.ReportDate).AlignRight();
                       });
                   });
            });
    }

    void CreateContent(PageDescriptor page)
    {
        page.Content()
            .Scale(scale)
            //.Background(Colors.Green.Darken1)
            .Column(column =>
            {
                AddSection(column, "1. REPORT INFORMATION");
                AddApplicationInfo(column);
                AddReportInformation(column);
                AddSection(column, "2. SUMMARY PAGE");
                AddSummaryTable(column);
                AddRemark(column);
                column.Item().PageBreak();
                AddSection(column, "3. TEST RESULT");
                AddTestResult(column);
                AddEndOfReport(column);
            });
        //page.Content().PageBreak();

    }

    void CreateFooter(PageDescriptor page)
    {
        page.Footer()
            .Scale(scale)
            .AlignCenter()
            .Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });
    }
    void AddSection(ColumnDescriptor column, string value)
    {
        column
            .Item()
            .PaddingTop(15)
            .Background(GarmentColor.SectionBackGroundColor)
            .PaddingHorizontal(5)
            .Text(value)
            .FontSize(14)
            .FontColor(Colors.White);
    }

    void AddApplicationInfo(ColumnDescriptor column)
    {
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
        var tableFormats = new List<TableFormat>()
        {
            new TableFormat() {ColSpan = 3, Align = Align.Left, Caption = "TEST PROPERTY"},
            new TableFormat() {ColSpan = 3, Align = Align.Left, Caption = "TEST METHOD"},
            new TableFormat() {ColSpan = 1, Align = Align.Center, Caption = "RATING"},
        };
        column
            .Item()
            .PaddingTop(5)
            .Component(new TableComponent<TestResult>(report.TestResults, tableFormats));
    }

    void AddRemark(ColumnDescriptor column)
    {
        column
            .Item()
            .PaddingTop(30)
            .Row(row =>
            {
                row.RelativeItem(2).Text(text =>
                {
                    text.Span("Remark: ").Bold();
                    text.Span("P = PASS, F = FAIL, D = DATA, N/A = Not Applicable, * = See Remark");
                });

                row.RelativeItem().Text("For and on behalf of Laboratory center").Bold().AlignCenter();
            });
        column.Item()
              .PaddingTop(10)
              .Row(row =>
              {
                  row.RelativeItem(2)
                     .PaddingRight(10)
                     .Border(1)
                     .Column(col =>
                     {
                         col.Item().Text("*1:");
                         col.Item().Text("*2:");
                     });
                  row.RelativeItem()
                     .BorderTop(1)
                     .Column(col =>
                     {
                         col.Item()
                            .AlignCenter()
                            .Height(70)
                            .Image(signature);
                         col.Item().Text("Jason Huang").AlignCenter();
                         col.Item().Text("Senior Manager").AlignCenter();
                     });
              });
        column.Item()
              .PaddingTop(25)
              .BorderTop(0.5f)
              .PaddingTop(5)
              .Column(col =>
              {
                  foreach(var item in policyWarning)
                  {
                      col.Item().Text(item).FontSize(8).AlignCenter();
                  }
              });
        column.Item()
              .PaddingTop(35)
              .BorderTop(0.5f)
              .PaddingTop(5)
              .Column(col =>
              {
                  for (var i = 0;  i < companyInfo.Length; i++)
                  {
                      if (i == 0)
                      {
                          col.Item().Text(companyInfo[i]).FontSize(9).AlignCenter()
                             .Bold()
                             .FontColor(GarmentColor.SectionBackGroundColor);
                      }
                      else
                      {
                          col.Item().Text(companyInfo[i]).FontSize(8).AlignCenter();
                      }
                  }
              });
    }

    void AddTestResult(ColumnDescriptor column)
    {
        column
            .Item()
            .PaddingTop(10)
            .Row(row =>
            {
                foreach (var (label, col) in new (string, float)[] {
                    ("METHOD",2),
                    ("RESULTS",3),
                    ("REQUIREMENT", 1),
                    ("COMMENT", 1)
                })
                {
                    row.RelativeItem(col)
                       .Background(LabelBackGroundColor.Report)
                       .Border(0.5f)
                       .Padding(5)
                       .AlignMiddle()
                       .Text(label)
                       .FontColor(LabelTextColor)
                       .Bold()
                       .AlignCenter();
                }
            });
        column
            .Item()
            .Row(row =>
            {
                row.RelativeItem(2)
                   .Border(0.5f)
                   .Column(col =>
                   {
                       foreach(var (label, value) in methodResult)
                       {
                           col.Item()
                              .MinHeight(30)
                              .Component(new DataLabelComponent(label,
                                                                value,
                                                                Colors.Black,
                                                                GarmentColor.LabelBackGroundColor.Applicant,
                                                                GarmentColor.ValueTextColor,
                                                                GarmentColor.ValueBackGroundColor,
                                                                Colors.White)
                              );
                       }
                   });
                row.RelativeItem(3f)
                   .Border(0.5f)
                   .AlignMiddle()
                   .Column(col =>
                   {
                       col.Item()
                          .Row(r =>
                          {
                              r.ConstantItem(100).Text("Test Item").AlignCenter().Underline();
                              r.RelativeItem().Text("Result").AlignCenter().Underline();
                          });
                       col.Item().Height(30)
                        .Component(new DataLabelMultiValueComponent("Color change:",
                                                                new List<string>() { report.ColorChange },
                                                                Colors.Black,
                                                                GarmentColor.LabelBackGroundColor.Applicant,
                                                                GarmentColor.ValueTextColor,
                                                                GarmentColor.ValueBackGroundColor,
                                                                Colors.White));
                       col.Item()//.Height(60)
                        .Component(new DataLabelMultiValueComponent("General Appearance:",
                                                                report.GeneralAppearance,
                                                                Colors.Black,
                                                                GarmentColor.LabelBackGroundColor.Applicant,
                                                                GarmentColor.ValueTextColor,
                                                                GarmentColor.ValueBackGroundColor,
                                                                Colors.White));
                       
                   });
                row.RelativeItem()
                   .Border(0.5f)
                   .AlignMiddle()
                   .AlignCenter()
                   .Text("Satisfactory");
                row.RelativeItem()
                   .Border(0.5f)
                   .AlignMiddle()
                   .AlignCenter()
                   .Text($"{report.Comment}");
            });
    }

    void AddEndOfReport(ColumnDescriptor column)
    {
        column.Item()
              .ExtendVertical()
              .AlignBottom()
              .Column(col =>
              {
                  col.Item()
                     .BorderTop(0.5f)
                     .PaddingTop(2)
                     .BorderTop(0.5f);
                  col.Item()
                     .Text("## END OF THE REPORT ##")
                     .AlignCenter();
              });
    }
}
