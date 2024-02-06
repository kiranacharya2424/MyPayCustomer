using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class ReferEarnImageController : BaseAdminSessionController
    {
        // GET: ReferEarnImage
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            AddReferEarnImage outobject = new AddReferEarnImage();
            GetReferEarnImage inobject = new GetReferEarnImage();
            List<AddReferEarnImage> objList = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecordList(Common.StoreProcedures.sp_ReferEarnImage_Get, inobject, outobject);
            Req_Web_ReferEarnImage model = new Req_Web_ReferEarnImage();
            model.objData = objList;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(Req_Web_ReferEarnImage model)
        {
            AddReferEarnImage outobject = new AddReferEarnImage();
            GetReferEarnImage inobject = new GetReferEarnImage();
            List<AddReferEarnImage> objList = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecordList(Common.StoreProcedures.sp_ReferEarnImage_Get, inobject, outobject);
            model.objData = objList;
            return View(model);
        }


        [HttpGet]
        [Authorize]
        public ActionResult ReferEarnImageAdd(string Id)
        {
            AddReferEarnImage model = new AddReferEarnImage();
            if (!String.IsNullOrEmpty(Id))
            {
                AddReferEarnImage outobject = new AddReferEarnImage();
                GetReferEarnImage inobject = new GetReferEarnImage();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecord(Common.StoreProcedures.sp_ReferEarnImage_Get, inobject, outobject);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReferEarnImageAdd(AddReferEarnImage model, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                AddReferEarnImage outobject = new AddReferEarnImage();
                GetReferEarnImage inobject = new GetReferEarnImage();
                if (model.Id != 0)
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddReferEarnImage res = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecord(Common.StoreProcedures.sp_ReferEarnImage_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {

                        res.DisplayCashbackAmount = model.DisplayCashbackAmount;
                        res.CashbackAmount = model.CashbackAmount;
                        res.Image = model.Image;
                        res.IsActive = model.IsActive;
                        res.ContentText = model.ContentText;

                        if (Image == null && (model.Image == "" || model.Image == null))
                        {
                            ViewBag.Message = "Please upload image";
                            return View(model);
                        }

                        if (Image != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/ReferEarnImages/") + fileName);
                            Image.SaveAs(filePath);
                            res.Image = fileName;
                        }

                        if (Session["AdminMemberId"] != null)
                        {
                            res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddReferEarnImage, GetReferEarnImage>.Update(res, "referearnimage");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully Updated ReferEarnImage Detail.";
                                Common.AddLogs("Updated ReferEarnImage Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
                inobject.Id = Convert.ToInt64(model.Id);
                model = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecord(Common.StoreProcedures.sp_ReferEarnImage_Get, inobject, outobject);
                return View(model);
            }
            else
            {
                ViewBag.Message = "Not Updated.";
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult ReferEarnImageBlockUnblock(AddReferEarnImage model)
        {

            AddReferEarnImage outobject = new AddReferEarnImage();
            GetReferEarnImage inobject = new GetReferEarnImage();
            inobject.Id = model.Id;
            AddReferEarnImage res = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecord(Common.StoreProcedures.sp_ReferEarnImage_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddReferEarnImage, GetReferEarnImage>.Update(res, "referearnimage");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update ReferEarnImage";
                    Common.AddLogs("Updated ReferEarnImage by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);

                }
                else
                {
                    ViewBag.Message = "Not Updated ReferEarnImage";
                    Common.AddLogs("Not Updated ReferEarnImage", true, (int)AddLog.LogType.User);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}