using DeviceId;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Models.Request.WebRequest;
using MyPay.Models.Response.WebResponse;
using MyPay.Models.Response.WebResponse.Common;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPay.Controllers
{
    public class MyPayUserLoginController : Controller
    {
        // GET: MyPayUserLogin
        public ActionResult Index()
        {
            string deviceId = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();
            if (Request.Cookies["deviceId"] != null)
            {
                deviceId = Request.Cookies["deviceId"].Value;
            }
            else
            {
                deviceId = Common.EncryptString(Guid.NewGuid().ToString()) + "~" + Common.EncryptString(Common.GetUserIP());
                HttpCookie mydeviceId = new HttpCookie("deviceId");
                mydeviceId.Value = deviceId;  // Case sensitivity
                mydeviceId.Expires = DateTime.Now.AddDays(15);
                HttpContext.Response.Cookies.Add(mydeviceId);
            }
            Session["MyPayUserBrowserID"] = deviceId;
            return View();
        }
        public ActionResult LoginPin()
        {
            if (Session["MyPayUserMemberId"] == null || string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Name = Convert.ToString(Session["MyPayFullName"]);
            }
            return View();
        }

        [HttpPost]
        public JsonResult Index(AddUser model)
        {
            var result = string.Empty;
            try
            {
                string ApiName = "api/RegisterVerification";

                WebRequest_RegisterVerification objReq = new WebRequest_RegisterVerification();
                objReq.ContactNumber = model.ContactNumber;
                objReq.SecretKey = Common.SecretKeyForWebAPICall;
                objReq.PlatForm = "Web";
                objReq.PhoneExt = "977";
                objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                string JSON = JsonConvert.SerializeObject(objReq);
                result = Common.RequestMyPayAPI(ApiName, JSON, "");
                if (!string.IsNullOrEmpty(result))
                {
                    WebRes_UserDetail objResponse = new WebRes_UserDetail();
                    objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                    if (objResponse.status && objResponse.Message.ToLower() == "success")
                    {
                        Session["MyPayUserLoginContact"] = Convert.ToString(model.ContactNumber);
                        if (objResponse.IsDetailUpdated)
                        {
                            result = "Login";
                        }
                        else
                        {
                            result = "RegisterVerification";
                        }
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {

            }

            Session["MyPayUserLoginContact"] = model.ContactNumber;
            ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return Json(result);
        }

        [HttpGet]
        public ActionResult RegisterVerification(string Type)
        {
            if (Session["MyPayUserLoginContact"] == null || string.IsNullOrEmpty(Session["MyPayUserLoginContact"].ToString()))
            {
                return RedirectToAction("Index");
            }
            ViewBag.Type = string.IsNullOrEmpty(Type) ? "" : Type;
            ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return View();
        }
        public JsonResult RegisterVerification(string Type, string OTP)
        {
            string jsonresult = string.Empty;
            if (Session["MyPayUserLoginContact"] != null && !string.IsNullOrEmpty(Session["MyPayUserLoginContact"].ToString()))
            {
                WebRequest_Login objReq = new WebRequest_Login();
                string ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
                string ApiName = "";

                if (Type == "Verification")
                {
                    objReq.PhoneNumber = ContactNumber;
                    objReq.Digits = OTP;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.PlatForm = "Web";
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    ApiName = "api/LoginSms";
                }
                else if (Type == "Register")
                {
                    objReq.PhoneExt = "977";
                    objReq.ContactNumber = ContactNumber;
                    objReq.VerificationOtp = OTP;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.PlatForm = "Web";
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    ApiName = "api/AddUser";
                }
                var result = string.Empty;

                string JSON = JsonConvert.SerializeObject(objReq);
                result = Common.RequestMyPayAPI(ApiName, JSON, "");

                if (!string.IsNullOrEmpty(result))
                {
                    WebRes_UserDetail objResponse = new WebRes_UserDetail();
                    objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                    if (objResponse.ReponseCode == 1)
                    {
                        if (Type == "Verification")
                        {
                            RepMyPayUserLogin.Login(objResponse.ContactNumber, "", false, false, objResponse.Token);
                            var expiration = DateTime.Now.AddHours(1);
                            FormsAuthentication.SetAuthCookie(ContactNumber, false);
                            var authTicket = new FormsAuthenticationTicket(
                            1,
                            Convert.ToString(ContactNumber),
                            DateTime.Now,
                            expiration,
                            false,
                            string.Empty,
                            "/"
                            );
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                            Response.Cookies.Add(cookie);
                        }
                        objResponse.Message = "success";
                        jsonresult = JsonConvert.SerializeObject(objResponse);
                    }
                    else
                    {
                        jsonresult = objResponse.Message;
                    }
                }
                else
                {
                    jsonresult = "Invalid API Request";
                }
            }
            else
            {
                jsonresult = "Invalid API Request";
            }
            //ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return Json(jsonresult);
        }
        public ActionResult Login()
        {
            if (Session["MyPayUserLoginContact"] == null || string.IsNullOrEmpty(Session["MyPayUserLoginContact"].ToString()))
            {
                return RedirectToAction("Index");
            }
            ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return View();
        }

        [HttpPost]
        public JsonResult RequestLogin(string UserName, string Password)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserLoginContact"] != null && !string.IsNullOrEmpty(Session["MyPayUserLoginContact"].ToString()))
                {
                    string ApiName = "api/Login";
                    string ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
                    string DeviceId = string.Empty;
                    // DeviceId = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();
                    //HttpCookie DeviceInfo1 = Request.Cookies["CookieInfo"];
                    //if (DeviceInfo1 != null)
                    //{
                    //    DeviceId = DeviceInfo1["InfoID"].ToString();
                    //}
                    //else
                    //{
                    //    DeviceId = Common.EncryptString(Guid.NewGuid().ToString()) + "~" + Common.EncryptString(Common.GetUserIP());
                    //}
                    //// Check User IP Validatoin
                    //if (DeviceId.Split('~')[1] != Common.EncryptString(Common.GetUserIP()))
                    //{
                    //    DeviceId = Common.EncryptString(Guid.NewGuid().ToString()) + "~" + Common.EncryptString(Common.GetUserIP());
                    //}
                    //HttpCookie DeviceInfo = new HttpCookie("CookieInfo");
                    //DeviceInfo["InfoID"] = DeviceId;
                    //DeviceInfo.Expires.Add(new TimeSpan(20, 0, 0, 0));
                    //Response.Cookies.Add(DeviceInfo);
                    //Session["MyPayUserBrowserID"] = Common.DecryptString(DeviceId.Split('~')[0]) + "~" + Common.DecryptString(DeviceId.Split('~')[1]);

                    AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                    inobject.ContactNumber = ContactNumber;
                    inobject.CheckDelete = 0;
                    AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                    if (res != null && res.Id != 0)
                    {
                        if (Common.DecryptString(res.Password) == Password)
                        {
                            Session["MyPayUserJWTToken"] = res.JwtToken;
                            if (string.IsNullOrEmpty(res.DeviceId))
                            {
                                Session["MyPayUserBrowserID"] = Common.RandomString(20);
                            }
                            else
                            {
                                Session["MyPayUserBrowserID"] = res.DeviceId;
                            }
                        }
                        WebRequest_Login objReq = new WebRequest_Login();
                        objReq.PhoneNumber = ContactNumber;
                        objReq.Password = Common.Encryption(Password);
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.PlatForm = "Web";
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        result = Common.RequestMyPayAPI(ApiName, JSON, res.JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_UserDetail objResponse = new WebRes_UserDetail();
                            objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                RepMyPayUserLogin.Login(ContactNumber, Password, false, false, objResponse.Token);
                                var expiration = DateTime.Now.AddHours(1);
                                FormsAuthentication.SetAuthCookie(ContactNumber, false);
                                var authTicket = new FormsAuthenticationTicket(
                                1,
                                Convert.ToString(ContactNumber),
                                DateTime.Now,
                                expiration,
                                false,
                                string.Empty,
                                "/"
                                );
                                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                                Response.Cookies.Add(cookie);
                            }
                            else if (objResponse.ReponseCode == 12)
                            {
                                result = "12";
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }

                    }
                    else
                    {
                        result = "User not found";
                    }

                }
                else
                {
                    result = "Invalid Request";
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ForgotPassword(AddUser model)
        {
            var result = string.Empty;
            try
            {
                string ApiName = "api/ForgotPasswordWithPhone";

                WebRequest_RegisterVerification objReq = new WebRequest_RegisterVerification();
                objReq.ContactNumber = model.ContactNumber;
                objReq.SecretKey = Common.SecretKeyForWebAPICall;
                objReq.PlatForm = "Web";
                string JSON = JsonConvert.SerializeObject(objReq);
                result = Common.RequestMyPayAPI(ApiName, JSON, "");
                if (!string.IsNullOrEmpty(result))
                {
                    WebRes_UserDetail objResponse = new WebRes_UserDetail();
                    objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                    if (objResponse.status && objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                    {
                        result = "success";
                    }
                    else if (objResponse.ReponseCode != 1)
                    {
                        result = objResponse.Message;
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return Json(result);
        }
        [HttpPost]
        public JsonResult ResetPassword(string ContactNumber, string VerificationCode, string Password, string RePassword)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ContactNumber))
                {
                    result = "Please enter contact number";
                }
                else if (string.IsNullOrEmpty(VerificationCode))
                {
                    result = "Please enter OTP";
                }
                else if (VerificationCode.Length < 6)
                {
                    result = "Please enter 6 digit OTP";
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    result = "Please enter Password";
                }
                else if (string.IsNullOrEmpty(RePassword))
                {
                    result = "Please enter Confirm Password";
                }
                else if (Password != RePassword)
                {
                    result = "Password and Confirm Password does not match";
                }
                else if (Password.Length < 8)
                {
                    result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                }
                else if (!string.IsNullOrEmpty(Password))
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    string ApiName = "api/ResetPasswordWithOTC";

                    WebRequest_ResetPassword objReq = new WebRequest_ResetPassword();
                    objReq.ContactNumber = ContactNumber;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.PlatForm = "Web";
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.Password = Password;
                    objReq.OTC = VerificationCode;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    result = Common.RequestMyPayAPI(ApiName, JSON, "");
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebRes_UserDetail objResponse = new WebRes_UserDetail();
                        objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                        {
                            result = "success";
                        }
                        else if (objResponse.ReponseCode != 1)
                        {
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return Json(result);
        }
        [HttpPost]
        public JsonResult ResendOTP(string ContactNumber)
        {
            var result = string.Empty;
            try
            {
                string ApiName = "api/ResendOTPUser";

                WebRequest_RegisterVerification objReq = new WebRequest_RegisterVerification();
                objReq.ContactNumber = ContactNumber;
                objReq.SecretKey = Common.SecretKeyForWebAPICall;
                objReq.PlatForm = "Web";
                objReq.PhoneExt = "977";
                objReq.DeviceId = Session["MyPayUserBrowserID"].ToString();
                string JSON = JsonConvert.SerializeObject(objReq);
                result = Common.RequestMyPayAPI(ApiName, JSON, "");
                if (!string.IsNullOrEmpty(result))
                {
                    WebRes_UserDetail objResponse = new WebRes_UserDetail();
                    objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                    if (objResponse.status && objResponse.Message.ToLower() == "success")
                    {
                        Session["MyPayUserLoginContact"] = Convert.ToString(ContactNumber);
                        if (objResponse.IsDetailUpdated)
                        {
                            result = "Login";
                        }
                        else
                        {
                            result = "RegisterVerification";
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {

            }

            Session["MyPayUserLoginContact"] = ContactNumber;
            ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return Json(result);
        }


        [HttpPost]
        public JsonResult LoginUserWithPin(string Pin)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Pin) || Pin == "")
                {
                    result = "Please Enter Pin";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/LoginWithPin";
                        WebRequest_Pin objReq = new WebRequest_Pin();
                        objReq.Pin = Common.Encryption(Pin);
                        objReq.MemberId = Convert.ToInt32(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.ReponseCode.ToString() == "1")
                            {
                                string URL = string.Empty;
                                if (Common.ApplicationEnvironment.IsProduction)
                                {
                                    URL = Common.LiveApiUrl;
                                }
                                else
                                {
                                    URL = Common.TestApiUrl;
                                }

                                string Result = string.Empty;
                                string token = Common.GetWebToken();
                                VendorApi_CommonHelper.MyPayWebPostMethod(URL + "api/GetUserWebToken", JSON, JwtToken, token);
                                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                                inobject.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                                inobject.ContactNumber = Convert.ToString(Session["MyPayContactNumber"]);
                                inobject.CheckDelete = 0;
                                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                                if (res != null && res.Id != 0 && res.IsActive)
                                {
                                    Common.SetMyPayUserSession(res);
                                }
                                result = "success";
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]
        public ActionResult Dashboard()
        {
            AddUserLoginWithPin model = new AddUserLoginWithPin();
            string msg = "False";
            if (Session["MyPayUserMemberId"] != null)
            {
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"].ToString());
                model = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                if (model.IsKYCApproved == 0)
                {
                    ViewBag.IsKYCApproved = "False";
                }
                else
                {
                    ViewBag.IsKYCApproved = "True";
                }
                if (Session["UserIsFirstLogin"] == null || Convert.ToString(Session["UserIsFirstLogin"]) != "1")
                {
                    ViewBag.IsFirstLogin = "0";
                }
                else
                {
                    ViewBag.IsFirstLogin = "1";
                }
                Session["UserIsFirstLogin"] = null;
            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.ResetPassword = msg;

            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "This is MyPayUser Change Password Page";
            AddUser model = new AddUser();

            if (Session["MyPayUserMemberId"] != null)
            {
                AddUser outobject = new AddUser();
                GetUser inobject = new GetUser();
                inobject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                model = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject, outobject);
                if (model != null && model.Id != 0)
                {
                    ViewBag.MyPayUserOldPassword = Common.DecryptString(model.Password);
                }
                else
                {
                    ViewBag.MyPayUserOldPassword = "";
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(string Password, string passwordconfirm)
        {
            string msg = "";
            AddUser model = new AddUser();
            if (Session["MyPayUserMemberId"] != null)
            {
                if (string.IsNullOrEmpty(Password) || Password == "")
                {
                    msg = "Please Enter New Password";
                }
                else if (string.IsNullOrEmpty(passwordconfirm) || passwordconfirm == "")
                {
                    msg = "Please Enter Confirm Password";
                }
                else if (Password != passwordconfirm)
                {
                    msg = "Confirm Password doesn't match.";
                }
                else if (Password.Length < 8)
                {
                    msg = "Minimum Password length should be 8 characters.";
                }
                else if (Password.IndexOf(":") >= 0)
                {
                    msg = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                }
                else if (Password != "")
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        msg = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";

                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    if (!string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        AddUser outobject = new AddUser();
                        GetUser inobject = new GetUser();
                        inobject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"].ToString());
                        model = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject, outobject);
                        if (model != null && model.Id > 0)
                        {
                            ViewBag.MyPayUserOldPassword = Common.DecryptString(model.Password);
                            if (Common.DecryptString(model.Password) == Password)
                            {
                                msg = "New Password cannot be same as old Password!";
                            }
                            if (string.IsNullOrEmpty(msg))
                            {
                                model.Password = Common.EncryptString(Password);
                                model.UpdatedDate = DateTime.UtcNow;
                                bool status = RepCRUD<AddUser, GetUser>.Update(model, "MyPayUser");
                                if (status)
                                {
                                    msg = "Successfully Updated Password.";
                                    ViewBag.SuccessMessage = msg;
                                    Common.AddLogs("Updated MyPayUser Password of (MyPayUserMemberId:" + model.MemberId.ToString() + "  by(MyPayUserMemberId:" + Session["MyPayUserMemberId"].ToString() + ")", true, (int)AddLog.LogType.User);
                                    FormsAuthentication.SignOut();
                                    TempData["AdminMessage"] = "Password changed successfully. Please login to continue.";
                                    return RedirectToAction("/Index");
                                }
                                else
                                {
                                    msg = "Not Updated.";
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(ViewBag.SuccessMessage))
                                {
                                    ViewBag.Message = msg;
                                }
                            }
                        }
                    }
                    else
                    {
                        msg = "Session expired";
                        FormsAuthentication.SignOut();
                    }
                }
                if (string.IsNullOrEmpty(ViewBag.SuccessMessage))
                {
                    ViewBag.Message = msg;
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            if (Common.RemoveMyPayUserSession())
            {
                return RedirectToAction("/Index");
            }
            else
            {
                return RedirectToAction("/Dashboard");
            }
        }
        [HttpPost]
        public JsonResult BindDashboard(string Type)
        {
            AddUserDashboard res_dashboard = new AddUserDashboard();
            return Json(res_dashboard, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SignupDetail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SignupDetail(string FirstName, string LastName, string Gender, string Email, string AlternateNumber, string RefferalCode)
        {
            string jsonresult = string.Empty;
            if (Session["MyPayUserLoginContact"] != null && !string.IsNullOrEmpty(Session["MyPayUserLoginContact"].ToString()))
            {
                if (string.IsNullOrEmpty(FirstName))
                {
                    jsonresult = "Please Enter First Name";
                }
                else if (string.IsNullOrEmpty(LastName))
                {
                    jsonresult = "Please Enter Last Name";
                }
                else if (string.IsNullOrEmpty(Gender) || Gender == "0")
                {
                    jsonresult = "Please Select Gender";
                }
                else if (!string.IsNullOrEmpty(Email) && !Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    jsonresult = "Please Enter Valid Email";
                }
                Req_Web_User objReq = new Req_Web_User();
                string ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
                string ApiName = "api/UserPersonalUpdate";

                AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                inuser.ContactNumber = ContactNumber;
                AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                var result = string.Empty;
                objReq.MemberId = resuser.MemberId.ToString();
                objReq.Email = Email;
                objReq.FirstName = FirstName;
                objReq.LastName = LastName;
                objReq.Email = Email;
                objReq.AlternateNumber = AlternateNumber;
                objReq.RefCode = RefferalCode;
                objReq.Gender = Gender;
                objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                objReq.PlatForm = "Web";
                objReq.SecretKey = Common.SecretKeyForWebAPICall;

                string JSON = JsonConvert.SerializeObject(objReq);
                if (resuser != null && resuser.Id > 0)
                {
                    result = Common.RequestMyPayAPI(ApiName, JSON, resuser.JwtToken);
                }
                else
                {
                    result = Common.RequestMyPayAPI(ApiName, JSON, "");
                }

                if (!string.IsNullOrEmpty(result))
                {
                    WebRes_UserDetail objResponse = new WebRes_UserDetail();
                    objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                    if (objResponse.ReponseCode == 1 && objResponse.Message.ToLower() == "success")
                    {
                        jsonresult = "success";
                    }
                    else
                    {
                        jsonresult = objResponse.Message;
                    }
                }
                else
                {
                    jsonresult = "Invalid API Request";
                }
            }
            else
            {
                jsonresult = "Invalid API Request";
            }
            //ViewBag.ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
            return Json(jsonresult);
        }

        [HttpPost]
        public JsonResult CreatePassword(string Password, string RePassword)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Password) || Password == "")
                {
                    result = "Please Enter New Password";
                }
                else if (string.IsNullOrEmpty(RePassword) || RePassword == "")
                {
                    result = "Please Enter Confirm Password";
                }
                else if (Password != RePassword)
                {
                    result = "Confirm Password doesn't match.";
                }
                else if (Password.Length < 8)
                {
                    result = "Minimum Password length should be 8 characters.";
                }
                else if (Password.IndexOf(":") >= 0)
                {
                    result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                }
                else if (Password != "")
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";

                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserLoginContact"] != null && !string.IsNullOrEmpty(Session["MyPayUserLoginContact"].ToString()))
                    {
                        string ApiName = "api/ResetPassword";
                        string ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
                        AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                        GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                        inuser.ContactNumber = ContactNumber;
                        AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                        WebRequest_Login objReq = new WebRequest_Login();

                        objReq.Password = Password;
                        objReq.ConfirmPassword = RePassword;
                        objReq.MemberId = Convert.ToInt32(resuser.MemberId);
                        objReq.IsMobile = false;
                        //objReq.Password = Common.Encryption(Password);
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        result = Common.RequestMyPayAPI(ApiName, JSON, resuser.JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_UserDetail objResponse = new WebRes_UserDetail();
                            objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                result = "success";
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult CreatePin(string Pin, string ConfirmPin, string Password)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Pin) || Pin == "")
                {
                    result = "Please Enter New Pin";
                }
                else if (string.IsNullOrEmpty(ConfirmPin) || ConfirmPin == "")
                {
                    result = "Please Enter Confirm Pin";
                }
                else if (Pin != ConfirmPin)
                {
                    result = "Confirm Pin doesn't match.";
                }
                else if (Pin.Length < 4)
                {
                    result = "Pin must be 4 characters long";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserLoginContact"] != null && !string.IsNullOrEmpty(Session["MyPayUserLoginContact"].ToString()))
                    {
                        string ApiName = "api/SetPin";
                        string ContactNumber = Convert.ToString(Session["MyPayUserLoginContact"]);
                        AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                        GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                        inuser.ContactNumber = ContactNumber;
                        AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);

                        if (resuser != null && resuser.Id != 0)
                        {
                            Session["MyPayUserBrowserID"] = resuser.DeviceId;
                            Session["MyPayUserJWTToken"] = resuser.JwtToken;
                        }

                        WebRequest_Pin objReq = new WebRequest_Pin();
                        objReq.Pin = Pin;
                        objReq.ConfirmPin = ConfirmPin;
                        objReq.Password = Password;
                        objReq.MemberId = Convert.ToInt32(resuser.MemberId);
                        objReq.IsMobile = false;
                        //objReq.Password = Common.Encryption(Password);
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        result = Common.RequestMyPayAPI(ApiName, JSON, resuser.JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_UserDetail objResponse = new WebRes_UserDetail();
                            objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                            if (objResponse.ReponseCode == 1)
                            {
                                RepMyPayUserLogin.Login(ContactNumber, Password, false, false, resuser.JwtToken);
                                var expiration = DateTime.Now.AddHours(1);
                                FormsAuthentication.SetAuthCookie(ContactNumber, false);
                                var authTicket = new FormsAuthenticationTicket(
                                1,
                                Convert.ToString(ContactNumber),
                                DateTime.Now,
                                expiration,
                                false,
                                string.Empty,
                                "/"
                                );
                                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                                Response.Cookies.Add(cookie);
                                result = "success";
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


    }
}