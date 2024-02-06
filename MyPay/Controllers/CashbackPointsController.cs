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
    public class CashbackPointsController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddSettings vmodel)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetAdminCashbacksLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Created By");
            columns.Add("Type");
            columns.Add("Registration Commision");
            columns.Add("Registration Points");
            columns.Add("SignUp Bonus");
            columns.Add("SignUp Bonus Points");
            columns.Add("KYC Commission");
            columns.Add("KYC Points");
            columns.Add("KYCStatus");
            columns.Add("GenderType");
            columns.Add("GenderTypeName");
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
            string Id = context.Request.Form["Id"];
            string Type = context.Request.Form["Type"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            GetSettings w = new GetSettings();
            w.Type = Convert.ToInt32(Type);
            w.CheckDelete = 0;
            w.CheckActive = 1;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);
            List<AddSettings> trans = new List<AddSettings>();
            trans = (from DataRow row in dt.Rows

                     select new AddSettings
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         RegistrationCommission = Convert.ToDecimal(row["RegistrationCommission"]),
                         RegistrationRewardPoint = Convert.ToDecimal(row["RegistrationRewardPoint"]),
                         SignUpBonus = Convert.ToDecimal(row["SignUpBonus"]),
                         SignUpBonusRewardPoint = Convert.ToDecimal(row["SignUpBonusRewardPoint"]),
                         KYCCommission = Convert.ToDecimal(row["KYCCommission"]),
                         KYCRewardPoint = Convert.ToDecimal(row["KYCRewardPoint"]),
                         TransactionCommission = Convert.ToDecimal(row["TransactionCommission"]),
                         TransactionRewardPoint = Convert.ToDecimal(row["TransactionRewardPoint"]),
                         MaxAmountTransactionCommission = Convert.ToDecimal(row["MaxAmountTransactionCommission"]),
                         MinAmountTransactionCommission = Convert.ToDecimal(row["MinAmountTransactionCommission"]),
                         MinRewardPointTransactionCommission = Convert.ToDecimal(row["MinRewardPointTransactionCommission"]),
                         MaxRewardPointTransactionCommission = Convert.ToDecimal(row["MaxRewardPointTransactionCommission"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         IsKYCApproved = Convert.ToInt32(row["IsKYCApproved"].ToString()),
                         KYCStatusName = @Enum.GetName(typeof(AddSettings.KycTypes), Convert.ToInt64(row["IsKycApproved"].ToString())),
                         GenderType = Convert.ToInt32(row["GenderType"].ToString()),
                         GenderTypeName = @Enum.GetName(typeof(AddSettings.GenderTypes), Convert.ToInt64(row["GenderType"].ToString())).Replace("_", " "),
                         TypeName = @Enum.GetName(typeof(AddSettings.ReferType), Convert.ToInt64(row["Type"])),
                         Type = Convert.ToInt32(row["Type"])
                     }).OrderBy(c => c.KYCStatusName).ThenBy(c => c.GenderType).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddSettings>> objDataTableResponse = new DataTableResponse<List<AddSettings>>
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
        public JsonResult CashbackUpdateCall(AddSettings vmodel)
        {

            AddSettings outobject = new AddSettings();
            GetSettings inobject = new GetSettings();
            inobject.Id = vmodel.Id;
            if (ModelState.IsValid)
            {
                AddSettings objUpdate = RepCRUD<GetSettings, AddSettings>.GetRecord(Common.StoreProcedures.sp_Settings_Get, inobject, outobject);
                objUpdate.RegistrationCommission = Convert.ToDecimal(vmodel.RegistrationCommission);
                objUpdate.SignUpBonus = Convert.ToDecimal(vmodel.SignUpBonus);
                objUpdate.KYCCommission = Convert.ToDecimal(vmodel.KYCCommission);
                objUpdate.TransactionCommission = Convert.ToDecimal(vmodel.TransactionCommission);
                objUpdate.RegistrationRewardPoint = Convert.ToDecimal(vmodel.RegistrationRewardPoint);
                objUpdate.SignUpBonusRewardPoint = Convert.ToDecimal(vmodel.SignUpBonusRewardPoint);
                objUpdate.KYCRewardPoint = Convert.ToDecimal(vmodel.KYCRewardPoint);
                objUpdate.TransactionRewardPoint = Convert.ToDecimal(vmodel.TransactionRewardPoint);
                objUpdate.MinAmountTransactionCommission = Convert.ToDecimal(vmodel.MinAmountTransactionCommission);
                objUpdate.MaxAmountTransactionCommission = Convert.ToDecimal(vmodel.MaxAmountTransactionCommission);
                objUpdate.MinRewardPointTransactionCommission = Convert.ToDecimal(vmodel.MinRewardPointTransactionCommission);
                objUpdate.MaxRewardPointTransactionCommission = Convert.ToDecimal(vmodel.MaxRewardPointTransactionCommission);
                bool IsUpdated = RepCRUD<AddSettings, GetSettings>.Update(objUpdate, "settings");
                if (IsUpdated)
                {
                    //Insert Into SettingsHistory
                    AddSettingsHistory outobject_history = new AddSettingsHistory();
                    outobject_history.SettingsId = objUpdate.Id;
                    outobject_history.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    outobject_history.CreatedByName = Session["AdminUserName"].ToString();
                    outobject_history.IsActive = true;
                    outobject_history.IsApprovedByAdmin = true;
                    outobject_history.KYCCommission = objUpdate.KYCCommission;
                    outobject_history.KYCRewardPoint = objUpdate.KYCRewardPoint;
                    outobject_history.RegistrationCommission = objUpdate.RegistrationCommission;
                    outobject_history.RegistrationRewardPoint = objUpdate.RegistrationRewardPoint;
                    outobject_history.SignUpBonus = objUpdate.SignUpBonus;
                    outobject_history.SignUpBonusRewardPoint = objUpdate.SignUpBonusRewardPoint;
                    outobject_history.TransactionCommission = objUpdate.TransactionCommission;
                    outobject_history.TransactionRewardPoint = objUpdate.TransactionRewardPoint;
                    outobject_history.MinAmountTransactionCommission = objUpdate.MinAmountTransactionCommission;
                    outobject_history.MaxAmountTransactionCommission = objUpdate.MaxAmountTransactionCommission;
                    outobject_history.MinRewardPointTransactionCommission = objUpdate.MinRewardPointTransactionCommission;
                    outobject_history.MaxRewardPointTransactionCommission = objUpdate.MaxRewardPointTransactionCommission;
                    outobject_history.IsKycApproved = objUpdate.IsKYCApproved;
                    outobject_history.GenderType = objUpdate.GenderType;
                    outobject_history.Type = objUpdate.Type;
                    Int64 i = RepCRUD<AddSettingsHistory, GetSettingsHistory>.Insert(outobject_history, "settingshistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Cashback and Reward Points are successfully Added in Settings History", true, Convert.ToInt32(AddLog.LogType.Rates));
                    }
                    Common.AddLogs("Cashback and Reward Points are successfully Updated in Settings", true, Convert.ToInt32(AddLog.LogType.Rates));
                    ViewBag.SuccessMessage = "Cashback and Reward Points are successfully updated";
                }
                else
                {
                    ViewBag.Message = "Not Updated";
                }
            }
            AddSettings model = RepCRUD<GetSettings, AddSettings>.GetRecord(Common.StoreProcedures.sp_Settings_Get, inobject, outobject);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CashbackPointsHistory()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetCashbackPointsHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Type");
            columns.Add("Reg Commission");
            columns.Add("KYC Commission");
            columns.Add("Reg Reward Point");
            columns.Add("KYC Reward Point");
            columns.Add("Txn Commission(%)");
            columns.Add("TXN Reward Points(%)");
            columns.Add("TXN Commission(Min)");
            columns.Add("TXN Commission(Max)");
            columns.Add("TXN Reward Points(Min)");
            columns.Add("TXN Reward Points(Max)");
            columns.Add("KYC Status");
            columns.Add("Gender");
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
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            string Type = context.Request.Form["Type"];

            List<AddSettingsHistory> trans = new List<AddSettingsHistory>();
            GetSettingsHistory w = new GetSettingsHistory();
            w.Type = Convert.ToInt32(Type);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddSettingsHistory
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         KYCCommission = Convert.ToDecimal(row["KYCCommission"]),
                         KYCRewardPoint = Convert.ToDecimal(row["KYCRewardPoint"]),
                         RegistrationCommission = Convert.ToDecimal(row["RegistrationCommission"]),
                         RegistrationRewardPoint = Convert.ToDecimal(row["RegistrationRewardPoint"]),
                         TransactionCommission = Convert.ToDecimal(row["TransactionCommission"]),
                         TransactionRewardPoint = Convert.ToDecimal(row["TransactionRewardPoint"]),
                         CreatedDateDt = row["IndiaDate"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         KYCStatusName = @Enum.GetName(typeof(AddSettings.KycTypes), Convert.ToInt64(row["IsKycApproved"].ToString())),
                         GenderType = Convert.ToInt32(row["GenderType"].ToString()),
                         GenderTypeName = @Enum.GetName(typeof(AddSettings.GenderTypes), Convert.ToInt64(row["GenderType"].ToString())),
                         TypeName = @Enum.GetName(typeof(AddSettings.ReferType), Convert.ToInt64(row["Type"])),
                         Type = Convert.ToInt32(row["Type"]),
                         MaxAmountTransactionCommission = Convert.ToDecimal(row["MaxAmountTransactionCommission"]),
                         MinAmountTransactionCommission = Convert.ToDecimal(row["MinAmountTransactionCommission"]),
                         MinRewardPointTransactionCommission = Convert.ToDecimal(row["MinRewardPointTransactionCommission"]),
                         MaxRewardPointTransactionCommission = Convert.ToDecimal(row["MaxRewardPointTransactionCommission"]),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddSettingsHistory>> objDataTableResponse = new DataTableResponse<List<AddSettingsHistory>>
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