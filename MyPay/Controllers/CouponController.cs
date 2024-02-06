using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class CouponController : BaseAdminSessionController
    {
        // GET: Coupon
        [Authorize]
        public ActionResult Index()
        {
            AddCoupons model = new AddCoupons();
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            inobject.RoleId = (int)AddUser.UserRoles.User;
            List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
            // *********  ADD DEFAULT VALUE IN DROPDOWN ********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Category ---";
                objDefault.Selected = true;
                objProviderServiceCategoryList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objProviderServiceCategoryList.Count; i++)
            {

                SelectListItem objItem = new SelectListItem();
                objItem.Value = objProviderServiceCategoryList[i].Id.ToString();
                objItem.Text = objProviderServiceCategoryList[i].ProviderCategoryName.ToString();
                objProviderServiceCategoryList_SelectList.Add(objItem);


            }
            ViewBag.ProviderType = objProviderServiceCategoryList_SelectList;

            // *********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN ********** //

            {
                List<SelectListItem> objProviderURLList = new List<SelectListItem>();
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Service ---";
                objDefault.Selected = true;
                objProviderURLList.Add(objDefault);
                ViewBag.SERVICEId = objProviderURLList;
            }

            List<SelectListItem> CouponKycTypelist = CommonHelpers.GetSelectList_CouponKycType();
            ViewBag.KycStatus = CouponKycTypelist;
            ViewBag.KycStatusListJson = JsonConvert.SerializeObject(CouponKycTypelist);

            List<SelectListItem> CouponGenderTypelist = CommonHelpers.GetSelectList_CouponGenderType();
            ViewBag.GenderStatus = CouponGenderTypelist;

            ViewBag.GenderListJson = JsonConvert.SerializeObject(CouponGenderTypelist);
            ViewBag.CategoryListJson = JsonConvert.SerializeObject(objProviderServiceCategoryList_SelectList);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetAdminCouponLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Sno");
            columns.Add("ServiceId");
            columns.Add("ServiceName");
            columns.Add("Title");
            columns.Add("Description");
            columns.Add("Amount");
            columns.Add("CouponPercentage");
            columns.Add("CouponsCount");
            columns.Add("CouponsUsedCoupens");
            columns.Add("GenderTypeName");
            columns.Add("KycTypeName");
            columns.Add("FromDate");
            columns.Add("ToDate");
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
            string Title = context.Request.Form["Title"];
            string Description = context.Request.Form["Description"];
            string Amount = context.Request.Form["Amount"];
            string CouponPercentage = context.Request.Form["CouponPercentage"];
            string CouponsCount = context.Request.Form["CouponsCount"];
            string CouponsUsedCoupens = context.Request.Form["CouponsUsedCoupens"];
            string GenderType = context.Request.Form["GenderType"];
            string KycType = context.Request.Form["KycType"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string ServiceId = context.Request.Form["ServiceId"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            string CouponType = context.Request.Form["CouponType"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            Coupons w = new Coupons();
            w.Id = Convert.ToInt64(Id);
            w.ServiceId = Convert.ToInt32(ServiceId == "" ? "0" : ServiceId);

            w.GenderType = Convert.ToInt32(GenderType);

            w.KycType = Convert.ToInt32(KycType);
            w.FromDate = FromDate;
            w.ToDate = ToDate;
            w.CheckDelete = 0;
            w.CouponType = Convert.ToInt32(CouponType);
            if (ScheduleStatus != "0")
            {
                if (ScheduleStatus == "1")
                {
                    w.Running = "Running";
                }
                else if (ScheduleStatus == "2")
                {
                    w.Scheduled = "Scheduled";
                }
                else if (ScheduleStatus == "3")
                {
                    w.Expired = "Expired";
                }
            }

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);


            List<AddCoupons> objCoupon = (List<AddCoupons>)CommonEntityConverter.DataTableToList<AddCoupons>(dt);
            for (int i = 0; i < objCoupon.Count; i++)
            {
                objCoupon[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objCoupon[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");

                objCoupon[i].Sno = (i + 1).ToString();
                objCoupon[i].FromDateDT = objCoupon[i].FromDate.ToString("dd MMMM yyyy");
                objCoupon[i].ToDateDT = objCoupon[i].ToDate.ToString("dd MMMM yyyy");
                objCoupon[i].CreatedDateDt = objCoupon[i].CreatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                objCoupon[i].UpdatedDateDt = objCoupon[i].UpdatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                objCoupon[i].GenderStatusName = ((AddCoupons.GenderStatussEnum)Convert.ToInt32(objCoupon[i].GenderStatus)).ToString();
                objCoupon[i].KycStatusName = ((AddCoupons.KycStatussEnum)Convert.ToInt32(objCoupon[i].KycStatus)).ToString();
                objCoupon[i].CouponTypeName = ((AddCoupons.CouponTypeEnum)Convert.ToInt32(objCoupon[i].CouponType)).ToString();
                objCoupon[i].ApplyTypeName = ((AddCoupons.CouponReceivedBy)Convert.ToInt32(objCoupon[i].ApplyType)).ToString();
                objCoupon[i].ScheduleStatus = objCoupon[i].ScheduleStatus;

            }
            // Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddCoupons>> objDataTableResponse = new DataTableResponse<List<AddCoupons>>
            {
                draw = ajaxDraw,
                recordsFiltered = 1,
                recordsTotal = 10,
                data = objCoupon
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewCouponDataRow(AddCoupons req_ObjCoupon_req)
        {

            var context = HttpContext;
            int GenderType = Convert.ToInt32(req_ObjCoupon_req.GenderStatus);
            int KycType = Convert.ToInt32(req_ObjCoupon_req.KycStatus);
            Int32 ServiceId = req_ObjCoupon_req.ServiceId;
            AddCoupons ObjCoupon = new AddCoupons();
            ObjCoupon.ServiceId = Convert.ToInt32(ServiceId);
            ObjCoupon.CouponType = req_ObjCoupon_req.CouponType;
            ObjCoupon.KycStatus = req_ObjCoupon_req.KycStatus;
            ObjCoupon.GenderStatus = req_ObjCoupon_req.GenderStatus;
            ObjCoupon.Status = req_ObjCoupon_req.Status;
            ObjCoupon.ApplyType = req_ObjCoupon_req.ApplyType;
            ObjCoupon.IsActive = false;
            ObjCoupon.IsDeleted = false;
            ObjCoupon.FromDate = System.DateTime.UtcNow;
            ObjCoupon.ToDate = System.DateTime.UtcNow;
            Int64 i = RepCRUD<AddCoupons, GetCoupons>.Insert(ObjCoupon, "coupons");
            return Json(ObjCoupon, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public ActionResult GetProviderServiceList(string ProviderId)
        {
            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_Providerchange(ProviderId);
            return Json(districtlist);
        }
        [HttpPost]
        [Authorize]
        public JsonResult CouponUpdate(AddCoupons req_ObjCoupon_req)
        {

            AddCoupons outCoupons = new AddCoupons();
            GetCoupons inobjectCoupons = new GetCoupons();
            inobjectCoupons.Id = Convert.ToInt64(req_ObjCoupon_req.Id);
            AddCoupons resCoupons = RepCRUD<GetCoupons, AddCoupons>.GetRecord(Common.StoreProcedures.sp_Coupons_Get, inobjectCoupons, outCoupons);

            resCoupons.Title = req_ObjCoupon_req.Title;
            resCoupons.Description = req_ObjCoupon_req.Description;
            resCoupons.Amount = req_ObjCoupon_req.Amount;
            resCoupons.TransactionCount = req_ObjCoupon_req.TransactionCount;
            resCoupons.TransactionVolume = req_ObjCoupon_req.TransactionVolume;
            resCoupons.MinimumAmount = req_ObjCoupon_req.MinimumAmount;
            resCoupons.MaximumAmount = req_ObjCoupon_req.MaximumAmount;
            resCoupons.IsOneTime = req_ObjCoupon_req.IsOneTime;
            resCoupons.IsOneTimePerDay = req_ObjCoupon_req.IsOneTimePerDay;
            resCoupons.CouponPercentage = req_ObjCoupon_req.CouponPercentage;
            resCoupons.CouponsCount = req_ObjCoupon_req.CouponsCount;
            resCoupons.FromDate = Convert.ToDateTime(req_ObjCoupon_req.FromDate);
            resCoupons.IsActive = false;
            resCoupons.IsDeleted = false;
            resCoupons.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            resCoupons.CreatedByName = Session["AdminUserName"].ToString();
            resCoupons.ToDate = Convert.ToDateTime(req_ObjCoupon_req.ToDate);

            bool IsUpdated = RepCRUD<AddCoupons, GetCoupons>.Update(resCoupons, "coupons");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Coupons(CouponId:" + req_ObjCoupon_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                //Insert into CommissionUpdateHistory Table
                AddCoupons outCoupon = new AddCoupons();
                GetCoupons inobjectCoupon = new GetCoupons();
                inobjectCoupon.Id = Convert.ToInt64(req_ObjCoupon_req.Id);
                AddCoupons res = RepCRUD<GetCoupons, AddCoupons>.GetRecord(Common.StoreProcedures.sp_Coupons_Get, inobjectCoupon, outCoupon);
                if (res != null && res.Id != 0)
                {
                    AddCouponsHistory Objcouponhistory = new AddCouponsHistory();
                    Objcouponhistory.CouponId = res.Id;
                    Objcouponhistory.ServiceId = res.ServiceId;
                    Objcouponhistory.IsActive = true;
                    Objcouponhistory.FromDate = res.FromDate;
                    Objcouponhistory.ToDate = res.ToDate;
                    Objcouponhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    Objcouponhistory.CreatedByName = Session["AdminUserName"].ToString();
                    Objcouponhistory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    Objcouponhistory.UpdatedByName = Session["AdminUserName"].ToString();
                    Objcouponhistory.MinimumAmount = res.MinimumAmount;
                    Objcouponhistory.MaximumAmount = res.MaximumAmount;
                    Objcouponhistory.Status = (int)AddCouponsHistory.UpdatedStatuses.Updated;
                    Objcouponhistory.Title = res.Title;
                    Objcouponhistory.Description = res.Description;
                    Objcouponhistory.ScheduledDate = res.ScheduledDate;
                    Objcouponhistory.CouponType = res.CouponType;
                    Objcouponhistory.KycStatus = res.KycStatus;
                    Objcouponhistory.GenderStatus = res.GenderStatus;
                    Objcouponhistory.Amount = res.Amount;
                    Objcouponhistory.CouponPercentage = res.CouponPercentage;
                    Objcouponhistory.CouponsCount = res.CouponsCount;
                    Objcouponhistory.CouponsUsedCount = res.CouponsUsedCount;
                    Objcouponhistory.TransactionCount = res.TransactionCount;
                    Objcouponhistory.TransactionVolume = res.TransactionVolume;
                    Objcouponhistory.IsOneTime = res.IsOneTime;
                    Objcouponhistory.IsOneTimePerDay = res.IsOneTimePerDay;
                    Objcouponhistory.ApplyType = res.ApplyType;
                    Int64 i = RepCRUD<AddCouponsHistory, GetCouponsHistory>.Insert(Objcouponhistory, "couponshistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add CouponsHistory(CouponId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                }
            }

            return Json(resCoupons, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult DeleteCouponDataRow(string Id)
        {

            AddCoupons ObjCoupon = new AddCoupons();
            GetCoupons inobjectCoupon = new GetCoupons();
            inobjectCoupon.Id = Convert.ToInt64(Id);
            AddCoupons resCoupon = RepCRUD<GetCoupons, AddCoupons>.GetRecord(Common.StoreProcedures.sp_Coupons_Get, inobjectCoupon, ObjCoupon);
            resCoupon.Id = Convert.ToInt64(Id);
            resCoupon.IsDeleted = true;
            bool IsUpdated = RepCRUD<AddCoupons, GetCoupons>.Update(resCoupon, "coupons");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Coupens(Coupon:" + Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                //Insert into CommissionUpdateHistory Table
                AddCouponsHistory Objcouponhistory = new AddCouponsHistory();
                Objcouponhistory.CouponId = resCoupon.Id;
                Objcouponhistory.ServiceId = resCoupon.ServiceId;
                Objcouponhistory.IsActive = true;
                Objcouponhistory.FromDate = resCoupon.FromDate;
                Objcouponhistory.ToDate = resCoupon.ToDate;
                Objcouponhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                Objcouponhistory.CreatedByName = Session["AdminUserName"].ToString();
                Objcouponhistory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                Objcouponhistory.UpdatedByName = Session["AdminUserName"].ToString();
                Objcouponhistory.MinimumAmount = resCoupon.MinimumAmount;
                Objcouponhistory.MaximumAmount = resCoupon.MaximumAmount;
                Objcouponhistory.Status = (int)AddCouponsHistory.UpdatedStatuses.Deleted;
                Objcouponhistory.Title = resCoupon.Title;
                Objcouponhistory.Description = resCoupon.Description;
                Objcouponhistory.ScheduledDate = resCoupon.ScheduledDate;
                Objcouponhistory.CouponType = resCoupon.CouponType;
                Objcouponhistory.KycStatus = resCoupon.KycStatus;
                Objcouponhistory.GenderStatus = resCoupon.GenderStatus;
                Objcouponhistory.Amount = resCoupon.Amount;
                Objcouponhistory.CouponPercentage = resCoupon.CouponPercentage;
                Objcouponhistory.CouponsCount = resCoupon.CouponsCount;
                Objcouponhistory.CouponsUsedCount = resCoupon.CouponsUsedCount;
                Objcouponhistory.TransactionCount = resCoupon.TransactionCount;
                Objcouponhistory.TransactionVolume = resCoupon.TransactionVolume;
                Objcouponhistory.IsOneTime = resCoupon.IsOneTime;
                Objcouponhistory.IsOneTimePerDay = resCoupon.IsOneTimePerDay;
                Objcouponhistory.ApplyType = resCoupon.ApplyType;
                Int64 i = RepCRUD<AddCouponsHistory, GetCouponsHistory>.Insert(Objcouponhistory, "couponshistory");
                if (i > 0)
                {
                    Common.AddLogs("Successfully Add CouponsHistory(CouponId:" + resCoupon.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                }

            }
            return Json(ObjCoupon, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateCoupon(Coupon req_ObjCoupon_req, string IsActive)
        {

            AddCoupons outobject = new AddCoupons();
            GetCoupons inobject = new GetCoupons();
            inobject.Id = req_ObjCoupon_req.Id;
            AddCoupons objUpdateCoupon = RepCRUD<GetCoupons, AddCoupons>.GetRecord(Common.StoreProcedures.sp_Coupons_Get, inobject, outobject);
            objUpdateCoupon.IsActive = Convert.ToBoolean(IsActive);
            objUpdateCoupon.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCoupon.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCoupon.CreatedByName = Session["AdminUserName"].ToString();
            bool IsUpdated = RepCRUD<AddCoupons, GetCoupons>.Update(objUpdateCoupon, "coupons");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Coupon Status(CouponId:" + req_ObjCoupon_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                //Insert into CommissionUpdateHistory Table
                AddCouponsHistory Objcouponhistory = new AddCouponsHistory();
                Objcouponhistory.CouponId = objUpdateCoupon.Id;
                Objcouponhistory.ServiceId = objUpdateCoupon.ServiceId;
                Objcouponhistory.IsActive = true;
                Objcouponhistory.FromDate = objUpdateCoupon.FromDate;
                Objcouponhistory.ToDate = objUpdateCoupon.ToDate;
                Objcouponhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                Objcouponhistory.CreatedByName = Session["AdminUserName"].ToString();
                Objcouponhistory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                Objcouponhistory.UpdatedByName = Session["AdminUserName"].ToString();
                Objcouponhistory.MinimumAmount = objUpdateCoupon.MinimumAmount;
                Objcouponhistory.MaximumAmount = objUpdateCoupon.MaximumAmount;
                if (objUpdateCoupon.IsActive)
                {
                    Objcouponhistory.Status = (int)AddCouponsHistory.UpdatedStatuses.Publish;
                }
                else
                {
                    Objcouponhistory.Status = (int)AddCouponsHistory.UpdatedStatuses.UnPublish;
                }
                Objcouponhistory.Title = objUpdateCoupon.Title;
                Objcouponhistory.Description = objUpdateCoupon.Description;
                Objcouponhistory.ScheduledDate = objUpdateCoupon.ScheduledDate;
                Objcouponhistory.CouponType = objUpdateCoupon.CouponType;
                Objcouponhistory.KycStatus = objUpdateCoupon.KycStatus;
                Objcouponhistory.GenderStatus = objUpdateCoupon.GenderStatus;
                Objcouponhistory.Amount = objUpdateCoupon.Amount;
                Objcouponhistory.CouponPercentage = objUpdateCoupon.CouponPercentage;
                Objcouponhistory.CouponsCount = objUpdateCoupon.CouponsCount;
                Objcouponhistory.CouponsUsedCount = objUpdateCoupon.CouponsUsedCount;
                Objcouponhistory.TransactionCount = objUpdateCoupon.TransactionCount;
                Objcouponhistory.TransactionVolume = objUpdateCoupon.TransactionVolume;
                Objcouponhistory.IsOneTime = objUpdateCoupon.IsOneTime;
                Objcouponhistory.IsOneTimePerDay = objUpdateCoupon.IsOneTimePerDay;
                Objcouponhistory.ApplyType = objUpdateCoupon.ApplyType;
                Int64 i = RepCRUD<AddCouponsHistory, GetCouponsHistory>.Insert(Objcouponhistory, "couponshistory");
                if (i > 0)
                {
                    Common.AddLogs("Successfully Add CouponsHistory(CouponId:" + objUpdateCoupon.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                }
            }
            return Json(objUpdateCoupon, JsonRequestBehavior.AllowGet);
        }

        // GET: ScratchedCoupon
        [HttpGet]
        [Authorize]
        public ActionResult ScratchedCoupons()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetScratchedCouponLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Sno");
            columns.Add("Date");
            columns.Add("TxnId");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("ContactNo");
            columns.Add("CouponCode");
            columns.Add("Service Name");
            columns.Add("Remarks");
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
            string GenderType = context.Request.Form["GenderType"];
            string KycType = context.Request.Form["KycType"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string ServiceId = context.Request.Form["ServiceId"];
            string CouponType = context.Request.Form["CouponType"];
            string MemberId = context.Request.Form["MemberId"];
            string TxnId = context.Request.Form["TransactionId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            GetCouponsScratched w = new GetCouponsScratched();
            w.IsScratched = 1;
            w.ServiceId = Convert.ToInt32(ServiceId == "" ? "0" : ServiceId);

            //w.GenderStatus = Convert.ToInt32(GenderType);

            //w.KycStatus = Convert.ToInt32(KycType);
            w.FromDate = FromDate;
            w.ToDate = ToDate;
            w.CheckDelete = 0;
            //w.CouponType = Convert.ToInt32(CouponType);
            w.TransactionId = TxnId;
            w.MemberId = Convert.ToInt64(MemberId == "" ? "0" : MemberId);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            List<AddCouponsScratched> objCoupon = (List<AddCouponsScratched>)CommonEntityConverter.DataTableToList<AddCouponsScratched>(dt);
            for (int i = 0; i < objCoupon.Count; i++)
            {
                objCoupon[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objCoupon[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");

                objCoupon[i].Sno = (i + 1).ToString();
                //objCoupon[i].FromDateDT = objCoupon[i].FromDate.ToString("dd MMMM yyyy");
                //objCoupon[i].ToDateDT = objCoupon[i].ToDate.ToString("dd MMMM yyyy");
                objCoupon[i].CreatedDateDt = objCoupon[i].CreatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                objCoupon[i].UpdatedDateDt = objCoupon[i].UpdatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                //objCoupon[i].GenderStatusName = ((AddCoupons.GenderStatussEnum)Convert.ToInt32(objCoupon[i].GenderStatus)).ToString();
                //objCoupon[i].KycStatusName = ((AddCoupons.KycStatussEnum)Convert.ToInt32(objCoupon[i].KycStatus)).ToString();
                objCoupon[i].CouponTypeName = ((AddCoupons.CouponTypeEnum)Convert.ToInt32(objCoupon[i].CouponType)).ToString();

            }
             Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddCouponsScratched>> objDataTableResponse = new DataTableResponse<List<AddCouponsScratched>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objCoupon
            };

            return Json(objDataTableResponse);

        }

        // GET: CouponsHistory
        [HttpGet]
        [Authorize]
        public ActionResult CouponsHistory()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetCouponsHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Sno");
            columns.Add("Date");
            columns.Add("TxnId");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("ContactNo");
            columns.Add("CouponCode");
            columns.Add("Service Name");
            columns.Add("Remarks");
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
            string GenderType = context.Request.Form["GenderType"];
            string KycType = context.Request.Form["KycType"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string ServiceId = context.Request.Form["ServiceId"];
            string CouponType = context.Request.Form["CouponType"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            GetCouponsHistory w = new GetCouponsHistory();
            w.ServiceId = Convert.ToInt32(ServiceId == "" ? "0" : ServiceId);
            w.FromDate = FromDate;
            w.ToDate = ToDate;
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            List<AddCouponsHistory> objCoupon = (List<AddCouponsHistory>)CommonEntityConverter.DataTableToList<AddCouponsHistory>(dt);
            for (int i = 0; i < objCoupon.Count; i++)
            {
                objCoupon[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objCoupon[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");

                objCoupon[i].Sno = (i + 1).ToString();
                //objCoupon[i].FromDateDT = objCoupon[i].FromDate.ToString("dd MMMM yyyy");
                //objCoupon[i].ToDateDT = objCoupon[i].ToDate.ToString("dd MMMM yyyy");
                objCoupon[i].CreatedDateDt = objCoupon[i].CreatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                objCoupon[i].UpdatedDateDt = objCoupon[i].UpdatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                //objCoupon[i].GenderStatusName = ((AddCoupons.GenderStatussEnum)Convert.ToInt32(objCoupon[i].GenderStatus)).ToString();
                //objCoupon[i].KycStatusName = ((AddCoupons.KycStatussEnum)Convert.ToInt32(objCoupon[i].KycStatus)).ToString();
                objCoupon[i].StatusName = ((AddCouponsHistory.UpdatedStatuses)Convert.ToInt32(objCoupon[i].Status)).ToString();
                objCoupon[i].CouponTypeName = ((AddCoupons.CouponTypeEnum)Convert.ToInt32(objCoupon[i].CouponType)).ToString();

            }
            // Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddCouponsHistory>> objDataTableResponse = new DataTableResponse<List<AddCouponsHistory>>
            {
                draw = ajaxDraw,
                recordsFiltered = 1,
                recordsTotal = 10,
                data = objCoupon
            };

            return Json(objDataTableResponse);

        }
    }
}