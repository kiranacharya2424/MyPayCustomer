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
    public class KYCRemarksController : BaseAdminSessionController
    {
        // GET: KYCRemarks
        [HttpGet]
        [Authorize]
        public ActionResult Index(string Id)
        {
            AddKYCRemarks model = new AddKYCRemarks();
            if (!String.IsNullOrEmpty(Id))
            {
                AddKYCRemarks outobject = new AddKYCRemarks();
                GetKYCRemarks inobject = new GetKYCRemarks();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetKYCRemarks, AddKYCRemarks>.GetRecord(Common.StoreProcedures.sp_KYCRemarks_Get, inobject, outobject);
            }
            return View(model);
        }

        // Post: AddKYCRemarks
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddKYCRemarks model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                ViewBag.Message = "Please enter title";
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                ViewBag.Message = "Please enter Description";
            }

            AddKYCRemarks outobject = new AddKYCRemarks();
            GetKYCRemarks inobject = new GetKYCRemarks();
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                if (model.Id != 0)
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddKYCRemarks res = RepCRUD<GetKYCRemarks, AddKYCRemarks>.GetRecord(Common.StoreProcedures.sp_KYCRemarks_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        res.Title = model.Title;
                        res.Description = model.Description;
                        res.IsActive = model.IsActive;

                        if (Session["AdminMemberId"] != null)
                        {
                            res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddKYCRemarks, GetKYCRemarks>.Update(res, "kycremarks");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully updated kyc remarks detail.";
                                Common.AddLogs("Updated kyc remarks(id" + res.Id + ") Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Kyc);
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
                else
                {
                    AddKYCRemarks res = new AddKYCRemarks();

                    res.Title = model.Title;
                    res.Description = model.Description;
                    res.IsActive = model.IsActive;
                    if (Session["AdminMemberId"] != null)
                    {
                        res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddKYCRemarks, GetKYCRemarks>.Insert(res, "kycremarks");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully added kyc remarks detail.";
                            Common.AddLogs("Addded kyc remarks Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Kyc);
                        }
                        else
                        {
                            ViewBag.Message = "Not Added ! Try Again later.";
                        }
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetKYCRemarks, AddKYCRemarks>.GetRecord(Common.StoreProcedures.sp_KYCRemarks_Get, inobject, outobject);

            return View(model);
        }

        // GET: KYCRemarksList
        [HttpGet]
        [Authorize]
        public ActionResult KYCRemarksList()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetKYCRemarksList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("Title");
            columns.Add("CreatedBy");  
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
            GetTransaction inobject_Trans = new GetTransaction();

            List<AddKYCRemarks> trans = new List<AddKYCRemarks>();

            GetKYCRemarks w = new GetKYCRemarks();
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddKYCRemarks
                     {
                         Sno = row["Sno"].ToString(),
                         Id=Convert.ToInt32(row["Id"]),
                         Title = row["Title"].ToString(),
                         Description = row["Description"].ToString(),
                         CreatedByName= row["CreatedByName"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         IsActive=Convert.ToBoolean(row["IsActive"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddKYCRemarks>> objDataTableResponse = new DataTableResponse<List<AddKYCRemarks>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        //KYCRemarksEnableDisable
        [HttpPost]
        public JsonResult KYCRemarksBlockUnblock(AddKYCRemarks model)
        {
            AddKYCRemarks outobject = new AddKYCRemarks();
            GetKYCRemarks inobject = new GetKYCRemarks();
            inobject.Id = model.Id;
            AddKYCRemarks res = RepCRUD<GetKYCRemarks, AddKYCRemarks>.GetRecord(Common.StoreProcedures.sp_KYCRemarks_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddKYCRemarks, GetKYCRemarks>.Update(res, "kycremarks");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update kyc remarks";
                    Common.AddLogs("Updated kyc remarks by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Kyc);

                }
                else
                {
                    ViewBag.Message = "Not Updated kyc remarks";
                    Common.AddLogs("Not Updated kyc remarks by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Kyc);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}