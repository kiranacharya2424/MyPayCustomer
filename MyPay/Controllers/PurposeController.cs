using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class PurposeController : BaseAdminSessionController
    {
        // GET: AddPurpose
        [HttpGet]
        [Authorize]
        public ActionResult Index(string Id)
        {
            AddPurpose model = new AddPurpose();
            if (!String.IsNullOrEmpty(Id))
            {
                AddPurpose outobject = new AddPurpose();
                GetPurpose inobject = new GetPurpose();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetPurpose, AddPurpose>.GetRecord(Common.StoreProcedures.sp_Purpose_Get, inobject, outobject);
            }
            return View(model);
        }

        // Post: AddPurpose
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddPurpose model)
        {
            if (string.IsNullOrEmpty(model.CategoryName))
            {
                ViewBag.Message = "Please enter title";
            }

            AddPurpose outobject = new AddPurpose();
            GetPurpose inobject = new GetPurpose();
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                if (model.Id != 0)
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddPurpose res = RepCRUD<GetPurpose, AddPurpose>.GetRecord(Common.StoreProcedures.sp_Purpose_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        res.CategoryName = model.CategoryName;
                        res.IsActive = model.IsActive;

                        if (Session["AdminMemberId"] != null)
                        {
                            res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.CreatedByName = Session["AdminUserName"].ToString();
                            bool status = RepCRUD<AddPurpose, GetPurpose>.Update(res, "purpose");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully updated purpose detail.";
                                Common.AddLogs("Updated purpose(id" + res.Id + ") Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
                    AddPurpose res = new AddPurpose();

                    res.CategoryName = model.CategoryName;
                    res.IsActive = model.IsActive;
                    if (Session["AdminMemberId"] != null)
                    {
                        res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddPurpose, GetRedeemPoints>.Insert(res, "purpose");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully added purpose detail.";
                            Common.AddLogs("Addded purpose Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                        }
                        else
                        {
                            ViewBag.Message = "Not Added ! Try Again later.";
                        }
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetPurpose, AddPurpose>.GetRecord(Common.StoreProcedures.sp_Purpose_Get, inobject, outobject);

            return View(model);
        }

        // GET:PurposeList
        [HttpGet]
        [Authorize]
        public ActionResult PurposeList()
        {
            AddPurpose outobject = new AddPurpose();
            GetPurpose inobject = new GetPurpose();
            List<AddPurpose> objList = RepCRUD<GetPurpose, AddPurpose>.GetRecordList(Common.StoreProcedures.sp_Purpose_Get, inobject, outobject);
            Req_Web_Purpose model = new Req_Web_Purpose();
            model.objData = objList;
            return View(model);
        }

        // Post: PurposeList
        [HttpPost]
        [Authorize]
        public ActionResult PurposeList(Req_Web_Purpose model)
        {
            AddPurpose outobject = new AddPurpose();
            GetPurpose inobject = new GetPurpose();
            List<AddPurpose> objList = RepCRUD<GetPurpose, AddPurpose>.GetRecordList(Common.StoreProcedures.sp_Purpose_Get, inobject, outobject);
            model.objData = objList;
            return View(model);
        }

        //PurposeBlockUnblock
        [HttpPost]
        [Authorize]
        public JsonResult PurposeBlockUnblock(AddPurpose model)
        {
            AddPurpose outobject = new AddPurpose();
            GetPurpose inobject = new GetPurpose();
            inobject.Id = model.Id;
            AddPurpose res = RepCRUD<GetPurpose, AddPurpose>.GetRecord(Common.StoreProcedures.sp_Purpose_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddPurpose, GetPurpose>.Update(res, "purpose");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully update purpose";
                    Common.AddLogs("Updated purpose(Id:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                }
                else
                {
                    ViewBag.Message = "Not Updated purpose";
                    Common.AddLogs("Not Updated purpose", true, (int)AddLog.LogType.User);
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}