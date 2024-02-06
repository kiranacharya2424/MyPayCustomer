using ClosedXML.Excel;
using log4net;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPay.Controllers
{
    public class UserController : BaseAdminSessionController
    {
        private static ILog log = LogManager.GetLogger(typeof(UserController));

        [Authorize]
        public ActionResult Index()
        {
            AddUser outobject = new AddUser();
            //GetUser inobject = new GetUser();
            //List<AddUser> objList = RepCRUD<GetUser, AddUser>.GetRecordList("sp_Users_Get", inobject, outobject);
            //Req_Web_User model = new Req_Web_User();
            //model.objData = objList;
            ViewBag.DumpURL = Common.dumpurl;
            return View(outobject);
        }
        //[HttpPost]
        //[Authorize]
        //public ActionResult Index(AddUser model)
        //{
        //    return View();
        //}

        [Authorize]
        public JsonResult GetUserLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Full Name");
            columns.Add("Contact No");
            columns.Add("Joining Date");
            columns.Add("Email");
            columns.Add("DOB");
            columns.Add("DOBType2");
            columns.Add("Gender");
            columns.Add("Wallet");
            columns.Add("Status");
            columns.Add("KYC Status");

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
            string RefId = context.Request.Form["RefId"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string Email = context.Request.Form["Email"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string LoginFromDate = context.Request.Form["LoginFromDate"];
            string LoginToDate = context.Request.Form["LoginToDate"];
            string KycStatus = context.Request.Form["KycStatus"];
            string RoleId = context.Request.Form["RoleId"];
            string IsActive = context.Request.Form["IsActive"];
            string RefCode = context.Request.Form["RefCode"];
            string OldUserStatuses = context.Request.Form["OldUserStatuses"];
            string OldAndNewUsers = context.Request.Form["OldAndNewUsers"];
            string RefCodeAttempted = context.Request.Form["RefCodeAttempted"];
            string DocumentNumber = context.Request.Form["DocumentNumber"];
            string DeviceId = context.Request.Form["DeviceId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<AddUser> trans = new List<AddUser>();

            GetUser w = new GetUser();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            if (!string.IsNullOrEmpty(RefId))
            {
                w.RefId = Convert.ToInt64(RefId);
            }
            w.ContactNumber = ContactNumber;
            w.FirstName = Name;
            w.DateFrom = fromdate;
            w.DateTo = todate;
            w.Email = Email;
            w.LoginFromDate = LoginFromDate;
            w.LoginToDate = LoginToDate;
            w.DocumentNumber = DocumentNumber;
            w.DeviceId = DeviceId;
            if (KycStatus != "-1")
            {
                w.IsKYCApproved = Convert.ToInt32(KycStatus);
            }
            else
            {
                w.IsKYCApproved = -1;
            }
            if (RoleId != "0")
            {
                w.RoleId = Convert.ToInt32(RoleId);
            }
            else
            {
                w.RoleId = -1;
            }
            if (!string.IsNullOrEmpty(RefId))
            {
                w.RefId = Convert.ToInt64(RefId);
            }
            w.CheckActive = Convert.ToInt32(IsActive);
            w.RefCode = RefCode;
            if (OldUserStatuses == "1")
            {
                w.CheckPasswordReset = "1";
            }
            else if (OldUserStatuses == "2")
            {
                w.CheckNotPasswordReset = "1";
            }
            else if (OldUserStatuses == "3")
            {
                w.CheckPin = "1";
            }
            else if (OldUserStatuses == "4")
            {
                w.CheckNotPin = "1";
            }
            else if (OldUserStatuses == "5")
            {
                w.CheckFirstName = "1";
            }
            else if (OldUserStatuses == "6")
            {
                w.CheckNotFirstName = "1";
            }

            if (OldAndNewUsers == "1")
            {
                w.CheckOldAndNewUser = 1;
            }
            else if (OldAndNewUsers == "0")
            {
                w.CheckOldAndNewUser = 0;
            }
            w.RefCodeAttempted = RefCodeAttempted;

            Int32 recordFiltered = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby, ref recordFiltered);

            trans = (from DataRow row in dt.Rows

                     select new AddUser
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         FirstName = row["Name"].ToString(),
                         Gender = Convert.ToInt32(row["Gender"]),
                         GenderName = @Enum.GetName(typeof(AddUser.sex), Convert.ToInt64(row["Gender"])),
                         Email = row["Email"].ToString(),
                         TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         IsKYCApproved = Convert.ToInt32(row["IsKYCApproved"]),
                         RoleId = Convert.ToInt32(row["RoleId"]),
                         RoleName = Convert.ToString(row["RoleName"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         IsPhoneVerified = Convert.ToBoolean(row["IsPhoneVerified"]),
                         IsEmailVerified = Convert.ToBoolean(row["IsEmailVerified"]),
                         TotalUserCount = recordFiltered.ToString(),
                         RefCodeAttempted = Convert.ToString(row["RefCodeAttempted"]),
                         DateofBirthdt = row["DateofBirth"].ToString(),
                         DOBType2 = row["DOBType2"].ToString(),
                         DeviceId = row["DeviceId"].ToString(),
                         VerificationCode = row["VerificationCode"].ToString(),
                         TotalRewardPoints = Convert.ToDecimal(row["TotalRewardPoints"].ToString())
                     }).ToList();



            DataTableResponse<List<AddUser>> objDataTableResponse = new DataTableResponse<List<AddUser>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public JsonResult UserBlockUnblock(string MemberId, string Remarks)
        {
            string msg = "";
            if (MemberId == "0" || string.IsNullOrEmpty(MemberId))
            {
                msg = "Please Select MemberId";
            }
            else if (string.IsNullOrEmpty(Remarks))
            {
                msg = "Please enter Remarks";
            }
            else
            {
                AddUser outobject = new AddUser();
                GetUser inobject = new GetUser();
                inobject.MemberId = Convert.ToInt64(MemberId);
                AddUser res = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddUserInActiveRemarks res_remarks = new AddUserInActiveRemarks();
                    res_remarks.MemberId = Convert.ToInt64(MemberId);
                    res_remarks.FirstName = res.FirstName;
                    res_remarks.LastName = res.LastName;
                    res_remarks.ContactNumber = res.ContactNumber;
                    res_remarks.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res_remarks.CreatedByName = Session["AdminUserName"].ToString();
                    if (res.IsActive)
                    {
                        res_remarks.Action = "User Blocked by Admin";
                        res.IsActive = false;
                    }
                    else
                    {
                        res_remarks.Action = "User UnBlock by Admin";
                        res.IsActive = true;
                    }
                    res_remarks.Remarks = Remarks;
                    Int64 Id = RepCRUD<AddUserInActiveRemarks, GetUserInActiveRemarks>.Insert(res_remarks, "userinactiveremarks");

                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Convert.ToString(Session["AdminUserName"]);
                    bool IsUpdated = RepCRUD<AddUser, GetUser>.Update(res, "user");
                    if (IsUpdated)
                    {
                        ViewBag.SuccessMessage = "Successfully Update";
                        Common.AddLogs("Updated user(MemberId:" + res.MemberId + ")  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User, res.MemberId, res.FirstName + " " + res.LastName, false, "", "", 0, Convert.ToInt64(Session["AdminMemberId"]), Convert.ToString(Session["AdminUserName"]));
                        msg = "Successfully Update";
                    }
                    else
                    {
                        ViewBag.Message = "Not Updated";
                        Common.AddLogs("Not Updated user", true, (int)AddLog.LogType.User);
                        msg = "Not Updated";
                    }
                }
                else
                {
                    msg = "User Not Found.";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult UserDetails(string MemberId)
        {

            if (string.IsNullOrEmpty(MemberId))
            {
                return RedirectToAction("Index");
            }
            AddUserLoginWithPin outobject = new AddUserLoginWithPin();
            GetUserLoginWithPin inobject = new GetUserLoginWithPin();
            inobject.MemberId = Convert.ToInt64(MemberId);
            AddUserLoginWithPin model = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
            if (!string.IsNullOrEmpty(MemberId) && (model == null || model.Id == 0 || MemberId == "0"))
            {
                return RedirectToAction("Index");
            }

            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQR(model.MemberId.ToString());

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UserDetails(AddUserLoginWithPin model)
        {
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(model.MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQR(model.MemberId.ToString());
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult UserExport()
        {
            //AddUser outobject = new AddUser();
            //GetUser inobject = new GetUser();
            //List<AddUser> objList = RepCRUD<GetUser, AddUser>.GetRecordList("sp_Users_Get", inobject, outobject);
            //List<Req_WebUserExport> objExportList = new List<Req_WebUserExport>();
            //for (int i = 0; i < objList.Count; i++)
            //{
            //    Req_WebUserExport Row = new Req_WebUserExport();
            //    Row.MemberId = objList[i].MemberId.ToString();
            //    Row.ContactNumber = objList[i].ContactNumber.ToString();
            //    Row.FirstName = objList[i].FirstName.ToString();
            //    Row.LastName = objList[i].LastName.ToString();
            //    Row.Email = objList[i].Email;
            //    Row.CreatedDate = objList[i].CreatedDate.ToString("dd-MMM-yy hh:mm tt");
            //    Row.Gender = Enum.GetName(typeof(AddUser.sex), objList[i].Gender);
            //    Row.IsKycApproved = Enum.GetName(typeof(AddUser.kyc), objList[i].IsKYCApproved).Replace("_", " ");
            //    Row.TotalAmount = objList[i].TotalAmount.ToString();
            //    objExportList.Add(Row);
            //}
            //DataTable dtObject = Common.ConvertToDataTable(objExportList);

            GetUser w = new GetUser();
            DataTable dtObject = w.GetData_UserExport();

            Common.GenerateExcel(dtObject, Response, "UsersList");
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult UserEdit(string MemberId)
        {

            if (string.IsNullOrEmpty(MemberId))
            {
                return RedirectToAction("Index");            
            }
            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(MemberId);
            AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
            model.UserRoleEnumType = (AddUser.UserRoles)Enum.Parse(typeof(AddUser.UserRoles), model.RoleId.ToString());
            if (!string.IsNullOrEmpty(MemberId) && (model == null || model.Id == 0 || MemberId == "0"))
            {
                return RedirectToAction("Index");
            }
            if(model.DateofBirth != null || model.DateofBirth != "")
            {
                string[] date = model.DateofBirth.Split('/');
                if (date.Length == 3)
                {
                    if (int.TryParse(date[0], out int day) &&
                        int.TryParse(date[1], out int month) &&
                        int.TryParse(date[2], out int year)) 
                    {
                        model.Day = day;
                        model.Month = month;
                        model.Year = year;
                    }
                }
            }
            ViewBag.Month = model.Month;
            ViewBag.Day = model.Day;
            ViewBag.DOBType = model.DOBType2;

            if (model.IssueDate != null || model.IssueDate != "")
            {
                string[] date = model.IssueDate.Split('/');
                if (date.Length == 3)
                {
                    if (int.TryParse(date[0], out int day) &&
                        int.TryParse(date[1], out int month) &&
                        int.TryParse(date[2], out int year))
                    {
                        model.IssueDay = day;
                        model.IssueMonth = month;
                        model.IssueYear = year;
                    }
                }
            }
            ViewBag.IssueMonth = model.IssueMonth;
            ViewBag.IssueDay = model.IssueDay;
            ViewBag.IssueType = model.IssueDateType;

            if (model.ExpiryDate != null || model.ExpiryDate != "")
            {
                string[] date = model.ExpiryDate.Split('/');
                if (date.Length == 3)
                {
                    if (int.TryParse(date[0], out int day) &&
                        int.TryParse(date[1], out int month) &&
                        int.TryParse(date[2], out int year))
                    {
                        model.ExpiryDay = day;
                        model.ExpiryMonth = month;
                        model.ExpiryYear = year;
                    }
                }
            }
            ViewBag.ExpiryMonth = model.ExpiryMonth;
            ViewBag.ExpiryDay = model.ExpiryDay;
            ViewBag.ExpiryType = model.ExpiryDateType;

            List<SelectListItem> genderlist = CommonHelpers.GetSelectList_Gender(model);
            List<SelectListItem> meritallist = CommonHelpers.GetSelectList_MeritalStatus(model);
            List<SelectListItem> nationalitylist = CommonHelpers.GetSelectList_Nationality(model);
            List<SelectListItem> prooftypelist = CommonHelpers.GetSelectList_ProofType(model);
            List<SelectListItem> occupationlist = CommonHelpers.GetSelectList_Occupation(model);

            if (model.EmployeeType != 0)
            {
                occupationlist.Find(c => c.Value == model.EmployeeType.ToString()).Selected = true;
            }
            //PermanantAddress
            List<SelectListItem> statelist = CommonHelpers.GetSelectList_State(model.StateId);

            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_District(model.StateId, model.DistrictId);

            List<SelectListItem> municipalitylist = CommonHelpers.GetSelectList_Municipality(model.DistrictId, model.MunicipalityId);

            //CurrentAddress
            List<SelectListItem> currentstatelist = CommonHelpers.GetSelectList_State(model.StateId);
            List<SelectListItem> currentdistrictlist = CommonHelpers.GetSelectList_CurrentDistrict(model);
            List<SelectListItem> currentmunicipalitylist = CommonHelpers.GetSelectList_CurrentMunicipality(model);
            List<SelectListItem> issuedistrictlist = CommonHelpers.GetSelectList_IssueDistrict(model);
            List<SelectListItem> issuestatelist = new List<SelectListItem>();
            try
            {

                CommonHelpers objCommonHelpers = new CommonHelpers();
                if (model.CurrentStateId != 0)
                {
                    currentstatelist = objCommonHelpers.BindSelectedListDefaultValues(currentstatelist, model.CurrentStateId.ToString());

                }
                if (model.CurrentDistrictId != 0)
                {
                    currentdistrictlist = objCommonHelpers.BindSelectedListDefaultValues(currentdistrictlist, model.CurrentDistrictId.ToString());

                }
                if (model.CurrentMunicipalityId != 0)
                {
                    currentmunicipalitylist = objCommonHelpers.BindSelectedListDefaultValues(currentmunicipalitylist, model.CurrentMunicipalityId.ToString());
                }
                //IssueFrom State And District
                issuestatelist = CommonHelpers.GetSelectList_State(model.StateId);
                if (model.IssueFromStateID != 0)
                {
                    issuestatelist = objCommonHelpers.BindSelectedListDefaultValues(issuestatelist, model.IssueFromStateID.ToString());
                }
                if (model.IssueFromDistrictID != 0)
                {
                    issuedistrictlist = objCommonHelpers.BindSelectedListDefaultValues(issuedistrictlist, model.IssueFromDistrictID.ToString());
                }
            }
            catch (Exception ex)
            {

            }
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }

            ViewBag.Gender = genderlist;
            ViewBag.MeritalStatus = meritallist;
            ViewBag.Nationality = nationalitylist;
            ViewBag.EmployeeType = occupationlist;
            ViewBag.StateId = statelist;
            ViewBag.DistrictId = districtlist;
            ViewBag.MunicipalityId = municipalitylist;
            ViewBag.CurrentStateId = currentstatelist;
            ViewBag.CurrentDistrictId = currentdistrictlist;
            ViewBag.CurrentMunicipalityId = currentmunicipalitylist;
            ViewBag.ProofType = prooftypelist;
            ViewBag.IssueFromStateID = issuestatelist;
            ViewBag.IssueFromDistrictID = issuedistrictlist;
            ViewBag.QRCode = Common.GetQRReferCode(model);
            ViewBag.MeritalStatuss = model.MeritalStatus;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UserEdit(AddUser model, HttpPostedFileBase NationalIdProofFrontFile, HttpPostedFileBase NationalIdProofBackFile, HttpPostedFileBase UserImageFile)
        {
            if (string.IsNullOrEmpty(model.FirstName))
            {
                ViewBag.Message = "Please enter first name";
            }
            else if (string.IsNullOrEmpty(model.LastName))
            {
                ViewBag.Message = "Please enter last name";
            }
            else if (model.Gender == 0)
            {
                ViewBag.Message = "Please select gender";
            }
            //else if (string.IsNullOrEmpty(model.DateofBirth))
            //{
            //    ViewBag.Message = "Please select date of birth";
            //}
            else if (model.Day == 0)
            {
                ViewBag.Message = "Please select date of birth";
            }
            else if (model.Month == 0)
            {
                ViewBag.Message = "Please select date of birth";
            }
            else if (model.Year == 0)
            {
                ViewBag.Message = "Please select date of birth";
            }
            else if (model.IssueDay == 0)
            {
                ViewBag.Message = "Please select issue date";
            }
            else if (model.IssueMonth == 0)
            {
                ViewBag.IssueMessage = "Please select issue date";
            }
            else if (model.Year == 0)
            {
                ViewBag.IssueMessage = "Please select issue date";
            }
            else if (string.IsNullOrEmpty(model.ContactNumber))
            {
                ViewBag.Message = "Please enter contact number";
            }
            else if (string.IsNullOrEmpty(model.FatherName))
            {
                ViewBag.Message = "Please enter father's name";
            }
            else if (string.IsNullOrEmpty(model.GrandFatherName))
            {
                ViewBag.Message = "Please enter grand father's name";
            }
            else if (model.MeritalStatus == 0)
            {
                ViewBag.Message = "Please select merital status";
            }
            else if (model.EmployeeType == 0)
            {
                ViewBag.Message = "Please select occupation";
            }
            else if (model.Nationality == 0)
            {
                ViewBag.Message = "Please select nationality";
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
            else if (string.IsNullOrEmpty(model.WardNumber))
            {
                ViewBag.Message = "Please enter ward number";
            }
            else if (model.CurrentStateId == 0)
            {
                ViewBag.Message = "Please select current state";
            }
            else if (model.CurrentDistrictId == 0)
            {
                ViewBag.Message = "Please select current district";
            }
            else if (model.CurrentMunicipalityId == 0)
            {
                ViewBag.Message = "Please select current municipality";
            }
            else if (string.IsNullOrEmpty(model.CurrentWardNumber))
            {
                ViewBag.Message = "Please enter current ward number";
            }
            //else if (string.IsNullOrEmpty(model.IssueDate))
            //{
            //    ViewBag.Message = "Please enter document's issue date";
            //}
            //else if (string.IsNullOrEmpty(model.ExpiryDate))
            //{
            //    ViewBag.Message = "Please enter document's expiry date";
            //}
            else if (model.ProofType == 0)
            {
                ViewBag.Message = "Please select proof type";
            }
            else if (string.IsNullOrEmpty(model.NationalIdProofFront))
            {
                if (NationalIdProofFrontFile == null)
                {
                    ViewBag.Message = "Please upload id front image";
                }
            }
            //else if (model.ProofType != (int)AddUser.ProofTypes.Driving_Licence || model.ProofType != (int)AddUser.ProofTypes.Voter_Id)
            //{
            //    if (string.IsNullOrEmpty(model.NationalIdProofBack))
            //    {
            //        if (NationalIdProofBackFile == null)
            //        {
            //            ViewBag.Message = "Please upload id back image";
            //        }
            //    }
            //}
            else if (string.IsNullOrEmpty(model.UserImage))
            {
                if (UserImageFile == null)
                {
                    ViewBag.Message = "Please upload selfie image";
                }
            }
            else if (model.IssueFromStateID == 0)
            {
                ViewBag.Message = "Please select issue by state";
            }
            else if (model.IssueFromDistrictID == 0)
            {
                ViewBag.Message = "Please select issue by district";
            }
            else if (string.IsNullOrEmpty(model.MotherName))
            {
                ViewBag.Message = "Please enter mother's name";
            }
            else if (model.MeritalStatus == 2)
            {
                if (string.IsNullOrEmpty(model.SpouseName))
                {
                    ViewBag.Message = "Please enter spouse name";
                }
            }

            ViewBag.Day = (1, 2, 3, 4, 5);
            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(model.MemberId);
            AddUser res = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    if (res.ContactNumber != model.ContactNumber)
                    {
                        AddUser outobjectContactNumber = new AddUser();
                        GetUser inobjectContactNumber = new GetUser();
                        inobjectContactNumber.ContactNumber = model.ContactNumber;
                        AddUser resContactNumber = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobjectContactNumber, outobjectContactNumber);
                        if (resContactNumber != null && resContactNumber.Id != 0)
                        {
                            ViewBag.Message = "ContactNumber Already Exists";
                        }
                    }
                    if (res.DocumentNumber != model.DocumentNumber)
                    {
                        AddUser outobjectDocumentNumber = new AddUser();
                        GetUser inobjectDocumentNumber = new GetUser();
                        inobjectDocumentNumber.DocumentNumber = model.DocumentNumber;
                        AddUser resDocumentNumber = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobjectDocumentNumber, outobjectDocumentNumber);
                        if (resDocumentNumber != null && resDocumentNumber.Id != 0)
                        {
                            ViewBag.Message = "DocumentNumber Already Exists";
                        }
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        res.FirstName = model.FirstName.Replace("<", "").Replace(">", "");
                        res.MiddleName = model.MiddleName.Replace("<", "").Replace(">", "");
                        res.LastName = model.LastName.Replace("<", "").Replace(">", "");
                        res.ContactNumber = model.ContactNumber.Replace("<", "").Replace(">", "");
                        res.DateofBirth = model.Day + "/" + model.Month + "/" + model.Year;
                        res.DOBType2 = model.DOBType2;
                        //res.DateofBirth = model.DateofBirth;
                        res.Email = model.Email.Replace("<", "").Replace(">", "");
                        res.EmployeeType = model.EmployeeType;
                        res.Gender = model.Gender;
                        res.Nationality = model.Nationality;
                        res.MeritalStatus = model.MeritalStatus;

                        res.City = model.City;
                        res.DistrictId = Convert.ToInt32(model.DistrictId);
                        res.HouseNumber = model.HouseNumber;
                        res.Municipality = model.Municipality;
                        res.MunicipalityId = Convert.ToInt32(model.MunicipalityId);
                        res.StateId = Convert.ToInt32(model.StateId);
                        res.State = model.State;
                        res.StreetName = model.StreetName;
                        res.WardNumber = model.WardNumber;

                        res.CurrentDistrict = model.CurrentDistrict;
                        res.CurrentDistrictId = Convert.ToInt32(model.CurrentDistrictId);
                        res.CurrentHouseNumber = model.CurrentHouseNumber;
                        res.CurrentMunicipality = model.CurrentMunicipality;
                        res.CurrentMunicipalityId = Convert.ToInt32(model.CurrentMunicipalityId);
                        res.CurrentState = model.CurrentState;
                        res.CurrentStateId = Convert.ToInt32(model.CurrentStateId);
                        res.CurrentStreetName = model.CurrentStreetName;
                        res.CurrentWardNumber = model.CurrentWardNumber;
                        res.ProofType = model.ProofType;
                        res.FatherName = model.FatherName.Replace("<", "").Replace(">", "");
                        res.GrandFatherName = model.GrandFatherName.Replace("<", "").Replace(">", "");
                        res.MotherName = model.MotherName.Replace("<", "").Replace(">", "");
                        res.SpouseName = model.SpouseName.Replace("<", "").Replace(">", "");
                        res.IssueFromDistrictID = model.IssueFromDistrictID;
                        res.IssueFromDistrictName = model.IssueFromDistrictName;
                        res.IssueFromStateID = model.IssueFromStateID;
                        res.IssueFromStateName = model.IssueFromStateName;
                        //res.ExpiryDate = model.ExpiryDate;
                        res.ExpiryDate = model.ExpiryDay + "/" + model.ExpiryMonth + "/" + model.ExpiryYear;
                        res.ExpiryDateType = model.ExpiryDateType;
                        //res.IssueDate = model.IssueDate;
                        res.IssueDate = model.IssueDay + "/" + model.IssueMonth + "/" + model.IssueYear;
                        res.IssueDateType = model.IssueDateType;
                        res.IssuedBy = model.IssuedBy;
                        res.DocumentNumber = model.DocumentNumber;
                        res.RoleId = (int)model.UserRoleEnumType;
                        res.RoleName = Enum.GetName(typeof(AddUser.UserRoles), model.UserRoleEnumType);
                        res.ApprovedorRejectedByName = Session["AdminUserName"].ToString();
                        res.ApprovedorRejectedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        string Remarks = "Updated User Detail (MemberId:" + res.MemberId + ") by(AdminId:" + Session["AdminMemberId"] + ")";
                        res.Remarks = Remarks;
                        if (string.IsNullOrEmpty(res.Email))
                        {
                            res.IsEmailVerified = false;
                        }
                        res.UpdatedDate = DateTime.UtcNow;
                        res.KYCReviewDate = DateTime.UtcNow;
                        if (NationalIdProofFrontFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(NationalIdProofFrontFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/UserDocuments/Images/") + fileName);
                            NationalIdProofFrontFile.SaveAs(filePath);
                            res.NationalIdProofFront = fileName;
                        }
                        if (NationalIdProofBackFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(NationalIdProofBackFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/UserDocuments/Images/") + fileName);
                            NationalIdProofBackFile.SaveAs(filePath);
                            res.NationalIdProofBack = fileName;
                        }
                        if (UserImageFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(UserImageFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/UserDocuments/Images/") + fileName);
                            UserImageFile.SaveAs(filePath);
                            res.UserImage = fileName;
                        }
                        if (Session["AdminMemberId"] != null)
                        {
                            res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.UpdatedByName = Convert.ToString(Session["AdminUserName"]);
                            bool status = RepCRUD<AddUser, GetUser>.Update(res, "user");
                            if (status)
                            {
                                if (res.IsEmailVerified == false)
                                {
                                    AddVerification outobjectVerification = new AddVerification();
                                    GetVerification inobjectVerification = new GetVerification();
                                    inobjectVerification.PhoneNumber = res.ContactNumber;
                                    inobjectVerification.VerificationType = (int)AddVerification.VerifyType.EmailVerification;
                                    AddVerification resverification = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobjectVerification, outobjectVerification);
                                    if (resverification != null && resverification.Id != 0)
                                    {
                                        resverification.IsVerified = false;
                                        resverification.Email = "";
                                        resverification.UpdatedDate = DateTime.UtcNow;
                                        RepCRUD<AddVerification, GetVerification>.Update(resverification, "verification");
                                    }
                                }

                                Int64 AdminMemberId = Convert.ToInt64(Session["AdminMemberId"]);
                                string AdminUserName = Session["AdminUserName"].ToString();
                                RepUser.UpdateKYCDetailsAdminEdit(res, Remarks, res.IsKYCApproved.ToString(), AdminMemberId.ToString(), AdminUserName);
                                ViewBag.SuccessMessage = "Successfully Updated KYC status.";
                                Common.AddLogs(Remarks, true, (int)AddLog.LogType.User, res.MemberId, res.FirstName, false, "", "", 0, AdminMemberId, AdminUserName);
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
                ViewBag.Message = "User not found.";
            }

            inobject.MemberId = Convert.ToInt64(model.MemberId);
            model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);

            List<SelectListItem> genderlist = CommonHelpers.GetSelectList_Gender(model);
            List<SelectListItem> meritallist = CommonHelpers.GetSelectList_MeritalStatus(model);
            List<SelectListItem> nationalitylist = CommonHelpers.GetSelectList_Nationality(model);
            List<SelectListItem> prooftypelist = CommonHelpers.GetSelectList_ProofType(model);
            List<SelectListItem> occupationlist = CommonHelpers.GetSelectList_Occupation(model);
            List<SelectListItem> statelist = new List<SelectListItem>();
            List<SelectListItem> districtlist = new List<SelectListItem>();
            List<SelectListItem> municipalitylist = new List<SelectListItem>();
            List<SelectListItem> currentstatelist = new List<SelectListItem>();
            List<SelectListItem> currentdistrictlist = new List<SelectListItem>();
            List<SelectListItem> currentmunicipalitylist = new List<SelectListItem>();
            List<SelectListItem> issuestatelist = new List<SelectListItem>();
            List<SelectListItem> issuedistrictlist = new List<SelectListItem>();

            DateTime dob;
            DateTime issue;
            DateTime expiry;

            if (DateTime.TryParseExact(model.DateofBirth, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
            {
                string formattedDob = dob.ToString("dd/MM/yyyy");
                model.Day = dob.Day;
                model.Month = dob.Month;
                int year = dob.Year;
            }
            if (DateTime.TryParseExact(model.IssueDate, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out issue))
            {
                string formattedissue = issue.ToString("dd/MM/yyyy");
                model.IssueDay = issue.Day;
                model.IssueMonth = issue.Month;
                int year = issue.Year;
            }
            if (DateTime.TryParseExact(model.ExpiryDate, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiry))
            {
                string formattedissue = expiry.ToString("dd/MM/yyyy");
                model.ExpiryDay = expiry.Day;
                model.ExpiryMonth = expiry.Month;
                int year = expiry.Year;
            }

            try
            {

                if (model.EmployeeType != 0)
                {
                    occupationlist.Find(c => c.Value == model.EmployeeType.ToString()).Selected = true;
                }
                //PermanantAddress
                statelist = CommonHelpers.GetSelectList_State(model.StateId);
                if (model.StateId != 0)
                {
                    statelist.Find(c => c.Value == model.StateId.ToString()).Selected = true;
                }
                districtlist = CommonHelpers.GetSelectList_District(model.StateId, model.DistrictId);
                if (model.DistrictId != 0)
                {
                    districtlist.Find(c => c.Value == model.DistrictId.ToString()).Selected = true;
                }
                municipalitylist = CommonHelpers.GetSelectList_Municipality(model.DistrictId, model.MunicipalityId);
                if (model.MunicipalityId != 0)
                {
                    municipalitylist.Find(c => c.Value == model.MunicipalityId.ToString()).Selected = true;
                }
                //CurrentAddress
                currentstatelist = CommonHelpers.GetSelectList_State(model.StateId);
                if (model.CurrentStateId != 0)
                {
                    currentstatelist.Find(c => c.Value == model.CurrentStateId.ToString()).Selected = true;
                }
                currentdistrictlist = CommonHelpers.GetSelectList_CurrentDistrict(model);
                if (model.CurrentDistrictId != 0)
                {
                    currentdistrictlist.Find(c => c.Value == model.CurrentDistrictId.ToString()).Selected = true;
                }
                currentmunicipalitylist = CommonHelpers.GetSelectList_CurrentMunicipality(model);
                if (model.CurrentMunicipalityId != 0)
                {
                    currentmunicipalitylist.Find(c => c.Value == model.CurrentMunicipalityId.ToString()).Selected = true;
                }

                //IssueFrom State And District
                issuestatelist = CommonHelpers.GetSelectList_State(model.StateId);
                if (model.IssueFromStateID != 0)
                {
                    issuestatelist.Find(c => c.Value == model.IssueFromStateID.ToString()).Selected = true;
                }
                issuedistrictlist = CommonHelpers.GetSelectList_IssueDistrict(model);
                if (model.IssueFromDistrictID != 0)
                {
                    issuedistrictlist.Find(c => c.Value == model.IssueFromDistrictID.ToString()).Selected = true;
                }

            }
            catch (Exception ex)
            {

            }
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(model.MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.Gender = genderlist;
            ViewBag.MeritalStatus = meritallist;
            ViewBag.Nationality = nationalitylist;
            ViewBag.EmployeeType = occupationlist;
            ViewBag.StateId = statelist;
            ViewBag.DistrictId = districtlist;
            ViewBag.MunicipalityId = municipalitylist;
            ViewBag.CurrentStateId = currentstatelist;
            ViewBag.CurrentDistrictId = currentdistrictlist;
            ViewBag.CurrentMunicipalityId = currentmunicipalitylist;
            ViewBag.ProofType = prooftypelist;
            ViewBag.IssueFromStateID = issuestatelist;
            ViewBag.IssueFromDistrictID = issuedistrictlist;
            ViewBag.QRCode = Common.GetQRReferCode(model);
            ViewBag.MeritalStatuss = model.MeritalStatus;
            return View(model);
        }

        public ActionResult GetDistrictList(string StateId)
        {
            List<SelectListItem> districtlist = CommonHelpers.GetSelectList_Districtonchange(StateId);
            ViewBag.DistrictId = districtlist;
            return Json(districtlist);
        }

        public ActionResult GetMunicipalityList(string DistrictId)
        {
            List<SelectListItem> municipalitylist = CommonHelpers.GetSelectList_Municipalityonchange(DistrictId);
            ViewBag.MunicipalityId = municipalitylist;
            return Json(municipalitylist);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ResetPassword(string MemberId = "0")
        {

            if (string.IsNullOrEmpty(MemberId))
            {
                return RedirectToAction("Index");
            }
            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(MemberId);
            AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
            if (!string.IsNullOrEmpty(MemberId) && (model == null || model.Id == 0 || MemberId == "0"))
            {
                return RedirectToAction("Index");
            }
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQRReferCode(model);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult ResetPassword(AddUser vmodel, string Password)
        {
            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(vmodel.MemberId);
            AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
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
                model.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                model.UpdatedByName = Convert.ToString(Session["AdminUserName"]);
                bool status = RepCRUD<AddUser, GetUser>.Update(model, "user");
                if (status)
                {
                    Common.AddLogs("Password updated successfully (MemberId:" + vmodel.MemberId + " ) by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User);
                    ViewBag.SuccessMessage = "Password updated successfully.";
                }
            }
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(vmodel.MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQRReferCode(model);
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreditDebit(string MemberId = "0")
        {
            if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
            {
                return RedirectToAction("Index");
            }
            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(MemberId);
            AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
            if (!string.IsNullOrEmpty(MemberId) && (model == null || model.Id == 0 || MemberId == "0"))
            {
                return RedirectToAction("Index");
            }
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQRReferCode(model);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreditDebit(AddUser vmodel, string TransactionAmount, string Type, string TransactionType, string AdminRemarksCD)
        {

            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit" + Environment.NewLine);
            // Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(vmodel.MemberId);
            AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
            if (model != null && model.Id != 0)
            {
                log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- sp_Users_Get executed" + Environment.NewLine);
                //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- sp_Users_Get executed" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                string UserMessage = string.Empty;
                string Referenceno = new CommonHelpers().GenerateUniqueId();
                string msg = "";
                if (Type == "0")
                {
                    msg = RepTransactions.WalletUpdateFromAdmin(vmodel.MemberId, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD, "");
                }
                else if (Type == "1")
                {
                    msg = RepTransactions.MPCoinsUpdateFromAdmin(vmodel.MemberId, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD);
                }
                if (msg.ToString().ToLower() == "success")
                {
                    ViewBag.SuccessMessage = UserMessage;
                }
                else
                {
                    ViewBag.Message = msg;
                    Common.AddLogs("Not Updated user", true, (int)AddLog.LogType.User);
                }
            }
            else
            {
                ViewBag.Message = "User Not Found";
            }
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(vmodel.MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQRReferCode(model);
            return View(model);
        }


        [HttpGet]
        [Authorize]
        public ActionResult RefCodeManage(string MemberId = "0")
        {
            if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
            {
                return RedirectToAction("Index");
            }
            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(MemberId);
            AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
            if (!string.IsNullOrEmpty(MemberId) && (model == null || model.Id == 0 || MemberId == "0"))
            {
                return RedirectToAction("Index");
            }
            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQRReferCode(model);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult RefCodeManage(AddUser vmodel, string AdminRemarks)
        {

            AddUser outobject = new AddUser();
            GetUser inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(vmodel.MemberId);
            AddUser resGetRecord = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
            if (resGetRecord != null && resGetRecord.Id != 0)
            {
                string AdminMemberId = Convert.ToString(Session["AdminMemberId"]);
                string AdminMemberName = Convert.ToString(Session["AdminUserName"]);
                string msg = RepUser.RefCodeUpdateFromAdmin(vmodel.RefCode, AdminRemarks, AdminMemberId, AdminMemberName, ref resGetRecord);
                if (msg.ToString().ToLower() == "success")
                {
                    vmodel = resGetRecord;
                    ViewBag.SuccessMessage = "RefCode updated successfully.";
                }
                else
                {
                    ViewBag.Message = msg;
                    Common.AddLogs("Not Updated user", true, (int)AddLog.LogType.User);
                }
            }
            else
            {
                ViewBag.Message = "User Not Found";
            }

            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(vmodel.MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactions = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            ViewBag.QRCode = Common.GetQRReferCode(resGetRecord);
            return View(resGetRecord);
        }


        [HttpPost]
        [Authorize]
        public ActionResult ExportExcel(Int32 Take, Int32 Skip, string Sort, string SortOrder, Int64 MemberId, string ContactNo, string Name, string Email, string FromDate, string ToDate, string KYCStatus, string RoleId, string IsActive, string RefCode, string OldUserStatuses, string RefId)
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Full Name");
            columns.Add("Joining Date");
            columns.Add("Email");
            columns.Add("Gender");
            columns.Add("Wallet");
            columns.Add("Status");
            columns.Add("KYC Status");
            //Sort = columns[Convert.ToInt32(Sort)];
            var fileName = "Users-" + DateTime.Now.ToShortDateString() + ".xlsx";
            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);
            GetUser user = new GetUser();

            user.MemberId = Convert.ToInt64(MemberId);
            user.ContactNumber = ContactNo;
            user.FirstName = Name;
            user.Email = Email;
            user.DateFrom = FromDate;
            user.DateTo = ToDate;
            if (KYCStatus != "0")
            {
                user.IsKYCApproved = Convert.ToInt32(KYCStatus);
            }
            else
            {
                user.IsKYCApproved = -1;
            }
            if (RoleId != "0")
            {
                user.RoleId = Convert.ToInt32(RoleId);
            }
            else
            {
                user.RoleId = -1;
            }
            user.RefCode = RefCode;
            if (!string.IsNullOrEmpty(RefId))
            {
                user.RefId = Convert.ToInt32(RefId);
            }
            user.CheckActive = Convert.ToInt32(IsActive);
            if (OldUserStatuses == "1")
            {
                user.CheckPasswordReset = "1";
            }
            else if (OldUserStatuses == "2")
            {
                user.CheckNotPasswordReset = "1";
            }
            else if (OldUserStatuses == "3")
            {
                user.CheckPin = "1";
            }
            else if (OldUserStatuses == "4")
            {
                user.CheckNotPin = "1";
            }
            else if (OldUserStatuses == "5")
            {
                user.CheckFirstName = "1";
            }
            else if (OldUserStatuses == "6")
            {
                user.CheckNotFirstName = "1";
            }
            Int32 recordFiltered = 0;
            DataTable dt = user.GetData(Sort, SortOrder, Skip, Take, "", ref recordFiltered);

            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Name", "ContactNumber", "IndiaDate", "Email", "GenderName", "TotalAmount", "VerificationCode", "RoleName", "StatusName", "KYCName", "DeviceId");
                //dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["Name"].ColumnName = "Full Name";
                dt.Columns["IndiaDate"].ColumnName = "Joining Date";
                dt.Columns["GenderName"].ColumnName = "Gender";
                dt.Columns["TotalAmount"].ColumnName = "Wallet (Rs)";
                dt.Columns["VerificationCode"].ColumnName = "OTP";
                dt.Columns["StatusName"].ColumnName = "Status";
                dt.Columns["KYCName"].ColumnName = "KYC";
                dt.Columns["DeviceId"].ColumnName = "Device Id";
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "User(" + DateTime.Now.ToString("MMM") + ")");
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    ws.Columns().AdjustToContents();  // Adjust column width
                    ws.Rows().AdjustToContents();
                    wb.SaveAs(fullPath);
                }
            }
            var errorMessage = "you can return the errors here!";
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage });
        }

        //[HttpPost]
        //[Authorize]
        //public ActionResult ExportExcelOldUser()
        //{
        //    var fileName = "";
        //    var errorMessage = "";
        //    AddExportData outobject = new AddExportData();
        //    GetExportData inobject = new GetExportData();
        //    inobject.Type = (int)AddExportData.ExportType.User;
        //    inobject.StartDate = DateTime.UtcNow.AddHours(-4).ToString("MM-dd-yyyy hh:mm:ss tt");
        //    inobject.EndDate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");
        //    //inobject.CheckCreatedDate = DateTime.Now.ToString();
        //    AddExportData res = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject, outobject);
        //    if (res != null && res.Id != 0)
        //    {
        //        errorMessage = "Your File was already generated! You can download the file from Download link. ";
        //    }
        //    else
        //    {
        //        fileName = "OldUsers-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
        //        //Save the file to server temp folder
        //        string fullPath = Path.Combine(Server.MapPath("~/ExportData/User"), fileName);

        //        GetUser user = new GetUser();
        //        DataTable dt = user.GetData_OldUserExport();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            //dt = dt.DefaultView.ToTable(false, "MemberId", "ContactNumber", "Email", "FirstName", "LastName", "TotalAmount", "DateofBirth", "DOBType", "Gender", "Nationality", "CreatedDate", "MaritalStatus", "State", "District", "Address", "StreetName", "Municipality", "WardNumber", "CurrentState", "CurrentStreetName", "CurrentMunicipality", "CurrentWardNumber", "CurrentDistrict", "CurrentHouseNumber", "FatherName", "MotherName", "GrandFatherName", "SpouseName", "DocumentType", "DocumentNumber", "Occupation", "ExpiryDate", "IssueDate", "IssueFromDistrictName", "IssueFromStateName", "KycStatus", "LastLogin", "ActiveStatus", "UserStatus");

        //            AddExportData outobject_file = new AddExportData();
        //            GetExportData inobject_file = new GetExportData();
        //            inobject_file.Type = (int)AddExportData.ExportType.User;
        //            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
        //            if (res_file != null && res_file.Id != 0)
        //            {
        //                string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/User"), res_file.FilePath);
        //                if (System.IO.File.Exists(oldfilePath))
        //                {
        //                    System.IO.File.Delete(oldfilePath);
        //                }
        //                res_file.FilePath = fullPath;
        //                res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
        //                bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
        //                if (status)
        //                {

        //                    Common.AddLogs("User's Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
        //                }
        //            }
        //            else
        //            {
        //                AddExportData export = new AddExportData();
        //                export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
        //                export.CreatedByName = Session["AdminUserName"].ToString();
        //                export.FilePath = fileName;
        //                export.Type = (int)AddExportData.ExportType.User;
        //                export.IsActive = true;
        //                export.IsApprovedByAdmin = true;
        //                Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
        //                if (Id > 0)
        //                {
        //                    Common.AddLogs("User's Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
        //                }
        //            }
        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                var ws = wb.Worksheets.Add(dt, "User(" + DateTime.Now.ToString("MMM") + ")");
        //                ws.Tables.FirstOrDefault().ShowAutoFilter = false;
        //                ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
        //                ws.Columns().AdjustToContents();  // Adjust column width
        //                ws.Rows().AdjustToContents();
        //                wb.SaveAs(fullPath);
        //            }
        //        }
        //    }
        //    //Return the Excel file name
        //    return Json(new { fileName = fileName, errorMessage = errorMessage });
        //}

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelOldUser(string Sort, string SortOrder, Int64 MemberId, string ContactNo, string Name, string Email, string FromDate, string ToDate, string KYCStatus, string RoleId, string IsActive, string RefCode, string OldUserStatuses, string RefId, string RefCodeAttempted)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(FromDate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(ToDate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                {
                    DateTime d1 = Convert.ToDateTime(FromDate);
                    DateTime d2 = Convert.ToDateTime(ToDate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 5)
                    {
                        errorMessage = "You cannot export report more than 5 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    AddExportData outobject = new AddExportData();
                    GetExportData inobject = new GetExportData();
                    inobject.Type = (int)AddExportData.ExportType.User;
                    inobject.StartDate = DateTime.UtcNow.AddHours(-4).ToString("MM-dd-yyyy hh:mm:ss tt");
                    inobject.EndDate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");
                    //inobject.CheckCreatedDate = DateTime.Now.ToString();
                    AddExportData res = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        errorMessage = "Your File was already generated! You can download the file from Download link. ";
                    }
                }
                if (errorMessage == "")
                {
                    fileName = "OldUsers-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                    //Save the file to server temp folder
                    string fullPath = Path.Combine(Server.MapPath("~/ExportData/User"), fileName);

                    GetUser user = new GetUser();
                    if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                    {
                        user.MemberId = Convert.ToInt64(MemberId);
                        user.ContactNumber = ContactNo;
                        user.FirstName = Name;
                        user.Email = Email;
                        user.DateFrom = FromDate;
                        user.DateTo = ToDate;
                        if (KYCStatus != "0")
                        {
                            user.IsKYCApproved = Convert.ToInt32(KYCStatus);
                        }
                        else
                        {
                            user.IsKYCApproved = -1;
                        }
                        if (RoleId != "0")
                        {
                            user.RoleId = Convert.ToInt32(RoleId);
                        }
                        else
                        {
                            user.RoleId = -1;
                        }
                        user.RefCode = RefCode;
                        user.RefCodeAttempted = RefCodeAttempted;
                        if (!string.IsNullOrEmpty(RefId))
                        {
                            user.RefId = Convert.ToInt32(RefId);
                        }
                        user.CheckActive = Convert.ToInt32(IsActive);
                        if (OldUserStatuses == "1")
                        {
                            user.CheckPasswordReset = "1";
                        }
                        else if (OldUserStatuses == "2")
                        {
                            user.CheckNotPasswordReset = "1";
                        }
                        else if (OldUserStatuses == "3")
                        {
                            user.CheckPin = "1";
                        }
                        else if (OldUserStatuses == "4")
                        {
                            user.CheckNotPin = "1";
                        }
                        else if (OldUserStatuses == "5")
                        {
                            user.CheckFirstName = "1";
                        }
                        else if (OldUserStatuses == "6")
                        {
                            user.CheckNotFirstName = "1";
                        }
                    }
                    DataTable dt = user.GetData_OldUserExport();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //dt = dt.DefaultView.ToTable(false, "MemberId", "ContactNumber", "Email", "FirstName", "LastName", "TotalAmount", "DateofBirth", "DOBType", "Gender", "Nationality", "CreatedDate", "MaritalStatus", "State", "District", "Address", "StreetName", "Municipality", "WardNumber", "CurrentState", "CurrentStreetName", "CurrentMunicipality", "CurrentWardNumber", "CurrentDistrict", "CurrentHouseNumber", "FatherName", "MotherName", "GrandFatherName", "SpouseName", "DocumentType", "DocumentNumber", "Occupation", "ExpiryDate", "IssueDate", "IssueFromDistrictName", "IssueFromStateName", "KycStatus", "LastLogin", "ActiveStatus", "UserStatus");

                        AddExportData outobject_file = new AddExportData();
                        GetExportData inobject_file = new GetExportData();
                        inobject_file.Type = (int)AddExportData.ExportType.User;
                        AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
                        if (res_file != null && res_file.Id != 0)
                        {
                            string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/User"), res_file.FilePath);
                            if (System.IO.File.Exists(oldfilePath))
                            {
                                System.IO.File.Delete(oldfilePath);
                            }
                            res_file.FilePath = fullPath;
                            res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                            if (status)
                            {

                                Common.AddLogs("User's Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
                            }
                        }
                        else
                        {
                            AddExportData export = new AddExportData();
                            export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            export.CreatedByName = Session["AdminUserName"].ToString();
                            export.FilePath = fileName;
                            export.Type = (int)AddExportData.ExportType.User;
                            export.IsActive = true;
                            export.IsApprovedByAdmin = true;
                            Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                            if (Id > 0)
                            {
                                Common.AddLogs("User's Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
                            }
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "User(" + DateTime.Now.ToString("MMM") + ")");
                            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Columns().AdjustToContents();  // Adjust column width
                            ws.Rows().AdjustToContents();
                            wb.SaveAs(fullPath);
                        }
                    }
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

        [HttpPost]
        [Authorize]
        public ActionResult DownloadOldUserFileName()
        {
            var errorMessage = "";
            AddExportData outobject_file = new AddExportData();
            GetExportData inobject_file = new GetExportData();
            inobject_file.Type = (int)AddExportData.ExportType.User;
            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
            if (res_file != null && res_file.Id != 0)
            {
                return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
            }
            else
            {
                errorMessage = "";
                return Json(new { errorMessage = errorMessage });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult DownloadOldUser(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/User"), fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                    //System.IO.File.Delete(fullPath);
                    return File(fileByteArray, "application/vnd.ms-excel", fileName);
                }
                else
                {
                    return RedirectToAction("/Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("/Index");
            }
        }
        [HttpGet]
        public ActionResult Download(string fileName)
        {
            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }

        [HttpGet]
        [Authorize]
        public ActionResult InactiveUsers()
        {
            AddUser outobject = new AddUser();
            return View(outobject);
        }

        [Authorize]
        public JsonResult GetInactiveUsersLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Full Name");
            columns.Add("Contact No");
            columns.Add("Joining Date");
            columns.Add("Email");
            columns.Add("DOB");
            columns.Add("Gender");
            columns.Add("Wallet");
            columns.Add("Status");
            columns.Add("KYC Status");

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
            string RefId = context.Request.Form["RefId"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string Email = context.Request.Form["Email"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string LoginFromDate = context.Request.Form["LoginFromDate"];
            string LoginToDate = context.Request.Form["LoginToDate"];
            string KycStatus = context.Request.Form["KycStatus"];
            string RoleId = context.Request.Form["RoleId"];
            //string IsActive = context.Request.Form["IsActive"];
            string RefCode = context.Request.Form["RefCode"];
            string OldUserStatuses = context.Request.Form["OldUserStatuses"];
            string OldAndNewUsers = context.Request.Form["OldAndNewUsers"];
            string RefCodeAttempted = context.Request.Form["RefCodeAttempted"];
            string DocumentNumber = context.Request.Form["DocumentNumber"];
            string DeviceId = context.Request.Form["DeviceId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<AddUser> trans = new List<AddUser>();

            GetUser w = new GetUser();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            if (!string.IsNullOrEmpty(RefId))
            {
                w.RefId = Convert.ToInt64(RefId);
            }
            w.ContactNumber = ContactNumber;
            w.FirstName = Name;
            w.DateFrom = fromdate;
            w.DateTo = todate;
            w.Email = Email;
            w.LoginFromDate = LoginFromDate;
            w.LoginToDate = LoginToDate;
            w.DocumentNumber = DocumentNumber;
            w.DeviceId = DeviceId;
            if (KycStatus != "-1")
            {
                w.IsKYCApproved = Convert.ToInt32(KycStatus);
            }
            else
            {
                w.IsKYCApproved = -1;
            }
            if (RoleId != "0")
            {
                w.RoleId = Convert.ToInt32(RoleId);
            }
            else
            {
                w.RoleId = -1;
            }
            if (!string.IsNullOrEmpty(RefId))
            {
                w.RefId = Convert.ToInt64(RefId);
            }
            w.CheckActive = 0;
            w.RefCode = RefCode;
            if (OldUserStatuses == "1")
            {
                w.CheckPasswordReset = "1";
            }
            else if (OldUserStatuses == "2")
            {
                w.CheckNotPasswordReset = "1";
            }
            else if (OldUserStatuses == "3")
            {
                w.CheckPin = "1";
            }
            else if (OldUserStatuses == "4")
            {
                w.CheckNotPin = "1";
            }
            else if (OldUserStatuses == "5")
            {
                w.CheckFirstName = "1";
            }
            else if (OldUserStatuses == "6")
            {
                w.CheckNotFirstName = "1";
            }

            if (OldAndNewUsers == "1")
            {
                w.CheckOldAndNewUser = 1;
            }
            else if (OldAndNewUsers == "0")
            {
                w.CheckOldAndNewUser = 0;
            }
            w.RefCodeAttempted = RefCodeAttempted;

            Int32 recordFiltered = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby, ref recordFiltered);

            trans = (from DataRow row in dt.Rows

                     select new AddUser
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         FirstName = row["Name"].ToString(),
                         Gender = Convert.ToInt32(row["Gender"]),
                         GenderName = @Enum.GetName(typeof(AddUser.sex), Convert.ToInt64(row["Gender"])),
                         Email = row["Email"].ToString(),
                         TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         IsKYCApproved = Convert.ToInt32(row["IsKYCApproved"]),
                         RoleId = Convert.ToInt32(row["RoleId"]),
                         RoleName = Convert.ToString(row["RoleName"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         IsPhoneVerified = Convert.ToBoolean(row["IsPhoneVerified"]),
                         IsEmailVerified = Convert.ToBoolean(row["IsEmailVerified"]),
                         TotalUserCount = recordFiltered.ToString(),
                         RefCodeAttempted = Convert.ToString(row["RefCodeAttempted"]),
                         DateofBirthdt = row["DateofBirth"].ToString(),
                         DeviceId = row["DeviceId"].ToString(),
                         VerificationCode = row["VerificationCode"].ToString(),
                         TotalRewardPoints = Convert.ToDecimal(row["TotalRewardPoints"].ToString())
                     }).ToList();



            DataTableResponse<List<AddUser>> objDataTableResponse = new DataTableResponse<List<AddUser>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelActiveUserNoTxn()
        {
            var fileName = "";
            var errorMessage = "";
            if (errorMessage == "")
            {
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    errorMessage = "You cannot download the dump.";
                }
                else
                {
                    fileName = "ActiveUsersNoTxn-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                    //Save the file to server temp folder
                    string fullPath = Path.Combine(Server.MapPath("~/ExportData/User"), fileName);

                    GetUser user = new GetUser();

                    DataTable dt = user.GetDataDump_ActiveUserNoTxnExport();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddExportData outobject_file = new AddExportData();
                        GetExportData inobject_file = new GetExportData();
                        inobject_file.Type = (int)AddExportData.ExportType.User;
                        AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
                        if (res_file != null && res_file.Id != 0)
                        {
                            string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/User"), res_file.FilePath);
                            if (System.IO.File.Exists(oldfilePath))
                            {
                                System.IO.File.Delete(oldfilePath);
                            }
                            res_file.FilePath = fullPath;
                            res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                            if (status)
                            {

                                Common.AddLogs("User's Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
                            }
                        }
                        else
                        {
                            AddExportData export = new AddExportData();
                            export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            export.CreatedByName = Session["AdminUserName"].ToString();
                            export.FilePath = fileName;
                            export.Type = (int)AddExportData.ExportType.User;
                            export.IsActive = true;
                            export.IsApprovedByAdmin = true;
                            Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                            if (Id > 0)
                            {
                                Common.AddLogs("User's Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
                            }
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "User(" + DateTime.Now.ToString("MMM") + ")");
                            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Columns().AdjustToContents();  // Adjust column width
                            ws.Rows().AdjustToContents();
                            wb.SaveAs(fullPath);
                        }
                    }
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

    }
}