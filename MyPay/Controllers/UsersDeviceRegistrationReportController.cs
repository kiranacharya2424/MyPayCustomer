using MyPay.Models.Add;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class UsersDeviceRegistrationReportController : BaseAdminSessionController
    {
        // GET: UsersDeviceRegistrationReport
        public ActionResult Index(string MemberId)
        {
            ViewBag.MemberId = MemberId;
            return View();
        }

        [Authorize]
        public JsonResult GetUsersDeviceRegistrationLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            columns.Add("MemberId");
            columns.Add("PlatForm");
            columns.Add("IMIE");
            columns.Add("IsActive");
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
            string MemberId = context.Request.Form["MemberId"];
            string DeviceId = context.Request.Form["DeviceId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddUsersDeviceRegistration> trans = new List<AddUsersDeviceRegistration>();

            GetUsersDeviceRegistration w = new GetUsersDeviceRegistration();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.SequenceNo = -1;
            w.IMIE = DeviceId;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddUsersDeviceRegistration
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         DeviceCode = row["DeviceCode"].ToString(),
                         PlatForm = row["PlatForm"].ToString(),
                         IpAddress = row["IpAddress"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         PreviousDeviceCode = row["DisabledFromDeviceCode"].ToString(),
                         IMIE = row["IMIE"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddUsersDeviceRegistration>> objDataTableResponse = new DataTableResponse<List<AddUsersDeviceRegistration>>
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