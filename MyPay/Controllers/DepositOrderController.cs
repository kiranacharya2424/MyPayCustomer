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
    public class DepositOrderController : BaseAdminSessionController
    {
        [Authorize]
        // GET: DepositOrderReport
        public ActionResult Index(string TransactionId)
        {
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
            List<SelectListItem> status = new List<SelectListItem>();
            status.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            //status.Add(new SelectListItem
            //{
            //    Text = "Success",
            //    Value = "1"
            //});
            status.Add(new SelectListItem
            {
                Text = "Failed",
                Value = "2"
            });
            status.Add(new SelectListItem
            {
                Text = "Cancelled",
                Value = "3"
            });
            status.Add(new SelectListItem
            {
                Text = "Pending",
                Value = "4"
            });
            status.Add(new SelectListItem
            {
                Text = "Incomplete",
                Value = "5"
            });
            status.Add(new SelectListItem
            {
                Text = "Refund",
                Value = "6"
            });
            ViewBag.Status = status;

            List<SelectListItem> type = new List<SelectListItem>();
            type.Add(new SelectListItem
            {
                Text = "Select Type",
                Value = "0"
            });
            type.Add(new SelectListItem
            {
                Text = "ConnectIPS",
                Value = "1"
            });
            type.Add(new SelectListItem
            {
                Text = "Card",
                Value = "2"
            });
            type.Add(new SelectListItem
            {
                Text = "Bank Transfer",
                Value = "3"
            });
            type.Add(new SelectListItem
            {
                Text = "Internet Banking",
                Value = "4"
            });
            type.Add(new SelectListItem
            {
                Text = "Mobile Banking",
                Value = "5"
            });
            type.Add(new SelectListItem
            {
                Text = "Linked Bank Transaction",
                Value = "6"
            });
            type.Add(new SelectListItem
            {
                Text = "Credit Card Payment",
                Value = "7"
            });
            type.Add(new SelectListItem
            {
                Text = "Linked Bank Deposit",
                Value = "8"
            });

            ViewBag.Type = type;

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
        public JsonResult GetDepositOrdersLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TransactionId");
            columns.Add("CreatedByName");
            columns.Add("MemberId");
            columns.Add("Status");
            columns.Add("Amount");
            columns.Add("Type");
            columns.Add("Remarks");
            columns.Add("Particulars");
            columns.Add("RefferalsId");
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
            string TransactionId = context.Request.Form["TransactionId"];
            string RefferalsId = context.Request.Form["RefferalsId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string Type = context.Request.Form["Type"];
            string TypeMultiple = context.Request.Form["TypeMultiple"];
            string DayWise = context.Request.Form["Today"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddDepositOrders> trans = new List<AddDepositOrders>();

            AddDepositOrders w = new AddDepositOrders();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.TransactionId = TransactionId;
            w.RefferalsId = RefferalsId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.Type = Convert.ToInt32(Type);
            if (!string.IsNullOrEmpty(TypeMultiple))
            {
                w.TypeMultiple = TypeMultiple;
                w.Type = 0;
            }
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

                     select new AddDepositOrders
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         ResponseCode = row["ResponseCode"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         TransactionId = row["TransactionId"].ToString(),
                         StatusName = @Enum.GetName(typeof(AddDepositOrders.DepositStatus), Convert.ToInt64(row["Status"])).ToString(),
                         TypeName = row["TypeName"].ToString(),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         Particulars = row["Particulars"].ToString(),
                         RefferalsId = row["RefferalsId"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         Status = Convert.ToInt32(row["Status"]),
                         GatewayName = Convert.ToString(row["GatewayName"]),
                         GatewayType = Convert.ToInt32(row["GatewayType"]),
                         TotalSuccess = Convert.ToDecimal(row["TotalSuccess"].ToString()),
                         TotalFailed = Convert.ToDecimal(row["TotalFailed"].ToString()),
                         TotalPending = Convert.ToDecimal(row["TotalPending"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddDepositOrders>> objDataTableResponse = new DataTableResponse<List<AddDepositOrders>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        public string GetDepositOrderJson(string transactionid)
        {
            string result = "";
            try
            {
                AddDepositOrders outobject = new AddDepositOrders();
                GetDepositOrders inobject = new GetDepositOrders();
                inobject.TransactionId = transactionid;
                AddDepositOrders res = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(Models.Common.Common.StoreProcedures.sp_DepositOrders_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    result = res.JsonResponse;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [HttpPost]
        public ActionResult ExportExcelDepositOrder(Int64 MemberId, string TxnId, string FromDate, string ToDate, string Status, string DayWise, string Type)
        {
            var fileName = "DepositOrder-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetDepositOrders inobject = new GetDepositOrders();
            inobject.TransactionId = TxnId;
            inobject.MemberId = Convert.ToInt64(MemberId);
            inobject.StartDate = FromDate;
            inobject.EndDate = ToDate;
            inobject.Status = Convert.ToInt32(Status);
            inobject.Type = Convert.ToInt32(Type);
            inobject.Take = 50;
            inobject.Skip = 0;
            if (DayWise != "" && DayWise != "0")
            {
                if (DayWise == "1")
                {
                    inobject.Today = "Today";
                }
                else if (DayWise == "2")
                {
                    inobject.Weekly = "Weekly";
                }
                else if (DayWise == "3")
                {
                    inobject.Monthly = "Monthly";
                }
                else
                {
                    inobject.Today = "";
                    inobject.Monthly = "";
                    inobject.Weekly = "";
                }
            }

            DataTable dt = inobject.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "IndiaDate", "TransactionId", "MemberId", "StatusName", "Amount", "TypeName", "Remarks", "Particulars", "RefferalsId");
                //dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionId"].ColumnName = "Transaction Id";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["StatusName"].ColumnName = "Status";
                dt.Columns["TypeName"].ColumnName = "Type";
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

        //DepositOrder Dump Export
        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelDepositOrderDump()
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                AddExportData outobject = new AddExportData();
                GetExportData inobject = new GetExportData();
                inobject.Type = (int)AddExportData.ExportType.Transaction;
                inobject.StartDate = DateTime.UtcNow.AddHours(-4).ToString("MM-dd-yyyy hh:mm:ss tt");
                inobject.EndDate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");
                //inobject.CheckCreatedDate = DateTime.Now.ToString();
                AddExportData res = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    errorMessage = "Your File was already generated! You can download the file from Download link. ";
                }
            }
            if (errorMessage == "")
            {
                fileName = "LoadFundReport-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                //Save the file to server temp folder
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), fileName);

                GetDepositOrders w = new GetDepositOrders();

                DataTable dt = w.GetList();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "TransactionId", "MemberId", "StatusName", "Amount", "TypeName", "Remarks", "Particulars", "RefferalsId");
                    dt.Columns["Sno"].ColumnName = "Sr No";
                    dt.Columns["IndiaDate"].ColumnName = "Date";
                    dt.Columns["MemberId"].ColumnName = "Member Id";
                    dt.Columns["TransactionId"].ColumnName = "Transaction Id";
                    dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                    dt.Columns["StatusName"].ColumnName = "Status";
                    dt.Columns["TypeName"].ColumnName = "Type";
                    dt.AcceptChanges();
                    AddExportData outobject_file = new AddExportData();
                    GetExportData inobject_file = new GetExportData();
                    inobject_file.Type = (int)AddExportData.ExportType.Transaction;
                    AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
                    if (res_file != null && res_file.Id != 0)
                    {
                        string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), res_file.FilePath);
                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                        res_file.FilePath = fullPath;
                        res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                        if (status)
                        {

                            Common.AddLogs("LoadFund Report Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                        }
                    }
                    else
                    {
                        AddExportData export = new AddExportData();
                        export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        export.CreatedByName = Session["AdminUserName"].ToString();
                        export.FilePath = fullPath;
                        export.Type = (int)AddExportData.ExportType.Transaction;
                        export.IsActive = true;
                        export.IsApprovedByAdmin = true;
                        Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                        if (Id > 0)
                        {
                            Common.AddLogs("LoadFund Report Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(dt, "LoadFundReport(" + DateTime.Now.ToString("MMM") + ")");
                        ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                        ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        ws.Columns().AdjustToContents();  // Adjust column width
                        ws.Rows().AdjustToContents();
                        wb.SaveAs(fullPath);
                    }
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadFileName()
        {
            var errorMessage = "";
            AddExportData outobject_file = new AddExportData();
            GetExportData inobject_file = new GetExportData();
            inobject_file.Type = (int)AddExportData.ExportType.Transaction;
            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
            if (res_file != null && res_file.Id != 0)
            {
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), res_file.FilePath);
                if (System.IO.File.Exists(fullPath))
                {
                    return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
                }
                else
                {
                    errorMessage = "";
                    return Json(new { fileName = "", errorMessage = errorMessage });
                }

            }
            else
            {
                errorMessage = "";
                return Json(new { fileName = "", errorMessage = errorMessage });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult DownloadFile(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                    //System.IO.File.Delete(fullPath);
                    return File(fileByteArray, "application/vnd.ms-excel", fileName);
                }
                else
                {
                    return RedirectToAction("/Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("/Index");
            }
        }
    }
}