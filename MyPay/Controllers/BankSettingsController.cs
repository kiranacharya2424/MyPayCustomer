using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class BankSettingsController : BaseAdminSessionController
    {
        // GET: BankSettings
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            ApiSetting objApiSettings = new ApiSetting();
            using (var db = new MyPayEntities())
            {
                objApiSettings = db.ApiSettings.FirstOrDefault();
            }
            ViewBag.BankTransferType = objApiSettings.BankTransferType;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(string BankTransferType)
        {
            int dbresult = 0;
            ApiSetting objApiSettings = new ApiSetting();
            using (var db = new MyPayEntities())
            {
                objApiSettings = db.ApiSettings.FirstOrDefault();
                objApiSettings.BankTransferType = Convert.ToInt32(BankTransferType);
                dbresult = db.SaveChanges();

                if (dbresult == 1)
                {
                    AddApiSettingsHistory outobject = new AddApiSettingsHistory();
                    outobject.BankTransferType = Convert.ToInt32(BankTransferType);
                    outobject.LinkBankTransferType = Convert.ToInt32(objApiSettings.LinkBankTransferType);
                    outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    outobject.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    outobject.CreatedByName = Session["AdminUserName"].ToString();
                    outobject.UpdatedByName = Session["AdminUserName"].ToString();
                    outobject.IsActive = true;
                    Int64 Id = RepCRUD<AddApiSettingsHistory, GetApiSettingsHistory>.Insert(outobject, "apisettingshistory");
                    Common.AddLogs("Updated Bank Settings by(AdminMemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Maintenance, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());

                    ViewBag.SuccessMessage = "Updated Successfully";
                }
                else
                {
                    ViewBag.Message = "Not Updated";
                }
            }
            ViewBag.BankTransferType = objApiSettings.BankTransferType;
            return View();
        }

        //BankSettingsHistory
        [HttpGet]
        [Authorize]
        public ActionResult BankSettingsHistory()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetBankSettingsHistory()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            //columns.Add("UpdatedDate");
            columns.Add("BankTransferType");
            //columns.Add("CreatedBy");
            columns.Add("UpdatedBy");

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
            string BankTransferType = context.Request.Form["BankTransferType"];

            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddApiSettingsHistory> trans = new List<AddApiSettingsHistory>();

            GetApiSettingsHistory w = new GetApiSettingsHistory();
            w.BankTransferType = Convert.ToInt32(BankTransferType);

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddApiSettingsHistory
                     {
                         Sno = row["Sno"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         BankTransferType = Convert.ToInt32(row["BankTransferType"].ToString()),
                         MPCoinsDateSettingsDT = Convert.ToDateTime(row["MPCoinsDateSettings"].ToString()).ToString("dd MMM yyyy")
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddApiSettingsHistory>> objDataTableResponse = new DataTableResponse<List<AddApiSettingsHistory>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }
        [HttpGet]
        [Authorize]
        public ActionResult LinkBankSettings()
        {
            ApiSetting objApiSettings = new ApiSetting();
            using (var db = new MyPayEntities())
            {
                objApiSettings = db.ApiSettings.FirstOrDefault();
            }
            AddApiSettings apiSetting = new AddApiSettings();
            apiSetting.LinkBankTypeEnum = (AddApiSettings.LinkBankType)objApiSettings.LinkBankTransferType;
            return View(apiSetting);
        }
        [HttpPost]
        [Authorize]
        public ActionResult LinkBankSettings(AddApiSettings apiSetting)
        {
            int dbresult = 0;
            ApiSetting objApiSettings = new ApiSetting();
            using (var db = new MyPayEntities())
            {
                objApiSettings = db.ApiSettings.FirstOrDefault();
                objApiSettings.LinkBankTransferType = Convert.ToInt32(apiSetting.LinkBankTypeEnum);
                dbresult = db.SaveChanges();

                if (dbresult == 1)
                {
                    AddApiSettingsHistory outobject = new AddApiSettingsHistory();
                    outobject.BankTransferType = Convert.ToInt32(objApiSettings.BankTransferType);
                    outobject.LinkBankTransferType = Convert.ToInt32(objApiSettings.LinkBankTransferType);
                    outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    outobject.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    outobject.CreatedByName = Session["AdminUserName"].ToString();
                    outobject.UpdatedByName = Session["AdminUserName"].ToString();
                    outobject.IsActive = true;
                    Int64 Id = RepCRUD<AddApiSettingsHistory, GetApiSettingsHistory>.Insert(outobject, "apisettingshistory");
                    Common.AddLogs("Updated Link Bank Settings by(AdminMemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Maintenance, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());

                    ViewBag.SuccessMessage = "Updated Successfully";
                }
                else
                {
                    ViewBag.Message = "Not Updated";
                }
            }

            return View();
        }

    }
}