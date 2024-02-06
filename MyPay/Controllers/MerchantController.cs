using ClosedXML.Excel;
using DeviceId;
using Microsoft.Ajax.Utilities;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.Response;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
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
    public class MerchantController : BaseAdminSessionController
    {
        // GET: AddMerchant
        [Authorize]
        [HttpGet]
        public ActionResult Index(string MerchantUniqueId)
        {
            AddMerchant model = new AddMerchant();
            if (MerchantUniqueId != null && MerchantUniqueId != "0")
            {
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = MerchantUniqueId;
                model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                ViewBag.Password = MyPay.Models.Common.Common.DecryptionFromKey(model.Password, model.secretkey);
            }
            //List<SelectListItem> MerchantType = CommonHelpers.GetSelectList_MerchantType(model);
            //ViewBag.MerchantType = MerchantType;

            List<SelectListItem> countrylist = new List<SelectListItem>();
            countrylist = CommonHelpers.GetSelectList_Country(Convert.ToInt32(model.CountryId));
            ViewBag.CountryId = countrylist;
            model.MerChantTypeEnum = (AddMerchant.MerChantType)model.MerchantType;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(AddMerchant model, HttpPostedFileBase MerchantImageFile)
        {

            if ((string.IsNullOrEmpty(model.FirstName)) && (model.MerchantType == (int)AddMerchant.MerChantType.Merchant))
            {
                ViewBag.Message = "Please enter first name";
            }
            else if ((string.IsNullOrEmpty(model.LastName)) && (model.MerchantType == (int)AddMerchant.MerChantType.Merchant))
            {
                ViewBag.Message = "Please enter last name";
            }
            else if ((string.IsNullOrEmpty(model.ContactNo)) && (model.MerchantType == (int)AddMerchant.MerChantType.Merchant))
            {
                ViewBag.Message = "Please enter contact number";
            }
            else if ((Convert.ToInt64(model.ContactNo) == 0) && ((model.MerchantType == (int)AddMerchant.MerChantType.Merchant)))
            {
                ViewBag.Message = "Please enter valid contact number";
            }
            else if (string.IsNullOrEmpty(model.EmailID))
            {
                ViewBag.Message = "Please enter email";
            }
            else if (model.CountryId == 0)
            {
                ViewBag.Message = "Please select country";
            }
            else if (string.IsNullOrEmpty(model.Address))
            {
                ViewBag.Message = "Please enter Address";
            }
            else if (string.IsNullOrEmpty(model.UserName))
            {
                ViewBag.Message = "Please enter UserName.";
            }
            else if (model.UserName.IndexOf(":") >= 0)
            {
                ViewBag.Message = "Username not allowed Colon (:)";
            }
            else if ((string.IsNullOrEmpty(model.WebsiteURL)) && (model.MerchantType == (int)AddMerchant.MerChantType.Merchant))
            {
                ViewBag.Message = "Please enter Website";
            }
            else if (string.IsNullOrEmpty(model.MerchantIpAddress))
            {
                ViewBag.Message = "Please enter IpAddress";
            }
            else if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Message = "Please enter password";
            }
            else if (model.Password.Length < 8)
            {
                ViewBag.Message = "Minimum Password length should be 8 characters.";
            }
            else if (model.Password != "")
            {
                if (model.Password.IndexOf(":") >= 0)
                {
                    ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                }
                else
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(model.Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                    }
                }
            }

            else if (!string.IsNullOrEmpty(model.EmailID) && !Regex.Match(model.EmailID, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                ViewBag.Message = "Please enter valid email";
            }
            if ((!string.IsNullOrEmpty(model.SuccessURL)) && (model.MerchantType == (int)AddMerchant.MerChantType.Merchant))
            {
                if (!model.SuccessURL.StartsWith("http://") && !model.SuccessURL.StartsWith("https://"))
                {
                    ViewBag.Message = "Please enter a valid SuccessURL.";
                }
            }
            if ((!string.IsNullOrEmpty(model.CancelURL)) && (model.MerchantType == (int)AddMerchant.MerChantType.Merchant))
            {
                if (!model.CancelURL.StartsWith("http://") && !model.CancelURL.StartsWith("https://"))
                {
                    ViewBag.Message = "Please enter a valid CancelURL.";
                }
            }

            if ((!string.IsNullOrEmpty(model.WebsiteURL)) && (model.MerchantType == (int)AddMerchant.MerChantType.Merchant))
            {
                if (!model.WebsiteURL.StartsWith("http://") && !model.WebsiteURL.StartsWith("https://"))
                {
                    ViewBag.Message = "Please enter a valid WebsiteURL.";
                }
            }


            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                model.EmailID = model.EmailID.Replace(" ", "");
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                if (model.MerchantUniqueId != "")
                {
                    inobject.MerchantUniqueId = model.MerchantUniqueId;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0 && !string.IsNullOrEmpty(model.MerchantUniqueId))
                    {
                        if (res.EmailID != model.EmailID)
                        {
                            AddMerchant outobjectemail = new AddMerchant();
                            GetMerchant inobjectemail = new GetMerchant();
                            inobjectemail.EmailID = model.EmailID;
                            AddMerchant resemail = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectemail, outobjectemail);
                            if (resemail != null && resemail.Id != 0)
                            {
                                ViewBag.Message = "EmailId already exists";
                            }
                        }
                        if (res.OrganizationName != model.OrganizationName)
                        {
                            AddMerchant outobjectorg = new AddMerchant();
                            GetMerchant inobjectorg = new GetMerchant();
                            inobjectorg.OrganizationName = model.OrganizationName;
                            AddMerchant resorg = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectorg, outobjectorg);
                            if (resorg != null && resorg.Id != 0)
                            {
                                ViewBag.Message = "Organization Name already exists";
                            }
                        }
                        if (res.ContactNo != model.ContactNo)
                        {
                            AddMerchant outobjectmobile = new AddMerchant();
                            GetMerchant inobjectmobile = new GetMerchant();
                            inobjectmobile.ContactNo = model.ContactNo;
                            AddMerchant resmobile = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectmobile, outobjectmobile);
                            if (resmobile != null && resmobile.Id != 0)
                            {
                                ViewBag.Message = "Mobile Number already exists";
                            }
                            AddUserLoginWithPin outobjectuser = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobjectuser = new GetUserLoginWithPin();
                            inobjectuser.ContactNumber = model.ContactNo;
                            AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectuser, outobjectuser);
                            if (resuser != null && resuser.Id != 0)
                            {
                                ViewBag.Message = "Mobile Number already exists";
                            }
                        }
                        if (string.IsNullOrEmpty(ViewBag.Message))
                        {
                            res.Password = Common.EncryptionFromKey(model.Password, res.secretkey);
                            res.FirstName = model.FirstName;
                            res.LastName = model.LastName;
                            //res.ContactNo = model.ContactNo;
                            res.EmailID = model.EmailID;
                            res.City = model.City;
                            res.State = model.State;
                            res.Address = model.Address;
                            res.ZipCode = model.ZipCode;
                            res.OrganizationName = model.OrganizationName;
                            res.CountryId = model.CountryId;
                            res.CountryName = model.CountryName;
                            res.SuccessURL = (model.MerchantType == (int)AddMerchant.MerChantType.Merchant) ? model.SuccessURL : "";
                            res.CancelURL = (model.MerchantType == (int)AddMerchant.MerChantType.Merchant) ? model.CancelURL : "";
                            res.WebsiteURL = (model.MerchantType == (int)AddMerchant.MerChantType.Merchant) ? model.WebsiteURL : "";
                            res.MerchantIpAddress = model.MerchantIpAddress;

                            if (MerchantImageFile != null)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(MerchantImageFile.FileName);
                                string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                                MerchantImageFile.SaveAs(filePath);
                                res.Image = fileName;
                            }
                            if (Session["AdminMemberId"] != null)
                            {
                                res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                res.UpdatedByName = Session["AdminUserName"].ToString();
                                res.UpdatedDate = DateTime.UtcNow;
                                bool status = RepCRUD<AddMerchant, GetMerchant>.Update(res, "merchant");
                                if (status)
                                {
                                    ViewBag.SuccessMessage = "Successfully Updated Merchant.";
                                    Common.AddLogs("Updated Merchant Detail of (MerchantUniqueId:" + res.MerchantUniqueId + "  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant, res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", 0, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                                    return RedirectToAction("MerchantList");
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
                    AddMerchant outobjectMerchant = new AddMerchant();
                    GetMerchant inobjectMerchant = new GetMerchant();
                    inobjectMerchant.UserName = model.UserName;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectMerchant, outobjectMerchant);
                    if (res != null && res.Id != 0)
                    {
                        ViewBag.Message = "Username already exists";
                    }
                    else if (model.UserName.IndexOf(":") >= 0)
                    {
                        ViewBag.Message = "Username not allowed Colon (:)";
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddMerchant outobjectemail = new AddMerchant();
                        GetMerchant inobjectemail = new GetMerchant();
                        inobjectemail.EmailID = model.EmailID;
                        AddMerchant resemail = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectemail, outobjectemail);
                        if (resemail != null && resemail.Id != 0)
                        {
                            ViewBag.Message = "EmailId already exists";
                        }
                    }

                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddMerchant outobjectorg = new AddMerchant();
                        GetMerchant inobjectorg = new GetMerchant();
                        inobjectorg.OrganizationName = model.OrganizationName;
                        AddMerchant resorg = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectorg, outobjectorg);
                        if (resorg != null && resorg.Id != 0)
                        {
                            ViewBag.Message = "Organization Name already exists";
                        }
                    }

                    string ContactNumber = model.ContactNo;
                    if (model.MerchantType == (int)AddMerchant.MerChantType.Bank)
                    {
                        ContactNumber = Common.GenerateBankContactNumber().PadLeft(10, '0');
                        model.FirstName = model.OrganizationName;
                        model.LastName = "Bank";
                    }
                    else if (model.MerchantType == (int)AddMerchant.MerChantType.Remittance)
                    {
                        ContactNumber = Common.GenerateBankContactNumber().PadLeft(10, '0');
                        model.FirstName = model.OrganizationName;
                        model.LastName = "Remittance";
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddMerchant outobjectmobile = new AddMerchant();
                        GetMerchant inobjectmobile = new GetMerchant();
                        inobjectmobile.ContactNo = ContactNumber;
                        AddMerchant resmobile = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectmobile, outobjectmobile);
                        if (resmobile != null && resmobile.Id != 0)
                        {
                            ViewBag.Message = "Merchant Mobile Number already exists";
                        }
                    }
                    //if (string.IsNullOrEmpty(ViewBag.Message))
                    //{
                    //    AddUserLoginWithPin outobjectuser = new AddUserLoginWithPin();
                    //    GetUserLoginWithPin inobjectuser = new GetUserLoginWithPin();
                    //    inobjectuser.ContactNumber = ContactNumber;
                    //    AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectuser, outobjectuser);
                    //    if (resuser != null && resuser.Id != 0)
                    //    {
                    //        ViewBag.Message = "Merchant User Contact Number already exists";
                    //    }
                    //}
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        bool IsDetailUpdated = false;
                        bool IsLoginWithPassword = false;

                        string Otp = string.Empty;
                        string ReturnString = string.Empty;
                        DateTime dt = System.DateTime.Now;
                        string authenticationToken = Common.GetWebToken();
                        string devicecode = Request.Browser.Type;
                        string deviceId = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();
                        int MerchantType = (int)model.MerChantTypeEnum;
                        string msg = RepUser.RegisterVerification(ref ReturnString, ref IsLoginWithPassword, ref IsDetailUpdated, authenticationToken, ContactNumber, "977", "Web", devicecode, true, ref Otp, "", true);
                        if (msg.ToLower() == "success" || msg.ToLower().Replace(" ", "") == "getuserdetail")
                        {
                            Int64 MemberID = 0;
                            AddUser resAddUser = new AddUser();
                            bool CheckMerchantUserExists = Common.CheckMerchantUserExists(model.ContactNo);
                            if (CheckMerchantUserExists == false)
                            {
                                Otp = Common.DecryptString(Otp);
                                bool IsCouponUnlocked = false;
                                msg = RepUser.Register(ref IsCouponUnlocked, ref resAddUser, deviceId, "0", "0", ContactNumber, "", "977", Otp, "Web", devicecode, false, "", authenticationToken, ref MemberID, true, MerchantType);
                                MemberID = resAddUser.MemberId;
                            }
                            else
                            {
                                AddUserLoginWithPin resUserOutObject = new AddUserLoginWithPin();
                                GetUserLoginWithPin resUserInObject = new GetUserLoginWithPin();
                                resUserInObject.ContactNumber = model.ContactNo;
                                AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, resUserInObject, resUserOutObject);
                                if (resUser != null && resUser.Id > 0 && resUser.RoleId == (int)AddUser.UserRoles.Merchant)
                                {
                                    MemberID = resUser.MemberId;
                                    msg = "success";
                                }
                                else
                                {
                                    msg = "User Contact Already Exists.";
                                }
                            }
                            if ((msg.ToLower() == "success"))
                            {
                                if (CheckMerchantUserExists == false)
                                {
                                    bool IsCouponUnlocked = false;
                                    msg = RepUser.UserPersonalUpdate(ref resAddUser, authenticationToken, MemberID, model.EmailID, "", "", model.FirstName, "", model.LastName, "", "0", "Web", false, devicecode, "", deviceId, ref IsCouponUnlocked, true, MerchantType);
                                }
                                if (msg.ToLower() == "success")
                                {
                                    string Secretkey = Common.RandomString(16);
                                    string Password = model.Password;
                                    msg = RepUser.ResetPasswordMerchant(Password, Password, MemberID, false, "Web", devicecode, true, MerchantType);
                                    if (msg.ToLower() == "success")
                                    {
                                        string RandomUserId = MyPay.Models.Common.Common.RandomNumber(11111111, 99999999).ToString();
                                        //outobject.MerchantUniqueId = (model.MerchantType == (int)AddMerchant.MerChantType.Merchant) ? "MER" + RandomUserId : "BNK" + RandomUserId;

                                        if (model.MerchantType == (int)AddMerchant.MerChantType.Merchant)
                                        {
                                            outobject.MerchantUniqueId = "MER" + RandomUserId;
                                        }
                                        else if (model.MerchantType == (int)AddMerchant.MerChantType.Bank)
                                        {
                                            outobject.MerchantUniqueId = "BNK" + RandomUserId;
                                        }
                                        else if (model.MerchantType == (int)AddMerchant.MerChantType.Remittance)
                                        {
                                            outobject.MerchantUniqueId = "RMT" + RandomUserId;
                                        }

                                        outobject.MerchantType = MerchantType;
                                        outobject.UserMemberId = MemberID;
                                        outobject.secretkey = Secretkey;
                                        outobject.IsActive = true;
                                        outobject.IsApprovedByAdmin = true;
                                        outobject.Password = Common.EncryptionFromKey(model.Password, outobject.secretkey);
                                        outobject.apikey = Common.EncryptionFromKey(model.UserName + ":" + model.Password + ":" + outobject.MerchantUniqueId, outobject.secretkey);
                                        outobject.FirstName = model.FirstName;
                                        outobject.LastName = model.LastName;
                                        outobject.ContactNo = ContactNumber;
                                        outobject.EmailID = model.EmailID;
                                        outobject.City = model.City;
                                        outobject.State = model.State;
                                        outobject.Address = model.Address;
                                        outobject.UserName = model.UserName;
                                        outobject.Address = model.Address;
                                        outobject.ZipCode = model.ZipCode;
                                        outobject.OrganizationName = model.OrganizationName;
                                        outobject.CountryId = model.CountryId;
                                        outobject.CountryName = model.CountryName;
                                        outobject.IsPasswordReset = false;
                                        outobject.SuccessURL = (model.MerchantType == (int)AddMerchant.MerChantType.Merchant) ? model.SuccessURL : "";
                                        outobject.CancelURL = (model.MerchantType == (int)AddMerchant.MerChantType.Merchant) ? model.CancelURL : "";
                                        outobject.WebsiteURL = (model.MerchantType == (int)AddMerchant.MerChantType.Merchant) ? model.WebsiteURL : "";
                                        outobject.RoleId = 4;
                                        outobject.RoleName = "merchant";
                                        outobject.MerchantIpAddress = model.MerchantIpAddress;
                                        outobject.MerchantMemberId = RepMerchants.GetNewMerchantId();
                                        List<string> PublicKeyPair = RepMerchants.GenerateKeyPair_Merchant();
                                        outobject.PrivateKey = PublicKeyPair[0].Replace("  ", "\r\n");
                                        outobject.PublicKey = PublicKeyPair[1].Replace("ssh-rsa", "").Replace("generated-key", "");
                                        outobject.API_User = outobject.UserName;
                                        outobject.API_Password = Common.EncryptionFromKey(Common.RandomString(15), outobject.secretkey);
                                        if (MerchantImageFile != null)
                                        {
                                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(MerchantImageFile.FileName);
                                            string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                                            MerchantImageFile.SaveAs(filePath);
                                            outobject.Image = fileName;
                                        }
                                        if (Session["AdminMemberId"] != null)
                                        {
                                            outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                            outobject.CreatedByName = Session["AdminUserName"].ToString();
                                            outobject.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                            outobject.UpdatedByName = Session["AdminUserName"].ToString();
                                            Int64 Id = RepCRUD<AddMerchant, GetMerchant>.Insert(outobject, "merchant");
                                            if (Id > 0)
                                            {
                                                AddMerchant outobject_merchant = new AddMerchant();
                                                GetMerchant inobject_merchant = new GetMerchant();
                                                inobject.Id = Id;
                                                AddMerchant resMerchant = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject_merchant, outobject_merchant);
                                                if (resMerchant != null || resMerchant.Id > 0)
                                                {
                                                    AddMerchantIPAddress merhantiPAddress = new AddMerchantIPAddress();
                                                    merhantiPAddress.MerchantMemberId = resMerchant.MerchantMemberId;
                                                    merhantiPAddress.MerchantUniqueId = resMerchant.MerchantUniqueId;
                                                    merhantiPAddress.MechantName = resMerchant.FirstName + " " + resMerchant.LastName;
                                                    merhantiPAddress.MerchantOrganization = resMerchant.OrganizationName;
                                                    merhantiPAddress.IPAddress = resMerchant.MerchantIpAddress;
                                                    merhantiPAddress.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                                    merhantiPAddress.CreatedByName = Session["AdminUserName"].ToString();
                                                    merhantiPAddress.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                                    merhantiPAddress.UpdatedByName = Session["AdminUserName"].ToString();

                                                    if (merhantiPAddress.Add())
                                                    {
                                                        Common.AddLogs("Add MerchantIPAddress for Merchant (" + resMerchant.Id + ")  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant);

                                                    }
                                                    else
                                                    {
                                                        Common.AddLogs("Failed to Add MerchantIPAddress ", true, (int)AddLog.LogType.Merchant);

                                                    }
                                                }
                                                ViewBag.SuccessMessage = "Successfully Added Merchant.";
                                                Common.AddLogs("Added Merchant Detail by(AdminMemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant);
                                                return RedirectToAction("MerchantList");
                                            }
                                            else
                                            {
                                                ViewBag.Message = "Merchant Not Added.";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Merchant Credentials Information Not Added. " + msg;
                                    }
                                }
                                else
                                {
                                    ViewBag.Message = "Merchant User Information Not Added." + msg;
                                }
                            }
                            else
                            {
                                ViewBag.Message = "Merchant User Not Added. " + msg;
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Merchant User Verification Failed." + msg;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(model.MerchantUniqueId))
                {
                    inobject.MerchantUniqueId = model.MerchantUniqueId;
                    model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                }
            }


            List<SelectListItem> countrylist = new List<SelectListItem>();
            countrylist = CommonHelpers.GetSelectList_Country(Convert.ToInt32(model.CountryId));
            ViewBag.CountryId = countrylist;
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MerchantList()
        {

            List<SelectListItem> MerchantType = CommonHelpers.GetSelectList_MerchantType();
            ViewBag.MerchantType = MerchantType;

            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult MerchantIPList(string MerchantId)
        {
            ViewBag.MerchantId = string.IsNullOrEmpty(MerchantId) ? "" : MerchantId;
            return View();
        }
        [Authorize]
        public JsonResult GetMerchantIpLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Member Id");
            columns.Add("Name");
            columns.Add("Organization");
            columns.Add("Ip Address");
            columns.Add("Created By");
            columns.Add("Updated By");

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
            string MerchantId = context.Request.Form["MerchantId"];
            string Name = context.Request.Form["Name"];
            string Organization = context.Request.Form["Organization"];
            string IpAddress = context.Request.Form["IpAddress"];

            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantIPAddress> trans = new List<AddMerchantIPAddress>();
            GetMerchantIpAddress w = new GetMerchantIpAddress();
            w.MerchantUniqueId = MerchantId;
            w.MechantName = Name;
            w.MerchantOrganization = Organization;
            w.IPAddress = IpAddress;
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantIPAddress
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         MerchantUniqueId = row["MerchantUniqueId"].ToString(),
                         MechantName = row["MechantName"].ToString(),
                         MerchantOrganization = row["MerchantOrganization"].ToString(),
                         IPAddress = row["IPAddress"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantIPAddress>> objDataTableResponse = new DataTableResponse<List<AddMerchantIPAddress>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        public JsonResult GetMerchantLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("FullName");
            columns.Add("UserName");
            columns.Add("Contact");
            columns.Add("Email");
            columns.Add("UniqueId");
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
            string ContactNumber = context.Request.Form["ContactNumber"];
            string MerchantUniqueId = context.Request.Form["MerchantUniqueId"];
            string Name = context.Request.Form["Name"];
            string Email = context.Request.Form["Email"];
            string UserName = context.Request.Form["UserName"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            int merchantType = Convert.ToInt32(context.Request.Form["MerchantType"]);
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchant> trans = new List<AddMerchant>();

            GetMerchant w = new GetMerchant();
            w.MerchantUniqueId = MerchantUniqueId;
            w.ContactNo = ContactNumber;
            w.FirstName = Name;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.EmailID = Email;
            w.MerchantType = merchantType;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchant
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantUniqueId = row["MerchantUniqueId"].ToString(),
                         FirstName = row["FirstName"].ToString(),
                         LastName = row["LastName"].ToString(),
                         EmailID = row["EmailID"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         ContactNo = row["ContactNo"].ToString(),
                         UserName = row["UserName"].ToString(),
                         TotalUserCount = dt.Rows[0]["FilterTotalCount"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         OrganizationName = row["OrganizationName"].ToString(),
                         apikey = row["apikey"].ToString(),
                         API_Password = Common.DecryptionFromKey(row["API_Password"].ToString(), row["secretkey"].ToString()),
                         MerchantTotalAmount = Convert.ToDecimal(row["MerchantTotalAmount"].ToString()),
                         MerchantIpAddress = row["MerchantIpAddress"].ToString(),
                         UserMemberId = Convert.ToInt64(row["UserMemberId"].ToString()),
                         MerchantTypeName = @Enum.GetName(typeof(AddMerchant.MerChantType), Convert.ToInt64(row["MerchantType"])),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchant>> objDataTableResponse = new DataTableResponse<List<AddMerchant>>
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
        public JsonResult MerchantBlockUnblock(AddMerchant model)
        {
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            inobject.MerchantUniqueId = model.MerchantUniqueId;
            AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            if (res != null && res.Id != 0 && !string.IsNullOrEmpty(model.MerchantUniqueId))
            {
                if (res.IsActive)
                {
                    res.IsActive = false;
                }
                else
                {
                    res.IsActive = true;
                }
                bool IsUpdated = RepCRUD<AddMerchant, GetMerchant>.Update(res, "merchant");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update Merchant";
                    Common.AddLogs("Updated Merchant of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant);

                }
                else
                {
                    ViewBag.Message = "Not Updated Merchant";
                    Common.AddLogs("Not Updated (MerchantID: " + res.MerchantUniqueId + " )", true, (int)AddLog.LogType.Merchant);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult MerchantBankBlockUnblock(AddMerchantBankDetail model)
        {
            AddMerchantBankDetail outobject = new AddMerchantBankDetail();
            GetMerchantBankDetail inobject = new GetMerchantBankDetail();
            inobject.MerchantId = model.MerchantId;
            inobject.Id = model.Id;
            AddMerchantBankDetail res = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Update(res, "merchantbankdetail");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update Merchant Bank";
                    Common.AddLogs("Updated Merchant Bank of (MerchantID: " + res.MerchantId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant);
                }
                else
                {
                    ViewBag.Message = "Not Updated Merchant";
                    Common.AddLogs("Not Updated (MerchantID: " + res.MerchantId + " )", true, (int)AddLog.LogType.Merchant);

                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult AddMerchantIp(AddMerchant model)
        {

            AddMerchant outobject_merchant = new AddMerchant();
            GetMerchant inobject_merchant = new GetMerchant();
            inobject_merchant.MerchantUniqueId = model.MerchantUniqueId; ;
            AddMerchant resMerchant = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject_merchant, outobject_merchant);
            if (resMerchant != null || resMerchant.Id > 0)
            {
                AddMerchantIPAddress merhantiPAddress = new AddMerchantIPAddress();
                merhantiPAddress.MerchantMemberId = resMerchant.MerchantMemberId;
                merhantiPAddress.MerchantUniqueId = resMerchant.MerchantUniqueId;
                merhantiPAddress.MechantName = resMerchant.FirstName + " " + resMerchant.LastName;
                merhantiPAddress.MerchantOrganization = resMerchant.OrganizationName;
                merhantiPAddress.IPAddress = model.MerchantIpAddress;
                merhantiPAddress.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                merhantiPAddress.CreatedByName = Session["AdminUserName"].ToString();
                merhantiPAddress.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                merhantiPAddress.UpdatedByName = Session["AdminUserName"].ToString();

                if (merhantiPAddress.Add())
                {
                    Common.AddLogs("Add Merchant IPAddress for Merchant (" + resMerchant.Id + ")  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant);
                    return Json(merhantiPAddress, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Common.AddLogs("Failed to Add Merchant IPAddress ", true, (int)AddLog.LogType.Merchant);
                    return Json(null, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ResetMerchantKeys(AddMerchant model)
        {
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            inobject.MerchantUniqueId = model.MerchantUniqueId;
            AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            if (res != null && res.Id != 0 && !string.IsNullOrEmpty(model.MerchantUniqueId))
            {
                string Password = Common.DecryptionFromKey(res.Password, res.secretkey);
                string APIPassword = Common.DecryptionFromKey(res.API_Password, res.secretkey);
                res.secretkey = Common.RandomString(16);
                res.apikey = Common.EncryptionFromKey(res.UserName + ":" + Password + ":" + res.MerchantUniqueId, res.secretkey);
                res.Password = Common.EncryptionFromKey(Password, res.secretkey);
                res.API_Password = Common.EncryptionFromKey(APIPassword, res.secretkey);
                if (Session["AdminMemberId"] != null || !string.IsNullOrEmpty(Session["AdminMemberId"].ToString()))
                {
                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Session["AdminUserName"].ToString();
                }
                else if (Session["MerchantUniqueId"] != null)
                {
                    //res.UpdatedBy = Convert.ToInt64(Session["MerchantUniqueId"]);
                    res.UpdatedByName = Session["MerchantUniqueId"].ToString();
                }
                res.UpdatedDate = DateTime.UtcNow;
                bool IsUpdated = RepCRUD<AddMerchant, GetMerchant>.Update(res, "merchant");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update Merchant";
                    if (Session["AdminMemberId"] != null || !string.IsNullOrEmpty(Session["AdminMemberId"].ToString()))
                    {
                        Common.AddLogs("Updated Merchant Keys of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                    else if (Session["MerchantUniqueId"] != null)
                    {
                        Common.AddLogs("Updated Merchant Keys of (MerchantID: " + res.MerchantUniqueId + " ) by (Merchant:" + Session["MerchantUniqueId"] + ")", false, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, res.MerchantMemberId, Session["MerchantUniqueId"].ToString());
                    }
                }
                else
                {
                    ViewBag.Message = "Not Updated Merchant";
                    Common.AddLogs("Not Updated Merchant Keys", true, (int)AddLog.LogType.Merchant, 0, "", false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys);
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MerchantOrdersList(string MerchantUniqueId)
        {
            ViewBag.MerchantUniqueId = string.IsNullOrEmpty(MerchantUniqueId) ? "" : MerchantUniqueId;
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantTransactionStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            List<SelectListItem> signs = new List<SelectListItem>();

            signs = CommonHelpers.GetSelectList_MerchantOrderSign();
            signs.Add(new SelectListItem
            {
                Text = "Select Sign",
                Value = "0",
                Selected = true
            });
            ViewBag.Sign = signs;

            List<SelectListItem> type = new List<SelectListItem>();
            type = CommonHelpers.GetSelectList_KhaltiEnumMerchantOrdrs();
            ViewBag.Type = type;

            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }

        [Authorize]
        public JsonResult GetMerchantOrdersLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TxnId");
            columns.Add("TrackerId");
            columns.Add("MerchantId");
            columns.Add("OrderId");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("MemberContatNo");
            columns.Add("Amount");
            columns.Add("Status");
            columns.Add("ServiceCharge");
            //columns.Add("IpAddress");
            columns.Add("Type");

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
            string MerchantId = context.Request.Form["MerchantId"];
            string OrderId = context.Request.Form["OrderId"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string TrackerId = context.Request.Form["TrackerId"];
            string MemberName = context.Request.Form["MemberName"];
            string MemberContactNumber = context.Request.Form["MemberContactNumber"];
            string Status = context.Request.Form["Status"];
            string TransactionId = context.Request.Form["TransactionId"];
            string Sign = context.Request.Form["Sign"];
            string Type = context.Request.Form["Type"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantOrders> trans = new List<AddMerchantOrders>();

            GetMerchantOrders w = new GetMerchantOrders();
            w.MerchantId = MerchantId;
            w.OrderId = OrderId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.TransactionId = TransactionId;
            w.TrackerId = TrackerId;
            w.MemberContactNumber = MemberContactNumber;
            w.MemberName = MemberName;
            w.Type = 0;
            if (!String.IsNullOrEmpty(Sign))
            {
                w.Sign = Convert.ToInt32(Sign);
            }
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            if (!String.IsNullOrEmpty(Type))
            {
                w.Type = Convert.ToInt32(Type);
            }
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantOrders
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantId = row["MerchantId"].ToString(),
                         OrderId = row["OrderId"].ToString(),
                         TransactionId = row["TransactionId"].ToString(),
                         TrackerId = row["TrackerId"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                         MemberName = row["MemberName"].ToString(),
                         MemberContactNumber = row["MemberContactNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), Convert.ToInt64(row["Status"])),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         Sign = Convert.ToInt32(row["Sign"]),
                         SignName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderSign), Convert.ToInt64(row["Sign"])),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"].ToString()),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         OrganizationName = row["OrganizationName"].ToString(),
                         ServiceCharges = Convert.ToDecimal(row["ServiceCharges"].ToString()),
                         DiscountAmount = Convert.ToDecimal(row["DiscountAmount"].ToString()),
                         CommissionAmount = Convert.ToDecimal(row["CommissionAmount"].ToString()),
                         CashbackAmount = Convert.ToDecimal(row["CashbackAmount"].ToString()),
                         MerchantContributionPercentage = Convert.ToDecimal(row["MerchantContributionPercentage"].ToString()),
                         NetAmount = Convert.ToDecimal(row["NetAmount"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantOrders>> objDataTableResponse = new DataTableResponse<List<AddMerchantOrders>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        public JsonResult GetMerchantWithdrawOrdersLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TxnId");
            columns.Add("TrackerId");
            columns.Add("MerchantId");
            columns.Add("OrderId");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("MemberContatNo");
            columns.Add("Amount");
            columns.Add("Status");
            columns.Add("ServiceCharge");
            //columns.Add("IpAddress");
            columns.Add("Type");

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
            string MerchantId = context.Request.Form["MerchantId"];
            string OrderId = context.Request.Form["OrderId"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string TrackerId = context.Request.Form["TrackerId"];
            string MemberName = context.Request.Form["MemberName"];
            string MemberContactNumber = context.Request.Form["MemberContactNumber"];
            string Status = context.Request.Form["Status"];
            string TransactionId = context.Request.Form["TransactionId"];
            string Sign = context.Request.Form["Sign"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantOrders> trans = new List<AddMerchantOrders>();

            GetMerchantOrders w = new GetMerchantOrders();
            w.MerchantId = MerchantId;
            w.OrderId = OrderId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.TransactionId = TransactionId;
            w.TrackerId = TrackerId;
            w.MemberContactNumber = MemberContactNumber;
            w.MemberName = MemberName;
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Withdrawal;
            if (!String.IsNullOrEmpty(Sign))
            {
                w.Sign = Convert.ToInt32(Sign);
            }
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantOrders
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantId = row["MerchantId"].ToString(),
                         OrderId = row["OrderId"].ToString(),
                         TransactionId = row["TransactionId"].ToString(),
                         TrackerId = row["TrackerId"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                         MemberName = row["MemberName"].ToString(),
                         MemberContactNumber = row["MemberContactNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), Convert.ToInt64(row["Status"])),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         Sign = Convert.ToInt32(row["Sign"]),
                         SignName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderSign), Convert.ToInt64(row["Sign"])),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"].ToString()),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         OrganizationName = row["OrganizationName"].ToString(),
                         ServiceCharges = Convert.ToDecimal(row["ServiceCharges"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantOrders>> objDataTableResponse = new DataTableResponse<List<AddMerchantOrders>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpGet]
        [Authorize]
        public ActionResult MerchantWalletWithdrawList(string MerchantUniqueId)
        {
            ViewBag.MerchantUniqueId = string.IsNullOrEmpty(MerchantUniqueId) ? "" : MerchantUniqueId;
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantTransactionStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            List<SelectListItem> signs = new List<SelectListItem>();

            signs = CommonHelpers.GetSelectList_MerchantOrderSign();
            signs.Add(new SelectListItem
            {
                Text = "Select Sign",
                Value = "0",
                Selected = true
            });
            ViewBag.Sign = signs;
            return View();
        }
        // GET: MerchantTransactions
        [HttpGet]
        [Authorize]
        public ActionResult MerchantTransactions(string MemberId, string TransactionId, string Reference, string ParentTransactionId, string GatewayTransactionId, string SubscriberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
            ViewBag.Reference = string.IsNullOrEmpty(Reference) ? "" : Reference;
            ViewBag.GatewayTransactionId = string.IsNullOrEmpty(GatewayTransactionId) ? "" : GatewayTransactionId;
            ViewBag.ParentTransactionId = string.IsNullOrEmpty(ParentTransactionId) ? "" : ParentTransactionId;
            ViewBag.SubscriberId = string.IsNullOrEmpty(SubscriberId) ? "" : SubscriberId;
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantTransactionStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            List<SelectListItem> sign = new List<SelectListItem>();
            sign.Add(new SelectListItem
            {
                Text = "Select Sign",
                Value = "0",
                Selected = true
            });
            sign.Add(new SelectListItem
            {
                Text = "Credit",
                Value = "1"
            });
            sign.Add(new SelectListItem
            {
                Text = "Debit",
                Value = "2"
            });
            ViewBag.Sign = sign;

            List<SelectListItem> type = new List<SelectListItem>();
            type = CommonHelpers.GetSelectList_KhaltiEnumMerchantTxn();
            ViewBag.Type = type;

            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetMerchantTransactionsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("RequestId");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("Service");
            columns.Add("ServiceCharge");
            columns.Add("Cashback");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
            columns.Add("UpdateBy");
            columns.Add("AvailableBalance");
            columns.Add("PreviousBalance");
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
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string Sign = context.Request.Form["Sign"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            string Reference = context.Request.Form["Reference"];
            string CustomerId = context.Request.Form["CustomerId"];
            string Type = context.Request.Form["Type"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTransactionId;
            w.ParentTransactionId = ParentTransactionId;
            w.Reference = Reference;
            w.Sign = Convert.ToInt32(Sign);
            w.CustomerID = CustomerId;
            w.IsMerchantTxn = 1;
            w.Type = Convert.ToInt32(Type);
            w.RoleId = (int)AddUser.UserRoles.Merchant;
            //w.Type = (int)Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         ParentTransactionId = row["ParentTransactionId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         Type = Convert.ToInt32(row["Type"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         UpdateByName = row["UpdateByName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         CashBack = Convert.ToDecimal(row["CashBack"]),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         VendorType = Convert.ToInt32(row["VendorType"]),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])),
                         IpAddress = row["IpAddress"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         CustomerID = row["CustomerID"].ToString(),
                         SenderBankName = row["SenderBankName"].ToString(),
                         SenderAccountNo = row["SenderAccountNo"].ToString(),
                         RecieverContactNumber = row["RecieverContactNumber"].ToString(),
                         RecieverBankName = row["RecieverBankName"].ToString(),
                         RecieverAccountNo = row["RecieverAccountNo"].ToString(),
                         WalletTypeName = @Enum.GetName(typeof(WalletTransactions.WalletTypes), Convert.ToInt64(row["WalletType"])).ToUpper(),
                         MPCoinsDebit = row["WalletType"].ToString() != "4" ? 0 : Convert.ToDecimal(row["MPCoinsDebit"].ToString()),
                         TransactionAmount = Convert.ToDecimal(row["TransactionAmount"].ToString()),
                         RewardPoint = row["Type"].ToString() == "20" ? 0 : Convert.ToDecimal(row["RewardPoint"].ToString()),
                         RewardPointBalance = Convert.ToDecimal(row["RewardPointBalance"].ToString()) + Convert.ToDecimal(row["RewardPoint"].ToString()),
                         PreviousRewardPointBalance = Convert.ToDecimal(row["PreviousRewardPointBalance"].ToString()),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<WalletTransactions>> objDataTableResponse = new DataTableResponse<List<WalletTransactions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        // GET: MerchantBankDetail
        [Authorize]
        [HttpGet]
        public ActionResult MerchantBankDetail(string MerchantId)
        {
            ViewBag.MerchantName = "";
            AddMerchantBankDetail model = new AddMerchantBankDetail();
            if (MerchantId != null && MerchantId != "" && MerchantId != "0")
            {
                AddMerchant outobjectres = new AddMerchant();
                GetMerchant inobjectres = new GetMerchant();
                inobjectres.MerchantUniqueId = MerchantId;
                AddMerchant resMerchant = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectres, outobjectres);
                if (resMerchant != null && resMerchant.Id > 0)
                {
                    ViewBag.MerchantName = resMerchant.FirstName + " " + resMerchant.LastName;
                }

                AddMerchantBankDetail outobject = new AddMerchantBankDetail();
                GetMerchantBankDetail inobject = new GetMerchantBankDetail();
                inobject.MerchantId = MerchantId;
                model = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobject, outobject);
            }
            List<SelectListItem> banklist = new List<SelectListItem>();
            banklist = CommonHelpers.GetSelectList_BankList_NPS(model.BankCode);
            ViewBag.BankCode = banklist;
            List<SelectListItem> merchantlist = new List<SelectListItem>();
            merchantlist = CommonHelpers.GetSelectList_MerchantList(model.MerchantId);
            ViewBag.MerchantId = merchantlist;
            ViewBag.MerchantUniqueId = MerchantId;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult MerchantBankDetail(AddMerchantBankDetail model)
        {
            if (string.IsNullOrEmpty(model.MerchantId))
            {
                ViewBag.Message = "Please select MerchantId";
            }
            else if (string.IsNullOrEmpty(model.BankName))
            {
                ViewBag.Message = "Please select bank";
            }
            else if (string.IsNullOrEmpty(model.AccountNumber))
            {
                ViewBag.Message = "Please enter account number";
            }
            else if (string.IsNullOrEmpty(model.BankCode))
            {
                ViewBag.Message = "Please select bank";
            }
            else if (string.IsNullOrEmpty(model.Name))
            {
                ViewBag.Message = "Please select Merchant Name";
            }
            try
            {

                AddMerchantBankDetail outobject = new AddMerchantBankDetail();
                GetMerchantBankDetail inobject = new GetMerchantBankDetail();

                AddMerchant outobjectres = new AddMerchant();
                GetMerchant inobjectres = new GetMerchant();
                inobjectres.MerchantUniqueId = model.MerchantId;
                AddMerchant resMerchant = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectres, outobjectres);
                if (resMerchant != null && resMerchant.Id > 0)
                {
                    if (resMerchant != null && resMerchant.Id > 0)
                    {
                        ViewBag.MerchantName = resMerchant.FirstName + " " + resMerchant.LastName;
                    }
                    if (model.MerchantId != "")
                    {
                        inobject.MerchantId = model.MerchantId;
                        AddMerchantBankDetail res = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobject, outobject);
                        if (res != null && res.Id != 0)
                        {
                            if (res.AccountNumber != model.AccountNumber)
                            {
                                //AddMerchantBankDetail outobjectemail = new AddMerchantBankDetail();
                                //GetMerchantBankDetail inobjectemail = new GetMerchantBankDetail();
                                //inobjectemail.AccountNumber = model.AccountNumber;
                                //AddMerchantBankDetail resemail = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobjectemail, outobjectemail);
                                //if (resemail != null && resemail.Id != 0)
                                //{
                                //    ViewBag.Message = "Account Number already exists";
                                //}
                            }
                            if (string.IsNullOrEmpty(ViewBag.Message))
                            {
                                if (res.BankCode != model.BankCode)
                                {
                                    AddBankListNps resbank = new AddBankListNps();
                                    resbank.BANK_CD = model.BankCode;
                                    resbank.GetRecord();
                                    if (resbank != null && resbank.Id != 0)
                                    {
                                        res.BranchId = resbank.BRANCH_CD;
                                        res.BranchName = resbank.BRANCH_NAME;
                                        res.ICON_NAME = resbank.ICON_NAME;
                                    }
                                }
                                res.AccountNumber = model.AccountNumber;
                                res.BankCode = model.BankCode;
                                res.BankName = model.BankName;
                                res.IsActive = false;
                                res.IsVerified = false;
                                res.IsApprovedByAdmin = model.IsActive;
                                res.IsPrimary = true;
                                res.Name = model.Name;

                                if (Session["AdminMemberId"] != null)
                                {
                                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    res.UpdatedByName = Session["AdminUserName"].ToString();
                                    res.UpdatedDate = DateTime.UtcNow;
                                    res.LinkedBankToken = String.Empty;
                                    res.LinkedBankTransactionId = String.Empty;
                                    bool status = true;
                                    //bool status = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Update(res, "merchantbankdetail");
                                    if (status)
                                    {
                                        string BankType = "NPS";
                                        bool IsAccountVerifiedByBank = false;
                                        //using (MyPayEntities db = new MyPayEntities())
                                        //{
                                        //    ApiSetting objApiSettings = db.ApiSettings.FirstOrDefault();
                                        //    if (objApiSettings.BankTransferType > 0)
                                        //    {
                                        //        if (objApiSettings.BankTransferType == 2)
                                        //        {
                                        //            BankType = "NPS";
                                        //        }
                                        //        else
                                        //        {
                                        //            BankType = "NCHL";
                                        //        }
                                        //    }

                                        //}
                                        if (BankType == "NPS")
                                        {
                                            GetFundAccountValidation valid = RepNps.FundAccountValidation(res.Name, res.AccountNumber, res.BankCode);
                                            string AccountValidationJson = JsonConvert.SerializeObject(valid);
                                            Common.AddLogs("Merchant Bank Account Validation JSON (MerchantUniqueId:" + res.MerchantId + ") INPUT : AccountName=" + res.Name + " Accountnumber=" + res.AccountNumber + " BankCode = " + res.BankCode + " OUTPUT: " + AccountValidationJson, true, (int)AddLog.LogType.Merchant);
                                            if (valid != null && !string.IsNullOrEmpty(valid.code) && (valid.code == "0" || (valid.data != null && Convert.ToInt32(valid.data.NameMatchPercentage) >= 80)))
                                            {
                                                IsAccountVerifiedByBank = true;
                                                status = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Update(res, "merchantbankdetail");
                                                if (status)
                                                {
                                                    ViewBag.SuccessMessage = "Bank Updated Successfully.";
                                                }
                                                else
                                                {
                                                    ViewBag.Message = "Bank Update Failed.";
                                                }
                                            }
                                            else
                                            {
                                                ViewBag.Message = "Bank verification Failed.";
                                            }
                                        }
                                        else
                                        {
                                            string token = RepNCHL.gettoken();
                                            Req_AccountValidationWeb user = new Req_AccountValidationWeb();
                                            user.accountId = res.AccountNumber;
                                            user.accountName = res.Name;
                                            user.bankId = res.BankCode;
                                            string data = RepNCHL.PostMethodWithToken("api/validatebankaccount", JsonConvert.SerializeObject(user), token);
                                            if (!string.IsNullOrEmpty(data))
                                            {
                                                try
                                                {

                                                    Res_AccountValidationWeb res_bank = JsonConvert.DeserializeObject<Res_AccountValidationWeb>(data);
                                                    if ((res_bank.responseCode == "000" || res_bank.responseCode == "999"))
                                                    {
                                                        IsAccountVerifiedByBank = true;
                                                        status = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Update(res, "merchantbankdetail");
                                                        if (status)
                                                        {
                                                            ViewBag.SuccessMessage = "Bank Updated Successfully.";
                                                        }
                                                        else
                                                        {
                                                            ViewBag.Message = "Bank Update Failed.";
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                            }
                                        }

                                        if (IsAccountVerifiedByBank)
                                        {
                                            res.IsVerified = true;
                                            res.IsActive = true;
                                            bool statusVerified = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Update(res, "merchantbankdetail");
                                            if (statusVerified)
                                            {
                                                ViewBag.SuccessMessage = "Account Information Verified Successfully";
                                            }
                                            else
                                            {
                                                ViewBag.Message = "Merchant Bank Update Failed.";
                                            }
                                            Common.AddLogs("Updated Merchant Bank Detail of (MerchantUniqueId:" + res.MerchantId + "  by(AdminId:" + Session["AdminMemberId"] + ") ", true, (int)AddLog.LogType.Merchant);

                                        }
                                        else
                                        {
                                            ViewBag.Message = "Invalid Account Information";
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Not Updated.";
                                    }
                                }
                            }
                        }
                        else
                        {
                            //AddMerchantBankDetail outobjectemail = new AddMerchantBankDetail();
                            //GetMerchantBankDetail inobjectemail = new GetMerchantBankDetail();
                            //inobjectemail.AccountNumber = model.AccountNumber;
                            //AddMerchantBankDetail resemail = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobjectemail, outobjectemail);
                            //if (resemail != null && resemail.Id != 0)
                            //{
                            //    ViewBag.Message = "Account Number already exists";
                            //}
                            if (string.IsNullOrEmpty(ViewBag.Message))
                            {
                                AddBankList outobjectbank = new AddBankList();
                                GetBankList inobjectbank = new GetBankList();
                                inobjectbank.BANK_CD = model.BankCode;
                                AddBankList resbank = RepCRUD<GetBankList, AddBankList>.GetRecord(Common.StoreProcedures.sp_BankList_Get, inobjectbank, outobjectbank);
                                if (resbank != null && resbank.Id != 0)
                                {
                                    outobject.BranchId = resbank.BRANCH_CD;
                                    outobject.BranchName = resbank.BRANCH_NAME;
                                    outobject.ICON_NAME = resbank.ICON_NAME;
                                }
                                outobject.MerchantId = model.MerchantId;
                                outobject.AccountNumber = model.AccountNumber;
                                outobject.BankCode = model.BankCode;
                                outobject.BankName = model.BankName;
                                outobject.IsActive = model.IsActive;
                                outobject.IsApprovedByAdmin = model.IsActive;
                                outobject.IsPrimary = true;
                                outobject.IsActive = false;
                                outobject.IsVerified = false;
                                outobject.Name = model.Name;

                                if (Session["AdminMemberId"] != null)
                                {
                                    outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    outobject.CreatedByName = Session["AdminUserName"].ToString();
                                    outobject.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    outobject.UpdatedByName = Session["AdminUserName"].ToString();

                                    string BankType = "NPS";
                                    bool IsAccountVerifiedByBank = false;
                                    //using (MyPayEntities db = new MyPayEntities())
                                    //{
                                    //    ApiSetting objApiSettings = db.ApiSettings.FirstOrDefault();
                                    //    if (objApiSettings.BankTransferType > 0)
                                    //    {
                                    //        if (objApiSettings.BankTransferType == 2)
                                    //        {
                                    //            BankType = "NPS";
                                    //        }
                                    //        else
                                    //        {
                                    //            BankType = "NCHL";
                                    //        }
                                    //    }

                                    //}
                                    if (BankType == "NPS")
                                    {
                                        GetFundAccountValidation valid = RepNps.FundAccountValidation(res.Name, res.AccountNumber, res.BankCode);
                                        string AccountValidationJson = JsonConvert.SerializeObject(valid);
                                        Common.AddLogs("Merchant Bank Account Validation JSON (MerchantUniqueId:" + res.MerchantId + ") INPUT : AccountName=" + res.Name + " Accountnumber=" + res.AccountNumber + " BankCode = " + res.BankCode + " OUTPUT: " + AccountValidationJson, true, (int)AddLog.LogType.Merchant);
                                        if (valid != null && !string.IsNullOrEmpty(valid.code) && (valid.code == "0" || (valid.data != null && Convert.ToInt32(valid.data.NameMatchPercentage) >= 80)))
                                        {
                                            IsAccountVerifiedByBank = true;
                                            Int64 Id = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Insert(outobject, "merchantbankdetail");
                                            if (Id > 0)
                                            {
                                                ViewBag.SuccessMessage = "Bank Added Successfully.";
                                            }
                                            else
                                            {
                                                ViewBag.Message = "Bank Not Added.";
                                            }
                                        }
                                        else
                                        {
                                            ViewBag.Message = "Bank verification Failed.";
                                        }
                                    }
                                    else
                                    {
                                        string token = RepNCHL.gettoken();
                                        Req_AccountValidationWeb user = new Req_AccountValidationWeb();
                                        user.accountId = res.AccountNumber;
                                        user.accountName = res.Name;
                                        user.bankId = res.BankCode;
                                        string data = RepNCHL.PostMethodWithToken("api/validatebankaccount", JsonConvert.SerializeObject(user), token);
                                        if (!string.IsNullOrEmpty(data))
                                        {
                                            try
                                            {

                                                Res_AccountValidationWeb res_bank = JsonConvert.DeserializeObject<Res_AccountValidationWeb>(data);
                                                if ((res_bank.responseCode == "000" || res_bank.responseCode == "999"))
                                                {
                                                    IsAccountVerifiedByBank = true;
                                                    Int64 Id = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Insert(outobject, "merchantbankdetail");
                                                    if (Id > 0)
                                                    {
                                                        ViewBag.SuccessMessage = "Bank Added Successfully.";
                                                    }
                                                    else
                                                    {
                                                        ViewBag.Message = "Bank Not Added.";
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }

                                        if (IsAccountVerifiedByBank)
                                        {
                                            res.IsVerified = true;
                                            res.IsActive = true;
                                            bool statusVerified = RepCRUD<AddMerchantBankDetail, GetMerchantBankDetail>.Update(res, "merchantbankdetail");
                                            if (statusVerified)
                                            {
                                                ViewBag.SuccessMessage = "Account Information Verified Successfully";
                                            }
                                            else
                                            {
                                                ViewBag.Message = "Merchant Bank Details Not Added.  ";
                                            }
                                        }
                                        else
                                        {
                                            ViewBag.Message = "Invalid Account Information";
                                        }
                                        Common.AddLogs("Added Merchant Bank Detail by(AdminMemberId:" + Session["AdminMemberId"] + ") ", true, (int)AddLog.LogType.Merchant);
                                    }
                                }
                                else
                                {
                                    ViewBag.Message = "Something went wrong. Please try again later.";
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please select Merchant Name";
                    }
                }
                else
                {
                    ViewBag.Message = "Merchant Not Found";
                }
                inobject.MerchantId = model.MerchantId;
                model = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobject, outobject);

            }
            catch (Exception ex)
            {

            }

            List<SelectListItem> banklist = new List<SelectListItem>();
            banklist = CommonHelpers.GetSelectList_BankList_NPS(model.BankCode);
            ViewBag.BankCode = banklist;

            List<SelectListItem> merchantlist = new List<SelectListItem>();
            merchantlist = CommonHelpers.GetSelectList_MerchantList(model.MerchantId);
            ViewBag.MerchantId = merchantlist;


            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MerchantBankList(string MerchantId)
        {
            ViewBag.MerchantId = string.IsNullOrEmpty(MerchantId) ? "" : MerchantId;
            return View();
        }

        [Authorize]
        public JsonResult GetMerchantBankLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Member Id");
            columns.Add("Name");
            columns.Add("Bank Code");
            columns.Add("Bank Name");
            columns.Add("Branch Id");
            columns.Add("Branch Name");
            columns.Add("Account Number");
            columns.Add("Is Primary");
            columns.Add("Created By");
            columns.Add("Updated By");

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
            string MerchantId = context.Request.Form["MerchantId"];
            string Name = context.Request.Form["Name"];
            string BankCode = context.Request.Form["BankCode"];
            string BankName = context.Request.Form["BankName"];
            string BranchName = context.Request.Form["BranchName"];
            string AccountNumber = context.Request.Form["AccountNumber"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["EndDate"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantBankDetail> trans = new List<AddMerchantBankDetail>();
            GetMerchantBankDetail w = new GetMerchantBankDetail();
            w.MerchantId = MerchantId;
            w.Name = Name;
            w.BankCode = BankCode;
            w.BankName = BankName;
            w.BranchName = BranchName;
            w.AccountNumber = AccountNumber;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantBankDetail
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         MerchantId = row["MerchantId"].ToString(),
                         Name = row["Name"].ToString(),
                         BankCode = row["BankCode"].ToString(),
                         BankName = row["BankName"].ToString(),
                         BranchId = row["BranchId"].ToString(),
                         BranchName = row["BranchName"].ToString(),
                         AccountNumber = row["AccountNumber"].ToString(),
                         IsPrimary = Convert.ToBoolean(row["IsPrimary"]),
                         IsVerified = Convert.ToBoolean(row["IsVerified"]),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantBankDetail>> objDataTableResponse = new DataTableResponse<List<AddMerchantBankDetail>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        // GET: MerchantWithdrawalRequests
        [HttpGet]
        [Authorize]
        public ActionResult MerchantWithdrawalRequests(string MerchantId)
        {
            ViewBag.MerchantId = string.IsNullOrEmpty(MerchantId) ? "" : MerchantId;
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantWithdrawalStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            return View();
        }

        [Authorize]
        public JsonResult GetMerchantWithdrawalRequestsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
            columns.Add("MerchantId");
            columns.Add("Amount");
            columns.Add("Remarks");
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

            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string Status = context.Request.Form["Status"];
            string MerchantId = context.Request.Form["MerchantId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddMerchantWithdrawalRequest> trans = new List<AddMerchantWithdrawalRequest>();

            GetMerchantWithdrawalRequest w = new GetMerchantWithdrawalRequest();
            if (!string.IsNullOrEmpty(MerchantId))
            {
                w.MerchantId = MerchantId;
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            else
            {
                w.Status = (int)AddMerchantWithdrawalRequest.MerchantWithdrawalStatus.Pending;
            }
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddMerchantWithdrawalRequest
                     {
                         Sno = row["Sno"].ToString(),
                         Id = Convert.ToInt64(row["Id"].ToString()),
                         MerchantId = row["MerchantId"].ToString(),
                         MerchantName = row["MerchantName"].ToString(),
                         MerchantContactNumber = row["MerchantContactNumber"].ToString(),
                         MerchantOrganization = row["MerchantOrganization"].ToString(),
                         OrderId = row["OrderId"].ToString(),
                         TrackerId = row["TrackerId"].ToString(),
                         Particulars = row["Particulars"].ToString(),
                         JsonResponse = row["JsonResponse"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         BankName = row["BankName"].ToString(),
                         AccountNumber = row["AccountNumber"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         BankStatus = Convert.ToInt32(row["BankStatus"]),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), Convert.ToInt64(row["Status"])),
                         BankStatusName = @Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), Convert.ToInt64(row["BankStatus"])),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         WithdrawalRequestType = Convert.ToInt32(row["WithdrawalRequestType"]),
                         WithdrawalRequestTypeName = @Enum.GetName(typeof(AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType), Convert.ToInt64(row["WithdrawalRequestType"])),
                         IsWithdrawalApproveByAdmin = Convert.ToBoolean(row["IsWithdrawalApproveByAdmin"]),
                         IsApprovedByAdmin = Convert.ToBoolean(row["IsApprovedByAdmin"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddMerchantWithdrawalRequest>> objDataTableResponse = new DataTableResponse<List<AddMerchantWithdrawalRequest>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }
        [Authorize]
        public string GetVendorjsonDetails(string OrderId)
        {
            string result = "";
            try
            {
                AddMerchantWithdrawalRequest outobject = new AddMerchantWithdrawalRequest();
                GetMerchantWithdrawalRequest inobject = new GetMerchantWithdrawalRequest();
                inobject.OrderId = OrderId;
                AddMerchantWithdrawalRequest res = RepCRUD<GetMerchantWithdrawalRequest, AddMerchantWithdrawalRequest>.GetRecord(Models.Common.Common.StoreProcedures.sp_MerchantWithdrawalRequest_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    result = res.JsonResponse;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        [Authorize]
        public ActionResult MerchantQRCodeExport(string MerchantContact)
        {
            AddMerchant model = new AddMerchant();
            RepMechantsCommon objRepMechantsCommon = new RepMechantsCommon();
            objRepMechantsCommon.ExportToPDF(MerchantContact);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ShowPublicPrivateKeys(AddMerchant model)
        {
            if (Session["AdminMemberId"] != null)
            {
                string privateKeyString = (new CommonHelpers()).GetMerchantPrivateKey(model.MerchantUniqueId).Replace("\\n", Environment.NewLine);


                string publicKeyString = (new CommonHelpers()).GetMerchantPublicKey(model.MerchantUniqueId).Replace("\\n", Environment.NewLine);

                return Json(privateKeyString + "(" + publicKeyString);
            }
            else
            {
                return Json("");
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult ShowMerchantWalletBalance(AddMerchant model)
        {
            if (Session["AdminMemberId"] != null)
            {
                string MerchantWalletBalance = (new CommonHelpers()).GetMerchantWalletBalance(model.UserMemberId.ToString()).Replace("\\n", Environment.NewLine);

                return Json(MerchantWalletBalance);
            }
            else
            {
                return Json("");
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult ApproveWithdrawalRequest(string Id, string MerchantUniqueId, string RequestType)
        {
            string msg = "";
            if (Session["AdminMemberId"] != null && !string.IsNullOrEmpty(Convert.ToString(Session["AdminMemberId"])))
            {
                if (Id == "0" || string.IsNullOrEmpty(Id))
                {
                    msg = "Please select Withrawal Id";
                }
                else if (MerchantUniqueId == "0" || string.IsNullOrEmpty(MerchantUniqueId))
                {
                    msg = "MerchantUniqueId Not Found";
                }
                else if (RequestType == "0" || string.IsNullOrEmpty(RequestType))
                {
                    msg = "Please select Withrawal Request Type";
                }
                else
                {
                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    inobject.MerchantUniqueId = MerchantUniqueId;
                    AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (model != null && model.Id > 0 && !string.IsNullOrEmpty(MerchantUniqueId))
                    {
                        if (model.MerchantType == (int)AddMerchant.MerChantType.Merchant)
                        {
                            AddUserLoginWithPin resUserOutObject = new AddUserLoginWithPin();
                            GetUserLoginWithPin resUserInObject = new GetUserLoginWithPin();
                            resUserInObject.CheckDelete = 0;
                            resUserInObject.MemberId = model.UserMemberId;
                            AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, resUserInObject, resUserOutObject);
                            if (resUser != null && resUser.Id > 0 && resUser.IsActive == true)
                            {
                                AddMerchantWithdrawalRequest outobject_request = new AddMerchantWithdrawalRequest();
                                GetMerchantWithdrawalRequest inobject_request = new GetMerchantWithdrawalRequest();
                                inobject_request.Id = Convert.ToInt64(Id);
                                AddMerchantWithdrawalRequest res = RepCRUD<GetMerchantWithdrawalRequest, AddMerchantWithdrawalRequest>.GetRecord(Common.StoreProcedures.sp_MerchantWithdrawalRequest_Get, inobject_request, outobject_request);
                                if (res != null && res.Id != 0)
                                {
                                    if (res.Status == (int)AddMerchantWithdrawalRequest.MerchantWithdrawalStatus.ApprovalPending)
                                    {
                                        string Remarks = string.Empty;
                                        Remarks = "Withdrawal Pending";
                                        AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                        GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                        inobjectOrders.MerchantId = MerchantUniqueId;
                                        inobjectOrders.OrderId = res.OrderId;
                                        AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                        if (resOrders != null && resOrders.Id != 0)
                                        {
                                            if (resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.ApprovalPending)
                                            {
                                                string OrderTransactionId = resOrders.TransactionId;
                                                if (RequestType.ToLower() == "disapprove")
                                                {
                                                    msg = RepMerchants.RefundMerchantWithdrawal(res.Amount.ToString("0.00"), model, resUser, res.WithdrawalRequestType, resOrders.ServiceCharges, resOrders, resOrders.TransactionId, Remarks);
                                                    if (msg.ToLower() == "success")
                                                    {
                                                        res.IsWithdrawalApproveByAdmin = false;
                                                        res.Status = (int)AddMerchantWithdrawalRequest.MerchantWithdrawalStatus.Cancelled;
                                                        res.IsApprovedByAdmin = false;
                                                        Remarks = "Withdrawal Rejected";
                                                    }
                                                    else
                                                    {
                                                        Remarks = msg;
                                                    }
                                                }
                                                else if (RequestType.ToLower() == "approve" && res.Status == (int)AddMerchantWithdrawalRequest.MerchantWithdrawalStatus.ApprovalPending)
                                                {
                                                    string error_message = "";
                                                    string Particulars = "";
                                                    string Json_Response = "";
                                                    string ProcessId = DateTime.UtcNow.ToString("ddMMyyyyhhmmssms") + Common.RandomNumber(111, 999).ToString();
                                                    string Description = res.Description;
                                                    bool IsWithdrawalApproveByAdmin = res.IsWithdrawalApproveByAdmin;
                                                    int BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Pending;
                                                    msg = RepMerchants.BankWithdrawalMerchant(res.Amount.ToString("0.00"), ref IsWithdrawalApproveByAdmin, model, resUser, res.WithdrawalRequestType, resOrders.ServiceCharges, res.MerchantName, res.BankCode, res.AccountNumber, ref error_message, ref Particulars, ref Json_Response, ref BankStatus, Description, resOrders, resOrders.TransactionId, ProcessId);

                                                    if (msg.ToLower() == "success")
                                                    {
                                                        if (BankStatus == (int)AddMerchantOrders.MerchantOrderStatus.Success)
                                                        {
                                                            res.IsWithdrawalApproveByAdmin = IsWithdrawalApproveByAdmin;
                                                            res.Status = (int)AddMerchantWithdrawalRequest.MerchantWithdrawalStatus.Success;
                                                            res.IsApprovedByAdmin = true;
                                                            res.JsonResponse = Json_Response;
                                                            res.Particulars = Particulars;
                                                            res.BankStatus = BankStatus;
                                                            Remarks = "Withdrawal Approved ";
                                                        }
                                                        else
                                                        {
                                                            res.Status = BankStatus;
                                                            res.JsonResponse = Json_Response;
                                                            res.Particulars = Particulars;
                                                            res.BankStatus = BankStatus;
                                                            Remarks = $"BankStatus: {@Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), Convert.ToInt64(BankStatus))} ";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        res.Status = BankStatus;
                                                        res.JsonResponse = Json_Response;
                                                        res.Particulars = Particulars;
                                                        res.BankStatus = BankStatus;
                                                        Remarks = msg;
                                                    }
                                                }

                                                if (msg.ToLower() == "success")
                                                {
                                                    res.Remarks = Remarks;
                                                    res.UpdatedDate = DateTime.UtcNow;
                                                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                                    res.UpdatedByName = Session["AdminUserName"].ToString();
                                                    bool IsUpdated = RepCRUD<AddMerchantWithdrawalRequest, GetMerchantWithdrawalRequest>.Update(res, "MerchantWithdrawalRequest");
                                                    if (IsUpdated)
                                                    {
                                                        AddMerchantOrders outobjectOrdersUpdate = new AddMerchantOrders();
                                                        GetMerchantOrders inobjectOrdersUpdate = new GetMerchantOrders();
                                                        inobjectOrdersUpdate.MerchantId = model.MerchantUniqueId;
                                                        inobjectOrdersUpdate.TransactionId = OrderTransactionId;
                                                        AddMerchantOrders resOrdersCheckUpdate = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrdersUpdate, outobjectOrdersUpdate);
                                                        if (resOrdersCheckUpdate != null && resOrdersCheckUpdate.Id != 0)
                                                        {
                                                            resOrdersCheckUpdate.Remarks = Remarks;
                                                            resOrdersCheckUpdate.Status = res.Status;
                                                            bool resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrdersCheckUpdate, "merchantorders");
                                                        }
                                                        Common.AddLogs("Withdrawal Request " + RequestType + " by Admin (Admin : " + Session["AdminMemberId"].ToString() + ") (MerchantId:" + res.MerchantId + ")", true, (int)AddLog.LogType.Merchant, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Approve_Withdrawal_Requests);
                                                        msg = "success";
                                                    }
                                                    else
                                                    {
                                                        msg = "Withdrawal Request not updated. Please Try Again";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                msg = "Withdrawal Order already updated. ";
                                            }
                                        }
                                        else
                                        {
                                            msg = "Withdrawal Order not found. ";
                                        }
                                    }
                                    else
                                    {
                                        msg = "Withdrawal Request already updated. ";
                                    }
                                }
                                else
                                {
                                    msg = "Withdrawal Request Not Found";
                                }
                            }
                            else
                            {
                                msg = "User Not Exists";
                            }
                        }
                        else
                        {
                            msg = "Withdrawal feature is not available for Banking Merchants";
                        }
                    }
                    else
                    {
                        msg = "Something went wrong. Please try again later.";
                    }
                }
            }
            else
            {
                msg = "Session expired. Please login and try again. ";

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelAllMerchantOrdersDump(string Type, string TransactionId, string Sign, string Status, string MerchantId, string OrderId, string TrackerId, string MemberName, string fromdate, string todate, string MemberContactNumber)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(fromdate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(todate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
                {
                    DateTime d1 = Convert.ToDateTime(fromdate);
                    DateTime d2 = Convert.ToDateTime(todate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 7)
                    {
                        errorMessage = "You cannot export report more than 7 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                fileName = "MerchantOrdersReport.xlsx";
                //Save the file to server temp folder
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), fileName);

                GetMerchantOrders w = new GetMerchantOrders();
               // if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                
                    w.StartDate = fromdate;
                    w.EndDate = todate;
                    w.Type = Convert.ToInt32(Type);
                    w.Status = Convert.ToInt32(Status);
                    w.Sign = Convert.ToInt32(Sign);
                    w.TransactionId = TransactionId;
                    w.TrackerId = TrackerId;
                    w.MerchantId = MerchantId;
                    w.MemberName = MemberName;
                    w.MemberContactNumber = MemberContactNumber;
                    w.OrderId = OrderId;
                
                DataTable dt = w.GetDataDump();
                if (dt != null && dt.Rows.Count > 0)
                {
                    AddExportData outobject_file = new AddExportData();
                    GetExportData inobject_file = new GetExportData();
                    inobject_file.Type = (int)AddExportData.ExportType.MerchantOrders;
                    AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);

                    if (res_file != null && res_file.Id != 0)
                    {

                        string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), res_file.FilePath);
                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                        res_file.FilePath = fullPath;
                        res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res_file.UpdatedDate = DateTime.UtcNow;
                        bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                        if (status)
                        {

                            Common.AddLogs("Merchant orders Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }
                    else
                    {
                        AddExportData export = new AddExportData();
                        export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        export.CreatedByName = Session["AdminUserName"].ToString();
                        export.FilePath = fullPath;
                        export.Type = (int)AddExportData.ExportType.MerchantOrders;
                        export.IsActive = true;
                        export.IsApprovedByAdmin = true;
                        Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                        if (Id > 0)
                        {
                            Common.AddLogs("Merchant orders Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(dt, "MerchantOrders");
                        ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                        ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        ws.Columns().AdjustToContents();  // Adjust column width
                        ws.Rows().AdjustToContents();
                        wb.SaveAs(fullPath);
                    }
                }
                else
                {
                    errorMessage = "No data found";
                    fileName = "";
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }
        [HttpPost]
        [Authorize]
        public ActionResult DownloadMerchantOrdersFileName()
        {
            var errorMessage = "";
            AddExportData outobject_file = new AddExportData();
            GetExportData inobject_file = new GetExportData();
            inobject_file.Type = (int)AddExportData.ExportType.MerchantOrders;
            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
            string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), res_file.FilePath);
            if (System.IO.File.Exists(fullPath))
            {
                return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
            }
            else
            {
                errorMessage = "";
                return Json(new { fileName = "", errorMessage = errorMessage });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult DownloadMerchantOrdersFile(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), fileName);
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelAllMerchantDump(string MerchantUniqueId, string Mobile, string Name, string Email, string fromdate, string todate, string MerchantType)
        {

            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(fromdate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(todate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
                {
                    DateTime d1 = Convert.ToDateTime(fromdate);
                    DateTime d2 = Convert.ToDateTime(todate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 7)
                    {
                        errorMessage = "You cannot export report more than 7 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                fileName = "MerchantUserReport.xlsx";
                //Save the file to server temp folder
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), fileName);

                //string fromdate = DateTime.UtcNow.AddDays(-7).ToString("dd-MMM-yyyy");
                //string todate = DateTime.UtcNow.ToString("dd-MMM-yyyy");

                GetMerchant w = new GetMerchant();
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    w.StartDate = fromdate;
                    w.EndDate = todate;
                    w.MerchantUniqueId = MerchantUniqueId;
                    w.FirstName = Name;
                    w.ContactNo = Mobile;
                    w.EmailID = Email;
                    if (MerchantType == "")
                    {
                        MerchantType = "0";
                    }
                    w.MerchantType = Convert.ToInt32(MerchantType);
                }
                DataTable dt = w.GetDataDump();
                if (dt != null && dt.Rows.Count > 0)
                {
                    AddExportData outobject_file = new AddExportData();
                    GetExportData inobject_file = new GetExportData();
                    inobject_file.Type = (int)AddExportData.ExportType.Merchant;
                    AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);

                    if (res_file != null && res_file.Id != 0)
                    {
                        string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), res_file.FilePath);
                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                        res_file.FilePath = fullPath;
                        res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                        if (status)
                        {

                            Common.AddLogs("Merchant Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }
                    else
                    {
                        AddExportData export = new AddExportData();
                        export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        export.CreatedByName = Session["AdminUserName"].ToString();
                        export.FilePath = fullPath;
                        export.Type = (int)AddExportData.ExportType.Merchant;
                        export.IsActive = true;
                        export.IsApprovedByAdmin = true;
                        Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                        if (Id > 0)
                        {
                            Common.AddLogs("Merchant Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(dt, "MerchantUser");
                        ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                        ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        ws.Columns().AdjustToContents();  // Adjust column width
                        ws.Rows().AdjustToContents();
                        wb.SaveAs(fullPath);
                    }

                }
                else
                {
                    errorMessage = "No data found for last 7 days";
                    fileName = "";
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }
        [HttpPost]
        [Authorize]
        public ActionResult DownloadMerchantFileName()
        {
            var errorMessage = "";
            AddExportData outobject_file = new AddExportData();
            GetExportData inobject_file = new GetExportData();
            inobject_file.Type = (int)AddExportData.ExportType.Merchant;

            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
            string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), res_file.FilePath);
            if (System.IO.File.Exists(fullPath))
            {
                return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
            }
            else
            {
                errorMessage = "No file found";
                return Json(new { fileName = "", errorMessage = errorMessage });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult DownloadMerchantFile(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), fileName);
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
        [HttpPost]
        [Authorize]
        public JsonResult MerchantIpBlockUnblock(AddMerchantIPAddress model)
        {
            AddMerchantIPAddress outobject = new AddMerchantIPAddress();
            GetMerchantIpAddress inobject = new GetMerchantIpAddress();
            inobject.Id = model.Id;
            AddMerchantIPAddress res = RepCRUD<GetMerchantIpAddress, AddMerchantIPAddress>.GetRecord(Common.StoreProcedures.sp_MerchantIPAddress_Get, inobject, outobject);
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
                bool IsUpdated = RepCRUD<AddMerchantIPAddress, GetMerchantIpAddress>.Update(res, "MerchantIPAddress");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Updated Merchant Ip";
                    Common.AddLogs("Updated MerchantIp of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant);

                }
                else
                {
                    ViewBag.Message = "Not Updated Merchant Ip";
                    Common.AddLogs("Not Updated Merchant Ip of(MerchantID: " + res.MerchantUniqueId + " )", true, (int)AddLog.LogType.Merchant);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreditDebit(string MerchantId = "0")
        {
            AddMerchant model = new AddMerchant();
            if (string.IsNullOrEmpty(MerchantId) || MerchantId == "0")
            {
                return RedirectToAction("Index");
            }
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            inobject.MerchantUniqueId = MerchantId;
            model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            if (!string.IsNullOrEmpty(MerchantId) && (model == null || model.Id == 0 || MerchantId == "0"))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreditDebit(string MerchantUniqueId, string TransactionAmount, string Type, string TransactionType, string AdminRemarksCD)
        {

            AddMerchant model = new AddMerchant();
            try
            {
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = MerchantUniqueId;
                model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                if (model != null && model.Id != 0 && !string.IsNullOrEmpty(MerchantUniqueId))
                {
                    string UserMessage = string.Empty;
                    string Referenceno = new CommonHelpers().GenerateUniqueId();
                    string msg = "";
                    if (Type == "0")
                    {
                        msg = RepTransactions.MerchantWalletUpdateFromAdmin(model, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD, "");
                    }
                    else if (Type == "1")
                    {
                        msg = RepTransactions.WalletUpdateFromAdmin(model.UserMemberId, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD, "");
                    }
                    if (msg.ToString().ToLower() == "success")
                    {
                        ViewBag.SuccessMessage = UserMessage;
                    }
                    else
                    {
                        ViewBag.Message = msg;
                        Common.AddLogs("Not Updated Merchanat user Wallet", true, (int)AddLog.LogType.Merchant);
                    }
                }
                else
                {
                    ViewBag.Message = "Merchant user Not Found";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }
            return View(model);
        }

        // GET: MerchantDirectWalletLoadReport
        [HttpGet]
        [Authorize]
        public ActionResult MerchantDirectWalletLoadReport()
        {
            List<SelectListItem> sign = new List<SelectListItem>();
            sign = CommonHelpers.GetSelectList_TxnSign();
            ViewBag.Sign = sign;
            List<SelectListItem> status = new List<SelectListItem>();
            status = CommonHelpers.GetSelectList_TxnStatus();
            ViewBag.Status = status;

            ViewBag.DumpURL = Common.dumpurl;

            return View();
        }

        [Authorize]
        public JsonResult GetMerchantDirectWalletLoadReportLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
            columns.Add("Amount");
            columns.Add("Remarks");
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
            string TransactionId = context.Request.Form["TransactionId"];
            string MerchantId = context.Request.Form["MerchantId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string Sign = context.Request.Form["Sign"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            string Reference = context.Request.Form["Reference"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            //Int64 MerchantMemberId = 0;
            //AddMerchant outobject = new AddMerchant();
            //GetMerchant inobject = new GetMerchant();
            ////inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
            //AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            //if (model != null && model.Id > 0)
            //{
            //    MerchantMemberId = model.MerchantMemberId;
            //}

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            //if (!string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
            //{
            //    w.MerchantMemberId = model.MerchantMemberId;
            //}
            //w.MerchantId = Session["MerchantUniqueId"].ToString();
            w.StartDate = fromdate;
            w.EndDate = todate;
            if (!String.IsNullOrEmpty(Sign))
            {
                w.Sign = Convert.ToInt32(Sign);
            }
            if (!String.IsNullOrEmpty(Status))
            {
                w.Status = Convert.ToInt32(Status);
            }
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load;
            w.TransactionUniqueId = TransactionId;
            w.Reference = Reference;
            w.MerchantId = MerchantId;
            if (!String.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            //w.RoleId = (int)AddUser.UserRoles.Merchant;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MerchantMemberId = Convert.ToInt64(row["MerchantMemberId"]),
                         MerchantId = Convert.ToString(row["MerchantId"]),
                         MerchantOrganization = Convert.ToString(row["MerchantOrganization"]),
                         RecieverName = Convert.ToString(row["RecieverName"]),
                         RecieverContactNumber = Convert.ToString(row["RecieverContactNumber"]),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         Status = Convert.ToInt32(row["Status"]),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                         MemberName = row["MemberName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"].ToString()),
                         Remarks = Convert.ToString(row["Remarks"].ToString()),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         Sign = Convert.ToInt32(row["Sign"]),
                         SignName = @Enum.GetName(typeof(WalletTransactions.Signs), Convert.ToInt64(row["Sign"])),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"].ToString()),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<WalletTransactions>> objDataTableResponse = new DataTableResponse<List<WalletTransactions>>
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
        public ActionResult ExportExcelMerchantDirectWalletLoadDump(string TrackerId, string Status, string Sign, string TransactionId, string fromdate, string todate)
        {

            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(fromdate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(todate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
                {
                    DateTime d1 = Convert.ToDateTime(fromdate);
                    DateTime d2 = Convert.ToDateTime(todate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 7)
                    {
                        errorMessage = "You cannot export report more than 7 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                fileName = "MerchantBankLoadReport.xlsx";
                //Save the file to server temp folder
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), fileName);

                WalletTransactions w = new WalletTransactions();
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    w.StartDate = fromdate;
                    w.EndDate = todate;
                    if (!String.IsNullOrEmpty(Sign))
                    {
                        w.Sign = Convert.ToInt32(Sign);
                    }
                    if (!String.IsNullOrEmpty(Status))
                    {
                        w.Status = Convert.ToInt32(Status);
                    }

                    w.TransactionUniqueId = TransactionId;
                    w.Reference = TrackerId;
                }
                w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load;
                DataTable dt = w.GetMerchantAllTxnDataDump();
                if (dt != null && dt.Rows.Count > 0)
                {
                    AddExportData outobject_file = new AddExportData();
                    GetExportData inobject_file = new GetExportData();
                    inobject_file.Type = (int)AddExportData.ExportType.Merchant;
                    AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);

                    if (res_file != null && res_file.Id != 0)
                    {
                        string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), res_file.FilePath);
                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                        res_file.FilePath = fullPath;
                        res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                        if (status)
                        {

                            Common.AddLogs("Merchant Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }
                    else
                    {
                        AddExportData export = new AddExportData();
                        export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        export.CreatedByName = Session["AdminUserName"].ToString();
                        export.FilePath = fullPath;
                        export.Type = (int)AddExportData.ExportType.Merchant;
                        export.IsActive = true;
                        export.IsApprovedByAdmin = true;
                        Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                        if (Id > 0)
                        {
                            Common.AddLogs("Merchant Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(dt, "MerchantUser");
                        ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                        ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        ws.Columns().AdjustToContents();  // Adjust column width
                        ws.Rows().AdjustToContents();
                        wb.SaveAs(fullPath);
                    }

                }
                else
                {
                    errorMessage = "No data found";
                    fileName = "";
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelMerchantAllTxnDump(string Sign, string Type, string Status, string SubscriberId, string Reference, string ParentTransactionId, string GatewayTransactionId, string MemberId, string ContactNumber, string Name, string TransactionId, string fromdate, string todate)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(fromdate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(todate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
                {
                    DateTime d1 = Convert.ToDateTime(fromdate);
                    DateTime d2 = Convert.ToDateTime(todate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 7)
                    {
                        errorMessage = "You cannot export report more than 7 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                fileName = "MerchantSettlementReport.xlsx";
                //Save the file to server temp folder
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), fileName);

                WalletTransactions w = new WalletTransactions();
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    if (!string.IsNullOrEmpty(MemberId))
                    {
                        w.MemberId = Convert.ToInt64(MemberId);
                    }
                    w.ContactNumber = ContactNumber;
                    w.MemberName = Name;
                    w.TransactionUniqueId = TransactionId;
                    w.StartDate = fromdate;
                    w.EndDate = todate;
                    w.Status = Convert.ToInt32(Status);
                    w.VendorTransactionId = GatewayTransactionId;
                    w.ParentTransactionId = ParentTransactionId;
                    w.Reference = Reference;
                    w.Sign = Convert.ToInt32(Sign);
                    w.CustomerID = SubscriberId;
                    w.Type = Convert.ToInt32(Type);
                }
                w.IsMerchantTxn = 1;
                w.RoleId = (int)AddUser.UserRoles.Merchant;
                DataTable dt = w.GetMerchantAllTxnDataDump();
                if (dt != null && dt.Rows.Count > 0)
                {
                    AddExportData outobject_file = new AddExportData();
                    GetExportData inobject_file = new GetExportData();
                    inobject_file.Type = (int)AddExportData.ExportType.Merchant;
                    AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);

                    if (res_file != null && res_file.Id != 0)
                    {
                        string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/Merchant"), res_file.FilePath);
                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                        res_file.FilePath = fullPath;
                        res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                        if (status)
                        {

                            Common.AddLogs("Merchant Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }
                    else
                    {
                        AddExportData export = new AddExportData();
                        export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        export.CreatedByName = Session["AdminUserName"].ToString();
                        export.FilePath = fullPath;
                        export.Type = (int)AddExportData.ExportType.Merchant;
                        export.IsActive = true;
                        export.IsApprovedByAdmin = true;
                        Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                        if (Id > 0)
                        {
                            Common.AddLogs("Merchant Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.MerchantTransactions);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(dt, "MerchantUser");
                        ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                        ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        ws.Columns().AdjustToContents();  // Adjust column width
                        ws.Rows().AdjustToContents();
                        wb.SaveAs(fullPath);
                    }

                }
                else
                {
                    errorMessage = "No data found";
                    fileName = "";
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

    }
}