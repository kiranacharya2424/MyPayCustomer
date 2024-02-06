using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
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
    public class MerchantOrdersController : BaseMerchantSessionController
    {
        // GET: MerchantOrders
        [HttpGet]
        [Authorize]
        public ActionResult Index(string MerchantUniqueId)
        {
            ViewBag.MerchantUniqueId = string.IsNullOrEmpty(MerchantUniqueId) ? "" : MerchantUniqueId;
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantTransactionStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            List<SelectListItem> signs = new List<SelectListItem>();

            signs = CommonHelpers.GetSelectList_MerchantOrderSign();
            //signs.Add(new SelectListItem
            //{
            //    Text = "Select Sign",
            //    Value = "0",
            //    Selected = true
            //});
            ViewBag.Sign = signs;

            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }

        // GET: MerchantWithdrawals
        [HttpGet]
        [Authorize]
        public ActionResult MerchantWithdrawals(string MerchantUniqueId)
        {
            ViewBag.MerchantUniqueId = string.IsNullOrEmpty(MerchantUniqueId) ? "" : MerchantUniqueId;
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantTransactionStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            List<SelectListItem> signs = new List<SelectListItem>();

            signs = CommonHelpers.GetSelectList_MerchantOrderSign();
            //signs.Add(new SelectListItem
            //{
            //    Text = "Select Sign",
            //    Value = "0",
            //    Selected = true
            //});
            ViewBag.Sign = signs;
            return View();
        }


        [Authorize]
        public JsonResult GetLoginMerchantOrdersLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TxnId");
            columns.Add("TrackerId");
            columns.Add("MerchantId");
            columns.Add("OrderId");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("MemberContatNo");
            columns.Add("Amount");
            columns.Add("Status");
            columns.Add("ServiceCharge");
            //columns.Add("IpAddress");
            columns.Add("Type");

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
            string MerchantId = context.Request.Form["MerchantId"];
            string OrderId = context.Request.Form["OrderId"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string TrackerId = context.Request.Form["TrackerId"];
            string MemberName = context.Request.Form["MemberName"];
            string MemberContactNumber = context.Request.Form["MemberContactNumber"];
            string Status = context.Request.Form["Status"];
            string Type = context.Request.Form["Type"];
            string TransactionId = context.Request.Form["TransactionId"];
            string PageType = context.Request.Form["PageType"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantOrders> trans = new List<AddMerchantOrders>();

            GetMerchantOrders w = new GetMerchantOrders();
            if (!string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
            {
                w.MerchantId = Session["MerchantUniqueId"].ToString();
            }
            w.OrderId = OrderId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.TransactionId = TransactionId;
            w.TrackerId = TrackerId;
            w.MemberContactNumber = MemberContactNumber;
            w.MemberName = MemberName;
            if (!String.IsNullOrEmpty(Type))
            {
                w.Type = Convert.ToInt32(Type);
            }
            if (!String.IsNullOrEmpty(PageType))
            {
                //if (PageType.ToLower() == "orders")
                //{
                //    w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                //}
                //else 
                if (PageType.ToLower() == "withdrawal")
                {
                    w.Sign = (int)AddMerchantOrders.MerchantOrderSign.Debit;
                }
            }
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantOrders
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantId = row["MerchantId"].ToString(),
                         OrderId = row["OrderId"].ToString(),
                         TransactionId = row["TransactionId"].ToString(),
                         TrackerId = row["TrackerId"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                         MemberName = row["MemberName"].ToString(),
                         MemberContactNumber = row["MemberContactNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), Convert.ToInt64(row["Status"])),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         Sign = Convert.ToInt32(row["Sign"]),
                         SignName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderSign), Convert.ToInt64(row["Sign"])),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"].ToString()),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         ServiceCharges = Convert.ToDecimal(row["ServiceCharges"].ToString()),
                         DiscountAmount = Convert.ToDecimal(row["DiscountAmount"].ToString()),
                         CommissionAmount = Convert.ToDecimal(row["CommissionAmount"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantOrders>> objDataTableResponse = new DataTableResponse<List<AddMerchantOrders>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        // GET: MerchantWithdrawalRequests
        [HttpGet]
        [Authorize]
        public ActionResult MerchantWithdrawalRequests()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantWithdrawalStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            List<SelectListItem> type = new List<SelectListItem>();
            type = CommonHelpers.GetSelectList_MerchantWithdrawalRequestType();
            ViewBag.Type = type;

            return View();
        }

        [Authorize]
        public JsonResult GetMerchantWithdrawalRequestsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
            columns.Add("Amount");
            columns.Add("Remarks");
            columns.Add("Status");

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

            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string Status = context.Request.Form["Status"];
            string Type = context.Request.Form["Type"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantWithdrawalRequest> trans = new List<AddMerchantWithdrawalRequest>();

            GetMerchantWithdrawalRequest w = new GetMerchantWithdrawalRequest();
            if (!string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
            {
                w.MerchantId = Session["MerchantUniqueId"].ToString();
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            if (!String.IsNullOrEmpty(Type))
            {
                w.WithdrawalRequestType = Convert.ToInt32(Type);
            }
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantWithdrawalRequest
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantId = row["MerchantId"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), Convert.ToInt64(row["Status"])),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         WithdrawalRequestType = Convert.ToInt32(row["WithdrawalRequestType"].ToString()),
                         WithdrawalRequestTypeName = @Enum.GetName(typeof(AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType), Convert.ToInt64(row["WithdrawalRequestType"])),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantWithdrawalRequest>> objDataTableResponse = new DataTableResponse<List<AddMerchantWithdrawalRequest>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }


        // GET: MerchantWithdrawalRequests
        [HttpGet]
        [Authorize]
        public ActionResult MerchantSettlementReport()
        {
            List<SelectListItem> type = new List<SelectListItem>();
            type = CommonHelpers.GetSelectList_KhaltiEnumMerchantTxn();
            ViewBag.Type = type;
            List<SelectListItem> sign = new List<SelectListItem>();
            sign = CommonHelpers.GetSelectList_TxnSign();
            ViewBag.Sign = sign;
            List<SelectListItem> status = new List<SelectListItem>();
            status = CommonHelpers.GetSelectList_TxnStatus();
            ViewBag.Status = status;

            return View();
        }

        [Authorize]
        public JsonResult GetMerchantSettlementReportLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
            columns.Add("Amount");
            columns.Add("Remarks");
            columns.Add("Status");

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
            string ContactNumber = context.Request.Form["MemberContactNumber"];
            string Name = context.Request.Form["MemberName"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string DayWise = context.Request.Form["Today"];
            string Sign = context.Request.Form["Sign"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            string Reference = context.Request.Form["Reference"];
            string CustomerId = context.Request.Form["CustomerId"];
            string Type = context.Request.Form["Type"];
            string TypeMultiple = context.Request.Form["TypeMultiple"];
            string WalletType = context.Request.Form["WalletType"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            Int64 MerchantMemberId = 0;
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
            AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            if (model != null && model.Id > 0)
            {
                MerchantMemberId = model.MerchantMemberId;
            }

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
            {
                w.MerchantMemberId = model.MerchantMemberId;
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            if (!String.IsNullOrEmpty(Sign))
            {
                w.Sign = Convert.ToInt32(Sign);
            }
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            if (!String.IsNullOrEmpty(Type))
            {
                w.Type = Convert.ToInt32(Type);
            }
            w.TransactionUniqueId = TransactionId;
            w.Reference = Reference;
            if (!String.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.MemberName = Name;
            w.ContactNumber = ContactNumber;
            w.RoleId = (int)AddUser.UserRoles.Merchant;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MerchantMemberId = Convert.ToInt64(row["MerchantMemberId"]),
                         MerchantId = Convert.ToString(row["MerchantId"]),
                         MerchantOrganization = Convert.ToString(row["MerchantOrganization"]),
                         RecieverName = Convert.ToString(row["RecieverName"]),
                         RecieverContactNumber = Convert.ToString(row["RecieverContactNumber"]),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                         MemberName = row["MemberName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         Sign = Convert.ToInt32(row["Sign"]),
                         SignName = @Enum.GetName(typeof(WalletTransactions.Signs), Convert.ToInt64(row["Sign"])),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"].ToString()),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<WalletTransactions>> objDataTableResponse = new DataTableResponse<List<WalletTransactions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelMerchantOrdersDump(string TransactionId, string Status, string OrderId, string TrackerId, string MemberName, string fromdate, string todate, string MemberContactNumber)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(fromdate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(todate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
                {
                    DateTime d1 = Convert.ToDateTime(fromdate);
                    DateTime d2 = Convert.ToDateTime(todate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 7)
                    {
                        errorMessage = "You cannot export report more than 7 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            try
            {
                if (errorMessage == "")
                {

                    fileName = "MerchantUserOrdersReport.xlsx";
                    //Save the file to server temp folder
                    string fullPath = Path.Combine(Server.MapPath("~/ExportData/MerchantUserOrders"), fileName);

                    //string fromdate = DateTime.UtcNow.AddDays(-7).ToString("dd-MMM-yyyy");
                    //string todate = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);

                    GetMerchantOrders w = new GetMerchantOrders();
                    if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                    {
                        w.StartDate = fromdate;
                        w.EndDate = todate;
                        w.StartDate = fromdate;
                        w.EndDate = todate;
                        w.Status = Convert.ToInt32(Status);
                        w.TransactionId = TransactionId;
                        w.TrackerId = TrackerId;
                        w.MemberName = MemberName;
                        w.MemberContactNumber = MemberContactNumber;
                        w.OrderId = OrderId;
                    }
                    if (!string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
                    {
                        w.MerchantId = Session["MerchantUniqueId"].ToString();
                    }

                    DataTable dt = w.GetDataDump();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddExportData outobject_file = new AddExportData();
                        GetExportData inobject_file = new GetExportData();
                        inobject_file.Type = (int)AddExportData.ExportType.MerchantLogin;
                        AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);

                        if (res_file != null && res_file.Id != 0)
                        {
                            string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/MerchantUserOrders"), res_file.FilePath);
                            if (System.IO.File.Exists(oldfilePath))
                            {
                                System.IO.File.Delete(oldfilePath);
                            }
                            res_file.FilePath = fullPath;
                            res_file.UpdatedBy = res.MerchantMemberId;
                            //res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res_file.UpdatedDate = DateTime.UtcNow;
                            res_file.IsDeleted = false;
                            bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                            if (status)
                            {

                                Common.AddLogs("Merchant login user order Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                            }
                        }
                        else
                        {
                            AddExportData export = new AddExportData();

                            export.CreatedBy = res.MerchantMemberId;
                            export.CreatedByName = res.FirstName + " " + res.LastName;
                            export.FilePath = fullPath;
                            export.Type = (int)AddExportData.ExportType.MerchantLogin;
                            export.IsActive = true;
                            export.IsApprovedByAdmin = true;
                            export.IsDeleted = false;
                            Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                            if (Id > 0)
                            {
                                Common.AddLogs("Merchant login user order Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                            }
                        }

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "MerchantUserOrders");
                            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Columns().AdjustToContents();  // Adjust column width
                            ws.Rows().AdjustToContents();
                            wb.SaveAs(fullPath);
                        }

                    }
                    else
                    {
                        errorMessage = "No data found for last 7 days";
                        fileName = "";
                    }
                }
                //Return the Excel file name
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }
        [HttpPost]
        [Authorize]
        public ActionResult DownloadMerchantOrdersFileName()
        {
            var errorMessage = "";
            AddExportData outobject_file = new AddExportData();
            GetExportData inobject_file = new GetExportData();
            inobject_file.Type = (int)AddExportData.ExportType.MerchantLogin;
            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
            if (res_file != null && res_file.Id != 0)
            {
                return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
            }
            else
            {
                errorMessage = "No flle found";
                return Json(new { fileName = "", errorMessage = errorMessage });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult DownloadMerchantOrdersFile(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/MerchantUserOrders"), fileName);
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
        // GET: MerchantDirectWalletLoadReport
        [HttpGet]
        [Authorize]
        public ActionResult MerchantDirectWalletLoadReport()
        {
            List<SelectListItem> sign = new List<SelectListItem>();
            sign = CommonHelpers.GetSelectList_TxnSign();
            ViewBag.Sign = sign;
            List<SelectListItem> status = new List<SelectListItem>();
            status = CommonHelpers.GetSelectList_TxnStatus();
            ViewBag.Status = status;

            return View();
        }

        [Authorize]
        public JsonResult GetMerchantDirectWalletLoadReportLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
            columns.Add("Amount");
            columns.Add("Remarks");
            columns.Add("Status");

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
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string Sign = context.Request.Form["Sign"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            string Reference = context.Request.Form["Reference"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            Int64 MerchantMemberId = 0;
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
            AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            if (model != null && model.Id > 0)
            {
                MerchantMemberId = model.MerchantMemberId;
            }

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            //if (!string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
            //{
            //    w.MerchantMemberId = model.MerchantMemberId;
            //}
            w.MerchantId = Session["MerchantUniqueId"].ToString();
            w.StartDate = fromdate;
            w.EndDate = todate;
            if (!String.IsNullOrEmpty(Sign))
            {
                w.Sign = Convert.ToInt32(Sign);
            }
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load;
            w.TransactionUniqueId = TransactionId;
            w.Reference = Reference;
            if (!String.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            //w.RoleId = (int)AddUser.UserRoles.Merchant;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MerchantMemberId = Convert.ToInt64(row["MerchantMemberId"]),
                         MerchantId = Convert.ToString(row["MerchantId"]),
                         MerchantOrganization = Convert.ToString(row["MerchantOrganization"]),
                         RecieverName = Convert.ToString(row["RecieverName"]),
                         RecieverContactNumber = Convert.ToString(row["RecieverContactNumber"]),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                         MemberName = row["MemberName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         Sign = Convert.ToInt32(row["Sign"]),
                         SignName = @Enum.GetName(typeof(WalletTransactions.Signs), Convert.ToInt64(row["Sign"])),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"].ToString()),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<WalletTransactions>> objDataTableResponse = new DataTableResponse<List<WalletTransactions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);
        }
    }
}