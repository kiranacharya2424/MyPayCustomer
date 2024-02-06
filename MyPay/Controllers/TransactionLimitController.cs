using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
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
    public class TransactionLimitController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            AddTransactionLimit model = new AddTransactionLimit();
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddTransactionLimit vmodel)
        {
            AddTransactionLimit model = new AddTransactionLimit();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewTransactionLimitDataRow(TransactionLimit req_ObjTransactionLimit_req)
        {

            var context = HttpContext;
            int TransactionTransferType = Convert.ToInt32(req_ObjTransactionLimit_req.TransactionTransferType);
            int TransferLimitPerTransaction = Convert.ToInt32(req_ObjTransactionLimit_req.TransferLimitPerTransaction);
            int TransferLimitPerDay = Convert.ToInt32(req_ObjTransactionLimit_req.TransferLimitPerDay);
            int TransferLimitPerMonth = Convert.ToInt32(req_ObjTransactionLimit_req.TransferLimitPerMonth);
            int TransferLimitPerDayTransactionCount = Convert.ToInt32(req_ObjTransactionLimit_req.TransferLimitPerDayTransactionCount);
            int TransferLimitPerMonthTransactionCount = Convert.ToInt32(req_ObjTransactionLimit_req.TransferLimitPerMonthTransactionCount);
            AddTransactionLimit ObjTransactionLimit = new AddTransactionLimit();
            ObjTransactionLimit.TransactionTransferType = TransactionTransferType;
            ObjTransactionLimit.TransferLimitPerTransaction = TransferLimitPerTransaction;
            ObjTransactionLimit.TransferLimitPerDay = TransferLimitPerDay;
            ObjTransactionLimit.TransferLimitPerMonth = TransferLimitPerMonth;
            ObjTransactionLimit.TransferLimitPerDayTransactionCount = TransferLimitPerDayTransactionCount;
            ObjTransactionLimit.TransferLimitPerMonthTransactionCount = TransferLimitPerMonthTransactionCount;
            ObjTransactionLimit.Id = req_ObjTransactionLimit_req.Id;
            ObjTransactionLimit.IsActive = true;
            ObjTransactionLimit.IsDeleted = false;
            //  Int64 i = RepCRUD<AddTransactionLimit, GetTransactionLimit>.Insert(ObjTransactionLimit, "TransactionLimit");
            return Json(ObjTransactionLimit, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult DeleteTransactionLimitDataRow(string Id)
        {

            AddTransactionLimit ObjTransactionLimit = new AddTransactionLimit();
            GetTransactionLimit inobjectTransactionLimit = new GetTransactionLimit();
            inobjectTransactionLimit.Id = Convert.ToInt64(Id);
            AddTransactionLimit resTransactionLimit = RepCRUD<GetTransactionLimit, AddTransactionLimit>.GetRecord(Common.StoreProcedures.sp_TransactionLimit_Get, inobjectTransactionLimit, ObjTransactionLimit);
            resTransactionLimit.IsDeleted = true;
            bool IsUpdated = RepCRUD<AddTransactionLimit, GetTransactionLimit>.Update(resTransactionLimit, "TransactionLimit");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated TransactionLimit(TransactionLimitId:" + Id + ") by(AdminId:" + Session["AdminMemberId"] + " )", true, Convert.ToInt32(AddLog.LogType.Rates));
                //Insert into TransactionLimitUpdateHistory Table
                AddTransactionLimit outobject = new AddTransactionLimit();
                GetTransactionLimit inobject = new GetTransactionLimit();
                inobject.Id = Convert.ToInt64(Id);
                AddTransactionLimit res = RepCRUD<GetTransactionLimit, AddTransactionLimit>.GetRecord(Common.StoreProcedures.sp_TransactionLimit_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddTransactionLimitHistory ObjTransactionLimithistory = new AddTransactionLimitHistory();
                    ObjTransactionLimithistory.TransactionLimitId = res.Id;
                    ObjTransactionLimithistory.TransactionTransferType = res.TransactionTransferType;
                    ObjTransactionLimithistory.TransferLimitPerTransaction = res.TransferLimitPerTransaction;
                    ObjTransactionLimithistory.TransferLimitPerDay = res.TransferLimitPerDay;
                    ObjTransactionLimithistory.TransferLimitPerMonth = res.TransferLimitPerMonth;
                    ObjTransactionLimithistory.TransferLimitPerDayTransactionCount = res.TransferLimitPerDayTransactionCount;
                    ObjTransactionLimithistory.TransferLimitPerMonthTransactionCount = res.TransferLimitPerMonthTransactionCount;
                    ObjTransactionLimithistory.KycStatus = res.KycStatus;
                    ObjTransactionLimithistory.IsActive = true;
                    ObjTransactionLimithistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjTransactionLimithistory.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 i = RepCRUD<AddTransactionLimitHistory, GetTransactionLimitHistory>.Insert(ObjTransactionLimithistory, "transactionlimithistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add TransactionLimitUpdateHistory(TransactionLimitId:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + " )", true, Convert.ToInt32(AddLog.LogType.Rates));
                    }
                }
            }
            return Json(ObjTransactionLimit, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetAdminTransactionLimitLists(GetTransactionLimit objval)
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Sno");
            columns.Add("Updated Date");
            columns.Add("TransactionTransferType");
            columns.Add("TransferLimitPerTransaction");
            columns.Add("TransferLimitPerDay");
            columns.Add("TransferLimitPerMonth");
            columns.Add("TransferLimitPerMonthTransactionCount");
            columns.Add("TransferLimitPerDayTransactionCount");
            columns.Add("KycStatus");
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
            
            TransactionLimits w = new TransactionLimits(); 
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            List<AddTransactionLimit> objTransactionLimit = (List<AddTransactionLimit>)CommonEntityConverter.DataTableToList<AddTransactionLimit>(dt);
            for (int i = 0; i < objTransactionLimit.Count; i++)
            {
                objTransactionLimit[i].Sno = (i + 1).ToString();
                objTransactionLimit[i].TransactionTransferTypeName = @Enum.GetName(typeof(AddTransactionLimit.TransactionTransferTypeEnum), objTransactionLimit[i].TransactionTransferType).ToString().ToUpper().ToString().Replace("_"," ");
                objTransactionLimit[i].KycStatusName = @Enum.GetName(typeof(MyPay.Models.Add.AddTransactionLimit.KycTypes), objTransactionLimit[i].KycStatus).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddTransactionLimit>> objDataTableResponse = new DataTableResponse<List<AddTransactionLimit>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objTransactionLimit
            };

            return Json(objDataTableResponse);

        }
        [HttpPost]
        [Authorize]
        public JsonResult TransactionLimitUpdateCall(TransactionLimit req_ObjTransactionLimit_req)
        {
            AddTransactionLimit ObjTransactionLimit = new AddTransactionLimit
            {
                Id = req_ObjTransactionLimit_req.Id,
                TransactionTransferType = req_ObjTransactionLimit_req.TransactionTransferType,
                TransferLimitPerTransaction = req_ObjTransactionLimit_req.TransferLimitPerTransaction,
                TransferLimitPerDay = req_ObjTransactionLimit_req.TransferLimitPerDay,
                TransferLimitPerMonth = req_ObjTransactionLimit_req.TransferLimitPerMonth,
                TransferLimitPerDayTransactionCount = req_ObjTransactionLimit_req.TransferLimitPerDayTransactionCount,
                TransferLimitPerMonthTransactionCount = req_ObjTransactionLimit_req.TransferLimitPerMonthTransactionCount,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Convert.ToInt64(Session["AdminMemberId"]),
                CreatedByName = Session["AdminUserName"].ToString(),
                KycStatus = req_ObjTransactionLimit_req.KycStatus
            };
            bool IsUpdated = RepCRUD<AddTransactionLimit, GetTransactionLimit>.Update(ObjTransactionLimit, "TransactionLimit");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated TransactionLimit(TransactionLimitId:" + req_ObjTransactionLimit_req.Id + ") by(AdminId:" + Session["AdminMemberId"] + " )", true, Convert.ToInt32(AddLog.LogType.Rates));
                //Insert into TransactionLimitUpdateHistory Table
                AddTransactionLimit outobject_update = new AddTransactionLimit();
                GetTransactionLimit inobject_update = new GetTransactionLimit();
                inobject_update.Id = ObjTransactionLimit.Id;
                AddTransactionLimit res = RepCRUD<GetTransactionLimit, AddTransactionLimit>.GetRecord(Common.StoreProcedures.sp_TransactionLimit_Get, inobject_update, outobject_update);
                if (res != null && res.Id != 0)
                {
                    AddTransactionLimitHistory ObjTransactionLimithistory = new AddTransactionLimitHistory();
                    ObjTransactionLimithistory.TransactionLimitId = res.Id;
                    ObjTransactionLimithistory.TransactionTransferType = res.TransactionTransferType;
                    ObjTransactionLimithistory.TransferLimitPerTransaction = res.TransferLimitPerTransaction;
                    ObjTransactionLimithistory.TransferLimitPerDay = res.TransferLimitPerDay;
                    ObjTransactionLimithistory.TransferLimitPerMonth = res.TransferLimitPerMonth;
                    ObjTransactionLimithistory.TransferLimitPerDayTransactionCount = res.TransferLimitPerDayTransactionCount;
                    ObjTransactionLimithistory.TransferLimitPerMonthTransactionCount = res.TransferLimitPerMonthTransactionCount;
                    ObjTransactionLimithistory.KycStatus = res.KycStatus;
                    ObjTransactionLimithistory.IsActive = true;
                    ObjTransactionLimithistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjTransactionLimithistory.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 i = RepCRUD<AddTransactionLimitHistory, GetTransactionLimitHistory>.Insert(ObjTransactionLimithistory, "transactionlimithistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add TransactionLimitHistory(TransactionLimitId:" + res.Id + ") by(AdminId:"+ Session["AdminMemberId"] +" )", true, Convert.ToInt32(AddLog.LogType.Rates));
                    }
                }
            }
            return Json(ObjTransactionLimit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateTransactionLimitCall(TransactionLimit req_ObjTransactionLimit_req, string IsActive)
        {

            AddTransactionLimit outobject = new AddTransactionLimit();
            GetTransactionLimit inobject = new GetTransactionLimit();
            inobject.Id = req_ObjTransactionLimit_req.Id;
            AddTransactionLimit objUpdateTransactionLimit = RepCRUD<GetTransactionLimit, AddTransactionLimit>.GetRecord(Common.StoreProcedures.sp_TransactionLimit_Get, inobject, outobject);
            objUpdateTransactionLimit.IsActive = Convert.ToBoolean(IsActive);
            objUpdateTransactionLimit.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateTransactionLimit.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateTransactionLimit.CreatedByName = Session["AdminUserName"].ToString();
            bool IsUpdated = RepCRUD<AddTransactionLimit, GetTransactionLimit>.Update(objUpdateTransactionLimit, "TransactionLimit");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated TransactionLimit Status(TransactionLimitId:" + req_ObjTransactionLimit_req.Id + ") by(AdminId:" + Session["AdminMemberId"] + " )", true, Convert.ToInt32(AddLog.LogType.Rates));
            }
            return Json(objUpdateTransactionLimit, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult GetProviderServiceList(string ProviderId)
        {
            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_Providerchange(ProviderId);
            return Json(districtlist);
        }
        [HttpGet]
        [Authorize]
        public ActionResult TransactionLimitHistory()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetTransactionLimitHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Type");
            columns.Add("Limit/Transaction");
            columns.Add("Amount/Day");
            columns.Add("Amount/Month");
            columns.Add("Transaction/Month");
            columns.Add("Transaction/Day");
            columns.Add("KYC Type");
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
            string KycStatus = context.Request.Form["KycStatus"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddTransactionLimitHistory> trans = new List<AddTransactionLimitHistory>();
            GetTransactionLimitHistory w = new GetTransactionLimitHistory();
            //w.KycStatus = Convert.ToInt32(KycStatus);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddTransactionLimitHistory
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         //TransferLimitTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["TransactionTransferType"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         TransferLimitPerDay = Convert.ToDecimal(row["TransferLimitPerDay"]),
                         TransferLimitPerMonth= Convert.ToDecimal(row["TransferLimitPerMonth"]),
                         TransferLimitPerTransaction = Convert.ToDecimal(row["TransferLimitPerTransaction"]),
                         TransferLimitPerDayTransactionCount = Convert.ToInt64(row["TransferLimitPerDayTransactionCount"]),
                         TransferLimitPerMonthTransactionCount = Convert.ToInt64(row["TransferLimitPerTransaction"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         TransferLimitTypeName = @Enum.GetName(typeof(AddTransactionLimit.TransactionTransferTypeEnum), Convert.ToInt64(row["TransactionTransferType"])).ToString().ToUpper().Replace("_", " ").ToString(),
                         StatusName=@Enum.GetName(typeof(AddTransactionLimit.KycTypes),Convert.ToInt32(row["KycStatus"])),
                         CreatedDateDt = row["CreatedDateDt"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddTransactionLimitHistory>> objDataTableResponse = new DataTableResponse<List<AddTransactionLimitHistory>>
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