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
    public class OfferBannersController : BaseAdminSessionController
    {
        // GET: OfferBanners
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            AddOfferBanners outobject = new AddOfferBanners();
            GetOfferBanners inobject = new GetOfferBanners();
            List<AddOfferBanners> objList = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecordList(Common.StoreProcedures.sp_OfferBanners_Get, inobject, outobject);
            Req_Web_OfferBanners model = new Req_Web_OfferBanners();
                model.objData = objList;
            
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(Req_Web_OfferBanners model)
        {
            AddOfferBanners outobject = new AddOfferBanners();
            GetOfferBanners inobject = new GetOfferBanners();
            if (!string.IsNullOrEmpty(model.Name))
            {
                inobject.Name = model.Name;
            }
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                inobject.CheckFromDate = model.FromDate;
            }
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                inobject.CheckToDate = model.ToDate;
            }
            int type= Convert.ToInt32(model.EnumTypeProviders);
            if(type!=0)
            {
                inobject.Type = type;
            }
            int ScheduleStatus = Convert.ToInt32(model.EnumScheduleStatus);
            if (ScheduleStatus != 0)
            {
                if (ScheduleStatus == 1)
                {
                    inobject.Running = "Running";
                }
                else if (ScheduleStatus == 2)
                {
                    inobject.Scheduled = "Scheduled";
                }
                else if (ScheduleStatus == 3)
                {
                    inobject.Expired = "Expired";
                }
            }

            int ActiveStatus = Convert.ToInt32(model.EnumActiveStatus);
            if (ActiveStatus != 0)
            {
                if (ActiveStatus == 1)
                {
                    inobject.CheckActive = 1;
                }
                else if (ActiveStatus == 2)
                {
                    inobject.CheckActive = 0;
                }
            }

            List<AddOfferBanners> objList = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecordList(Common.StoreProcedures.sp_OfferBanners_Get, inobject, outobject);
            model.objData = objList;
            return View(model);
        }


        [HttpGet]
        [Authorize]
        public ActionResult OfferBannersAdd(string Id)
        {
            AddOfferBanners model = new AddOfferBanners();
            if (!String.IsNullOrEmpty(Id))
            {
                AddOfferBanners outobject = new AddOfferBanners();
                GetOfferBanners inobject = new GetOfferBanners();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecord(Common.StoreProcedures.sp_OfferBanners_Get, inobject, outobject);
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
        public ActionResult OfferBannersAdd(AddOfferBanners model, HttpPostedFileBase Image)
        {
            AddOfferBanners outobject_priority = new AddOfferBanners();
            List<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem
            {
                Text = "Select Priority",
                Value = "0",
                Selected = true
            });
            item.Add(new SelectListItem
            {
                Text = "1",
                Value = "1"
            });
            item.Add(new SelectListItem
            {
                Text = "2",
                Value = "2"
            });
            item.Add(new SelectListItem
            {
                Text = "3",
                Value = "3"
            });
            item.Add(new SelectListItem
            {
                Text = "4",
                Value = "4"
            });
            item.Add(new SelectListItem
            {
                Text = "5",
                Value = "5"
            });
            ViewBag.Priority = item;
            GetOfferBanners inobject_priority = new GetOfferBanners();
            if (model.Priority != 0)
            {
                inobject_priority.Priority = model.Priority;
            }
            AddOfferBanners res_priority = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecord(Common.StoreProcedures.sp_OfferBanners_Get, inobject_priority, outobject_priority);
            if (res_priority != null && res_priority.Id != 0)
            {
                res_priority.Priority = 0;
                bool status_priority = RepCRUD<AddOfferBanners, GetOfferBanners>.Update(res_priority, "offerbanners");
            }

            AddOfferBanners outobject = new AddOfferBanners();
            GetOfferBanners inobject = new GetOfferBanners();
            if (model.Id != 0)

            {
                inobject.Id = Convert.ToInt64(model.Id);
                AddOfferBanners res = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecord(Common.StoreProcedures.sp_OfferBanners_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (string.IsNullOrEmpty(model.Name))
                    {
                        ViewBag.Message = "Please enter name";
                        return View(model);
                    }
                    res.Name = model.Name;
                    res.Type = Convert.ToInt32(model.EnumTypeProviders);
                    res.FromDate = model.FromDate;
                    res.ToDate = model.ToDate;
                    res.IsActive = model.IsActive;
                    res.Priority = model.Priority;
                    res.URL = model.URL;

                    if (Image == null && (model.Image == "" || model.Image == null))
                    {
                        ViewBag.Message = "Please upload image";
                        return View(model);
                    }

                    if (Image != null)
                    {
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/BannerImages/") + fileName);
                        Image.SaveAs(filePath);
                        res.Image = fileName;
                    }

                
                    if (Session["AdminMemberId"] != null)
                    {
                        res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddOfferBanners, GetOfferBanners>.Update(res, "offerbanners");
                        if (status)
                        {
                            ViewBag.SuccessMessage = "Successfully Updated OfferBanners Detail.";
                            Common.AddLogs("Updated OfferBanners Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
                AddOfferBanners res = new AddOfferBanners();
                if (string.IsNullOrEmpty(model.Name))
                {
                    ViewBag.Message = "Please enter name";
                    return View(model);
                }
                res.Name = model.Name;
                res.Type = Convert.ToInt32(model.EnumTypeProviders);
                res.FromDate = model.FromDate;
                res.ToDate = model.ToDate;
                res.IsActive = model.IsActive;
                res.Priority = model.Priority;
                res.URL = model.URL;

                if (Image == null && (model.Image == "" || model.Image == null))
                {
                    ViewBag.Message = "Please upload image";
                    return View(model);
                }
                if (Image != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/BannerImages/") + fileName);
                    Image.SaveAs(filePath);
                    res.Image = fileName;
                }
                if (Session["AdminMemberId"] != null)
                {
                    res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.CreatedByName = Session["AdminUserName"].ToString();
                    Int64 Id = RepCRUD<AddOfferBanners, GetOfferBanners>.Insert(res, "offerbanners");
                    if (Id > 0)
                    {
                        ViewBag.SuccessMessage = "Successfully Added OfferBanners.";
                        Common.AddLogs("Added OfferBanners Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                    }
                    else
                    {
                        ViewBag.Message = "Not Added ! Try Again later.";
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecord(Common.StoreProcedures.sp_OfferBanners_Get, inobject, outobject);

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
        public JsonResult OffersBlockUnblock(AddOfferBanners model)
        {

            AddOfferBanners outobject = new AddOfferBanners();
            GetOfferBanners inobject = new GetOfferBanners();
            inobject.Id = model.Id;
            AddOfferBanners res = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecord(Common.StoreProcedures.sp_OfferBanners_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddOfferBanners, GetOfferBanners>.Update(res, "offerbanners");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update offer";
                    Common.AddLogs("Updated OfferBanners by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);

                }
                else
                {
                    ViewBag.Message = "Not Updated offer";
                    Common.AddLogs("Not Updated OfferBanners", true, (int)AddLog.LogType.User);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}