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
    public class OccupationController : BaseAdminSessionController
    {
        // GET: AddOccupation
        [HttpGet]
        [Authorize]
        public ActionResult Index(string Id)
        {
            AddOccupation model = new AddOccupation();
            if (!String.IsNullOrEmpty(Id))
            {
                AddOccupation outobject = new AddOccupation();
                GetOccupation inobject = new GetOccupation();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetOccupation, AddOccupation>.GetRecord(Common.StoreProcedures.sp_Occupation_Get, inobject, outobject);
            }
            return View(model);
        }

        // Post: AddOccupation
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddOccupation model)
        {
            if (string.IsNullOrEmpty(model.CategoryName))
            {
                ViewBag.Message = "Please enter title";                
            }

            AddOccupation outobject = new AddOccupation();
            GetOccupation inobject = new GetOccupation();
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                if (model.Id != 0)
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddOccupation res = RepCRUD<GetOccupation, AddOccupation>.GetRecord(Common.StoreProcedures.sp_Occupation_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        res.CategoryName = model.CategoryName;
                        res.IsActive = model.IsActive;

                        if (Session["AdminMemberId"] != null)
                        {
                            res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.CreatedByName = Session["AdminUserName"].ToString();
                            bool status = RepCRUD<AddOccupation, GetOccupation>.Update(res, "occupation");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully Updated occupation Detail.";
                                Common.AddLogs("Updated occupation(id"+res.Id+ ") Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
                    AddOccupation res = new AddOccupation();

                    res.CategoryName = model.CategoryName;
                    res.IsActive = model.IsActive;
                    if (Session["AdminMemberId"] != null)
                    {
                        res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddOccupation, GetOccupation>.Insert(res, "occupation");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully Added occupation detail.";
                            Common.AddLogs("Addded occupation Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                        }
                        else
                        {
                            ViewBag.Message = "Not Added ! Try Again later.";
                        }
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetOccupation, AddOccupation>.GetRecord(Common.StoreProcedures.sp_Occupation_Get, inobject, outobject);

            return View(model);
        }

        // GET:OccupationList
        [HttpGet]
        [Authorize]
        public ActionResult OccupationList()
        {
            AddOccupation outobject = new AddOccupation();
            GetOccupation inobject = new GetOccupation();
            List<AddOccupation> objList = RepCRUD<GetOccupation, AddOccupation>.GetRecordList(Common.StoreProcedures.sp_Occupation_Get, inobject, outobject);
            Req_Web_Occupation model = new Req_Web_Occupation();
            model.objData = objList;
            return View(model);
        }

        // Post: OccupationList
        [HttpPost]
        [Authorize]
        public ActionResult OccupationList(Req_Web_Occupation model)
        {
            AddOccupation outobject = new AddOccupation();
            GetOccupation inobject = new GetOccupation();
            List<AddOccupation> objList = RepCRUD<GetOccupation, AddOccupation>.GetRecordList(Common.StoreProcedures.sp_Occupation_Get, inobject, outobject);
            model.objData = objList;
            return View(model);
        }

        //OccupationBlockUnblock
        [HttpPost]
        [Authorize]
        public JsonResult OccupationBlockUnblock(AddOccupation model)
        {
            AddOccupation outobject = new AddOccupation();
            GetOccupation inobject = new GetOccupation();
            inobject.Id = model.Id;
            AddOccupation res = RepCRUD<GetOccupation, AddOccupation>.GetRecord(Common.StoreProcedures.sp_Occupation_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddOccupation, GetVotingPackages>.Update(res, "occupation");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully update occupation";
                    Common.AddLogs("Updated occupation(Id:"+res.Id+ ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                }
                else
                {
                    ViewBag.Message = "Not Updated occupation";
                    Common.AddLogs("Not Updated occupation", true, (int)AddLog.LogType.User);
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}