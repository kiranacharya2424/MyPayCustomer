using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class RedeemPointsController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        // GET: RedeemPoints
        public ActionResult Index(string Id)
        {
            AddRedeemPoints model = new AddRedeemPoints();
            if (!String.IsNullOrEmpty(Id))
            {
                AddRedeemPoints outobject = new AddRedeemPoints();
                GetRedeemPoints inobject = new GetRedeemPoints();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetRedeemPoints, AddRedeemPoints>.GetRecord(Common.StoreProcedures.sp_RedeemPoints_Get, inobject, outobject);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(AddRedeemPoints model, HttpPostedFileBase Image)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                ViewBag.Message = "Please enter title";
                return View(model);
            }
            if (model.Amount == 0)
            {
                ViewBag.Message = "Please enter amount";
                return View(model);
            }
            if (model.Points == 0)
            {
                ViewBag.Message = "Please enter points";
                return View(model);
            }
            AddRedeemPoints outobject = new AddRedeemPoints();
            GetRedeemPoints inobject = new GetRedeemPoints();
            if (model.Id != 0)
            {
                inobject.Id = Convert.ToInt64(model.Id);
                AddRedeemPoints res = RepCRUD<GetRedeemPoints, AddRedeemPoints>.GetRecord(Common.StoreProcedures.sp_RedeemPoints_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {                    
                    res.Title = model.Title;
                    res.Amount = model.Amount;
                    res.Points = model.Points;
                    res.IsActive = model.IsActive;
                    res.TermsAndConditions = model.TermsAndConditions;

                    if (Image != null)
                    {
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/RedeemPointImages/") + fileName);
                        Image.SaveAs(filePath);
                        res.Image = fileName;
                    }

                    if (Session["AdminMemberId"] != null)
                    {
                        res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddRedeemPoints, GetRedeemPoints>.Update(res, "redeempoints");
                        if (status)
                        {
                            ViewBag.SuccessMessage = "Successfully Updated.";
                            Common.AddLogs("Updated Redeem points Detail(Id:"+res.Id+ ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                        }
                        else
                        {
                            ViewBag.Message = "Not Updated.";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Not Updated.";
                    }
                }
            }
            else
            {
                AddRedeemPoints res = new AddRedeemPoints();
                res.Title = model.Title;
                res.Amount = model.Amount;
                res.Points = model.Points;
                res.IsActive = model.IsActive;
                res.TermsAndConditions = model.TermsAndConditions;

                if (Image != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/RedeemPointImages/") + fileName);
                    Image.SaveAs(filePath);
                    res.Image = fileName;
                }
                if (Session["AdminMemberId"] != null)
                {
                    res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 Id = RepCRUD<AddRedeemPoints, GetRedeemPoints>.Insert(res, "redeempoints");
                    if (Id > 0)
                    {
                        ViewBag.SuccessMessage = "Successfully Added.";
                        Common.AddLogs("Addded Redeem Points Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                    }
                    else
                    {
                        ViewBag.Message = "Not Added ! Try Again later.";
                    }
                }
                else
                {
                    ViewBag.Message = "Not Added ! Try Again later.";
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetRedeemPoints, AddRedeemPoints>.GetRecord(Common.StoreProcedures.sp_RedeemPoints_Get, inobject, outobject);

            return View(model);
        }


        [HttpGet]
        [Authorize]
        // GET: RedeemPointsList
        public ActionResult RedeemPointsList()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetRedeemPointsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Title");
            columns.Add("Amount");
            columns.Add("Points");
            columns.Add("Created By");
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

            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Title = context.Request.Form["Title"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRedeemPoints> trans = new List<AddRedeemPoints>();
            GetRedeemPoints w = new GetRedeemPoints();
            w.Title = Title;
            w.StartDate = fromdate;
            w.EndDate = todate;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRedeemPoints
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         Title = row["Title"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         Points = Convert.ToInt64(row["Points"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         IsActive= Convert.ToBoolean(row["IsActive"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRedeemPoints>> objDataTableResponse = new DataTableResponse<List<AddRedeemPoints>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        public JsonResult RedeemPointsBlockUnblock(AddRedeemPoints model)
        {

            AddRedeemPoints outobject = new AddRedeemPoints();
            GetRedeemPoints inobject = new GetRedeemPoints();
            inobject.Id = model.Id;
            AddRedeemPoints res = RepCRUD<GetRedeemPoints, AddRedeemPoints>.GetRecord(Common.StoreProcedures.sp_RedeemPoints_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddRedeemPoints, GetRedeemPoints>.Update(res, "redeempoints");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update redeem points";
                    Common.AddLogs("Updated redeem points by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);

                }
                else
                {
                    ViewBag.Message = "Not Updated redeem points";
                    Common.AddLogs("Not Updated redeem points", true, (int)AddLog.LogType.User);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}