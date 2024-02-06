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
    public class UserInActiveStatusController : BaseAdminSessionController
    {
        // GET: UserInActiveStatus
        [HttpGet]
        [Authorize]
        public ActionResult Index(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            return View();
        }

        [Authorize]
        public JsonResult GetInActiveUserLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("Full Name");
            columns.Add("Contact No");            
            columns.Add("Remarks");
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
            string MemberId = context.Request.Form["MemberId"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddUserInActiveRemarks> trans = new List<AddUserInActiveRemarks>();

            GetUserInActiveRemarks w = new GetUserInActiveRemarks();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.ContactNumber = ContactNumber;
            w.FirstName = Name;
            w.StartDate = fromdate;
            w.EndDate = todate;            
            
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize);

            trans = (from DataRow row in dt.Rows
                     select new AddUserInActiveRemarks
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         FirstName = row["FirstName"].ToString(),
                         LastName = row["LastName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),                         
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         TotalUserCount =row["FilterTotalCount"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         Action = row["Action"].ToString(),
                         CreatedByName= row["CreatedByName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddUserInActiveRemarks>> objDataTableResponse = new DataTableResponse<List<AddUserInActiveRemarks>>
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