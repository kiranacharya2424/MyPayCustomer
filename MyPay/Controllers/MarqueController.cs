using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;

namespace MyPay.Controllers
{
    public class MarqueController : BaseAdminSessionController
    {
        // GET: OfferBanners
        [HttpGet]
        [Authorize]
        public ActionResult Index(string Id)
        {
            AddMarque model = new AddMarque();
            if (!String.IsNullOrEmpty(Id))
            {

                AddMarque outobject = new AddMarque();
                GetMarque inobject = new GetMarque();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetMarque, AddMarque>.GetRecord(Common.StoreProcedures.sp_Marque_Get, inobject, outobject);

            }


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Priority",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "1",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "2",
                Value = "2"
            });
            items.Add(new SelectListItem
            {
                Text = "3",
                Value = "3"
            });
            items.Add(new SelectListItem
            {
                Text = "4",
                Value = "4"
            });
            items.Add(new SelectListItem
            {
                Text = "5",
                Value = "5"
            });
            ViewBag.Priority = items;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(AddMarque model)
        {
            //if (string.IsNullOrEmpty(model.Title))
            //{
            //    ViewBag.Message = "Please enter title";
            //}
            if (string.IsNullOrEmpty(model.Description))
            {
                ViewBag.Message = "Please enter Description";
            }
            if (model.Priority == 0)
            {
                ViewBag.Message = "Please Select  Priority";
            }
            AddMarque outobject = new AddMarque();
            GetMarque inobject = new GetMarque();

            //if (model.Id != 0)
            //{
            int Marquefor = Convert.ToInt32(model.MarqueFor);
            if (model.Priority != 0)
            {
                inobject.Priority = model.Priority;
                inobject.MarqueFor = model.MarqueFor;
            }

            AddMarque res = RepCRUD<GetMarque, AddMarque>.GetRecord(Common.StoreProcedures.sp_Marque_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    res.Title = model.Title;
                    res.Description = model.Description;
                    res.IsActive = model.IsActive;
                    res.Priority = model.Priority;
                    res.MarqueFor = model.MarqueFor;
                    res.Link = model.Link;

                    if (Session["AdminMemberId"] != null)
                    {
                        res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.UpdatedByName = Session["AdminUserName"].ToString();
                        bool status = RepCRUD<AddMarque, GetMarque>.Update(res, "Marque");
                        if (status)
                        {
                            ViewBag.SuccessMessage = "Successfully Updated.";
                            Common.AddLogs("Updated Marque Detail(Id:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    AddMarque resAdd = new AddMarque();
                    resAdd.Title = model.Title;
                    resAdd.Description = model.Description;
                    resAdd.IsActive = model.IsActive;
                    resAdd.Priority = model.Priority;
                    res.MarqueFor = model.MarqueFor;
                    res.Link = model.Link;

                    if (Session["AdminMemberId"] != null)
                    {
                        res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddMarque, GetMarque>.Insert(resAdd, "Marque");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully Added.";
                            Common.AddLogs("Addded Marquee Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetMarque, AddMarque>.GetRecord(Common.StoreProcedures.sp_Marque_Get, inobject, outobject);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Priority",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "1",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "2",
                Value = "2"
            });
            items.Add(new SelectListItem
            {
                Text = "3",
                Value = "3"
            });
            items.Add(new SelectListItem
            {
                Text = "4",
                Value = "4"
            });
            items.Add(new SelectListItem
            {
                Text = "5",
                Value = "5"
            });
            ViewBag.Priority = items;
            return View(model);

        }


        [HttpGet]
        [Authorize]

        public ActionResult MarqueList()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetMarqueLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Title");
            columns.Add("Description");
            columns.Add("Link");
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
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMarque> trans = new List<AddMarque>();
            GetMarque M = new GetMarque();

            DataTable dt = M.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMarque
                     {

                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         Title = row["Title"].ToString(),
                         Description = row["Description"].ToString(),
                         Link = row["Link"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         UpdatedByName = row["UpdatedByName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMarque>> objDataTableResponse = new DataTableResponse<List<AddMarque>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }
        [HttpPost]
        public JsonResult MarqueBlockUnblock(AddMarque model)
        {
            int isactive;
            AddMarque outobject = new AddMarque();
            GetMarque inobject = new GetMarque();
            inobject.Id = model.Id;

            //if (model.IsActive)
            //{
            //    isactive = 1;
            //}
            //else
            //{
            //    isactive = 0;
            //}
            AddMarque res = RepCRUD<GetMarque, AddMarque>.GetRecord(Common.StoreProcedures.sp_Marque_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (model.IsActive)
                {
                    res.IsActive = false;
                }
                else
                {
                    res.IsActive = true;
                }
                bool IsUpdated = RepCRUD<AddMarque, GetMarque>.Update(res, "marque");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update marque";
                    Common.AddLogs("Updated marque by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);

                }
                else
                {
                    ViewBag.Message = "Not Updated Marque";
                    Common.AddLogs("Not Updated Marque by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }


    }
}