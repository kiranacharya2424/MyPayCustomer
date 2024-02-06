using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using log4net;
using NepaliCalendarBS;
using ServiceStack;

namespace MyPay.Repository
{
    public static class RepAdminUser
    {
        public static string Login(string UserId, string Password, bool RememberMe, ref AddAdmin objAddAdmin)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    result = "Please enter UserId.";
                    return result;
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    result = "Please enter password";
                    return result;
                }
                AddAdmin outobject = new AddAdmin();
                GetAdmin inobject = new GetAdmin();
                inobject.UserId = UserId;
                inobject.CheckDelete = 0;
                inobject.CheckActive = 1;
    
                //inobject.RoleId = 1;
                AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
      
                if (res != null && res.Id != 0)
                {
                    if (!res.IsActive)
                    {
                        return "Account is not active";
                    }


                    else if (Common.DecryptString(res.Password) != Password)
                    {
                        return "Invalid Credentials";
                    }
                    else
                    {
                        if (Common.SetAdminSession(res))
                        {
                            if (RememberMe == true)
                            {
                                objAddAdmin = res;
                                if (RememberMe)
                                {
                                    HttpCookie cookie = new HttpCookie("AdminLogin");
                                    cookie.Values.Add("username", res.UserId);
                                    cookie.Expires = DateTime.UtcNow.AddDays(20);
                                    HttpContext.Current.Response.Cookies.Add(cookie);
                                }
                            }
                            Common.AddLogs("Logged in admin", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]), Convert.ToString(HttpContext.Current.Session["AdminUserName"]), false, "Web", "", (int)AddLog.LogActivityEnum.Login_Admin, Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]), Convert.ToString(HttpContext.Current.Session["AdminUserName"]));
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
                Models.Common.Common.AddLogs("Admin Login  Error:" + ex.Message, true);
              

                log.Info("Error: " + ex.ToString());

                result = ex.Message;
            }
            return result;
        }
        public static string GetAdminDetailsFromEmail(string Email, ref AddAdmin objAddAdmin)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    result = "Please enter Email.";
                    return result;
                }
                AddAdmin outobject = new AddAdmin();
                GetAdmin inobject = new GetAdmin();
                inobject.Email = Email;
                result = GetAdminDetails(ref outobject, inobject);
                if (result == "success")
                {
                    Common.AddLogs("Forgot Password: Get Admin Details From Email", true, 0, 0, "", false, "WEB");
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Admin Login  Error:" + ex.Message, true);
                //new ClsException("RepUsers.cs", "UserPersonal", ex);
                result = ex.Message;
            }
            return result;
        }
        public static string GetAdminDetailsFromUserName(string UserName, ref AddAdmin objAddAdmin)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    result = "Please enter Email.";
                    return result;
                }
                GetAdmin inobject = new GetAdmin();
                inobject.UserName = UserName;
                result = GetAdminDetails(ref objAddAdmin, inobject);
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Admin Login  Error:" + ex.Message, true);
                //new ClsException("RepUsers.cs", "UserPersonal", ex);
                result = ex.Message;
            }
            return result;
        }

        private static string GetAdminDetails(ref AddAdmin outobject, GetAdmin inobject)
        {
            inobject.CheckDelete = 0;
            //inobject.RoleId = 1;
            outobject = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
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
                Common.AddLogs("Logged Out admin", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]), Convert.ToString(HttpContext.Current.Session["AdminUserName"]), false, "Web", "", (int)AddLog.LogActivityEnum.Logout_Admin);
                Common.RemoveAdminSession();
                result = "success";
                return result;
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Admin Logout  Error:" + ex.Message, true);
                //new ClsException("RepUsers.cs", "UserPersonal", ex);
                result = ex.Message;
            }
            return result;
        }

    }
}