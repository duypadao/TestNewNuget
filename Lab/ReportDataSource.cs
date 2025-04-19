using PDFCreator.Lab.Model;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCreator.Lab
{
    public static class ReportDataSource
    {
        private static Random Random = new Random();
        private static DateTime Now = DateTime.Now;
        public static ReportInfo GenerateRandomReportInfo()
        {
            var randomTestResult = Enumerable.Range(1, 5).Select(x => GenerateRandomTestResult()).ToList();
            return new ReportInfo
            {
                ReportName = "GARMENT TEST REPORT",
                ReportNo = "GTR-" + Now.ToString("yyMMdd") + "-" + Random.Next(100).ToString().PadLeft(3, '0'),
                RecveicedDate = Now.AddDays(-Random.Next(7)).ToString("yyyy-MM-dd"),
                ReportDate = Now.ToString("yyyy-MM-dd"),
                OverallRating = (Rating)Random.Next(3),
                Department = "RMLAB",
                Applicant = "Phạm Thị Huyền Trang",
                Email = "monica.pham@reginamiracle.com",
                Tel = Placeholders.PhoneNumber(),
                Stage = "Bulk/大货",
                Customer = Placeholders.Name(),
                InternalStyle = "51" + Random.Next(10000000, 99999999),
                Color = Placeholders.Color(),
                Size = (new string[] { "XS", "S", "M", "L", "XL", "2XL" })[Random.Next(1, 2)],
                InternalOrder = "570" + Random.Next(1000000, 9999999),
                SO = "A16" + Random.Next(1000000,9999999),
                ActualFiberContent = "/",
                Instruction = "/",
                Country = "Viet Nam",
                Factory = "REGINA MIRACLE INTERNATIONAL",
                SampleNumber = Random.Next(1,100).ToString(),
                Season = (new string[] { "FW", "SS" })[Random.Next(1,2)] + Now.ToString("yy"),
                SampleDescription = Placeholders.LoremIpsum().Substring(0,20),
                Reporter = Placeholders.Name(),
                Checker = Placeholders.Name(),
                TestingDate = Placeholders.DateTime(),
                Remark = "/",
                TestResults = randomTestResult,
                WashingMethod = "AATCC 150",
                TempC = Random.Next(20,40).ToString(),
                DryMethod = "Không được sử dụng lò sấy khô/Non tumble dry",
                Cycle = Random.Next(1,10).ToString(),
                ColorChange = Random.Next(1, 10).ToString(),
                GeneralAppearance = new List<string>() { "G", "A", "B" },
                Comment = "Fail"
            };
        }
        private static TestResult GenerateRandomTestResult()
        {
            return new TestResult
            {
                TestProperty = Placeholders.Sentence(),
                TestMethod = Placeholders.Sentence(),
                Rating = (new string[] { "P", "F", "D" })[Random.Next(1, 3)]
            };
        }
    }
}
