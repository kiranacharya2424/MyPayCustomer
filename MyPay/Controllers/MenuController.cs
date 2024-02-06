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
    public class MenuController : BaseAdminSessionController
    {
        // GET: Menu
        public ActionResult Index(string Id)
        {
            AddMenu model = new AddMenu();
            if (!String.IsNullOrEmpty(Id))
            {
                AddMenu outobject = new AddMenu();
                GetMenu inobject = new GetMenu();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetMenu, AddMenu>.GetRecord(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            }
            //Get Parent MenuList in DropDown
            List<SelectListItem> menulist = CommonHelpers.GetSelectList_ParentMenu(model.ParentId.ToString());
            if (model.ParentId != 0)
            {
                menulist.Find(c => c.Value == model.ParentId.ToString()).Selected = true;
            }

            ViewBag.ParentId = menulist;
            return View(model);
        }

        // Post: AddMenu
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddMenu model)
        {
            if (string.IsNullOrEmpty(model.MenuName))
            {
                ViewBag.Message = "Please enter menu name";
            }

            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                if (model.Id != 0)
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddMenu res = RepCRUD<GetMenu, AddMenu>.GetRecord(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        res.MenuName = model.MenuName;
                        res.ParentId = model.ParentId;
                        res.Url = model.Url;
                        res.Icon = model.Icon;
                        res.IsActive = model.IsActive;
                        res.IsInnerURL = model.IsInnerURL;
                        res.SortingId = model.SortingId;
                        res.IsAdminMenu = model.IsAdminMenu;
                        if (Session["AdminMemberId"] != null)
                        {
                            res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.CreatedByName = Session["AdminUserName"].ToString();
                            bool status = RepCRUD<AddMenu, GetMenu>.Update(res, "menus");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully Updated menu Detail.";
                                Common.AddLogs("Updated menu(id" + res.Id + ") Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
                    AddMenu res = new AddMenu();

                    res.MenuName = model.MenuName;
                    res.IsActive = model.IsActive;
                    res.ParentId = model.ParentId;
                    res.Url = model.Url;
                    res.Icon = model.Icon;
                    res.IsInnerURL = model.IsInnerURL;
                    res.SortingId = model.SortingId;
                    res.IsAdminMenu = model.IsAdminMenu;
                    if (Session["AdminMemberId"] != null)
                    {
                        res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddMenu, GetMenu>.Insert(res, "menus");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully added menu detail.";
                            Common.AddLogs("Addded Menu Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                        }
                        else
                        {
                            ViewBag.Message = "Not Added ! Try Again later.";
                        }
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetMenu, AddMenu>.GetRecord(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);

            //Get Parent MenuList in DropDown
            List<SelectListItem> menulist = CommonHelpers.GetSelectList_ParentMenu(model.ParentId.ToString());
            if (model.ParentId != 0)
            {
                menulist.Find(c => c.Value == model.ParentId.ToString()).Selected = true;
            }

            ViewBag.ParentId = menulist;
            //ModelState.Clear();
            return RedirectToAction("Index");
            //return View();

        }

        // GET:MenuList
        [HttpGet]
        [Authorize]
        public ActionResult MenuList()
        {
            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            List<AddMenu> objList = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            Req_Web_Menu model = new Req_Web_Menu();
            model.objData = objList;
            return View(model);
        }

        // Post: MenuList
        [HttpPost]
        [Authorize]
        public ActionResult MenuList(Req_Web_Menu model)
        {
            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            List<AddMenu> objList = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            model.objData = objList;
            return View(model);
        }

        //MenuBlockUnblock
        [HttpPost]
        [Authorize]
        public JsonResult MenuBlockUnblock(AddMenu model)
        {
            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            inobject.Id = model.Id;
            AddMenu res = RepCRUD<GetMenu, AddMenu>.GetRecord(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddMenu, GetMenu>.Update(res, "menus");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully update menu";
                    Common.AddLogs("Updated menu(Id:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                }
                else
                {
                    ViewBag.Message = "Not Updated menu";
                    Common.AddLogs("Not Updated menu", true, (int)AddLog.LogType.User);
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        // GET: AddRoles
        public ActionResult AddRoles(string Id)
        {
            AddRole model = new AddRole();
            if (!String.IsNullOrEmpty(Id))
            {
                AddRole outobject = new AddRole();
                GetRole inobject = new GetRole();
                inobject.Id = Convert.ToInt64(Id);
                model = RepCRUD<GetRole, AddRole>.GetRecord(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
            }
            return View(model);
        }

        // Post: AddRoles
        [HttpPost]
        [Authorize]
        public ActionResult AddRoles(AddRole model)
        {
            if (string.IsNullOrEmpty(model.RoleName))
            {
                ViewBag.Message = "Please enter role name";
            }

            AddRole outobject = new AddRole();
            GetRole inobject = new GetRole();
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                if (model.Id != 0)
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddRole res = RepCRUD<GetRole, AddRole>.GetRecord(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        res.RoleName = model.RoleName;
                        res.IsActive = model.IsActive;

                        if (Session["AdminMemberId"] != null)
                        {
                            res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.CreatedByName = Session["AdminUserName"].ToString();
                            bool status = RepCRUD<AddRole, GetRole>.Update(res, "roles");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully Updated role Detail.";
                                Common.AddLogs("Updated role(id" + res.Id + ") Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
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
                    AddRole res = new AddRole();

                    res.RoleName = model.RoleName;
                    res.IsActive = model.IsActive;
                    if (Session["AdminMemberId"] != null)
                    {
                        res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddRole, GetRole>.Insert(res, "roles");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully added role detail.";
                            Common.AddLogs("Addded role Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                        }
                        else
                        {
                            ViewBag.Message = "Not Added ! Try Again later.";
                        }
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetRole, AddRole>.GetRecord(Common.StoreProcedures.sp_Role_Get, inobject, outobject);

            return View(model);
        }

        // GET:RolesList
        [HttpGet]
        [Authorize]
        public ActionResult RolesList()
        {
            AddRole outobject = new AddRole();
            GetRole inobject = new GetRole();
            List<AddRole> objList = RepCRUD<GetRole, AddRole>.GetRecordList(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
            Req_Web_Roles model = new Req_Web_Roles();
            model.objData = objList;
            return View(model);
        }

        // Post: RolesList
        [HttpPost]
        [Authorize]
        public ActionResult RolesList(Req_Web_Roles model)
        {
            AddRole outobject = new AddRole();
            GetRole inobject = new GetRole();
            List<AddRole> objList = RepCRUD<GetRole, AddRole>.GetRecordList(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
            model.objData = objList;
            return View(model);
        }

        //RolesBlockUnblock
        [HttpPost]
        [Authorize]
        public JsonResult RolesBlockUnblock(AddRole model)
        {
            AddRole outobject = new AddRole();
            GetRole inobject = new GetRole();
            inobject.Id = model.Id;
            AddRole res = RepCRUD<GetRole, AddRole>.GetRecord(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddRole, GetRole>.Update(res, "roles");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully update role";
                    Common.AddLogs("Updated role(Id:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                }
                else
                {
                    ViewBag.Message = "Not Updated role";
                    Common.AddLogs("Not Updated role", true, (int)AddLog.LogType.User);
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult GetMenuList(string roleid, string parentid, string menuname, int skip = 0, int take = 200)
        {

            Int64 MemberId = Convert.ToInt64(Session["AdminMemberId"]);
            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            inobject.Take = take;
            inobject.Skip = (take * skip);
            if (parentid == "0")
            {
                inobject.CheckParentId = -1;
            }
            else
            {
                inobject.CheckParentId = Convert.ToInt32(parentid);
            }
            inobject.RoleId = Convert.ToInt32(roleid);

            inobject.MenuName = menuname;
            List<AddMenu> objticket = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            Int32 recordFiltered = objticket.Count;
            //foreach (DataRow item in dt.Rows)
            //{
            //    if (item["IsApprove"].ToString() == "1")
            //    {
            //        hdnIds.Value += item["Id"].ToString() + "-" + item["ParentId"].ToString() + "-" + item["RoleId"].ToString() + ",";
            //    }
            //}
            DataTableResponse<List<AddMenu>> objDataTableResponse = new DataTableResponse<List<AddMenu>>
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
        public JsonResult GetMyPayMenuList(string roleid, string parentid, string menuname, int skip = 0, int take = 200)
        {

            Int64 MemberId = Convert.ToInt64(Session["AdminMemberId"]);
            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            inobject.Take = take;
            inobject.Skip = (take * skip);
            if (parentid == "0")
            {
                inobject.CheckParentId = -1;
            }
            else
            {
                inobject.CheckParentId = Convert.ToInt32(parentid);
            }
            inobject.RoleId = Convert.ToInt32(roleid);

            inobject.MenuName = menuname;
            inobject.CheckAdminMenu = 1;
            List<AddMenu> objticket = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            Int32 recordFiltered = objticket.Count;
            //foreach (DataRow item in dt.Rows)
            //{
            //    if (item["IsApprove"].ToString() == "1")
            //    {
            //        hdnIds.Value += item["Id"].ToString() + "-" + item["ParentId"].ToString() + "-" + item["RoleId"].ToString() + ",";
            //    }
            //}
            DataTableResponse<List<AddMenu>> objDataTableResponse = new DataTableResponse<List<AddMenu>>
            {
                //draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objticket
            };

            return Json(objDataTableResponse);
            //return RepTicket.GetTickets(MemberId, isSeen, isFavourite, isAttached, isClosed, TicketId, usertype, skip, take);

        }


        //GET:RoleAssign
        [HttpGet]
        [Authorize]
        public ActionResult RoleAssign()
        {
            //AddMenu outobject = new AddMenu();
            //GetMenu inobject = new GetMenu();
            //List<AddMenu> objList = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            //Req_Web_Menu model = new Req_Web_Menu();
            //model.objData = objList;

            //Get Parent MenuList in DropDown
            List<SelectListItem> menulist = CommonHelpers.GetSelectList_MenuList();
            ViewBag.ParentId = menulist;

            //Get RoleList in DropDown
            List<SelectListItem> rolelist = CommonHelpers.GetSelectList_RolesList();
            ViewBag.RoleId = rolelist;
            return View();
        }

        // Post:RoleAssign
        [HttpPost]
        [Authorize]
        public ActionResult RoleAssign(Req_Web_Menu model, string hdnIds, string ParentId, string RoleId, string MenuName)
        {
            if (RoleId == "" || RoleId == "0")
            {
                Response.Write("<script>alert('Please select role.');</script>");
                ViewBag.Message = "Please select Role";
            }
            if (ViewBag.Message == null)
            {
                if (hdnIds != "")
                {
                    string[] splitvalues = hdnIds.Split(',');
                    if (splitvalues != null)
                    {
                        int count = 0;
                        foreach (var item in splitvalues)
                        {
                            if (item != "")
                            {
                                string[] splititem = item.Split('-');
                                AddRoleOfMenus outobj = new AddRoleOfMenus();
                                GetRoleOfMenus inobj = new GetRoleOfMenus();
                                outobj.MenuId = Convert.ToInt32(splititem[0]);
                                outobj.ParentId = Convert.ToInt32(splititem[1]);
                                outobj.RoleId = Convert.ToInt32(RoleId);
                                outobj.IsActive = true;
                                if (count == 0)
                                {
                                    AddRoleOfMenus outobjdel = new AddRoleOfMenus();
                                    GetRoleOfMenus inobjdel = new GetRoleOfMenus();
                                    inobjdel.RoleId = Convert.ToInt32(RoleId);
                                    if (splititem[1].ToString() == "0")
                                    {
                                        inobjdel.CheckParentId = -1;
                                    }
                                    else
                                    {
                                        inobjdel.CheckParentId = outobj.ParentId;
                                    }
                                    inobjdel.Delete();
                                    count = count + 1;
                                }
                                Int64 Id = RepCRUD<AddRoleOfMenus, GetRoleOfMenus>.Insert(outobj, "menuassign");
                                if (Id > 0)
                                {
                                    ViewBag.SuccessMessage = "Successfully assign role.";
                                    Common.AddLogs("Role Assign by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                                }
                                else
                                {
                                    ViewBag.Message = "Not assign role.";
                                    Common.AddLogs("Not Role Assign by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                                }
                            }
                        }
                    }
                }
            }

            //Get Parent MenuList in DropDown
            List<SelectListItem> menulist = CommonHelpers.GetSelectList_MenuList();
            ViewBag.ParentId = menulist;

            //Get RoleList in DropDown
            List<SelectListItem> rolelist = CommonHelpers.GetSelectList_RolesList();
            ViewBag.RoleId = rolelist;
            return View();
        }

        //GET:RoleAssignDemo
        [HttpGet]
        [Authorize]
        public ActionResult RoleAssignMyPay()
        {
            //AddMenu outobject = new AddMenu();
            //GetMenu inobject = new GetMenu();
            //List<AddMenu> objList = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            //Req_Web_Menu model = new Req_Web_Menu();
            //model.objData = objList;

            //Get Parent MenuList in DropDown
            List<SelectListItem> menulist = CommonHelpers.GetSelectList_MenuList();
            ViewBag.ParentId = menulist;

            //Get RoleList in DropDown
            List<SelectListItem> rolelist = CommonHelpers.GetSelectList_AdminLoginRole(0);
            ViewBag.RoleId = rolelist;
            return View();
        }

        // Post:RoleAssignDemo
        [HttpPost]
        [Authorize]
        public ActionResult RoleAssignMyPay(Req_Web_Menu model, string hdnIds, string ParentId, string RoleId, string MenuName)
        {

            if (RoleId == "" || RoleId == "0")
            {
                Response.Write("<script>alert('Please select role.');</script>");
                ViewBag.Message = "Please select Role";
            }
            if (ViewBag.Message == null)
            {
                if (hdnIds != "")
                {
                    string[] splitvalues = hdnIds.Split(',');
                    if (splitvalues != null)
                    {
                        int count = 0;
                        foreach (var item in splitvalues)
                        {
                            if (item != "")
                            {
                                string[] splititem = item.Split('-');
                                AddRoleOfMenus outobj = new AddRoleOfMenus();
                                GetRoleOfMenus inobj = new GetRoleOfMenus();
                                outobj.MenuId = Convert.ToInt32(splititem[0]);
                                outobj.ParentId = Convert.ToInt32(splititem[1]);
                                outobj.RoleId = Convert.ToInt32(RoleId);
                                outobj.IsActive = true;
                                if (count == 0)
                                {
                                    AddRoleOfMenus outobjdel = new AddRoleOfMenus();
                                    GetRoleOfMenus inobjdel = new GetRoleOfMenus();
                                    inobjdel.RoleId = Convert.ToInt32(RoleId);
                                    if (splititem[1].ToString() == "0")
                                    {
                                        inobjdel.CheckParentId = -1;
                                    }
                                    else
                                    {
                                        inobjdel.CheckParentId = outobj.ParentId;
                                    }
                                    inobjdel.Delete();
                                    count = count + 1;
                                }
                                Int64 Id = RepCRUD<AddRoleOfMenus, GetRoleOfMenus>.Insert(outobj, "menuassign");
                                if (Id > 0)
                                {
                                    ViewBag.SuccessMessage = "Successfully assign role.";
                                    Common.AddLogs("Role Assign by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                                }
                                else
                                {
                                    ViewBag.Message = "Not assign role.";
                                    Common.AddLogs("Not Role Assign by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                                }
                            }
                        }
                    }
                }
            }

            //Get Parent MenuList in DropDown
            List<SelectListItem> menulist = CommonHelpers.GetSelectList_MenuList();
            ViewBag.ParentId = menulist;

            //Get RoleList in DropDown
            List<SelectListItem> rolelist = CommonHelpers.GetSelectList_AdminLoginRole(0);
            ViewBag.RoleId = rolelist;
            return View();
        }
    }
}