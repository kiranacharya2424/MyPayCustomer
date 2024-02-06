using MyPay.Models.Add;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class RemittanceConversionRateSettingsController : BaseAdminSessionController
    {
        // GET: RemittanceConversionRateSettings
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            AddRemittanceConversionRateSettings model = new AddRemittanceConversionRateSettings();
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
        public ActionResult Index(AddRemittanceConversionRateSettings vmodel)
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


            AddRemittanceConversionRateSettings model = new AddRemittanceConversionRateSettings();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddNewRemittanceConversionDataRow(AddRemittanceConversionRateSettings req_ObjRemittanceCommission_req)
        {
            if (req_ObjRemittanceCommission_req.SourceCurrency != req_ObjRemittanceCommission_req.DestinationCurrency)
            {
                AddRemittanceConversionRateSettings ObjRemittanceCommission = new AddRemittanceConversionRateSettings();
                ObjRemittanceCommission.SourceCurrencyId = Convert.ToInt32(req_ObjRemittanceCommission_req.SourceCurrencyId);
                ObjRemittanceCommission.SourceCurrency = Convert.ToString(req_ObjRemittanceCommission_req.SourceCurrency);
                ObjRemittanceCommission.DestinationCurrencyId = Convert.ToInt32(req_ObjRemittanceCommission_req.DestinationCurrencyId);
                ObjRemittanceCommission.DestinationCurrency = Convert.ToString(req_ObjRemittanceCommission_req.DestinationCurrency);
                ObjRemittanceCommission.IsActive = true;
                ObjRemittanceCommission.IsDeleted = false;
                ObjRemittanceCommission.FromDate = System.DateTime.UtcNow.TimeOfDay;
                ObjRemittanceCommission.ToDate = System.DateTime.UtcNow.TimeOfDay;
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
        public JsonResult DeleteRemittanceConversionDataRow(string Id)
        {

            AddRemittanceConversionRateSettings resRemittanceCommission = new AddRemittanceConversionRateSettings();
            resRemittanceCommission.Id = Convert.ToInt64(Id);
            bool resRemittanceCommissionFlag = resRemittanceCommission.GetRecord();
            resRemittanceCommission.IsDeleted = true;
            bool IsUpdated = resRemittanceCommission.Update();
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Remittance Conversion Rate Settings(RemittanceConversionId:" + Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                //Insert into AddRemittanceCommissionUpdateHistory Table
                AddRemittanceConversionRateSettings res = new AddRemittanceConversionRateSettings();
                res.Id = Convert.ToInt64(Id);
                bool resFlag = res.GetRecord();
                if (resFlag && res != null && res.Id != 0)
                {
                    AddRemittanceConversionRateSettingsHistory ObjRemittanceCommissionhistory = new AddRemittanceConversionRateSettingsHistory();
                    ObjRemittanceCommissionhistory.ConversionRateId = res.Id;
                    ObjRemittanceCommissionhistory.IsActive = true;
                    ObjRemittanceCommissionhistory.FromDate = res.FromDate;
                    ObjRemittanceCommissionhistory.ToDate = res.ToDate;
                    ObjRemittanceCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    ObjRemittanceCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                    ObjRemittanceCommissionhistory.ConversionRate = res.ConversionRate;
                    //ObjRemittanceCommissionhistory.Status = (int)AddRemittanceCommissionUpdateHistory.UpdatedStatuses.Deleted;
                    bool InsertFlag = ObjRemittanceCommissionhistory.Add();
                    if (InsertFlag)
                    {
                        Common.AddLogs("Successfully Add Remittance Conversion Rate Settings History(RemittanceConversionRateId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                }
            }
            return Json(resRemittanceCommission, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetAdminRemittanceConversionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Sno");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
            columns.Add("SourceCurrency");
            columns.Add("DestinationCurrency");
            columns.Add("ConversionRate");
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
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            AddRemittanceConversionRateSettings w = new AddRemittanceConversionRateSettings();
            w.Id = Convert.ToInt64(Id);
            w.SourceCurrencyId = SourceCurrencyId != "" ? Convert.ToInt64(SourceCurrencyId) : 0;
            w.DestinationCurrencyId = DestinationCurrencyId != "" ? Convert.ToInt64(DestinationCurrencyId) : 0;
            //if (!string.IsNullOrEmpty(FromDate))
            //{
            //    w.FromDate = FromDate;
            //    w.ToDate = Convert.ToDateTime(ToDate);
            //}
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


            List<AddRemittanceConversionRateSettings> objRemittanceCommission = (List<AddRemittanceConversionRateSettings>)CommonEntityConverter.DataTableToList<AddRemittanceConversionRateSettings>(dt);
            //for (int i = 0; i < objRemittanceCommission.Count; i++)
            //{
            //    objRemittanceCommission[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objRemittanceCommission[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");
            //    objRemittanceCommission[i].GenderTypeName = ((AddRemittanceCommission.GenderTypes)Convert.ToInt32(objRemittanceCommission[i].GenderType)).ToString();
            //    objRemittanceCommission[i].KycTypeName = ((AddRemittanceCommission.KycTypes)Convert.ToInt32(objRemittanceCommission[i].KycType)).ToString();
            //    objRemittanceCommission[i].Sno = (i + 1).ToString();
            //}
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceConversionRateSettings>> objDataTableResponse = new DataTableResponse<List<AddRemittanceConversionRateSettings>>
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
        public JsonResult AddRemittanceConversionUpdateCall(AddRemittanceConversionRateSettings req_ObjRemittanceCommission_req)
        {

            AddRemittanceConversionRateSettings outobject = new AddRemittanceConversionRateSettings();
            float AddRemittanceCommissionlabCount = outobject.CountCommissionCheck(req_ObjRemittanceCommission_req.FromDate, req_ObjRemittanceCommission_req.ToDate, req_ObjRemittanceCommission_req.Id, req_ObjRemittanceCommission_req.SourceCurrencyId, req_ObjRemittanceCommission_req.DestinationCurrencyId);
            if (AddRemittanceCommissionlabCount > 0)
            {
                AddRemittanceConversionRateSettings ObjRemittanceCommission = new AddRemittanceConversionRateSettings();
                return Json(ObjRemittanceCommission, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AddRemittanceConversionRateSettings resRemittanceCommission = new AddRemittanceConversionRateSettings();
                resRemittanceCommission.Id = Convert.ToInt64(req_ObjRemittanceCommission_req.Id);
                resRemittanceCommission.GetRecord();

                resRemittanceCommission.ConversionRate = req_ObjRemittanceCommission_req.ConversionRate;
                resRemittanceCommission.FromDate = req_ObjRemittanceCommission_req.FromDate;
                resRemittanceCommission.IsActive = true;
                resRemittanceCommission.IsDeleted = false;
                resRemittanceCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                resRemittanceCommission.CreatedByName = Session["AdminUserName"].ToString();
                resRemittanceCommission.ToDate = req_ObjRemittanceCommission_req.ToDate;
                resRemittanceCommission.UpdatedDate = System.DateTime.UtcNow;
                bool IsUpdated = resRemittanceCommission.Update();
                if (IsUpdated)
                {
                    Common.AddLogs("Successfully Updated Remittance Conversion Rate Settings(RemittanceConversionId:" + req_ObjRemittanceCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
                    //Insert into AddRemittanceCommissionUpdateHistory Table
                    AddRemittanceConversionRateSettings res = new AddRemittanceConversionRateSettings();
                    res.Id = resRemittanceCommission.Id;
                    bool resFlag = res.GetRecord();
                    if (resFlag && res != null && res.Id != 0)
                    {
                        AddRemittanceConversionRateSettingsHistory ObjRemittanceCommissionhistory = new AddRemittanceConversionRateSettingsHistory();
                        ObjRemittanceCommissionhistory.SourceCurrencyId = Convert.ToInt32(res.SourceCurrencyId);
                        ObjRemittanceCommissionhistory.DestinationCurrencyId = Convert.ToInt32(res.DestinationCurrencyId);
                        ObjRemittanceCommissionhistory.ConversionRateId = res.Id;
                        ObjRemittanceCommissionhistory.ConversionRate = res.ConversionRate;
                        ObjRemittanceCommissionhistory.IsActive = res.IsActive;
                        ObjRemittanceCommissionhistory.IsDeleted = res.IsDeleted;
                        ObjRemittanceCommissionhistory.FromDate = res.FromDate;
                        ObjRemittanceCommissionhistory.ToDate = res.ToDate;
                        ObjRemittanceCommissionhistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        ObjRemittanceCommissionhistory.CreatedByName = Session["AdminUserName"].ToString();
                        //ObjRemittanceCommissionhistory.Status = (int)AddRemittanceCommissionUpdateHistory.UpdatedStatuses.Updated;
                        bool iFlag = ObjRemittanceCommissionhistory.Add();
                        if (iFlag)
                        {
                            Common.AddLogs("Successfully Add Remittance Conversion Rate Settings History(RemittanceConversionId:" + res.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                        }
                    }
                }
                return Json(resRemittanceCommission, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateRemittanceConversionCall(AddRemittanceConversionRateSettings req_ObjRemittanceCommission_req, string IsActive)
        {

            AddRemittanceConversionRateSettings objUpdateRemittanceCommission = new AddRemittanceConversionRateSettings();
            objUpdateRemittanceCommission.Id = req_ObjRemittanceCommission_req.Id;
            objUpdateRemittanceCommission.GetRecord();
            objUpdateRemittanceCommission.IsActive = Convert.ToBoolean(IsActive);
            objUpdateRemittanceCommission.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateRemittanceCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            objUpdateRemittanceCommission.CreatedByName = Session["AdminUserName"].ToString();
            bool IsUpdated = objUpdateRemittanceCommission.Update();
            if (IsUpdated)
            {
                Common.AddLogs("Successfully Updated Remittance Conversion Status(RemittanceConversionId:" + req_ObjRemittanceCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates));
            }
            return Json(objUpdateRemittanceCommission, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemittanceConversionUpdateHistory()
        {
            AddRemittanceSourceCurrencyList objSourceCurrency = new AddRemittanceSourceCurrencyList();
            objSourceCurrency.CheckActive = 1;
            objSourceCurrency.CheckDelete = 0;
            List<AddRemittanceSourceCurrencyList> objSourceCurrencyList = objSourceCurrency.GetRecordList();

            List<SelectListItem> objSourceCurrencyList_SelectList = new List<SelectListItem>();
            // *********  ADD DEFAULT VALUE IN DROPDOWN ********** //
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

            // ********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN ********* //


            AddRemittanceDestinationCurrencyList objDestinationCurrency = new AddRemittanceDestinationCurrencyList();
            objDestinationCurrency.CheckActive = 1;
            objDestinationCurrency.CheckDelete = 0;
            List<AddRemittanceDestinationCurrencyList> objDestinationCurrencyList = objDestinationCurrency.GetRecordList();

            List<SelectListItem> objDestinationCurrencyList_SelectList = new List<SelectListItem>();
            // *********  ADD DEFAULT VALUE IN DROPDOWN ********** //
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

            AddRemittanceConversionRateSettingsHistory model = new AddRemittanceConversionRateSettingsHistory();
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
            columns.Add("ConversionRate");
            columns.Add("StartDate");
            columns.Add("EndDate");
            columns.Add("UpdatedBy");
            columns.Add("IpAddress");
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

            List<AddRemittanceConversionRateSettingsHistory> trans = new List<AddRemittanceConversionRateSettingsHistory>();
            AddRemittanceConversionRateSettingsHistory w = new AddRemittanceConversionRateSettingsHistory();
            w.Id = Convert.ToInt64(Id)
;
            w.SourceCurrencyId = SourceCurrencyId != "" ? Convert.ToInt32(SourceCurrencyId) : 0;
            w.DestinationCurrencyId = DestinationCurrencyId != "" ? Convert.ToInt32(DestinationCurrencyId) : 0;
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

                     select new AddRemittanceConversionRateSettingsHistory
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         SourceCurrencyName = objSourceCurrencyList.Where(x => x.CurrencyId == Convert.ToInt32(row["SourceCurrencyId"])).FirstOrDefault().CurrencyName,
                         DestinationCurrencyName = objDestinationCurrencyList.Where(x => x.CurrencyId == Convert.ToInt32(row["DestinationCurrencyId"])).FirstOrDefault().CurrencyName,
                         ConversionRate = Convert.ToDecimal(row["ConversionRate"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         IPAddress = row["IpAddress"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         FromDateDT = row["FromDateDt"].ToString(),
                         ToDateDT = row["ToDateDt"].ToString()

                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceConversionRateSettingsHistory>> objDataTableResponse = new DataTableResponse<List<AddRemittanceConversionRateSettingsHistory>>
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