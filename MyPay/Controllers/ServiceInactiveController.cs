using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class ServiceInactiveController : BaseAdminSessionController
    {
        // GET: ServiceInactive
        public ActionResult Index()
        {
            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();

            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
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
            return View();
        }

        [Authorize]
        public JsonResult GetProviderServicesList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("FullName");
            columns.Add("UserName");
            columns.Add("Contact");
            columns.Add("Email");
            columns.Add("UniqueId");
            columns.Add("Status");

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
            string Provider = context.Request.Form["Provider"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddProviderLogoList> trans = new List<AddProviderLogoList>();

            AddProviderLogoList w = new AddProviderLogoList();
            w.ProviderServiceCategoryId = Provider;
            w.CheckActive = 1;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddProviderLogoList
                     {
                         Id=Convert.ToInt64(row["Id"]),
                         ProviderServiceCategoryId = row["ProviderServiceCategoryId"].ToString(),
                         ServiceAPIName = row["ServiceAPIName"].ToString(),                         
                         IsActive = Convert.ToInt32(row["IsActive"]),
                         IsServiceDown= Convert.ToBoolean(row["IsServiceDown"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddProviderLogoList>> objDataTableResponse = new DataTableResponse<List<AddProviderLogoList>>
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
        public JsonResult ServiceBlockUnblock(string Id, string Remarks)
        {
            string msg = "";
            if (Id == "0" || string.IsNullOrEmpty(Id))
            {
                msg = "Please Select Service";
            }
            else if (string.IsNullOrEmpty(Remarks))
            {
                msg = "Please enter Remarks";
            }
            else
            {
                AddProviderLogoList outobject = new AddProviderLogoList();
                GetProviderLogoList inobject = new GetProviderLogoList();
                inobject.Id = Convert.ToInt64(Id);
                AddProviderLogoList res = RepCRUD<GetProviderLogoList, AddProviderLogoList>.GetRecord(Common.StoreProcedures.sp_ProviderLogoList_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddServiceInactiveRemarks res_remarks = new AddServiceInactiveRemarks();
                    res_remarks.ServiceId = Convert.ToInt32(res.ProviderTypeId);
                    res_remarks.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res_remarks.CreatedByName = Session["AdminUserName"].ToString();
                    res_remarks.IsActive = true;
                    if (res.IsServiceDown)
                    {
                        res_remarks.Action = "Service Active by "+ Session["AdminUserName"].ToString();
                        res.IsServiceDown = false;
                    }
                    else
                    {
                        res_remarks.Action = "Service InActive by " + Session["AdminUserName"].ToString();
                        res.IsServiceDown = true;
                    }
                    res_remarks.Remarks = Remarks;
                    res_remarks.Add();

                    bool IsUpdated = RepCRUD<AddProviderLogoList, GetProviderLogoList>.Update(res, "providerlogoslist");
                    if (IsUpdated)
                    {
                        res.ProviderServiceCategoryListUpdate();
                        ViewBag.SuccessMessage = "Successfully Update";
                        Common.AddLogs("Updated Providerlogoslist(Id:" + res.Id + ")  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.ProviderService, Convert.ToInt64(Session["AdminMemberId"]), Convert.ToString(Session["AdminUserName"]), false, "web","",(int)AddLog.LogActivityEnum.ProviderService_Status_Change);
                        msg = "Successfully Update";
                    }
                    else
                    {
                        ViewBag.Message = "Not Updated";
                        Common.AddLogs("Not Updated Providerlogoslist", true, (int)AddLog.LogType.ProviderService);
                        msg = "Not Updated";
                    }
                }
                else
                {
                    msg = "Providerlogoslist Id Not Found.";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        // GET: ServiceInactive
        public ActionResult ServiceInactiveRemarks()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetServiceInactiveRemarksList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("FullName");
            columns.Add("UserName");
            columns.Add("Contact");
            columns.Add("Email");
            columns.Add("UniqueId");
            columns.Add("Status");

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
            string Provider = context.Request.Form["Provider"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddServiceInactiveRemarks> trans = new List<AddServiceInactiveRemarks>();

            AddServiceInactiveRemarks w = new AddServiceInactiveRemarks();
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddServiceInactiveRemarks
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         ServiceId = Convert.ToInt32(row["ServiceId"]),
                         ServiceName= @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt32(row["ServiceId"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         Action = row["Action"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         CreatedByName= row["CreatedByName"].ToString(),
                         CreatedDateDt = row["IndiaDate"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddServiceInactiveRemarks>> objDataTableResponse = new DataTableResponse<List<AddServiceInactiveRemarks>>
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