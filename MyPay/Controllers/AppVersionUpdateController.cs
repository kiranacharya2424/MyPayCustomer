using MyPay.Models.Add;
using MyPay.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class AppVersionUpdateController : BaseAdminSessionController
    {
        // GET: AppVersionUpdate
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                var json = System.IO.File.ReadAllText("C:\\MyPaySettings\\versionsettings.json");
                AddApiVersionSettings objApiSettings = new AddApiVersionSettings();
                objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiVersionSettings>(json);
                ViewBag.IosVersion = objApiSettings.iosVersion;
                ViewBag.AndroidVersion = objApiSettings.androidVersion;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex;
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult UpdateAppVersion(string ios, string android)
        {
            string result = string.Empty;
            try
            {

                if (string.IsNullOrEmpty(android))
                {
                    result = "Please enter android version";
                }
                else if (string.IsNullOrEmpty(ios))
                {
                    result = "Please enter ios version";
                }
                if (string.IsNullOrEmpty(result))
                {
                    string editjson = System.IO.File.ReadAllText("C:\\MyPaySettings\\versionsettings.json");
                    //string editjson = System.IO.File.ReadAllText(path);
                    //List<AddApiVersionSettings> jsonObj = JsonConvert.DeserializeObject<List<AddApiVersionSettings>>(editjson);
                    AddApiVersionSettings objApiSettings = new AddApiVersionSettings();
                    objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiVersionSettings>(editjson);
                    //objApiSettings.ForEach(j =>
                    //{
                    objApiSettings.androidVersion = android;
                    objApiSettings.iosVersion = ios;
                    //});
                    string haveeditjson = JsonConvert.SerializeObject(objApiSettings, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText("C:\\MyPaySettings\\versionsettings.json", haveeditjson);

                    AddAppVersionHistory histrory = new AddAppVersionHistory();
                    histrory.Android = android;
                    histrory.IOS = ios;
                    histrory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    histrory.CreatedByName = Session["AdminUserName"].ToString();
                    histrory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    histrory.UpdatedByName = Session["AdminUserName"].ToString();
                    if (histrory.Add())
                    {
                        result = "success";
                        Common.AddLogs("Updated App Version by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.VersionUpdate);

                    }
                    else
                    {
                        result = "failed to update app version";
                        Common.AddLogs("Failed to update App Version by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.VersionUpdate);
                    }
                }


            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        // GET: ServiceInactive
        public ActionResult History()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetHistoryList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("IOS");
            columns.Add("Android");
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
            string Provider = context.Request.Form["Provider"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            AddAppVersionHistory w = new AddAppVersionHistory();
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            List<AddAppVersionHistory> trans = (List<AddAppVersionHistory>)CommonEntityConverter.DataTableToList<AddAppVersionHistory>(dt);
            for (int i = 0; i < trans.Count; i++)
            {
                trans[i].Sno = (i + 1).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddAppVersionHistory>> objDataTableResponse = new DataTableResponse<List<AddAppVersionHistory>>
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