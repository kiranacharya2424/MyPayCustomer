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
    public class NotificationController : BaseAdminSessionController
    {
        [Authorize]
        // GET: NotificationReport
        public ActionResult Index()
        {

            return View();
        }

        [Authorize]
        public JsonResult GetNotificationLists()
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
            string MemberId = context.Request.Form["MemberId"];
            string SenderMemberId = context.Request.Form["SenderMemberId"];
            string Status = context.Request.Form["Status"];
            string StartDate = context.Request.Form["StartDate"];
            string EndDate = context.Request.Form["EndDate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddNotification> trans = new List<AddNotification>();

            AddNotification w = new AddNotification();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = StartDate;
            w.EndDate = EndDate;

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows
                     select new AddNotification
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         CreatedDatedt = row["CreatedDatedt"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         Title = row["Title"].ToString(), 
                         UpdatedDatedt = row["UpdatedDatedt"].ToString(),
                         SentStatusName = (row["SentStatus"].ToString() == "1" ? "Sent" : "Pending"),
                         ReadStatusName = (row["ReadStatus"].ToString() == "1" ? "Read" : "UnRead")
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddNotification>> objDataTableResponse = new DataTableResponse<List<AddNotification>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        // GET: AddNotification
        [HttpGet]
        [Authorize]
        public ActionResult AddNotification(string Id)
        {
            AddNotification model = new AddNotification();
            if (!String.IsNullOrEmpty(Id))
            {
                AddNotification outobject = new AddNotification();
                GetNotification inobject = new GetNotification();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetNotification, AddNotification>.GetRecord(Common.StoreProcedures.sp_Notification_Get, inobject, outobject);
                if (model == null && model.Id == 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // Post: AddNotification
        [HttpPost]
        [Authorize]
        public ActionResult AddNotification(AddNotification model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                ViewBag.Message = "Please enter title";
            }
            else if (string.IsNullOrEmpty(model.NotificationDescription))
            {
                ViewBag.Message = "Please enter description";
            }

            AddNotification outobject = new AddNotification();
            GetNotification inobject = new GetNotification();
            if (model.Id != 0)
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddNotification res = RepCRUD<GetNotification, AddNotification>.GetRecord(Common.StoreProcedures.sp_Notification_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        res.Title = model.Title;
                        res.NotificationDescription = model.NotificationDescription;
                        res.NotificationMessage = model.NotificationMessage;
                        res.IsActive = model.IsActive;

                        if (Session["AdminMemberId"] != null)
                        {
                            res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.CreatedByName = Session["AdminUserName"].ToString();
                            bool status = RepCRUD<AddNotification, GetNotification>.Update(res, "notification");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully updated notification detail.";
                                Common.AddLogs("Updated notification Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                            }
                            else
                            {
                                ViewBag.Message = "Not Updated.";
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                AddNotification res = new AddNotification();
                res.Title = model.Title;
                res.NotificationDescription = model.NotificationDescription;
                res.NotificationMessage = model.NotificationMessage;
                res.IsActive = model.IsActive;

                if (Session["AdminMemberId"] != null)
                {
                    res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 Id = RepCRUD<AddNotification, GetNotification>.Insert(res, "notification");
                    if (Id > 0)
                    {
                        ViewBag.SuccessMessage = "Successfully Added Notification.";
                        Common.AddLogs("Addded Notification Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                    }
                    else
                    {
                        ViewBag.Message = "Not Added ! Try Again later.";
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetNotification, AddNotification>.GetRecord(Common.StoreProcedures.sp_Notification_Get, inobject, outobject);

            return View(model);
        }

    }
}