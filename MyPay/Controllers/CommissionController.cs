using ClosedXML.Excel;
using MyPay.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using QRCoder;
using ServiceStack;
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
    public class CommissionController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            AddCommission model = new AddCommission();
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            inobject.RoleId = (int)AddUser.UserRoles.User;
            List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
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
                List<SelectListItem> objProviderURLList = new List<SelectListItem>();
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Service ---";
                objDefault.Selected = true;
                objProviderURLList.Add(objDefault);
                ViewBag.SERVICEId = objProviderURLList;
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddCommission vmodel)
        {
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            inobject.RoleId = (int)AddUser.UserRoles.User;
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

            AddCommission model = new AddCommission();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewCommissionDataRow(Commissions req_ObjCommission_req)
        {

            var context = HttpContext;
            int GenderType = Convert.ToInt32(req_ObjCommission_req.GenderType);
            int KycType = Convert.ToInt32(req_ObjCommission_req.KycType);
            Int32 ServiceId = req_ObjCommission_req.ServiceId;
            AddCommission ObjCommission = new AddCommission();
            ObjCommission.ServiceId = Convert.ToInt32(ServiceId);
            ObjCommission.GenderType = GenderType;
            ObjCommission.KycType = KycType;
            ObjCommission.IsActive = true;
            ObjCommission.IsDeleted = false;
            ObjCommission.FromDate = System.DateTime.UtcNow;
            ObjCommission.ToDate = System.DateTime.UtcNow;
            ObjCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            ObjCommission.CreatedByName = Session["AdminUserName"].ToString();
            Int64 i = RepCRUD<AddCommission, GetCommission>.Insert(ObjCommission, "commission");
            return Json(ObjCommission, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult DeleteCommissionDataRow(string Id)
        {

            AddCommission ObjCommission = new AddCommission();
            GetCommission inobjectCommission = new GetCommission();
            inobjectCommission.Id = Convert.ToInt64(Id);
            AddCommission resCommission = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobjectCommission, ObjCommission);
            resCommission.IsDeleted = true;
            bool IsUpdated = RepCRUD<AddCommission, GetCommission>.Update(resCommission, "commission");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Commission(CommissionId:" + Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                //Insert into CommissionUpdateHistory Table
                AddCommission outobject = new AddCommission();
                GetCommission inobject = new GetCommission();
                inobject.Id = Convert.ToInt64(Id);
                AddCommission res = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddCommissionUpdateHistory ObjCommissionhistory = new AddCommissionUpdateHistory();
                    ObjCommissionhistory.CommissionId = res.Id;
                    ObjCommissionhistory.ServiceId = res.ServiceId;
                    ObjCommissionhistory.GenderType = res.GenderType;
                    ObjCommissionhistory.KycType = res.KycType;
                    ObjCommissionhistory.IsActive = true;
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
                    ObjCommissionhistory.Status = (int)AddCommissionUpdateHistory.UpdatedStatuses.Deleted;
                    ObjCommissionhistory.Type = res.Type;
                    ObjCommissionhistory.ServiceCharge = res.ServiceCharge;
                    Int64 i = RepCRUD<AddCommissionUpdateHistory, GetCommissionUpdateHistory>.Insert(ObjCommissionhistory, "commissionupdatehistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add CommissionUpdateHistory(CommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                }
            }
            return Json(ObjCommission, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetAdminCommissionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Sno");
            columns.Add("ServiceId");
            columns.Add("ServiceName");
            columns.Add("MinimumAmount");
            columns.Add("MaximumAmount");
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
            string GenderType = context.Request.Form["GenderType"];
            string KycType = context.Request.Form["KycType"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string ServiceId = context.Request.Form["ServiceId"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetUser inobject_user = new GetUser();
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            Commissions w = new Commissions();
            w.Id = Convert.ToInt64(Id);
            w.ServiceId = Convert.ToInt32(ServiceId);
            w.MinimumAmount = Convert.ToDecimal(MinimumAmount);
            w.MaximumAmount = Convert.ToDecimal(MaximumAmount);
            w.FixedCommission = Convert.ToDecimal(FixedCommission);
            w.PercentageCommission = Convert.ToDecimal(PercentageCommission);
            w.PercentageRewardPoints = Convert.ToDecimal(PercentageRewardPoints);
            w.PercentageRewardPointsDebit = Convert.ToDecimal(PercentageRewardPointsDebit);
            w.GenderType = Convert.ToInt32(GenderType);
            w.MinimumAllowed = Convert.ToDecimal(MinimumAllowed);
            w.MaximumAllowed = Convert.ToDecimal(MaximumAllowed);
            w.ServiceCharge = Convert.ToDecimal(ServiceCharge);
            w.MinimumAllowedSC = Convert.ToDecimal(MinimumAllowedSC);
            w.MaximumAllowedSC = Convert.ToDecimal(MaximumAllowedSC);
            w.KycType = Convert.ToInt32(KycType);
            w.FromDate = FromDate;
            w.ToDate = ToDate;
            w.CheckDelete = 0;
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


            List<AddCommission> objCommission = (List<AddCommission>)CommonEntityConverter.DataTableToList<AddCommission>(dt);
            for (int i = 0; i < objCommission.Count; i++)
            {
                objCommission[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objCommission[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");
                objCommission[i].GenderTypeName = ((AddCommission.GenderTypes)Convert.ToInt32(objCommission[i].GenderType)).ToString();
                objCommission[i].KycTypeName = ((AddCommission.KycTypes)Convert.ToInt32(objCommission[i].KycType)).ToString();
                objCommission[i].Sno = (i + 1).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddCommission>> objDataTableResponse = new DataTableResponse<List<AddCommission>>
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
        public JsonResult CommissionUpdateCall(Commissions req_ObjCommission_req)
        {

            AddCommission outobject = new AddCommission();
            float CommissionSlabCount = outobject.CountCommissionCheck(req_ObjCommission_req.ServiceId.ToString(), req_ObjCommission_req.GenderType, req_ObjCommission_req.KycType, req_ObjCommission_req.MinimumAmount, req_ObjCommission_req.MaximumAmount, req_ObjCommission_req.Id);
            if (CommissionSlabCount > 0)
            {
                AddCommission ObjCommission = new AddCommission();
                return Json(ObjCommission, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AddCommission outCommission = new AddCommission();
                GetCommission inobjectCommission = new GetCommission();
                inobjectCommission.Id = Convert.ToInt64(req_ObjCommission_req.Id);
                AddCommission resCommission = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobjectCommission, outCommission);


                resCommission.ServiceId = req_ObjCommission_req.ServiceId;
                resCommission.GenderType = req_ObjCommission_req.GenderType;
                resCommission.KycType = req_ObjCommission_req.KycType;
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
                bool IsUpdated = RepCRUD<AddCommission, GetCommission>.Update(resCommission, "commission");
                if (IsUpdated)
                {
                    Common.AddLogs("Successfully Updated Commission(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    //Insert into CommissionUpdateHistory Table
                    AddCommission outobject_update = new AddCommission();
                    GetCommission inobject_update = new GetCommission();
                    inobject_update.Id = resCommission.Id;
                    AddCommission res = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobject_update, outobject_update);
                    if (res != null && res.Id != 0)
                    {
                        AddCommissionUpdateHistory ObjCommissionhistory = new AddCommissionUpdateHistory();
                        ObjCommissionhistory.CommissionId = res.Id;
                        ObjCommissionhistory.ServiceId = res.ServiceId;
                        ObjCommissionhistory.GenderType = res.GenderType;
                        ObjCommissionhistory.KycType = res.KycType;
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
                        ObjCommissionhistory.Status = (int)AddCommissionUpdateHistory.UpdatedStatuses.Updated;
                        ObjCommissionhistory.Type = res.Type;
                        ObjCommissionhistory.ServiceCharge = res.ServiceCharge;
                        ObjCommissionhistory.IsApprovedByAdmin = true;
                        ObjCommissionhistory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        ObjCommissionhistory.UpdatedByName = Session["AdminUserName"].ToString();
                        Int64 i = RepCRUD<AddCommissionUpdateHistory, GetCommissionUpdateHistory>.Insert(ObjCommissionhistory, "commissionupdatehistory");
                        if (i > 0)
                        {
                            Common.AddLogs("Successfully Add CommissionUpdateHistory(CommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                        }
                    }
                }
                return Json(resCommission, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateCommissionCall(Commissions req_ObjCommission_req, string IsActive)
        {

            AddCommission outobject = new AddCommission();
            GetCommission inobject = new GetCommission();
            inobject.Id = req_ObjCommission_req.Id;
            AddCommission objUpdateCommission = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobject, outobject);
            objUpdateCommission.IsActive = Convert.ToBoolean(IsActive);
            objUpdateCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCommission.CreatedByName = Session["AdminUserName"].ToString();
            bool IsUpdated = RepCRUD<AddCommission, GetCommission>.Update(objUpdateCommission, "commission");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Commission Status(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
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
        public ActionResult CommissionUpdateHistory()
        {
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            inobject.RoleId = (int)AddUser.UserRoles.User;
            List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
            // *********  ADD DEFAULT VALUE IN DROPDOWN ********** //
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
            ViewBag.LoginUserId = Convert.ToInt64(Session["AdminMemberId"]);
            AddCommissionUpdateHistory model = new AddCommissionUpdateHistory();
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
            string GenderType = context.Request.Form["GenderType"];
            string KycType = context.Request.Form["KycType"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string Status = context.Request.Form["Status"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            string Id = context.Request.Form["Id"];
            string ServiceId = context.Request.Form["ServiceId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddCommissionUpdateHistory> trans = new List<AddCommissionUpdateHistory>();
            GetCommissionUpdateHistory w = new GetCommissionUpdateHistory();
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
            w.GenderType = Convert.ToInt32(GenderType);
            w.KycType = Convert.ToInt32(KycType);
            w.FromDate = FromDate;
            w.ToDate = ToDate;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddCommissionUpdateHistory
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         ServiceName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["ServiceId"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),

                         GenderTypeName = @Enum.GetName(typeof(AddCommission.GenderTypes), Convert.ToInt64(row["GenderType"])),
                         KycTypeName = @Enum.GetName(typeof(AddCommission.KycTypes), Convert.ToInt64(row["KycType"])),
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
                         CreatedBy = Convert.ToInt64(row["CreatedBy"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         ScheduleStatus = row["ScheduleStatus"].ToString(),
                         StatusName = @Enum.GetName(typeof(AddCommissionUpdateHistory.UpdatedStatuses), Convert.ToInt64(row["Status"])),
                         IpAddress = row["IpAddress"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         MinimumAllowedSC = Convert.ToDecimal(row["MinimumAllowedSC"]),
                         MaximumAllowedSC = Convert.ToDecimal(row["MaximumAllowedSC"]),
                         IsApprovedByAdmin = Convert.ToBoolean(row["IsApprovedByAdmin"]),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         UpdatedBy = Convert.ToInt64(row["UpdatedBy"]),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddCommissionUpdateHistory>> objDataTableResponse = new DataTableResponse<List<AddCommissionUpdateHistory>>
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
                ObjCommission.IsActive = res.IsActive;
                ObjCommission.IsDelete = res.IsDelete;
                ObjCommission.RoleId = res.RoleId;
                bool IsUpdated = RepCRUD<AddProviderServiceCategoryList, GetProviderServiceCategoryList>.Update(ObjCommission, "providerservicecategorylist");
                if (IsUpdated)
                {
                    Common.AddLogs("Successfully Updated ProviderServiceCategoryList(Id:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());

                }
            }
            return Json(ObjCommission, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CommissionMakerSetUp()
        {
            AddCommission model = new AddCommission();
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            inobject.RoleId = (int)AddUser.UserRoles.User;
            List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
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
                List<SelectListItem> objProviderURLList = new List<SelectListItem>();
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Service ---";
                objDefault.Selected = true;
                objProviderURLList.Add(objDefault);
                ViewBag.SERVICEId = objProviderURLList;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CommissionUpdateCallMaker(Commissions req_ObjCommission_req)
        {

            AddCommission outobject = new AddCommission();
            float CommissionSlabCount = outobject.CountCommissionCheck(req_ObjCommission_req.ServiceId.ToString(), req_ObjCommission_req.GenderType, req_ObjCommission_req.KycType, req_ObjCommission_req.MinimumAmount, req_ObjCommission_req.MaximumAmount, req_ObjCommission_req.Id);
            if (CommissionSlabCount > 0)
            {
                AddCommissionUpdateHistory ObjCommissionhistory = new AddCommissionUpdateHistory();
                return Json(ObjCommissionhistory, JsonRequestBehavior.AllowGet);
            }
            else
            {

                AddCommissionUpdateHistory ObjCommissionhistory = new AddCommissionUpdateHistory();
                ObjCommissionhistory.CommissionId = req_ObjCommission_req.Id;
                ObjCommissionhistory.ServiceId = req_ObjCommission_req.ServiceId;
                ObjCommissionhistory.GenderType = req_ObjCommission_req.GenderType;
                ObjCommissionhistory.KycType = req_ObjCommission_req.KycType;
                ObjCommissionhistory.IsActive = true;
                ObjCommissionhistory.IsDeleted = false;
                ObjCommissionhistory.FromDate = Convert.ToDateTime(req_ObjCommission_req.FromDate);
                ObjCommissionhistory.ToDate = Convert.ToDateTime(req_ObjCommission_req.ToDate);
                ObjCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                ObjCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                ObjCommissionhistory.FixedCommission = req_ObjCommission_req.FixedCommission;
                ObjCommissionhistory.MinimumAmount = req_ObjCommission_req.MinimumAmount;
                ObjCommissionhistory.MaximumAmount = req_ObjCommission_req.MaximumAmount;
                ObjCommissionhistory.PercentageCommission = req_ObjCommission_req.PercentageCommission;
                ObjCommissionhistory.PercentageRewardPoints = req_ObjCommission_req.PercentageRewardPoints;
                ObjCommissionhistory.PercentageRewardPointsDebit = req_ObjCommission_req.PercentageRewardPointsDebit;
                ObjCommissionhistory.MinimumAllowed = req_ObjCommission_req.MinimumAllowed;
                ObjCommissionhistory.MaximumAllowed = req_ObjCommission_req.MaximumAllowed;
                ObjCommissionhistory.MinimumAllowedSC = req_ObjCommission_req.MinimumAllowedSC;
                ObjCommissionhistory.MaximumAllowedSC = req_ObjCommission_req.MaximumAllowedSC;
                ObjCommissionhistory.Status = (int)AddCommissionUpdateHistory.UpdatedStatuses.Updated;
                ObjCommissionhistory.Type = req_ObjCommission_req.Type;
                ObjCommissionhistory.ServiceCharge = req_ObjCommission_req.ServiceCharge;
                Int64 i = RepCRUD<AddCommissionUpdateHistory, GetCommissionUpdateHistory>.Insert(ObjCommissionhistory, "commissionupdatehistory");
                if (i > 0)
                {
                    Common.AddLogs("Successfully Add CommissionUpdateHistory maker(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                }

                return Json(ObjCommissionhistory, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [Authorize]
        public JsonResult ApproveCommissionMaker(int id)
        {
            AddCommissionUpdateHistory outCommissionHistory = new AddCommissionUpdateHistory();
            GetCommissionUpdateHistory inobjectCommissionHistory = new GetCommissionUpdateHistory();
            inobjectCommissionHistory.Id = id;
            AddCommissionUpdateHistory resCommissionHistory = RepCRUD<GetCommissionUpdateHistory, AddCommissionUpdateHistory>.GetRecord(Common.StoreProcedures.sp_CommissionUpdateHistory_Get, inobjectCommissionHistory, outCommissionHistory);
            resCommissionHistory.IsApprovedByAdmin = true;
            resCommissionHistory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            resCommissionHistory.UpdatedByName = Session["AdminUserName"].ToString();
            bool IsUpdated = RepCRUD<AddCommissionUpdateHistory, GetCommissionUpdateHistory>.Update(resCommissionHistory, "commissionupdatehistory");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Approved CommissionUpdateHistory maker(Id:" + id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());

                AddCommission outCommission = new AddCommission();
                GetCommission inobjectCommission = new GetCommission();
                inobjectCommission.Id = Convert.ToInt64(resCommissionHistory.CommissionId);
                AddCommission resCommission = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobjectCommission, outCommission);

                resCommission.MinimumAmount = resCommissionHistory.MinimumAmount;
                resCommission.MaximumAmount = resCommissionHistory.MaximumAmount;
                resCommission.FixedCommission = resCommissionHistory.FixedCommission;
                resCommission.PercentageCommission = resCommissionHistory.PercentageCommission;
                resCommission.PercentageRewardPoints = resCommissionHistory.PercentageRewardPoints;
                resCommission.PercentageRewardPointsDebit = resCommissionHistory.PercentageRewardPointsDebit;
                resCommission.MinimumAllowed = resCommissionHistory.MinimumAllowed;
                resCommission.MaximumAllowed = resCommissionHistory.MaximumAllowed;
                resCommission.MinimumAllowedSC = resCommissionHistory.MinimumAllowedSC;
                resCommission.MaximumAllowedSC = resCommissionHistory.MaximumAllowedSC;
                resCommission.ServiceCharge = resCommissionHistory.ServiceCharge;
                resCommission.FromDate = Convert.ToDateTime(resCommissionHistory.FromDate);
                resCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                resCommission.ToDate = Convert.ToDateTime(resCommissionHistory.ToDate);
                resCommission.UpdatedDate = System.DateTime.UtcNow;
                resCommission.IsDeleted = resCommissionHistory.Status == 2 ? true : false;
                resCommission.IsActive = resCommissionHistory.IsActive;
                bool IsUpdatedCommission = RepCRUD<AddCommission, GetCommission>.Update(resCommission, "commission");
                if (IsUpdatedCommission)
                {
                    Common.AddLogs("Successfully Approve Commission maker(CommissionId:" + resCommissionHistory.CommissionId + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                }

            }

            return Json(resCommissionHistory, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult DeleteCommissionDataRowMaker(string Id)
        {
            AddCommission outobject = new AddCommission();
            GetCommission inobject = new GetCommission();
            inobject.Id = Convert.ToInt64(Id);
            AddCommission res = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                AddCommissionUpdateHistory ObjCommissionhistory = new AddCommissionUpdateHistory();
                ObjCommissionhistory.CommissionId = res.Id;
                ObjCommissionhistory.ServiceId = res.ServiceId;
                ObjCommissionhistory.GenderType = res.GenderType;
                ObjCommissionhistory.KycType = res.KycType;
                ObjCommissionhistory.IsActive = true;
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
                ObjCommissionhistory.Status = (int)AddCommissionUpdateHistory.UpdatedStatuses.Deleted;
                ObjCommissionhistory.Type = res.Type;
                ObjCommissionhistory.ServiceCharge = res.ServiceCharge;
                Int64 i = RepCRUD<AddCommissionUpdateHistory, GetCommissionUpdateHistory>.Insert(ObjCommissionhistory, "commissionupdatehistory");
                if (i > 0)
                {
                    Common.AddLogs("Successfully Add  CommissionUpdateHistoryMaker Delete(CommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                }

            }
            return Json(outobject, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult UpdateStatusCommissionDataRowMaker(Commissions req_ObjCommission_req, string IsActive)
        {
            AddCommission outobject = new AddCommission();
            GetCommission inobject = new GetCommission();
            inobject.Id = req_ObjCommission_req.Id;
            AddCommission res = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                AddCommissionUpdateHistory ObjCommissionhistory = new AddCommissionUpdateHistory();
                ObjCommissionhistory.CommissionId = res.Id;
                ObjCommissionhistory.ServiceId = res.ServiceId;
                ObjCommissionhistory.GenderType = res.GenderType;
                ObjCommissionhistory.KycType = res.KycType;
                ObjCommissionhistory.IsActive = Convert.ToBoolean(IsActive);
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
                ObjCommissionhistory.Status = (int)AddCommissionUpdateHistory.UpdatedStatuses.Updated;
                ObjCommissionhistory.Type = res.Type;
                ObjCommissionhistory.ServiceCharge = res.ServiceCharge;
                Int64 i = RepCRUD<AddCommissionUpdateHistory, GetCommissionUpdateHistory>.Insert(ObjCommissionhistory, "commissionupdatehistory");
                if (i > 0)
                {
                    Common.AddLogs("Successfully Add  CommissionUpdateHistoryMaker Status update(CommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                }

            }
            return Json(outobject, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public ActionResult AirlineCommission()
        {
            AddAirlineCommission model = new AddAirlineCommission();
            AddAirlinesList outobj = new AddAirlinesList();
            GetAirlinesList inobject = new GetAirlinesList();
            inobject.RoleId = (int)AddUser.UserRoles.User;
            List<AddAirlinesList> objAirlineList = RepCRUD<GetAirlinesList, AddAirlinesList>.GetRecordList(Common.StoreProcedures.sp_AirlineList_Get, inobject, outobj);

            AddSectorList outSectorobj = new AddSectorList();
            GetSectorList inSectorobject = new GetSectorList();
            List<AddSectorList> objSectorList = RepCRUD<GetSectorList, AddSectorList>.GetRecordList(Common.StoreProcedures.sp_SectorList_Get, inSectorobject, outSectorobj);

            List<SelectListItem> objAirlineList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Airline ---";
                objDefault.Selected = true;
                objAirlineList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objAirlineList.Count; i++)
            {

                SelectListItem objItem = new SelectListItem();
                objItem.Value = objAirlineList[i].Id.ToString();
                objItem.Text = objAirlineList[i].AirlineName.ToString();
                objAirlineList_SelectList.Add(objItem);
            }
            ViewBag.AirlineType = objAirlineList_SelectList;

            List<SelectListItem> objSectorList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select From ---";
                objDefault.Selected = true;
                objSectorList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objSectorList.Count; i++)
            {

                SelectListItem objItem = new SelectListItem();
                objItem.Value = objSectorList[i].Id.ToString();
                objItem.Text = objSectorList[i].SectorName.ToString();
                objSectorList_SelectList.Add(objItem);

            }
            ViewBag.Sector = objSectorList_SelectList;

            return View(model);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetAirlineCommissionLists(string AirlineId)
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Sno");
            columns.Add("AirlineName");
            columns.Add("FromSector");
            columns.Add("ToSector");
            columns.Add("ClassName");
            columns.Add("Cashback_Percentage");
            columns.Add("MPCoinsDebit");
            columns.Add("MPCoinsCredit");
            columns.Add("ServiceCharge");
            columns.Add("IsCashbackPerTicket");
            columns.Add("MinServiceCharge");
            columns.Add("MaxServiceCharge");
            columns.Add("GenderTypeName");
            columns.Add("KycType");
            columns.Add("KycTypeName");
            columns.Add("FromDate");
            columns.Add("ToDate");
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
            string Id = context.Request.Form["Id"];
            string FromSectorId = context.Request.Form["FromSectorId"];
            string ToSectorId = context.Request.Form["ToSectorId"];
            string AirlineName = context.Request.Form["AirlineName"];
            string ClassName = context.Request.Form["ClassName"];
            string AirlineClassId = context.Request.Form["AirlineClassId"];
            string Cashback_Percentage = context.Request.Form["Cashback_Percentage"];
            string MPCoinsCredit = context.Request.Form["MPCoinsCredit"];
            string MPCoinsDebit = context.Request.Form["MPCoinsDebit"];
            string ServiceCharge = context.Request.Form["ServiceCharge"];
            string MinimumCashbackAllowed = context.Request.Form["MinimumCashbackAllowed"];
            string MaximumCashbackAllowed = context.Request.Form["MaximumCashbackAllowed"];
            string MinServiceCharge = context.Request.Form["MinServiceCharge"];
            string MaxServiceCharge = context.Request.Form["MaxServiveCharge"];
            string GenderTypeName = context.Request.Form["GenderTypeName"];
            string KycType = context.Request.Form["KycType"];
            string KycTypeName = context.Request.Form["KycTypeName"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetUser inobject_user = new GetUser();
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            AirlineCommissions w = new AirlineCommissions();
            w.Id = Convert.ToInt64(Id);
            w.FromSectorId = Convert.ToInt32(FromSectorId);
            w.ToSectorId = Convert.ToInt32(ToSectorId);
            w.AirlineName = AirlineName;
            w.ClassName = ClassName;
            w.AirlineClassId = Convert.ToInt32(AirlineClassId);
            w.Cashback_Percentage = Convert.ToDecimal(Cashback_Percentage);
            w.MPCoinsCredit = Convert.ToDecimal(MPCoinsCredit);
            w.MPCoinsDebit = Convert.ToDecimal(MPCoinsDebit);
            w.ServiceCharge = Convert.ToDecimal(ServiceCharge);
            w.MinimumCashbackAllowed = Convert.ToDecimal(MinimumCashbackAllowed);
            w.MaximumCashbackAllowed = Convert.ToDecimal(MaximumCashbackAllowed);
            w.MinServiceCharge = Convert.ToDecimal(MinServiceCharge);
            w.MaxServiceCharge = Convert.ToDecimal(MaxServiceCharge);
            w.KycType = Convert.ToInt32(KycType);
            w.FromDate = Convert.ToDateTime(FromDate);
            w.ToDate = Convert.ToDateTime(ToDate);
            w.CheckDelete = 0;
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
            List<AddAirlineCommission> objCommission = (List<AddAirlineCommission>)CommonEntityConverter.DataTableToList<AddAirlineCommission>(dt);
            List<AddAirlineCommission> objCommission2 = new List<AddAirlineCommission>();
            if (Convert.ToInt32(AirlineId) > 0)
            {
                objCommission2 = objCommission.Where(x => x.AirlineId == Convert.ToInt32(AirlineId)).ToList();
            }
            else
            {
                objCommission2 = objCommission;
            }
            for (int i = 0; i < objCommission2.Count; i++)
            {
                objCommission2[i].GenderTypeName = ((AddCommission.GenderTypes)Convert.ToInt32(objCommission[i].GenderType)).ToString();
                objCommission2[i].KycTypeName = ((AddCommission.KycTypes)Convert.ToInt32(objCommission[i].KycType)).ToString();
                objCommission2[i].Sno = (i + 1).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddAirlineCommission>> objDataTableResponse = new DataTableResponse<List<AddAirlineCommission>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objCommission2
            };

            return Json(objDataTableResponse);

        }
        [HttpPost]
        [Authorize]
        public JsonResult AddAirlineCommissionDataRow(AirlineCommissions req_ObjCommission_req)
        {
            var context = HttpContext;
            int GenderType = Convert.ToInt32(req_ObjCommission_req.GenderType);
            int KycType = Convert.ToInt32(req_ObjCommission_req.KycType);
            AddAirlineCommission outobject = new AddAirlineCommission();
            List<(string, string)> CommissionSlabCount = outobject.CountAirlineClass(req_ObjCommission_req.AirlineId);
            AddAirlineCommission ObjCommission = new AddAirlineCommission();
            for (int i = 0; i < CommissionSlabCount.Count; i++)
            {
                ObjCommission.GenderType = GenderType;
                ObjCommission.KycType = KycType;
                ObjCommission.IsActive = true;
                ObjCommission.IsDeleted = false;
                ObjCommission.AirlineId = req_ObjCommission_req.AirlineId;
                ObjCommission.FromSectorId = req_ObjCommission_req.FromSectorId;
                ObjCommission.ToSectorId = req_ObjCommission_req.ToSectorId;
                ObjCommission.ClassName = CommissionSlabCount[i].Item1;
                ObjCommission.AirlineClassId = Convert.ToInt32(CommissionSlabCount[i].Item2);
                ObjCommission.IsActive = true;
                ObjCommission.IsDeleted = false;
                ObjCommission.FromDate = System.DateTime.UtcNow;
                ObjCommission.ToDate = System.DateTime.UtcNow;
                ObjCommission.CreatedDate = req_ObjCommission_req.CreatedDate;
                ObjCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                ObjCommission.CreatedByName = Session["AdminUserName"].ToString();
                CommonDBResonse result = ObjCommission.AddAirline();
            }

            return Json(ObjCommission, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult AirlineCommissionUpdateCall(AirlineCommissions req_ObjCommission_req)
        {

            AddAirlineCommission outobject = new AddAirlineCommission();
            float CommissionSlabCount = outobject.CountCommissionCheck(req_ObjCommission_req.Id);
            if (CommissionSlabCount > 0)
            {
                AddAirlineCommission ObjCommission = new AddAirlineCommission();
                return Json(ObjCommission, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AddAirlineCommission outCommission = new AddAirlineCommission();
                GetAirlineCommission inobjectCommission = new GetAirlineCommission();
                inobjectCommission.Id = Convert.ToInt64(req_ObjCommission_req.Id);
                AddAirlineCommission resCommission = RepCRUD<GetAirlineCommission, AddAirlineCommission>.GetRecord(Common.StoreProcedures.sp_AddAirlineCommision, inobjectCommission, outCommission);

                resCommission.Id = req_ObjCommission_req.Id;
                resCommission.AirlineId = req_ObjCommission_req.AirlineId;
                resCommission.FromSectorId = req_ObjCommission_req.FromSectorId;
                resCommission.ToSectorId = req_ObjCommission_req.ToSectorId;
                resCommission.AirlineClassId = req_ObjCommission_req.AirlineClassId;
                resCommission.Cashback_Percentage = req_ObjCommission_req.Cashback_Percentage;
                resCommission.MPCoinsCredit = req_ObjCommission_req.MPCoinsCredit;
                resCommission.MPCoinsDebit = req_ObjCommission_req.MPCoinsDebit;
                resCommission.ServiceCharge = req_ObjCommission_req.ServiceCharge;
                resCommission.IsCashbackPerTicket = req_ObjCommission_req.IsCashbackPerTicket;
                resCommission.MinServiceCharge = req_ObjCommission_req.MinServiceCharge;
                resCommission.MaxServiceCharge = req_ObjCommission_req.MaxServiceCharge;
                resCommission.IsCashbackPerTicket = req_ObjCommission_req.IsCashbackPerTicket;
                resCommission.MinimumCashbackAllowed = req_ObjCommission_req.MinimumCashbackAllowed;
                resCommission.MaximumCashbackAllowed = req_ObjCommission_req.MaximumCashbackAllowed;
                resCommission.FromDate = req_ObjCommission_req.FromDate;
                resCommission.GenderType = req_ObjCommission_req.GenderType;
                resCommission.KycType = req_ObjCommission_req.KycType;
                resCommission.IsActive = true;
                resCommission.IsDeleted = false;
                resCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                resCommission.CreatedByName = Session["AdminUserName"].ToString();
                resCommission.CreatedDate = Convert.ToDateTime(req_ObjCommission_req.CreatedDate);
                resCommission.UpdatedDate = System.DateTime.UtcNow;
                //bool IsUpdated = RepCRUD<AddAirlineCommission, GetAirlineCommission>.Update(resCommission, "commission");
                CommonDBResonse result = req_ObjCommission_req.UpdateAirlineCommission();
                if (result.code == "0")
                {
                    Common.AddLogs("Successfully Updated Airline Commission(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    //Insert into CommissionUpdateHistory Table
                    AddAirlineCommission outobject_update = new AddAirlineCommission();
                    GetAirlineCommission inobject_update = new GetAirlineCommission();
                    inobject_update.Id = resCommission.Id;
                    AddAirlineCommission res = RepCRUD<GetAirlineCommission, AddAirlineCommission>.GetRecord(Common.StoreProcedures.sp_AirlinesCommission, inobject_update, outobject_update);
                    if (res != null && res.Id != 0)
                    {
                        AddAirlineCommission ObjCommissionhistory = new AddAirlineCommission();
                        //ObjCommissionhistory.CommissionId = res.Id;
                        ObjCommissionhistory.Id = res.Id;
                        ObjCommissionhistory.GenderType = res.GenderType;
                        ObjCommissionhistory.KycType = res.KycType;
                        ObjCommissionhistory.IsActive = res.IsActive;
                        ObjCommissionhistory.IsDeleted = res.IsDeleted;
                        ObjCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        ObjCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                        ObjCommissionhistory.FixedCommission = res.FixedCommission;
                        ObjCommissionhistory.Cashback_Percentage = res.Cashback_Percentage;
                        ObjCommissionhistory.MPCoinsCredit = res.MPCoinsCredit;
                        ObjCommissionhistory.MPCoinsDebit = res.MPCoinsDebit;
                        ObjCommissionhistory.ServiceCharge = res.ServiceCharge;
                        ObjCommissionhistory.IsCashbackPerTicket = res.IsCashbackPerTicket;
                        ObjCommissionhistory.MinServiceCharge = res.MinServiceCharge;
                        ObjCommissionhistory.MaxServiceCharge = res.MaxServiceCharge;
                        ObjCommissionhistory.MinimumCashbackAllowed = res.MinimumCashbackAllowed;
                        ObjCommissionhistory.MaximumCashbackAllowed = res.MaximumCashbackAllowed;
                        ObjCommissionhistory.Status = (int)AddCommissionUpdateHistory.UpdatedStatuses.Updated;
                        ObjCommissionhistory.Type = res.Type;
                        ObjCommissionhistory.IsApprovedByAdmin = true;
                        ObjCommissionhistory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        ObjCommissionhistory.UpdatedByName = Session["AdminUserName"].ToString();
                        Int64 i = RepCRUD<AddAirlineCommission, GetAirlineCommission>.Insert(ObjCommissionhistory, "commissionupdatehistory");
                        if (i > 0)
                        {
                            Common.AddLogs("Successfully Add AirlineCommission(CommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                        }
                    }
                }
                return Json(resCommission, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult AirlineStatusUpdateCommissionCall(AirlineCommissions req_ObjCommission_req, string IsActive)
        {
            AddAirlineCommission outobject = new AddAirlineCommission();
            GetAirlineCommission inobject = new GetAirlineCommission();
            inobject.Id = req_ObjCommission_req.Id;
            AddAirlineCommission objUpdateCommission = RepCRUD<GetAirlineCommission, AddAirlineCommission>.GetRecord(Common.StoreProcedures.sp_AddAirlineCommision, inobject, outobject);
            objUpdateCommission.Id = req_ObjCommission_req.Id;
            objUpdateCommission.IsActive = Convert.ToBoolean(IsActive);
            objUpdateCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCommission.CreatedByName = Session["AdminUserName"].ToString();
            CommonDBResonse result = req_ObjCommission_req.UpdateStatusAirlineCommission();
            //bool IsUpdated = RepCRUD<AddAirlineCommission, GetAirlineCommission>.Update(objUpdateCommission, "commission");
            if (result.code == "0")
            {
                Common.AddLogs("Successfully Updated Commission Status(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
            }
            return Json(objUpdateCommission, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult DeleteAirlineCommission(AirlineCommissions req_ObjCommission_req)
        {
            AddAirlineCommission outobject = new AddAirlineCommission();
            GetAirlineCommission inobject = new GetAirlineCommission();
            inobject.Id = req_ObjCommission_req.Id;
            AddAirlineCommission objUpdateCommission = RepCRUD<GetAirlineCommission, AddAirlineCommission>.GetRecord(Common.StoreProcedures.sp_AddAirlineCommision, inobject, outobject);
            req_ObjCommission_req.IsDeleted = true;
            objUpdateCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateCommission.CreatedByName = Session["AdminUserName"].ToString();
            CommonDBResonse result = req_ObjCommission_req.DeleteAirlineCommission();
            //bool IsUpdated = RepCRUD<AddAirlineCommission, GetAirlineCommission>.Update(objUpdateCommission, "commission");
            if (result.code == "0")
            {
                Common.AddLogs("Successfully Deleted Airline Commission (CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
            }
            return Json(objUpdateCommission, JsonRequestBehavior.AllowGet);
        }
    }
}