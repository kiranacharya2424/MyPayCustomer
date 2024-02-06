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
    public class RequestFundReportController : BaseAdminSessionController
    {
        [Authorize]
        // GET: RequestFundReport
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "-1",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Pending",
                Value = "0"
            });
            items.Add(new SelectListItem
            {
                Text = "Accepted",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Rejected",
                Value = "2"
            });
            ViewBag.Status = items;

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
        public JsonResult GetRequestFundLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Receiver MemberId");
            columns.Add("Receiver Name");
            columns.Add("Receiver ContactNo");
            columns.Add("Amount");
            columns.Add("Sender MemberId");            
            columns.Add("Sender Name");
            columns.Add("Sender ContactNo");
            columns.Add("Status");
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
            string SenderMemberId = context.Request.Form["SenderMemberId"];
            string Status = context.Request.Form["Status"];
            string DayWise = context.Request.Form["Today"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRequestFund> trans = new List<AddRequestFund>();

            GetRequestFund w = new GetRequestFund();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            if (!string.IsNullOrEmpty(SenderMemberId))
            {
                w.SenderMemberId = Convert.ToInt64(SenderMemberId);
            }
            w.RequestStatus = Convert.ToInt32(Status);
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
            w.StartDate = fromdate;
            w.EndDate = todate;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows
                     select new AddRequestFund
                     {
                         Sno= row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         SenderMemberId = Convert.ToInt64(row["SenderMemberId"]),
                         SenderMemberName= row["SenderMemberName"].ToString(),
                         SenderPhoneNumber= row["SenderPhoneNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         CreatedByName= row["CreatedByName"].ToString(),
                         UpdatedDatedt= row["UpdateIndiaDate"].ToString(),
                         ReceiverMemberName = row["ReceiverMemberName"].ToString(),
                         ReceiverPhoneNumber = row["ReceiverPhoneNumber"].ToString(),
                         StatusName = @Enum.GetName(typeof(AddRequestFund.RequestStatuses), Convert.ToInt64(row["RequestStatus"])),
                         RequestStatus = Convert.ToInt32(row["RequestStatus"]),
                         TotalPending = Convert.ToDecimal(row["TotalPending"].ToString()),
                         TotalAccepted = Convert.ToDecimal(row["TotalAccepted"].ToString()),
                         TotalRejected = Convert.ToDecimal(row["TotalRejected"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         IpAddress=row["IpAddress"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRequestFund>> objDataTableResponse = new DataTableResponse<List<AddRequestFund>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        public ActionResult ExportExcel(Int64 MemberId, Int64 SenderMemberId, string FromDate, string ToDate, string Status, string DayWise)
        {
            var fileName = "RequestFund-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetRequestFund w = new GetRequestFund();
            w.MemberId = MemberId;
            w.SenderMemberId = SenderMemberId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.RequestStatus = Convert.ToInt32(Status);
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
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdateIndiaDate", "MemberId", "ReceiverMemberName", "ReceiverPhoneNumber", "Amount", "SenderMemberId", "SenderMemberName", "SenderPhoneNumber", "StatusName", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Created Date";
                dt.Columns["UpdateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["ReceiverMemberName"].ColumnName = "Receiver Name";
                dt.Columns["ReceiverPhoneNumber"].ColumnName = "Receiver ContactNo";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SenderMemberId"].ColumnName = "Sender MemberId";
                dt.Columns["SenderMemberName"].ColumnName = "Sender Name";
                dt.Columns["SenderPhoneNumber"].ColumnName = "Sender ContactNo";
                dt.Columns["StatusName"].ColumnName = "Status";
                dt.Columns["IpAddress"].ColumnName = "Ip Address";
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