using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using NepaliCalendarBS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using log4net;

namespace MyPay.Repository
{
    public static class RepUser
    {
        //public static Int64 MemberId = 0;
        public static string VerificationOtp = string.Empty;
        public static decimal RewardPoint = 0;
        public static string UserId = string.Empty;
        public static string Token = string.Empty;
        public static string ContactNumber = string.Empty;

        public static string Register(ref bool IsCouponUnlocked, ref AddUser res, string DeviceID, string Lat, string Lon, string PhoneNumber, string RefCode, string PhoneExt, string Otp, string platform, string devicecode, bool ismobile, string UserInput, string authenticationToken, ref Int64 MemberId_Return, bool IsMerchant = false, int MerchantType = 0)
        {
            try
            {
                if (RefCode == "FF691360")
                {
                    return "Invalid RefCode";
                }
                if (string.IsNullOrEmpty(DeviceID))
                {
                    return "Please enter user DeviceID.";
                }
                if (string.IsNullOrEmpty(PhoneNumber))
                {
                    return "Please enter a valid 10 digit phone number.";
                }
                else if (string.IsNullOrEmpty(PhoneExt))
                {
                    return "Please select country code.";
                }
                else if (PhoneNumber.Length < 10)
                {
                    return "Please enter a valid 10 digit phone number.";
                }
                bool isdigitpresent = PhoneNumber.Any(c => char.IsLetter(c));
                if (isdigitpresent)
                {
                    return "Please enter a valid 10 digit phone number.";
                }
                if (string.IsNullOrEmpty(Otp))
                {
                    return "Please enter the verification OTP that you have received on your phone number";
                }
                else
                {
                    AddUserOTPAttempt objOTPAttemp = new AddUserOTPAttempt();
                    objOTPAttemp.ContactNumber = PhoneNumber;
                    objOTPAttemp.GetRecord();

                    if (objOTPAttemp.AttemptCount < 5 || (System.DateTime.UtcNow - objOTPAttemp.AttemptDateTime).TotalHours > 2)
                    {
                        AddVerification outobjectver = new AddVerification();
                        GetVerification inobjectver = new GetVerification();
                        inobjectver.PhoneNumber = PhoneNumber;
                        inobjectver.PhoneExtension = PhoneExt;
                        inobjectver.Otp = Common.EncryptString(Otp);
                        inobjectver.VerificationType = (int)AddVerification.VerifyType.Register;
                        AddVerification resver = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobjectver, outobjectver);

                        if (resver != null && resver.Id != 0)
                        {
                            resver.IsVerified = true;
                            bool statusver = RepCRUD<AddVerification, GetVerification>.Update(resver, "verification");
                            if (statusver)
                            {
                                AddUser outobject = new AddUser();
                                GetUser inobject = new GetUser();
                                inobject.ContactNumber = PhoneNumber;
                                res = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);

                                if (res != null && res.Id != 0)
                                {
                                    //MemberId = res.MemberId;
                                    VerificationOtp = string.Empty;// res.VerificationCode;
                                    UserId = res.UserId;
                                    // *****************************************************************//
                                    // ************* PHONE VERIFIED BECAUSE OTP WAS CORRECT ***********//
                                    // ***************************************************************//
                                    res.IsPhoneVerified = true;
                                    res.DeviceCode = devicecode;
                                    res.DeviceId = DeviceID;
                                    res.JwtToken = new CommonHelpers().GetJWToken(res.ContactNumber);
                                    Token = res.JwtToken;
                                    res.LoginUpdate(authenticationToken);
                                    if (objOTPAttemp.Id == 0)
                                    {
                                        objOTPAttemp.Add();
                                    }
                                    MemberId_Return = res.MemberId;
                                    //DeactiveDeviceFromMultipleLogins(authenticationToken, platform, devicecode, res);
                                    return "Your phone number (" + PhoneExt + ") " + PhoneNumber + " already has an account on this app.";

                                }
                                else
                                {

                                    //string RandomUserId = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();

                                    res.ContactNumber = PhoneNumber;
                                    res.AlternateContactNumber = PhoneNumber;
                                    res.PhoneExtension = PhoneExt;
                                    res.IsPhoneVerified = true;
                                    res.CountryId = 216;
                                    res.Lat = Lat;
                                    res.Lon = Lon;
                                    res.VerificationCode = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                                    //res.TransactionPassword = objcls.EncryptString(password);
                                    //res.Password = objcls.EncryptString(password);
                                    res.PlatForm = platform;
                                    res.RoleName = AddUser.UserRoles.User.ToString();
                                    res.RoleId = (int)AddUser.UserRoles.User;
                                    if (IsMerchant)
                                    {
                                        res.RoleName = AddUser.UserRoles.Merchant.ToString();
                                        res.RoleId = (int)AddUser.UserRoles.Merchant;
                                    }
                                    res.DeviceCode = devicecode;
                                    res.DeviceId = DeviceID;

                                    res.IsKYCApproved = (int)AddUser.kyc.Not_Filled;
                                    res.IsActive = true;
                                    res.IsApprovedByAdmin = true;
                                    res.IpAddress = Common.GetUserIP();
                                    res.LastLoginAttempt = System.DateTime.UtcNow;
                                    res.CreatedBy = Common.GetCreatedById(authenticationToken);
                                    res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                    // *********** RefCode Generate *************
                                    //Guid g = Guid.NewGuid();
                                    //res.RefCode = g.ToString().Substring(0, 4) + MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                                    MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                                    res.RefCode = commonHelpers.GetScalarValueWithValue("SELECT [dbo].[fnGetRefCodeUnique]();");
                                    // *********** RefCode Check *************
                                    // **********  RefCode  NOT USED NOW IN THIS API : USED IN USER PERSONAL UPDATE ***********
                                    AddUserLoginWithPin refres_Parent = new AddUserLoginWithPin();
                                    if (!string.IsNullOrEmpty(RefCode) && res.RefCode != RefCode)
                                    {
                                        AddUserLoginWithPin outrefobject_Parent = new AddUserLoginWithPin();
                                        GetUserLoginWithPin inrefobject_Parent = new GetUserLoginWithPin();
                                        inrefobject_Parent.RefCode = RefCode;
                                        refres_Parent = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord("sp_Users_GetLoginWithPin", inrefobject_Parent, outrefobject_Parent);
                                        if (refres_Parent.Id > 0)
                                        {
                                            if (refres_Parent.IsKYCApproved == (int)AddUser.kyc.Verified)
                                            {
                                                res.RefId = refres_Parent.MemberId;
                                            }
                                        }
                                        else
                                        {
                                            return "RefCode is Invalid. Please try again.";
                                        }
                                    }
                                    res.JwtToken = new CommonHelpers().GetJWToken(res.ContactNumber);
                                    res.TokenUpdatedTime = DateTime.UtcNow.AddMinutes(5);
                                    res.MemberId = GetNewId();
                                    res.UserId = "UD" + res.MemberId.ToString();
                                    Int64 Id = RepCRUD<AddUser, GetUser>.Insert(res, "user");
                                    if (Id > 0)
                                    {
                                        res.Id = Id;
                                        Token = res.JwtToken;
                                        //MemberId = res.MemberId;
                                        VerificationOtp = string.Empty;// res.VerificationCode;
                                        UserId = res.UserId;
                                        Models.Common.Common.AddLogs("User registered successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.FirstName, ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.User_Register);

                                        if (res.RoleId == (int)AddUser.UserRoles.User || (res.RoleId == (int)AddUser.UserRoles.Merchant && MerchantType == (int)AddMerchant.MerChantType.Merchant))
                                        {
                                            #region SendEmailConfirmation
                                            string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/RegisterMail.html"));
                                            string body = mystring;
                                            string Subject = MyPay.Models.Common.Common.WebsiteName + " - Welcome to MyPay";
                                            if (!string.IsNullOrEmpty(res.Email))
                                            {
                                                Common.SendAsyncMail(res.Email, Subject, body);
                                            }
                                            #endregion
                                        }
                                        //*********SIGN UP BONUS DISTRIBUTION ********* //
                                        bool IsSignUpBonusDistributed = Common.SignUpBonusDistribution(platform, devicecode, ismobile, res, authenticationToken);
                                        // *********  NEW DEVICE REGISTRATION HISTORY ********* //
                                        //DeactiveDeviceFromMultipleLogins(authenticationToken, platform, devicecode, res);
                                        // *********  Coupon is provided only on Register and KYC ********* //
                                        //IsCouponUnlocked = Common.AssignCoupons(res.MemberId, "", (int)AddCoupons.CouponReceivedBy.SignUp);

                                        if (objOTPAttemp.Id == 0)
                                        {

                                            objOTPAttemp.MemberId = res.MemberId;
                                            objOTPAttemp.Add();
                                        }
                                        else
                                        {
                                            objOTPAttemp.AttemptCount = 0;
                                            objOTPAttemp.Update();
                                        }
                                        MemberId_Return = res.MemberId;
                                        return "success";
                                    }
                                    else
                                    {
                                        Models.Common.Common.AddLogs("User not registered successfully", false, Convert.ToInt32(AddLog.LogType.User), 0, res.ContactNumber, ismobile, platform, devicecode);
                                        return "Something went wrong. Please try again later.";
                                    }
                                }

                            }
                            else
                            {
                                return "Something went wrong. Please try again later.";
                            }
                        }
                        else
                        {
                            bool InValidOTPUpdate = Common.InvalidOTPUpdate(ref objOTPAttemp, PhoneNumber);
                            return "OTP you entered is wrong. Please try again. ";
                        }
                    }
                    else
                    {
                        return Common.InvalidOTPMessage;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return e.Message;
                throw;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string UpdateReadNotification(string authenticationToken, Int64 MemberId, Int64 NotificationId, string platform, bool IsMobile, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter member id";
                    return result;
                }
                else if (NotificationId == 0)
                {
                    result = "Please enter notification id";
                    return result;
                }
                AddNotification outobject = new AddNotification();
                GetNotification inobject = new GetNotification();
                inobject.MemberId = MemberId;
                inobject.Id = NotificationId;
                AddNotification res = RepCRUD<GetNotification, AddNotification>.GetRecord(Common.StoreProcedures.sp_Notification_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    res.ReadStatus = 1;

                    bool status = RepCRUD<AddNotification, GetNotification>.Update(res, "notification");
                    if (status)
                    {
                        Models.Common.Common.AddLogs("Notification read status changed successfully", false, Convert.ToInt32(AddLog.LogType.User), MemberId, "", IsMobile, platform, devicecode);
                        result = "success";
                    }
                }
                else
                {
                    result = Common.CommonMessage.Data_Not_Found;
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Notification read status Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string RegisterVerification(ref string ReturnString, ref bool IsLoginWithPassword, ref bool IsDetailUpdated, string authenticationToken, string PhoneNumber, string PhoneExt, string platform, string devicecode, bool ismobile, ref string Otp, string sendotp = "", bool IsMerchant = false, string version_number= "")
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



            string msg = string.Empty;
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                msg = "Please enter a valid 10 digit phone number.";
            }
            else if (string.IsNullOrEmpty(PhoneExt))
            {
                msg = "Please select PhoneExt.";
            }
            else if (PhoneNumber.Length < 10)
            {
                msg = "Please enter a valid 10 digit phone number.";
            }
            bool isdigitpresent = PhoneNumber.Any(c => char.IsLetter(c));
            if (isdigitpresent)
            {
                msg = "Please enter a valid 10 digit phone number.";
            }
            else
            {
                string SMSText = string.Empty;

                AddVerification outobject = new AddVerification();
                GetVerification inobject = new GetVerification();
                inobject.PhoneNumber = PhoneNumber;
                inobject.PhoneExtension = PhoneExt;
                inobject.VerificationType = (int)AddVerification.VerifyType.Register;

                log.Info("sp_verification_Get called");
                AddVerification res = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobject, outobject);
                log.Info("sp_verification_Get finished");

                if (res != null && res.Id != 0)
                {
                    if (ismobile)
                    {
                        string NotEncryptOTP = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                        if (Common.SandboxContactsList.Contains(PhoneNumber))
                        {
                            NotEncryptOTP = "123456";
                        }

                        if (!Common.ApplicationEnvironment.IsProduction)
                        {
                            NotEncryptOTP = "123456";
                        }

                        res.Otp = Common.EncryptString(NotEncryptOTP);
                        Otp = NotEncryptOTP;

                        log.Info("update verification called");
                        bool statusver = RepCRUD<AddVerification, GetVerification>.Update(res, "verification");
                        log.Info("update verification ended");

                        if (statusver)
                        {
                            if (res.IsVerified)
                            {
                                AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                                GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                                inobjectUser.ContactNumber = PhoneNumber;

                                log.Info("sp_Users_GetLoginWithPin called");
                                AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                                log.Info("sp_Users_GetLoginWithPin ended");

                                if (resUser != null && resUser.RoleId == (int)AddUser.UserRoles.Merchant)
                                {
                                    msg = "This user already registered as Merchant";
                                }
                                else
                                {
                                    if (resUser != null && resUser.Id != 0)
                                    {
                                        resUser.DeviceCode = devicecode;
                                        resUser.PlatForm = platform;
                                        //resUser.LastLogin = System.DateTime.UtcNow;
                                        //resUser.LastLoginAttempt = System.DateTime.UtcNow;
                                        //resUser.LoginAttemptCount = 0;
                                        if (resUser.FirstName != "")
                                        {
                                            IsDetailUpdated = true;
                                        }

                                        log.Info("LoginUpdate called");
                                        bool isUpdated = resUser.LoginUpdate(authenticationToken, version_number);
                                        log.Info("LoginUpdate ended");

                                        //DeactiveDeviceFromMultipleLogins(authenticationToken, platform, devicecode, resUser);
                                    }

                                    if (resUser.FirstName != "" && resUser.Pin != "" && resUser.IsPhoneVerified && resUser.Password != "")
                                    {
                                        ReturnString = "Login";
                                        msg = ReturnString;
                                        IsLoginWithPassword = true;
                                    }
                                    else
                                    {
                                        if (!IsMerchant)
                                        {
                                           // SMSText = "Thank you for registering with MyPay.Your activation code is " + NotEncryptOTP + ".Please enter this code to activate MyPay.Thank you for using MyPay";
                                            SMSText = "Thank you for registering with MyPay.Use code " + NotEncryptOTP + " to activate MyPay. Stand a chance to win 1 Tola Gold. For more info, contact: 16600162000.";
                                            //Models.Common.Common.SendSMS(res.PhoneNumber, SMSText);
                                            CheckOTPLimit(PhoneNumber, SMSText, ref ReturnString);
                                            if (string.IsNullOrEmpty(ReturnString))
                                            {
                                                ReturnString = "Get User Detail";
                                            }
                                            msg = ReturnString;
                                        }
                                        else
                                        {
                                            msg = "Get User Detail";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!IsMerchant)
                                {
                                    //SMSText = "Thank you for registering with MyPay.Your activation code is " + NotEncryptOTP + ".Please enter this code to activate MyPay.Thank you for using MyPay";
                                    SMSText = "Thank you for registering with MyPay.Use code " + NotEncryptOTP + " to activate MyPay. Stand a chance to win 1 Tola Gold. For more info, contact: 16600162000.";

                                    //Models.Common.Common.SendSMS(res.PhoneNumber, SMSText);
                                    if (!string.IsNullOrEmpty(SMSText))
                                    {
                                        CheckOTPLimit(PhoneNumber, SMSText, ref ReturnString);
                                    }
                                    msg = ReturnString;
                                }
                                else
                                {
                                    msg = "success";
                                }

                            }
                            Otp = res.Otp;
                        }
                    }
                    else
                    {
                        msg = "Your phone number (" + PhoneExt + ") " + PhoneNumber + " already has an account on this app.";
                    }
                }
                else
                {
                    string NotEncryptOTP = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();

                    res.VerificationType = (int)AddVerification.VerifyType.Register;
                    res.PhoneExtension = PhoneExt;
                    res.PhoneNumber = PhoneNumber;
                    res.IsActive = true;
                    res.Type = (int)AddVerification.PlatformType.Phone;
                    if (Common.SandboxContactsList.Contains(PhoneNumber))
                    {
                        NotEncryptOTP = "123456";
                    }
                    res.Otp = Common.EncryptString(NotEncryptOTP);
                    Otp = NotEncryptOTP;
                    log.Info("inserting OTP into verification called");
                    Int64 id = RepCRUD<AddVerification, GetVerification>.Insert(res, "verification");
                    log.Info("inserting OTP into verification ended");

                    if (id > 0)
                    {
                        if (!IsMerchant)
                        {
                            //SMSText = $"Thank you for registering with MyPay. Your activation code is {NotEncryptOTP}. Please enter this code to activate MyPay. Thank you for using MyPay.";
                            SMSText = $"Use code {NotEncryptOTP} to activate MyPay. Visit Scratch & Win in Menu section for 100% guaranteed cashback. For more info, contact: 16600162000. MyPay";

                            CheckOTPLimit(PhoneNumber, SMSText, ref ReturnString);
                            msg = ReturnString;
                        }
                        else
                        {
                            msg = "success";
                        }
                    }
                    Otp = res.Otp;
                }

            }
            return msg;
        }

        private static Int64 GetNewId()
        {
            Int64 Id = 0;
            MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
            string Result = commonHelpers.GetScalarValueWithValue("SELECT TOP 1 max(MemberId) FROM Users with(nolock)");
            if (!string.IsNullOrEmpty(Result) && Result != "0")
            {
                Id = Convert.ToInt64(Result) + 1;
            }
            else
            {
                Id = MyPay.Models.Common.Common.StartingNumber;
            }
            return Id;
        }

        public static bool CheckOTPLimit(string PhoneNumber, string SMSText, ref string msg)
        {
            bool resFlag = false;
            if (!string.IsNullOrEmpty(SMSText))
            {
                AddUserOTPAttempt objOTPAttemp = new AddUserOTPAttempt();
                objOTPAttemp.ContactNumber = PhoneNumber;
                objOTPAttemp.GetRecord();
                if (objOTPAttemp.Id == 0)
                {
                    objOTPAttemp.Add();
                }
                else if ((System.DateTime.UtcNow - objOTPAttemp.LastOTPSendDateTime).TotalHours > 24)
                {
                    objOTPAttemp.OTPSendCount = 0;
                    objOTPAttemp.UpdatedDate = System.DateTime.UtcNow;
                    objOTPAttemp.LastOTPSendDateTime = System.DateTime.UtcNow;
                    objOTPAttemp.Update();
                }
                //if (Common.SandboxContactsList.Contains(objOTPAttemp.ContactNumber) || objOTPAttemp.OTPSendCount < 5 || (System.DateTime.UtcNow - objOTPAttemp.LastOTPSendDateTime).TotalHours > 24)
                //{
                    if (Common.SandboxContactsList.Contains(objOTPAttemp.ContactNumber) || ((System.DateTime.UtcNow - objOTPAttemp.LastOTPSendDateTime).TotalSeconds > Convert.ToInt32(Common.ApplicationEnvironment.ResendOTPTime) || objOTPAttemp.OTPSendCount == 0))
                    {
                        objOTPAttemp.OTPSendCount = objOTPAttemp.OTPSendCount + 1;
                        objOTPAttemp.UpdatedDate = System.DateTime.UtcNow;
                        objOTPAttemp.LastOTPSendDateTime = System.DateTime.UtcNow;
                        objOTPAttemp.Update();
                        Models.Common.Common.SendSMS(PhoneNumber, SMSText);
                        resFlag = true;
                        msg = "success";
                    }
                    else
                    {
                        msg = Common.ReSendOTPMessage;
                    }
                //}
                //else
                //{
                //    msg = "You have exceed your daily limit to receive the OTP";
                //}
            }
            return resFlag;
        }


        public static AddUserLoginWithPin Login(ref string result, string authenticationToken, string PhoneNumber, string Password, bool IsMobile, string platform, string devicecode, string deviceId, string version_number)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


            AddUserLoginWithPin res = new AddUserLoginWithPin();
            try
            {
                string SMSText = string.Empty;
                if (string.IsNullOrEmpty(PhoneNumber))
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return res;
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    result = "Please enter password";
                    return res;
                }
                else if (string.IsNullOrEmpty(deviceId))
                {
                    result = "Please enter deviceId";
                    return res;
                }
                else if (PhoneNumber.Length < 10)
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return res;
                }
                bool isdigitpresent = PhoneNumber.Any(c => char.IsLetter(c));
                if (isdigitpresent)
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return res;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.ContactNumber = PhoneNumber;
                inobject.CheckDelete = 0;

                log.Info("AddUserLoginWithPin sp_Users_GetLoginWithPin called");
                res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                log.Info("AddUserLoginWithPin sp_Users_GetLoginWithPin called");


                if (res != null && res.Id != 0 && (res.RoleId == 2))
                {
                    if (!res.IsActive)
                    {
                        result = Common.InactiveUserMessage;
                        return res;
                    }
                    else if (!res.IsPhoneVerified)
                    {
                        result = "Phone number Not Verified";
                        return res;
                    }
                    else if (!Common.SandboxContactsList.Contains(PhoneNumber) && (res.LoginAttemptCount >= 5 && (DateTime.UtcNow - res.LastLoginAttempt).TotalMinutes <= 10))
                    {
                        Models.Common.Common.AddLogs("Too many failed attempts. Please try again in 10 minutes", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                        result = "Too many failed attempts. Please try again in 10 minutes";
                        return res;
                    }
                    else if (Common.DecryptString(res.Password) != Password)
                    {

                        res.LoginAttemptCount = res.LoginAttemptCount + 1;
                        res.LastLoginAttempt = System.DateTime.UtcNow;
                        res.LastInvalidPinUpdate(res.Id, res.LoginAttemptCount, res.LastLoginAttempt);
                        result = "Invalid Credentials";
                        return res;
                    }
                    else
                    {

                        string APIUser = Common.GetCreatedByName(authenticationToken);

                        if (Common.GetDeviceActiveStatus(res.MemberId, deviceId) == false || (APIUser.ToLower() == "web" && res.WebLoginAttempted == false))
                        {
                            AddVerification outobjectVerification = new AddVerification();
                            GetVerification inobjectVerification = new GetVerification();
                            inobjectVerification.PhoneNumber = PhoneNumber;
                            inobjectVerification.VerificationType = (int)AddVerification.VerifyType.Verification;
                            AddVerification resVerification = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobjectVerification, outobjectVerification);

                            string NotEncryptOTP = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                            if (Common.SandboxContactsList.Contains(PhoneNumber))
                            {
                                NotEncryptOTP = "123456";
                            }
                            if (!Common.ApplicationEnvironment.IsProduction)
                            {
                                NotEncryptOTP = "123456";
                            }

                            resVerification.Otp = Common.EncryptString(NotEncryptOTP);
                            string Otp = NotEncryptOTP;
                            bool statusver = false;
                            if (resVerification != null && resVerification.Id != 0)
                            {
                                resVerification.IsVerified = false;
                                statusver = RepCRUD<AddVerification, GetVerification>.Update(resVerification, "verification");
                            }
                            else
                            {
                                resVerification.PhoneNumber = PhoneNumber;
                                resVerification.VerificationType = (int)AddVerification.VerifyType.Verification;
                                Int64 ID = RepCRUD<AddVerification, GetVerification>.Insert(resVerification, "verification");
                                if (ID > 0)
                                {
                                    statusver = true;
                                }
                            }
                            if (statusver)
                            {
                                SMSText = "Your activation code is " + NotEncryptOTP + ". Thank you for using MyPay";
                                if (!string.IsNullOrEmpty(SMSText))
                                {
                                    string retMsg = string.Empty;
                                    if (CheckOTPLimit(PhoneNumber, SMSText, ref retMsg))
                                    {
                                        result = Common.LoginWithOTP;
                                    }
                                    else
                                    {
                                        result = retMsg;
                                    }
                                }
                                Models.Common.Common.AddLogs("Relogin User", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Login_user);
                            }
                            else
                            {
                                result = "Something Went Wrong Try Again Later";
                            }

                            return res;
                        }
                        else
                        {
                            res.PlatForm = platform;
                            res.DeviceCode = devicecode;
                            res.DeviceId = deviceId;
                            res.JwtToken = new CommonHelpers().GetJWToken(res.ContactNumber);
                            bool status = res.LoginUpdate(authenticationToken, version_number);
                            if (status)
                            {
                                Models.Common.Common.AddLogs("Login User", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Login_user);
                                // NEW DEVICE REGISTRATION HISTORY
                                //DeactiveDeviceFromMultipleLogins(authenticationToken, platform, devicecode, res);
                                res.LoginAttemptCount = 1;
                                res.LastLoginAttempt = System.DateTime.UtcNow;
                                res.LastInvalidPinUpdate(res.Id, res.LoginAttemptCount, res.LastLoginAttempt);
                                result = "Success";
                                return res;
                            }
                            else
                            {
                                result = "Something Went Wrong Try Again Later";
                                return res;
                            }
                        }
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Create pin  Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return res;
        }

        public static AddUserLoginWithPin LoginWithOTP(ref string result, string authenticationToken, string PhoneNumber, string OTP, bool IsMobile, string platform, string devicecode, string deviceId)
        {
            AddUserLoginWithPin res = new AddUserLoginWithPin();
            try
            {
                if (string.IsNullOrEmpty(PhoneNumber))
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return res;
                }
                else if (string.IsNullOrEmpty(deviceId))
                {
                    result = "Please enter deviceId";
                    return res;
                }
                else if (PhoneNumber.Length < 10)
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return res;
                }
                bool isdigitpresent = PhoneNumber.Any(c => char.IsLetter(c));
                if (isdigitpresent)
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return res;
                }
                else if (string.IsNullOrEmpty(OTP))
                {
                    result = "Please enter the verification OTP that you have received on your phone number";
                    return res;
                }
                else
                {
                    AddUserOTPAttempt objOTPAttemp = new AddUserOTPAttempt();
                    objOTPAttemp.ContactNumber = PhoneNumber;
                    objOTPAttemp.GetRecord();

                    string APIUser = Common.GetCreatedByName(authenticationToken);

                    if (Common.SandboxContactsList.Contains(PhoneNumber) || (objOTPAttemp.AttemptCount < 5 || (System.DateTime.UtcNow - objOTPAttemp.AttemptDateTime).TotalHours > 2))
                    {
                        AddVerification outobjectver = new AddVerification();
                        GetVerification inobjectver = new GetVerification();
                        inobjectver.PhoneNumber = PhoneNumber;
                        inobjectver.CheckVerified = 0;
                        inobjectver.Otp = Common.EncryptString(OTP);
                        inobjectver.VerificationType = (int)AddVerification.VerifyType.Verification;
                        AddVerification resver = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobjectver, outobjectver);
                        if (resver != null && resver.Id != 0)
                        {
                            resver.IsVerified = true;
                            bool statusver = RepCRUD<AddVerification, GetVerification>.Update(resver, "verification");
                            if (statusver)
                            {
                                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                                inobject.ContactNumber = PhoneNumber;
                                inobject.CheckDelete = 0;
                                //inobject.RoleId = 2;
                                res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                                if (res != null && res.Id != 0 && (res.RoleId == 2))
                                {
                                    if (!res.IsActive)
                                    {
                                        result = Common.InactiveUserMessage;
                                        return res;
                                    }
                                    else if (!res.IsPhoneVerified)
                                    {
                                        result = "Phone number Not Verified";
                                        return res;
                                    }
                                    else
                                    {
                                        res.PlatForm = platform;
                                        res.DeviceCode = devicecode;
                                        res.DeviceId = deviceId;
                                        res.JwtToken = new CommonHelpers().GetJWToken(res.ContactNumber);
                                        bool status = res.LoginUpdate(authenticationToken);
                                        if (status)
                                        {
                                            if (objOTPAttemp.Id == 0)
                                            {

                                                objOTPAttemp.MemberId = res.MemberId;
                                                objOTPAttemp.Add();
                                            }
                                            else
                                            {
                                                objOTPAttemp.AttemptCount = 0;
                                                objOTPAttemp.Update();
                                            }
                                            if (APIUser.ToLower() == "web" && res.WebLoginAttempted == false)
                                            {
                                                res.WebLoginUpdate(res.Id);
                                            }
                                            Common.AddLogs("Login User with OTP", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Login_user);
                                            result = "Success";
                                            return res;
                                        }
                                        else
                                        {
                                            result = "Something Went Wrong Try Again Later";
                                            return res;
                                        }
                                    }
                                }
                                else
                                {
                                    result = "This user does not exist";
                                }
                            }
                            else
                            {
                                result = "Something went wrong. Please try again later.";
                            }
                        }
                        else
                        {
                            bool InValidOTPUpdate = Common.InvalidOTPUpdate(ref objOTPAttemp, PhoneNumber);
                            result = "Invalid OTP";
                        }
                    }
                    else
                    {
                        result = Common.InvalidOTPMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Check OTP Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return res;
        }

        public static string LogoutDevice(AddUserLoginWithPin res, string authenticationToken, Int64 MemberId, string DeviceCode, bool IsMobile, string platform)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(DeviceCode))
                {
                    result = "Please enter DeviceCode";
                    return result;
                }
                else if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                if (res != null && res.Id != 0)
                {
                    if (res.DeviceCode == DeviceCode)
                    {
                        // *********************************************************************************************************// 
                        // ******************** IF LOGOUT FROM CURRENT DEVICE THEN CLEAR DEVICECODE ********************************// 
                        // *********************************************************************************************************// 
                        res.DeviceCode = String.Empty;
                    }

                    bool status = res.LogoutDevice(res.Id, res.DeviceCode);
                    if (status)
                    {
                        Models.Common.Common.AddLogs($"User Device Logged Out Successfully. (Ph: {res.ContactNumber} )", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, DeviceCode, (int)AddLog.LogActivityEnum.Logout_user);
                        return "Success";
                    }
                    else
                    {
                        return "Something Went Wrong Try Again Later";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }

            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("User Device Logged Out Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        private static void DeactiveDeviceFromMultipleLogins(string authenticationToken, string platform, string devicecode, AddUser res)
        {
            AddUsersDeviceRegistration outuserDeviceRegistration = new AddUsersDeviceRegistration();
            GetUsersDeviceRegistration inuserDeviceRegistration = new GetUsersDeviceRegistration();
            inuserDeviceRegistration.MemberId = Convert.ToInt64(res.MemberId);
            inuserDeviceRegistration.DeviceCode = devicecode;
            inuserDeviceRegistration.SequenceNo = 0;
            inuserDeviceRegistration.PlatForm = platform;
            inuserDeviceRegistration.IpAddress = Common.GetUserIP();
            inuserDeviceRegistration.CreatedBy = Common.GetCreatedById(authenticationToken);
            inuserDeviceRegistration.CreatedByName = Common.GetCreatedByName(authenticationToken);
            AddUsersDeviceRegistration resuserDeviceRegistration = RepCRUD<GetUsersDeviceRegistration, AddUsersDeviceRegistration>.GetRecord(nameof(Common.StoreProcedures.sp_UsersDeviceRegistration_Check), inuserDeviceRegistration, outuserDeviceRegistration);
            if (!string.IsNullOrEmpty(resuserDeviceRegistration.PreviousDeviceCode))
            {
                string Title = "Device limit reached";
                string Message = "Your device has been removed due to multiple logins on different devices.";
                Int32 VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Device_Inactivate;
                Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message, resuserDeviceRegistration.PreviousDeviceCode);

            }
        }
        public static AddUserLoginWithPin LoginWithPin(ref string result, string authenticationToken, Int64 MemberId, string Pin, bool IsMobile, string platform, string devicecode, string deviceId , string version_number ="" )
        {
            AddUserLoginWithPin res = new AddUserLoginWithPin();
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return res;
                }
                else if (string.IsNullOrEmpty(deviceId))
                {
                    result = "Please enter deviceId";
                    return res;
                }
                else if (string.IsNullOrEmpty(platform))
                {
                    result = "Please enter platform";
                    return res;
                }
                else if (string.IsNullOrEmpty(Pin))
                {
                    result = "Please enter pin";
                    return res;
                }
                else if (Pin.ToString().Length < 4)
                {
                    result = "Please enter 4 digit pin";
                    return res;
                }
                bool isdigitpresent = Pin.ToString().Any(c => char.IsLetter(c));
                if (isdigitpresent)
                {
                    result = "Please enter valid pin.";
                    return res;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.MemberId = MemberId;
                inobject.CheckDelete = 0;
                //inobject.RoleId = 2;
                res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);


                if (res != null && res.Id != 0 && (res.RoleId == 2))
                {
                    if (string.IsNullOrEmpty(res.DeviceId) || res.DeviceId != deviceId)
                    {
                        result = Common.Relogin;
                        return res;
                    }
                    else if (!res.IsActive)
                    {
                        result = Common.InactiveUserMessage;
                        return res;
                    }
                    else if (!res.IsPhoneVerified)
                    {
                        result = "Phone number Not Verified";
                        return res;
                    }
                    else if (string.IsNullOrEmpty(res.Pin))
                    {
                        result = "Pin Not created";
                        return res;
                    }
                    else if ((res.LoginAttemptCount >= 5 && (DateTime.UtcNow - res.LastLoginAttempt).TotalMinutes <= 10))
                    {
                        Models.Common.Common.AddLogs("Too many failed attempts. Please try again in 10 minutes", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                        result = "Too many failed attempts. Please try again in 10 minutes";
                        return res;
                    }
                    else if (Common.DecryptString(res.Pin) != Pin)
                    {
                        res.LoginAttemptCount = res.LoginAttemptCount + 1;
                        res.LastLoginAttempt = System.DateTime.UtcNow;
                        res.LastInvalidPinUpdate(res.Id, res.LoginAttemptCount, res.LastLoginAttempt);
                        result = Common.Invalidpin;
                        return res;
                    }
                    else
                    {
                        res.PlatForm = platform;
                        res.DeviceCode = devicecode;
                        res.JwtToken = new CommonHelpers().GetJWToken(res.ContactNumber);
                        bool status = res.LoginUpdate(authenticationToken, version_number);
                        if (status)
                        {
                            Models.Common.Common.AddLogs("Login User with pin", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Login_With_Pin);
                            // NEW DEVICE REGISTRATION HISTORY
                            //DeactiveDeviceFromMultipleLogins(authenticationToken, platform, devicecode, res);
                            res.LoginAttemptCount = 1;
                            res.LastLoginAttempt = System.DateTime.UtcNow;
                            res.LastInvalidPinUpdate(res.Id, res.LoginAttemptCount, res.LastLoginAttempt);
                            result = "Success";
                            return res;
                        }
                        else
                        {
                            result = "Something Went Wrong Try Again Later";
                            return res;
                        }
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Login User with pin  Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return res;
        }

        public static string GetShareReferLink(string authenticationToken, string MemberId, string RefCode, bool IsMobile, string platform, string devicecode, ref AddShareReferLink outobject_ref)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(MemberId) || MemberId.Trim() == "0")
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (string.IsNullOrEmpty(RefCode))
                {
                    result = "Please enter RefCode";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.RefCode = RefCode;
                inobject.MemberId = Convert.ToInt64(MemberId);
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                if (res != null && res.Id != 0)
                {
                    if (!res.IsActive)
                    {
                        return Common.InactiveUserMessage;
                    }
                    else
                    {
                        AddShareReferLink outobjectShareLnk = new AddShareReferLink();
                        GetShareReferLink inobjectShareLnk = new GetShareReferLink();
                        inobjectShareLnk.RefCode = RefCode;
                        inobjectShareLnk.CheckDelete = 0;
                        inobjectShareLnk.IPAddress = Common.GetUserIP();
                        inobjectShareLnk.CheckOpened = 0;
                        AddShareReferLink resShareReferLink = RepCRUD<GetShareReferLink, AddShareReferLink>.GetRecord("sp_ShareReferLink_Get", inobjectShareLnk, outobjectShareLnk);

                        if (resShareReferLink != null && resShareReferLink.Id != 0)
                        {
                            result = "success";
                            outobject_ref = resShareReferLink;
                        }
                        else
                        {
                            AddShareReferLink outobjectShareLnkNew = new AddShareReferLink();
                            outobjectShareLnkNew.Id = 0;
                            outobjectShareLnkNew.RefCode = RefCode;
                            outobjectShareLnkNew.IPAddress = Common.GetUserIP();
                            outobjectShareLnkNew.IsDeleted = false;
                            outobjectShareLnkNew.IsActive = true;
                            outobjectShareLnkNew.IsOpened = false;
                            outobjectShareLnkNew.MemberId = res.MemberId;
                            outobjectShareLnkNew.PhoneNumber = res.ContactNumber;
                            outobjectShareLnkNew.Platform = platform;
                            outobjectShareLnkNew.CreatedDate = System.DateTime.UtcNow;
                            outobjectShareLnkNew.UpdatedDate = System.DateTime.UtcNow;
                            outobjectShareLnkNew.CreatedBy = Common.GetCreatedById(Common.authenticationToken);
                            outobjectShareLnkNew.CreatedByName = Common.GetCreatedByName(Common.authenticationToken);
                            outobjectShareLnkNew.UpdatedBy = Common.GetCreatedById(Common.authenticationToken);
                            outobjectShareLnkNew.SharedLinkURL = Common.LiveSiteUrl + $"ReferShareLink?refcode={res.RefCode}&IPAddress={Common.GetUserIP()}&Platform={platform}";

                            Int64 Id = RepCRUD<AddShareReferLink, GetShareReferLink>.Insert(outobjectShareLnkNew, "sharereferlink");
                            if (Id > 0)
                            {
                                outobject_ref = outobjectShareLnkNew;
                            }
                            result = "success";
                        }
                    }

                }
                else
                {
                    result = "This user does not exist";
                }

            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("RefCode share link  Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string GetLinkedRefCode(string authenticationToken, bool IsMobile, string platform, string devicecode, ref AddShareReferLink outobject_ref)
        {
            string result = "";
            try
            {
                AddShareReferLink outobjectShareLnk = new AddShareReferLink();
                GetShareReferLink inobjectShareLnk = new GetShareReferLink();
                inobjectShareLnk.CheckDelete = 0;
                //inobjectShareLnk.CheckOpened = 0;
                inobjectShareLnk.IPAddress = Common.GetUserIP();
                AddShareReferLink resShareReferLink = RepCRUD<GetShareReferLink, AddShareReferLink>.GetRecord("sp_ShareReferLink_Get", inobjectShareLnk, outobjectShareLnk);
                if (resShareReferLink != null && resShareReferLink.Id > 0)
                {
                    outobject_ref = resShareReferLink;
                    result = "success";
                }
                else
                {
                    result = "Refer Code not found";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("RefCode share link  Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string VerifyEmail(AddUserLoginWithPin res, Int64 MemberId, string email, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            if (MemberId == 0)
            {
                result = "Please enter MemberId";
                return result;
            }
            else if (email == "")
            {
                result = "Please enter email";
                return result;
            }
            else if (!Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                result = "Please enter valid email";
                return result;
            }
            AddUserLoginWithPin outobjectemail = new AddUserLoginWithPin();
            GetUserLoginWithPin inobjectemail = new GetUserLoginWithPin();
            inobjectemail.Email = email;
            inobjectemail.CheckDelete = 0;
            AddUserLoginWithPin resemail = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectemail, outobjectemail);
            if (resemail != null && resemail.MemberId != 0)
            {
                result = "This email address is already associated with another account. Please enter a new email address.";
                return result;
            }
            if (res != null && res.MemberId != 0)
            {
                string NotEncryptOTP = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                AddVerification outobject = new AddVerification();
                GetVerification inobject = new GetVerification();
                inobject.PhoneNumber = res.ContactNumber;
                inobject.VerificationType = (int)AddVerification.VerifyType.EmailVerification;
                AddVerification resotp = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobject, outobject);
                if (resotp != null && resotp.Id != 0)
                {
                    if (resotp.IsVerified)
                    {
                        result = "Email already verified.";
                    }
                    else
                    {
                        resotp.Email = email;
                        resotp.VerificationType = (int)AddVerification.VerifyType.EmailVerification;
                        resotp.PhoneExtension = res.PhoneExtension;
                        resotp.PhoneNumber = res.ContactNumber;
                        resotp.IsActive = true;

                        resotp.Type = (int)AddVerification.PlatformType.Email;
                        if (Common.SandboxContactsList.Contains(res.ContactNumber))
                        {
                            NotEncryptOTP = "123456";
                        }
                        resotp.Otp = Common.EncryptString(NotEncryptOTP);
                        resotp.UpdatedDate = DateTime.UtcNow;
                        bool status = RepCRUD<AddVerification, GetVerification>.Update(resotp, "verification");
                        if (status)
                        {
                            string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/email-verify.html"));
                            string body = mystring;
                            if (!string.IsNullOrEmpty(res.FirstName))
                            {
                                body = body.Replace("##UserName##", res.FirstName);
                            }
                            else
                            {
                                body = body.Replace("##UserName##", res.Email);
                            }
                            body = body.Replace("##OTC##", NotEncryptOTP);

                            string Subject = MyPay.Models.Common.Common.WebsiteName + " -OTC (One Time Code) to verify email";
                            if (!string.IsNullOrEmpty(email))
                            {
                                Common.SendAsyncMail(email, Subject, body);
                            }
                            MemberId = res.MemberId;
                            VerificationOtp = string.Empty;// res.VerificationCode;
                            Models.Common.Common.AddLogs("OTP sent successfully to verify Email :(" + email + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);

                            result = "success";
                        }
                        else
                        {
                            Models.Common.Common.AddLogs("OTP not sent successfully to verify Email", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                            result = "Email Not Sent";
                        }
                    }

                }
                else
                {
                    outobject.Email = email;
                    outobject.VerificationType = (int)AddVerification.VerifyType.EmailVerification;
                    outobject.PhoneExtension = res.PhoneExtension;
                    outobject.PhoneNumber = res.ContactNumber;
                    outobject.IsActive = true;

                    outobject.Type = (int)AddVerification.PlatformType.Email;
                    if (Common.SandboxContactsList.Contains(res.ContactNumber))
                    {
                        NotEncryptOTP = "123456";
                    }
                    outobject.Otp = Common.EncryptString(NotEncryptOTP);
                    Int64 id = RepCRUD<AddVerification, GetVerification>.Insert(outobject, "verification");
                    if (id > 0)
                    {
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/email-verify.html"));
                        string body = mystring;
                        if (!string.IsNullOrEmpty(res.FirstName))
                        {
                            body = body.Replace("##UserName##", res.FirstName);
                        }
                        else
                        {
                            body = body.Replace("##UserName##", res.Email);
                        }
                        body = body.Replace("##OTC##", NotEncryptOTP);

                        string Subject = MyPay.Models.Common.Common.WebsiteName + " -OTC (One Time Code) to verify email";
                        if (!string.IsNullOrEmpty(email))
                        {
                            Common.SendAsyncMail(email, Subject, body);
                        }
                        MemberId = res.MemberId;
                        VerificationOtp = string.Empty;// res.VerificationCode;
                        Models.Common.Common.AddLogs("OTP sent successfully to verify Email :(" + email + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);

                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("OTP not sent successfully to verify Email", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                        result = "Email Not Sent";
                    }
                }
            }
            else
            {
                Models.Common.Common.AddLogs("Verify Email user not exist", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                result = "Sorry!! We could not find this user, Please Register";
            }
            return result;
        }
        public static string ConfirmEmail(AddUserLoginWithPin res, Int64 MemberId, string email, string otp, bool IsMobile, string platform, string devicecode, ref bool IsCouponUnlocked)
        {
            string result = "";
            if (MemberId == 0)
            {
                result = "Please enter MemberId";
                return result;
            }
            else if (email == "")
            {
                result = "Please enter email";
                return result;
            }
            else if (otp == "")
            {
                result = "Please enter the verification OTP that you have received on registered email address";
                return result;
            }
            else if (!Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                result = "Please enter valid email";
                return result;
            }
            AddUserLoginWithPin outobjectemail = new AddUserLoginWithPin();
            GetUserLoginWithPin inobjectemail = new GetUserLoginWithPin();
            inobjectemail.Email = email;
            inobjectemail.CheckDelete = 0;
            AddUserLoginWithPin resemail = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectemail, outobjectemail);
            if (resemail != null && resemail.Id != 0)
            {
                result = "This email address is already associated with another account. Please enter a new email address.";
                return result;
            }
            if (res != null && res.Id != 0)
            {
                AddVerification outobjectver = new AddVerification();
                GetVerification inobjectver = new GetVerification();
                inobjectver.PhoneNumber = res.ContactNumber;
                inobjectver.Otp = Common.EncryptString(otp);
                inobjectver.Email = email;
                inobjectver.VerificationType = (int)AddVerification.VerifyType.EmailVerification;
                AddVerification resver = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobjectver, outobjectver);
                if (resver != null && resver.Id != 0)
                {
                    if ((System.DateTime.UtcNow - resver.UpdatedDate).TotalSeconds > 60)
                    {
                        return "OTP you received is expired, please try again.";
                    }
                    else
                    {
                        resver.IsVerified = true;
                        bool statusver = RepCRUD<AddVerification, GetVerification>.Update(resver, "verification");
                        if (statusver)
                        {
                            res.IsEmailVerified = true;
                            res.Email = email;
                            bool status = res.ConfirmEmail(res.Id, res.Email, res.IsEmailVerified);
                            if (status)
                            {
                                string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/EmailVerified.html"));
                                string body = mystring;
                                if (!string.IsNullOrEmpty(res.FirstName))
                                {
                                    body = body.Replace("##UserName##", res.FirstName);
                                }
                                else
                                {
                                    body = body.Replace("##UserName##", res.Email);
                                }

                                string Subject = MyPay.Models.Common.Common.WebsiteName + " - Email Verification Successful";
                                if (!string.IsNullOrEmpty(email))
                                {
                                    Common.SendAsyncMail(email, Subject, body);
                                }
                                MemberId = res.MemberId;
                                Models.Common.Common.AddLogs("Email Verified", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                                // *********  Coupon is provided On Email Verify ********* //
                                IsCouponUnlocked = Common.AssignCoupons(res.MemberId, "", (int)AddCoupons.CouponReceivedBy.EmailVerify);
                                result = "success";
                            }
                            else
                            {
                                Models.Common.Common.AddLogs("Request for email verification not updated", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                                result = "Email Not Sent";
                            }

                        }
                        else
                        {
                            return "Something went wrong. Please try again later.";
                        }
                    }
                }
                else
                {
                    //bool InValidOTPUpdate = Common.InvalidOTPUpdate(ref objOTPAttemp, PhoneNumber);
                    result = "OTP you entered is wrong. Please try again. ";
                }
            }
            else
            {
                Models.Common.Common.AddLogs("Confirm email user not exist", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                result = "Sorry!! We could not find this user, Please Register";
            }
            return result;
        }
        public static string ForgetPasswordwithEmail(string email, bool IsMobile, string platform, string devicecode, ref AddUserLoginWithPin res)
        {
            string result = "";
            if (email == "")
            {
                result = "Please enter email";
                return result;
            }
            else if (!Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                result = "Please enter valid email";
                return result;
            }
            AddUserLoginWithPin outobject = new AddUserLoginWithPin();
            GetUserLoginWithPin inobject = new GetUserLoginWithPin();
            inobject.Email = email;
            inobject.CheckDelete = 0;
            res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                res.VerificationCode = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                bool status = res.ResetVerificationCode(res.Id, res.VerificationCode);
                if (status)
                {
                    string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/forgot-password.html"));
                    string body = mystring;
                    if (!string.IsNullOrEmpty(res.FirstName))
                    {
                        body = body.Replace("##UserName##", res.FirstName);
                    }
                    else
                    {
                        body = body.Replace("##UserName##", res.Email);
                    }
                    body = body.Replace("##OTC##", res.VerificationCode);

                    string Subject = MyPay.Models.Common.Common.WebsiteName + " -OTC (One Time Code) to reset your password";
                    if (!string.IsNullOrEmpty(res.Email))
                    {
                        Common.SendAsyncMail(res.Email, Subject, body);
                    }
                    //MemberId = res.MemberId;
                    ContactNumber = res.ContactNumber;
                    VerificationOtp = string.Empty;// res.VerificationCode;
                    Models.Common.Common.AddLogs("Request for password forgot", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                    result = "success";
                }
                else
                {
                    Models.Common.Common.AddLogs("Request for password forgot not updated", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                    result = "Email Not Sent";
                }
            }
            else
            {
                Models.Common.Common.AddLogs("Forget Password user not exist", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), email, IsMobile, platform, devicecode);
                result = "Sorry!! We could not find this user, Please Register";
            }
            return result;
        }

        public static string ForgetPasswordwithPhone(string phone, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            string SMSText = string.Empty;
            if (phone == "")
            {
                result = "Please enter a valid 10 digit phone number.";
                return result;
            }
            else if (phone.Length < 10)
            {
                result = "Please enter a valid 10 digit phone number.";
                return result;
            }
            bool isdigitpresent = phone.Any(c => char.IsLetter(c));
            if (isdigitpresent)
            {
                result = "Please enter a valid 10 digit phone number.";
                return result;
            }
            AddUserLoginWithPin outobject = new AddUserLoginWithPin();
            GetUserLoginWithPin inobject = new GetUserLoginWithPin();
            inobject.ContactNumber = phone;
            inobject.CheckDelete = 0;
            AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                res.VerificationCode = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                if (Common.SandboxContactsList.Contains(phone))
                {
                    res.VerificationCode = "123456";
                }

                bool status = res.ResetVerificationCode(res.Id, res.VerificationCode);
                if (status)
                {
                    VerificationOtp = string.Empty;// res.VerificationCode;
                    SMSText = $"Your verification code is {res.VerificationCode}. Please enter the code to verify your account. Thank you for using MyPay.";
                    Models.Common.Common.AddLogs("Request for password forgot", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                    result = "success";
                }
                else
                {
                    Models.Common.Common.AddLogs("Request for password forgot not updated", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                    result = "Message Not Sent";
                }
            }
            else
            {
                Models.Common.Common.AddLogs("Forget Password user not exist", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), phone, IsMobile, platform, devicecode);
                result = "Sorry!! We could not find this user, Please Register";
            }


            if (!string.IsNullOrEmpty(SMSText))
            {
                string retMsg = string.Empty;
                CheckOTPLimit(phone, SMSText, ref retMsg);
                result = retMsg;
            }
            return result;
        }

        public static string ResetPassword(ref AddUserLoginWithPin res, string Password, string ConfirmPassword, Int64 MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (Password == "")
                {
                    result = "Please enter password";
                    return result;
                }
                else if (Password.Length < 8)
                {
                    result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                    return result;
                }
                else if (Password != "")
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                        return result;
                    }
                }
                if (ConfirmPassword == "")
                {
                    result = "Please enter confirm password";
                    return result;
                }
                else if (ConfirmPassword != Password)
                {
                    result = "Password does not match please type again!";
                    return result;
                }
                if (res != null && res.Id != 0)
                {
                    if (Models.Common.Common.DecryptString(res.Password) == Password)
                    {
                        result = "New Password cannot be same as old password!";
                        return result;
                    }
                    string NewPassword = Models.Common.Common.EncryptString(Password);
                    res.Password = NewPassword;
                    //res.TransactionPassword = NewPassword;
                    bool status = res.ResetPassword(res.Id, NewPassword, res.IsEmailVerified);
                    if (status)
                    {
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/password-changed.html"));
                        string body = mystring;
                        if (!string.IsNullOrEmpty(res.FirstName))
                        {
                            body = body.Replace("##UserName##", res.FirstName);
                        }
                        else
                        {
                            body = body.Replace("##UserName##", res.Email);
                        }
                        string Subject = Models.Common.Common.WebsiteName + " -Your password has changed";

                        if (!string.IsNullOrEmpty(res.Email))
                        {
                            Common.SendAsyncMail(res.Email, Subject, body);
                        }
                        Models.Common.Common.AddLogs("Password reset successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Password_Reset);
                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Password reset failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                        result = "Password not reset right now.Please try again later";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Reset Password Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }


        public static string ResetPasswordMerchant(string Password, string ConfirmPassword, Int64 MemberId, bool IsMobile, string platform, string devicecode, bool IsMerchant = false, int MerchantType = 0)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (Password == "")
                {
                    result = "Please enter password";
                    return result;
                }
                else if (Password.Length < 8)
                {
                    result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                    return result;
                }
                else if (Password != "")
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                        return result;
                    }
                }
                if (ConfirmPassword == "")
                {
                    result = "Please enter confirm password";
                    return result;
                }
                else if (ConfirmPassword != Password)
                {
                    result = "Password does not match please type again!";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.MemberId = MemberId;
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                if (res != null && res.MemberId != 0)
                {
                    if (Models.Common.Common.DecryptString(res.Password) == Password)
                    {
                        result = "New Password cannot be same as old password!";
                        return result;
                    }
                    string NewPassword = Models.Common.Common.EncryptString(Password);
                    res.Password = NewPassword;
                    //res.TransactionPassword = NewPassword;
                    bool status = res.ResetPassword(res.Id, NewPassword, res.IsEmailVerified);
                    if (status)
                    {
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/password-changed.html"));
                        string body = mystring;
                        if (!string.IsNullOrEmpty(res.FirstName))
                        {
                            body = body.Replace("##UserName##", res.FirstName);
                        }
                        else
                        {
                            body = body.Replace("##UserName##", res.Email);
                        }
                        string Subject = Models.Common.Common.WebsiteName + " -Your password has changed";


                        Models.Common.Common.AddLogs("Password reset successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Password_Reset);
                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Password reset failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                        result = "Password not reset right now.Please try again later";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Reset Password Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string ResetPasswordWithOTC(string ContactNumber, string Password, string Code, Int64 MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(ContactNumber))
                {
                    result = "Please enter contact number";
                    return result;
                }
                else
                if (Password == "")
                {
                    result = "Please enter password";
                    return result;
                }
                else if (Password.Length < 8)
                {
                    result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                    return result;
                }
                else if (Password != "")
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                        return result;
                    }
                }
                else if (Code == "")
                {
                    result = "Please enter the verification OTP that you have received on registered email address / phone number";
                    return result;
                }

                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.ContactNumber = ContactNumber;
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                if (res != null && res.Id != 0)
                {
                    AddUserOTPAttempt objOTPAttemp = new AddUserOTPAttempt();
                    objOTPAttemp.ContactNumber = ContactNumber;
                    objOTPAttemp.GetRecord();

                    if (Common.SandboxContactsList.Contains(ContactNumber) || (objOTPAttemp.AttemptCount < 5 || (System.DateTime.UtcNow - objOTPAttemp.AttemptDateTime).TotalHours > 2))
                    {
                        if (res.VerificationCode == Code)
                        {
                            string NewPassword = Models.Common.Common.EncryptString(Password);
                            res.IsEmailVerified = true;
                            bool status = res.ResetPassword(res.Id, NewPassword, res.IsEmailVerified);
                            if (status)
                            {
                                string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/password-changed.html"));
                                string body = mystring;
                                if (!string.IsNullOrEmpty(res.FirstName))
                                {
                                    body = body.Replace("##UserName##", res.FirstName);
                                }
                                else
                                {
                                    body = body.Replace("##UserName##", res.Email);
                                }
                                string Subject = Models.Common.Common.WebsiteName + " -Your password has changed";

                                if (!string.IsNullOrEmpty(res.Email))
                                {
                                    Common.SendAsyncMail(res.Email, Subject, body);
                                }
                                if (objOTPAttemp.Id == 0)
                                {

                                    objOTPAttemp.MemberId = res.MemberId;
                                    objOTPAttemp.Add();
                                }
                                else
                                {
                                    objOTPAttemp.AttemptCount = 0;
                                    objOTPAttemp.Update();
                                }
                                Common.AddLogs("Password reset successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Password_Reset);
                                result = "success";
                            }
                            else
                            {
                                Models.Common.Common.AddLogs("Password reset failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                                result = "Password not reset right now.Please try again later";
                            }
                        }
                        else
                        {

                            bool InValidOTPUpdate = Common.InvalidOTPUpdate(ref objOTPAttemp, ContactNumber);
                            Common.AddLogs("Password reset invalid code", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                            result = "Invalid OTP";
                        }
                    }
                    else
                    {
                        return Common.InvalidOTPMessage;
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Reset Password Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string ResendOtp(string ContactNumber, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            string SMSText = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ContactNumber))
                {
                    result = "Please enter contact number";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.ContactNumber = ContactNumber;
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    res.VerificationCode = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();

                    res.PlatForm = platform;
                    res.DeviceCode = devicecode;
                    bool status = res.UpdatePlatform(res.Id, res.PlatForm, res.DeviceCode, res.VerificationCode);
                    if (status)
                    {
                        SMSText = $"Your verification code is {res.VerificationCode}. Please enter the code to verify your account. Thank you for using MyPay.";
                        Models.Common.Common.AddLogs("Otp Resend (MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Password_Reset);
                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Otp Resend failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                        result = "Otp Resend failed.Please try again later";
                    }
                    if (!string.IsNullOrEmpty(SMSText))
                    {
                        string retMsg = string.Empty;
                        CheckOTPLimit(ContactNumber, SMSText, ref retMsg);
                        result = retMsg;
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Resend Otp Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string AddUserBankDetail(string LogoUrl, string authenticationToken, AddUserLoginWithPin resGetRecord, string Name, string BankCode, string BankName, string Token, string Email, string AccountNumber, string platform, bool IsMobile, string devicecode)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    result = "Please enter name";
                    return result;
                }
                else if (string.IsNullOrEmpty(BankCode))
                {
                    result = "Please enter bank code";
                    return result;
                }
                else if (string.IsNullOrEmpty(BankName))
                {
                    result = "Please enter bank name";
                    return result;
                }

                else if (string.IsNullOrEmpty(AccountNumber))
                {
                    result = "Please enter bank account number";
                    return result;
                }
                else
                {

                    AddUserBankDetail checkaccountoutobject = new AddUserBankDetail();
                    GetUserBankDetail checkaccountinobject = new GetUserBankDetail();
                    checkaccountinobject.MemberId = resGetRecord.MemberId;
                    checkaccountinobject.AccountNumber = AccountNumber;
                    checkaccountinobject.CheckActive = 1;
                    checkaccountinobject.CheckDelete = 0;
                    AddUserBankDetail checkaccountres = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, checkaccountinobject, checkaccountoutobject);
                    if (checkaccountres.Id > 0)
                    {
                        result = "Account has been already added";
                        return result;
                    }
                    AddUserBankDetail outobject = new AddUserBankDetail();
                    GetUserBankDetail inobject = new GetUserBankDetail();
                    inobject.MemberId = resGetRecord.MemberId;
                    AddUserBankDetail res = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobject, outobject);

                    if (res != null && res.Id != 0)
                    {
                        AddUserBankDetail outobject_primary = new AddUserBankDetail();
                        GetUserBankDetail inobject_primary = new GetUserBankDetail();
                        inobject_primary.MemberId = resGetRecord.MemberId;
                        inobject_primary.CheckPrimary = 1;
                        AddUserBankDetail res_primary = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobject_primary, outobject_primary);
                        if (res_primary != null && res_primary.Id != 0)
                        {
                            res_primary.IsPrimary = false;
                            res_primary.CreatedBy = Common.GetCreatedById(authenticationToken);
                            res_primary.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            bool status_primary = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Update(res_primary, "userbankdetail");
                        }
                    }

                    AddUserBankDetail resBankDtl = new AddUserBankDetail();
                    resBankDtl.MemberId = resGetRecord.MemberId;
                    resBankDtl.Name = Name;
                    resBankDtl.AccountNumber = AccountNumber;
                    resBankDtl.BankCode = BankCode;
                    resBankDtl.BankName = BankName;
                    resBankDtl.BranchId = "1";
                    resBankDtl.BranchName = Token;
                    resBankDtl.CreatedBy = Common.GetCreatedById(authenticationToken);
                    resBankDtl.CreatedByName = Common.GetCreatedByName(authenticationToken);
                    resBankDtl.IsActive = true;
                    resBankDtl.IsPrimary = true;
                    resBankDtl.ICON_NAME = LogoUrl;
                    Int64 id = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Insert(resBankDtl, "userbankdetail");
                    if (id > 0)
                    {
                        resGetRecord.IsBankAdded = 1;
                        bool IsBankAdded = resGetRecord.UpdateIsBankAdded(resGetRecord.Id, resGetRecord.IsBankAdded);
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/BankAccountLinked.html"));
                        string body = mystring;
                        if (!string.IsNullOrEmpty(Name))
                        {
                            body = body.Replace("##UserName##", Name);
                        }
                        else
                        {
                            body = body.Replace("##UserName##", String.Empty);
                        }
                        body = body.Replace("##AccountHolderName##", Name);
                        body = body.Replace("##AccountNumber##", AccountNumber);
                        body = body.Replace("##BankName##", BankName);

                        string Subject = MyPay.Models.Common.Common.WebsiteName + " - Bank Account Linked";
                        if (!string.IsNullOrEmpty(Email))
                        {
                            Common.SendAsyncMail(Email, Subject, body);
                        }
                        Models.Common.Common.AddLogs("User bank detail added successfully", false, Convert.ToInt32(AddLog.LogType.User), resGetRecord.MemberId, Name, IsMobile, platform, devicecode);
                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("User bank detail added failed", false, Convert.ToInt32(AddLog.LogType.User), resGetRecord.MemberId, Name, IsMobile, platform, devicecode);
                        result = "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("User bank detail Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string MakePrimaryUserBank(string authenticationToken, Int64 MemberId, string BankCode, string platform, bool IsMobile, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter member id";
                    return result;
                }
                else if (string.IsNullOrEmpty(BankCode))
                {
                    result = "Please enter bank code";
                    return result;
                }
                AddUserBankDetail outobject = new AddUserBankDetail();
                GetUserBankDetail inobject = new GetUserBankDetail();
                inobject.MemberId = MemberId;
                inobject.BankCode = BankCode;
                AddUserBankDetail res = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddUserBankDetail outobject_primary = new AddUserBankDetail();
                    GetUserBankDetail inobject_primary = new GetUserBankDetail();
                    inobject_primary.MemberId = MemberId;
                    inobject_primary.CheckPrimary = 1;
                    AddUserBankDetail res_primary = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobject_primary, outobject_primary);
                    if (res_primary != null && res_primary.Id != 0)
                    {
                        //res_primary.IsPrimary = false;
                        //res_primary.UpdatedBy = Common.GetCreatedById(authenticationToken);
                        //res_primary.UpdatedByName = Common.GetCreatedByName(authenticationToken);
                        //bool status_primary = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Update(res_primary, "userbankdetail");
                        bool status_primary = (new CommonHelpers()).UserBankPrimaryUpdate(MemberId, Common.GetCreatedById(authenticationToken), Common.GetCreatedByName(authenticationToken));
                        if (status_primary == false)
                        {
                            Models.Common.Common.AddLogs("Make Primary Bank Failed", false, Convert.ToInt32(AddLog.LogType.User), MemberId, "", IsMobile, platform, devicecode);
                            result = "Make Primary Bank Failed. Please Try Again Later.";
                            return result;
                        }
                    }
                    res.IsPrimary = true;
                    bool status = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Update(res, "userbankdetail");
                    if (status)
                    {
                        Models.Common.Common.AddLogs("Make primary User bank detail successfully", false, Convert.ToInt32(AddLog.LogType.User), MemberId, "", IsMobile, platform, devicecode);
                        result = "success";
                    }
                }
                else
                {
                    result = "No record found";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Make primary User bank detail Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string UserPersonalUpdate(ref AddUser res, string authenticationToken, Int64 MemberId, string Email, string AlternateNumber, string RefCode, string firstname, string middlename, string lastname, string dateofbirth, string gender, string platform, bool IsMobile, string devicecode, string UserInput, string DeviceId, ref bool IsCouponUnlocked, bool IsMerchant = false, int MerchantType = 0)

        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(firstname))
                {
                    result = "Please enter first name";
                    return result;
                }
                else if (MemberId == 0)
                {
                    result = "Please enter member id";
                    return result;
                }
                else if (string.IsNullOrEmpty(lastname))
                {
                    result = "Please enter last name";
                    return result;
                }

                firstname = firstname.Trim(new Char[] { ' ' });
                lastname = lastname.Trim(new Char[] { ' ' });
                if (result != "")
                {
                    return result;
                }
                if (!string.IsNullOrEmpty(Email))
                {
                    AddUserLoginWithPin outobjectemail = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobjectemail = new GetUserLoginWithPin();
                    inobjectemail.Email = Email;
                    inobjectemail.CheckDelete = 0;
                    AddUserLoginWithPin resemail = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectemail, outobjectemail);
                    if (resemail != null && resemail.Id != 0)
                    {
                        result = "This email address is already associated with another account. Please enter a new email address.";
                        return result;
                    }
                }

                if (res != null && res.MemberId != 0)
                {
                    res.FirstName = firstname.Replace("<", "").Replace(">", "");
                    res.LastName = lastname.Replace("<", "").Replace(">", "");
                    res.MiddleName = middlename.Replace("<", "").Replace(">", "");
                    if (!string.IsNullOrEmpty(dateofbirth))
                    {
                        res.DateofBirth = dateofbirth;
                    }

                    bool IsSameDeviceID = false;
                    res.Gender = Convert.ToInt32(string.IsNullOrEmpty(gender) ? "0" : gender);
                    res.Email = Email;
                    res.AlternateContactNumber = AlternateNumber;
                    AddUserLoginWithPin refres_Parent = new AddUserLoginWithPin();
                    if (!string.IsNullOrEmpty(RefCode))
                    {
                        AddUserLoginWithPin outrefobject = new AddUserLoginWithPin();
                        GetUserLoginWithPin inrefobject = new GetUserLoginWithPin();
                        inrefobject.RefCode = RefCode;
                        refres_Parent = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inrefobject, outrefobject);
                        if (refres_Parent.Id > 0)
                        {
                            if ((res.RefId == 0) && (res.RefCode != RefCode)) // IF never refered before && User cannot Not referred to self
                            {
                                if ((new CommonHelpers()).IsSameDeviceID_Registration(DeviceId))
                                {
                                    IsSameDeviceID = true;
                                }

                                if (IsSameDeviceID == false)
                                {
                                    res.RefId = refres_Parent.MemberId;
                                }
                            }
                        }
                        else
                        {
                            return "RefCode is Invalid. Please try again.";
                        }
                    }
                    res.RefCodeAttempted = RefCode;
                    res.DeviceCode = devicecode;
                    res.UpdatedDate = DateTime.UtcNow;
                    res.IpAddress = Common.GetUserIP();
                    res.CreatedBy = Common.GetCreatedById(authenticationToken);
                    res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                    bool status = RepCRUD<AddUser, GetUser>.Update(res, "user");
                    if (status)
                    {
                        AddShareReferLink outobjectShareLnk = new AddShareReferLink();
                        GetShareReferLink inobjectShareLnk = new GetShareReferLink();
                        inobjectShareLnk.CheckDelete = 0;
                        inobjectShareLnk.RefCode = RefCode;
                        inobjectShareLnk.IPAddress = Common.GetUserIP();
                        AddShareReferLink resShareReferLink = RepCRUD<GetShareReferLink, AddShareReferLink>.GetRecord("sp_ShareReferLink_Get", inobjectShareLnk, outobjectShareLnk);
                        if (resShareReferLink != null && resShareReferLink.Id > 0)
                        {
                            resShareReferLink.IsDeleted = true;
                            RepCRUD<AddShareReferLink, GetShareReferLink>.Update(resShareReferLink, "sharereferlink");
                        }
                        AddKYCStatusHistory res_kyc = new AddKYCStatusHistory();
                        res_kyc.MemberId = res.MemberId;
                        res_kyc.UpdatedBy = res.MemberId;
                        res_kyc.CreatedBy = Common.GetCreatedById(authenticationToken);
                        res_kyc.CreatedByName = Common.GetCreatedByName(authenticationToken);
                        res_kyc.KYCStatus = res.IsKYCApproved;
                        res_kyc.IsActive = true;
                        res_kyc.IsApprovedByAdmin = true;
                        res_kyc.Remarks = "User Personal Profile Updated.";
                        Int64 Id = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Insert(res_kyc, "kycstatushistory");
                        Models.Common.Common.AddLogs($"Updated Personal profile of {res.FirstName} {res.LastName} (Ph: {res.ContactNumber} )", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(MemberId), res.FirstName, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.User_Profile_Update);

                        // *********  Coupon is provided only on Register and KYC ********* //
                        IsCouponUnlocked = Common.AssignCoupons(res.MemberId, "", (int)AddCoupons.CouponReceivedBy.SignUp);

                        // ********* REGISTRATION COMMISION DISTRIBUTION ******* //
                        //if (!string.IsNullOrEmpty(RefCode) && res.RefId == refres_Parent.MemberId)
                        {
                            bool IsCommissionDistributed = Common.DistributeRegistrationCommisionPoints(platform, devicecode, true, res.MemberId, refres_Parent, authenticationToken, UserInput, ref RewardPoint, (int)AddSettings.CommissionType.RegistrationCommission, "", IsSameDeviceID);
                        }
                        if (res.RoleId == (int)AddUser.UserRoles.User || (res.RoleId == (int)AddUser.UserRoles.Merchant && MerchantType == (int)AddMerchant.MerChantType.Merchant))
                        {
                            #region SendEmailConfirmation
                            string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/RegisterMail.html"));
                            string body = mystring;
                            string Subject = MyPay.Models.Common.Common.WebsiteName + " - Welcome to MyPay";
                            if (!string.IsNullOrEmpty(res.Email))
                            {
                                Common.SendAsyncMail(res.Email, Subject, body);
                            }
                            #endregion
                        }
                        result = "Success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Not Updated Personal profile ", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(MemberId), res.FirstName, IsMobile, platform, devicecode);
                        result = "Not Update";
                    }
                }
                else
                {
                    Models.Common.Common.AddLogs("User Profile Id not Exist", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(MemberId), res.FirstName, IsMobile, platform, devicecode);
                    result = "User Profile Id not Exist";
                }

            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("User Profile Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string SetPin(ref AddUserLoginWithPin res, string authenticationToken, string pin, Int64 MemberId, string otp, string IsForget, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(pin))
                {
                    result = "Please enter pin";
                    return result;
                }
                else if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (IsForget.ToLower() == "yes" && string.IsNullOrEmpty(otp))
                {
                    result = "Please enter the OTP that you have received on registered email address /phone number";
                    return result;
                }
                else if (Common.allCharactersSame(pin) || Common.IsSerialNumberOrder(pin))
                {
                    result = "Repeating digits(1111) or Sequence digits(1234) are not secure. Please enter a strong pin. ";
                    return result;
                }
                AddUserLoginWithPin outRes = new AddUserLoginWithPin();

                if (res != null && res.Id != 0 && (res.RoleId == 2))
                {
                    if (res.IsActive)
                    {
                        if (IsForget.ToLower() == "yes" && otp != res.VerificationCode)
                        {
                            result = "Invalid otp";
                            return result;
                        }
                        res.Pin = Common.EncryptString(pin);
                        bool status = outRes.SetPin(res.Id, res.Pin);
                        if (status)
                        {
                            result = "success";
                            Models.Common.Common.AddLogs("Created pin successfully", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Created_Pin);
                        }
                        else
                        {
                            result = "Something went wrong try again later";
                        }
                    }
                    else
                    {
                        result = "User Id Is Not Active";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Create pin  Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }
        public static string ForgetPin(string email, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (email == "")
                {
                    result = "Please enter email";
                    return result;
                }
                else if (!Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    result = "Please enter valid email";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.Email = email;
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                if (res != null && res.Id != 0)
                {
                    res.VerificationCode = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                    bool status = res.ResetVerificationCode(res.Id, res.VerificationCode);
                    if (status)
                    {
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/forget-pin.html"));
                        string body = mystring;
                        if (!string.IsNullOrEmpty(res.FirstName))
                        {
                            body = body.Replace("##UserName##", res.FirstName);
                        }
                        else
                        {
                            body = body.Replace("##UserName##", res.Email);
                        }
                        body = body.Replace("##OTC##", res.VerificationCode);
                        string Subject = Models.Common.Common.WebsiteName + " -OTC (One Time Code) to reset your PIN";
                        if (!string.IsNullOrEmpty(res.Email))
                        {
                            Common.SendAsyncMail(res.Email, Subject, body);
                        }
                        Models.Common.Common.AddLogs("Request for pin reset", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);

                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Request for pin reset not updated", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                        result = "Email Not Sent";
                    }
                }
                else
                {
                    Models.Common.Common.AddLogs("Forget pin user not exist", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode);
                    result = "Sorry!! We could not find this user, Please Register";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Forget Pin Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile, platform, devicecode);
                result = ex.Message;
            }
            return result;
        }


        public static string CompleteUserProfile(ref bool IsCouponUnlocked, ref AddUser res, string StepCompleted, string MotherName, string SpouseName, string authenticationToken, Int64 IssueFromDistrictID, string IssueFromDistrictName, Int64 IssueFromStateID, string IssueFromStateName, Int64 MemberId, string firstname, string middlename, string lastname, string dateofbirth, int gender, int meritalstatus, string fathername, string grandfathername, string occupation, int nationality, int stateid, string state, int districtid, string district, int municipalityid, string municipality, string wardnumber, string streetname, string housenumber, int prooftype, bool ispermanantaddresssame, int currentstateid, string currentstate, int currentdistrictid, string currentdistrict, int currentmunicipalityid, string currentmunicipality, string currentwardnumber, string currentstreetname, string currenthousenumber, string issuedby, int dobtype, string issuedate, string expirydate, string documentnumber, string platform, bool IsMobile, string devicecode)
        {
            string result = "";
            try
            {
                firstname = firstname.Replace("<", "").Replace(">", "");
                lastname = lastname.Replace("<", "").Replace(">", "");
                MotherName = MotherName.Replace("<", "").Replace(">", "");
                fathername = fathername.Replace("<", "").Replace(">", "");
                SpouseName = SpouseName.Replace("<", "").Replace(">", "");
                grandfathername = grandfathername.Replace("<", "").Replace(">", "");

                if (string.IsNullOrEmpty(StepCompleted) || StepCompleted == "0")
                {
                    result = "Please enter step";
                    return result;
                }
                else if (string.IsNullOrEmpty(firstname) && StepCompleted == "1")
                {
                    result = "Please enter first name";
                    return result;
                }
                else if (MemberId == null || MemberId == 0)
                {
                    result = "Please enter member id";
                    return result;
                }
                else if (gender == null || gender == 0 && StepCompleted == "1")
                {
                    result = "Please select gender";
                    return result;
                }
                else if (string.IsNullOrEmpty(lastname) && StepCompleted == "1")
                {
                    result = "Please enter last name";
                    return result;
                }
                else if (meritalstatus == 0 && StepCompleted == "1")
                {
                    result = "Please select merital status";
                    return result;
                }
                else if (string.IsNullOrEmpty(fathername) && StepCompleted == "1")
                {
                    result = "Please enter father name";
                    return result;
                }
                else if (string.IsNullOrEmpty(grandfathername) && StepCompleted == "1")
                {
                    result = "Please enter grandfather name";
                    return result;
                }
                else if (string.IsNullOrEmpty(MotherName) && StepCompleted == "1")
                {
                    result = "Please enter mother name";
                    return result;
                }
                else if (string.IsNullOrEmpty(SpouseName) && meritalstatus == (int)AddUser.meritalstatus.Married && StepCompleted == "1")
                {
                    result = "Please enter spouse name";
                    return result;
                }
                else if (string.IsNullOrEmpty(occupation) || occupation == "0" && StepCompleted == "1")
                {
                    result = "Please select occupation";
                    return result;
                }
                else if (nationality == 0 && StepCompleted == "1")
                {
                    result = "Please select nationality";
                    return result;
                }
                else if (prooftype == 0 && StepCompleted == "3")
                {
                    result = "Please select prooftype";
                    return result;
                }
                else if (ispermanantaddresssame == false && StepCompleted == "2")
                {
                    if (currentstateid == 0)
                    {
                        result = "Please select current state";
                        return result;
                    }
                    else if (currentdistrictid == 0)
                    {
                        result = "Please select current district";
                        return result;
                    }
                    else if (currentmunicipalityid == 0)
                    {
                        result = "Please select current municipality";
                        return result;
                    }
                    else if (string.IsNullOrEmpty(currentwardnumber))
                    {
                        result = "Please enter current wardnumber";
                        return result;
                    }
                    else if (string.IsNullOrEmpty(currentdistrict))
                    {
                        result = "Please enter current district";
                        return result;
                    }
                    else if (string.IsNullOrEmpty(currentmunicipality))
                    {
                        result = "Please enter current municipality";
                        return result;
                    }
                    else if (string.IsNullOrEmpty(currentstate))
                    {
                        result = "Please enter current state";
                        return result;
                    }
                }
                if (result != "")
                {
                    return result;
                }

                if (currenthousenumber == null)
                {
                    currenthousenumber = "";

                }
                if (housenumber == null)
                {
                    housenumber = "";

                }

                if (res != null && res.Id != 0)
                {
                    if (StepCompleted == "1")
                    {
                        res.FirstName = string.IsNullOrEmpty(firstname) ? string.Empty : (firstname.First().ToString().ToUpper() + firstname.Substring(1));
                        res.LastName = string.IsNullOrEmpty(lastname) ? string.Empty : (lastname.First().ToString().ToUpper() + lastname.Substring(1));
                        res.MiddleName = string.IsNullOrEmpty(middlename) ? string.Empty : (middlename.First().ToString().ToUpper() + middlename.Substring(1));
                        res.DateofBirth = dateofbirth;
                        res.DOBType = dobtype;
                        res.MotherName = MotherName;
                        res.SpouseName = SpouseName;
                        res.Nationality = nationality;
                        res.MeritalStatus = meritalstatus;
                        res.FatherName = fathername;
                        res.GrandFatherName = grandfathername;
                        res.Gender = Convert.ToInt32(gender);
                        res.EmployeeType = Convert.ToInt32(occupation);
                    }
                    if (StepCompleted == "2")
                    {
                        res.CurrentStateId = currentstateid;
                        res.CurrentState = currentstate;
                        res.CurrentDistrictId = currentdistrictid;
                        res.CurrentDistrict = currentdistrict;
                        res.CurrentMunicipalityId = currentmunicipalityid;
                        res.CurrentMunicipality = currentmunicipality;
                        res.CurrentStreetName = currentstreetname;
                        res.CurrentWardNumber = currentwardnumber;
                        if (!string.IsNullOrEmpty(currenthousenumber))
                        {
                            res.CurrentHouseNumber = currenthousenumber;
                        }
                        res.CurrentHouseNumber = currenthousenumber;
                    }
                    if (StepCompleted == "3")
                    {
                        res.ProofType = prooftype;
                        res.IsKYCApproved = (int)AddUser.kyc.Pending;
                    }
                    res.UpdatedDate = DateTime.UtcNow;
                    res.IpAddress = Common.GetUserIP();
                    res.KYCCreatedDate = DateTime.UtcNow;
                    res.DeviceCode = devicecode;
                    res.UpdatedBy = res.MemberId;
                    res.UpdatedByName = res.FirstName + " " + res.LastName;
                    res.CreatedBy = Common.GetCreatedById(authenticationToken);
                    res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                    bool status = RepCRUD<AddUser, GetUser>.Update(res, "user");
                    if (status)
                    {

                        // *********  Coupon is provided only on Register and KYC ********* //
                        //IsCouponUnlocked = Common.AssignCoupons(res.MemberId, "", (int)AddCoupons.CouponReceivedBy.KYC);

                        AddKYCStatusHistory res_kyc = new AddKYCStatusHistory();
                        res_kyc.MemberId = res.MemberId;
                        res_kyc.UpdatedBy = res.MemberId;
                        res_kyc.CreatedBy = Common.GetCreatedById(authenticationToken);
                        res_kyc.CreatedByName = Common.GetCreatedByName(authenticationToken);
                        res_kyc.KYCStatus = res.IsKYCApproved;
                        res_kyc.IsActive = true;
                        res_kyc.IsApprovedByAdmin = true;
                        res_kyc.Remarks = "Update profile for kyc review";
                        Int64 Id = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Insert(res_kyc, "kycstatushistory");
                        Models.Common.Common.AddLogs("Complete user profile (MemberId:" + MemberId + " )", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(MemberId), res.FirstName, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Complete_User_Profile);
                        result = "Success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Not complete user profile (MemberId:" + MemberId + " ) ", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(MemberId), res.FirstName, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Complete_User_Profile);
                        result = "Not Update";
                    }
                }
                else
                {
                    Models.Common.Common.AddLogs("User Profile Id not Exist", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(MemberId), res.FirstName, IsMobile, platform, devicecode);
                    result = "User Profile Id not Exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("User Profile completion Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string DisableAllDevice(Int64 MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                if (string.IsNullOrEmpty(platform))
                {
                    result = "Please enter platform";
                    return result;
                }
                if (string.IsNullOrEmpty(devicecode))
                {
                    result = "Please enter devicecode";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.MemberId = MemberId;
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                if (res != null && res.Id != 0)
                {
                    int dbresult = 0;
                    using (var db = new MyPayEntities())
                    {
                        var UsersDeviceRegistrationList = db.UsersDeviceRegistrations.Where(f => f.MemberId == res.MemberId).ToList();
                        UsersDeviceRegistrationList.ForEach(a =>
                        {
                            a.SequenceNo = 0;
                            a.IsActive = false;
                            a.IsDeleted = true;
                            a.DisabledFromDeviceCode = devicecode;
                        });
                        dbresult = db.SaveChanges();
                    }
                    Models.Common.Common.AddLogs("Disabled all device successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                    result = "success";
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Disable all device error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string AddFeedback(string TransactionUniqueId, string Subject, string Message, string MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (string.IsNullOrEmpty(Subject) && Subject == "0")
                {
                    result = "Please Enter Rating";
                    return result;
                }
                else if (string.IsNullOrEmpty(Message))
                {
                    result = "Please enter UserMessage";
                    return result;
                }
                else if (!string.IsNullOrEmpty(MemberId))
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(MemberId, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid MemberId.";
                        return result;
                    }
                }

                AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                inobjectUser.MemberId = Convert.ToInt64(MemberId);
                AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);

                if (resUser == null || resUser.Id == 0)
                {

                    result = "Invalid MemberId";
                    return result;
                }
                else
                {

                    {
                        AddFeedback resInsert = new AddFeedback();
                        resInsert.Id = 0;
                        resInsert.TransactionUniqueId = TransactionUniqueId;
                        resInsert.Subject = Subject;
                        resInsert.UserMessage = Message;
                        resInsert.MemberId = Convert.ToInt64(MemberId);
                        Int64 id = RepCRUD<AddFeedback, GetFeedback>.Insert(resInsert, "feedback");
                        if (id > 0)
                        {

                            Models.Common.Common.AddLogs("Feedback submitted successfully : (" + MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(MemberId), resUser.FirstName + " " + resUser.LastName, IsMobile, platform, devicecode);
                            result = "success";
                        }
                        else
                        {
                            result = "Something went wrong try again later";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public static string GetCompleteUserProfile(Int64 memberid, string platform, string devicecode, bool ismobile)
        {
            try
            {
                if (memberid == 0)
                {
                    return "Please enter memberid.";
                }
                else
                {
                    AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                    inobject.MemberId = memberid;
                    AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        return "success";
                    }
                    else
                    {
                        return "User not found with this memberid.";
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return e.Message;
                throw;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string ChangePassword(string authenticationToken, string OldPassword, string Password, string ConfirmPassword, Int64 MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (OldPassword == "")
                {
                    result = "Please enter OldPassword";
                    return result;
                }
                else if (Password == "")
                {
                    result = "Please enter password";
                    return result;
                }
                else if (Password.Length < 8)
                {
                    result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                    return result;
                }
                else if (Password != "")
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        result = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";
                        return result;
                    }
                }
                if (ConfirmPassword == "")
                {
                    result = "Please enter confirm password";
                    return result;
                }
                else if (ConfirmPassword != Password)
                {
                    result = "Password does not match please type again!";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.MemberId = MemberId;
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                if (res != null && res.Id != 0)
                {
                    if (Models.Common.Common.DecryptString(res.Password) != OldPassword)
                    {
                        result = "Old Password is incorrect!";
                        return result;
                    }
                    if (Models.Common.Common.DecryptString(res.Password) == Password)
                    {
                        result = "New Password cannot be same as old password!";
                        return result;
                    }
                    res.Password = Models.Common.Common.EncryptString(Password);
                    res.IsResetPasswordFromAdmin = false;
                    bool status = res.ChangePassword(res.Id, res.Password, res.IsResetPasswordFromAdmin);
                    if (status)
                    {
                        res.PlatForm = platform;
                        res.DeviceCode = devicecode;
                        res.DeviceId = "";
                        res.JwtToken = new CommonHelpers().GetJWToken(res.ContactNumber);
                        bool UpdateFlag = res.LoginUpdateWebClient(authenticationToken);

                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/password-changed.html"));
                        string body = mystring;
                        if (!string.IsNullOrEmpty(res.FirstName))
                        {
                            body = body.Replace("##UserName##", res.FirstName);
                        }
                        else
                        {
                            body = body.Replace("##UserName##", res.Email);
                        }
                        string Subject = Models.Common.Common.WebsiteName + " -Your password has changed";

                        if (!string.IsNullOrEmpty(res.Email))
                        {
                            Common.SendAsyncMail(res.Email, Subject, body);
                        }
                        Models.Common.Common.AddLogs("Password reset successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Password_Reset);

                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Password reset failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                        result = "Password not reset right now.Please try again later";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Reset Password Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string ChangePin(AddUserLoginWithPin res, string Password, string Pin, string ConfirmPin, Int64 MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (Password == "")
                {
                    result = "Please enter Password";
                    return result;
                }
                else if (Pin == "")
                {
                    result = "Please enter Pin";
                    return result;
                }
                else if (Pin.Length < 4)
                {
                    result = "Pin must be 4 characters long";
                    return result;
                }
                if (ConfirmPin == "")
                {
                    result = "Please enter confirm pin";
                    return result;
                }
                else if (ConfirmPin != Pin)
                {
                    result = "Pin does not match please type again!";
                    return result;
                }
                else if (Common.allCharactersSame(Pin) || Common.IsSerialNumberOrder(Pin))
                {
                    result = "Repeating digits(1111) or Sequence digits(1234) are not secure. Please enter a strong pin. ";
                    return result;
                }
                AddUserLoginWithPin resObj = new AddUserLoginWithPin();
                if (res != null && res.Id != 0)
                {
                    if (Models.Common.Common.DecryptString(res.Password) != Password)
                    {
                        result = " Password is incorrect!";
                        return result;
                    }
                    if (Common.DecryptString(res.Pin) == Pin)
                    {
                        result = "New MPin cannot be same as old MPin!";
                        return result;
                    }
                    res.Pin = Common.EncryptString(Pin);
                    bool status = resObj.SetPin(res.Id, res.Pin);
                    if (status)
                    {

                        Models.Common.Common.AddLogs("Pin reset successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);

                        #region SendEmailConfirmation
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/MPinChanged.html"));
                        string body = mystring;
                        if (!string.IsNullOrEmpty(res.FirstName))
                        {
                            body = body.Replace("##UserName##", res.FirstName);
                        }
                        else
                        {
                            body = body.Replace("##UserName##", res.Email);
                        }
                        string Subject = MyPay.Models.Common.Common.WebsiteName + " - MPin Changed";
                        if (!string.IsNullOrEmpty(res.Email))
                        {
                            Common.SendAsyncMail(res.Email, Subject, body);
                        }
                        #endregion

                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("Password reset failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                        result = "Pin not reset right now.Please try again later";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Reset Pin Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string ChangePinSendOTP(ref AddUserLoginWithPin res, ref bool status, string Password, string Pin, string ConfirmPin, Int64 MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            string SMSText = string.Empty;
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (Password == "")
                {
                    result = "Please enter Password";
                    return result;
                }
                else if (Pin == "")
                {
                    result = "Please enter Pin";
                    return result;
                }
                else if (Pin.Length < 4)
                {
                    result = "Pin must be 4 characters long";
                    return result;
                }
                if (ConfirmPin == "")
                {
                    result = "Please enter confirm pin";
                    return result;
                }
                else if (ConfirmPin != Pin)
                {
                    result = "Pin does not match please type again!";
                    return result;
                }
                else if (Common.allCharactersSame(Pin) || Common.IsSerialNumberOrder(Pin))
                {
                    result = "Repeating digits(1111) or Sequence digits(1234) are not secure. Please enter a strong pin. ";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.MemberId = MemberId;
                res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (Models.Common.Common.DecryptString(res.Password) != Password)
                    {
                        result = " Password is incorrect!";
                        return result;
                    }
                    if (Common.DecryptString(res.Pin) == Pin)
                    {
                        result = "New MPin cannot be same as old MPin!";
                        return result;
                    }

                    string PhoneNumber = res.ContactNumber;
                    AddVerification outobjectVerification = new AddVerification();
                    GetVerification inobjectVerification = new GetVerification();
                    inobjectVerification.PhoneNumber = PhoneNumber;
                    inobjectVerification.VerificationType = (int)AddVerification.VerifyType.ForgotPin;
                    AddVerification resVerification = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobjectVerification, outobjectVerification);

                    string NotEncryptOTP = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                    if (Common.SandboxContactsList.Contains(PhoneNumber))
                    {
                        NotEncryptOTP = "123456";
                    }
                    resVerification.Otp = Common.EncryptString(NotEncryptOTP);
                    string Otp = NotEncryptOTP;
                    bool statusver = false;
                    if (resVerification != null && resVerification.Id != 0)
                    {
                        resVerification.IsVerified = false;
                        statusver = RepCRUD<AddVerification, GetVerification>.Update(resVerification, "verification");
                    }
                    else
                    {
                        resVerification.PhoneNumber = PhoneNumber;
                        resVerification.VerificationType = (int)AddVerification.VerifyType.ForgotPin;
                        Int64 ID = RepCRUD<AddVerification, GetVerification>.Insert(resVerification, "verification");
                        if (ID > 0)
                        {
                            statusver = true;
                        }
                    }
                    if (statusver)
                    {
                        status = true;

                        SMSText = "Your Pin Reset Code is " + NotEncryptOTP + ". Thank you for using MyPay";
                        if (!string.IsNullOrEmpty(SMSText))
                        {
                            string retMsg = string.Empty;
                            if (CheckOTPLimit(PhoneNumber, SMSText, ref retMsg))
                            {
                                result = Common.LoginWithOTP;
                            }
                            else
                            {
                                result = retMsg;
                            }
                        }
                        Models.Common.Common.AddLogs($"Forgot Pin Reset Code Sent For MemberId {res.MemberId}", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, IsMobile, platform, devicecode, (int)AddLog.LogActivityEnum.Login_user);
                    }
                    else
                    {
                        result = "Something Went Wrong Try Again Later";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Reset Pin Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }
        public static string ChangePinWithOTP(ref AddUserLoginWithPin res, string Phonenumber, string Password, string Pin, string ConfirmPin, string OTP, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(Phonenumber))
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return result;
                }
                else if (Phonenumber.Length < 10)
                {
                    result = "Please enter a valid 10 digit phone number.";
                    return result;
                }
                else if (OTP == "")
                {
                    result = "Please enter OTP";
                    return result;
                }
                else if (Password == "")
                {
                    result = "Please enter Password";
                    return result;
                }
                else if (Pin == "")
                {
                    result = "Please enter Pin";
                    return result;
                }
                else if (Pin.Length < 4)
                {
                    result = "Pin must be 4 characters long";
                    return result;
                }
                if (ConfirmPin == "")
                {
                    result = "Please enter confirm pin";
                    return result;
                }
                else if (ConfirmPin != Pin)
                {
                    result = "Pin does not match please type again!";
                    return result;
                }
                else if (Common.allCharactersSame(Pin) || Common.IsSerialNumberOrder(Pin))
                {
                    result = "Repeating digits(1111) or Sequence digits(1234) are not secure. Please enter a strong pin. ";
                    return result;
                }
                AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                inobjectUser.CheckDelete = 0;
                inobjectUser.ContactNumber = Phonenumber;
                res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                if (res != null && res.Id != 0 && res.IsActive)
                {
                    AddUserOTPAttempt objOTPAttemp = new AddUserOTPAttempt();
                    objOTPAttemp.ContactNumber = ContactNumber;
                    objOTPAttemp.GetRecord();

                    if (Common.SandboxContactsList.Contains(ContactNumber) || (objOTPAttemp.AttemptCount < 5 || (System.DateTime.UtcNow - objOTPAttemp.AttemptDateTime).TotalHours > 2))
                    {
                        if (Models.Common.Common.DecryptString(res.Password) != Password)
                        {
                            result = " Password is incorrect!";
                            return result;
                        }
                        if (Common.DecryptString(res.Pin) == Pin)
                        {
                            result = "New MPin cannot be same as old MPin!";
                            return result;
                        }
                        string PhoneNumber = res.ContactNumber;
                        AddVerification outobjectver = new AddVerification();
                        GetVerification inobjectver = new GetVerification();
                        inobjectver.PhoneNumber = PhoneNumber;
                        inobjectver.CheckVerified = 0;
                        inobjectver.Otp = Common.EncryptString(OTP);
                        inobjectver.VerificationType = (int)AddVerification.VerifyType.ForgotPin;
                        AddVerification resver = RepCRUD<GetVerification, AddVerification>.GetRecord("sp_verification_Get", inobjectver, outobjectver);
                        if (resver != null && resver.Id != 0)
                        {
                            resver.IsVerified = true;
                            bool statusver = RepCRUD<AddVerification, GetVerification>.Update(resver, "verification");
                            if (statusver)
                            {
                                res.Pin = Common.EncryptString(Pin);
                                bool status = res.SetPin(res.Id, res.Pin);
                                if (status)
                                {
                                    if (objOTPAttemp.Id == 0)
                                    {
                                        objOTPAttemp.MemberId = res.MemberId;
                                        objOTPAttemp.Add();
                                    }
                                    else
                                    {
                                        objOTPAttemp.AttemptCount = 0;
                                        objOTPAttemp.Update();
                                    }
                                    Common.AddLogs("Pin reset successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);

                                    #region SendEmailConfirmation
                                    string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/MPinChanged.html"));
                                    string body = mystring;
                                    if (!string.IsNullOrEmpty(res.FirstName))
                                    {
                                        body = body.Replace("##UserName##", res.FirstName);
                                    }
                                    else
                                    {
                                        body = body.Replace("##UserName##", res.Email);
                                    }
                                    string Subject = MyPay.Models.Common.Common.WebsiteName + " - MPin Changed";
                                    if (!string.IsNullOrEmpty(res.Email))
                                    {
                                        Common.SendAsyncMail(res.Email, Subject, body);
                                    }
                                    #endregion
                                    if (objOTPAttemp.Id == 0)
                                    {

                                        objOTPAttemp.MemberId = res.MemberId;
                                        objOTPAttemp.Add();
                                    }
                                    else
                                    {
                                        objOTPAttemp.AttemptCount = 0;
                                        objOTPAttemp.Update();
                                    }
                                    result = "success";
                                }
                                else
                                {
                                    Models.Common.Common.AddLogs("Password reset failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                                    result = "Pin not reset right now.Please try again later";
                                }

                            }
                            else
                            {
                                result = "Something went wrong please try again";
                            }
                        }
                        else
                        {
                            bool InValidOTPUpdate = Common.InvalidOTPUpdate(ref objOTPAttemp, PhoneNumber);
                            result = "Invalid OTP";
                        }
                    }
                    else
                    {
                        result = Common.InvalidOTPMessage;
                    }
                }
                else
                {
                    result = "Invalid User";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Reset Pin Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string ChangePushNotificationStatus(AddUserLoginWithPin res, string EnablePushNotification, Int64 MemberId, bool IsMobile, string platform, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId";
                    return result;
                }
                else if (EnablePushNotification == "")
                {
                    result = "Please enter EnablePushNotification";
                    return result;
                }
                AddUserLoginWithPin objres = new AddUserLoginWithPin();
                if (res != null && res.Id != 0)
                {
                    bool status = objres.EnablePushNotification(res.Id, bool.Parse(EnablePushNotification));
                    if (status)
                    {
                        Models.Common.Common.AddLogs("PushNotification Status changed successfully(MemberId:" + res.MemberId + ")", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                        result = "success";
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("PushNotification Status changed failed", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", IsMobile, platform, devicecode);
                        result = "PushNotification Status not changed right now.Please try again later";
                    }
                }
                else
                {
                    result = "This user does not exist";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("PushNotification Status changed Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string RedeemPoints(AddUserLoginWithPin res, string authenticationToken, Int64 MemberId, Int64 Id, string platform, bool IsMobile, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter member id";
                    return result;
                }
                else if (Id == 0)
                {
                    result = "Please enter id";
                    return result;
                }
                else if (res.IsActive == false)
                {
                    result = "User Inactive";
                    return result;
                }
                Guid referenceGuid = Guid.NewGuid();
                string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                string ReferenceNo = referenceGuid.ToString();
                if (res != null && res.Id != 0)
                {
                    AddRedeemPoints outobject_primary = new AddRedeemPoints();
                    GetRedeemPoints inobject_primary = new GetRedeemPoints();
                    inobject_primary.Id = Id;
                    inobject_primary.CheckActive = 1;
                    AddRedeemPoints res_primary = RepCRUD<GetRedeemPoints, AddRedeemPoints>.GetRecord(Common.StoreProcedures.sp_RedeemPoints_Get, inobject_primary, outobject_primary);
                    if (res_primary != null && res_primary.Id != 0)
                    {
                        if (res.TotalRewardPoints >= res_primary.Points)
                        {
                            RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                            res_rewardpoint.MemberId = res.MemberId;
                            res_rewardpoint.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            res_rewardpoint.Amount = Convert.ToDecimal(res_primary.Points);
                            res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                            res_rewardpoint.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                            res_rewardpoint.CurrentBalance = Convert.ToDecimal(res_primary.Points);
                            res_rewardpoint.CreatedBy = Common.GetCreatedById(authenticationToken);
                            res_rewardpoint.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                            res_rewardpoint.Remarks = "Redeem Reward Points";
                            res_rewardpoint.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Reedem_Points;
                            res_rewardpoint.Description = "Redeem Reward Points";
                            res_rewardpoint.Status = (int)WalletTransactions.Statuses.Success;
                            res_rewardpoint.Reference = ReferenceNo;
                            res_rewardpoint.IsApprovedByAdmin = true;
                            res_rewardpoint.IsActive = true;
                            res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Debit);
                            bool RewardPointUpdate = false;
                            RewardPointUpdate = res_rewardpoint.Add();
                            if (RewardPointUpdate)
                            {
                                WalletTransactions res_transaction = new WalletTransactions();
                                res_transaction.MemberId = Convert.ToInt64(res.MemberId);
                                res_transaction.ContactNumber = res.ContactNumber;
                                res_transaction.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                res_transaction.Amount = Convert.ToDecimal(res_primary.Amount);
                                res_transaction.UpdateBy = Convert.ToInt64(res.MemberId);
                                res_transaction.UpdateByName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                res_transaction.CurrentBalance = Convert.ToDecimal(res_primary.Amount) + res.TotalAmount;
                                res_transaction.CreatedBy = Common.GetCreatedById(authenticationToken);
                                res_transaction.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                res_transaction.TransactionUniqueId = TransactionUniqueID;
                                res_transaction.Remarks = "Redeem Reward Points";
                                res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Reedem_Points;
                                res_transaction.Description = "Redeem Reward Points";
                                res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                res_transaction.Reference = ReferenceNo;
                                res_transaction.IsApprovedByAdmin = true;
                                res_transaction.IsActive = true;
                                res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                res_transaction.NetAmount = res_transaction.Amount + res_transaction.ServiceCharge;
                                res_transaction.RewardPoint = res_primary.Points;
                                res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                if (res_transaction.Add())
                                {
                                    Common.AddLogs("Redeem Reward Points (MemberId: " + res.MemberId + ") from Redeem Points Id: " + res_primary.Id + ")", false, Convert.ToInt32(AddLog.LogType.User), MemberId, "", IsMobile, platform, devicecode);

                                }
                                result = "success";
                            }
                            else
                            {
                                result = "Reward Points Not Redeemed successfully";
                            }
                        }
                        else
                        {
                            result = "Insufficient balance";
                        }

                    }
                    else
                    {
                        result = "No active redeem points found";
                    }

                }
                else
                {
                    result = Common.CommonMessage.MemberId_Not_Found;
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Redeem Points Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static string GetApiRewardPoints(AddUserLoginWithPin resGetRecord, string DateFrom, string DateTo, string MemberId, string Take, string Skip, ref List<AddRewardPointTransactions> list, ref string WalletBalance, ref string TotalRewardsPoints)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Take))
            {
                Take = "0";
            }
            if (string.IsNullOrEmpty(Skip))
            {
                Skip = "0";
            }
            if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
            {
                msg = "Please enter MemberId.";
            }
            else if (!string.IsNullOrEmpty(MemberId))
            {
                decimal Num;
                bool isNum = decimal.TryParse(MemberId, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid MemberId.";
                }
            }
            if (string.IsNullOrEmpty(msg))
            {
                if (resGetRecord.Id == 0)
                {
                    msg = Common.CommonMessage.MemberId_Not_Found;
                }
                else
                {
                    WalletBalance = resGetRecord.TotalAmount.ToString("0.00");
                    TotalRewardsPoints = resGetRecord.TotalRewardPoints.ToString();
                    AddRewardPointTransactions outobjecttrans = new AddRewardPointTransactions();
                    GetRewardPointTransactions inobjectTrans = new GetRewardPointTransactions();
                    inobjectTrans.Take = Convert.ToInt32(Take);
                    inobjectTrans.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
                    inobjectTrans.MemberId = Convert.ToInt64(MemberId);
                    inobjectTrans.StartDate = DateFrom;
                    inobjectTrans.EndDate = DateTo;
                    list = RepCRUD<GetRewardPointTransactions, AddRewardPointTransactions>.GetRecordList(Common.StoreProcedures.sp_RewardPointTransactions_Get, inobjectTrans, outobjecttrans);
                    if (list.Count > 0)
                    {
                        msg = Common.CommonMessage.success;
                    }
                    else
                    {
                        list.Clear();
                        msg = Common.CommonMessage.Data_Not_Found;
                    }
                }
            }
            return msg;
        }

        public static string RemoveUserBankAccount(string authenticationToken, Int64 MemberId, string BankId, string platform, bool IsMobile, string devicecode)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter member id";
                    return result;
                }
                else if (string.IsNullOrEmpty(BankId))
                {
                    result = "Please enter bank id";
                    return result;
                }
                AddUserBankDetail outobject = new AddUserBankDetail();
                GetUserBankDetail inobject = new GetUserBankDetail();
                inobject.MemberId = MemberId;
                inobject.Id = Convert.ToInt64(BankId);
                AddUserBankDetail res = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    res.IsDeleted = true;
                    GetTokenUnLink restoken = new GetTokenUnLink();
                    GetTokenUnLinkNCHL restokenNCHL = new GetTokenUnLinkNCHL();
                    if (res.BankTransferType == 2)
                    {
                        restoken = RepNps.UnlinkAccount(res.BranchName);
                    }
                    else
                    {
                        restokenNCHL = RepNCHL.UnlinkAccount(res.BranchName, res.TransactionId, MemberId);
                    }
                    if (restoken.ReponseCode == 1 || restokenNCHL.ReponseCode == 1)
                    {
                        bool status = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Update(res, "userbankdetail");
                        if (status)
                        {
                            if (res.IsPrimary)
                            {
                                AddUserBankDetail outobjectPrimary = new AddUserBankDetail();
                                GetUserBankDetail inobjectPrimary = new GetUserBankDetail();
                                inobjectPrimary.MemberId = MemberId;
                                inobjectPrimary.CheckPrimary = 0;
                                inobjectPrimary.CheckDelete = 0;
                                inobjectPrimary.CheckActive = 1;
                                AddUserBankDetail resPrimary = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobjectPrimary, outobjectPrimary);
                                if (resPrimary != null && resPrimary.Id != 0)
                                {
                                    resPrimary.IsPrimary = true;

                                    status = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Update(resPrimary, "userbankdetail");
                                }
                            }
                            Models.Common.Common.AddLogs("User bank detail removed successfully", false, Convert.ToInt32(AddLog.LogType.User), MemberId, "", IsMobile, platform, devicecode);
                            result = "success";
                        }
                    }
                    else
                    {
                        Models.Common.Common.AddLogs("User bank detail not removed successfully (" + restoken.message + " " + restokenNCHL.responseMessage + ")", false, Convert.ToInt32(AddLog.LogType.User), MemberId, "", IsMobile, platform, devicecode);
                        result = restoken.Details + " " + restokenNCHL.Details;
                    }
                }
                else
                {
                    result = Common.CommonMessage.Data_Not_Found;
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("User bank detail removed Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.User), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }
        public static string RefCodeUpdateFromAdmin(string NewRefCode, string Remarks, string AdminMemberId, string AdminMemberName, ref AddUser resGetRecord)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(NewRefCode))
                {
                    result = "Please enter NewRefCode.";
                    return result;
                }
                else if (NewRefCode.Length < 8)
                {
                    result = "Please enter 8 digits NewRefCode.";
                    return result;
                }
                else if (string.IsNullOrEmpty(Remarks))
                {
                    result = "Please enter Remarks.";
                    return result;
                }
                else if (Common.HasSpecialChars(NewRefCode))
                {
                    result = "Refcode Invalid.";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.RefCode = NewRefCode;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    result = "Refcode already exists.";
                    return result;
                }
                if (string.IsNullOrEmpty(result))
                {
                    string OldRefCode = resGetRecord.RefCode;
                    resGetRecord.RefCode = NewRefCode;
                    bool IsUpdated = RepCRUD<AddUser, GetUser>.Update(resGetRecord, "user");
                    if (IsUpdated)
                    {
                        Common.AddLogs($"Updated User RefCode from '{OldRefCode}' to '{NewRefCode}' for MemberId: {resGetRecord.MemberId} Changed by  AdminId: {AdminMemberId} ({AdminMemberName})", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(resGetRecord.MemberId), resGetRecord.FirstName);
                        AddKYCStatusHistory res_newkyc = new AddKYCStatusHistory();
                        res_newkyc.MemberId = Convert.ToInt64(resGetRecord.MemberId);
                        res_newkyc.Remarks = $"Updated User RefCode from '{OldRefCode}' to '{NewRefCode}' with Admin Remarks as : '{Remarks}'";
                        res_newkyc.IsAdmin = true;
                        res_newkyc.IpAddress = Common.GetUserIP();
                        res_newkyc.KYCStatus = Convert.ToInt32(resGetRecord.IsKYCApproved);
                        res_newkyc.CreatedBy = Convert.ToInt64(AdminMemberId);
                        res_newkyc.CreatedByName = AdminMemberName;
                        res_newkyc.IsActive = true;
                        res_newkyc.IsApprovedByAdmin = true;
                        Int64 id = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Insert(res_newkyc, "kycstatushistory");
                        if (id > 0)
                        {
                            result = "success";
                            Common.AddLogs($"Added User RefCode Change History. RefCode changed from '{OldRefCode}' to '{NewRefCode}' for MemberId: {resGetRecord.MemberId} Changed by  AdminId: {AdminMemberId} ({AdminMemberName})", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(resGetRecord.MemberId), resGetRecord.FirstName);
                        }
                    }
                    else
                    {
                        result = "Not Updated";
                        Common.AddLogs($"Failed to Update User RefCode from '{OldRefCode}' to '{NewRefCode}' for MemberId: {resGetRecord.MemberId} Changed by  AdminId: {AdminMemberId} ({AdminMemberName})", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(resGetRecord.MemberId), resGetRecord.FirstName);
                    }
                }
                return result;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                result = e.Message;
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
        }

        public static AddUser UpdateKYCDetails(AddUser res, string Remarks, string IsKYCApproved, string AdminMemberId, string AdminUserName)
        {
            if (res.MemberId.ToString() == "0")
            {
                res.Message = "Please enter Member id";
            }
            if ((IsKYCApproved == ((int)AddUser.kyc.Rejected).ToString() || IsKYCApproved == ((int)AddUser.kyc.Risk_High).ToString() || IsKYCApproved == ((int)AddUser.kyc.Proof_Rejected).ToString()) && (string.IsNullOrEmpty(Remarks) || Remarks == "0"))
            {
                res.Message = "Please select Remarks";
            }
            else
            {

                if (res != null && res.MemberId != 0)
                {
                    AddKYCRemarks outobject_remarks = new AddKYCRemarks();
                    GetKYCRemarks inobject_remarks = new GetKYCRemarks();
                    inobject_remarks.Id = Convert.ToInt32(Remarks);
                    AddKYCRemarks remarks = RepCRUD<GetKYCRemarks, AddKYCRemarks>.GetRecord(Common.StoreProcedures.sp_KYCRemarks_Get, inobject_remarks, outobject_remarks);
                    if (IsKYCApproved == ((int)AddUser.kyc.Verified).ToString())
                    {
                        remarks.Description = "Congratulations " + res.FirstName + "! " + " Your KYC Approved successfully.";
                    }
                    else if ((IsKYCApproved == ((int)AddUser.kyc.Rejected).ToString()) || (IsKYCApproved == ((int)AddUser.kyc.Proof_Rejected).ToString()) || (IsKYCApproved == ((int)AddUser.kyc.Risk_High).ToString()))
                    {

                    }
                    else
                    {
                        remarks.Description = string.Empty;
                    }
                    res.Remarks = remarks.Description;
                    res.IsKYCApproved = Convert.ToInt32(IsKYCApproved);
                    res.UpdatedDate = DateTime.UtcNow;
                    res.UpdatedBy = Convert.ToInt64(AdminMemberId);
                    res.ApprovedorRejectedBy = Convert.ToInt64(AdminMemberId);
                    res.ApprovedorRejectedByName = AdminUserName;
                    res.KYCReviewDate = DateTime.UtcNow;
                    AddUserLoginWithPin objRes = new AddUserLoginWithPin();
                    bool status = objRes.UpdateKycDetails(res.Id, res.Remarks, res.IsKYCApproved, res.UpdatedBy, res.ApprovedorRejectedBy, res.ApprovedorRejectedByName, res.KYCReviewDate);
                    if (status)
                    {
                        if (res.IsKYCApproved == (int)AddUser.kyc.Verified)
                        {
                            string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/KYCApproved.html"));
                            string body = mystring;
                            if (!string.IsNullOrEmpty(res.FirstName))
                            {
                                body = body.Replace("##UserName##", res.FirstName);
                            }
                            else
                            {
                                body = body.Replace("##UserName##", res.Email);
                            }
                            string Subject = Models.Common.Common.WebsiteName + " -KYC Approved";

                            if (!string.IsNullOrEmpty(res.Email))
                            {
                                Common.SendAsyncMail(res.Email, Subject, body);
                            }
                            string Title = remarks.Title;
                            string Message = "Congratulations " + res.FirstName + "! " + " Your KYC Approved successfully.";

                            Models.Common.Common.CreatedBy = Convert.ToInt64(AdminMemberId);
                            Models.Common.Common.CreatedByName = AdminUserName;
                            Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.KYC, res.MemberId, Title, Message);

                            bool IsCouponUnlocked = Common.AssignCoupons(res.MemberId, "", (int)AddCoupons.CouponReceivedBy.KYC);

                        }
                        if ((res.IsKYCApproved == (int)AddUser.kyc.Rejected) || (res.IsKYCApproved == (int)AddUser.kyc.Proof_Rejected) || (res.IsKYCApproved == (int)AddUser.kyc.Risk_High))
                        {
                            string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/KYCRejected.html"));
                            string body = mystring;
                            if (!string.IsNullOrEmpty(res.FirstName))
                            {
                                body = body.Replace("##UserName##", res.FirstName);
                            }
                            else
                            {
                                body = body.Replace("##UserName##", res.Email);
                            }
                            body = body.Replace("##Remarks##", res.Remarks);
                            string Subject = Models.Common.Common.WebsiteName + " -KYC Rejected";

                            if (!string.IsNullOrEmpty(res.Email))
                            {
                                Common.SendAsyncMail(res.Email, Subject, body);
                            }

                            string Title = remarks.Title;
                            string Message = "Hello " + res.FirstName + "! " + remarks.Description;

                            Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.KYC, res.MemberId, Title, Message);
                        }
                        AddKycHistory(res.MemberId.ToString(), IsKYCApproved, AdminMemberId, AdminUserName, res, remarks);
                        res.Message = "success";
                        Common.AddLogs("Updated User KYC status (MemberId:" + res.MemberId.ToString() + ") by(AdminId: " + AdminMemberId + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(res.MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                    }
                    else
                    {
                        res.Message = "Not Updated.";
                    }
                }
            }
            return res;
        }

        public static AddUser UpdateKYCDetailsAdminEdit(AddUser res, string Remarks, string IsKYCApproved, string AdminMemberId, string AdminUserName)
        {

            if (res != null && res.Id != 0)
            {
                AddKYCStatusHistory outobject_kyc = new AddKYCStatusHistory();
                GetKYCStatusHistory inobject_kyc = new GetKYCStatusHistory();
                inobject_kyc.MemberId = Convert.ToInt64(res.MemberId);
                inobject_kyc.CheckActive = 1;
                AddKYCStatusHistory res_kyc = RepCRUD<GetKYCStatusHistory, AddKYCStatusHistory>.GetRecord(Models.Common.Common.StoreProcedures.sp_KYCStatusHistory_Get, inobject_kyc, outobject_kyc);
                if (res_kyc != null && res_kyc.Id > 0)
                {
                    res_kyc.IsActive = false;
                    bool status_kyc = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Update(res_kyc, "kycstatushistory");
                    Common.AddLogs($"Updated User KYC status history (MemberId: {res.MemberId}) by  (AdminId: {AdminMemberId})", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(res.MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                }
                AddKYCStatusHistory res_newkyc = new AddKYCStatusHistory();
                res_newkyc.MemberId = Convert.ToInt64(res.MemberId);
                res_newkyc.Remarks = Remarks;
                res_newkyc.IsAdmin = true;
                res_newkyc.IpAddress = Common.GetUserIP();
                res_newkyc.KYCStatus = Convert.ToInt32(IsKYCApproved);
                res_newkyc.CreatedBy = Convert.ToInt64(AdminMemberId);
                res_newkyc.CreatedByName = AdminUserName;
                res_newkyc.IsActive = true;
                res_newkyc.IsApprovedByAdmin = true;
                Int64 id = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Insert(res_newkyc, "kycstatushistory");
                if (id > 0)
                {
                    Common.AddLogs("Add User KYC status history by(MemberId:" + res.MemberId + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(res.MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                }
                else
                {
                    Common.AddLogs("Not Added User KYC status history by(MemberId:" + res.MemberId.ToString() + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(res.MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                }


                res.Message = "success";

            }
            else
            {
                res.Message = "MemberId Not Found";
            }

            return res;
        }

        private static void AddKycHistory(string MemberId, string IsKYCApproved, string AdminMemberId, string AdminUserName, AddUser res, AddKYCRemarks remarks)
        {
            AddKYCStatusHistory outobject_kyc = new AddKYCStatusHistory();
            GetKYCStatusHistory inobject_kyc = new GetKYCStatusHistory();
            inobject_kyc.MemberId = Convert.ToInt64(MemberId);
            inobject_kyc.CheckActive = 1;
            AddKYCStatusHistory res_kyc = RepCRUD<GetKYCStatusHistory, AddKYCStatusHistory>.GetRecord(Models.Common.Common.StoreProcedures.sp_KYCStatusHistory_Get, inobject_kyc, outobject_kyc);
            if (res_kyc != null && res_kyc.Id != 0)
            {
                res_kyc.IsActive = false;
                bool status_kyc = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Update(res_kyc, "kycstatushistory");
                if (status_kyc)
                {
                    Common.AddLogs($"Updated User KYC status history (MemberId: {MemberId}) by  (AdminId: {AdminMemberId})", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                    AddKYCStatusHistory res_newkyc = new AddKYCStatusHistory();
                    res_newkyc.MemberId = Convert.ToInt64(MemberId);
                    res_newkyc.Remarks = remarks.Description;
                    res_newkyc.IsAdmin = true;
                    res_newkyc.IpAddress = Common.GetUserIP();
                    res_newkyc.KYCStatus = Convert.ToInt32(IsKYCApproved);
                    res_newkyc.CreatedBy = Convert.ToInt64(AdminMemberId);
                    res_newkyc.CreatedByName = AdminUserName;
                    res_newkyc.IsActive = true;
                    res_newkyc.IsApprovedByAdmin = true;
                    Int64 id = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Insert(res_newkyc, "kycstatushistory");
                    if (id > 0)
                    {
                        Common.AddLogs("Add User KYC status history by(MemberId:" + MemberId + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                        if (res_newkyc.KYCStatus == (int)AddUser.kyc.Verified)
                        {
                            // ******* KYC COMMISION DISTRIBUTION ***** //
                            //if (res.RefId != 0)
                            {
                                AddUserLoginWithPin outobjectRefree = new AddUserLoginWithPin();
                                GetUserLoginWithPin inobjectRefree = new GetUserLoginWithPin();
                                inobjectRefree.MemberId = res.RefId;
                                AddUserLoginWithPin refres_Parent = new AddUserLoginWithPin();
                                if (res.RefId > 0)
                                {
                                    refres_Parent = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Models.Common.Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectRefree, outobjectRefree);
                                }
                                //if (refres_Parent != null && refres_Parent.Id != 0)
                                {
                                    string platform = "web";
                                    string devicecode = HttpContext.Current.Request.Browser.Type;
                                    bool ismobile = false;
                                    string authenticationToken = string.Empty;
                                    string UserInput = string.Empty;

                                    bool IsCommissionDistributed = Common.DistributeRegistrationCommisionPoints(platform, devicecode, ismobile, res.MemberId, refres_Parent, authenticationToken, UserInput, ref RewardPoint, (int)AddSettings.CommissionType.KycApprovedCommission);
                                }
                            }
                        }
                    }
                    else
                    {
                        Common.AddLogs("Not Added User KYC status history by(MemberId:" + MemberId.ToString() + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                    }
                }
                else
                {
                    Common.AddLogs("Not Updated User KYC status history by(MemberId:" + MemberId.ToString() + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                }
            }
            else
            {
                AddKYCStatusHistory res_newkyc = new AddKYCStatusHistory();
                res_newkyc.MemberId = Convert.ToInt64(MemberId);
                res_newkyc.Remarks = remarks.Description;
                res_newkyc.IpAddress = Common.GetUserIP();
                res_newkyc.IsAdmin = true;
                res_newkyc.KYCStatus = Convert.ToInt32(IsKYCApproved);
                res_newkyc.CreatedBy = Convert.ToInt64(AdminMemberId);
                res_newkyc.CreatedByName = AdminUserName;
                res_newkyc.IsActive = true;
                res_newkyc.IsApprovedByAdmin = true;
                Int64 id = RepCRUD<AddKYCStatusHistory, GetKYCStatusHistory>.Insert(res_newkyc, "kycstatushistory");
                if (id > 0)
                {
                    Common.AddLogs("Add User KYC status history (MemberId:" + MemberId.ToString() + ") by(AdminId: " + AdminMemberId + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                }
                else
                {
                    Common.AddLogs("Not Added User KYC status history (MemberId:" + MemberId.ToString() + ") by(AdminId: " + AdminMemberId + ")", true, (int)AddLog.LogType.Kyc, Convert.ToInt64(MemberId), res.FirstName, false, "", "", 0, Convert.ToInt64(AdminMemberId), AdminUserName);
                }
            }
        }

        public static string VotingListAdd(ref bool IsCouponUnlocked, ref string TransactionID, AddCouponsScratched resCoupon, AddUserLoginWithPin resGetRecord, string BankTransactionId, string WalletType, string CustomerId, string authenticationToken, Int64 MemberId, int Type, string VotingCandidateUniqueId, Int64 VotingPackageID, int NoofVotes, string platform, string devicecode, string IpAddress, ref AddVendor_API_Requests objVendor_API_Requests, int VendorApiType)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter member id";
                    return result;
                }
                else if (resGetRecord.MemberId == 0)
                {
                    result = "Invalid User";
                    return result;
                }
                else if (string.IsNullOrEmpty(VotingCandidateUniqueId))
                {
                    result = "Please enter VotingCandidateUniqueId";
                    return result;
                }
                else if (Type == Convert.ToInt16(AddVotingList.Type.VotingPackage) && VotingPackageID == 0)
                {
                    result = "Please Select Voting Package.";
                    return result;
                }

                else if (Type == Convert.ToInt16(AddVotingList.Type.Manual) && NoofVotes == 0)
                {
                    result = "Please Enter No. of Votes.";
                    return result;
                }

                if (resGetRecord == null || resGetRecord.Id == 0)
                {
                    result = "User Not Found.";
                    return result;
                }
                //variables
                decimal WalletBalance = 0;
                Int64 VotingCompetitionID = 0;

                string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                decimal amount = 0;

                //Get Candidate Record
                AddVotingCandidate outobject = new AddVotingCandidate();
                GetVotingCandidate inobject = new GetVotingCandidate();
                inobject.UniqueId = VotingCandidateUniqueId;
                inobject.CheckActive = 1;
                inobject.CheckDelete = 0;
                AddVotingCandidate resCandidate = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject, outobject);

                if (resCandidate.Id > 0)
                {
                    VotingCompetitionID = resCandidate.VotingCompetitionID;
                    AddVotingCompetition outobjComp = new AddVotingCompetition();
                    GetVotingCompetition inobjComp = new GetVotingCompetition();
                    inobjComp.Id = resCandidate.VotingCompetitionID;
                    AddVotingCompetition resComp = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecord(Common.StoreProcedures.sp_VotingCompetition_Get, inobjComp, outobjComp);
                    if (resComp.Id == 0)
                    {
                        result = "Event Not Found";
                        return result;

                    }
                    AddVotingPackages outobjPack = new AddVotingPackages();
                    GetVotingPackages inobjPack = new GetVotingPackages();
                    inobjPack.Id = VotingPackageID;
                    inobjPack.VotingCompetitionID = resCandidate.VotingCompetitionID;
                    AddVotingPackages respackages = RepCRUD<GetVotingPackages, AddVotingPackages>.GetRecord(Common.StoreProcedures.sp_VotingPackages_Get, inobjPack, outobjPack);
                    if (respackages.Id == 0 && Type == Convert.ToInt32(AddVotingList.Type.VotingPackage))
                    {
                        result = "Package Not Found";
                        return result;
                    }
                    //wallet Balance of User
                    WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
                    //Amount Calculation for voting packages or Manually
                    if (Type == Convert.ToInt32(AddVotingList.Type.VotingPackage))
                    {
                        amount = respackages.Amount;
                        NoofVotes = respackages.NoOfVotes;
                    }
                    else if (Type == Convert.ToInt32(AddVotingList.Type.Manual))
                    {
                        amount = (resComp.PricePerVote * NoofVotes);
                    }
                    else
                    {
                        result = "Type Not Found";
                        return result;
                    }

                    string CompetitionName = resComp.Title;
                    if (VotingPackageID > 0 && amount == 0)
                    {
                        if (!respackages.Type)
                        {
                            AddVotingList outobjListCheck = new AddVotingList();
                            GetVotingList inobjListCheck = new GetVotingList();
                            inobjListCheck.VotingCompetitionID = resCandidate.VotingCompetitionID;
                            inobjListCheck.VotingPackageID = VotingPackageID;
                            inobjListCheck.MemberID = MemberId;
                            //inobjListCheck.VotingCandidateUniqueId = VotingCandidateUniqueId;

                            AddVotingList resdataCheck = RepCRUD<GetVotingList, AddVotingList>.GetRecord("sp_VotingList_Get", inobjListCheck, outobjListCheck);
                            //TimeSpan ts = DateTime.UtcNow - resdata.CreatedDate;
                            //CompetitionName = resdata.CompetitionName;
                            //if (resdataCheck != null && resdataCheck.Id > 0 && ts.TotalHours <= 24)
                            if (resdataCheck != null && resdataCheck.Id > 0)
                            {
                                result = "You have already given 1 free vote.";
                                return result;
                            }
                        }
                    }


                    ///Adding Record to votingList 
                    string JsonReq = string.Empty;
                    string TransactionUniqueId = string.Empty;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(VendorApiType)).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests.Res_Khalti_State = "pending";
                    TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalance(resCoupon, ref TransactionID, BankTransactionId, WalletType, CustomerId, amount.ToString(), out result, VendorApiType, resGetRecord, objVendor_API_Requests, "", out WalletBalance, "Voting Pending", "", (int)VendorApi_CommonHelper.VendorTypes.MyPay);
                    if (result == "success")
                    {
                        string VendorOutputResponse = string.Empty;
                        result = AddVoting_Commit(ref IsCouponUnlocked, TransactionUniqueId, resGetRecord, authenticationToken, MemberId, Type, VotingCandidateUniqueId, VotingPackageID, NoofVotes, platform, devicecode, IpAddress, VotingCompetitionID, TransactionUniqueID, amount, resCandidate, respackages, CompetitionName);
                        if (objVendor_API_Requests.Id != 0 && result.ToLower() == "success")
                        {
                            //VendorApi_CommonHelper.UpdateWalletBalance(resCoupon, ref TransactionID,  BankTransactionId, WalletType, UniqueCustomerID, Amount, out msg, VendorApiType, resGetRecord, objVendor_API_Requests, out WalletBalance);
                            objVendor_API_Requests.Res_Khalti_Status = true;
                            if (WalletType == ((int)WalletTransactions.WalletTypes.Bank).ToString())
                            {
                                objVendor_API_Requests.Res_Khalti_Id = BankTransactionId;
                            }
                            else
                            {
                                objVendor_API_Requests.Res_Khalti_Id = TransactionUniqueId;
                            }
                            string Remarks = NoofVotes + $" Vote(s) of amount Rs.{amount} to {resCandidate.Name} ({CompetitionName})";
                            Int32 CouponApplyType = (int)AddCoupons.CouponReceivedBy.Voting;
                            result = Common.UpdateCompleteTransaction(ref IsCouponUnlocked, ref TransactionID, resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, "", Remarks, "", CouponApplyType);
                            if (result.ToLower() == "success")
                            {
                                string Title = "Voting Completed";
                                string Message = Remarks; //$"Voting Transaction Completed Successfully. Number Of Votes {NoofVotes} Rs. {amount}.";
                                Int32 VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Voting;
                                Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, resGetRecord.MemberId, Title, Message, resGetRecord.DeviceCode);
                            }
                        }
                        else
                        {
                            result = Common.RefundUpdateTransaction(resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, BankTransactionId, VendorApiType, WalletType, platform, devicecode);
                        }
                    }

                }
                else
                {
                    Common.AddLogs("Candidate not found", false, Convert.ToInt32(AddLog.LogType.Transaction), resGetRecord.MemberId, resGetRecord.FirstName + " " + resGetRecord.LastName, true, platform, devicecode);
                    result = "Candidate not found";
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                result = e.Message;
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Voting List Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.DBLogs), 0, "");
                result = ex.Message;
            }
            return result;
        }

        private static string AddVoting_Commit(ref bool IsCouponUnlocked, string TransactionUniqueId, AddUserLoginWithPin resGetRecord, string authenticationToken, long MemberId, int Type, string VotingCandidateUniqueId, long VotingPackageID, int NoofVotes, string platform, string devicecode, string IpAddress, long VotingCompetitionID, string TransactionUniqueID, decimal amount, AddVotingCandidate resCandidate, AddVotingPackages respackages, string CompetitionName)
        {
            string msg = "";
            try
            {

                VotingLists res = new VotingLists();
                res.VotingCandidateName = resCandidate.Name;
                res.VotingCompetitionId = VotingCompetitionID;
                res.VotingCandidateUniqueId = VotingCandidateUniqueId;
                res.VotingPackageID = VotingPackageID;
                res.NoofVotes = NoofVotes;
                res.MemberID = MemberId;
                res.MemberName = resGetRecord.FirstName + " " + resGetRecord.LastName;
                res.MemberContactNumber = resGetRecord.ContactNumber;
                res.PlatForm = platform;
                res.Amount = amount;
                res.DeviceCode = devicecode;
                res.IpAddress = IpAddress;
                res.IsActive = true;
                res.IsApprovedByAdmin = true;
                res.IsDeleted = false;
                res.TransactionUniqueId = TransactionUniqueID;
                res.CreatedBy = Common.GetCreatedById(authenticationToken);
                res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                res.CreatedDate = DateTime.UtcNow;
                if (respackages.Amount == 0 && Type == Convert.ToInt32(AddVotingList.Type.VotingPackage))
                {
                    res.FreeVotes = respackages.NoOfVotes;
                }
                else
                {
                    res.PaidVotes = NoofVotes;
                }
                bool IsVotingAdded = res.Add();

                if (IsVotingAdded && res.Id > 0)
                {

                    Common.AddLogs($"Voting Transaction Completed Successfully for {resCandidate.Name}, {CompetitionName}", false, Convert.ToInt32(AddLog.LogType.Transaction), resGetRecord.MemberId, resGetRecord.FirstName + " " + resGetRecord.LastName, true, platform, devicecode);
                    msg = "success";
                    Common.UpdateVotingCandidateRank(resCandidate, res);


                }
                else
                {
                    msg = "Vote to candidate not added";
                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.Transaction), resGetRecord.MemberId, resGetRecord.FirstName + " " + resGetRecord.LastName, true, platform, devicecode);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public static string ValidateDateForNepali(string authenticationToken, string EnglishDateStr, string NepaliDateStr, string platform, string devicecode, string IpAddress)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(NepaliDateStr))
                {
                    result = "Please enter NepaliDate";
                }
                else
                {
                    NepaliDate objNepaliDate = NepaliCalendar.FixDate(NepaliDateStr);
                    if (objNepaliDate != null)
                    {
                        result = "success";
                    }
                    else
                    {
                        result = "Invalid";
                    }
                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                result = e.Message;
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Voting List Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.DBLogs), 0, "");
                result = ex.Message;
            }
            return result;
        }

    }
}