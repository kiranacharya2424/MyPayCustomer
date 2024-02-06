using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class BalanceHistoryReportController : BaseAdminSessionController
    {
        // GET: BalanceHistoryReport
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetBalanceHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TotalBalance");
            columns.Add("Type");
            columns.Add("UserCount");
            columns.Add("ActiveUser");
            columns.Add("InActiveUser");
            columns.Add("TotalCoinsBalance");
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
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddBalanceHistory> trans = new List<AddBalanceHistory>();

            GetBalanceHistory w = new GetBalanceHistory();

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddBalanceHistory
                     {
                         Sno = row["Sno"].ToString(),
                         Id = Convert.ToInt64(row["Id"].ToString()),
                         TotalBalance = Convert.ToDecimal(row["TotalBalance"]),
                         UserCount = Convert.ToInt64(row["UserCount"].ToString()),
                         ActiveUser = Convert.ToInt64(row["ActiveUser"].ToString()),
                         InActiveUser = Convert.ToInt64(row["InActiveUser"].ToString()),
                         TypeName = @Enum.GetName(typeof(AddBalanceHistory.Types), Convert.ToInt64(row["Type"])),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         TotalCoinsBalance = Convert.ToDecimal(row["TotalCoinsBalance"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddBalanceHistory>> objDataTableResponse = new DataTableResponse<List<AddBalanceHistory>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        [HttpPost]
        public ActionResult ExportExcel(Int32 Take, Int32 Skip, string Sort, string SortOrder)
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TotalBalance");
            columns.Add("Type");
            columns.Add("UserCount");
            columns.Add("ActiveUser");
            columns.Add("InActiveUser");
            Sort = columns[Convert.ToInt32(Sort)];
            var fileName = "BalanceHistory-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetBalanceHistory w = new GetBalanceHistory();

            DataTable dt = w.GetData(Sort, SortOrder, Skip, Take, "");

            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "IndiaDate", "TotalBalance", "Type", "UserCount", "ActiveUser", "InActiveUser");
                //dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["TotalBalance"].ColumnName = "Total Balance";
                dt.Columns["UserCount"].ColumnName = "User Count";
                dt.Columns["ActiveUser"].ColumnName = "Active User";
                dt.Columns["InActiveUser"].ColumnName = "InActive User";
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "User(" + DateTime.Now.ToString("MMM") + ")");
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

        [Authorize]
        [HttpGet]
        public ActionResult Download(string fileName)
        {
            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }

        [HttpPost]
        [Authorize]
        public JsonResult EditBalanceHistory(Int64 Id, string TotalBalance)
        {
            string msg = "";
            if (Id == 0)
            {
                msg = "Please Select id";
            }
            else if (string.IsNullOrEmpty(TotalBalance))
            {
                msg = "Please Enter Total Balance";
            }
            else
            {
                string platform = "web";
                string devicecode = HttpContext.Request.Browser.Type;
                AddBalanceHistory outobject = new AddBalanceHistory();
                GetBalanceHistory inobject = new GetBalanceHistory();
                inobject.Id = Id;
                AddBalanceHistory res = RepCRUD<GetBalanceHistory, AddBalanceHistory>.GetRecord(Common.StoreProcedures.sp_BalanceHistory_Get, inobject, outobject);
                if (res != null && res.Id > 0)
                {
                    string PrevBalance = res.TotalBalance.ToString();
                    res.TotalBalance = Convert.ToDecimal(TotalBalance);
                    bool IsUpdated = RepCRUD<AddBalanceHistory, GetBalanceHistory>.Update(res, "balancehistory");
                    if (IsUpdated)
                    {
                        Models.Common.Common.AddLogs($"Balance History Id:{res.Id}, TotalBalance is Changed successfully from {PrevBalance} to {res.TotalBalance}, Action performed by : {Session["AdminUserName"].ToString()}", false, Convert.ToInt32(AddLog.LogType.BalanceHistory), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, platform, devicecode, (int)AddLog.LogActivityEnum.EOD_Update);
                        msg = "success";
                    }
                    else
                    {
                        msg = "Not Updated.Please try again.";
                    }
                }
                else
                {
                    msg = "Balance History Id not found";
                }
            }
            return Json(msg);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CalculateBalanceHistory(string SelectedDate)
        {
            string msg = "";

            if (SelectedDate == "")
            {
                msg = "Please select date";
            }
            else if (Convert.ToDateTime(SelectedDate) >= Convert.ToDateTime(System.DateTime.Now.ToShortDateString()))
            {
                msg = "Please select previous date";
            }
            else
            {
                string platform = "web";
                string devicecode = HttpContext.Request.Browser.Type;
                AddBalanceHistory outobject = new AddBalanceHistory();
                GetBalanceHistory inobject = new GetBalanceHistory();
                inobject.CheckTodayDate = SelectedDate;
                AddBalanceHistory res = RepCRUD<GetBalanceHistory, AddBalanceHistory>.GetRecord(Common.StoreProcedures.sp_BalanceHistory_Get, inobject, outobject);
                if (res == null || res.Id == 0)
                {
                    AddBalanceHistory outobject_history = new AddBalanceHistory();
                    GetCalculatedBalance inobject_history = new GetCalculatedBalance();
                    inobject_history.date = Convert.ToDateTime(SelectedDate);
                    AddBalanceHistory res_history = RepCRUD<GetCalculatedBalance, AddBalanceHistory>.GetRecord(Common.StoreProcedures.sp_CalculateBalance_From_Date, inobject_history, outobject_history);
                    if (res_history != null)
                    {
                        AddBalanceHistory addhistory = new AddBalanceHistory();
                        addhistory.Type = (int)AddBalanceHistory.Types.User;
                        addhistory.ActiveUser = res_history.ActiveUser;
                        addhistory.InActiveUser = res_history.InActiveUser;
                        addhistory.TotalBalance = res_history.TotalBalance;
                        addhistory.UserCount = res_history.UserCount;
                        addhistory.TotalCoinsBalance = res_history.TotalCoinsBalance;
                        addhistory.UpdatedBy = Common.CreatedBy;
                        addhistory.IsActive = true;
                        addhistory.IsApprovedByAdmin = true;
                        addhistory.CreatedBy = Common.CreatedBy;
                        addhistory.CreatedByName = Common.CreatedByName;
                        addhistory.CreatedDate = Convert.ToDateTime(SelectedDate + " " + "06:14 PM");
                        Int64 id = RepCRUD<AddBalanceHistory, GetBalanceHistory>.Insert(addhistory, "balancehistory");
                        if (id > 0)
                        {
                            msg = "success";
                            Common.AddLogs("EOD Balance Added Successfully for Date:(" + SelectedDate + ") on (Date:" + DateTime.UtcNow.ToString() + ")", true, (int)AddLog.LogType.User, 10000, "Admin", false, "WEB", System.Web.HttpContext.Current.Request.Browser.Type);
                        }
                    }
                }
                else
                {
                    msg = "Balance History aleardy there for selected date";
                }
            }
            return Json(msg);
        }


    }
}