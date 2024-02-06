using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.StyledXmlParser.Jsoup.Safety;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Common.Notifications;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class NotificationCampaignController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
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

            List<SelectListItem> khaltienumlist = new List<SelectListItem>();
            khaltienumlist = CommonHelpers.GetSelectList_KhaltiEnumList();
            SelectListItem objPlayStore = new SelectListItem();
            objPlayStore.Value = "-1";
            objPlayStore.Text = "Mobile App Store";
            khaltienumlist.Add(objPlayStore);
            ViewBag.NotificationRedirectType = khaltienumlist;
            objPlayStore = new SelectListItem();
            objPlayStore.Value = ((int)VendorApi_CommonHelper.KhaltiAPIName.Voting).ToString();
            objPlayStore.Text = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), (int)VendorApi_CommonHelper.KhaltiAPIName.Voting).ToString().ToUpper();
            khaltienumlist.Add(objPlayStore);
            AddNotificationCampaign model = new AddNotificationCampaign();
            List<SelectListItem> statelist = CommonHelpers.GetSelectList_State(0);
            
            ViewBag.Province = statelist;

            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_District(0,0);
                 ViewBag.District = districtlist;
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddNotificationCampaign vmodel)
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

            List<SelectListItem> khaltienumlist = new List<SelectListItem>();
            khaltienumlist = CommonHelpers.GetSelectList_KhaltiEnumList();
            ViewBag.NotificationRedirectType = khaltienumlist;

            AddNotificationCampaign model = new AddNotificationCampaign();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewNotificationCampaignDataRow(NotificationCampaign req_ObjNotificationCampaign_req)
        {
            List<SelectListItem> statelist = CommonHelpers.GetSelectList_State(0);

            int provinceid = Convert.ToInt32(req_ObjNotificationCampaign_req.Province);
            var provincename = GetProvinceDistrict(statelist, provinceid);
            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_District(0, 0);
            int districtid = Convert.ToInt32(req_ObjNotificationCampaign_req.District);

            var districtname = GetProvinceDistrict(districtlist, districtid);
            /*foreach (var item in statelist)
            {
              if (provinceid== Convert.ToInt32( item.Value))
                {
                    provincename = item.Text;
                }
            }*/

            var context = HttpContext;
            int GenderStatus = Convert.ToInt32(req_ObjNotificationCampaign_req.GenderStatus);
            int KycStatus = Convert.ToInt32(req_ObjNotificationCampaign_req.KycStatus);
            int GeographyType = Convert.ToInt32(req_ObjNotificationCampaign_req.Geography);
            int UserType = Convert.ToInt32(req_ObjNotificationCampaign_req.IsOldUserStatus);
            int DeviceType = Convert.ToInt32(req_ObjNotificationCampaign_req.DeviceTypeStatus);
            int RedirectType = Convert.ToInt32(req_ObjNotificationCampaign_req.NotificationRedirectType);
            string URL =req_ObjNotificationCampaign_req.URL;
            AddNotificationCampaign ObjNotificationCampaign = new AddNotificationCampaign();
            ObjNotificationCampaign.Title = req_ObjNotificationCampaign_req.Title;
            ObjNotificationCampaign.NotificationMessage = req_ObjNotificationCampaign_req.NotificationMessage;
            ObjNotificationCampaign.NotificationDescription = req_ObjNotificationCampaign_req.NotificationDescription;
            ObjNotificationCampaign.IsOldUserStatus = UserType;
            ObjNotificationCampaign.GenderStatus = GenderStatus;
            ObjNotificationCampaign.KycStatus = KycStatus;
            ObjNotificationCampaign.DeviceTypeStatus = DeviceType;
            ObjNotificationCampaign.NotificationRedirectType = RedirectType;
            ObjNotificationCampaign.Geography = GeographyType;
            ObjNotificationCampaign.Province = provincename;
            ObjNotificationCampaign.District = districtname;
            ObjNotificationCampaign.URL = URL;
            ObjNotificationCampaign.IsActive = true;
            ObjNotificationCampaign.IsDeleted = false;
            ObjNotificationCampaign.ScheduleDateTime = System.DateTime.UtcNow;
            Int64 i = RepCRUD<AddNotificationCampaign, GetNotificationCampaign>.Insert(ObjNotificationCampaign, "notificationcampaign");
            return Json(ObjNotificationCampaign, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult DeleteNotificationCampaignDataRow(string Id)
        {

            AddNotificationCampaign ObjNotificationCampaign = new AddNotificationCampaign();
            GetNotificationCampaign inobjectNotificationCampaign = new GetNotificationCampaign();
            inobjectNotificationCampaign.Id = Convert.ToInt64(Id);
            AddNotificationCampaign resNotificationCampaign = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobjectNotificationCampaign, ObjNotificationCampaign);
            resNotificationCampaign.IsDeleted = true;
            bool IsUpdated = RepCRUD<AddNotificationCampaign, GetNotificationCampaign>.Update(resNotificationCampaign, "notificationcampaign");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated NotificationCampaign(NotificationCampaignId:" + Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                AddNotificationCampaign outobject = new AddNotificationCampaign();
                GetNotificationCampaign inobject = new GetNotificationCampaign();
                inobject.Id = Convert.ToInt64(Id);
                AddNotificationCampaign res = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddNotificationCampaignHistory ObjNotificationCampaignhistory = new AddNotificationCampaignHistory();
                    ObjNotificationCampaignhistory.NotificationCampaignId = res.Id;
                    ObjNotificationCampaignhistory.GenderStatus = res.GenderStatus;
                    ObjNotificationCampaignhistory.KycStatus = res.KycStatus;
                    ObjNotificationCampaignhistory.IsActive = true;
                    ObjNotificationCampaignhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjNotificationCampaignhistory.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 i = RepCRUD<AddNotificationCampaignHistory, GetNotificationCampaignHistory>.Insert(ObjNotificationCampaignhistory, "NotificationCampaignHistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add NotificationCampaignHistory(NotificationCampaignId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                    }
                }
            }
            return Json(ObjNotificationCampaign, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetAdminNotificationCampaignLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Sno");
            columns.Add("CreatedDateDt");
            columns.Add("UpdatedDateDt");
            columns.Add("Title");
            columns.Add("NotificationMessage");
            columns.Add("NotificationDescription");
            columns.Add("IsOldUserStatus");
            columns.Add("UserTypeName");
            columns.Add("GenderStatusName");
            columns.Add("KycStatusName");
            columns.Add("SentStatusName");
            columns.Add("URL");
            columns.Add("Geography");
            columns.Add("Province");
            columns.Add("District");

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
            string OldUserStatus = context.Request.Form["UserType"];
            string GenderStatus = context.Request.Form["GenderStatus"];
            string KycStatus = context.Request.Form["KycStatus"];
            string Geography = context.Request.Form["Geography"];
            string DeviceTypeStatus = context.Request.Form["DeviceTypeStatus"];
            string RedirectionType = context.Request.Form["RedirectionType"];
            string URL = context.Request.Form["URL"];
            string Province = context.Request.Form["Province"];
            string District = context.Request.Form["District"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetUser inobject_user = new GetUser();
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            GetNotificationCampaign w = new GetNotificationCampaign();
            w.Id = Convert.ToInt64(Id);
            w.CheckOldUserStatus = Convert.ToInt32(OldUserStatus);
            w.CheckGenderStatus = Convert.ToInt32(GenderStatus);
            w.CheckKycStatus = Convert.ToInt32(KycStatus);
            w.Geography = Convert.ToInt32(Geography);
            w.CheckDeviceTypeStatus = Convert.ToInt32(DeviceTypeStatus);
            w.CheckRedirectType = Convert.ToInt32(RedirectionType);
            w.URL = URL;
            w.Province = Province;
            w.District = District;
            w.CheckDelete = 0;

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            List<AddNotificationCampaign> objNotificationCampaign = (List<AddNotificationCampaign>)CommonEntityConverter.DataTableToList<AddNotificationCampaign>(dt);
            for (int i = 0; i < objNotificationCampaign.Count; i++)
            {
                objNotificationCampaign[i].IsOldUserStatusName = ((AddNotificationCampaign.OldUserStatus)Convert.ToInt32(objNotificationCampaign[i].IsOldUserStatus)).ToString();
                objNotificationCampaign[i].GenderStatusName = ((AddNotificationCampaign.GenderStatuss)Convert.ToInt32(objNotificationCampaign[i].GenderStatus)).ToString();
                objNotificationCampaign[i].KycStatusName = ((AddNotificationCampaign.KycStatuss)Convert.ToInt32(objNotificationCampaign[i].KycStatus)).ToString();
                objNotificationCampaign[i].DeviceTypeStatusName = ((AddNotificationCampaign.DeviceType)Convert.ToInt32(objNotificationCampaign[i].DeviceTypeStatus)).ToString();
                objNotificationCampaign[i].SentStatusName = ((AddNotificationCampaign.SentStatuses)Convert.ToInt32(objNotificationCampaign[i].SentStatus)).ToString();

                if (objNotificationCampaign[i].NotificationRedirectType == -1)
                {
                    objNotificationCampaign[i].NotificationRedirectTypeName = "Mobile App Store";
                }
                else
                {
                    objNotificationCampaign[i].NotificationRedirectTypeName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objNotificationCampaign[i].NotificationRedirectType)).ToString().Replace("_", " ").Replace("khalti", "");
                }
                objNotificationCampaign[i].URL = Convert.ToString(objNotificationCampaign[i].URL);

                objNotificationCampaign[i].Sno = (i + 1).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddNotificationCampaign>> objDataTableResponse = new DataTableResponse<List<AddNotificationCampaign>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objNotificationCampaign
            };

            return Json(objDataTableResponse);

        }
        [HttpPost]
        [Authorize]
        public JsonResult NotificationCampaignUpdateCall(NotificationCampaign req_ObjNotificationCampaign_req)
        {
            AddNotificationCampaign outobject_update = new AddNotificationCampaign();
            GetNotificationCampaign inobject_update = new GetNotificationCampaign();
            inobject_update.Id = req_ObjNotificationCampaign_req.Id;
            AddNotificationCampaign resNotificationCampaign = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject_update, outobject_update);
            if (resNotificationCampaign.Id!=null)
            {
                resNotificationCampaign.Title = req_ObjNotificationCampaign_req.Title;
                resNotificationCampaign.NotificationMessage = req_ObjNotificationCampaign_req.NotificationMessage;
                resNotificationCampaign.NotificationDescription = req_ObjNotificationCampaign_req.NotificationDescription;
                resNotificationCampaign.ScheduleDateTime = Convert.ToDateTime(req_ObjNotificationCampaign_req.ScheduleDateTime);

                resNotificationCampaign.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                resNotificationCampaign.CreatedByName = Session["AdminUserName"].ToString();
                resNotificationCampaign.UpdatedDate = System.DateTime.UtcNow;
                resNotificationCampaign.Id = req_ObjNotificationCampaign_req.Id;
                bool IsUpdated = RepCRUD<AddNotificationCampaign, GetNotificationCampaign>.Update(resNotificationCampaign, "notificationcampaign");
                if (IsUpdated)
                {
                    Common.AddLogs("Successfully Updated Notification Campaign (NotificationCampaignId:" + req_ObjNotificationCampaign_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.User), resNotificationCampaign.CreatedBy, resNotificationCampaign.CreatedByName, false, "Web", "", (int)AddLog.LogActivityEnum.MyPay_Notification, resNotificationCampaign.CreatedBy, resNotificationCampaign.CreatedByName);
                    AddNotificationCampaignHistory ObjNotificationCampaignhistory = new AddNotificationCampaignHistory();
                    ObjNotificationCampaignhistory.NotificationCampaignId = resNotificationCampaign.Id;
                    ObjNotificationCampaignhistory.Title = resNotificationCampaign.Title;
                    ObjNotificationCampaignhistory.NotificationMessage = resNotificationCampaign.NotificationMessage;
                    ObjNotificationCampaignhistory.NotificationDescription = resNotificationCampaign.NotificationDescription;
                    ObjNotificationCampaignhistory.IsActive = resNotificationCampaign.IsActive;
                    ObjNotificationCampaignhistory.IsDeleted = resNotificationCampaign.IsDeleted;
                    ObjNotificationCampaignhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjNotificationCampaignhistory.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 i = RepCRUD<AddNotificationCampaignHistory, GetNotificationCampaignHistory>.Insert(ObjNotificationCampaignhistory, "NotificationCampaignHistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add Notification Campaign History(NotificationCampaignId:" + resNotificationCampaign.Id + ")", true, Convert.ToInt32(AddLog.LogType.User), ObjNotificationCampaignhistory.CreatedBy, ObjNotificationCampaignhistory.CreatedByName, false, "Web", "", (int)AddLog.LogActivityEnum.MyPay_Notification, ObjNotificationCampaignhistory.CreatedBy, ObjNotificationCampaignhistory.CreatedByName);
                    }
                }
            }
            return Json(resNotificationCampaign, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult BroadcastNotificationCampaignCall(NotificationCampaign req_ObjNotificationCampaign_req)
        {
            AddNotificationCampaign outobject = new AddNotificationCampaign();
            GetNotificationCampaign inobject = new GetNotificationCampaign();
            inobject.Id = req_ObjNotificationCampaign_req.Id;
            inobject.CheckDelete = 0;
            inobject.CheckSentStatus = (int)(AddNotificationCampaign.SentStatuses.Pending);
            AddNotificationCampaign objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
            if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0 && !string.IsNullOrEmpty(objUpdateNotificationCampaign.Title) && !string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationMessage))
            {
                objUpdateNotificationCampaign.SentStatus = (int)(AddNotificationCampaign.SentStatuses.Progress);
                bool isUpdate = RepCRUD<AddNotificationCampaign, GetNotificationCampaign>.Update(objUpdateNotificationCampaign, "notificationcampaign");
                Int64 CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                string CreatedByName = Session["AdminUserName"].ToString();
                if (isUpdate)
                {
                    if (objUpdateNotificationCampaign.ScheduleDateTime <= Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)))
                    {
                        Common.ExecuteBulkNotificationCampaign(req_ObjNotificationCampaign_req.Id, req_ObjNotificationCampaign_req.Province, req_ObjNotificationCampaign_req.District);
       

                    }
                    Common.AddLogs($"Bulk Notification Scheduled(ID: " + objUpdateNotificationCampaign.Id + ")", true, Convert.ToInt32(AddLog.LogType.User), CreatedBy, CreatedByName, false, "Web", "", 0, CreatedBy, CreatedByName);
                }
                else
                {
                    Common.AddLogs($"Bulk Notification Scheduled Failed(ID: " + objUpdateNotificationCampaign.Id + ")", true, Convert.ToInt32(AddLog.LogType.User), CreatedBy, CreatedByName, false, "Web", "", 0, CreatedBy, CreatedByName);
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddNotificationImage(string Id)
        {
            AddNotificationCampaign model = new AddNotificationCampaign();
            if (!string.IsNullOrEmpty(Id))
            {
                AddNotificationCampaign outobject = new AddNotificationCampaign();
                GetNotificationCampaign inobject = new GetNotificationCampaign();
                inobject.Id = Convert.ToInt64(Id);
                AddNotificationCampaign res = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    res.NotificationRedirectTypeName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(res.NotificationRedirectType)).ToString().Replace("_", " ").Replace("khalti", "");
                    ViewBag.NotificationRedirectTypeName = res.NotificationRedirectTypeName;

                    res.IsOldUserStatusName = ((AddNotificationCampaign.OldUserStatus)Convert.ToInt32(res.IsOldUserStatus)).ToString();
                    ViewBag.IsOldUserStatusName = res.IsOldUserStatusName;
                    res.GenderStatusName = ((AddNotificationCampaign.GenderStatuss)Convert.ToInt32(res.GenderStatus)).ToString();
                    ViewBag.GenderStatusName = res.GenderStatusName;
                    res.KycStatusName = ((AddNotificationCampaign.KycStatuss)Convert.ToInt32(res.KycStatus)).ToString();
                    ViewBag.KycStatusName = res.KycStatusName;
                    res.DeviceTypeStatusName = ((AddNotificationCampaign.DeviceType)Convert.ToInt32(res.DeviceTypeStatus)).ToString();
                    ViewBag.DeviceTypeStatusName = res.DeviceTypeStatusName;
                    model = res;
                }
                else
                {
                    ViewBag.NotificationRedirectType = "";
                    ViewBag.IsOldUserStatus = "";
                    ViewBag.GenderStatus = "";
                    ViewBag.KycStatus = "";
                    ViewBag.DeviceTypeStatus = "";
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
        public ActionResult AddNotificationImage(AddNotificationCampaign vmodel, HttpPostedFileBase NotificationImageFile)
        {
            AddNotificationCampaign model = new AddNotificationCampaign();

            if (NotificationImageFile != null)
            {
                var fileSize = NotificationImageFile.ContentLength / 1000;
                if (fileSize > 1054)
                {
                    ViewBag.Message = "Allowed file size exceeded. (Max. " + 1054 + " KB)";
                    return View(model);

                }
                AddNotificationCampaign outobject = new AddNotificationCampaign();
                GetNotificationCampaign inobject = new GetNotificationCampaign();
                inobject.Id = vmodel.Id;
                AddNotificationCampaign res = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (NotificationImageFile != null)
                    {
                        string subPath = "~/Images/NotificationImages/";

                        if (!System.IO.Directory.Exists(Server.MapPath(subPath)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(NotificationImageFile.FileName);
                        string filePath = Path.Combine(Server.MapPath(subPath) + fileName);

                        NotificationImageFile.SaveAs(filePath);
                        res.NotificationImage = fileName;
                    }
                    else
                    {
                        ViewBag.Message = "Please upload an image.";
                    }
                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Session["AdminUserName"].ToString();
                    res.UpdatedDate = DateTime.UtcNow;
                    bool status = RepCRUD<AddNotificationCampaign, GetNotificationCampaign>.Update(res, "notificationcampaign");
                    if (status)
                    {
                        ViewBag.SuccessMessage = "Successfully Added Notification Image.";
                        Common.AddLogs("Added Notification Image of (NotificationId:" + res.Id + "  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Not Updated.";
                    }

                }
                else
                {
                    ViewBag.Message = "Notification Id Not Found.";
                }
            }
            else
            {
                ViewBag.Message = "Please upload an image.";
            }

            AddNotificationCampaign outobjectv = new AddNotificationCampaign();
            GetNotificationCampaign inobjectv = new GetNotificationCampaign();
            inobjectv.Id = vmodel.Id;
            AddNotificationCampaign resv = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobjectv, outobjectv);
            if (resv != null && resv.Id != 0)
            {
                resv.NotificationRedirectTypeName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(resv.NotificationRedirectType)).ToString().Replace("_", " ").Replace("khalti", "");
                ViewBag.NotificationRedirectType = resv.NotificationRedirectTypeName;

                resv.IsOldUserStatusName = ((AddNotificationCampaign.OldUserStatus)Convert.ToInt32(resv.IsOldUserStatus)).ToString();
                ViewBag.IsOldUserStatus = resv.IsOldUserStatusName;
                resv.GenderStatusName = ((AddNotificationCampaign.GenderStatuss)Convert.ToInt32(resv.GenderStatus)).ToString();
                ViewBag.GenderStatus = resv.GenderStatusName;
                resv.KycStatusName = ((AddNotificationCampaign.KycStatuss)Convert.ToInt32(resv.KycStatus)).ToString();
                ViewBag.KycStatus = resv.KycStatusName;
                resv.DeviceTypeStatusName = ((AddNotificationCampaign.DeviceType)Convert.ToInt32(resv.DeviceTypeStatus)).ToString();
                ViewBag.DeviceTypeStatus = resv.DeviceTypeStatusName;
                model = resv;
            }
            else
            {
                ViewBag.NotificationRedirectType = "";
                ViewBag.IsOldUserStatus = "";
                ViewBag.GenderStatus = "";
                ViewBag.KycStatus = "";
                ViewBag.DeviceTypeStatus = "";
            }

            return View(model);
        }

        private string GetProvinceDistrict(List<SelectListItem> list, int id)
        {
            
            var name = "";
            if (id == 0)
             {
                return null;
            }

            foreach (var item in list)
            {
                if (id == Convert.ToInt32(item.Value))
                {
                    name = item.Text;
                }
            }
            return name;
        }
    }

   
}