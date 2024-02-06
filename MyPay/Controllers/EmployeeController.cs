using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class EmployeeController : BaseAdminSessionController
    {
        // GET: Employee
        [Authorize]
        [HttpGet]
        public ActionResult Index(string MemberId)
        {
            AddAdmin model = new AddAdmin();
            if (MemberId != null && MemberId != "0")
            {
                AddAdmin outobject = new AddAdmin();
                GetAdmin inobject = new GetAdmin();
                inobject.MemberId = Convert.ToInt64(MemberId);
                model = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
            }

            List<SelectListItem> statelist = CommonHelpers.GetSelectList_State(model.StateId);

            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_District(model.StateId, model.DistrictId);

            List<SelectListItem> municipalitylist = CommonHelpers.GetSelectList_Municipality(model.DistrictId, model.MunicipalityId);
            List<SelectListItem> rolelist = CommonHelpers.GetSelectList_AdminLoginRole(model.RoleId);
            ViewBag.StateId = statelist;
            ViewBag.DistrictId = districtlist;
            ViewBag.MunicipalityId = municipalitylist;
            ViewBag.RoleId = rolelist;
            return View(model);
        }
         
        // POST: Employee
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddAdmin model, HttpPostedFileBase EmpImageFile)
        {
            if (string.IsNullOrEmpty(model.RoleName))
            {
                ViewBag.Message = "Please select role";
            }
            else if (string.IsNullOrEmpty(model.FirstName))
            {
                ViewBag.Message = "Please enter first name";
            }
            else if (string.IsNullOrEmpty(model.LastName))
            {
                ViewBag.Message = "Please enter last name";
            }
            else if (string.IsNullOrEmpty(model.ContactNumber))
            {
                ViewBag.Message = "Please enter contact number";
            }
            else if (string.IsNullOrEmpty(model.Email))
            {
                ViewBag.Message = "Please enter email";
            }
            else if (model.StateId == 0)
            {
                ViewBag.Message = "Please select state";
            }
            else if (model.DistrictId == 0)
            {
                ViewBag.Message = "Please select district";
            }
            else if (model.MunicipalityId == 0)
            {
                ViewBag.Message = "Please select municipality";
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Message = "Please enter password";
            }
            else if (model.Password.Length < 8)
            {
                ViewBag.Message = "Minimum Password length should be 8 characters.";
            }
            else if (model.Password != "")
            {
                Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                Match m = test.Match(model.Password);
                if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                {
                    ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";

                }
            }

            AddAdmin outobject = new AddAdmin();
            GetAdmin inobject = new GetAdmin();
            if (model.MemberId != 0)
            {
                inobject.MemberId = Convert.ToInt64(model.MemberId);
                AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        res.RoleId = model.RoleId;
                        res.RoleName = model.RoleName;
                        res.FirstName = model.FirstName;
                        res.LastName = model.LastName;
                        res.ContactNumber = model.ContactNumber;
                        res.Email = model.Email;
                        res.City = model.City;
                        res.DistrictId = Convert.ToInt32(model.DistrictId);
                        res.Municipality = model.Municipality;
                        res.MunicipalityId = Convert.ToInt32(model.MunicipalityId);
                        res.StateId = Convert.ToInt32(model.StateId);
                        res.State = model.State;
                        res.Address = model.Address;
                        res.Password = Common.EncryptString(model.Password);
                        res.TransactionPassword = Common.EncryptString(model.Password);
                        res.ZipCode = model.ZipCode;
                        if (EmpImageFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(EmpImageFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/EmployeeImages/") + fileName);
                            EmpImageFile.SaveAs(filePath);
                            res.Image = fileName;
                        }
                        if (Session["AdminMemberId"] != null)
                        {
                            bool status = RepCRUD<AddAdmin, GetAdmin>.Update(res, "adminlogin");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully Updated Employee.";
                                Common.AddLogs("Updated Employee Detail of (EmployeeID:" + res.MemberId + "  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                                return RedirectToAction("EmployeeList");
                            }
                            else
                            {
                                ViewBag.Message = "Not Updated.";
                            }
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    string RandomUserId = Common.GetNewAdminLoginId().ToString();
                    outobject.UserId = "user" + RandomUserId;
                    outobject.CountryId = 216;
                    outobject.VerificationCode = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                    outobject.MemberId = Convert.ToInt64(RandomUserId);
                    outobject.IsActive = true;
                    outobject.IsApprovedByAdmin = true;
                    outobject.IpAddress = Common.GetUserIP();
                    outobject.Password = Common.EncryptString(model.Password);
                    outobject.TransactionPassword = Common.EncryptString(model.Password);

                    outobject.RoleId =model.RoleId;
                    outobject.RoleName = model.RoleName;


                    outobject.FirstName = model.FirstName;
                    outobject.LastName = model.LastName;
                    outobject.ContactNumber = model.ContactNumber;
                    outobject.Email = model.Email;
                    outobject.City = model.City;
                    outobject.DistrictId = Convert.ToInt32(model.DistrictId);
                    outobject.Municipality = model.Municipality;
                    outobject.MunicipalityId = Convert.ToInt32(model.MunicipalityId);
                    outobject.StateId = Convert.ToInt32(model.StateId);
                    outobject.State = model.State;
                    outobject.Address = model.Address;
                    outobject.ZipCode = model.ZipCode;

                    if (EmpImageFile != null)
                    {
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(EmpImageFile.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/EmployeeImages/") + fileName);
                        EmpImageFile.SaveAs(filePath);
                        outobject.Image = fileName;
                    }
                    if (Session["AdminMemberId"] != null)
                    {
                        outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        outobject.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddAdmin, GetAdmin>.Insert(outobject, "adminlogin");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully added Employee.";
                            Common.AddLogs("Added Employee Detail by(MemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                            return RedirectToAction("EmployeeList");
                        }
                        else
                        {
                            ViewBag.Message = "Not Added.";
                        }
                    }
                }
            }

            inobject.MemberId = Convert.ToInt64(model.MemberId);
            model = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);

            //PermanantAddress
            List<SelectListItem> statelist = CommonHelpers.GetSelectList_State(model.StateId);

            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_District(model.StateId, model.DistrictId);

            List<SelectListItem> municipalitylist = CommonHelpers.GetSelectList_Municipality(model.DistrictId, model.MunicipalityId);
            List<SelectListItem> rolelist = CommonHelpers.GetSelectList_AdminLoginRole(model.RoleId);

            ViewBag.StateId = statelist;
            ViewBag.DistrictId = districtlist;
            ViewBag.MunicipalityId = municipalitylist;
            ViewBag.RoleId = rolelist;
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EmployeeList()
        {
            int RoleId = 0;
            List<SelectListItem> rolelist = CommonHelpers.GetSelectList_AdminLoginRole(RoleId);
            ViewBag.RoleId = rolelist;
            return View();
        }

        [Authorize]
        public JsonResult GetEmployeeLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("FullName");
            columns.Add("Joining Date");
            columns.Add("UserId");
            columns.Add("Contact");
            columns.Add("Email");
            columns.Add("Contact");
            columns.Add("Status");

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
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string Email = context.Request.Form["Email"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string RoleId = context.Request.Form["RoleId"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<AddAdmin> trans = new List<AddAdmin>();

            GetAdmin w = new GetAdmin();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.ContactNumber = ContactNumber;
            w.FirstName = Name;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Email = Email;
            if (!string.IsNullOrEmpty(RoleId))
            {
                w.RoleId = Convert.ToInt32(RoleId);
            }           
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddAdmin
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         FirstName = row["FirstName"].ToString(),
                         LastName = row["LastName"].ToString(),
                         Email = row["Email"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         RoleId = Convert.ToInt32(row["RoleId"]),
                         RoleName = Convert.ToString(row["RoleName"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         TotalUserCount = dt.Rows[0]["FilterTotalCount"].ToString(),
                         UserId = row["UserId"].ToString(),
                         IsPasswordExpired = Convert.ToBoolean(row["IsPasswordExpired"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddAdmin>> objDataTableResponse = new DataTableResponse<List<AddAdmin>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        public JsonResult EmpBlockUnblock(AddAdmin model)
        {
            AddAdmin outobject = new AddAdmin();
            GetAdmin inobject = new GetAdmin();
            inobject.MemberId = model.MemberId;
            AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddAdmin, GetAdmin>.Update(res, "adminlogin");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update employee";
                    Common.AddLogs("Updated employee of (EmployeeID: " + res.MemberId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);

                }
                else
                {
                    ViewBag.Message = "Not Updated employee";
                    Common.AddLogs("Not Updated (EmployeeID: " + res.MemberId + " )", true, (int)AddLog.LogType.User);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ResetPassword(string MemberId)
        {

            if (string.IsNullOrEmpty(MemberId))
            {
                return RedirectToAction("Index");
            }
            AddAdmin outobject = new AddAdmin();
            GetAdmin inobject = new GetAdmin();
            inobject.MemberId = Convert.ToInt64(MemberId);
            AddAdmin model = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
            if (!string.IsNullOrEmpty(MemberId) && (model == null || model.Id == 0 || MemberId == "0"))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult ResetPassword(AddUser vmodel, string Password)
        {
            AddAdmin outobject = new AddAdmin();
            GetAdmin inobject = new GetAdmin();
            inobject.MemberId = Convert.ToInt64(vmodel.MemberId);
            AddAdmin model = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
            model.Password = (Common.EncryptString(Password));
            if (string.IsNullOrEmpty(Password))
            {
                ViewBag.Message = "Please enter password";
            }
            else if ((Password.Length < 8))
            {
                ViewBag.Message = "Minimum Password length should be 8 characters.";
            }
            else if (Password != "")
            {
                Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                Match m = test.Match(Password);
                if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                {
                    ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";

                }
            }
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                bool status = RepCRUD<AddAdmin, GetAdmin>.Update(model, "adminlogin");
                if (status)
                {
                    Common.AddLogs("Password updated successfully for (MemberId:" + model.MemberId + ") by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                    ViewBag.SuccessMessage = "Password updated successfully.";
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult EmpPasswordExpired(AddAdmin model)
        {
            AddAdmin outobject = new AddAdmin();
            GetAdmin inobject = new GetAdmin();
            inobject.MemberId = model.MemberId;
            AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (res.IsPasswordExpired)
                {
                    res.IsPasswordExpired = false;
                }
                else
                {
                    res.IsPasswordExpired = true;
                }
                bool IsUpdated = RepCRUD<AddAdmin, GetAdmin>.Update(res, "adminlogin");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update employee";
                    Common.AddLogs("Updated employee of (EmployeeID: " + res.MemberId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);

                }
                else
                {
                    ViewBag.Message = "Not Updated employee";
                    Common.AddLogs("Not Updated (EmployeeID: " + res.MemberId + " )", true, (int)AddLog.LogType.User);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}