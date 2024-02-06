using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class NotificationCampaignExcelImportController : BaseAdminSessionController
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
            AddNotificationCampaignExcel model = new AddNotificationCampaignExcel();
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddNotificationCampaignExcel vmodel)
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

            AddNotificationCampaignExcel model = new AddNotificationCampaignExcel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewNotificationCampaignDataRow(NotificationCampaignExcel req_ObjNotificationCampaign_req)
        {

            var context = HttpContext;
            int DeviceType = Convert.ToInt32(req_ObjNotificationCampaign_req.DeviceTypeStatus);
            int RedirectType = Convert.ToInt32(req_ObjNotificationCampaign_req.NotificationRedirectType);
            AddNotificationCampaignExcel ObjNotificationCampaign = new AddNotificationCampaignExcel();
            ObjNotificationCampaign.Title = req_ObjNotificationCampaign_req.Title;
            ObjNotificationCampaign.NotificationMessage = req_ObjNotificationCampaign_req.NotificationMessage;
            ObjNotificationCampaign.NotificationDescription = req_ObjNotificationCampaign_req.NotificationDescription;
            ObjNotificationCampaign.NotificationRedirectType = RedirectType;
            ObjNotificationCampaign.IsActive = true;
            ObjNotificationCampaign.IsDeleted = false;
            ObjNotificationCampaign.ScheduleDateTime = System.DateTime.UtcNow;
            Int64 i = RepCRUD<AddNotificationCampaignExcel, GetNotificationCampaignExcel>.Insert(ObjNotificationCampaign, "notificationcampaignexcel");
            return Json(ObjNotificationCampaign, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult DeleteNotificationCampaignDataRow(string Id)
        {

            AddNotificationCampaignExcel ObjNotificationCampaign = new AddNotificationCampaignExcel();
            GetNotificationCampaignExcel inobjectNotificationCampaign = new GetNotificationCampaignExcel();
            inobjectNotificationCampaign.Id = Convert.ToInt64(Id);
            AddNotificationCampaignExcel resNotificationCampaign = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobjectNotificationCampaign, ObjNotificationCampaign);
            resNotificationCampaign.IsDeleted = true;
            bool IsUpdated = RepCRUD<AddNotificationCampaignExcel, GetNotificationCampaignExcel>.Update(resNotificationCampaign, "notificationcampaignexcel");
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated NotificationCampaignExcel(NotificationCampaignExcelId:" + Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
                GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
                inobject.Id = Convert.ToInt64(Id);
                AddNotificationCampaignExcel res = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddNotificationCampaignExcelHistory ObjNotificationCampaignhistory = new AddNotificationCampaignExcelHistory();
                    ObjNotificationCampaignhistory.NotificationCampaignExcelId = res.Id;
                    ObjNotificationCampaignhistory.IsActive = true;
                    ObjNotificationCampaignhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjNotificationCampaignhistory.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 i = RepCRUD<AddNotificationCampaignExcelHistory, GetNotificationCampaignExcelHistory>.Insert(ObjNotificationCampaignhistory, "notificationcampaignexcelhistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add NotificationCampaignExcelHistory(NotificationCampaignExcelId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
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
            string DeviceTypeStatus = context.Request.Form["DeviceTypeStatus"];
            string RedirectionType = context.Request.Form["RedirectionType"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            GetNotificationCampaignExcel w = new GetNotificationCampaignExcel();
            w.Id = Convert.ToInt64(Id);
            w.CheckRedirectType = Convert.ToInt32(RedirectionType);
            w.CheckDelete = 0;

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            List<AddNotificationCampaignExcel> objNotificationCampaign = (List<AddNotificationCampaignExcel>)CommonEntityConverter.DataTableToList<AddNotificationCampaignExcel>(dt);
            for (int i = 0; i < objNotificationCampaign.Count; i++)
            {
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
                objNotificationCampaign[i].Sno = (i + 1).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddNotificationCampaignExcel>> objDataTableResponse = new DataTableResponse<List<AddNotificationCampaignExcel>>
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
        public JsonResult NotificationCampaignUpdateCall(NotificationCampaignExcel req_ObjNotificationCampaign_req)
        {
            AddNotificationCampaignExcel outobject_update = new AddNotificationCampaignExcel();
            GetNotificationCampaignExcel inobject_update = new GetNotificationCampaignExcel();
            inobject_update.Id = req_ObjNotificationCampaign_req.Id;
            AddNotificationCampaignExcel resNotificationCampaign = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject_update, outobject_update);
            if (resNotificationCampaign != null && resNotificationCampaign.Id != 0 && resNotificationCampaign.SentStatus == (int)AddNotificationCampaignExcel.SentStatuses.Pending)
            {
                resNotificationCampaign.Title = req_ObjNotificationCampaign_req.Title;
                resNotificationCampaign.NotificationMessage = req_ObjNotificationCampaign_req.NotificationMessage;
                resNotificationCampaign.NotificationDescription = req_ObjNotificationCampaign_req.NotificationDescription;
                resNotificationCampaign.ScheduleDateTime = Convert.ToDateTime(req_ObjNotificationCampaign_req.ScheduleDateTime);

                resNotificationCampaign.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                resNotificationCampaign.CreatedByName = Session["AdminUserName"].ToString();
                resNotificationCampaign.UpdatedDate = System.DateTime.UtcNow;
                bool IsUpdated = RepCRUD<AddNotificationCampaignExcel, GetNotificationCampaignExcel>.Update(resNotificationCampaign, "notificationcampaignexcel");
                if (IsUpdated)
                {
                    Common.AddLogs("Successfully Updated Notification Campaign Excel (NotificationCampaignExcelId:" + req_ObjNotificationCampaign_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.User), resNotificationCampaign.CreatedBy, resNotificationCampaign.CreatedByName, false, "Web", "", (int)AddLog.LogActivityEnum.MyPay_Notification, resNotificationCampaign.CreatedBy, resNotificationCampaign.CreatedByName);
                    AddNotificationCampaignExcelHistory ObjNotificationCampaignhistory = new AddNotificationCampaignExcelHistory();
                    ObjNotificationCampaignhistory.NotificationCampaignExcelId = resNotificationCampaign.Id;
                    ObjNotificationCampaignhistory.Title = resNotificationCampaign.Title;
                    ObjNotificationCampaignhistory.NotificationMessage = resNotificationCampaign.NotificationMessage;
                    ObjNotificationCampaignhistory.NotificationDescription = resNotificationCampaign.NotificationDescription;
                    ObjNotificationCampaignhistory.IsActive = resNotificationCampaign.IsActive;
                    ObjNotificationCampaignhistory.IsDeleted = resNotificationCampaign.IsDeleted;
                    ObjNotificationCampaignhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjNotificationCampaignhistory.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 i = RepCRUD<AddNotificationCampaignExcelHistory, GetNotificationCampaignExcelHistory>.Insert(ObjNotificationCampaignhistory, "notificationcampaignexcelhistory");
                    if (i > 0)
                    {
                        Common.AddLogs("Successfully Add Notification Campaign Excel History(NotificationCampaignExcelId:" + resNotificationCampaign.Id + ")", true, Convert.ToInt32(AddLog.LogType.User), ObjNotificationCampaignhistory.CreatedBy, ObjNotificationCampaignhistory.CreatedByName, false, "Web", "", (int)AddLog.LogActivityEnum.MyPay_Notification, ObjNotificationCampaignhistory.CreatedBy, ObjNotificationCampaignhistory.CreatedByName);
                    }
                }
            }
            return Json(resNotificationCampaign, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize]
        public JsonResult BroadcastNotificationCampaignCall(NotificationCampaignExcel req_ObjNotificationCampaign_req)
        {
            AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
            GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
            inobject.Id = req_ObjNotificationCampaign_req.Id;
            inobject.CheckDelete = 0;
            inobject.CheckSentStatus = (int)(AddNotificationCampaignExcel.SentStatuses.Pending);
            AddNotificationCampaignExcel objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
            if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0 && !string.IsNullOrEmpty(objUpdateNotificationCampaign.Title) && !string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationMessage))
            {
                objUpdateNotificationCampaign.SentStatus = (int)(AddNotificationCampaignExcel.SentStatuses.Progress);
                bool isUpdate = RepCRUD<AddNotificationCampaignExcel, GetNotificationCampaignExcel>.Update(objUpdateNotificationCampaign, "notificationcampaignexcel");
                Int64 CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                string CreatedByName = Session["AdminUserName"].ToString();
                if (isUpdate)
                {
                    if (objUpdateNotificationCampaign.ScheduleDateTime <= Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)))
                    {
                        Common.ExecuteBulkNotificationCampaignExcel(req_ObjNotificationCampaign_req.Id);
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
            AddNotificationCampaignExcel model = new AddNotificationCampaignExcel();
            if (!string.IsNullOrEmpty(Id))
            {
                AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
                GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
                inobject.Id = Convert.ToInt64(Id);
                AddNotificationCampaignExcel res = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    res.NotificationRedirectTypeName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(res.NotificationRedirectType)).ToString().Replace("_", " ").Replace("khalti", "");
                    ViewBag.NotificationRedirectTypeName = res.NotificationRedirectTypeName;

                    res.DeviceTypeStatusName = ((AddNotificationCampaign.DeviceType)Convert.ToInt32(res.DeviceTypeStatus)).ToString();
                    ViewBag.DeviceTypeStatusName = res.DeviceTypeStatusName;
                    model = res;
                }
                else
                {
                    ViewBag.NotificationRedirectType = "";
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
        public ActionResult AddNotificationImage(AddNotificationCampaignExcel vmodel, HttpPostedFileBase NotificationImageFile)
        {
            AddNotificationCampaignExcel model = new AddNotificationCampaignExcel();

            if (NotificationImageFile != null)
            {
                var fileSize = NotificationImageFile.ContentLength / 1000;
                if (fileSize > 1054)
                {
                    ViewBag.Message = "Allowed file size exceeded. (Max. " + 1054 + " KB)";
                    return View(model);

                }
                AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
                GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
                inobject.Id = vmodel.Id;
                AddNotificationCampaignExcel res = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
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
                    bool status = RepCRUD<AddNotificationCampaignExcel, GetNotificationCampaignExcel>.Update(res, "notificationcampaignexcel");
                    if (status)
                    {
                        ViewBag.SuccessMessage = "Successfully Added Notification Excel Image.";
                        Common.AddLogs("Added Notification Excel Image of (NotificationId:" + res.Id + "  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.ExcelNotification);
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

            AddNotificationCampaignExcel outobjectv = new AddNotificationCampaignExcel();
            GetNotificationCampaignExcel inobjectv = new GetNotificationCampaignExcel();
            inobjectv.Id = vmodel.Id;
            AddNotificationCampaignExcel resv = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobjectv, outobjectv);
            if (resv != null && resv.Id != 0)
            {
                resv.NotificationRedirectTypeName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(resv.NotificationRedirectType)).ToString().Replace("_", " ").Replace("khalti", "");
                ViewBag.NotificationRedirectType = resv.NotificationRedirectTypeName;

                resv.DeviceTypeStatusName = ((AddNotificationCampaign.DeviceType)Convert.ToInt32(resv.DeviceTypeStatus)).ToString();
                ViewBag.DeviceTypeStatus = resv.DeviceTypeStatusName;
                model = resv;
            }
            else
            {
                ViewBag.NotificationRedirectType = "";
                ViewBag.DeviceTypeStatus = "";
            }

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public ActionResult AddNotificationExcelFile(string Id)
        {
            AddNotificationCampaignExcel model = new AddNotificationCampaignExcel();
            if (!string.IsNullOrEmpty(Id))
            {
                ViewBag.NotificationId = Id; 
                AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
                GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
                inobject.Id = Convert.ToInt64(Id);
                AddNotificationCampaignExcel res = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    res.NotificationRedirectTypeName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(res.NotificationRedirectType)).ToString().Replace("_", " ").Replace("khalti", "");
                    ViewBag.NotificationRedirectTypeName = res.NotificationRedirectTypeName;

                    res.DeviceTypeStatusName = ((AddNotificationCampaign.DeviceType)Convert.ToInt32(res.DeviceTypeStatus)).ToString();
                    ViewBag.DeviceTypeStatusName = res.DeviceTypeStatusName;
                    model = res;
                }
                else
                {
                    ViewBag.NotificationRedirectType = "";
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
        public ActionResult AddNotificationExcelFile(AddNotificationCampaignExcel vmodel, HttpPostedFileBase NotificationExcelFile)
        {
            AddNotificationCampaignExcel model = new AddNotificationCampaignExcel();

            if (NotificationExcelFile == null)
            {
                ViewBag.Message = "You did not specify a file to upload.";
            }
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                var excelfilename = NotificationExcelFile.FileName.Split('.');
                var fileName = excelfilename[0] + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xlsx";

                //Save the file to server temp folder
                string FullPath = Path.Combine(Server.MapPath("~/Content/NotificationExcelImport/"), fileName);
                NotificationExcelFile.SaveAs(FullPath);

                string ExcelURL = string.Empty;
                if (Common.ApplicationEnvironment.IsProduction)
                {
                    ExcelURL = Common.LiveSiteUrl + "/Content/NotificationExcelImport/" + fileName;
                }
                else
                {
                    ExcelURL = Common.TestSiteUrl + "/Content/NotificationExcelImport/" + fileName;
                }
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var excel = new ExcelPackage(NotificationExcelFile.InputStream))
                {
                    var tbl = new DataTable();
                    //var ws= excel.Workbook.Worksheets[1];
                    //ExcelWorksheet ws = excel.Workbook.Worksheets[0];
                    var ws = excel.Workbook.Worksheets.First();

                    var rowscount = ws.Dimension.End.Row;
                    if (rowscount <= 1000)
                    {
                        var hasHeader = true;  // adjust accordingly

                        // add DataColumns to DataTable
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text
                                : String.Format("Column {0}", firstRowCell.Start.Column));

                        tbl.Columns.Add("ReturnMessage", typeof(string));
                        List<AddNotificationCampaignExcelData> resNotificationlist = new List<AddNotificationCampaignExcelData>();

                        // add DataRows to DataTable
                        int startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            DataRow row = tbl.NewRow();

                            foreach (var cell in wsRow)
                            {
                                if (tbl.Columns.Count > (cell.Start.Column - 1))
                                {
                                    row[cell.Start.Column - 1] = cell.Text;
                                }
                            }
                            if (row[1].ToString().Trim() != string.Empty)
                            {
                                if (string.IsNullOrEmpty(ViewBag.Message))
                                {
                                    //NotificationType = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Excel_Notification);
                                    if (row[1].ToString() != "")
                                    {

                                        AddNotificationCampaignExcelData res_notification = new AddNotificationCampaignExcelData();
                                        AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                                        GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                                        inuser.ContactNumber = row[1].ToString();
                                        AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                                        if (resuser.Id > 0)
                                        {
                                            res_notification.NotificationCampaignExcelId = vmodel.Id;
                                            res_notification.MemberId = resuser.MemberId;
                                            res_notification.MemberName = resuser.FirstName + " " + resuser.LastName;
                                            res_notification.ContactNumber = row[1].ToString();
                                            res_notification.SentStatus = 0;
                                            res_notification.CreatedDate = DateTime.UtcNow;
                                            res_notification.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                            res_notification.CreatedByName = Session["AdminUserName"].ToString();
                                            res_notification.IsApprovedByAdmin = true;
                                            res_notification.IsActive = true;
                                            row[2] = "Success";
                                        }
                                        else
                                        {
                                            row[2] = "Error: Contact Number does not exist";
                                        }
                                        tbl.Rows.Add(row);
                                        resNotificationlist.Add(res_notification);
                                    }
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(ViewBag.Message))
                        {
                            if (resNotificationlist.Count > 0)
                            {
                               // MyPay.Models.Common.Notifications.SentNotifications.ExecuteBulkExcelNotificationFromAdmin(Title, Message, NotificationType, DeviceCodeCSV);
                                Int64 Id = RepCRUD<AddNotificationCampaignExcelData, GetNotificationCampaignExcelData>.InsertList(resNotificationlist, "notificationcampaignexceldata");
                                if (Id > 0)
                                {
                                    Common.AddLogs("Excel Import done by " + Session["AdminUserName"].ToString() + " at " + fileName + ". Excel sheet file : " + ExcelURL, true, Convert.ToInt32(AddLog.LogType.ExcelNotification), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), true, "web", "", (int)AddLog.LogActivityEnum.MyPay_Notification);
                                }
                                else
                                {
                                    Common.AddLogs("Excel not Import Successfully", true, Convert.ToInt32(AddLog.LogType.ExcelNotification), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), true, "web", "", (int)AddLog.LogActivityEnum.MyPay_Notification);
                                }
                            }
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                string filename = DateTime.Now.ToShortDateString().Replace("/", "");
                                var wss = wb.Worksheets.Add(tbl, "Report(" + DateTime.Now.ToShortDateString().Replace("/", "") + ")");
                                wss.Columns().AdjustToContents();  // Adjust column width
                                wss.Rows().AdjustToContents();

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToShortDateString().Replace("/", "") + ".xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Excel file does not contains more than 1000 records. Please upload another file with max. 1000 records.";
                    }
                }
            }

            AddNotificationCampaignExcel outobjectv = new AddNotificationCampaignExcel();
            GetNotificationCampaignExcel inobjectv = new GetNotificationCampaignExcel();
            inobjectv.Id = vmodel.Id;
            AddNotificationCampaignExcel resv = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobjectv, outobjectv);
            if (resv != null && resv.Id != 0)
            {
                resv.NotificationRedirectTypeName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(resv.NotificationRedirectType)).ToString().Replace("_", " ").Replace("khalti", "");
                ViewBag.NotificationRedirectType = resv.NotificationRedirectTypeName;

                model = resv;
            }
            else
            {
                ViewBag.NotificationRedirectType = "";
            }

            return View(model);
        }
        [Authorize]
        public JsonResult GetNotificationExcelDataLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Title");
            columns.Add("Created By");
            columns.Add("SentStatusName");
            columns.Add("ReadStatusName");
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
            string SenderMemberId = context.Request.Form["SenderMemberId"];
            string Status = context.Request.Form["Status"];
            string StartDate = context.Request.Form["StartDate"];
            string EndDate = context.Request.Form["EndDate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddNotificationCampaignExcelData> trans = new List<AddNotificationCampaignExcelData>();

            AddNotificationCampaignExcelData w = new AddNotificationCampaignExcelData();
            w.NotificationCampaignExcelId = Convert.ToInt64(Id);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows
                     select new AddNotificationCampaignExcelData
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         ContactNumber = row["ContactNumber"].ToString(),
                         MemberName = row["MemberName"].ToString(),
                         SentStatus = Convert.ToInt32(row["SentStatus"]),
                         SentStatusName = @Enum.GetName(typeof(AddNotificationCampaignExcel.SentStatuses), Convert.ToInt64(row["SentStatus"])),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddNotificationCampaignExcelData>> objDataTableResponse = new DataTableResponse<List<AddNotificationCampaignExcelData>>
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