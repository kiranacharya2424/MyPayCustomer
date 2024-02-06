using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class UserBankDetailController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        // GET: UserBankDetail
        public ActionResult Index(string MemberId)
        {
            ViewBag.MemberId = MemberId;
            List<SelectListItem> items_date = new List<SelectListItem>();
            items_date.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0",
                Selected = true
            });
            items_date.Add(new SelectListItem
            {
                Text = "Today",
                Value = "1"
            });
            items_date.Add(new SelectListItem
            {
                Text = "Weekly",
                Value = "2"
            });
            items_date.Add(new SelectListItem
            {
                Text = "Monthly",
                Value = "3"
            });
            ViewBag.DayWise = items_date;
            return View();
        }

        [Authorize]
        public JsonResult GetUserBankLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Member Id");
            columns.Add("Name");
            columns.Add("Bank Code");
            columns.Add("Bank Name");
            columns.Add("Branch Id");
            columns.Add("Branch Name");
            columns.Add("Account Number");
            columns.Add("Is Primary");
            columns.Add("Created By");
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
            string BankCode = context.Request.Form["BankCode"];
            string BankName = context.Request.Form["BankName"];
            string BranchName = context.Request.Form["BranchName"];
            string AccountNumber = context.Request.Form["AccountNumber"];
            string DayWise = context.Request.Form["Today"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddUserBankDetail> trans = new List<AddUserBankDetail>();
            GetUserBankDetail w = new GetUserBankDetail();
            if (!string.IsNullOrEmpty(MemberId) && MemberId != "0")
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.Name = Name;
            w.BankCode = BankCode;
            w.BankName = BankName;
            w.BranchName = BranchName;
            w.AccountNumber = AccountNumber;
            if (DayWise != "" && DayWise != "0")
            {
                if (DayWise == "1")
                {
                    w.Today = "Today";
                }
                else if (DayWise == "2")
                {
                    w.Weekly = "Weekly";
                }
                else if (DayWise == "3")
                {
                    w.Monthly = "Monthly";
                }
                else
                {
                    w.Today = "";
                    w.Monthly = "";
                    w.Weekly = "";
                }
            }
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddUserBankDetail
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         Name = row["Name"].ToString(),
                         BankCode = row["BankCode"].ToString(),
                         BankName = row["BankName"].ToString(),
                         BranchId = row["BranchId"].ToString(),
                         BranchName = row["BranchName"].ToString(),
                         AccountNumber = row["AccountNumber"].ToString(),
                         IsPrimary = Convert.ToBoolean(row["IsPrimary"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddUserBankDetail>> objDataTableResponse = new DataTableResponse<List<AddUserBankDetail>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        public ActionResult ExportExcel(Int64 MemberId, string Name, string BankCode, string BankName, string BranchName,string AccountNumber, string DayWise)
        {
            var fileName = "UsersBankList-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetUserBankDetail w = new GetUserBankDetail();
            w.MemberId = MemberId;
            w.Name = Name;
            w.BankCode = BankCode;
            w.BankName = BankName;
            w.BranchName =BranchName;
            w.AccountNumber = AccountNumber;
            if (DayWise != "" && DayWise != "0")
            {
                if (DayWise == "1")
                {
                    w.Today = "Today";
                }
                else if (DayWise == "2")
                {
                    w.Weekly = "Weekly";
                }
                else if (DayWise == "3")
                {
                    w.Monthly = "Monthly";
                }
                else
                {
                    w.Today = "";
                    w.Monthly = "";
                    w.Weekly = "";
                }
            }
            DataTable dt = w.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedIndiaDate", "MemberId", "Name", "BankCode", "BankName", "BranchId", "BranchName", "AccountNumber", "BankPrimary");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Created Date";
                dt.Columns["UpdatedIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["Name"].ColumnName = "Name";
                dt.Columns["BankName"].ColumnName = "Bank Name";
                dt.Columns["BranchId"].ColumnName = "Branch Id";
                dt.Columns["BranchName"].ColumnName = "Branch Name";
                dt.Columns["AccountNumber"].ColumnName = "Account Number";
                dt.Columns["BankPrimary"].ColumnName = "Is Primary";
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