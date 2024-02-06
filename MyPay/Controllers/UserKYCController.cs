using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPay.Controllers
{
    public class UserKYCController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            AddUser outobject = new AddUser();
            //GetUser inobject = new GetUser();
            //List<AddUser> objList = RepCRUD<GetUser, AddUser>.GetRecordList("sp_Users_Get", inobject, outobject);
            //Req_Web_User model = new Req_Web_User();
            //model.objData = objList;

            //List<SelectListItem> KYCStatuslist = CommonHelpers.GetSelectList_KYCStatus(model.IsKycApproved);
            //ViewBag.KYCStatuslist = KYCStatuslist;

            ViewBag.DumpURL = Common.dumpurl;
            return View(outobject);
        }
        //[HttpPost]
        //[Authorize]
        //public ActionResult Index(Req_Web_User model)
        //{
        //    AddUser outobject = new AddUser();
        //    GetUser inobject = new GetUser();
        //    if (!string.IsNullOrEmpty(model.ContactNumber))
        //    {
        //        inobject.ContactNumber = model.ContactNumber;
        //    }
        //    if (!string.IsNullOrEmpty(model.Email))
        //    {
        //        inobject.Email = model.Email;
        //    }
        //    if (!string.IsNullOrEmpty(model.StartDate))
        //    {
        //        inobject.DateFrom = model.StartDate;
        //    }
        //    if (!string.IsNullOrEmpty(model.ToDate))
        //    {
        //        inobject.DateTo = model.ToDate;
        //    }
        //    List<AddUser> objList = RepCRUD<GetUser, AddUser>.GetRecordList("sp_Users_Get", inobject, outobject);

        //    List<SelectListItem> KYCStatuslist = CommonHelpers.GetSelectList_KYCStatus(model.IsKycApproved);
        //    ViewBag.KYCStatuslist = KYCStatuslist;
        //    model.objData = objList;
        //    return View(model);
        //}


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
            //Common.GenerateExcel(dtObject, Response, "UsersList");
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult UserKYCDetails(string MemberId)
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
            //GetOccupation
            AddOccupation outobject_occ = new AddOccupation();
            GetOccupation inobject_occ = new GetOccupation();
            if (model.EmployeeType == 0)
            {
                inobject_occ.Id = 0;
            }
            else
            {
                inobject_occ.Id = model.EmployeeType;
            }
            AddOccupation model_occ = RepCRUD<GetOccupation, AddOccupation>.GetRecord(Models.Common.Common.StoreProcedures.sp_Occupation_Get, inobject_occ, outobject_occ);
            ViewBag.CategoryName = model_occ.CategoryName;
            List<SelectListItem> KYCStatuslist = CommonHelpers.GetSelectList_KYCStatus(model.IsKYCApproved);
            ViewBag.IsKYCApproved = KYCStatuslist;

            //KYCRemarks List in DropDownList
            AddKYCRemarks outobject_remarks = new AddKYCRemarks();
            GetKYCRemarks inobject_remarks = new GetKYCRemarks();
            inobject_remarks.CheckActive = 1;
            inobject_remarks.CheckDelete = 0;
            List<AddKYCRemarks> remarkslist = RepCRUD<GetKYCRemarks, AddKYCRemarks>.GetRecordList(Common.StoreProcedures.sp_KYCRemarks_Get, inobject_remarks, outobject_remarks);
            List<SelectListItem> objkycremarks_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Remarks ---";
                objDefault.Selected = true;
                objkycremarks_SelectList.Add(objDefault);
            }

            for (int i = 0; i < remarkslist.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = remarkslist[i].Id.ToString();
                objItem.Text = remarkslist[i].Title.ToString();
                objkycremarks_SelectList.Add(objItem);
            }
            ViewBag.Remarks = objkycremarks_SelectList;

            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
            GetTransactionCount inobjectTrans = new GetTransactionCount();
            inobjectTrans.MemberId = Convert.ToInt64(model.MemberId);
            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
            if (modelTrans != null)
            {
                ViewBag.TotalTransactins = modelTrans.Count;
                ViewBag.SumTransactions = modelTrans.TotalAmount.ToString("f2");
            }
            //AddKYCStatusHistory outobject_kycstatus = new AddKYCStatusHistory();
            //GetKYCStatusHistory inobject_kycstatus = new GetKYCStatusHistory();
            //inobject_kycstatus.MemberId = Convert.ToInt64(MemberId);
            //List<AddKYCStatusHistory> modelkycstatus = new List<AddKYCStatusHistory>();
            //modelkycstatus = RepCRUD<GetKYCStatusHistory, AddKYCStatusHistory>.GetRecordList(Models.Common.Common.StoreProcedures.sp_KYCStatusHistory_Get, inobject_kycstatus, outobject_kycstatus);
            //ViewBag.KYCStatusHistory = modelkycstatus;
            ViewBag.QRCode = Common.GetQRReferCode(model);
            return View(model);

        }
        [HttpPost]
        [Authorize]
        public ActionResult UserKYCDetails(string MemberId, string Remarks, string IsKYCApproved)
        {
            if ((IsKYCApproved == ((int)AddUser.kyc.Rejected).ToString() || IsKYCApproved == ((int)AddUser.kyc.Risk_High).ToString() || IsKYCApproved == ((int)AddUser.kyc.Proof_Rejected).ToString()) && (string.IsNullOrEmpty(Remarks) || Remarks == "0"))
            {
                ViewBag.Message = "Please select Remarks.";
            }
            AddUser  outobject = new AddUser();
            GetUser  inobject = new GetUser();
            inobject.MemberId = Convert.ToInt64(MemberId);
            AddUser res = RepCRUD<GetUser, AddUser>.GetRecord(Models.Common.Common.StoreProcedures.sp_Users_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (ViewBag.Message == null || ViewBag.Message == "")
                {
                    if (Session["AdminMemberId"] != null)
                    {
                        string AdminMemberId = Convert.ToString(Session["AdminMemberId"]);
                        string AdminMemberName = Convert.ToString(Session["AdminUserName"]);
                        res = RepUser.UpdateKYCDetails(res, Remarks, IsKYCApproved, AdminMemberId, AdminMemberName);
                    }
                    if (res.Message.ToLower() == "success")
                    {
                        ViewBag.SuccessMessage = "Kyc Successfully Updated";
                    }
                    else
                    {
                        ViewBag.Message = res.Message;
                    }
                }
            }
            ViewBag.QRCode = Common.GetQRReferCode(res);
            List<SelectListItem> KYCStatuslist = CommonHelpers.GetSelectList_KYCStatus(res.IsKYCApproved);
            ViewBag.IsKYCApproved = KYCStatuslist;
            //AddKYCStatusHistory outobject_kycstatus = new AddKYCStatusHistory();
            //GetKYCStatusHistory inobject_kycstatus = new GetKYCStatusHistory();
            //inobject_kycstatus.MemberId = Convert.ToInt64(MemberId);
            //List<AddKYCStatusHistory> modelkycstatus = new List<AddKYCStatusHistory>();
            //modelkycstatus = RepCRUD<GetKYCStatusHistory, AddKYCStatusHistory>.GetRecordList(Models.Common.Common.StoreProcedures.sp_KYCStatusHistory_Get, inobject_kycstatus, outobject_kycstatus);
            //ViewBag.KYCStatusHistory = modelkycstatus;

            //KYCRemarks List in DropDownList
            AddKYCRemarks outobject_remarks = new AddKYCRemarks();
            GetKYCRemarks inobject_remarks = new GetKYCRemarks();
            inobject_remarks.CheckActive = 1;
            inobject_remarks.CheckDelete = 0;
            List<AddKYCRemarks> remarkslist = RepCRUD<GetKYCRemarks, AddKYCRemarks>.GetRecordList(Common.StoreProcedures.sp_KYCRemarks_Get, inobject_remarks, outobject_remarks);
            List<SelectListItem> objkycremarks_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Remarks ---";
                objDefault.Selected = true;
                objkycremarks_SelectList.Add(objDefault);
            }

            for (int i = 0; i < remarkslist.Count; i++)
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Value = remarkslist[i].Id.ToString();
                objItem.Text = remarkslist[i].Title.ToString();
                objkycremarks_SelectList.Add(objItem);
            }
            ViewBag.Remarks = objkycremarks_SelectList;

            return View(res);
        }
        [Authorize]
        public JsonResult GetUserKYCLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Name");
            columns.Add("Mobile");
            columns.Add("Gender");
            columns.Add("Date Of Birth");
            columns.Add("DOBType2");
            columns.Add("KYC Created Time");
            columns.Add("Review Date");
            columns.Add("Time Elapsed");
            columns.Add("Edit By");
            columns.Add("Remarks");
            //columns.Add("GatewayStatus");
            //columns.Add("TransferWiseStatus");
            //columns.Add("Action");
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
            string IsKYCApproved = context.Request.Form["IsKYCApproved"];
            string Mobile = context.Request.Form["ContactNumber"];
            string Email = context.Request.Form["Email"];
            string StartDate = context.Request.Form["StartDate"];
            string ToDate = context.Request.Form["ToDate"];
            string IsActive = context.Request.Form["IsActive"];
            string RefCode = context.Request.Form["RefCode"];
            string Name = context.Request.Form["Name"];
            string StartReviewDate = context.Request.Form["StartReviewDate"];
            string ToReviewDate = context.Request.Form["ToReviewDate"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetUser inobject_user = new GetUser();
            if (string.IsNullOrEmpty(MemberId))
            {
                MemberId = "0";
            }
            List<AddUser> user = new List<AddUser>();
            Int32 recordFiltered = 0;
            GetUser w = new GetUser();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.ContactNumber = Mobile;

            w.DateFrom = StartDate;
            w.DateTo = ToDate;
            w.Email = Email;
            w.IsKYCApproved = Convert.ToInt32(IsKYCApproved);
            w.RoleId = -1;
            //w.CheckActive = 1;
            w.CheckDelete = 0;
            w.FirstName = Name;
            w.CheckActive = Convert.ToInt32(IsActive);
            w.RefCode = RefCode;
            w.ReviewDateFrom = StartReviewDate;
            w.ReviewDateTo = ToReviewDate;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby, ref recordFiltered);

            user = (from DataRow row in dt.Rows

                    select new AddUser
                    {
                        Sno = row["Sno"].ToString(),
                        FirstName = row["Name"].ToString(),
                        ContactNumber = row["ContactNumber"].ToString(),
                        GenderName = Enum.GetName(typeof(AddUser.sex), Convert.ToInt32(row["Gender"])),
                        DateofBirthdt = Convert.ToString(row["DateofBirth"]),
                        DOBType2 = Convert.ToString(row["DOBType2"]),
                        CreatedDatedt = row["IndiaDate"].ToString(),
                        UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                        KYCCreatedDatedt = row["IndiaKYCCreatedDate"].ToString(),
                        KYCReviewDateDt = row["IndiaKYCReviewDate"].ToString(),
                        TimeElapsed = row["TimeElapsed"].ToString(),
                        ApprovedorRejectedByName = Convert.ToString(row["ApprovedorRejectedByName"].ToString()),
                        Remarks = row["Remarks"].ToString(),
                        IsKYCApproved = Convert.ToInt32(row["IsKYCApproved"].ToString()),
                        MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                        TotalUserCount = recordFiltered.ToString()


                    }).ToList();

            DataTableResponse<List<AddUser>> objDataTableResponse = new DataTableResponse<List<AddUser>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = user
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        public JsonResult GetKYCStatusHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("Created By");
            columns.Add("KYC Status");
            columns.Add("Remarks");
            columns.Add("Ip Address");
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

            string MemberId = context.Request.Form["MemberId"];

            List<AddKYCStatusHistory> user = new List<AddKYCStatusHistory>();

            GetKYCStatusHistory w = new GetKYCStatusHistory();
            if (MemberId != "" && MemberId != "0")
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            user = (from DataRow row in dt.Rows

                    select new AddKYCStatusHistory
                    {
                        CreatedByName = row["CreatedByName"].ToString(),
                        Remarks = row["Remarks"].ToString(),
                        KYCStatusName = Enum.GetName(typeof(AddUser.kyc), Convert.ToInt32(row["KYCStatus"])).Replace("_", " ").ToString(),
                        CreatedDateDt = row["CreatedDateDt"].ToString(),
                        IsAdmin = Convert.ToBoolean(row["IsAdmin"]),
                        IpAddress = row["IpAddress"].ToString()
                    }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddKYCStatusHistory>> objDataTableResponse = new DataTableResponse<List<AddKYCStatusHistory>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = user
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcel(Int32 Take, Int32 Skip, string Sort, string SortOrder, int KYCStatus, Int64 MemberId, string ContactNo, string Name, string Email, string FromDate, string ToDate, string IsActive, string RefCode)
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Name");
            columns.Add("Mobile");
            columns.Add("Gender");
            columns.Add("Date Of Birth");
            columns.Add("DOBType2");
            columns.Add("KYC Created Time");
            columns.Add("Review Date");
            columns.Add("Time Elapsed");
            columns.Add("Edit By");
            columns.Add("Remarks");
            Sort = columns[Convert.ToInt32(Sort)];
            var fileName = "UserKYCList-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetUser user = new GetUser();

            user.MemberId = Convert.ToInt64(MemberId);
            user.ContactNumber = ContactNo;
            user.FirstName = Name;
            user.Email = Email;
            user.DateFrom = FromDate;
            user.DateTo = ToDate;
            if (KYCStatus != 0)
            {
                user.IsKYCApproved = Convert.ToInt32(KYCStatus);
            }
            else
            {
                user.IsKYCApproved = -1;
            }
            user.RefCode = RefCode;
            user.CheckActive = Convert.ToInt32(IsActive);
            Int32 recordFiltered = 0;
            DataTable dt = user.GetData(Sort, SortOrder, Skip, Take, "", ref recordFiltered);

            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Name", "ContactNumber", "GenderName", "DateOfBirth", "DOBType2", "IndiaKYCCreatedDate", "UpdateIndiaDate", "TimeElapsed", "ApprovedorRejectedByName", "Remarks", "StatusName", "KYCName"/*, "DeviceCode"*/);
                //dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["Name"].ColumnName = "Name";
                dt.Columns["ContactNumber"].ColumnName = "Mobile";
                dt.Columns["GenderName"].ColumnName = "Gender";
                dt.Columns["DateOfBirth"].ColumnName = "Date Of Birth";
                dt.Columns["DOBType2"].ColumnName = "DOB Type";
                dt.Columns["IndiaKYCCreatedDate"].ColumnName = "KYC Created Time";
                dt.Columns["UpdateIndiaDate"].ColumnName = "Review Date";
                dt.Columns["TimeElapsed"].ColumnName = "Time Elapsed";
                dt.Columns["ApprovedorRejectedByName"].ColumnName = "Edit By";
                dt.Columns["StatusName"].ColumnName = "Status";
                dt.Columns["KYCName"].ColumnName = "KYC";
                //dt.Columns["DeviceCode"].ColumnName = "Device Id";
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

        [HttpGet]
        [Authorize]
        public ActionResult Download(string fileName)
        {
            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
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
        //        fileName = "UsersKYC-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
        //        //Save the file to server temp folder
        //        string fullPath = Path.Combine(Server.MapPath("~/ExportData/User"), fileName);

        //        GetUser user = new GetUser();
        //        DataTable dt = user.GetData_UserKYCExport();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            //dt = dt.DefaultView.ToTable(false, "MemberId", "ContactNumber", "Email", "FirstName", "LastName", "TotalAmount", "DateofBirth", "DOBType", "Gender", "Nationality", "CreatedDate", "MaritalStatus", "State", "District", "Address", "StreetName", "Municipality", "WardNumber", "CurrentState", "CurrentStreetName", "CurrentMunicipality", "CurrentWardNumber", "CurrentDistrict", "CurrentHouseNumber", "FatherName", "MotherName", "GrandFatherName", "SpouseName", "DocumentType", "DocumentNumber", "Occupation", "ExpiryDate", "IssueDate", "IssueFromDistrictName", "IssueFromStateName", "KycStatus", "LastLogin", "ActiveStatus", "UserStatus");
        //            //dt = dt.DefaultView.ToTable(false, "FirstName", "LastName", "ContactNumber", "Gender", "DateofBirth", "CreatedDate", "ReviewDate", "TimeElapsed", "ApprovedorRejectedByName", "Remarks", "KycStatus");

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

        //                    Common.AddLogs("User's KYC Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
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
        //                    Common.AddLogs("User's KYC Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
        //                }
        //            }
        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                var ws = wb.Worksheets.Add(dt, "UserKYC(" + DateTime.Now.ToString("MMM") + ")");
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
        public ActionResult ExportExcelOldUser(int KYCStatus, Int64 MemberId, string ContactNo, string Name, string Email, string FromDate, string ToDate, string IsActive, string RefCode,string FromReviewDate,string ToReviewDate)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(FromDate) && string.IsNullOrEmpty(FromReviewDate))
                {
                    errorMessage = "Please select Start Date or Start Review Date";
                }
                else if (string.IsNullOrEmpty(ToDate) && string.IsNullOrEmpty(ToReviewDate))
                {
                    errorMessage = "Please select End Date or End Review Date";
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
                else if (!string.IsNullOrEmpty(FromReviewDate) && !string.IsNullOrEmpty(ToReviewDate))
                {
                    DateTime d1 = Convert.ToDateTime(FromReviewDate);
                    DateTime d2 = Convert.ToDateTime(ToReviewDate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 5)
                    {
                        errorMessage = "You cannot export report more than 5 days. So please select Start Review Date and End Review Date accordingly.";
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
                    fileName = "UsersKYC-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
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
                        user.ReviewDateFrom = FromReviewDate;
                        user.ReviewDateTo = ToReviewDate;

                        if (KYCStatus != 0)
                        {
                            user.IsKYCApproved = Convert.ToInt32(KYCStatus);
                        }
                        else
                        {
                            user.IsKYCApproved = -1;
                        }
                        user.RefCode = RefCode;
                        user.CheckActive = Convert.ToInt32(IsActive);
                    }
                    DataTable dt = user.GetData_UserKYCExport();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //dt = dt.DefaultView.ToTable(false, "MemberId", "ContactNumber", "Email", "FirstName", "LastName", "TotalAmount", "DateofBirth", "DOBType", "Gender", "Nationality", "CreatedDate", "MaritalStatus", "State", "District", "Address", "StreetName", "Municipality", "WardNumber", "CurrentState", "CurrentStreetName", "CurrentMunicipality", "CurrentWardNumber", "CurrentDistrict", "CurrentHouseNumber", "FatherName", "MotherName", "GrandFatherName", "SpouseName", "DocumentType", "DocumentNumber", "Occupation", "ExpiryDate", "IssueDate", "IssueFromDistrictName", "IssueFromStateName", "KycStatus", "LastLogin", "ActiveStatus", "UserStatus");
                        //dt = dt.DefaultView.ToTable(false, "FirstName", "LastName", "ContactNumber", "Gender", "DateofBirth", "CreatedDate", "ReviewDate", "TimeElapsed", "ApprovedorRejectedByName", "Remarks", "KycStatus");

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

                                Common.AddLogs("User's KYC Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
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
                                Common.AddLogs("User's KYC Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.User_Export);
                            }
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "UserKYC(" + DateTime.Now.ToString("MMM") + ")");
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

    }
}