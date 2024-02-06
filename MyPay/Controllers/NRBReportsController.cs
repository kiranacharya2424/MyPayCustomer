using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class NRBReportsController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            List<SelectListItem> Annexture = CommonHelpers.GetSelectList_AnnextureNRB();
            ViewBag.Annexture = Annexture;
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddNRBReports vmodel)
        {
            List<SelectListItem> Annexture = CommonHelpers.GetSelectList_AnnextureNRB();
            ViewBag.Annexture = Annexture;
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetNRBReportAnnexture(GetNRBReports vmodel)
        {
            var context = HttpContext;
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            GetNRBReports w = new GetNRBReports();
            DataTable dt = w.GetData(vmodel.Annexture.ToString(), vmodel.MemberId.ToString(), vmodel.Type.ToString(), vmodel.StartAmount.ToString(), vmodel.EndAmount.ToString(), vmodel.StartDate, vmodel.EndDate);
            List<AddNRBReports> trans = new List<AddNRBReports>();
            List<string> columns = new List<string>();
            switch (vmodel.Annexture)
            {
                case 5:
                    columns.Add("FormOfTransaction");
                    columns.Add("ZeroToThousandCount");
                    columns.Add("ZeroToThousandSum");
                    columns.Add("ThousandToFiveThousandCount");
                    columns.Add("ThousandToFiveThousandSum");
                    columns.Add("FiveThousandToTenThousandCount");
                    columns.Add("FiveThousandToTenThousandSum");
                    columns.Add("TenThousandToTwentyThousandCount");
                    columns.Add("TenThousandToTwentyThousandSum");
                    columns.Add("TwentyThousandToTwentyFiveThousandCount");
                    columns.Add("TwentyThousandToTwentyFiveThousandSum");
                    columns.Add("TwentyFiveThousandAboveCount");
                    columns.Add("TwentyFiveThousandAboveSum");
                    trans = (from DataRow row in dt.Rows

                             select new AddNRBReports
                             {
                                 FormOfTransaction = Convert.ToString(row["FormOfTransaction"]),
                                 ZeroToThousandCount = Convert.ToInt64(row["ZeroToThousandCount"]),
                                 ZeroToThousandSum = Convert.ToDecimal(row["ZeroToThousandSum"]),
                                 ThousandToFiveThousandCount = Convert.ToInt64(row["ThousandToFiveThousandCount"]),
                                 ThousandToFiveThousandSum = Convert.ToDecimal(row["ThousandToFiveThousandSum"]),
                                 FiveThousandToTenThousandCount = Convert.ToInt64(row["FiveThousandToTenThousandCount"]),
                                 FiveThousandToTenThousandSum = Convert.ToDecimal(row["FiveThousandToTenThousandSum"]),
                                 TenThousandToTwentyThousandCount = Convert.ToInt64(row["TenThousandToTwentyThousandCount"]),
                                 TenThousandToTwentyThousandSum = Convert.ToDecimal(row["TenThousandToTwentyThousandSum"]),
                                 TwentyThousandToTwentyFiveThousandCount = Convert.ToInt64(row["TwentyThousandToTwentyFiveThousandCount"]),
                                 TwentyThousandToTwentyFiveThousandSum = Convert.ToDecimal(row["TwentyThousandToTwentyFiveThousandSum"]),
                                 TwentyFiveThousandAboveCount = Convert.ToInt64(row["TwentyFiveThousandAboveCount"]),
                                 TwentyFiveThousandAboveSum = Convert.ToDecimal(row["TwentyFiveThousandAboveSum"]),
                             }).ToList();
                    break;

                case 6:
                    columns.Add("FormOfTransaction");
                    columns.Add("Success");
                    columns.Add("Failed");
                    columns.Add("WalletTranserFailed");
                    columns.Add("TopupSuccess");
                    columns.Add("TopupFailed");
                    trans = (from DataRow row in dt.Rows

                             select new AddNRBReports
                             {
                                 FormOfTransaction = Convert.ToString(row["FormOfTransaction"]),
                                 Success = Convert.ToInt64(row["Success"]),
                                 Failed = Convert.ToInt64(row["Failed"]),
                             }).ToList();
                    break;

                case 8:
                    columns.Add("Wallet");
                    columns.Add("Gender");
                    columns.Add("TotalNumber");
                    columns.Add("TotalWalletSum");
                    trans = (from DataRow row in dt.Rows

                             select new AddNRBReports
                             {
                                 Wallet = Convert.ToString(row["Wallet"]),
                                 Gender = Convert.ToString(row["Gender"]),
                                 TotalNumber = Convert.ToInt64(row["TotalNumber"]),
                                 TotalWalletSum = Convert.ToDecimal(row["TotalWalletSum"])
                             }).ToList();
                    break;
                default:
                    break;
            }


            Int32 recordFiltered = 0;

            DataTableResponse<List<AddNRBReports>> objDataTableResponse = new DataTableResponse<List<AddNRBReports>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        public ActionResult ExportExcel(string Annexture, string MemberId, string Type, string StartAmount, string EndAmount, string StartDate, string EndDate)
        {
            var fileName = "NRB-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetNRBReports w = new GetNRBReports();

            DataTable dt = w.GetData(Annexture, MemberId, Type, StartAmount, EndAmount, StartDate, EndDate);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (Annexture == "5")
                {
                    dt = dt.DefaultView.ToTable(false, "FormOfTransaction", "ZeroToThousandCount", "ZeroToThousandSum", "ThousandToFiveThousandCount", "ThousandToFiveThousandSum", "FiveThousandToTenThousandCount", "FiveThousandToTenThousandSum", "TenThousandToTwentyThousandCount", "TenThousandToTwentyThousandSum", "TwentyThousandToTwentyFiveThousandCount", "TwentyThousandToTwentyFiveThousandSum",  "TwentyFiveThousandAboveCount", "TwentyFiveThousandAboveSum");
                    //Add New Column Grand Total Number
                    DataColumn newCol = new DataColumn("Grand Total Number", typeof(Decimal));
                    newCol.AllowDBNull = false;
                    dt.Columns.Add(newCol);
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal ZeroToThousandCount = Convert.ToDecimal(row["ZeroToThousandCount"]);
                        decimal ThousandToFiveThousandCount = Convert.ToDecimal(row["ThousandToFiveThousandCount"]);
                        decimal FiveThousandToTenThousandCount = Convert.ToDecimal(row["FiveThousandToTenThousandCount"]);
                        decimal TenThousandToTwentyThousandCount = Convert.ToDecimal(row["TenThousandToTwentyThousandCount"]);
                        decimal TwentyThousandToTwentyFiveThousandCount = Convert.ToDecimal(row["TwentyThousandToTwentyFiveThousandCount"]);
                        decimal TwentyFiveThousandAboveCount = Convert.ToDecimal(row["TwentyFiveThousandAboveCount"]);
                        row["Grand Total Number"] = Math.Round(ZeroToThousandCount, 2) + Math.Round(ThousandToFiveThousandCount, 2) + Math.Round(FiveThousandToTenThousandCount, 2) + Math.Round(TenThousandToTwentyThousandCount, 2) + Math.Round(TwentyThousandToTwentyFiveThousandCount, 2) + Math.Round(TwentyFiveThousandAboveCount, 2);
                    }
                    //Add New Column Grand Total Number
                    DataColumn newColmn = new DataColumn("Grand Total Value", typeof(Decimal));
                    newColmn.AllowDBNull = false;
                    dt.Columns.Add(newColmn);
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal ZeroToThousandSum = Convert.ToDecimal(row["ZeroToThousandSum"]);
                        decimal ThousandToFiveThousandSum = Convert.ToDecimal(row["ThousandToFiveThousandSum"]);
                        decimal FiveThousandToTenThousandSum = Convert.ToDecimal(row["FiveThousandToTenThousandSum"]);
                        decimal TenThousandToTwentyThousandSum = Convert.ToDecimal(row["TenThousandToTwentyThousandSum"]);
                        decimal TwentyThousandToTwentyFiveThousandSum = Convert.ToDecimal(row["TwentyThousandToTwentyFiveThousandSum"]);
                        decimal TwentyFiveThousandAboveSum = Convert.ToDecimal(row["TwentyFiveThousandAboveSum"]);
                        row["Grand Total Value"] = Math.Round(ZeroToThousandSum, 2) + Math.Round(ThousandToFiveThousandSum, 2) + Math.Round(FiveThousandToTenThousandSum, 2) + Math.Round(TenThousandToTwentyThousandSum, 2) + Math.Round(TwentyThousandToTwentyFiveThousandSum, 2) + Math.Round(TwentyFiveThousandAboveSum, 2);
                    }

                    dt.Columns["FormOfTransaction"].ColumnName = "Form Of Transaction";
                    dt.Columns["ZeroToThousandCount"].ColumnName = "Number Upto 1000";
                    dt.Columns["ZeroToThousandSum"].ColumnName = "Value Upto 1000";
                    dt.Columns["ThousandToFiveThousandCount"].ColumnName = "Number In 1000-5000";
                    dt.Columns["ThousandToFiveThousandSum"].ColumnName = "Value In 1000-5000";
                    dt.Columns["FiveThousandToTenThousandCount"].ColumnName = "Number In 5000-10000";
                    dt.Columns["FiveThousandToTenThousandSum"].ColumnName = "Value In 5000-10000";
                    dt.Columns["TenThousandToTwentyThousandCount"].ColumnName = "Number In 10000-20000";
                    dt.Columns["TenThousandToTwentyThousandSum"].ColumnName = "Value In 10000-20000";
                    dt.Columns["TwentyThousandToTwentyFiveThousandCount"].ColumnName = "Number In 20000-25000";
                    dt.Columns["TwentyThousandToTwentyFiveThousandSum"].ColumnName = "Value In 20000-25000";
                    dt.Columns["TwentyFiveThousandAboveCount"].ColumnName = "Number Above 25000";
                    dt.Columns["TwentyFiveThousandAboveSum"].ColumnName = "Value Above 25000";                    
                }
                else if (Annexture == "6")
                {
                    dt = dt.DefaultView.ToTable(false, "FormOfTransaction", "Success", "Failed");
                    dt.Columns["FormOfTransaction"].ColumnName = "Form Of Transaction";
                }
                else if (Annexture == "8")
                {
                    dt = dt.DefaultView.ToTable(false, "Wallet", "Gender", "TotalNumber", "TotalWalletSum");
                    dt.Columns["Wallet"].ColumnName = "Wallet Type";
                    dt.Columns["Gender"].ColumnName = "User Type";
                    dt.Columns["TotalNumber"].ColumnName = "Total Number";
                    dt.Columns["TotalWalletSum"].ColumnName = "Total Balance";
                }
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "TxnReport(" + DateTime.Now.ToString("MMM") + ")");
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    ws.Columns().AdjustToContents();  // Adjust column width
                    ws.Rows().AdjustToContents();
                    wb.SaveAs(fullPath);
                }
            }
            var errorMessage = "you can return the errors here!";
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage });
        }

        [HttpGet]
        public ActionResult Download(string fileName)
        {
            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }

    }
}