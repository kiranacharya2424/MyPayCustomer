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
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class DealsAndOfferController : BaseAdminSessionController
    {
        private static readonly Random random = new Random();
        // GET: DealsAndOffer
        [Authorize]
        public ActionResult Index()
        {
            AddDealsandOffers model = new AddDealsandOffers();
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
        public JsonResult GetAdminDealsAndOffersLists()
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
            //string CouponValue = context.Request.Form["CouponValue"];
            //string CouponQuantity = context.Request.Form["CouponQuantity"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            DealsAndOffers w = new DealsAndOffers();
            w.Id = Convert.ToInt64(Id);
            w.ServiceId = Convert.ToInt32(ServiceId == "" ? "0" : ServiceId);
            w.GenderType = Convert.ToInt32(GenderType);
            w.KycType = Convert.ToInt32(KycType);
            w.FromDate = FromDate;
            w.ToDate = ToDate;
            w.CheckDelete = 0;
            //w.CouponType = Convert.ToInt32(CouponType);
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


            List<AddDealsandOffers> objCoupon = (List<AddDealsandOffers>)CommonEntityConverter.DataTableToList<AddDealsandOffers>(dt);
            for (int i = 0; i < objCoupon.Count; i++)
            {
                objCoupon[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objCoupon[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");

                objCoupon[i].Sno = (i + 1).ToString();
                objCoupon[i].FromDateDT = objCoupon[i].FromDate.ToString("dd MMMM yyyy");
                objCoupon[i].ToDateDT = objCoupon[i].ToDate.ToString("dd MMMM yyyy");
                objCoupon[i].CreatedDateDt = objCoupon[i].CreatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                objCoupon[i].UpdatedDateDt = objCoupon[i].UpdatedDate.ToString("dd-MM-yyyy h: mm:ss tt");
                objCoupon[i].GenderStatusName = ((AddDealsandOffers.GenderStatussEnum)Convert.ToInt32(objCoupon[i].GenderStatus)).ToString();
                objCoupon[i].KycStatusName = ((AddDealsandOffers.KycStatussEnum)Convert.ToInt32(objCoupon[i].KycStatus)).ToString();
                //objCoupon[i].CouponTypeName = ((AddDealsandOffers.CouponTypeEnum)Convert.ToInt32(objCoupon[i].CouponType)).ToString();
                //objCoupon[i].ApplyTypeName = ((AddDealsandOffers.CouponReceivedBy)Convert.ToInt32(objCoupon[i].ApplyType)).ToString();
                objCoupon[i].ScheduleStatus = objCoupon[i].ScheduleStatus;

            }
            // Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddDealsandOffers>> objDataTableResponse = new DataTableResponse<List<AddDealsandOffers>>
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
        public JsonResult AddNewDealandOffersDataRow(AddDealsandOffers req_ObjCoupon_req)
        {

            var context = HttpContext;
            int GenderType = Convert.ToInt32(req_ObjCoupon_req.GenderStatus);
            int KycType = Convert.ToInt32(req_ObjCoupon_req.KycStatus);
            Int32 ServiceId = req_ObjCoupon_req.ServiceId;
            AddDealsandOffers ObjCoupon = new AddDealsandOffers();
            ObjCoupon.ServiceId = Convert.ToInt32(ServiceId);
            //ObjCoupon.CouponType = req_ObjCoupon_req.CouponType;
            ObjCoupon.KycStatus = req_ObjCoupon_req.KycStatus;
            ObjCoupon.GenderStatus = req_ObjCoupon_req.GenderStatus;
            ObjCoupon.Status = req_ObjCoupon_req.Status;
            //ObjCoupon.ApplyType = req_ObjCoupon_req.ApplyType;
            ObjCoupon.IsActive = true;
            ObjCoupon.IsDeleted = false;
            ObjCoupon.FromDate = System.DateTime.UtcNow;
            ObjCoupon.ToDate = System.DateTime.UtcNow;
            ObjCoupon.PromoCode= GenerateRandomCode(6);
            Int64 i = RepCRUD<AddDealsandOffers, GetDealsandOffers>.Insert(ObjCoupon, "dealsandoffers");
            if (i>0)
            {
                Common.AddLogs("Successfully Added a row for Deals and Offers(Id:" + req_ObjCoupon_req.Id + ") by Admin(Id:"+Session["AdminMemberId"].ToString()+")", true, Convert.ToInt32(AddLog.LogType.DealsandOffers));
            }
            return Json(ObjCoupon, JsonRequestBehavior.AllowGet);

        }

        public static string GenerateRandomCode(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(characters.Length);
                sb.Append(characters[randomIndex]);
            }

            return sb.ToString();
        }
        [Authorize]
        public ActionResult GetProviderServiceList(string ProviderId)
        {
            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_Providerchange(ProviderId);
            return Json(districtlist);
        }

        [HttpPost]
        [Authorize]
        public JsonResult DealandOffersUpdate(AddDealsandOffers req_ObjCoupon_req)
        {

            AddDealsandOffers outCoupons = new AddDealsandOffers();
            GetDealsandOffers inobjectCoupons = new GetDealsandOffers();
            inobjectCoupons.Id = Convert.ToInt64(req_ObjCoupon_req.Id);
            AddDealsandOffers resCoupons = RepCRUD<GetDealsandOffers, AddDealsandOffers>.GetRecord(Common.StoreProcedures.sp_DealsAndOffers_Get, inobjectCoupons, outCoupons);

            resCoupons.Title = req_ObjCoupon_req.Title;
            resCoupons.Description = req_ObjCoupon_req.Description;
            resCoupons.Amount = req_ObjCoupon_req.Amount;
            //resCoupons.TransactionCount = req_ObjCoupon_req.TransactionCount;
            //resCoupons.TransactionVolume = req_ObjCoupon_req.TransactionVolume;
            resCoupons.MinimumAmount = req_ObjCoupon_req.MinimumAmount;
            resCoupons.MaximumAmount = req_ObjCoupon_req.MaximumAmount;
            resCoupons.IsOneTime = req_ObjCoupon_req.IsOneTime;
            resCoupons.IsOneTimePerDay = req_ObjCoupon_req.IsOneTimePerDay;
            resCoupons.CouponPercentage = req_ObjCoupon_req.CouponPercentage;
            //resCoupons.CouponsCount = req_ObjCoupon_req.CouponsCount;
            resCoupons.FromDate = Convert.ToDateTime(req_ObjCoupon_req.FromDate);
            resCoupons.ServiceId = req_ObjCoupon_req.ServiceId;
            resCoupons.IsActive = true;
            resCoupons.IsDeleted = false;
            resCoupons.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            resCoupons.CreatedByName = Session["AdminUserName"].ToString();
            resCoupons.ToDate = Convert.ToDateTime(req_ObjCoupon_req.ToDate);
            resCoupons.PromoCode = req_ObjCoupon_req.PromoCode;
            resCoupons.CouponQuantity= req_ObjCoupon_req.CouponQuantity;
            resCoupons.CouponValue = req_ObjCoupon_req.CouponValue;
            resCoupons.Id = req_ObjCoupon_req.Id;
            bool IsUpdated = RepCRUD<AddDealsandOffers, GetDealsandOffers>.Update(resCoupons, "dealsandoffers");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Deals and Offers(Id:" + req_ObjCoupon_req.Id + ")by Admin(Id:" + Session["AdminMemberId"].ToString() + ")", true, Convert.ToInt32(AddLog.LogType.DealsandOffers));
            }
            return Json(resCoupons, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult DeleteDealsandOffersDataRow(string Id)
        {

            AddDealsandOffers ObjCoupon = new AddDealsandOffers();
            GetDealsandOffers inobjectCoupon = new GetDealsandOffers();
            inobjectCoupon.Id = Convert.ToInt64(Id);
            AddDealsandOffers resCoupon = RepCRUD<GetDealsandOffers, AddDealsandOffers>.GetRecord(Common.StoreProcedures.sp_DealsAndOffers_Get, inobjectCoupon, ObjCoupon);
            resCoupon.Id = Convert.ToInt64(Id);
            resCoupon.IsDeleted = true;
            bool IsUpdated = RepCRUD<AddDealsandOffers, GetDealsandOffers>.Update(resCoupon, "dealsandoffers");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Deals and Offers(Id:" + Id + ")by Admin(Id:" + Session["AdminMemberId"].ToString() + ")", true, Convert.ToInt32(AddLog.LogType.DealsandOffers));
                //Insert into CommissionUpdateHistory Table


            }
            return Json(ObjCoupon, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateDealsandOffers(DealsAndOffer req_ObjCoupon_req, string IsActive)
        {

            AddDealsandOffers outobject = new AddDealsandOffers();
            GetDealsandOffers inobject = new GetDealsandOffers();
            inobject.Id = req_ObjCoupon_req.Id;
            AddDealsandOffers objUpdateCoupon = RepCRUD<GetDealsandOffers, AddDealsandOffers>.GetRecord(Common.StoreProcedures.sp_DealsAndOffers_Get, inobject, outobject);
            objUpdateCoupon.IsActive = Convert.ToBoolean(IsActive);
            objUpdateCoupon.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCoupon.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCoupon.CreatedByName = Session["AdminUserName"].ToString();
            bool IsUpdated = RepCRUD<AddDealsandOffers, GetDealsandOffers>.Update(objUpdateCoupon, "dealsandoffers");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Deals and Offers Status(Id:" + req_ObjCoupon_req.Id + ") by Admin(Id:" + Session["AdminMemberId"].ToString() + ")", true, Convert.ToInt32(AddLog.LogType.DealsandOffers));
            }
            return Json(objUpdateCoupon, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddDealsandOffersImage(string Id)
        {
            AddDealsandOffers model = new AddDealsandOffers();
            if (!string.IsNullOrEmpty(Id))
            {
                AddDealsandOffers outobject = new AddDealsandOffers();
                GetDealsandOffers inobject = new GetDealsandOffers();
                inobject.Id = Convert.ToInt64(Id);
                AddDealsandOffers res = RepCRUD<GetDealsandOffers, AddDealsandOffers>.GetRecord(Common.StoreProcedures.sp_DealsAndOffers_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    res.ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(res.ServiceId)).ToString().Replace("_", " ").Replace("khalti", "");
                    ViewBag.Service = res.ServiceName;

                    res.GenderStatusName = ((AddDealsandOffers.GenderStatussEnum)Convert.ToInt32(res.GenderStatus)).ToString();
                    ViewBag.GenderStatus = res.GenderStatusName;
                    res.KycStatusName = ((AddDealsandOffers.KycStatussEnum)Convert.ToInt32(res.KycStatus)).ToString();
                    ViewBag.KycStatus = res.KycStatusName;
                   model = res;
                }
                else
                {
                    ViewBag.Service = "";
                    ViewBag.GenderStatus = "";
                    ViewBag.KycStatus = "";
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddDealsandOffersImage(AddDealsandOffers vmodel, HttpPostedFileBase ImageFile)
        {
            AddDealsandOffers model = new AddDealsandOffers();

            if (ImageFile != null)
            {
                var fileSize = ImageFile.ContentLength / 1000;
                if (fileSize > 1054)
                {
                    ViewBag.Message = "Allowed file size exceeded. (Max. " + 1054 + " KB)";
                    return View(model);

                }
                AddDealsandOffers outobject = new AddDealsandOffers();
                GetDealsandOffers inobject = new GetDealsandOffers();
                inobject.Id = vmodel.Id;
                AddDealsandOffers res = RepCRUD<GetDealsandOffers, AddDealsandOffers>.GetRecord(Common.StoreProcedures.sp_DealsAndOffers_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (ImageFile != null)
                    {
                        string subPath = "~/Images/DealsandOffers/";

                        if (!System.IO.Directory.Exists(Server.MapPath(subPath)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(ImageFile.FileName);
                        string filePath = Path.Combine(Server.MapPath(subPath) + fileName);

                        ImageFile.SaveAs(filePath);
                        res.Image = fileName;
                    }
                    else
                    {
                        ViewBag.Message = "Please upload an image.";
                    }
                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Session["AdminUserName"].ToString();
                    res.UpdatedDate = DateTime.UtcNow;
                    bool status = RepCRUD<AddDealsandOffers, GetDealsandOffers>.Update(res, "dealsandoffers");
                    if (status)
                    {
                        ViewBag.SuccessMessage = "Successfully Added Deals and Offers Image.";
                        Common.AddLogs("Added Deals and Offers Image of (Id:" + res.Id + "  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.DealsandOffers);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Not Updated.";
                    }

                }
                else
                {
                    ViewBag.Message = "Deals and Offers Id Not Found.";
                }
            }
            else
            {
                ViewBag.Message = "Please upload an image.";
            }

            AddDealsandOffers outobjectv = new AddDealsandOffers();
            GetDealsandOffers inobjectv = new GetDealsandOffers();
            inobjectv.Id = vmodel.Id;
            AddDealsandOffers resv = RepCRUD<GetDealsandOffers, AddDealsandOffers>.GetRecord(Common.StoreProcedures.sp_DealsAndOffers_Get, inobjectv, outobjectv);
            if (resv != null && resv.Id != 0)
            {
                resv.ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(resv.ServiceId)).ToString().Replace("_", " ").Replace("khalti", "");
                ViewBag.Service = resv.ServiceName;

                resv.GenderStatusName = ((AddDealsandOffers.GenderStatussEnum)Convert.ToInt32(resv.GenderStatus)).ToString();
                ViewBag.GenderStatus = resv.GenderStatusName;
                resv.KycStatusName = ((AddDealsandOffers.KycStatussEnum)Convert.ToInt32(resv.KycStatus)).ToString();
                ViewBag.KycStatus = resv.KycStatusName;
                model = resv;
            }
            else
            {
                ViewBag.Service = "";
                ViewBag.GenderStatus = "";
                ViewBag.KycStatus = "";
            }

            return View(model);
        }
    }
}