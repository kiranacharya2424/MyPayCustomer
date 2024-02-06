using ClosedXML.Excel;
using MyPay.Models.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class RewardPointsController : BaseAdminSessionController
    {
        [Authorize]
        // GET: RewardPoints
        public ActionResult Index()
        {
            List<SelectListItem> sign = new List<SelectListItem>();
            sign.Add(new SelectListItem
            {
                Text = "Select Sign",
                Value = "0",
                Selected = true
            });
            sign.Add(new SelectListItem
            {
                Text = "Credit",
                Value = "1"
            });
            sign.Add(new SelectListItem
            {
                Text = "Debit",
                Value = "2"
            });
            ViewBag.Sign = sign;
            return View();
        }

        [Authorize]
        public JsonResult GetRewardPointLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TransactionId");
            columns.Add("Name");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("Service");
            columns.Add("Description");
            //columns.Add("UpdateBy");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MemberId = context.Request.Form["MemberId"];
            string Name = context.Request.Form["Name"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Sign = context.Request.Form["Sign"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<RewardPointTransactions> trans = new List<RewardPointTransactions>();

            RewardPointTransactions w = new RewardPointTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }

            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Sign = Convert.ToInt32(Sign);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new RewardPointTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         ParentTransactionId = row["ParentTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(RewardPointTransactions.RewardTypes), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         Description = row["Description"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         Sign = Convert.ToInt32(row["Sign"]),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         VendorServiceID = Convert.ToInt32(row["VendorServiceID"]),
                         VendorServiceName = Convert.ToInt32(row["VendorServiceID"]) == 0 ? "" : @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["VendorServiceID"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),

                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<RewardPointTransactions>> objDataTableResponse = new DataTableResponse<List<RewardPointTransactions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        public ActionResult ExportExcel(Int64 MemberId, string Name, string TxnId, string FromDate, string ToDate)
        {
            var fileName = "RewardPoints-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            RewardPointTransactions w = new RewardPointTransactions();

            w.MemberId = Convert.ToInt64(MemberId);
            w.MemberName = Name;
            w.TransactionUniqueId = TxnId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            DataTable dt = w.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "MemberName", "Amount", "SignName", "Type", "Description");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["MemberName"].ColumnName = "Name";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["Type"].ColumnName = "Service";
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