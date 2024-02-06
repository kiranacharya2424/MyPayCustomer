using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class ActivityLogsController : BaseAdminSessionController
    {
        [Authorize]
        // GET: ActivityLogs
        public ActionResult Index()
        {
            List<SelectListItem> ActivityLogs = CommonHelpers.GetSelectList_ActivityLog();
            ViewBag.LogActivity = ActivityLogs;

            List<SelectListItem> OldUserStatus = CommonHelpers.GetSelectList_OldUserStatus();
            ViewBag.OldUserStatus = OldUserStatus;

            List<SelectListItem> UserType = CommonHelpers.GetSelectList_UserType();
            ViewBag.UserType = UserType;
            return View();
        }

        [Authorize]
        public ActionResult BankTransactionsLog()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "SUCCESS",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "NOT SUCCESS",
                Value = "2"
            });

            ViewBag.status = items;
            return View();
        }

        [Authorize]
        public JsonResult GetBankTransactionsLogLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("TxnId");
            columns.Add("BatchId");
            columns.Add("InstructionId");
            columns.Add("DebitStatus");
            columns.Add("CreditStatus");
            columns.Add("Amount");
            columns.Add("Description");
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

            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string searchtext = context.Request.Form["searchtext"];
            string status = context.Request.Form["status"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            string msg = "";
            List<GetAllCipsTransctions> list = RepNCHL.GetAllBankTransactions(Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd"), Convert.ToDateTime(todate).ToString("yyyy-MM-dd"), ref msg);

            if (!string.IsNullOrEmpty(searchtext))
            {
                list = list.Where(x => x.cipsTransactionDetailList[0].instructionId == searchtext.Trim() || x.cipsBatchDetail.id == searchtext.Trim() || x.cipsBatchDetail.batchId == searchtext.Trim()).ToList();
            }
            if (!string.IsNullOrEmpty(status) && status != "0")
            {
                if (status == "1")
                {
                    list = list.Where(x => x.cipsTransactionDetailList[0].reasonDesc.ToUpper() == "SUCCESS").ToList();
                }
                else
                {
                    list = list.Where(x => x.cipsTransactionDetailList[0].reasonDesc.ToUpper() != "SUCCESS").ToList();
                }
            }


            Int32 recordFiltered = list.Count > 0 ? list.Count : 0;

            DataTableResponse<List<GetAllCipsTransctions>> objDataTableResponse = new DataTableResponse<List<GetAllCipsTransctions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = list
            };

            return Json(objDataTableResponse);

        }


        [Authorize]
        public JsonResult GetLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("UserId");
            columns.Add("UserType");
            columns.Add("ContactNumber");
            columns.Add("OldUserStatusName");
            columns.Add("Action");
            columns.Add("IpAddress");
            columns.Add("Platform");

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
            string UserId = context.Request.Form["UserId"];
            string UserType = context.Request.Form["UserType"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetLog inobject_logs = new GetLog();
            string LogActivity = context.Request.Form["LogActivity"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string OldUserStatus = context.Request.Form["OldUserStatus"];
            List<AddLog> logs = new List<AddLog>();

            AddLog w = new AddLog();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.UserId = UserId;
            w.LogActivity = Convert.ToInt32(LogActivity);
            w.OldUserStatus = Convert.ToInt32(OldUserStatus);
            w.UserType = Convert.ToInt32(UserType);
            w.ContactNumber = Convert.ToString(ContactNumber);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            logs = (from DataRow row in dt.Rows

                    select new AddLog
                    {
                        MemberId = Convert.ToInt64(row["MemberId"]),
                        UserId = row["UserId"].ToString(),
                        ContactNumber = row["ContactNumber"].ToString(),
                        OldUserStatusName = Enum.GetName(typeof(AddUser.OldAndNewUser), Convert.ToInt16(row["OldUserStatus"])).ToString(),
                        UserTypeName = Enum.GetName(typeof(AddUser.UserType), Convert.ToInt16(row["IsAdmin"])).ToString(),
                        Action = row["Action"].ToString(),
                        Platform = row["Platform"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        DeviceCode = row["DeviceCode"].ToString(),
                        CreatedBy = Convert.ToInt64(row["CreatedBy"].ToString()),
                        CreatedByName = row["CreatedByName"].ToString(),
                        //Type= Convert.ToInt32(row["Type"]),
                        //TypeName = @Enum.GetName(typeof(AddLog.LogType), Convert.ToInt64(row["Type"])).ToString(),
                        CreatedDatedt = row["IndiaDate"].ToString()


                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddLog>> objDataTableResponse = new DataTableResponse<List<AddLog>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = logs
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        // GET: BankTransferLogs
        public ActionResult BankTransferLogs()
        {
            return View();
        }
        [Authorize]
        public JsonResult GetBankTransferLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("UserId");
            columns.Add("Action");
            columns.Add("IpAddress");
            columns.Add("Platform");
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
            string UserId = context.Request.Form["UserId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetLog inobject_logs = new GetLog();

            List<AddLog> logs = new List<AddLog>();

            AddLog w = new AddLog();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.UserId = UserId;
            w.Type = (int)AddLog.LogType.BankTransfer;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            logs = (from DataRow row in dt.Rows

                    select new AddLog
                    {
                        MemberId = Convert.ToInt64(row["MemberId"]),
                        UserId = row["UserId"].ToString(),
                        Action = row["Action"].ToString(),
                        Platform = row["Platform"].ToString(),
                        //TypeName = @Enum.GetName(typeof(AddLog.LogType), Convert.ToInt64(row["Type"])).ToString(),
                        CreatedDatedt = row["IndiaDate"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        DeviceCode = row["DeviceCode"].ToString()

                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddLog>> objDataTableResponse = new DataTableResponse<List<AddLog>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = logs
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        // GET: ConnectIPSLogs
        public ActionResult ConnectIPSLogs()
        {
            return View();
        }
        [Authorize]
        public JsonResult GetConnectIPSLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("UserId");
            columns.Add("Action");
            columns.Add("IpAddress");
            columns.Add("Platform");
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
            string UserId = context.Request.Form["UserId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetLog inobject_logs = new GetLog();

            List<AddLog> logs = new List<AddLog>();

            AddLog w = new AddLog();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.UserId = UserId;
            w.Type = (int)AddLog.LogType.ConnectIps_Deposit;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            logs = (from DataRow row in dt.Rows

                    select new AddLog
                    {
                        MemberId = Convert.ToInt64(row["MemberId"]),
                        UserId = row["UserId"].ToString(),
                        Action = row["Action"].ToString(),
                        Platform = row["Platform"].ToString(),
                        //TypeName = @Enum.GetName(typeof(AddLog.LogType), Convert.ToInt64(row["Type"])).ToString(),
                        CreatedDatedt = row["IndiaDate"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        DeviceCode = row["DeviceCode"].ToString()

                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddLog>> objDataTableResponse = new DataTableResponse<List<AddLog>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = logs
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        // GET: CardPaymentLogs
        public ActionResult CardPaymentLogs()
        {
            return View();
        }
        [Authorize]
        public JsonResult GetCardPaymentLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("UserId");
            columns.Add("Action");
            columns.Add("IpAddress");
            columns.Add("Platform");
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
            string UserId = context.Request.Form["UserId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetLog inobject_logs = new GetLog();

            List<AddLog> logs = new List<AddLog>();

            AddLog w = new AddLog();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.UserId = UserId;
            w.Type = (int)AddLog.LogType.CardPayment;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            logs = (from DataRow row in dt.Rows

                    select new AddLog
                    {
                        MemberId = Convert.ToInt64(row["MemberId"]),
                        UserId = row["UserId"].ToString(),
                        Action = row["Action"].ToString(),
                        Platform = row["Platform"].ToString(),
                        //TypeName = @Enum.GetName(typeof(AddLog.LogType), Convert.ToInt64(row["Type"])).ToString(),
                        CreatedDatedt = row["IndiaDate"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        DeviceCode = row["DeviceCode"].ToString()

                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddLog>> objDataTableResponse = new DataTableResponse<List<AddLog>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = logs
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        // GET: InternetBankingLogs
        public ActionResult InternetBankingLogs()
        {
            return View();
        }
        [Authorize]
        public JsonResult GetInternetBankingLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("UserId");
            columns.Add("Action");
            columns.Add("IpAddress");
            columns.Add("Platform");
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
            string UserId = context.Request.Form["UserId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetLog inobject_logs = new GetLog();

            List<AddLog> logs = new List<AddLog>();

            AddLog w = new AddLog();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.UserId = UserId;
            w.Type = (int)AddLog.LogType.Internet_Banking;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            logs = (from DataRow row in dt.Rows

                    select new AddLog
                    {
                        MemberId = Convert.ToInt64(row["MemberId"]),
                        UserId = row["UserId"].ToString(),
                        Action = row["Action"].ToString(),
                        Platform = row["Platform"].ToString(),
                        //TypeName = @Enum.GetName(typeof(AddLog.LogType), Convert.ToInt64(row["Type"])).ToString(),
                        CreatedDatedt = row["IndiaDate"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        DeviceCode = row["DeviceCode"].ToString()

                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddLog>> objDataTableResponse = new DataTableResponse<List<AddLog>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = logs
            };

            return Json(objDataTableResponse);

        }


        [Authorize]
        // GET: P2PLogs
        public ActionResult P2PLogs()
        {
            return View();
        }
        [Authorize]
        public JsonResult GetP2PLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("UserId");
            columns.Add("Action");
            columns.Add("IpAddress");
            columns.Add("Platform");
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
            string UserId = context.Request.Form["UserId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetLog inobject_logs = new GetLog();

            List<AddLog> logs = new List<AddLog>();

            AddLog w = new AddLog();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.UserId = UserId;
            //w.Type = (int)AddLog.LogType.Internet_Banking;
            w.LogActivity = (int)AddLog.LogActivityEnum.Fund_Transfer;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            logs = (from DataRow row in dt.Rows

                    select new AddLog
                    {
                        MemberId = Convert.ToInt64(row["MemberId"]),
                        UserId = row["UserId"].ToString(),
                        Action = row["Action"].ToString(),
                        Platform = row["Platform"].ToString(),
                        //TypeName = @Enum.GetName(typeof(AddLog.LogType), Convert.ToInt64(row["Type"])).ToString(),
                        CreatedDatedt = row["IndiaDate"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        DeviceCode = row["DeviceCode"].ToString()

                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddLog>> objDataTableResponse = new DataTableResponse<List<AddLog>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = logs
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        // GET: MobileBankingLogs
        public ActionResult MobileBankingLogs()
        {
            return View();
        }
        [Authorize]
        public JsonResult GetMobileBankingLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("UserId");
            columns.Add("Action");
            columns.Add("IpAddress");
            columns.Add("Platform");
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
            string UserId = context.Request.Form["UserId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetLog inobject_logs = new GetLog();

            List<AddLog> logs = new List<AddLog>();

            AddLog w = new AddLog();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.UserId = UserId;
            w.Type = (int)AddLog.LogType.Mobile_Banking;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            logs = (from DataRow row in dt.Rows

                    select new AddLog
                    {
                        MemberId = Convert.ToInt64(row["MemberId"]),
                        UserId = row["UserId"].ToString(),
                        Action = row["Action"].ToString(),
                        Platform = row["Platform"].ToString(),
                        //TypeName = @Enum.GetName(typeof(AddLog.LogType), Convert.ToInt64(row["Type"])).ToString(),
                        CreatedDatedt = row["IndiaDate"].ToString(),
                        IpAddress = row["IpAddress"].ToString(),
                        DeviceCode = row["DeviceCode"].ToString()

                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddLog>> objDataTableResponse = new DataTableResponse<List<AddLog>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = logs
            };

            return Json(objDataTableResponse);

        }
    }
}