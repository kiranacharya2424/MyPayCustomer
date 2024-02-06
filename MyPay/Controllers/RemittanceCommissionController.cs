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
    public class RemittanceCommissionController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            AddRemittanceCommission model = new AddRemittanceCommission();
            AddRemittanceSourceCurrencyList objSourceCurrency = new AddRemittanceSourceCurrencyList();
            objSourceCurrency.CheckActive = 1;
            objSourceCurrency.CheckDelete = 0;
            List<AddRemittanceSourceCurrencyList> objSourceCurrencyList = objSourceCurrency.GetRecordList();

            List<SelectListItem> objSourceCurrencyList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Source Currency ---";
                objDefault.Selected = true;
                objSourceCurrencyList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objSourceCurrencyList.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = objSourceCurrencyList[i].CurrencyId.ToString();
                objItem.Text = objSourceCurrencyList[i].CurrencyName.ToString();
                objSourceCurrencyList_SelectList.Add(objItem);
            }
            ViewBag.SourceCurrency = objSourceCurrencyList_SelectList;

            // **********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN *********** //

            AddRemittanceDestinationCurrencyList objDestinationCurrency = new AddRemittanceDestinationCurrencyList();
            objDestinationCurrency.CheckActive = 1;
            objDestinationCurrency.CheckDelete = 0;
            List<AddRemittanceDestinationCurrencyList> objDestinationCurrencyList = objDestinationCurrency.GetRecordList();

            List<SelectListItem> objDestinationCurrencyList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Destination Currency ---";
                objDefault.Selected = true;
                objDestinationCurrencyList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objDestinationCurrencyList.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = objDestinationCurrencyList[i].CurrencyId.ToString();
                objItem.Text = objDestinationCurrencyList[i].CurrencyName.ToString();
                objDestinationCurrencyList_SelectList.Add(objItem);
            }
            ViewBag.DestinationCurrency = objDestinationCurrencyList_SelectList;

            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddRemittanceCommission vmodel)
        {
            AddRemittanceSourceCurrencyList objSourceCurrency = new AddRemittanceSourceCurrencyList();
            objSourceCurrency.CheckActive = 1;
            objSourceCurrency.CheckDelete = 0;
            List<AddRemittanceSourceCurrencyList> objSourceCurrencyList = objSourceCurrency.GetRecordList();

            List<SelectListItem> objSourceCurrencyList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Source Currency ---";
                objDefault.Selected = true;
                objSourceCurrencyList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objSourceCurrencyList.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = objSourceCurrencyList[i].CurrencyId.ToString();
                objItem.Text = objSourceCurrencyList[i].CurrencyName.ToString();
                objSourceCurrencyList_SelectList.Add(objItem);
            }
            ViewBag.SourceCurrency = objSourceCurrencyList_SelectList;

            // **********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN *********** //

            AddRemittanceDestinationCurrencyList objDestinationCurrency = new AddRemittanceDestinationCurrencyList();
            objDestinationCurrency.CheckActive = 1;
            objDestinationCurrency.CheckDelete = 0;
            List<AddRemittanceDestinationCurrencyList> objDestinationCurrencyList = objDestinationCurrency.GetRecordList();

            List<SelectListItem> objDestinationCurrencyList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Destination Currency ---";
                objDefault.Selected = true;
                objDestinationCurrencyList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objDestinationCurrencyList.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = objDestinationCurrencyList[i].CurrencyId.ToString();
                objItem.Text = objDestinationCurrencyList[i].CurrencyName.ToString();
                objDestinationCurrencyList_SelectList.Add(objItem);
            }
            ViewBag.DestinationCurrency = objDestinationCurrencyList_SelectList;


            AddRemittanceCommission model = new AddRemittanceCommission();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewRemittanceCommissionDataRow(AddRemittanceCommission req_ObjRemittanceCommission_req)
        {
            if (req_ObjRemittanceCommission_req.SourceCurrency != req_ObjRemittanceCommission_req.DestinationCurrency)
            {
                Int32 ServiceId = req_ObjRemittanceCommission_req.ServiceId;
                AddRemittanceCommission ObjRemittanceCommission = new AddRemittanceCommission();
                ObjRemittanceCommission.ServiceId = Convert.ToInt32(ServiceId);
                ObjRemittanceCommission.SourceCurrencyId = Convert.ToInt32(req_ObjRemittanceCommission_req.SourceCurrencyId);
                ObjRemittanceCommission.SourceCurrency = Convert.ToString(req_ObjRemittanceCommission_req.SourceCurrency);
                ObjRemittanceCommission.DestinationCurrencyId = Convert.ToInt32(req_ObjRemittanceCommission_req.DestinationCurrencyId);
                ObjRemittanceCommission.DestinationCurrency = Convert.ToString(req_ObjRemittanceCommission_req.DestinationCurrency);
                ObjRemittanceCommission.ServiceId = Convert.ToInt32(ServiceId);
                ObjRemittanceCommission.IsActive = true;
                ObjRemittanceCommission.IsDeleted = false;
                ObjRemittanceCommission.FromDate = System.DateTime.UtcNow;
                ObjRemittanceCommission.ToDate = System.DateTime.UtcNow;
                ObjRemittanceCommission.Add();
                return Json(ObjRemittanceCommission, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Source and Destination Currency cannot be same", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult DeleteRemittanceCommissionDataRow(string Id)
        {

            AddRemittanceCommission resRemittanceCommission = new AddRemittanceCommission();
            resRemittanceCommission.Id = Convert.ToInt64(Id);
            bool resRemittanceCommissionFlag = resRemittanceCommission.GetRecord();
            resRemittanceCommission.IsDeleted = true;
            bool IsUpdated = resRemittanceCommission.Update();
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated AddRemittanceCommission(RemittanceCommissionId:" + Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                //Insert into AddRemittanceCommissionUpdateHistory Table
                AddRemittanceCommission res = new AddRemittanceCommission();
                res.Id = Convert.ToInt64(Id);
                bool resFlag = res.GetRecord();
                if (resFlag && res != null && res.Id != 0)
                {
                    AddRemittanceCommissionUpdateHistory ObjRemittanceCommissionhistory = new AddRemittanceCommissionUpdateHistory();
                    ObjRemittanceCommissionhistory.CommissionId = res.Id;
                    ObjRemittanceCommissionhistory.ServiceId = res.ServiceId;
                    ObjRemittanceCommissionhistory.IsActive = true;
                    ObjRemittanceCommissionhistory.FromDate = res.FromDate;
                    ObjRemittanceCommissionhistory.ToDate = res.ToDate;
                    ObjRemittanceCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjRemittanceCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                    ObjRemittanceCommissionhistory.MinimumAmount = res.MinimumAmount;
                    ObjRemittanceCommissionhistory.MaximumAmount = res.MaximumAmount;
                    ObjRemittanceCommissionhistory.Status = (int)AddRemittanceCommissionUpdateHistory.UpdatedStatuses.Deleted;
                    ObjRemittanceCommissionhistory.Type = res.Type;
                    ObjRemittanceCommissionhistory.ServiceCharge = res.ServiceCharge;
                    bool InsertFlag = ObjRemittanceCommissionhistory.Add();
                    if (InsertFlag)
                    {
                        Common.AddLogs("Successfully Add AddRemittanceCommissionUpdateHistory(RemittanceCommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                    }
                }
            }
            return Json(resRemittanceCommission, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [Authorize]
        public JsonResult GetAdminRemittanceCommissionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Sno");
            columns.Add("ServiceId");
            columns.Add("ServiceName");
            columns.Add("MinimumAmount");
            columns.Add("MaximumAmount");
            //columns.Add("FixedRemittanceCommission");
            columns.Add("PercentageRemittanceCommission");
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
            string SourceCurrencyId = context.Request.Form["SourceCurrencyId"];
            string DestinationCurrencyId = context.Request.Form["DestinationCurrencyId"];
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

            AddRemittanceCommission w = new AddRemittanceCommission();
            w.Id = Convert.ToInt64(Id);
            w.ServiceId = Convert.ToInt32(ServiceId);
            w.SourceCurrencyId = SourceCurrencyId != "" ? Convert.ToInt64(SourceCurrencyId) : 0;
            w.DestinationCurrencyId = DestinationCurrencyId != "" ? Convert.ToInt64(DestinationCurrencyId) : 0;
            if (!string.IsNullOrEmpty(FromDate))
            {
                w.FromDate = Convert.ToDateTime(FromDate);
                w.ToDate = Convert.ToDateTime(ToDate);
            }
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


            List<AddRemittanceCommission> objRemittanceCommission = (List<AddRemittanceCommission>)CommonEntityConverter.DataTableToList<AddRemittanceCommission>(dt);
            //for (int i = 0; i < objRemittanceCommission.Count; i++)
            //{
            //    objRemittanceCommission[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objRemittanceCommission[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");
            //    objRemittanceCommission[i].GenderTypeName = ((AddRemittanceCommission.GenderTypes)Convert.ToInt32(objRemittanceCommission[i].GenderType)).ToString();
            //    objRemittanceCommission[i].KycTypeName = ((AddRemittanceCommission.KycTypes)Convert.ToInt32(objRemittanceCommission[i].KycType)).ToString();
            //    objRemittanceCommission[i].Sno = (i + 1).ToString();
            //}
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceCommission>> objDataTableResponse = new DataTableResponse<List<AddRemittanceCommission>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objRemittanceCommission
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public JsonResult AddRemittanceCommissionUpdateCall(AddRemittanceCommission req_ObjRemittanceCommission_req)
        {

            AddRemittanceCommission outobject = new AddRemittanceCommission();
            float AddRemittanceCommissionlabCount = outobject.CountCommissionCheck(req_ObjRemittanceCommission_req.ServiceId.ToString(), req_ObjRemittanceCommission_req.MinimumAmount, req_ObjRemittanceCommission_req.MaximumAmount, req_ObjRemittanceCommission_req.Id, req_ObjRemittanceCommission_req.SourceCurrencyId, req_ObjRemittanceCommission_req.DestinationCurrencyId);
            if (AddRemittanceCommissionlabCount > 0)
            {
                AddRemittanceCommission ObjRemittanceCommission = new AddRemittanceCommission();
                return Json(ObjRemittanceCommission, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AddRemittanceCommission resRemittanceCommission = new AddRemittanceCommission();
                resRemittanceCommission.Id = Convert.ToInt64(req_ObjRemittanceCommission_req.Id);
                resRemittanceCommission.GetRecord();

                resRemittanceCommission.ServiceId = req_ObjRemittanceCommission_req.ServiceId;
                resRemittanceCommission.MinimumAmount = req_ObjRemittanceCommission_req.MinimumAmount;
                resRemittanceCommission.MaximumAmount = req_ObjRemittanceCommission_req.MaximumAmount;
                resRemittanceCommission.MinimumAllowedSC = req_ObjRemittanceCommission_req.MinimumAllowedSC;
                resRemittanceCommission.MaximumAllowedSC = req_ObjRemittanceCommission_req.MaximumAllowedSC;
                resRemittanceCommission.ServiceCharge = req_ObjRemittanceCommission_req.ServiceCharge;
                resRemittanceCommission.ServiceChargeFixed = req_ObjRemittanceCommission_req.ServiceChargeFixed; 
                resRemittanceCommission.FromDate = Convert.ToDateTime(req_ObjRemittanceCommission_req.FromDate);
                resRemittanceCommission.IsActive = true;
                resRemittanceCommission.IsDeleted = false;
                resRemittanceCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                resRemittanceCommission.CreatedByName = Session["AdminUserName"].ToString();
                resRemittanceCommission.ToDate = Convert.ToDateTime(req_ObjRemittanceCommission_req.ToDate);
                resRemittanceCommission.UpdatedDate = System.DateTime.UtcNow;
                bool IsUpdated = resRemittanceCommission.Update();
                if (IsUpdated)
                {
                    Common.AddLogs("Successfully Updated AddRemittanceCommission(RemittanceCommissionId:" + req_ObjRemittanceCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                    //Insert into AddRemittanceCommissionUpdateHistory Table
                    AddRemittanceCommission res = new AddRemittanceCommission();
                    res.Id = resRemittanceCommission.Id;
                    bool resFlag = res.GetRecord();
                    if (resFlag && res != null && res.Id != 0)
                    {
                        AddRemittanceCommissionUpdateHistory ObjRemittanceCommissionhistory = new AddRemittanceCommissionUpdateHistory();
                        ObjRemittanceCommissionhistory.SourceCurrencyId = Convert.ToInt32(res.SourceCurrencyId);
                        ObjRemittanceCommissionhistory.DestinationCurrencyId = Convert.ToInt32(res.DestinationCurrencyId);
                        ObjRemittanceCommissionhistory.CommissionId = res.Id;
                        ObjRemittanceCommissionhistory.ServiceId = res.ServiceId;
                        ObjRemittanceCommissionhistory.IsActive = res.IsActive;
                        ObjRemittanceCommissionhistory.IsDeleted = res.IsDeleted;
                        ObjRemittanceCommissionhistory.FromDate = res.FromDate;
                        ObjRemittanceCommissionhistory.ToDate = res.ToDate;
                        ObjRemittanceCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        ObjRemittanceCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                        ObjRemittanceCommissionhistory.MinimumAmount = res.MinimumAmount;
                        ObjRemittanceCommissionhistory.MaximumAmount = res.MaximumAmount;
                        ObjRemittanceCommissionhistory.MinimumAllowedSC = res.MinimumAllowedSC;
                        ObjRemittanceCommissionhistory.MaximumAllowedSC = res.MaximumAllowedSC;
                        ObjRemittanceCommissionhistory.Status = (int)AddRemittanceCommissionUpdateHistory.UpdatedStatuses.Updated;
                        ObjRemittanceCommissionhistory.Type = res.Type;
                        ObjRemittanceCommissionhistory.ServiceCharge = res.ServiceCharge;
                        bool iFlag = ObjRemittanceCommissionhistory.Add();
                        if (iFlag)
                        {
                            Common.AddLogs("Successfully Add AddRemittanceCommissionUpdateHistory(RemittanceCommissionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                        }
                    }
                }
                return Json(resRemittanceCommission, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateRemittanceCommissionCall(AddRemittanceCommission req_ObjRemittanceCommission_req, string IsActive)
        {

            AddRemittanceCommission objUpdateRemittanceCommission = new AddRemittanceCommission();
            objUpdateRemittanceCommission.Id = req_ObjRemittanceCommission_req.Id;
            objUpdateRemittanceCommission.GetRecord();
            objUpdateRemittanceCommission.IsActive = Convert.ToBoolean(IsActive);
            objUpdateRemittanceCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateRemittanceCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateRemittanceCommission.CreatedByName = Session["AdminUserName"].ToString();
            bool IsUpdated = objUpdateRemittanceCommission.Update();
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated AddRemittanceCommission Status(RemittanceCommissionId:" + req_ObjRemittanceCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
            }
            return Json(objUpdateRemittanceCommission, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult GetProviderServiceList(string ProviderId)
        {
            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_Providerchange(ProviderId);
            return Json(districtlist);
        }
        [HttpGet]
        [Authorize]
        public ActionResult RemittanceCommissionUpdateHistory()
        {
            AddRemittanceSourceCurrencyList objSourceCurrency = new AddRemittanceSourceCurrencyList();
            objSourceCurrency.CheckActive = 1;
            objSourceCurrency.CheckDelete = 0;
            List<AddRemittanceSourceCurrencyList> objSourceCurrencyList = objSourceCurrency.GetRecordList();

            List<SelectListItem> objSourceCurrencyList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Source Currency ---";
                objDefault.Selected = true;
                objSourceCurrencyList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objSourceCurrencyList.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = objSourceCurrencyList[i].Id.ToString();
                objItem.Text = objSourceCurrencyList[i].CurrencyName.ToString();
                objSourceCurrencyList_SelectList.Add(objItem);
            }
            ViewBag.SourceCurrency = objSourceCurrencyList_SelectList;

            // *********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN ********** //


            AddRemittanceDestinationCurrencyList objDestinationCurrency = new AddRemittanceDestinationCurrencyList();
            objDestinationCurrency.CheckActive = 1;
            objDestinationCurrency.CheckDelete = 0;
            List<AddRemittanceDestinationCurrencyList> objDestinationCurrencyList = objDestinationCurrency.GetRecordList();

            List<SelectListItem> objDestinationCurrencyList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Destination Currency ---";
                objDefault.Selected = true;
                objDestinationCurrencyList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objDestinationCurrencyList.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = objDestinationCurrencyList[i].Id.ToString();
                objItem.Text = objDestinationCurrencyList[i].CurrencyName.ToString();
                objDestinationCurrencyList_SelectList.Add(objItem);
            }
            ViewBag.DestinationCurrency = objDestinationCurrencyList_SelectList;

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

            AddRemittanceCommissionUpdateHistory model = new AddRemittanceCommissionUpdateHistory();
            return View(model);
        }

        [Authorize]
        public JsonResult GetRemittanceCommissionUpdateHistoryLists()
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
            string SourceCurrencyId = context.Request.Form["SourceCurrencyId"];
            string DestinationCurrencyId = context.Request.Form["DestinationCurrencyId"];
            string Status = context.Request.Form["Status"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            string Id = context.Request.Form["Id"];
            string ServiceId = context.Request.Form["ServiceId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceCommissionUpdateHistory> trans = new List<AddRemittanceCommissionUpdateHistory>();
            AddRemittanceCommissionUpdateHistory w = new AddRemittanceCommissionUpdateHistory();
            w.Id = Convert.ToInt64(Id);
            w.SourceCurrencyId = SourceCurrencyId != "" ? Convert.ToInt32(SourceCurrencyId) : 0;
            w.DestinationCurrencyId = DestinationCurrencyId != "" ? Convert.ToInt32(DestinationCurrencyId) : 0;
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
            AddRemittanceSourceCurrencyList objSourceCurrency = new AddRemittanceSourceCurrencyList();
            objSourceCurrency.CheckActive = 1;
            objSourceCurrency.CheckDelete = 0;
            List<AddRemittanceSourceCurrencyList> objSourceCurrencyList = objSourceCurrency.GetRecordList();
            AddRemittanceDestinationCurrencyList objDestinationCurrency = new AddRemittanceDestinationCurrencyList();
            objDestinationCurrency.CheckActive = 1;
            objDestinationCurrency.CheckDelete = 0;
            List<AddRemittanceDestinationCurrencyList> objDestinationCurrencyList = objDestinationCurrency.GetRecordList();
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceCommissionUpdateHistory
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         SourceCurrencyName = objSourceCurrencyList.Where(x => x.Id == Convert.ToInt32(row["SourceCurrencyId"])).FirstOrDefault().CurrencyName,
                         DestinationCurrencyName = objDestinationCurrencyList.Where(x => x.Id == Convert.ToInt32(row["DestinationCurrencyId"])).FirstOrDefault().CurrencyName,
                         MinimumAmount = Convert.ToDecimal(row["MinimumAmount"]),
                         MaximumAmount = Convert.ToDecimal(row["MaximumAmount"]),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         // ScheduleStatus = row["ScheduleStatus"].ToString(),
                         StatusName = @Enum.GetName(typeof(AddRemittanceCommissionUpdateHistory.UpdatedStatuses), Convert.ToInt64(row["Status"])),
                         IPAddress = row["IpAddress"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         FromDateDT = row["FromDateDt"].ToString(),
                         ToDateDT = row["ToDateDt"].ToString(),
                         MinimumAllowedSC = Convert.ToDecimal(row["MinimumAllowedSC"]),
                         MaximumAllowedSC = Convert.ToDecimal(row["MaximumAllowedSC"]),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceCommissionUpdateHistory>> objDataTableResponse = new DataTableResponse<List<AddRemittanceCommissionUpdateHistory>>
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

    }
}