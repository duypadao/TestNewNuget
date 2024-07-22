using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program;

namespace PDFCreator.Lab.Model
{
    public class ReportInfo
    {
        public string ReportName { get; set; } = string.Empty;
        public string ReportNo { get; set; } = string.Empty;
        public string RecveicedDate { get; set; } = string.Empty;
        public string ReportDate { get; set; } = string.Empty;
        public Rating OverallRating { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Applicant { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Stage { get; set; } = "Bulk/大货";
        public string Customer { get; set; } = string.Empty;
        public string InternalStyle { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string InternalOrder { get; set; } = string.Empty;
        public string SO { get; set; } = string.Empty;
        public string ActualFiberContent { get; set; } = "/";
        public string Instruction { get; set; } = "/";
        public string Country { get; set; } = "Viet Nam";
        public string Factory { get; set; } = "REGINA MIRACLE INTERNATIONAL";
        public string SampleNumber { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;
        public string SampleDescription { get; set; } = string.Empty;
        public string Reporter { get; set; } = string.Empty;
        public string Checker { get; set; } = string.Empty;
        public string TestingDate { get; set; } = string.Empty;
        public string Remark { get; set; } = "/";

        public List<TestResult> TestResults { get; set; } = new List<TestResult>();
        public string WashingMethod { get; set; } = string.Empty;
        public string TempC { get; set; } = string.Empty;
        public string DryMethod { get; set; } = string.Empty;
        public string Cycle { get; set; } = string.Empty;
        public string ColorChange { get; set; } = string.Empty;
        public string GeneralAppearance { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;

    }
}
