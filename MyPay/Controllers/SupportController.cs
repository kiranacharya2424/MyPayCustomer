using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class SupportController : BaseAdminSessionController
    {
        // GET: Support
        [HttpGet]
        [Authorize]
        public ActionResult Index(string ticketid, string req)
        {
            ViewBag.hdnticketid = string.IsNullOrEmpty(ticketid) ? "" : ticketid;
            ViewBag.hdnquerystring = string.IsNullOrEmpty(req) ? "" : req;
            AddTicket outobject = new AddTicket();
            GetTicketRecordDetail inobject = new GetTicketRecordDetail();
            AddTicket res = RepCRUD<GetTicketRecordDetail, AddTicket>.GetRecord(Common.StoreProcedures.sp_TicketRecordDetail_Get, inobject, outobject);
            if (res != null)
            {
                ViewBag.Totalticket = res.TotalTicket;
                ViewBag.CloseTicket = res.CloseTicket;
                ViewBag.PendingTicket = res.PendingTicket;
            }
            else
            {
                ViewBag.Totalticket = "0";
                ViewBag.CloseTicket = "0";
                ViewBag.PendingTicket = "0";
            }

            List<SelectListItem> ticketcategory = CommonHelpers.GetSelectList_TicketCategory();
            ViewBag.Category = ticketcategory;

            List<SelectListItem> ticketpriority = CommonHelpers.GetSelectList_TicketPriority();
            ViewBag.Priority = ticketpriority;

            return View();
        }

        [Authorize]
        public JsonResult GetTickets(int isSeen, int isFavourite, int isAttached, int isClosed, string TicketId, string Category, string Priority, string fromdate, string todate, string contactno, int usertype, int take = 10, int skip = 0)
        {
            Int64 MemberId = Convert.ToInt64(Session["AdminMemberId"]);
            AddTicket outobject = new AddTicket();
            GetTicket inobject = new GetTicket();
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            inobject.Take = take;
            inobject.Skip = (take * skip);
            inobject.CheckIsAttached = isAttached;
            inobject.CheckIsFavourite = isFavourite;
            inobject.CheckIsSeen = isSeen;
            //inobject.CreatedBy = MemberId;
            //if (usertype == 1)
            //{
            //    inobject.UserType = 3;
            //}
            if (isClosed != 0)
            {
                inobject.Status = isClosed;
            }
            if (!string.IsNullOrEmpty(TicketId))
            {
                inobject.TicketId = TicketId;
            }
            if (!string.IsNullOrEmpty(Category))
            {
                inobject.CategoryId = Convert.ToInt32(Category);
            }
            if (!string.IsNullOrEmpty(Priority))
            {
                inobject.Priority = Convert.ToInt32(Priority);
            }
            inobject.StartDate = fromdate;
            inobject.EndDate = todate;
            inobject.ContactNumber = contactno;


            //DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);


            //List<AddTicket> objticket = (List<AddTicket>)CommonEntityConverter.DataTableToList<AddTicket>(dt);
            List<AddTicket> objticket = RepCRUD<GetTicket, AddTicket>.GetRecordList(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);
            Int32 recordFiltered = objticket.Count;

            DataTableResponse<List<AddTicket>> objDataTableResponse = new DataTableResponse<List<AddTicket>>
            {
                //draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objticket
            };

            return Json(objDataTableResponse);
            //return RepTicket.GetTickets(MemberId, isSeen, isFavourite, isAttached, isClosed, TicketId, usertype, skip, take);

        }

        [Authorize]
        public JsonResult GetTickets_Reply(string TicketId,string Take,string Skip)
        {
            AddTicket outobject = new AddTicket();
            GetTicket inobject = new GetTicket();
            inobject.TicketId = TicketId;
            
            AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);

            if (res != null && res.Id > 0)
            {
                res.IsSeen = true;
                bool status = RepCRUD<AddTicket, GetTicket>.Update(res, "tickets");
            }
            AddTicketsReply outobject_rec = new AddTicketsReply();
            GetTicketsReply inobject_rec = new GetTicketsReply();
            inobject_rec.CheckActive = 1;
            inobject_rec.CheckDelete = 0;
            if (!string.IsNullOrEmpty(TicketId))
            {
                inobject_rec.TicketId = TicketId;
            }
            inobject_rec.Take = Convert.ToInt32(Take);
            inobject_rec.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
            List<AddTicketsReply> objticket = RepCRUD<GetTicketsReply, AddTicketsReply>.GetRecordList(Common.StoreProcedures.sp_TicketsReply_Get, inobject_rec, outobject_rec);
            Int32 recordFiltered = objticket.Count;

            DataTableResponse<List<AddTicketsReply>> objDataTableResponse = new DataTableResponse<List<AddTicketsReply>>
            {
                //draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objticket
            };
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            //Dictionary<string, object> row = new Dictionary<string, object>();
            //if (objDataTableResponse != null && objDataTableResponse.data.Count > 0)
            //{
            //    foreach (DataRow dr in objDataTableResponse.data.)
            //    {
            //        row = new Dictionary<string, object>();
            //        row.Add("status", "Success");
            //        row.Add("message", "List Found");
            //        foreach (DataColumn col in dtrec.Columns)
            //        {
            //            if (col.ColumnName == "CreatedDate")
            //            {
            //                row.Add(col.ColumnName, Convert.ToDateTime(dr[col]).ToString("dddd, dd MMMM yyyy HH:mm"));
            //            }
            //            else
            //            {
            //                row.Add(col.ColumnName, dr[col]);
            //            }

            //        }
            //        rows.Add(row);
            //    }
            //    //return serializer.Serialize(rows);
            //}

            return Json(objDataTableResponse);
        }

        [Authorize]
        [HttpPost]
        public JsonResult AddTicketDetail(string TicketId, string message, string filename, string isnote)
        {
            if (string.IsNullOrEmpty(message))
            {
                ViewBag.Message = "Please Enter Message";
            }
            else if (string.IsNullOrEmpty(TicketId))
            {
                ViewBag.Message = "Ticket Id Not Found";
            }
            if (ViewBag.Message == null)
            {
                AddTicketsReply outobject_rec = new AddTicketsReply();
                GetTicketsReply inobject_rec = new GetTicketsReply();

                AddTicket outobject = new AddTicket();
                GetTicket inobject = new GetTicket();
                //Tickets tg = new Tickets();
                inobject.TicketId = TicketId;
                AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);

                if (res != null && res.Id > 0)
                {
                    res.AssignedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.AssignedTo = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedMessage = message;
                    res.UpdatedDate = DateTime.UtcNow;
                    if (!string.IsNullOrEmpty(filename))
                    {
                        res.IsAttached = true;
                        outobject_rec.AttachFile = filename;
                    }
                    bool status = RepCRUD<AddTicket, GetTicket>.Update(res, "tickets");
                    if (status)
                    {
                        outobject_rec.IsActive = true;
                        if (isnote == "1")
                        {
                            outobject_rec.IsNote = true;
                        }
                        else
                        {
                            outobject_rec.IsNote = false;
                        }

                        outobject_rec.Name = Session["AdminUserName"].ToString();
                        outobject_rec.TicketId = res.TicketId;
                        outobject_rec.Type = (int)AddTicketsReply.Types.Reciever;
                        outobject_rec.Title = res.TicketTitle;
                        outobject_rec.Message = message;
                        outobject_rec.IsAdmin = true;
                        outobject_rec.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        outobject_rec.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 id_reply = RepCRUD<AddTicketsReply, GetTicketsReply>.Insert(outobject_rec, "ticketsreply");
                        if (id_reply > 0)
                        {
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Ticket;
                            RepTicket.AddTicketImages("", filename, res.Id, id_reply, true, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());

                            AddUser outobject_user = new AddUser();
                            GetUser inobject_user = new GetUser();
                            inobject_user.ContactNumber = res.ContactNumber;
                            AddUser res_user = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject_user, outobject_user);

                            if (res_user != null && res_user.Id > 0)
                            {
                                if (!outobject_rec.IsNote)
                                {
                                    Common.SendNotification("", VendorAPIType, res_user.MemberId, "Ticket Status", "Admin Added new comment on ticket Id:" + res.TicketId);
                                }
                            }
                            Common.AddLogs("Admin Add Ticket Reply, TicketId : " + res.TicketId + " by (AdminId" + Session["AdminMemberId"] + ")", true, Convert.ToInt32(AddLog.LogType.Ticket), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false);
                            ViewBag.Message = "success";
                        }
                        else
                        {
                            ViewBag.Message = "Ticket reply not sent.";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Ticket reply not sent.";
                    }
                }
                else
                {
                    ViewBag.Message = "Ticket Id Not Found";
                }
            }
            return Json(ViewBag.Message);
        }

        [Authorize]
        public JsonResult UpdateFavourite(string TicketId, int Status)
        {
            AddTicket outobject = new AddTicket();
            GetTicket inobject = new GetTicket();
            inobject.TicketId = TicketId;
            AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);
            if (res != null && res.Id > 0)
            {
                res.IsFavourite = Convert.ToBoolean(Status);
                bool status = RepCRUD<AddTicket, GetTicket>.Update(res, "tickets");
                if (status)
                {
                    ViewBag.SuccessMessage = "Ticket is set as favourite.";
                    Common.AddLogs("Ticket (Id:" + TicketId + ") is set as favourite by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Ticket);

                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult MarkClosed(string TicketId)
        {
            AddTicket outobject = new AddTicket();
            GetTicket inobject = new GetTicket();
            inobject.TicketId = TicketId;
            AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);
            if (res != null && res.Id > 0)
            {
                res.Status = (int)AddTicket.TicketStatus.Close;
                res.CloseDate = DateTime.UtcNow;
                res.UpdatedDate = DateTime.UtcNow;
                bool status = RepCRUD<AddTicket, GetTicket>.Update(res, "tickets");
                if (status)
                {
                    ViewBag.SuccessMessage = "Ticket Resolved";
                    Common.AddLogs("Ticket (Id:" + TicketId + ") is resolved by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Ticket);

                }
                AddUser outobject_user = new AddUser();
                GetUser inobject_user = new GetUser();
                inobject_user.ContactNumber = res.ContactNumber;
                AddUser res_user = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject_user, outobject_user);

                if (res_user != null && res_user.Id > 0)
                {
                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Ticket;
                    Common.SendNotification("", VendorAPIType, res_user.MemberId, "Ticket Resolved", "Ticket status updated to resolved ticket Id:" + res.TicketId);
                }

            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult SetPriority(string TicketId, int Priority)
        {

            AddTicket outobject = new AddTicket();
            GetTicket inobject = new GetTicket();
            inobject.TicketId = TicketId;
            AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);
            if (res != null && res.Id > 0)
            {
                res.Priority = Priority;
                bool status = RepCRUD<AddTicket, GetTicket>.Update(res, "tickets");
                if (status)
                {
                    ViewBag.SuccessMessage = "Ticket Priority Changed";
                    Common.AddLogs("Ticket (Id:" + TicketId + ") Priority Changed by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Ticket);
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public JsonResult GetUserDetail(string contactno)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = new Dictionary<string, object>();
            try
            {
                if (Session["AdminMemberId"] == null || string.IsNullOrEmpty(Session["AdminMemberId"].ToString()))
                {
                    row.Add("status", "Error");
                    row.Add("message", "Session Expired");
                    rows.Add(row);
                }
                else
                {
                    AddUser outobject_user = new AddUser();
                    GetUser inobject_user = new GetUser();
                    inobject_user.ContactNumber = contactno;
                    AddUser res_user = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject_user, outobject_user);

                    if (res_user != null && res_user.Id > 0)
                    {
                        row = new Dictionary<string, object>();
                        row.Add("status", "Success");
                        row.Add("message", "Data Found");
                        row.Add("UserName", res_user.FirstName + " " + res_user.LastName);
                        row.Add("Email", res_user.Email);
                        row.Add("MemberId", res_user.MemberId);
                        row.Add("Phone", res_user.ContactNumber);
                        row.Add("RefId", res_user.RefId);
                        row.Add("Address", res_user.Address);
                        row.Add("State", res_user.State + " " + res_user.City + "," + res_user.ZipCode);
                        row.Add("Platform", res_user.PlatForm);

                        AddTicket outobject = new AddTicket();
                        GetTicketRecordDetail inobject = new GetTicketRecordDetail();
                        inobject.CreatedBy = res_user.MemberId;
                        AddTicket res = RepCRUD<GetTicketRecordDetail, AddTicket>.GetRecord(Common.StoreProcedures.sp_TicketRecordDetail_Get, inobject, outobject);
                        if (res != null)
                        {
                            row.Add("TotalTickets", res.TotalTicket);
                            row.Add("CloseTickets", res.CloseTicket);
                            row.Add("PendingTickets", res.PendingTicket);                            
                        }
                        rows.Add(row);
                    }
                    else
                    {
                        row.Add("status", "Error");
                        row.Add("message", "Data Not Found");
                        rows.Add(row);
                    }
                }

            }
            catch (Exception ex)
            {
                row.Add("status", "Error");
                row.Add("message", ex.Message);
                rows.Add(row);
            }
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddTicketCategory(string Id)
        {
            AddTicketCategory model = new AddTicketCategory();
            if (!String.IsNullOrEmpty(Id))
            {
                AddTicketCategory outobject = new AddTicketCategory();
                GetTicketCategory inobject = new GetTicketCategory();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecord(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddTicketCategory(AddTicketCategory model)
        {
            AddTicketCategory outobject = new AddTicketCategory();
            GetTicketCategory inobject = new GetTicketCategory();
            if (model.Id != 0)
            {
                inobject.Id = Convert.ToInt64(model.Id);
                AddTicketCategory res = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecord(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (string.IsNullOrEmpty(model.CategoryName))
                    {
                        ViewBag.Message = "Please enter category name";
                        return View(model);
                    }
                    res.CategoryName = model.CategoryName;
                    res.IsActive = model.IsActive;
                    res.UpdatedDate = DateTime.UtcNow;

                    if (Session["AdminMemberId"] != null)
                    {
                        res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddTicketCategory, GetTicketCategory>.Update(res, "ticketscategory");
                        if (status)
                        {
                            ViewBag.SuccessMessage = "Successfully Updated Ticket Category Detail.";
                            Common.AddLogs("Updated Ticket Category Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Ticket);
                        }
                        else
                        {
                            ViewBag.Message = "Not Updated.";
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "No Record Found";
                    return View(model);
                }
            }
            else
            {
                AddTicketCategory res = new AddTicketCategory();
                if (string.IsNullOrEmpty(model.CategoryName))
                {
                    ViewBag.Message = "Please enter category name";
                    return View(model);
                }
                res.CategoryName = model.CategoryName;
                res.IsActive = model.IsActive;

                if (Session["AdminMemberId"] != null)
                {
                    res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 Id = RepCRUD<AddTicketCategory, GetTicketCategory>.Insert(res, "ticketscategory");
                    if (Id > 0)
                    {
                        ViewBag.SuccessMessage = "Successfully Added Ticket Category.";
                        Common.AddLogs("Added Ticket Category Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Ticket);
                    }
                    else
                    {
                        ViewBag.Message = "Not Added ! Try Again later.";
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecord(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult TicketCategoryList()
        {
            AddTicketCategory outobject = new AddTicketCategory();
            GetTicketCategory inobject = new GetTicketCategory();
            List<AddTicketCategory> objList = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecordList(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);
            Req_Web_TicketCategory model = new Req_Web_TicketCategory();
            model.objData = objList;
            return View(model);
        }

        // Post: OccupationList
        [HttpPost]
        [Authorize]
        public ActionResult TicketCategoryList(Req_Web_TicketCategory model)
        {
            AddTicketCategory outobject = new AddTicketCategory();
            GetTicketCategory inobject = new GetTicketCategory();
            List<AddTicketCategory> objList = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecordList(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);
            model.objData = objList;
            return View(model);
        }

        //TicketCategoryBlockUnblock
        [HttpPost]
        [Authorize]
        public JsonResult TicketCategoryBlockUnblock(AddTicketCategory model)
        {
            AddTicketCategory outobject = new AddTicketCategory();
            GetTicketCategory inobject = new GetTicketCategory();
            inobject.Id = model.Id;
            AddTicketCategory res = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecord(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (res.IsActive)
                {
                    res.IsActive = false;
                }
                else
                {
                    res.IsActive = true;
                }
                bool IsUpdated = RepCRUD<AddTicketCategory, GetTicketCategory>.Update(res, "ticketscategory");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully update ticket category";
                    Common.AddLogs("Updated ticket category(Id:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Ticket);
                }
                else
                {
                    ViewBag.Message = "Not Updated ticket category";
                    Common.AddLogs("Not Updated ticket category", true, (int)AddLog.LogType.Ticket);
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SupportReplyCount(string Id)
        {
            var result = string.Empty;
            try
            {
                AddTicketsReply outobject = new AddTicketsReply();
                result = outobject.TotalTicketReplyCount(Id).ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
    }
}