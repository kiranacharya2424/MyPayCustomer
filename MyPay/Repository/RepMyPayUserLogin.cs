using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Repository
{
    public static class RepMyPayUserLogin
    {
        public static string Login(string ContactNumber, string Password, bool RememberMe, bool checkPassword = true, string Token = "")
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(ContactNumber))
                {
                    result = "Please enter ContactNumber.";
                    return result;
                }
                else if (checkPassword && string.IsNullOrEmpty(Password))
                {
                    result = "Please enter password";
                    return result;
                }
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.ContactNumber = ContactNumber;
                inobject.CheckDelete = 0;
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);

                if (res != null && res.Id != 0)
                {
                    if (!res.IsActive)
                    {
                        return "Account is not active";
                    }
                    else if (checkPassword && Common.DecryptString(res.Password) != Password)
                    {
                        return "Invalid Credentials";
                    }
                    else
                    {
                        if (Common.SetMyPayUserSession(res, Token))
                        {
                            if (RememberMe)
                            {
                                HttpCookie cookie = new HttpCookie("MyPayUserLogin");
                                cookie.Values.Add("username", res.ContactNumber);
                                cookie.Expires = DateTime.UtcNow.AddDays(20);
                                HttpContext.Current.Response.Cookies.Add(cookie);
                            }
                            Common.AddLogs("Logged in MyPayUser", true, Convert.ToInt32(AddLog.LogType.User), res.MemberId, res.FirstName + " " + res.LastName, false, "Web", "", (int)AddLog.LogActivityEnum.Login_user);
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
                Models.Common.Common.AddLogs("MyPayUser Login  Error:" + ex.Message, true);
                result = ex.Message;
            }
            return result;
        }

        public static string Logout()
        {
            string result = "";
            try
            {
                Common.AddLogs("Logged Out MyPayUser", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(HttpContext.Current.Session["MyPayUserMemberId"]), Convert.ToString(HttpContext.Current.Session["MyPayUserName"]), false, "Web", "", (int)AddLog.LogActivityEnum.Logout_user);
                Common.RemoveMyPayUserSession();
                result = "success";
                return result;
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("MyPayUser Logout  Error:" + ex.Message, true);
                result = ex.Message;
            }
            return result;
        }
    }
}