using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyPay.Models.Add.AddMerchant;

namespace MyPay.Repository
{
    public static class RepMerchantLogin
    {
        public static string Login(string UserName, string Password, bool RememberMe, ref AddMerchant objMerchant)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    result = "Please enter UserName.";
                    return result;
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    result = "Please enter password";
                    return result;
                }
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.UserName = UserName;
                //inobject.MerchantType = Convert.ToInt32(MerChantType.Merchant);
                inobject.CheckDelete = 0; 
                //inobject.RoleId = 1;
                AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);

                if (res != null && res.Id != 0)
                {
                    if (!res.IsActive)
                    {
                        return "Account is not active";
                    }
                    else if (Common.DecryptionFromKey(res.Password, res.secretkey) != Password)
                    {
                        return "Invalid Credentials";
                    }
                    else
                    {
                        if (Common.SetMerchantSession(res))
                        {
                            if (RememberMe == true)
                            {
                                objMerchant = res;
                                if (RememberMe)
                                {
                                    HttpCookie cookie = new HttpCookie("MerchantLogin");
                                    cookie.Values.Add("username", res.UserName);
                                    cookie.Expires = DateTime.UtcNow.AddDays(20);
                                    HttpContext.Current.Response.Cookies.Add(cookie);
                                }
                            }
                            Common.AddLogs("Logged in Merchant", true, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " "+ res.LastName,  false, "Web", "", (int)AddLog.LogActivityEnum.Login_Merchant);
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
                Models.Common.Common.AddLogs("Merchant Login  Error:" + ex.Message, true);
                //new ClsException("RepUsers.cs", "UserPersonal", ex);
                result = ex.Message;
            }
            return result;
        }
        public static string GetMerchantDetailsFromEmail(string Email, ref AddMerchant objMerchant)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    result = "Please enter Email.";
                    return result;
                }
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.EmailID = Email;
                result = GetMerchantDetails(ref outobject, inobject);
                if (result == "success")
                {
                    Common.AddLogs("Logged in merchant", true,(int)AddLog.LogType.Merchant, outobject.UserMemberId, outobject.FirstName + " " + outobject.LastName);
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Merchant Login  Error:" + ex.Message, true);
                //new ClsException("RepUsers.cs", "UserPersonal", ex);
                result = ex.Message;
            }
            return result;
        }
        public static string GetMerchantDetailsFromUserName(string UserName, ref AddMerchant objMerchant)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    result = "Please enter UserName.";
                    return result;
                }
                GetMerchant inobject = new GetMerchant();
                inobject.UserName = UserName;
                result = GetMerchantDetails(ref objMerchant, inobject);
                if (result == "success")
                {
                    Common.AddLogs("Logged in merchant", true, (int)AddLog.LogType.Merchant, objMerchant.UserMemberId, objMerchant.FirstName + " " + objMerchant.LastName);
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Merchant Login  Error:" + ex.Message, true);
                //new ClsException("RepUsers.cs", "UserPersonal", ex);
                result = ex.Message;
            }
            return result;
        }

        private static string GetMerchantDetails(ref AddMerchant outobject, GetMerchant inobject)
        {
            inobject.CheckDelete = 0;
            //inobject.RoleId = 1;
            outobject = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            string result = string.Empty;
            if (outobject != null && outobject.Id != 0)
            {
                if (!outobject.IsActive)
                {
                    result = "Account is not active";
                }
                else
                {
                    result = "success";
                }
            }
            else
            {
                result = "This user does not exist";
            }
            return result;
        }

        public static string Logout()
        {
            string result = "";
            try
            {
                Common.AddLogs("Logged Out Merchant", true, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(HttpContext.Current.Session["MerchantUniqueId"]), Convert.ToString(HttpContext.Current.Session["MerchantUserName"]), false, "Web", "", (int)AddLog.LogActivityEnum.Logout_Merchant);
                Common.RemoveMerchantSession();
                result = "success";
                return result;
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Merchant Logout  Error:" + ex.Message, true);
                //new ClsException("RepUsers.cs", "UserPersonal", ex);
                result = ex.Message;
            }
            return result;
        }
    }
}