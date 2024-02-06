using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class MerchantCommissionSetupController : BaseAdminSessionController
    {
        // GET: MerchantCommissionSetup
        [HttpGet]
        [Authorize]
        public ActionResult Index(string MerchantUniqueId)
        {
            AddMerchantCommission model = new AddMerchantCommission();
            ViewBag.MerchantUniqueId = string.IsNullOrEmpty(MerchantUniqueId) ? "" : MerchantUniqueId;

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
            List<SelectListItem> objProviderURLList = new List<SelectListItem>();

            if (string.IsNullOrEmpty(MerchantUniqueId))
            {
                ViewBag.SERVICEId = objProviderURLList;
                ViewBag.ProviderType = objProviderServiceCategoryList_SelectList;
                return RedirectToAction("Dashboard", "AdminLogin");

            }
            else
            {
                AddMerchant outmerchant = new AddMerchant();
                GetMerchant inmerchant = new GetMerchant();
                inmerchant.MerchantUniqueId = MerchantUniqueId;
                AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inmerchant, outmerchant);
                if (res != null && res.Id != 0)
                {
                    ViewBag.OrganizationName = res.OrganizationName;
                }


                AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
                GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
                inobject.RoleId = (int)AddUser.UserRoles.Merchant;
                List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

                // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
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

                // **********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN *********** //

                {
                    SelectListItem objDefault = new SelectListItem();
                    objDefault.Value = "0";
                    objDefault.Text = "--- Select Service ---";
                    objDefault.Selected = true;
                    objProviderURLList.Add(objDefault);
                    ViewBag.SERVICEId = objProviderURLList;
                }

            }

            List<SelectListItem> defaultList = CommonHelpers.GetSelectList_DefaultsCommission();
            ViewBag.CommissionType = defaultList;
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddMerchantCommission vmodel)
        {
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            inobject.RoleId = (int)AddUser.UserRoles.Merchant;
            List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Provider ---";
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

            // **********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN *********** //

            {
                List<SelectListItem> objProviderURLList = new List<SelectListItem>();
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Service ---";
                objDefault.Selected = true;
                objProviderURLList.Add(objDefault);
                ViewBag.SERVICEId = objProviderURLList;
            }

            List<SelectListItem> defaultList = CommonHelpers.GetSelectList_DefaultsCommission();
            ViewBag.CommissionType = defaultList;

            AddMerchantCommission model = new AddMerchantCommission();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewCommissionDataRow(AddMerchantCommission req_ObjCommission_req)
        {

            var context = HttpContext;
            AddMerchantCommission ObjCommission = new AddMerchantCommission();
            try
            {

                Int32 ServiceId = req_ObjCommission_req.ServiceId;
                if (req_ObjCommission_req.IsDefault)
                {
                    AddMerchantCommission Objcheck = new AddMerchantCommission();
                    Objcheck.MerchantUniqueId = req_ObjCommission_req.MerchantUniqueId;
                    Objcheck.CheckIsDefault = 1;
                    Objcheck.ServiceId = req_ObjCommission_req.ServiceId;
                    if (Objcheck.GetRecord())
                    {
                        return Json("Fail", JsonRequestBehavior.AllowGet);
                    }
                }

                ObjCommission.ServiceId = Convert.ToInt32(ServiceId);
                ObjCommission.IsActive = true;
                ObjCommission.IsDeleted = false;
                ObjCommission.FromDate = System.DateTime.UtcNow;
                ObjCommission.ToDate = System.DateTime.UtcNow;
                ObjCommission.MerchantUniqueId = req_ObjCommission_req.MerchantUniqueId;
                ObjCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                ObjCommission.CreatedByName = Session["AdminUserName"].ToString();
                ObjCommission.IsDefault = req_ObjCommission_req.IsDefault;
                ObjCommission.Add();

            }
            catch (Exception ex)
            {

            }
            return Json(ObjCommission, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult DeleteCommissionDataRow(string Id)
        {

            AddMerchantCommission ObjCommission = new AddMerchantCommission();
            try
            {

                ObjCommission.Id = Convert.ToInt64(Id);
                if (ObjCommission.GetRecord())
                {
                    ObjCommission.IsDeleted = true;
                    ObjCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjCommission.UpdatedByName = Session["AdminUserName"].ToString();
                    ObjCommission.UpdatedDate = DateTime.UtcNow;
                    if (ObjCommission.Update())
                    {
                        Common.AddLogs("Successfully Updated MerchantCommission(MerchantId:" + ObjCommission.MerchantUniqueId + " and CommissionId:" + Id + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, "web", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantCommission));
                        //Insert into CommissionUpdateHistory Table
                        AddMerchantCommission outobject = new AddMerchantCommission();
                        outobject.Id = Convert.ToInt64(Id);
                        if (outobject.GetRecord())
                        {
                            AddMerchantCommissionHistory ObjCommissionhistory = new AddMerchantCommissionHistory();
                            ObjCommissionhistory.MerchantCommissionId = outobject.Id;
                            ObjCommissionhistory.ServiceId = outobject.ServiceId;
                            ObjCommissionhistory.IsActive = true;
                            ObjCommissionhistory.FromDate = outobject.FromDate;
                            ObjCommissionhistory.ToDate = outobject.ToDate;
                            ObjCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            ObjCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                            ObjCommissionhistory.FixedCommission = outobject.FixedCommission;
                            ObjCommissionhistory.MinimumAmount = outobject.MinimumAmount;
                            ObjCommissionhistory.MaximumAmount = outobject.MaximumAmount;
                            ObjCommissionhistory.PercentageCommission = outobject.PercentageCommission;
                            ObjCommissionhistory.PercentageRewardPoints = outobject.PercentageRewardPoints;
                            ObjCommissionhistory.PercentageRewardPointsDebit = outobject.PercentageRewardPointsDebit;
                            ObjCommissionhistory.MinimumAllowed = outobject.MinimumAllowed;
                            ObjCommissionhistory.MaximumAllowed = outobject.MaximumAllowed;
                            ObjCommissionhistory.Status = (int)AddMerchantCommissionHistory.UpdatedStatuses.Deleted;
                            ObjCommissionhistory.Type = outobject.Type;
                            ObjCommissionhistory.ServiceCharge = outobject.ServiceCharge;
                            ObjCommissionhistory.MerchantUniqueId = outobject.MerchantUniqueId;
                            ObjCommissionhistory.FixedCommissionMerchant = outobject.FixedCommissionMerchant;
                            ObjCommissionhistory.PercentageCommissionMerchant = outobject.PercentageCommissionMerchant;
                            ObjCommissionhistory.MyPayContribution = outobject.MyPayContribution;
                            ObjCommissionhistory.MerchantContribution = outobject.MerchantContribution;
                            ObjCommissionhistory.TransactionCountLimit = outobject.TransactionCountLimit;
                            ObjCommissionhistory.Discount = outobject.Discount;
                            ObjCommissionhistory.IsDefault = outobject.IsDefault;
                            ObjCommissionhistory.FixedDiscount = outobject.FixedDiscount;
                            ObjCommissionhistory.MinimumDiscount = outobject.MinimumDiscount;
                            ObjCommissionhistory.MaximumDiscount = outobject.MaximumDiscount;
                            ObjCommissionhistory.Add();
                            if (ObjCommissionhistory.Id > 0)
                            {
                                Common.AddLogs("Successfully Add MerchantCommissionHistory(CommissionId:" + outobject.Id + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, "web", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantCommission));
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Json(ObjCommission, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetMerchantCommissionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Sno");
            columns.Add("ServiceId");
            columns.Add("ServiceName");
            columns.Add("MinimumAmount");
            columns.Add("MaximumAmount");
            //columns.Add("FixedCommission");
            columns.Add("PercentageCommission");
            columns.Add("PercentageRewardPoints");
            columns.Add("PercentageRewardPointsDebit");
            columns.Add("MinimumAllowed");
            columns.Add("MaximumAllowed");
            columns.Add("ServiceCharge");
            columns.Add("MinimumAllowedSC");
            columns.Add("MaximumAllowedSC");
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
            string MinimumAmount = context.Request.Form["MinimumAmount"];
            string MaximumAmount = context.Request.Form["MaximumAmount"];
            string FixedCommission = context.Request.Form["FixedCommission"];
            string PercentageCommission = context.Request.Form["PercentageCommission"];
            string PercentageRewardPoints = context.Request.Form["PercentageRewardPoints"];
            string PercentageRewardPointsDebit = context.Request.Form["PercentageRewardPointsDebit"];
            string MinimumAllowed = context.Request.Form["MinimumAllowed"];
            string MaximumAllowed = context.Request.Form["MaximumAllowed"];
            string ServiceCharge = context.Request.Form["ServiceCharge"];
            string MinimumAllowedSC = context.Request.Form["MinimumAllowedSC"];
            string MaximumAllowedSC = context.Request.Form["MaximumAllowedSC"];
            string MerchantUniqueId = context.Request.Form["MerchantUniqueId"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string ServiceId = context.Request.Form["ServiceId"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            string CheckDefault = context.Request.Form["CheckDefault"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            AddMerchantCommission w = new AddMerchantCommission();
            w.Id = Convert.ToInt64(Id);
            w.MerchantUniqueId = MerchantUniqueId;
            w.ServiceId = Convert.ToInt32(ServiceId);
            w.MinimumAmount = Convert.ToDecimal(MinimumAmount);
            w.MaximumAmount = Convert.ToDecimal(MaximumAmount);
            w.FixedCommission = Convert.ToDecimal(FixedCommission);
            w.PercentageCommission = Convert.ToDecimal(PercentageCommission);
            w.PercentageRewardPoints = Convert.ToDecimal(PercentageRewardPoints);
            w.PercentageRewardPointsDebit = Convert.ToDecimal(PercentageRewardPointsDebit);
            w.MinimumAllowed = Convert.ToDecimal(MinimumAllowed);
            w.MaximumAllowed = Convert.ToDecimal(MaximumAllowed);
            w.ServiceCharge = Convert.ToDecimal(ServiceCharge);
            w.MinimumAllowedSC = Convert.ToDecimal(MinimumAllowedSC);
            w.MaximumAllowedSC = Convert.ToDecimal(MaximumAllowedSC);
            w.CheckFromDate = FromDate;
            w.CheckToDate = ToDate;
            w.CheckDelete = 0;
            if (CheckDefault == "0")
            {
                w.CheckIsDefault = 0;
            }
            else if (CheckDefault == "1")
            {
                w.CheckIsDefault = 1;
            }
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


            List<AddMerchantCommission> objCommission = (List<AddMerchantCommission>)CommonEntityConverter.DataTableToList<AddMerchantCommission>(dt);
            for (int i = 0; i < objCommission.Count; i++)
            {
                objCommission[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objCommission[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");
                objCommission[i].DefaultTypeName = ((AddMerchantCommission.Default)Convert.ToInt32(objCommission[i].IsDefault)).ToString().Replace("_", " ");
                //objCommission[i].KycTypeName = ((AddCommission.KycTypes)Convert.ToInt32(objCommission[i].KycType)).ToString();
                objCommission[i].Sno = (i + 1).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantCommission>> objDataTableResponse = new DataTableResponse<List<AddMerchantCommission>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objCommission
            };

            return Json(objDataTableResponse);

        }
        [HttpPost]
        [Authorize]
        public JsonResult CommissionUpdateCall(AddMerchantCommission req_ObjCommission_req)
        {

            AddMerchantCommission outobject = new AddMerchantCommission();
            float CommissionSlabCount = outobject.CountMerchantCommissionCheck(req_ObjCommission_req.ServiceId.ToString(), req_ObjCommission_req.MerchantUniqueId, req_ObjCommission_req.MinimumAmount, req_ObjCommission_req.MaximumAmount, req_ObjCommission_req.Id);

            AddMerchantCommission resCommission = new AddMerchantCommission();
            try
            {

                resCommission.Id = Convert.ToInt64(req_ObjCommission_req.Id);
                if (resCommission.GetRecord())
                {
                    resCommission.ServiceId = req_ObjCommission_req.ServiceId;
                    resCommission.MinimumAmount = req_ObjCommission_req.MinimumAmount;
                    resCommission.MaximumAmount = req_ObjCommission_req.MaximumAmount;
                    resCommission.FixedCommission = req_ObjCommission_req.FixedCommission;
                    resCommission.PercentageCommission = req_ObjCommission_req.PercentageCommission;
                    resCommission.PercentageRewardPoints = req_ObjCommission_req.PercentageRewardPoints;
                    resCommission.PercentageRewardPointsDebit = req_ObjCommission_req.PercentageRewardPointsDebit;
                    resCommission.MinimumAllowed = req_ObjCommission_req.MinimumAllowed;
                    resCommission.MaximumAllowed = req_ObjCommission_req.MaximumAllowed;
                    resCommission.MinimumAllowedSC = req_ObjCommission_req.MinimumAllowedSC;
                    resCommission.MaximumAllowedSC = req_ObjCommission_req.MaximumAllowedSC;
                    resCommission.ServiceCharge = req_ObjCommission_req.ServiceCharge;
                    resCommission.FromDate = Convert.ToDateTime(req_ObjCommission_req.FromDate);
                    resCommission.IsActive = true;
                    resCommission.IsDeleted = false;
                    resCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    resCommission.CreatedByName = Session["AdminUserName"].ToString();
                    resCommission.ToDate = Convert.ToDateTime(req_ObjCommission_req.ToDate);
                    resCommission.UpdatedDate = System.DateTime.UtcNow;
                    resCommission.MerchantUniqueId = req_ObjCommission_req.MerchantUniqueId;
                    resCommission.FixedCommissionMerchant = req_ObjCommission_req.FixedCommissionMerchant;
                    resCommission.PercentageCommissionMerchant = req_ObjCommission_req.PercentageCommissionMerchant;
                    resCommission.MyPayContribution = Convert.ToDecimal(100 - req_ObjCommission_req.MerchantContribution);
                    resCommission.MerchantContribution = req_ObjCommission_req.MerchantContribution;
                    resCommission.TransactionCountLimit = req_ObjCommission_req.TransactionCountLimit;
                    resCommission.Discount = req_ObjCommission_req.Discount;
                    resCommission.IsDefault = req_ObjCommission_req.IsDefault;
                    resCommission.FixedDiscount = req_ObjCommission_req.FixedDiscount;
                    resCommission.MinimumDiscount = req_ObjCommission_req.MinimumDiscount;
                    resCommission.MaximumDiscount = req_ObjCommission_req.MaximumDiscount;
                    if (req_ObjCommission_req.IsDefault)
                    {
                        resCommission.MinimumAmount = 0;
                        resCommission.MaximumAmount = 0;
                        resCommission.TransactionCountLimit = 0;
                    }
                    if (CommissionSlabCount > 0 && req_ObjCommission_req.IsDefault == false)
                    {
                        AddMerchantCommission ObjCommission = new AddMerchantCommission();
                        return Json(ObjCommission, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        bool IsUpdated = resCommission.Update();
                        if (IsUpdated)
                        {
                            Common.AddLogs("Successfully Updated MerchantCommission(CommissionId:" + req_ObjCommission_req.Id + " and MerchantId:" + req_ObjCommission_req.MerchantUniqueId + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, "web", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantCommission));
                            //Insert into MerchantCommissionHistory Table
                            AddMerchantCommission res = new AddMerchantCommission();
                            res.Id = resCommission.Id;
                            if (res.GetRecord())
                            {
                                AddMerchantCommissionHistory ObjCommissionhistory = new AddMerchantCommissionHistory();
                                ObjCommissionhistory.MerchantCommissionId = res.Id;
                                ObjCommissionhistory.ServiceId = res.ServiceId;
                                ObjCommissionhistory.IsActive = res.IsActive;
                                ObjCommissionhistory.IsDeleted = res.IsDeleted;
                                ObjCommissionhistory.FromDate = res.FromDate;
                                ObjCommissionhistory.ToDate = res.ToDate;
                                ObjCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                ObjCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                                ObjCommissionhistory.FixedCommission = res.FixedCommission;
                                ObjCommissionhistory.MinimumAmount = res.MinimumAmount;
                                ObjCommissionhistory.MaximumAmount = res.MaximumAmount;
                                ObjCommissionhistory.PercentageCommission = res.PercentageCommission;
                                ObjCommissionhistory.PercentageRewardPoints = res.PercentageRewardPoints;
                                ObjCommissionhistory.PercentageRewardPointsDebit = res.PercentageRewardPointsDebit;
                                ObjCommissionhistory.MinimumAllowed = res.MinimumAllowed;
                                ObjCommissionhistory.MaximumAllowed = res.MaximumAllowed;
                                ObjCommissionhistory.MinimumAllowedSC = res.MinimumAllowedSC;
                                ObjCommissionhistory.MaximumAllowedSC = res.MaximumAllowedSC;
                                ObjCommissionhistory.Status = (int)AddMerchantCommissionHistory.UpdatedStatuses.Updated;
                                ObjCommissionhistory.Type = res.Type;
                                ObjCommissionhistory.ServiceCharge = res.ServiceCharge;
                                ObjCommissionhistory.MerchantUniqueId = res.MerchantUniqueId;
                                ObjCommissionhistory.FixedCommissionMerchant = res.FixedCommissionMerchant;
                                ObjCommissionhistory.PercentageCommissionMerchant = res.PercentageCommissionMerchant;
                                ObjCommissionhistory.MyPayContribution = res.MyPayContribution;
                                ObjCommissionhistory.MerchantContribution = res.MerchantContribution;
                                ObjCommissionhistory.TransactionCountLimit = res.TransactionCountLimit;
                                ObjCommissionhistory.Discount = res.Discount;
                                ObjCommissionhistory.IsDefault = res.IsDefault;
                                ObjCommissionhistory.FixedDiscount = res.FixedDiscount;
                                ObjCommissionhistory.MinimumDiscount = res.MinimumDiscount;
                                ObjCommissionhistory.MaximumDiscount = res.MaximumDiscount;
                                if (ObjCommissionhistory.Add())
                                {
                                    Common.AddLogs("Successfully Add MerchantCommissionHistory(CommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, "web", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantCommission));
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Json(resCommission, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateCommissionCall(AddMerchantCommission req_ObjCommission_req, string IsActive)
        {

            AddMerchantCommission objUpdateCommission = new AddMerchantCommission();
            try
            {

                objUpdateCommission.Id = req_ObjCommission_req.Id;
                if (objUpdateCommission.GetRecord())
                {
                    objUpdateCommission.IsActive = Convert.ToBoolean(IsActive);
                    objUpdateCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    objUpdateCommission.UpdatedByName = Session["AdminUserName"].ToString();
                    bool IsUpdated = objUpdateCommission.Update();
                    if (IsUpdated)
                    {
                        Common.AddLogs("Successfully Updated Merchant Commission Status(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, "web", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantCommission));
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return Json(objUpdateCommission, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult GetProviderServiceList(string ProviderId)
        {
            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_Providerchange(ProviderId);
            return Json(districtlist);
        }
        [HttpGet]
        [Authorize]
        public ActionResult CommissionUpdateHistory(string MerchantUniqueId)
        {
            AddMerchantCommissionHistory model = new AddMerchantCommissionHistory();
            ViewBag.MerchantUniqueId = string.IsNullOrEmpty(MerchantUniqueId) ? "" : MerchantUniqueId;
            if (string.IsNullOrEmpty(MerchantUniqueId))
            {
                ViewBag.SERVICEId = "";
                ViewBag.ProviderType = "";
                return View(model);
            }
            else
            {
                AddMerchant outmerchant = new AddMerchant();
                GetMerchant inmerchant = new GetMerchant();
                inmerchant.MerchantUniqueId = MerchantUniqueId;
                AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inmerchant, outmerchant);
                if (res != null && res.Id != 0)
                {
                    ViewBag.OrganizationName = res.OrganizationName;
                }
                AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
                GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
                inobject.RoleId = (int)AddUser.UserRoles.Merchant;
                List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

                List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
                // ********  ADD DEFAULT VALUE IN DROPDOWN ********* //
                {
                    SelectListItem objDefault = new SelectListItem();
                    objDefault.Value = "0";
                    objDefault.Text = "--- Select Provider ---";
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

                // ********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN ********* //

                {
                    List<SelectListItem> objProviderURLList = new List<SelectListItem>();
                    SelectListItem objDefault = new SelectListItem();
                    objDefault.Value = "0";
                    objDefault.Text = "--- Select Service ---";
                    objDefault.Selected = true;
                    objProviderURLList.Add(objDefault);
                    ViewBag.SERVICEId = objProviderURLList;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem
                {
                    Text = "Select Status",
                    Value = "0",
                    Selected = true
                });
                items.Add(new SelectListItem
                {
                    Text = "Updated",
                    Value = "1"
                });
                items.Add(new SelectListItem
                {
                    Text = "Deleted",
                    Value = "2"
                });
                ViewBag.Status = items;

                List<SelectListItem> itemstype = new List<SelectListItem>();
                itemstype.Add(new SelectListItem
                {
                    Text = "Select Type",
                    Value = "0",
                    Selected = true
                });
                itemstype.Add(new SelectListItem
                {
                    Text = "Running",
                    Value = "1"
                });
                itemstype.Add(new SelectListItem
                {
                    Text = "Scheduled",
                    Value = "2"
                });
                itemstype.Add(new SelectListItem
                {
                    Text = "Expired",
                    Value = "3"
                });
                ViewBag.Type = itemstype;
            }
            return View(model);
        }

        [Authorize]
        public JsonResult GetCommissionUpdateHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
            columns.Add("ServiceName");
            columns.Add("MinimumAmount");
            columns.Add("MaximumAmount");
            columns.Add("CashbackAmount");
            columns.Add("RewardPoint");
            columns.Add("MinimumAllowed");
            columns.Add("MaximumAllowed");
            columns.Add("MinimumAllowedSC");
            columns.Add("MaximumAllowedSC");
            columns.Add("ServiceCharge");
            columns.Add("GenderType");
            columns.Add("KycType");
            columns.Add("StartDate");
            columns.Add("EndDate");
            columns.Add("UpdatedBy");
            columns.Add("IpAddress");
            columns.Add("Status");
            columns.Add("ScheduleStatus");
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
            string MinimumAmount = context.Request.Form["MinimumAmount"];
            string MaximumAmount = context.Request.Form["MaximumAmount"];
            string MerchantUniqueId = context.Request.Form["MerchantUniqueId"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string Status = context.Request.Form["Status"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            string Id = context.Request.Form["Id"];
            string ServiceId = context.Request.Form["ServiceId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantCommissionHistory> trans = new List<AddMerchantCommissionHistory>();
            AddMerchantCommissionHistory w = new AddMerchantCommissionHistory();
            w.Id = Convert.ToInt64(Id);
            w.ServiceId = Convert.ToInt32(ServiceId);
            w.Status = Convert.ToInt32(Status);
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
            w.MinimumAmount = Convert.ToDecimal(MinimumAmount);
            w.MaximumAmount = Convert.ToDecimal(MaximumAmount);
            w.CheckFromDate = FromDate;
            w.CheckToDate = ToDate;
            w.MerchantUniqueId = MerchantUniqueId;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantCommissionHistory
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         ServiceName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["ServiceId"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),

                         MinimumAllowed = Convert.ToDecimal(row["MinimumAllowed"]),
                         MaximumAllowed = Convert.ToDecimal(row["MaximumAllowed"]),
                         PercentageCommission = Convert.ToDecimal(row["PercentageCommission"]),
                         PercentageRewardPoints = Convert.ToDecimal(row["PercentageRewardPoints"]),
                         PercentageRewardPointsDebit = Convert.ToDecimal(row["PercentageRewardPointsDebit"]),
                         MinimumAmount = Convert.ToDecimal(row["MinimumAmount"]),
                         MaximumAmount = Convert.ToDecimal(row["MaximumAmount"]),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         FromDateDT = row["FromDateDT"].ToString(),
                         ToDateDT = row["ToDateDT"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         ScheduleStatus = row["ScheduleStatus"].ToString(),
                         StatusName = @Enum.GetName(typeof(AddMerchantCommissionHistory.UpdatedStatuses), Convert.ToInt64(row["Status"])),
                         IpAddress = row["IpAddress"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         MinimumAllowedSC = Convert.ToDecimal(row["MinimumAllowedSC"]),
                         MaximumAllowedSC = Convert.ToDecimal(row["MaximumAllowedSC"]),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantCommissionHistory>> objDataTableResponse = new DataTableResponse<List<AddMerchantCommissionHistory>>
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
        public ActionResult ProviderServiceCategories()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetProviderServiceCategoriesLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("ProviderCategoryName");
            columns.Add("Commission %");
            columns.Add("Action");
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

            List<AddProviderServiceCategoryList> trans = new List<AddProviderServiceCategoryList>();

            GetProviderServiceCategoryList w = new GetProviderServiceCategoryList();
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddProviderServiceCategoryList
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         ProviderCategoryName = row["ProviderCategoryName"].ToString(),
                         Commission = row["Commission"].ToString(),
                         MPCoinsCashback = row["MPCoinsCashback"].ToString(),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddProviderServiceCategoryList>> objDataTableResponse = new DataTableResponse<List<AddProviderServiceCategoryList>>
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
        public JsonResult ProviderCommissionUpdate(AddProviderServiceCategoryList req_ObjCommission_req)
        {
            AddProviderServiceCategoryList ObjCommission = new AddProviderServiceCategoryList();
            try
            {

                //GetRecord
                AddProviderServiceCategoryList outobject = new AddProviderServiceCategoryList();
                GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
                inobject.Id = req_ObjCommission_req.Id;
                AddProviderServiceCategoryList res = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecord(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    ObjCommission.Id = req_ObjCommission_req.Id;
                    ObjCommission.Commission = req_ObjCommission_req.Commission;
                    ObjCommission.MPCoinsCashback = req_ObjCommission_req.MPCoinsCashback;
                    ObjCommission.ProviderCategoryName = res.ProviderCategoryName;
                    bool IsUpdated = RepCRUD<AddProviderServiceCategoryList, GetProviderServiceCategoryList>.Update(ObjCommission, "providerservicecategorylist");
                    if (IsUpdated)
                    {
                        Common.AddLogs("Successfully Updated ProviderServiceCategoryList(Id:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());

                    }
                }

            }
            catch (Exception ex)
            {
            }
            return Json(ObjCommission, JsonRequestBehavior.AllowGet);
        }
    }
}