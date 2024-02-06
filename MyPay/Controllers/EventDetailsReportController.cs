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
    public class EventDetailsReportController : BaseAdminSessionController
    {
        // GET: EventDetailsReport
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Payment Status",
                Value = "2",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Completed",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Pending",
                Value = "0"
            });

            ViewBag.Status = items;
            return View();
        }

        [Authorize]
        public JsonResult GetEventDetailLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("RequestId");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("Service");
            columns.Add("ServiceCharge");
            columns.Add("Cashback");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
            columns.Add("UpdateBy");
            columns.Add("AvailableBalance");
            columns.Add("PreviousBalance");
            columns.Add("PaymentStatus");
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
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string MerchantCode = context.Request.Form["MerchantCode"];
            string PaymentMerchantId = context.Request.Form["PaymentMerchantId"];
            string EventName = context.Request.Form["EventName"];
            string OrderId = context.Request.Form["OrderId"];
            string PaymentStatus = context.Request.Form["PaymentStatus"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            //List<AddEventDetails> trans = new List<AddEventDetails>();

            AddEventDetails w = new AddEventDetails();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.merchantCode = MerchantCode;
            w.paymentMerchantId = PaymentMerchantId;
            w.customerName = Name;
            w.customerMobile = ContactNumber;
            w.eventName = EventName;
            w.OrderId = OrderId;
            w.CheckIsBooked = 1;
            w.CheckDelete = 0;
            w.CheckActive = 1;
            w.CheckPaymentDone = Convert.ToInt32(PaymentStatus);

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            List<AddEventDetails> trans = (List<AddEventDetails>)CommonEntityConverter.DataTableToList<AddEventDetails>(dt);


            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddEventDetails>> objDataTableResponse = new DataTableResponse<List<AddEventDetails>>
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