using Microsoft.IdentityModel.Tokens;
using MyPay.Models.Add;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.RemittanceAPI.Add;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using log4net;
using iTextSharp.text.pdf;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing;
using ServiceStack;
using QRCoder;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.Web.Helpers;
using ServiceStack.Html;
using MyPay.Models.Add.Agent;

namespace MyPay.Models.Common
{
    public class CommonHelpers
    {
        private SqlConnection con = new SqlConnection();
        private SqlConnection conslave = new SqlConnection();
        private SqlConnection conRemittance = new SqlConnection();
        private SqlDataAdapter adp = new SqlDataAdapter();
        private SqlCommand cmd = new SqlCommand();


        public CommonHelpers()
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


            //test
            con = new SqlConnection();
            conslave = new SqlConnection();
            //string text = Common.EncryptString("Data Source=103.90.85.82;Initial Catalog=MyPayWebcomTest;Integrated Security=false;USER ID=sa; Password=");

            string text1 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            con.ConnectionString = text1;
            conslave.ConnectionString = ConfigurationManager.ConnectionStrings["connectionstringSlave"].ToString();
            conRemittance.ConnectionString = ConfigurationManager.ConnectionStrings["connectionstringRemittance"].ToString();
            var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/apisettings.json"));

            AddApiSettings objApiSettings = new AddApiSettings();
            objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiSettings>(json);
            if (objApiSettings != null)
            {
                if (objApiSettings.Id == 1)
                {
                    conslave.ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                }
            }

        }
        public static List<SelectListItem> GetSelectList_AdminLoginRole(int RoleId)
        {
            List<AddRole> stlist = new List<AddRole>();
            AddRole outobject = new AddRole();
            GetRole inobject = new GetRole();
            inobject.CheckIsAdminLogin = 1;
            stlist = RepCRUD<GetRole, AddRole>.GetRecordList(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
            List<SelectListItem> roleList = (from p in stlist.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.RoleName,
                                                 Value = p.Id.ToString()
                                             }).ToList();
            return CreateDropdown(RoleId.ToString(), roleList, "Select Role");
        }
        public static List<SelectListItem> GetSelectList_RemittanceCurrency(int FromCurrencyId)
        {
            List<AddRemittanceCurrencyList> ctlist = new List<AddRemittanceCurrencyList>();
            AddRemittanceCurrencyList list = new AddRemittanceCurrencyList();
            list.CheckActive = 1;
            list.CheckDelete = 0;
            DataTable dt = list.GetList();
            ctlist = (List<AddRemittanceCurrencyList>)CommonEntityConverter.DataTableToList<AddRemittanceCurrencyList>(dt);
            //AddCountry outobject = new AddCountry();
            //GetCountry inobject = new GetCountry();
            //ctlist = RepCRUD<GetCountry, AddCountry>.GetRecordList(Common.StoreProcedures.sp_Country_Get, inobject, outobject);
            List<SelectListItem> currencyList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.CurrencyName,
                                                     Value = p.Id.ToString()
                                                 }).ToList();
            return CreateDropdown(FromCurrencyId.ToString(), currencyList, "Select Currency");
        }
        public static List<SelectListItem> GetSelectList_MerchantType()
        {
            var MerchantTypeList = new List<SelectListItem>();
            MerchantTypeList.Add(new SelectListItem { Text = "Merchant Type", Value = "2", Selected = true });
            foreach (int value in Enum.GetValues(typeof(AddMerchant.MerChantType)))
            {
                string stringValue = Enum.GetName(typeof(AddMerchant.MerChantType), value).Replace("_", " ");
                MerchantTypeList.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return MerchantTypeList;
        }
        public static List<SelectListItem> GetSelectList_RemittanceProofType(AddRemittanceUser model)
        {
            var prooftypelist = new List<SelectListItem>();
            prooftypelist.Add(new SelectListItem
            {
                Text = "Select ProofType",
                Value = "0",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(AddRemittanceUser.ProofTypes)))
            {
                string stringValue = Enum.GetName(typeof(AddRemittanceUser.ProofTypes), value).ToString().Replace("_", " ");
                if (value == model.ProofType)
                {
                    prooftypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    prooftypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return prooftypelist;
        }
        public static List<SelectListItem> GetSelectList_RemittanceGenderType(AddRemittanceUser model)
        {
            var gendertypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddRemittanceUser.GenderTypes)))
            {
                string stringValue = Enum.GetName(typeof(AddRemittanceUser.GenderTypes), value).ToString().Replace("_", " ");
                if (value == model.GenderType)
                {
                    gendertypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    gendertypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return gendertypelist;
        }
        public static List<SelectListItem> GetSelectList_RemittanceFeeType(AddRemittanceUser model)
        {
            var Feetypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddRemittanceUser.RemittanceFeeType)))
            {
                string stringValue = Enum.GetName(typeof(AddRemittanceUser.RemittanceFeeType), value).ToString().Replace("_", " ");
                if (value == model.FeeType)
                {
                    Feetypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    Feetypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return Feetypelist;
        }
        public static List<SelectListItem> GetSelectList_KhaltiEnumList()
        {

            var khaltienum = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(VendorApi_CommonHelper.KhaltiAPIName)))
            {
                string stringValue = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), value).Replace("_", " ");
                //int val = stringValue.IndexOf("ntc");
                if (stringValue.Contains("khalti") || stringValue.ToLower().Contains("mypay events"))
                {
                    khaltienum.Add(new SelectListItem { Text = stringValue.Replace("khalti", "").Replace("_", " ").ToUpper(), Value = value.ToString() });
                }
            }
            khaltienum = khaltienum.OrderBy(x => x.Text).ToList();

            return CreateDropdown("", khaltienum, "Select Type");
            // return khaltienum;
        }
        public static List<SelectListItem> GetSelectList_Role(int RoleId)
        {
            List<AddRole> stlist = new List<AddRole>();
            AddRole outobject = new AddRole();
            GetRole inobject = new GetRole();
            stlist = RepCRUD<GetRole, AddRole>.GetRecordList(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
            List<SelectListItem> roleList = (from p in stlist.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.RoleName,
                                                 Value = p.Id.ToString()
                                             }).ToList();
            return CreateDropdown(RoleId.ToString(), roleList, "Select Role");
        }
        public static List<SelectListItem> GetSelectList_MerchantWithdrawalStatus()
        {
            var statuslist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddMerchantWithdrawalRequest.MerchantWithdrawalStatus)))
            {
                string stringValue = Enum.GetName(typeof(AddMerchantWithdrawalRequest.MerchantWithdrawalStatus), value).Replace("_", " ");
                statuslist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return statuslist;
        }
        public static List<SelectListItem> GetSelectList_RemittanceSourceCurrency(int FromCurrencyId)
        {
            List<AddRemittanceSourceCurrencyList> ctlist = new List<AddRemittanceSourceCurrencyList>();
            AddRemittanceSourceCurrencyList list = new AddRemittanceSourceCurrencyList();
            list.CheckActive = 1;
            list.CheckDelete = 0;
            DataTable dt = list.GetList();
            ctlist = (List<AddRemittanceSourceCurrencyList>)CommonEntityConverter.DataTableToList<AddRemittanceSourceCurrencyList>(dt);
            //AddCountry outobject = new AddCountry();
            //GetCountry inobject = new GetCountry();
            //ctlist = RepCRUD<GetCountry, AddCountry>.GetRecordList(Common.StoreProcedures.sp_Country_Get, inobject, outobject);
            List<SelectListItem> currencyList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.CurrencyName,
                                                     Value = p.CurrencyId.ToString()
                                                 }).ToList();
            return CreateDropdown(FromCurrencyId.ToString(), currencyList, "Select Source Currency");
        }

        public static List<SelectListItem> GetSelectList_RemittanceSourceCurrency()
        {
            List<AddRemittanceSourceCurrencyList> ctlist = new List<AddRemittanceSourceCurrencyList>();
            AddRemittanceSourceCurrencyList list = new AddRemittanceSourceCurrencyList();
            list.CheckActive = 1;
            list.CheckDelete = 0;
            DataTable dt = list.GetList();
            ctlist = (List<AddRemittanceSourceCurrencyList>)CommonEntityConverter.DataTableToList<AddRemittanceSourceCurrencyList>(dt);
            List<SelectListItem> currencyList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.CurrencyName,
                                                     Value = p.CurrencyId.ToString()
                                                 }).ToList();
            return CreateDropdown("", currencyList, "Select Source Currency");
        }

        public static List<SelectListItem> GetSelectList_RemittanceDestinationCurrency()
        {
            List<AddRemittanceDestinationCurrencyList> ctlist = new List<AddRemittanceDestinationCurrencyList>();
            AddRemittanceDestinationCurrencyList list = new AddRemittanceDestinationCurrencyList();
            list.CheckActive = 1;
            list.CheckDelete = 0;
            DataTable dt = list.GetList();
            ctlist = (List<AddRemittanceDestinationCurrencyList>)CommonEntityConverter.DataTableToList<AddRemittanceDestinationCurrencyList>(dt);
            List<SelectListItem> currencyList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.CurrencyName,
                                                     Value = p.CurrencyId.ToString()
                                                 }).ToList();
            return CreateDropdown("", currencyList, "Select Destination Currency");
        }

        public static List<SelectListItem> GetSelectList_RemittanceWalletType()
        {
            var walletlist = new List<SelectListItem>();

            foreach (int value in Enum.GetValues(typeof(AddRemittanceTransactions.WalletTypes)))
            {
                string stringValue = Enum.GetName(typeof(AddRemittanceTransactions.WalletTypes), value).Replace("_", " ");

                walletlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return CreateDropdown("", walletlist, "Select Type");
        }

        public static List<SelectListItem> GetSelectList_RemittanceTxnSign()
        {
            var signlist = new List<SelectListItem>();

            foreach (int value in Enum.GetValues(typeof(AddRemittanceTransactions.Signs)))
            {
                string stringValue = Enum.GetName(typeof(AddRemittanceTransactions.Signs), value).Replace("_", " ");

                signlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return CreateDropdown("", signlist, "Select Sign");
        }
        //public string CheckApiToken(string hash, Int64 timestamp, string md5hash, string platform, string version, string devicecode, string secretkey)
        //{


        //    log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
        //    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //    string result = String.Empty;
        //    try
        //    {
        //        //
        //        //System.Threading.Thread.Sleep(1000);
        //        //return "MyPay will be performing System Maintenence from Sunday, Jun 5, 2022 4:00 PM to next 3Hrs. During this time you will not be able to access your account through App. Thank you for your patience as we make system improvements. ";

        //        //var userAgent = HttpContext.Current.Request.UserAgent.ToLower();

        //        var userAgent = "";
        //        if (HttpContext.Current.Request.UserAgent != null)
        //        {
        //            userAgent = HttpContext.Current.Request.UserAgent.ToLower();
        //        }
        //        var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/apisettings.json"));
        //        AddApiSettings objApiSettings = new AddApiSettings();
        //        objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiSettings>(json);
        //        //Common.IosVersion = objApiSettings.IOSVersion;
        //        //Common.AndroidVersion = objApiSettings.AndroidVersion;


        //        var jsonVersionSettings = File.ReadAllText("C:\\MyPaySettings\\versionsettings.json");
        //        AddApiVersionSettings objVersionSettings = new AddApiVersionSettings();
        //        objVersionSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiVersionSettings>(jsonVersionSettings);
        //        Common.IosVersion = objVersionSettings.iosVersion;
        //        Common.AndroidVersion = objVersionSettings.androidVersion;


        //        //using (MyPayEntities db = new MyPayEntities())
        //        //{
        //        //    objApiSettings = db.ApiSettings.FirstOrDefault();
        //        //    Common.IosVersion = objApiSettings.IOSVersion;
        //        //    Common.AndroidVersion = objApiSettings.AndroidVersion;
        //        //}
        //        DateTime date = new DateTime(1970, 01, 01).AddMilliseconds(timestamp);
        //        double diffInSeconds = (DateTime.UtcNow - date).TotalSeconds;

        //        if (objApiSettings != null && !string.IsNullOrEmpty(objApiSettings.SchedulerMessage) && (objApiSettings.ScheduleStatus) && ((objApiSettings.ScheduleStartTime < Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)) && objApiSettings.ScheduleEndTime > Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)))))
        //        {
        //            result = objApiSettings.SchedulerMessage;

        //        }
        //        else if ((objApiSettings == null || objApiSettings.CheckVersion == 1) && string.IsNullOrEmpty(version))
        //        {
        //            result = "Please Enter Version";
        //        }
        //        else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckDevicecode == 1) && string.IsNullOrEmpty(devicecode))
        //        {
        //            result = "Please Enter Device Code";
        //        }
        //        else if (!userAgent.Contains("android") && !userAgent.Contains("mypay"))
        //        {
        //            result = "Invalid Request";
        //        }
        //        else if (Common.SecretKey != Common.DecryptString(secretkey))
        //        {
        //            result = "Invalid Key";
        //        }
        //        //else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckAndroidVersion == 1) && version != AndroidVersion)
        //        //{
        //        //    result = "Please update the application";
        //        //}
        //        //else if (platform != "ios" && string.IsNullOrEmpty(result) && (timestamp == null || timestamp == 0 || Convert.ToInt64(diffInSeconds) > DiffSeconds))
        //        //{
        //        //    result = "Server communication timeout, Please try again.";
        //        //}



        //        else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckHash == 1) && (hash.Trim() != md5hash.Trim()) && !userAgent.Contains("mypay"))
        //        {
        //            log.Info("result: " + result);
        //            log.Info("objApiSettings: " + objApiSettings);

        //            log.Info("hash: " + hash);
        //            log.Info("md5hash: " + md5hash);
        //            log.Info("userAgent: " + userAgent);
        //            result = "Hash Not Matched";
        //        }
        //        else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckPlatform == 1) && string.IsNullOrEmpty(platform))
        //        {
        //            result = "Please Enter Platform";
        //        }
        //        else
        //        {
        //            result = "Success";
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }
        //    return result;
        //}


        public CommonDbResponse ExecuteProcedureGetValue(string Procedure, Hashtable HT)
        {
            CommonDbResponse obj = new CommonDbResponse();
            string result = "";
            string HTString = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString());
            if (Procedure.ToLower().IndexOf("remittance") > 0)
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstringRemittance"].ToString());
            }
            SqlCommand cmd = new SqlCommand();
            string key = null;
            cmd = new SqlCommand(Procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                foreach (string key_loopVariable in HT.Keys)
                {
                    key = key_loopVariable;
                    cmd.Parameters.AddWithValue("@" + key, HT[key]);

                    HTString += $"@{key}: '{HT[key]}', ";
                }
                SqlParameter retval = new SqlParameter("@OUTPUT", SqlDbType.VarChar, 200);
                retval.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(retval);
                cmd.CommandTimeout = 0;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        obj.code = reader.GetString(0); // Assuming the ID is an integer
                        obj.Message = reader.GetString(1); // Assuming the message is a string


                    }
                }
                // cmd.ExecuteNonQuery();
                return obj;
            }
            catch (Exception ex)
            {
                Common.AddLogs($"Exception occured on {Common.fnGetdatetime()} in {Procedure} with Parameters as : {HTString}. Exception Details: {ex.Message}", false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                con.Close();
                cmd.Dispose();
                result = "0";
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return obj;
        }

        public string CheckApiToken(string hash, Int64 timestamp, string md5hash, string platform, string version, string devicecode, string secretkey, string req = "")
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info("check user api token hit");


            string result = String.Empty;
            try
            {


                var userAgent = "";
                if (HttpContext.Current.Request.UserAgent != null)
                {
                    userAgent = HttpContext.Current.Request.UserAgent.ToLower();
                }
                var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/apisettings.json"));
                AddApiSettings objApiSettings = new AddApiSettings();
                objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiSettings>(json);
                //Common.IosVersion = objApiSettings.IOSVersion;
                //Common.AndroidVersion = objApiSettings.AndroidVersion;


                var jsonVersionSettings = File.ReadAllText("C:\\MyPaySettings\\versionsettings.json");
                AddApiVersionSettings objVersionSettings = new AddApiVersionSettings();
                objVersionSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiVersionSettings>(jsonVersionSettings);
                Common.IosVersion = objVersionSettings.iosVersion;
                Common.AndroidVersion = objVersionSettings.androidVersion;


                //using (MyPayEntities db = new MyPayEntities())
                //{
                //    objApiSettings = db.ApiSettings.FirstOrDefault();
                //    Common.IosVersion = objApiSettings.IOSVersion;
                //    Common.AndroidVersion = objApiSettings.AndroidVersion;
                //}
                DateTime date = new DateTime(1970, 01, 01).AddMilliseconds(timestamp);
                double diffInSeconds = (DateTime.UtcNow - date).TotalSeconds;

                if (objApiSettings != null && !string.IsNullOrEmpty(objApiSettings.SchedulerMessage) && (objApiSettings.ScheduleStatus) && ((objApiSettings.ScheduleStartTime < Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)) && objApiSettings.ScheduleEndTime > Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)))))
                {
                    result = objApiSettings.SchedulerMessage;

                }
                else if ((objApiSettings == null || objApiSettings.CheckVersion == 1) && string.IsNullOrEmpty(version))
                {
                    result = "Please Enter Version";
                }
                else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckDevicecode == 1) && string.IsNullOrEmpty(devicecode))
                {
                    result = "Please Enter Device Code";
                }
                //else if (!userAgent.Contains("android") && !userAgent.Contains("mypay"))
                //{
                //    result = "Invalid Request";
                //}
                else if (Common.SecretKey != Common.DecryptString(secretkey))
                {
                    result = "Invalid Key";
                }
                //else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckAndroidVersion == 1) && version != AndroidVersion)
                //{
                //    result = "Please update the application";
                //}
                //else if (platform != "ios" && string.IsNullOrEmpty(result) && (timestamp == null || timestamp == 0 || Convert.ToInt64(diffInSeconds) > DiffSeconds))
                //{
                //    result = "Server communication timeout, Please try again.";
                //}
                else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckHash == 1) && (hash.Trim() != md5hash.Trim()) && !userAgent.Contains("mypay"))
                {
                    log.Info("string.IsNullOrEmpty(result): " + string.IsNullOrEmpty(result));
                    log.Info("objApiSettings == null:" + objApiSettings == null);
                    log.Info("objApiSettings.CheckHash == 1:" + (objApiSettings.CheckHash == 1));
                    log.Info("hash.Trim() != md5hash.Trim(): " + hash.Trim() != md5hash.Trim());
                    log.Info("!userAgent.Contains(\"mypay\"): " + !userAgent.Contains("mypay"));
                    log.Info("hash: " + hash.Trim());
                    log.Info("hash2: " + md5hash.Trim());
                    log.Info("Actual JSON: " + req);

                    result = "Hash Not Matched";
                }
                else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckPlatform == 1) && string.IsNullOrEmpty(platform))
                {
                    result = "Please Enter Platform";
                }
                else
                {
                    result = "Success";
                }
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        public string GetJWToken(string ContactNumber)
        {
            //DateTime _ExpirationTime = DateTime.UtcNow.AddMinutes(2);
            DateTime _ExpirationTime = DateTime.UtcNow.AddMinutes(2);
            string key = "mypayappsecurecred495a"; //Secret
            var issuer = "http://mypay.com.np";

            Guid g = Guid.NewGuid();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("key", key));
            permClaims.Add(new Claim("data", g.ToString()));
            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                             expires: _ExpirationTime,
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            string originalFileName = Common.EncryptString(ContactNumber).Replace("/", "@@@");
            string filepath = HttpContext.Current.Server.MapPath("/UserCodes/" + originalFileName + ".txt");
            try
            {

                if (!System.IO.File.Exists(filepath))
                {
                    var UserCodesFile = System.IO.File.Create(filepath);
                    UserCodesFile.Close();
                    UserCodesFile.Dispose();
                }
                string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(15).ToString("dd-MMM-yyyy HH:mm:ss");
                //NewTokenUpdatedTime = Common.EncryptString(NewTokenUpdatedTime);
                System.IO.File.WriteAllText(filepath, NewTokenUpdatedTime);

            }
            catch (Exception ex)
            {

            }
            return jwt_token;
        }

        public AddUserLoginWithPin CheckUserDetail(string CustomerID, string UserInput, string ReferenceNo, string BankTransactionId, ref AddCouponsScratched resGetCouponsScratched, string CouponCode, string DeviceId, ref string result, int Type, int VendorApiType, Int64 memId, bool CheckInsuuficientBalance, bool checkamount, string amount, bool checkpin, string mpin, string Check = "", bool notcheckMemberId = false, bool forceGetUserDtl = false)
        {
            AddUserLoginWithPin resUser = new AddUserLoginWithPin();
            try
            {
                DeviceId = DeviceId.Replace("’", "").Replace("?", "");
                var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/apisettings.json"));
                AddApiSettings objApiSettings = new AddApiSettings();
                objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiSettings>(json);
                string VendorApiTypeName = string.Empty;

                string[] khalti_services_array = { "-tv", "internet", "khanepani", "insurance", "airlines", "flight", "electricity", "-nea", "antivirus", "datapack", "echalan", "ntc", "ncell", "smartcell", "dishhome", "landline", "nettv", "topup", "worldlink", "CableCar" };
                if (VendorApiType != 0)
                {
                    VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").ToString();
                    if (VendorApiTypeName.ToString().ToLower().Contains("khalti") && objApiSettings.CheckKhalti == 1)
                    {
                        result = Common.TemporaryServiceUnavailable;
                        return resUser;
                    }
                }
                else if (objApiSettings.CheckKhalti == 1)
                {
                    foreach (string x in khalti_services_array)
                    {
                        if (HttpContext.Current.Request.Url.ToString().ToLower().Contains(x))
                        {
                            result = Common.TemporaryServiceUnavailable;
                            return resUser;
                        }
                    }
                }

                //if (!CheckInsuuficientBalance && !checkamount && !checkpin && !forceGetUserDtl)
                //{
                //    result = "Success";
                //    return resUser;
                //}
                if (checkamount)
                {
                    if (!string.IsNullOrEmpty(amount))
                    {
                        decimal Num;
                        bool isNum = decimal.TryParse(amount, out Num);
                        if (!isNum)
                        {
                            result = "Please enter valid amount.";
                        }
                        else if (Convert.ToDecimal(amount) <= 0)
                        {
                            result = "Please enter amount greater than zero.";
                        }
                    }
                    else
                    {
                        result = "Please enter amount.";
                    }
                }
                if (string.IsNullOrEmpty(DeviceId))
                {
                    result = "Please enter DeviceId";
                }
                if (Check == "")
                {
                    decimal finalamount = 0;
                    if (HttpContext.Current.Request.Headers.GetValues("UToken") != null)
                    {
                        string secretkey = HttpContext.Current.Request.Headers.GetValues("UToken").First();

                        if (!string.IsNullOrEmpty(secretkey))
                        {

                            AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                            inobjectUser.JwtToken = secretkey;
                            inobjectUser.DeviceId = DeviceId;
                            inobjectUser.MemberId = memId;
                            resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                            if (resUser.Id > 0)
                            {
                                string originalFileName = Common.EncryptString(resUser.ContactNumber).Replace("/", "@@@");

                                string TokenUpdatedTime = string.Empty;
                                string filepath = HttpContext.Current.Server.MapPath("/UserCodes/" + originalFileName + ".txt");
                                if (!System.IO.File.Exists(filepath))
                                {
                                    result = Common.Invalidusertoken;
                                    return resUser;
                                }
                                if (resUser.IsActive == false)
                                {
                                    result = Common.InactiveUserMessage;
                                    return resUser;
                                }
                                TokenUpdatedTime = (File.ReadAllText(filepath));
                                if (!string.IsNullOrEmpty(TokenUpdatedTime) && DateTime.UtcNow < Convert.ToDateTime(TokenUpdatedTime))
                                {
                                    if (!notcheckMemberId && memId.ToString() != resUser.MemberId.ToString())
                                    {
                                        result = Common.Invalidusertoken;
                                    }
                                    else
                                    {
                                        if (CheckInsuuficientBalance)
                                        {
                                            AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(resUser.MemberId.ToString(), amount, VendorApiType.ToString());
                                            string WalletType = Type.ToString();
                                            decimal WalletBalance = resUser.TotalAmount;

                                            //****************************************
                                            //**********  COUPON VALIDATE ************
                                            //****************************************
                                            if (!string.IsNullOrEmpty(CouponCode))
                                            {
                                                if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins))
                                                {
                                                    result = "Coupons cannot be applied on MyPay Coins Payment";
                                                }
                                                else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Bank))
                                                {
                                                    result = "Coupons cannot be applied on Bank Payment";
                                                }
                                                else
                                                {
                                                    result = Common.ValidateCoupon(resUser.MemberId, CouponCode, Type, ref resGetCouponsScratched, VendorApiType);
                                                }
                                                if (result == "success")
                                                {
                                                    decimal CouponDeduct = (Convert.ToDecimal(amount) * resGetCouponsScratched.CouponPercentage) / 100;
                                                    if (resGetCouponsScratched.MinimumAmount > 0 && CouponDeduct < resGetCouponsScratched.MinimumAmount)
                                                    {
                                                        CouponDeduct = resGetCouponsScratched.MinimumAmount;
                                                    }
                                                    else if (resGetCouponsScratched.MaximumAmount > 0 && CouponDeduct > resGetCouponsScratched.MaximumAmount)
                                                    {
                                                        CouponDeduct = resGetCouponsScratched.MaximumAmount;
                                                    }
                                                    finalamount = Convert.ToDecimal(amount) + objOut.ServiceCharge - (CouponDeduct);
                                                    if (((Type == 0 || Type == (int)WalletTransactions.WalletTypes.Wallet)) && resUser.TotalAmount < finalamount)
                                                    {
                                                        result = Common.InsufficientBalance;
                                                    }
                                                    else
                                                    {
                                                        result = "";
                                                    }
                                                }
                                            }
                                            if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Bank) && string.IsNullOrEmpty(BankTransactionId))
                                            {
                                                result = "BankTransactionId not found";
                                            }
                                            else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (Convert.ToDecimal(resUser.TotalRewardPoints) < objOut.MPCoinsDebit))
                                            {
                                                result = Common.InsufficientBalance_MPCoins;
                                            }
                                            else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (WalletBalance < (Convert.ToDecimal(objOut.NetAmount) - objOut.MPCoinsDebit)))
                                            {
                                                result = Common.InsufficientBalance;
                                            }
                                            else if ((WalletType == "0" || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.FonePay) || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Wallet)) && WalletBalance < Convert.ToDecimal(objOut.NetAmount))
                                            {
                                                result = Common.InsufficientBalance;
                                            }

                                            if ((Type == 0 || Type == (int)WalletTransactions.WalletTypes.Wallet))
                                            {
                                                finalamount = Convert.ToDecimal(amount) + objOut.ServiceCharge;
                                                if (resUser.TotalAmount < finalamount)
                                                {
                                                    result = Common.InsufficientBalance;
                                                }
                                            }
                                            int hours = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Hour;
                                            int Minutes = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Minute;
                                            int Seconds = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Second;
                                            if (hours == 23 || hours == 0)
                                            {
                                                if (Minutes > 58 || (hours == 0 && Minutes <= 5))
                                                {
                                                    if ((resUser.TotalAmount > 50000))
                                                    {
                                                        result = Common.TemporaryServiceUnavailable;
                                                    }
                                                }
                                            }

                                            if (string.IsNullOrEmpty(result))
                                            {
                                                int FailedTxs = Common.GetFailedTxn(resUser, VendorApiType);
                                                if (FailedTxs >= 5)
                                                {
                                                    if ((FailedTxs % 5) == 0)
                                                    {
                                                        int LastTransactionStatus = Common.GetLastTxnStatus(resUser, VendorApiType);
                                                        if (LastTransactionStatus != 1)
                                                        {
                                                            int MinutesFailedTxn = Common.GetLastFailedTxn(resUser, VendorApiType);
                                                            if (MinutesFailedTxn < 30)
                                                            {
                                                                result = Common.FailedTxnLimit;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (string.IsNullOrEmpty(result))
                                            {
                                                int FailedTxs = Common.GetDepulicateRequestCheck(resUser, VendorApiType, UserInput);
                                                if (FailedTxs > 0)
                                                {
                                                    Common.AddLogs($"Transaction Not Initiated. Please try again. {UserInput} ", false, (int)AddLog.LogType.DBLogs);
                                                    result = "Transaction Not Initiated. Please try again.";
                                                }
                                                else
                                                {
                                                    FailedTxs = Common.GetDepulicateTransactionRequestCheck(resUser, VendorApiType, CustomerID, amount, WalletType);
                                                    if (FailedTxs > 0)
                                                    {
                                                        Common.AddLogs($"Transaction Failed To Initiate. Please try again. {UserInput} ", false, (int)AddLog.LogType.DBLogs);
                                                        result = $"Transaction Failed To Initiate As Similar Transaction Found With Same Amount. Please Try Again After {Common.ApplicationEnvironment.DuplicateMinutesCheck} Mins.";
                                                    }
                                                }
                                            }
                                        }
                                        if (checkpin)
                                        {
                                            int AttemptedCount = 0;
                                            DateTime AttemptedDatetime = DateTime.UtcNow;
                                            string originalFileNameForPin = Common.EncryptString(Convert.ToString(resUser.MemberId)).Replace("/", "@@@");
                                            string filepathForPin = HttpContext.Current.Server.MapPath("/LoginAttempt/" + originalFileNameForPin + ".txt");
                                            if (System.IO.File.Exists(filepathForPin))
                                            {
                                                string JsonForPin = (File.ReadAllText(filepathForPin));
                                                if (!string.IsNullOrEmpty(JsonForPin))
                                                {
                                                    CommonLoginAttempt onCommonLoginAttempt = JsonConvert.DeserializeObject<CommonLoginAttempt>(JsonForPin);
                                                    AttemptedCount = Convert.ToInt32(onCommonLoginAttempt.AttemptedCount);
                                                    AttemptedDatetime = Convert.ToDateTime(onCommonLoginAttempt.AttemptedDatetime);
                                                    if (AttemptedCount >= 5)
                                                    {
                                                        if ((System.DateTime.UtcNow - AttemptedDatetime).TotalHours < 2)
                                                        {
                                                            result = "Your account is blocked for 2 hours due to wrong pin attempts. Please try again later.";
                                                        }
                                                        else if (string.IsNullOrEmpty(mpin) || Common.Decryption(mpin) != Common.DecryptString(resUser.Pin))
                                                        {
                                                            InvalidPinUserUpdate(resUser.MemberId);
                                                            result = Common.SessionExpired;
                                                        }
                                                        else
                                                        {
                                                            AttemptedCount = 0;
                                                            string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                                                            System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);
                                                        }
                                                    }
                                                }
                                            }
                                            if (result == "")
                                            {
                                                if (string.IsNullOrEmpty(resUser.Pin))
                                                {
                                                    result = Common.SetYourPin;
                                                }
                                                else if (string.IsNullOrEmpty(mpin) || Common.Decryption(mpin) != Common.DecryptString(resUser.Pin))
                                                {
                                                    if (!System.IO.File.Exists(filepathForPin))
                                                    {
                                                        var LoginAttemptFile = System.IO.File.Create(filepathForPin);
                                                        LoginAttemptFile.Close();
                                                        LoginAttemptFile.Dispose();
                                                    }
                                                    else
                                                    {
                                                        string JsonForPin = (File.ReadAllText(filepathForPin));
                                                        if (!string.IsNullOrEmpty(JsonForPin))
                                                        {
                                                            CommonLoginAttempt onCommonLoginAttempt = JsonConvert.DeserializeObject<CommonLoginAttempt>(JsonForPin);
                                                            AttemptedCount = Convert.ToInt32(onCommonLoginAttempt.AttemptedCount) + 1;
                                                        }
                                                    }
                                                    if (AttemptedCount <= 5)
                                                    {
                                                        string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                                                        System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);
                                                    }
                                                    result = Common.Invalidpin;
                                                    Common.AddLogs($"Transaction {Common.Invalidpin} : {VendorApiTypeName} For Rs. {amount} ", false, (int)AddLog.LogType.User, resUser.MemberId, resUser.FirstName, true);

                                                }
                                                else
                                                {
                                                    AttemptedCount = 0;
                                                    string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                                                    System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);

                                                }
                                            }
                                        }
                                        if (result == "")
                                        {
                                            //resUser.TokenUpdatedTime = DateTime.UtcNow.AddMinutes(5);
                                            //RepCRUD<AddUser, GetUser>.Update(resUser, "user");
                                            //string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(15).ToString("dd-MMM-yyyy HH:mm:ss");
                                            string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["APISessionTimeout"])).ToString("dd-MMM-yyyy HH:mm:ss");
                                            //NewTokenUpdatedTime = Common.EncryptString(NewTokenUpdatedTime);
                                            System.IO.File.WriteAllText(filepath, NewTokenUpdatedTime);

                                            result = "Success";
                                        }
                                    }
                                }
                                else
                                {
                                    result = Common.Invalidusertoken;
                                }
                            }
                            else
                            {
                                result = Common.SessionExpired;
                            }

                        }
                        else
                        {
                            result = Common.Invalidusertoken;
                        }
                    }
                    else
                    {
                        result = Common.Invalidusertoken;
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                if (result.ToLower().Contains("the process cannot access the file"))
                {
                    if (VendorApiType != 0)
                    {
                        Common.AddLogs($"Exception:{ex.Message} For VendorAPIType {VendorApiType}", true, (int)AddLog.LogType.DBLogs);
                        result = Common.TemporaryErrorMessage;
                    }
                    else
                    {
                        result = "success";
                    }
                }
            }
            return resUser;
        }

        public AddUserLoginWithPin CheckUserDetailNCHL(string CustomerID, string UserInput, string ReferenceNo, string BankTransactionId, ref AddCouponsScratched resGetCouponsScratched, string CouponCode, string DeviceId, ref string result, int Type, int VendorApiType, Int64 memId, bool CheckInsuuficientBalance, bool checkamount, string amount, bool checkpin, string mpin, string Check = "", bool notcheckMemberId = false, bool forceGetUserDtl = false)
        {
            AddUserLoginWithPin resUser = new AddUserLoginWithPin();
            try
            {
               
               
                    decimal finalamount = 0;
                    
                 

                            AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                  
                            inobjectUser.DeviceId = DeviceId;
                            inobjectUser.MemberId = memId;
                            resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                            if (resUser.Id > 0)
                            {
                                string originalFileName = Common.EncryptString(resUser.ContactNumber).Replace("/", "@@@");

                                string TokenUpdatedTime = string.Empty;
                                string filepath = HttpContext.Current.Server.MapPath("/UserCodes/" + originalFileName + ".txt");
                                if (!System.IO.File.Exists(filepath))
                                {
                                    result = Common.Invalidusertoken;
                                    return resUser;
                                }
                                if (resUser.IsActive == false)
                                {
                                    result = Common.InactiveUserMessage;
                                    return resUser;
                                }
                                
                                    if (!notcheckMemberId && memId.ToString() != resUser.MemberId.ToString())
                                    {
                                        result = Common.Invalidusertoken;
                                    }
                                    else
                                    {
                                        
                                            AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(resUser.MemberId.ToString(), amount, VendorApiType.ToString());
                                            string WalletType = Type.ToString();
                                            decimal WalletBalance = resUser.TotalAmount;

                                            //****************************************
                                            //**********  COUPON VALIDATE ************
                                            //****************************************
                                            if (!string.IsNullOrEmpty(CouponCode))
                                            {
                                                if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins))
                                                {
                                                    result = "Coupons cannot be applied on MyPay Coins Payment";
                                                }
                                                else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Bank))
                                                {
                                                    result = "Coupons cannot be applied on Bank Payment";
                                                }
                                                else
                                                {
                                                    result = Common.ValidateCoupon(resUser.MemberId, CouponCode, Type, ref resGetCouponsScratched, VendorApiType);
                                                }
                                                if (result == "success")
                                                {
                                                    decimal CouponDeduct = (Convert.ToDecimal(amount) * resGetCouponsScratched.CouponPercentage) / 100;
                                                    if (resGetCouponsScratched.MinimumAmount > 0 && CouponDeduct < resGetCouponsScratched.MinimumAmount)
                                                    {
                                                        CouponDeduct = resGetCouponsScratched.MinimumAmount;
                                                    }
                                                    else if (resGetCouponsScratched.MaximumAmount > 0 && CouponDeduct > resGetCouponsScratched.MaximumAmount)
                                                    {
                                                        CouponDeduct = resGetCouponsScratched.MaximumAmount;
                                                    }
                                                    finalamount = Convert.ToDecimal(amount) + objOut.ServiceCharge - (CouponDeduct);
                                                    if (((Type == 0 || Type == (int)WalletTransactions.WalletTypes.Wallet)) && resUser.TotalAmount < finalamount)
                                                    {
                                                        result = Common.InsufficientBalance;
                                                    }
                                                    else
                                                    {
                                                        result = "";
                                                    }
                                                }
                                            }
                                            if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Bank) && string.IsNullOrEmpty(BankTransactionId))
                                            {
                                                result = "BankTransactionId not found";
                                            }
                                            else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (Convert.ToDecimal(resUser.TotalRewardPoints) < objOut.MPCoinsDebit))
                                            {
                                                result = Common.InsufficientBalance_MPCoins;
                                            }
                                            else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (WalletBalance < (Convert.ToDecimal(objOut.NetAmount) - objOut.MPCoinsDebit)))
                                            {
                                                result = Common.InsufficientBalance;
                                            }
                                            else if ((WalletType == "0" || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.FonePay) || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Wallet)) && WalletBalance < Convert.ToDecimal(objOut.NetAmount))
                                            {
                                                result = Common.InsufficientBalance;
                                            }

                                            if ((Type == 0 || Type == (int)WalletTransactions.WalletTypes.Wallet))
                                            {
                                                finalamount = Convert.ToDecimal(amount) + objOut.ServiceCharge;
                                                if (resUser.TotalAmount < finalamount)
                                                {
                                                    result = Common.InsufficientBalance;
                                                }
                                            }
                                            int hours = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Hour;
                                            int Minutes = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Minute;
                                            int Seconds = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Second;
                                            if (hours == 23 || hours == 0)
                                            {
                                                if (Minutes > 58 || (hours == 0 && Minutes <= 5))
                                                {
                                                    if ((resUser.TotalAmount > 50000))
                                                    {
                                                        result = Common.TemporaryServiceUnavailable;
                                                    }
                                                }
                                            }

                                            if (string.IsNullOrEmpty(result))
                                            {
                                                int FailedTxs = Common.GetFailedTxn(resUser, VendorApiType);
                                                if (FailedTxs >= 5)
                                                {
                                                    if ((FailedTxs % 5) == 0)
                                                    {
                                                        int LastTransactionStatus = Common.GetLastTxnStatus(resUser, VendorApiType);
                                                        if (LastTransactionStatus != 1)
                                                        {
                                                            int MinutesFailedTxn = Common.GetLastFailedTxn(resUser, VendorApiType);
                                                            if (MinutesFailedTxn < 30)
                                                            {
                                                                result = Common.FailedTxnLimit;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (string.IsNullOrEmpty(result))
                                            {
                                                int FailedTxs = Common.GetDepulicateRequestCheck(resUser, VendorApiType, UserInput);
                                                if (FailedTxs > 0)
                                                {
                                                    Common.AddLogs($"Transaction Not Initiated. Please try again. {UserInput} ", false, (int)AddLog.LogType.DBLogs);
                                                    result = "Transaction Not Initiated. Please try again.";
                                                }
                                                else
                                                {
                                                    FailedTxs = Common.GetDepulicateTransactionRequestCheck(resUser, VendorApiType, CustomerID, amount, WalletType);
                                                    if (FailedTxs > 0)
                                                    {
                                                        Common.AddLogs($"Transaction Failed To Initiate. Please try again. {UserInput} ", false, (int)AddLog.LogType.DBLogs);
                                                        result = $"Transaction Failed To Initiate As Similar Transaction Found With Same Amount. Please Try Again After {Common.ApplicationEnvironment.DuplicateMinutesCheck} Mins.";
                                                    }
                                                }
                                            }
                                     
                                       
                                        if (result == "")
                                        {
                                            //resUser.TokenUpdatedTime = DateTime.UtcNow.AddMinutes(5);
                                            //RepCRUD<AddUser, GetUser>.Update(resUser, "user");
                                            //string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(15).ToString("dd-MMM-yyyy HH:mm:ss");
                                            //NewTokenUpdatedTime = Common.EncryptString(NewTokenUpdatedTime);
                                            //System.IO.File.WriteAllText(filepath, NewTokenUpdatedTime);

                                            result = "Success";
                                        }
                                    }
                                
                            }
                            else
                            {
                                result = Common.SessionExpired;
                            }

                        
                 
            }
            catch (Exception ex)
            {
                result = ex.Message;
                if (result.ToLower().Contains("the process cannot access the file"))
                {
                    if (VendorApiType != 0)
                    {
                        Common.AddLogs($"Exception:{ex.Message} For VendorAPIType {VendorApiType}", true, (int)AddLog.LogType.DBLogs);
                        result = Common.TemporaryErrorMessage;
                    }
                    else
                    {
                        result = "success";
                    }
                }
            }
            return resUser;
        }


        public static string getHashMD5(string JSONInput)
        {

            //var jsonObject = JsonConvert.DeserializeObject(JSONInput);
            string concatenated = "";

            JObject jsonObject = JObject.Parse(JSONInput);

            foreach (var item in jsonObject)
            {
                if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()) && item.Key.ToLower() != "hash" &&
                        item.Key.ToLower() != "passengersclassstring" && item.Key.ToLower() != "playerlist" && item.Key.ToLower() != "bankdata" &&
                        item.Key.ToLower() != "vendorjsonlookup")
                {
                    if (item.Key.ToLower() == "mpin")
                    {
                        concatenated += Common.Decryption(item.Value.ToString());
                    }
                    else
                    {
                        concatenated += item.Value.ToString();
                    }
                }

            }

            string result = "";
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(concatenated));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();

            return result;
        }

        public CableCommon AddResponseCable(string RefernceNo, string transactionId, int ResponseCode)
        {
            CableCommon cableCommon = new CableCommon();

            CableCommon Outobject = new CableCommon();
            GetCommonCable InObject = new GetCommonCable();
            InObject.TransactionId = transactionId;
            InObject.ResponseCode = ResponseCode.ToString();
            InObject.ReferenceNumber = RefernceNo;
            Int64 Id = RepCRUD<GetCommonCable, CableCommon>.Insert(InObject, "Cable_Car");
            return cableCommon;

        }
        static string TrimOutsideBraces(string jsonString)
        {
            if (jsonString.StartsWith("{") && jsonString.EndsWith("}"))
            {
                int startIndex = 1;
                int endIndex = jsonString.Length - 1;

                if (startIndex < endIndex)
                {
                    return jsonString.Substring(startIndex, endIndex - startIndex);
                }
            }

            return jsonString;
        }


        public TicketInvoiceCommon AddTicketInvoiceCable(object ticket, Invoiceresponse invoiceResponse, string TransactionID, string MemberId, string msg)
        {


            TicketInvoiceCommon CC = new TicketInvoiceCommon();
            var data = ticket.ToString();
            List<TicketResponse> tickets = JsonConvert.DeserializeObject<List<TicketResponse>>(data);
            List<TicketResponse> response = new List<TicketResponse>();
            foreach (var item in tickets)
            {
                TicketInvoiceCommon Outobject = new TicketInvoiceCommon();
                GetTicketInvoiceCommon InObject = new GetTicketInvoiceCommon();
                TicketResponse tips = new TicketResponse();
                InObject.BarCode = item.BarCode;
                InObject.QRCode = item.QRCode;
                InObject.TripType = item.TripType;
                InObject.PassengerType = item.PassengerType;
                InObject.TicketNumber = item.TicketNumber;
                InObject.ValidUntil = item.ValidUntil;
                InObject.ReferenceId = invoiceResponse.ReferenceId;
                InObject.TicketMessage = invoiceResponse.TicketMessage;
                InObject.Username = invoiceResponse.UserName;
                InObject.VatBillMessage = invoiceResponse.VatBillMessage;
                InObject.BasePrice = invoiceResponse.BasePrice.ToString();
                InObject.VatTax = invoiceResponse.VatTax.ToString();
                InObject.TotalAmount = invoiceResponse.TotalAmount.ToString();
                Int64 Id = RepCRUD<GetTicketInvoiceCommon, TicketInvoiceCommon>.Insert(InObject, "CableCar_TicketInVoice");
                response.Add(tips);
            }
            walletTransactionDetail walletDetail = null;
            ReceiptsVendorResponse objreceiptsVendorResponse = null;
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Cable_Car;
            objreceiptsVendorResponse = VendorApi_CommonHelper.getUserDetails(MemberId);
            walletDetail = VendorApi_CommonHelper.GetWalletDetail(TransactionID);
            //VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "Cable_Car", MemberId, TransactionID, msg, objreceiptsVendorResponse.ContactNumber, objreceiptsVendorResponse.FullName, "Cable Car", "Cable Car", invoiceResponse.TotalAmount.ToString());
            //GET SERVICE NAME
            //string localServiceName = getLocalServiceForNCHLGovtService(resObj.cipsTransactionDetail.appId);

            var list = new List<KeyValuePair<String, String>>();

            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", DateTime.Now.ToString());
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Type", "Ticketing");
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Service", "Annapurna Cable Car");
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Voucher Number", TransactionID);
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Customer Name", invoiceResponse.UserName);
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", "Success");
            VendorApi_CommonHelper.addKeyValueToList(ref list, "CashBack(GREEN)", walletDetail.CashBack);
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Service Charge(RED)", walletDetail.ServiceCharge);
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", invoiceResponse.TotalAmount.ToString());
            VendorApi_CommonHelper.addKeyValueToList(ref list, "Remarks", "Cablecar payment of Rs." + invoiceResponse.TotalAmount.ToString() + " paid successfully for " + TransactionID + ".");

            string JSONForReceipt = VendorApi_CommonHelper.getJSONfromList(list);

            VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "Cable_Car", MemberId, TransactionID, JSONForReceipt, objreceiptsVendorResponse.ContactNumber, objreceiptsVendorResponse.FullName, "Annapurna Cable Car", invoiceResponse.ReferenceId, invoiceResponse.TotalAmount.ToString());
            VendorApi_CommonHelper.saveExtraReceiptData(TransactionID, msg);


            return null;

        }
        static string RemoveOuterQuotes(string input)
        {
            if (input.StartsWith("{\"") && input.EndsWith("\"}"))
            {
                return input.Substring(1, input.Length - 2);
            }
            else
            {
                throw new ArgumentException("Input format is not valid.");
            }
        }


        public AddUser CheckUserDetailOld(ref AddCouponsScratched resGetCouponsScratched, string CouponCode, string DeviceId, ref string result, int Type, int VendorApiType, Int64 memId, bool CheckInsuuficientBalance, bool checkamount, string amount, bool checkpin, string mpin, string Check = "", bool notcheckMemberId = false, bool forceGetUserDtl = false)
        {
            AddUser resUser = new AddUser();
            try
            {
                if (!CheckInsuuficientBalance && !checkamount && !checkpin && !forceGetUserDtl)
                {
                    result = "Success";
                    return resUser;
                }
                if (checkamount)
                {
                    if (!string.IsNullOrEmpty(amount))
                    {
                        decimal Num;
                        bool isNum = decimal.TryParse(amount, out Num);
                        if (!isNum)
                        {
                            result = "Please enter valid amount.";
                        }
                        else if (Convert.ToDecimal(amount) <= 0)
                        {
                            result = "Please enter amount greater than zero.";
                        }
                    }
                    else
                    {
                        result = "Please enter amount.";
                    }
                }
                if (string.IsNullOrEmpty(DeviceId))
                {
                    result = "Please enter DeviceId";
                }
                if (Check == "")
                {
                    decimal finalamount = 0;
                    if (HttpContext.Current.Request.Headers.GetValues("UToken") != null)
                    {
                        string secretkey = HttpContext.Current.Request.Headers.GetValues("UToken").First();
                        string VendorApiTypeName = string.Empty;

                        //VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").ToString();

                        if (!string.IsNullOrEmpty(secretkey))
                        {

                            AddUser outobjectUser = new AddUser();
                            GetUser inobjectUser = new GetUser();
                            inobjectUser.JwtToken = secretkey;
                            inobjectUser.DeviceId = DeviceId;
                            resUser = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_GetAdmin, inobjectUser, outobjectUser);
                            if (resUser.Id > 0)
                            {
                                string originalFileName = Common.EncryptString(resUser.ContactNumber).Replace("/", "@@@");
                                string TokenUpdatedTime = string.Empty;
                                string filepath = HttpContext.Current.Server.MapPath("/UserCodes/" + originalFileName + ".txt");
                                if (!System.IO.File.Exists(filepath))
                                {
                                    //System.IO.File.Create(filepath).Dispose();
                                    //string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(5).ToString("dd-MMM-yyyy HH:mm:ss");
                                    //NewTokenUpdatedTime = Common.EncryptString(NewTokenUpdatedTime);
                                    //System.IO.File.WriteAllText(filepath, NewTokenUpdatedTime);
                                    result = Common.Invalidusertoken;
                                    return resUser;
                                }

                                if (resUser.IsActive == false)
                                {
                                    result = Common.InactiveUserMessage;
                                    return resUser;
                                }
                                //TokenUpdatedTime = Common.DecryptString(File.ReadAllText(filepath));
                                TokenUpdatedTime = (File.ReadAllText(filepath));
                                //TokenUpdatedTime = Convert.ToDateTime(TokenUpdatedTime).ToString("dd-MMM-yyyy HH:mm:ss");
                                if (!string.IsNullOrEmpty(TokenUpdatedTime) && DateTime.UtcNow < Convert.ToDateTime(TokenUpdatedTime))
                                {
                                    if (!notcheckMemberId && memId.ToString() != resUser.MemberId.ToString())
                                    {
                                        result = Common.Invalidusertoken;
                                    }
                                    else
                                    {
                                        if (CheckInsuuficientBalance)
                                        {
                                            AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(resUser.MemberId.ToString(), amount, VendorApiType.ToString());
                                            string WalletType = Type.ToString();
                                            decimal WalletBalance = resUser.TotalAmount;

                                            //****************************************
                                            //**********  COUPON VALIDATE ************
                                            //****************************************
                                            if (!string.IsNullOrEmpty(CouponCode))
                                            {
                                                if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins))
                                                {
                                                    result = "Coupons cannot be applied on MyPay Coins Payment";
                                                }
                                                else
                                                {
                                                    result = Common.ValidateCoupon(resUser.MemberId, CouponCode, Type, ref resGetCouponsScratched, VendorApiType);
                                                }
                                                if (result == "success")
                                                {
                                                    decimal CouponDeduct = (Convert.ToDecimal(amount) * resGetCouponsScratched.CouponPercentage) / 100;
                                                    if (resGetCouponsScratched.MinimumAmount > 0 && CouponDeduct < resGetCouponsScratched.MinimumAmount)
                                                    {
                                                        CouponDeduct = resGetCouponsScratched.MinimumAmount;
                                                    }
                                                    else if (resGetCouponsScratched.MaximumAmount > 0 && CouponDeduct > resGetCouponsScratched.MaximumAmount)
                                                    {
                                                        CouponDeduct = resGetCouponsScratched.MaximumAmount;
                                                    }
                                                    finalamount = Convert.ToDecimal(amount) + objOut.ServiceCharge - (CouponDeduct);
                                                    if (((Type == 0 || Type == (int)WalletTransactions.WalletTypes.Wallet)) && resUser.TotalAmount < finalamount)
                                                    {
                                                        result = Common.InsufficientBalance;
                                                    }
                                                    else
                                                    {
                                                        result = "";
                                                    }
                                                }
                                            }


                                            if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (Convert.ToDecimal(resUser.TotalRewardPoints) < objOut.MPCoinsDebit))
                                            {
                                                result = Common.InsufficientBalance_MPCoins;
                                            }
                                            else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (WalletBalance < (Convert.ToDecimal(objOut.NetAmount) - objOut.MPCoinsDebit)))
                                            {
                                                result = Common.InsufficientBalance;
                                            }
                                            else if ((WalletType == "0" || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.FonePay) || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Wallet)) && WalletBalance < Convert.ToDecimal(objOut.NetAmount))
                                            {
                                                result = Common.InsufficientBalance;
                                            }
                                            if ((Type == 0 || Type == (int)WalletTransactions.WalletTypes.Wallet))
                                            {
                                                finalamount = Convert.ToDecimal(amount) + objOut.ServiceCharge;
                                                if (resUser.TotalAmount < finalamount)
                                                {
                                                    result = Common.InsufficientBalance;
                                                }
                                            }


                                            if (string.IsNullOrEmpty(result))
                                            {
                                                int FailedTxs = Common.GetFailedTxnOld(resUser, VendorApiType);
                                                if (FailedTxs >= 5)
                                                {
                                                    if ((FailedTxs % 5) == 0)
                                                    {
                                                        int LastTransactionStatus = Common.GetLastTxnStatusOld(resUser, VendorApiType);
                                                        if (LastTransactionStatus != 1)
                                                        {
                                                            int Minutes = Common.GetLastFailedTxnOld(resUser, VendorApiType);
                                                            if (Minutes < 30)
                                                            {
                                                                result = Common.FailedTxnLimit;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (checkpin)
                                        {
                                            int AttemptedCount = 0;
                                            DateTime AttemptedDatetime = DateTime.UtcNow;
                                            string originalFileNameForPin = Common.EncryptString(Convert.ToString(resUser.MemberId)).Replace("/", "@@@");
                                            string filepathForPin = HttpContext.Current.Server.MapPath("/LoginAttempt/" + originalFileNameForPin + ".txt");
                                            if (System.IO.File.Exists(filepathForPin))
                                            {
                                                string JsonForPin = (File.ReadAllText(filepathForPin));
                                                if (!string.IsNullOrEmpty(JsonForPin))
                                                {
                                                    CommonLoginAttempt onCommonLoginAttempt = JsonConvert.DeserializeObject<CommonLoginAttempt>(JsonForPin);
                                                    AttemptedCount = Convert.ToInt32(onCommonLoginAttempt.AttemptedCount);
                                                    AttemptedDatetime = Convert.ToDateTime(onCommonLoginAttempt.AttemptedDatetime);
                                                    if (AttemptedCount >= 5)
                                                    {
                                                        if ((System.DateTime.UtcNow - AttemptedDatetime).TotalHours < 2)
                                                        {
                                                            result = "Your account is blocked for 2 hours due to wrong pin attempts. Please try again later.";
                                                        }
                                                        else if (string.IsNullOrEmpty(mpin) || Common.Decryption(mpin) != Common.DecryptString(resUser.Pin))
                                                        {
                                                            InvalidPinUserUpdate(resUser.MemberId);
                                                            result = Common.SessionExpired;
                                                        }
                                                        else
                                                        {
                                                            AttemptedCount = 0;
                                                            string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                                                            System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);
                                                        }
                                                    }
                                                }
                                            }
                                            if (result == "")
                                            {
                                                if (string.IsNullOrEmpty(resUser.Pin))
                                                {
                                                    result = Common.SetYourPin;
                                                }
                                                else if (string.IsNullOrEmpty(mpin) || Common.Decryption(mpin) != Common.DecryptString(resUser.Pin))
                                                {
                                                    if (!System.IO.File.Exists(filepathForPin))
                                                    {
                                                        var LoginAttemptFile = System.IO.File.Create(filepathForPin);
                                                        LoginAttemptFile.Close();
                                                        LoginAttemptFile.Dispose();
                                                    }
                                                    else
                                                    {
                                                        string JsonForPin = (File.ReadAllText(filepathForPin));
                                                        if (!string.IsNullOrEmpty(JsonForPin))
                                                        {
                                                            CommonLoginAttempt onCommonLoginAttempt = JsonConvert.DeserializeObject<CommonLoginAttempt>(JsonForPin);
                                                            AttemptedCount = Convert.ToInt32(onCommonLoginAttempt.AttemptedCount) + 1;
                                                        }
                                                    }
                                                    if (AttemptedCount <= 5)
                                                    {
                                                        string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                                                        System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);
                                                    }
                                                    result = Common.Invalidpin;
                                                    Common.AddLogs(Common.Invalidpin, false, (int)AddLog.LogType.User, resUser.MemberId, resUser.FirstName, true);

                                                }
                                                else
                                                {

                                                    // Deduct Wallet
                                                }
                                            }
                                        }
                                        if (result == "")
                                        {
                                            //resUser.TokenUpdatedTime = DateTime.UtcNow.AddMinutes(5);
                                            //RepCRUD<AddUser, GetUser>.Update(resUser, "user");
                                            //string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(15).ToString("dd-MMM-yyyy HH:mm:ss");
                                            string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["APISessionTimeout"])).ToString("dd-MMM-yyyy HH:mm:ss");

                                            //NewTokenUpdatedTime = Common.EncryptString(NewTokenUpdatedTime);
                                            System.IO.File.WriteAllText(filepath, NewTokenUpdatedTime);

                                            result = "Success";
                                        }
                                    }
                                }
                                else
                                {
                                    result = Common.Invalidusertoken;
                                }
                            }
                            else
                            {
                                result = Common.SessionExpired;
                            }

                        }
                        else
                        {
                            result = Common.Invalidusertoken;
                        }
                    }
                    else
                    {
                        result = Common.Invalidusertoken;
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                if (result.ToLower().Contains("the process cannot access the file"))
                {
                    if (VendorApiType != 0)
                    {
                        Common.AddLogs($"Exception:{ex.Message} For VendorAPIType {VendorApiType}", true, (int)AddLog.LogType.DBLogs);
                        result = Common.TemporaryErrorMessage;
                    }
                    else
                    {
                        result = "success";
                    }
                }
            }
            return resUser;
        }

        public List<SelectListItem> BindSelectedListDefaultValues(List<SelectListItem> currentlist, string v)
        {
            try
            {
                currentlist.Find(c => c.Value == v).Selected = true;
            }
            catch (Exception ex)
            {

            }
            return currentlist;
        }
        public static List<SelectListItem> GetSelectList_CouponKycType()
        {
            var Couponlist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddCoupons.KycStatussEnum)))
            {
                string stringValue = Enum.GetName(typeof(AddCoupons.KycStatussEnum), value).Replace("_", " ");
                Couponlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return Couponlist;
        }
        public static List<SelectListItem> GetSelectList_CouponGenderType()
        {
            var Couponlist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddCoupons.GenderStatussEnum)))
            {
                string stringValue = Enum.GetName(typeof(AddCoupons.GenderStatussEnum), value).Replace("_", " ");
                Couponlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return Couponlist;
        }

        public static List<SelectListItem> GetSelectList_CouponApplyType()
        {
            var Couponlist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddCoupons.CouponReceivedBy)))
            {
                string stringValue = Enum.GetName(typeof(AddCoupons.CouponReceivedBy), value).Replace("_", " ");
                Couponlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return Couponlist;
        }
        public static List<SelectListItem> GetSelectList_TxnSign()
        {
            var signlist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(WalletTransactions.Signs)))
            {
                string stringValue = Enum.GetName(typeof(WalletTransactions.Signs), value).Replace("_", " ");
                signlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }
            return CreateDropdown("", signlist, "Select Sign");
            //return signlist;
        }

        public static List<SelectListItem> GetSelectList_TxnStatus()
        {
            var statuslist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(WalletTransactions.Statuses)))
            {
                string stringValue = Enum.GetName(typeof(WalletTransactions.Statuses), value).Replace("_", " ");
                statuslist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }
            return CreateDropdown("", statuslist, "Select Status");
            //return signlist;
        }
        public static List<SelectListItem> GetSelectList_RemittanceType(AddRemittanceUser model)
        {
            var type = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddRemittanceUser.RemittanceType)))
            {
                string stringValue = Enum.GetName(typeof(AddRemittanceUser.RemittanceType), value).Replace("_", " ");
                if (value == model.Type)
                {
                    type.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    type.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }
            return type;
        }
        public static List<SelectListItem> GetSelectList_DefaultsCommission()
        {
            var defaultList = new List<SelectListItem>();
            defaultList.Add(new SelectListItem { Text = "Commission Type", Value = "2", Selected = true });
            foreach (int value in Enum.GetValues(typeof(AddMerchantCommission.Default)))
            {
                string stringValue = Enum.GetName(typeof(AddMerchantCommission.Default), value).Replace("_", " ");
                defaultList.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return defaultList;
        }
        public bool InvalidPinUserUpdate(Int64 MemberId)
        {
            bool data = false;
            try
            {
                if (MemberId != 0)
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"exec sp_Users_InvalidPin_Update '{MemberId}'";
                    Result = obj.GetScalarValueWithValue(str);
                    if (!string.IsNullOrEmpty(Result) && Result != "0")
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public bool UserBankPrimaryUpdate(Int64 MemberId, Int64 CreatedBy, string CreatedByName)
        {
            bool data = false;
            try
            {
                if (MemberId != 0)
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"exec sp_Users_BankPrimary_Update '{MemberId}', '{CreatedBy}', '{CreatedByName}'";
                    Result = obj.GetScalarValueWithValue(str);
                    if (!string.IsNullOrEmpty(Result) && Result != "0")
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public bool SaveRecentTransaction(Int64 MemberId, Int32 Type, string TransactionId)
        {
            bool data = false;
            try
            {
                if (MemberId != 0)
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"exec sp_Users_RecentTransaction_Update '{MemberId}', '{Type}', '{TransactionId}'";
                    Result = obj.GetScalarValueWithValue(str);
                    if (!string.IsNullOrEmpty(Result) && Result != "0")
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public bool GetMerchantDuplicateReferenceTransaction(Int64 MerchantMemberid, string Reference)
        {
            bool data = false;
            try
            {
                if (MerchantMemberid != 0)
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"select Count(0) from WalletTransactions with(nolock)  where MerchantMemberid = '{MerchantMemberid}' and Reference= '{Reference}'";
                    Result = obj.GetScalarValueWithValue(str);
                    if (Result != "0")
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public bool GetFlightLogsIssuedStatus(Int64 Memberid, string BookingID)
        {
            bool data = false;
            try
            {
                if (Memberid != 0)
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"select count(0) from FlightBookingDetails  with(nolock) where Memberid = '{Memberid}' and BookingId =  '{BookingID}' and IsFlightBooked = 1  and (LogIDs != '' OR IsFlightIssued = 1) ";
                    Result = obj.GetScalarValueWithValue(str);
                    if (Result != "0")
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

 
        public bool GetDuplicateReferenceParameterFromMerchant(Int64 UserMemberId, string Reference, Int32 Type)
        {
            bool data = false;
            try
            {
                if (UserMemberId != 0)
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"select Count(0) from Vendor_API_Requests with(nolock)  where MemberId = '{UserMemberId}' and Req_ReferenceNo= '{Reference}' ";
                    Result = obj.GetScalarValueWithValue(str);
                    if (Convert.ToInt32(Result) > 1)
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public bool GetRemittanceDuplicateReferenceParameter(string MerchantUniqueId, string Reference, Int32 Type)
        {
            bool data = false;
            try
            {
                if (!string.IsNullOrEmpty(MerchantUniqueId))
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"select Count(0) from Remittance_API_Requests  with(nolock) where MerchantUniqueId = '{MerchantUniqueId}' and Req_ReferenceNo= '{Reference}' ";
                    Result = obj.GetScalarValueWithValue(str);
                    if (Convert.ToInt32(Result) > 1)
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public string GetMerchantPrivateKey(string MerchantUniqueId)
        {
            string Result = "";
            try
            {
                if (!string.IsNullOrEmpty(MerchantUniqueId))
                {
                    CommonHelpers obj = new CommonHelpers();
                    string str = $"exec sp_GetPrivateKey_Merchant '{MerchantUniqueId}'";
                    Result = obj.GetScalarValueWithValue(str);
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }


        public string GetMerchantWalletBalance(string MerchantUserMemberId)
        {
            string Result = "";
            try
            {
                if (!string.IsNullOrEmpty(MerchantUserMemberId))
                {
                    CommonHelpers obj = new CommonHelpers();
                    string str = $"Select TotalAmount From Users where MemberId =  '{MerchantUserMemberId}'";
                    Result = obj.GetScalarValueWithValue(str);
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public string GetMerchantPublicKey(string MerchantUniqueId)
        {
            string Result = "";
            try
            {
                if (!string.IsNullOrEmpty(MerchantUniqueId))
                {
                    CommonHelpers obj = new CommonHelpers();
                    string str = $"exec sp_GetPublicKey_Merchant '{MerchantUniqueId}'";
                    Result = obj.GetScalarValueWithValue(str);
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public string GetRemittancePrivateKey(string MerchantUniqueId)
        {
            string Result = "";
            try
            {
                if (!string.IsNullOrEmpty(MerchantUniqueId))
                {
                    CommonHelpers obj = new CommonHelpers();
                    string str = $"exec sp_GetPrivateKey_RemittanceUser '{MerchantUniqueId}'";
                    Result = obj.GetScalarValueWithValue(str);
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public string GetRemittancePublicKey(string MerchantUniqueId)
        {
            string Result = "";
            try
            {
                if (!string.IsNullOrEmpty(MerchantUniqueId))
                {
                    CommonHelpers obj = new CommonHelpers();
                    string str = $"exec sp_GetPublicKey_RemittanceUser '{MerchantUniqueId}'";
                    Result = obj.GetScalarValueWithValue(str);
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public bool IsSameDeviceID_Registration(string DeviceID)
        {
            bool data = false;
            try
            {
                if (DeviceID != "")
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"exec sp_Users_Validate_SameDeviceID '{DeviceID}'";
                    Result = obj.GetScalarValueWithValue(str);
                    if (!string.IsNullOrEmpty(Result) && Result != "0")
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public string GenerateUniqueId()
        {
            Guid g = Guid.NewGuid();
            return DateTime.UtcNow.ToString("ddMMyyyyhhmmssmsfffffff") + Common.RandomNumber(1111, 9999).ToString();
        }
        public string GenerateUniqueId_Limit20()
        {
            Guid g = Guid.NewGuid();
            return DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + Common.RandomNumber(1111, 9999).ToString();
        }
        public string GenerateUniqueId_NepalPay()
        {
            Guid g = Guid.NewGuid();
            return  DateTime.UtcNow.ToString("ddMMyyyyHHmmssms");
        }
        public string ExecuteProcedureGetReturnValue(string Procedure, Hashtable HT)
        {
            string result = "";
            string HTString = string.Empty;
            string HTStrings = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString());
            if (Procedure.ToLower().IndexOf("remittance") > 0)
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstringRemittance"].ToString());
            }
            SqlCommand cmd = new SqlCommand();
            string key = null;
            cmd = new SqlCommand(Procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                foreach (string key_loopVariable in HT.Keys)
                {
                    key = key_loopVariable;
                    cmd.Parameters.AddWithValue("@" + key, HT[key]);

                    HTString += $"@{key}: '{HT[key]}', ";
                   HTStrings += $"@{key}= '{HT[key]}', ";
                }
                SqlParameter retval = new SqlParameter("@OUTPUT", SqlDbType.VarChar, 200);
                retval.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(retval);
                cmd.CommandTimeout = 0;
                con.Open();
                cmd.ExecuteNonQuery();
                result = retval.Value.ToString();
            }
            catch (Exception ex)
            {
                Common.AddLogs($"Exception occured on {Common.fnGetdatetime()} in {Procedure} with Parameters as : {HTString}. Exception Details: {ex.Message}", false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                con.Close();
                cmd.Dispose();
                result = "0";
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return result;
        }

        public object Open_Conn()
        {
            object obj1 = null;
            if ((con.State != ConnectionState.Open))
            {
                con.Open();
            }
            return obj1;
        }
        public object Close_Conn()
        {
            object obj1 = null;
            if ((con.State != ConnectionState.Closed))
            {
                con.Close();
            }
            return obj1;
        }
        public DataTable GetDataFromStoredProcedure(string Procedure, Hashtable HT)
        {
            string HTString = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string key = null;
            SqlDataAdapter adp = new SqlDataAdapter();
            try
            {
                if (Procedure.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Open))
                    {
                        conRemittance.Open();
                    }
                    cmd.Connection = conRemittance;
                }
                else
                {
                    if ((conslave.State != ConnectionState.Open))
                    {
                        conslave.Open();
                    }
                    cmd.Connection = conslave;
                }

                adp.SelectCommand = cmd;

                foreach (string key_loopVariable in HT.Keys)
                {
                    key = key_loopVariable;
                    cmd.Parameters.AddWithValue("@" + key, HT[key]);
                    HTString += $"@{key}: '{HT[key]}', ";
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = Procedure;

                cmd.CommandTimeout = 0;
                adp.Fill(ds, Procedure);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    ds.Tables[0].TableName = Procedure;
                }
            }
            catch (Exception ex)
            {
                if (Procedure.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Closed))
                    {
                        conRemittance.Close();
                    }
                }
                else if ((conslave.State != ConnectionState.Closed))
                {
                    conslave.Close();
                }
                Common.AddLogs($"Exception occured on {Common.fnGetdatetime()} in {Procedure} with Parameters as : {HTString}. Exception Details: {ex.Message}", false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

            }
            finally
            {
                if (Procedure.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Closed))
                    {
                        conRemittance.Close();
                    }
                }
                else if ((conslave.State != ConnectionState.Closed))
                {
                    conslave.Close();
                }
            }
            return dt;
        }

        public static List<SelectListItem> GetSelectList_RemittanceAddressProofType(AddRemittanceUser model)
        {
            var prooftypelist = new List<SelectListItem>();
            prooftypelist.Add(new SelectListItem
            {
                Text = "Select ProofType",
                Value = "0",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(AddRemittanceUser.AddressProofTypes)))
            {
                string stringValue = Enum.GetName(typeof(AddRemittanceUser.AddressProofTypes), value).ToString().Replace("_", " ");
                if (value == model.ProofType)
                {
                    prooftypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    prooftypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return prooftypelist;
        }
        public static List<SelectListItem> GetSelectList_RemittanceWallet(string MerchantUniqueId)
        {
            List<AddRemittanceUserCurrencies> ctlist = new List<AddRemittanceUserCurrencies>();
            AddRemittanceUserCurrencies list = new AddRemittanceUserCurrencies();
            list.CheckActive = 1;
            list.CheckDelete = 0;
            list.MerchantUniqueId = MerchantUniqueId;
            DataTable dt = list.GetList();
            ctlist = (List<AddRemittanceUserCurrencies>)CommonEntityConverter.DataTableToList<AddRemittanceUserCurrencies>(dt);
            List<SelectListItem> currencyList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.CurrencyName,
                                                     Value = p.CurrencyID.ToString()
                                                 }).ToList();
            return CreateDropdown("", currencyList, "Select Currency");
        }


        public DataTable GetDataFromStoredProcedure_Remittance(string Procedure, Hashtable HT)
        {
            string HTString = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string key = null;
            SqlDataAdapter adp = new SqlDataAdapter();
            try
            {
                if ((conRemittance.State != ConnectionState.Open))
                {
                    conRemittance.Open();
                }
                cmd.Connection = conRemittance;

                adp.SelectCommand = cmd;

                foreach (string key_loopVariable in HT.Keys)
                {
                    key = key_loopVariable;
                    cmd.Parameters.AddWithValue("@" + key, HT[key]);
                    HTString += $"@{key}: '{HT[key]}', ";
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Procedure;

                cmd.CommandTimeout = 0;
                adp.Fill(ds, Procedure);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    ds.Tables[0].TableName = Procedure;
                }
            }
            catch (Exception ex)
            {
                if ((conRemittance.State != ConnectionState.Closed))
                {
                    conRemittance.Close();
                }

                Common.AddLogs($"Exception occured on {Common.fnGetdatetime()} in {Procedure} with Parameters as : {HTString}. Exception Details: {ex.Message}", false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

            }
            finally
            {
                if ((conRemittance.State != ConnectionState.Closed))
                {
                    conRemittance.Close();
                }

            }
            return dt;
        }

        public string GetScalarValueWithValue(string strQuery)
        {
            cmd = new SqlCommand();
            string vReturnValue = "";
            try
            {
                if (strQuery.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Open))
                    {
                        conRemittance.Open();
                    }
                    cmd.Connection = conRemittance;
                    cmd = new SqlCommand(strQuery, conRemittance);
                }
                else
                {
                    Open_Conn();
                    cmd.Connection = con;
                    cmd = new SqlCommand(strQuery, con);
                }
                vReturnValue = Convert.ToString(cmd.ExecuteScalar());
                return vReturnValue;
            }
            catch (Exception ex)
            {
                if (strQuery.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Closed))
                    {
                        conRemittance.Close();
                    }
                }
                else
                {
                    Close_Conn();
                }
                cmd.Dispose();
            }
            finally
            {
                if (strQuery.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Closed))
                    {
                        conRemittance.Close();
                    }
                }
                else
                {
                    Close_Conn();
                }
                cmd.Dispose();
            }
            return vReturnValue;
        }
        public List<(string, string)> GetAirlineScalarValueWithValue(string strQuery)
        {
            cmd = new SqlCommand();
            string vReturnValue = "";
            List<(string, string)> resultList = new List<(string, string)>();
            try
            {
                if (strQuery.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Open))
                    {
                        conRemittance.Open();
                    }
                    cmd.Connection = conRemittance;
                    cmd = new SqlCommand(strQuery, conRemittance);
                }
                else
                {
                    Open_Conn();
                    cmd.Connection = con;
                    cmd = new SqlCommand(strQuery, con);
                }
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultList.Add((reader[0].ToString(), reader[1].ToString())); // Assuming a single column result
                    }
                }
                vReturnValue = Convert.ToString(cmd.ExecuteScalar());
                return resultList;
            }
            catch (Exception ex)
            {
                if (strQuery.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Closed))
                    {
                        conRemittance.Close();
                    }
                }
                else
                {
                    Close_Conn();
                }
                cmd.Dispose();
            }
            finally
            {
                if (strQuery.ToLower().IndexOf("remittance") > 0)
                {
                    if ((conRemittance.State != ConnectionState.Closed))
                    {
                        conRemittance.Close();
                    }
                }
                else
                {
                    Close_Conn();
                }
                cmd.Dispose();
            }
            return resultList;
        }
        public static List<SelectListItem> GetSelectList_ParentMenu(string parentid)
        {
            List<AddMenu> stlist = new List<AddMenu>();
            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            inobject.CheckParentId = 0;
            stlist = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            List<SelectListItem> menuList = (from p in stlist.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.MenuName,
                                                 Value = p.Id.ToString()
                                             }).ToList();
            return CreateDropdown(parentid, menuList, "Select Parent Menu");
        }
        public static List<SelectListItem> GetSelectList_MenuList()
        {
            List<AddMenu> stlist = new List<AddMenu>();
            AddMenu outobject = new AddMenu();
            GetMenu inobject = new GetMenu();
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            inobject.CheckParentId = 0;
            stlist = RepCRUD<GetMenu, AddMenu>.GetRecordList(Common.StoreProcedures.sp_Menus_Get, inobject, outobject);
            List<SelectListItem> menuList = (from p in stlist.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.MenuName,
                                                 Value = p.Id.ToString()
                                             }).ToList();
            return CreateDropdown("", menuList, "Select Parent Menu");
        }

        public static List<SelectListItem> GetSelectList_RolesList()
        {
            List<AddRole> stlist = new List<AddRole>();
            AddRole outobject = new AddRole();
            GetRole inobject = new GetRole();
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            stlist = RepCRUD<GetRole, AddRole>.GetRecordList(Common.StoreProcedures.sp_Role_Get, inobject, outobject);
            List<SelectListItem> roleList = (from p in stlist.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.RoleName,
                                                 Value = p.Id.ToString()
                                             }).ToList();
            return CreateDropdown("", roleList, "Select Role");
        }
        public static List<SelectListItem> GetSelectList_VotingCompetition(string VotingCompetitionID)
        {
            List<AddVotingCompetition> stlist = new List<AddVotingCompetition>();
            AddVotingCompetition outobject = new AddVotingCompetition();
            GetVotingCompetition inobject = new GetVotingCompetition();
            inobject.CheckDelete = 0;
            stlist = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecordList(Common.StoreProcedures.sp_VotingCompetition_Get, inobject, outobject);
            List<SelectListItem> competitionList = (from p in stlist.AsEnumerable()
                                                    select new SelectListItem
                                                    {
                                                        Text = p.Title,
                                                        Value = p.Id.ToString()
                                                    }).ToList();
            return CreateDropdown(VotingCompetitionID, competitionList, "Select Competition");
        }

        public static List<SelectListItem> GetSelectList_KhaltiEnum(WalletTransaction model)
        {

            var khaltienum = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(VendorApi_CommonHelper.KhaltiAPIName)).Cast<VendorApi_CommonHelper.KhaltiAPIName>().Where(x => x != VendorApi_CommonHelper.KhaltiAPIName.khalti_School && x != VendorApi_CommonHelper.KhaltiAPIName.KYC
            && x != VendorApi_CommonHelper.KhaltiAPIName.Amount_Release_From_Admin && x != VendorApi_CommonHelper.KhaltiAPIName.Amount_Hold_By_Admin && x != VendorApi_CommonHelper.KhaltiAPIName.Device_Inactivate && x != VendorApi_CommonHelper.KhaltiAPIName.Ticket
            && x != VendorApi_CommonHelper.KhaltiAPIName.MyPay_Notification && x != VendorApi_CommonHelper.KhaltiAPIName.Reedem_Points))
            {
                string stringValue = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), value).Replace("_", " ");
                if (value == model.Type)
                {
                    khaltienum.Add(new SelectListItem { Text = stringValue.Replace("khalti", "").ToUpper(), Value = value.ToString(), Selected = true });
                }
                else
                {
                    khaltienum.Add(new SelectListItem { Text = stringValue.Replace("khalti", "").ToUpper(), Value = value.ToString() });
                }
            }
            khaltienum = khaltienum.OrderBy(x => x.Text).ToList();

            //return CreateDropdown("", khaltienum, "Select Service");
            return khaltienum;
        }
        public static List<SelectListItem> GetSelectList_LinkBankVendoTypes()
        {

            var VendoTypes = new List<SelectListItem>();

            foreach (int value in Enum.GetValues(typeof(VendorApi_CommonHelper.VendorTypes)))
            {
                string stringValue = Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), value).Replace("_", " ");
                if (stringValue.Contains("NPS") || stringValue.Contains("NCHL") || stringValue.Contains("Not Filled"))
                {
                    if (value == 0)
                    {
                        VendoTypes.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                    }
                    else
                    {
                        VendoTypes.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                    }
                }
            }
            return VendoTypes;
        }
        public static List<SelectListItem> GetSelectList_VendoTypes()
        {

            var VendoTypes = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(VendorApi_CommonHelper.VendorTypes)))
            {
                string stringValue = Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), value).Replace("_", " ");
                if (value == 0)
                {
                    VendoTypes.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    VendoTypes.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return VendoTypes;
        }
        public static List<SelectListItem> GetSelectList_Gender(AddUser model)
        {
            var genderlist = new List<SelectListItem>();
            //genderlist.Add(new SelectListItem
            //{
            //    Text = "Select Gender",
            //    Value = "0",
            //    Selected = true
            //});

            foreach (int value in Enum.GetValues(typeof(AddUser.sex)))
            {
                string stringValue = Enum.GetName(typeof(AddUser.sex), value).Replace("_", " ");
                if (value == model.Gender)
                {
                    genderlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    genderlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return genderlist;
        }

        public static List<SelectListItem> GetSelectList_AnnextureNRB()
        {
            var annexturenrb = new List<SelectListItem>();
            annexturenrb.Add(new SelectListItem
            {
                Text = "-- Select Annexture --",
                Value = "0",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(NRBReportsType)))
            {
                string stringValue = Enum.GetName(typeof(NRBReportsType), value);
                annexturenrb.Add(new SelectListItem { Text = stringValue.Replace("_", "."), Value = value.ToString() });
            }

            return annexturenrb;
        }
        public static List<SelectListItem> GetSelectList_ActivityLog()
        {
            var activity = new List<SelectListItem>();
            activity.Add(new SelectListItem
            {
                Text = "-- Select Activity --",
                Value = "0",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(AddLog.LogActivityEnum)))
            {
                string stringValue = Enum.GetName(typeof(AddLog.LogActivityEnum), value);
                activity.Add(new SelectListItem { Text = stringValue.Replace("_", " "), Value = value.ToString() });
            }

            return activity;
        }
        public static List<SelectListItem> GetSelectList_OldUserStatus()
        {
            var User = new List<SelectListItem>();
            User.Add(new SelectListItem
            {
                Text = "-- All Users --",
                Value = "2",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(AddUser.OldAndNewUser)))
            {
                string stringValue = Enum.GetName(typeof(AddUser.OldAndNewUser), value);
                User.Add(new SelectListItem { Text = stringValue.Replace("_", " "), Value = value.ToString() });
            }

            return User;
        }

        public static List<SelectListItem> GetSelectList_UserType()
        {
            var User = new List<SelectListItem>();
            User.Add(new SelectListItem
            {
                Text = "-- Select Users Type--",
                Value = "2",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(AddUser.UserType)))
            {
                string stringValue = Enum.GetName(typeof(AddUser.UserType), value);
                User.Add(new SelectListItem { Text = stringValue.Replace("_", " "), Value = value.ToString() });
            }

            return User;
        }
        public static List<SelectListItem> GetSelectList_KYCStatus(int IsKYCApproved_SelectedValue)
        {
            var KYCStatuslist = new List<SelectListItem>();
            KYCStatuslist.Add(new SelectListItem
            {
                Text = "Select KYC Status",
                Value = "0",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(AddUser.kyc)))
            {
                string stringValue = Enum.GetName(typeof(AddUser.kyc), value).Replace("_", " ");
                if (value == IsKYCApproved_SelectedValue)
                {
                    KYCStatuslist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    KYCStatuslist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return KYCStatuslist;
        }
        public static List<SelectListItem> GetSelectList_MeritalStatus(AddUser model)
        {
            var meritallist = new List<SelectListItem>();
            //meritallist.Add(new SelectListItem
            //{
            //    Text = "Select Merital Status",
            //    Value = "0",
            //    Selected = true
            //});

            foreach (int value in Enum.GetValues(typeof(AddUser.meritalstatus)))
            {
                string stringValue = Enum.GetName(typeof(AddUser.meritalstatus), value).Replace("_", " ");
                if (value == model.MeritalStatus)
                {
                    meritallist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    meritallist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return meritallist;
        }

        public static List<SelectListItem> GetSelectList_MerchantTransactionStatus()
        {
            var statuslist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddMerchantOrders.MerchantOrderStatus)))
            {
                string stringValue = Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), value).Replace("_", " ");
                statuslist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return statuslist;
        }
        public static List<SelectListItem> GetSelectList_Nationality(AddUser model)
        {
            var nationalitylist = new List<SelectListItem>();
            //nationalitylist.Add(new SelectListItem
            //{
            //    Text = "Select Nationality",
            //    Value = "0",
            //    Selected = true
            //});

            foreach (int value in Enum.GetValues(typeof(AddUser.nationality)))
            {
                string stringValue = Enum.GetName(typeof(AddUser.nationality), value).Replace("_", " ");
                if (value == model.Nationality)
                {
                    nationalitylist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    nationalitylist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return nationalitylist;
        }

        public static List<SelectListItem> GetSelectList_ProofType(AddUser model)
        {
            var prooftypelist = new List<SelectListItem>();
            prooftypelist.Add(new SelectListItem
            {
                Text = "Select ProofType",
                Value = "0",
                Selected = true
            });

            foreach (int value in Enum.GetValues(typeof(AddUser.ProofTypes)))
            {
                string stringValue = Enum.GetName(typeof(AddUser.ProofTypes), value).ToString().Replace("_", " ");
                if (value == model.ProofType)
                {
                    prooftypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    prooftypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return prooftypelist;
        }

        public static List<SelectListItem> GetSelectList_Occupation(AddUser model)
        {
            List<AddOccupation> occulist = new List<AddOccupation>();
            AddOccupation outobject = new AddOccupation();
            GetOccupation inobject = new GetOccupation();
            inobject.CheckDelete = 0;
            occulist = RepCRUD<GetOccupation, AddOccupation>.GetRecordList("sp_Occupation_Get", inobject, outobject);
            List<SelectListItem> occupationList = (from p in occulist.AsEnumerable()
                                                   select new SelectListItem
                                                   {
                                                       Text = p.CategoryName,
                                                       Value = p.Id.ToString()
                                                   }).ToList();


            return CreateDropdown(model.Occupation, occupationList, "Select Occupation");
        }

        public static List<SelectListItem> GetSelectList_State(int StateId)
        {
            List<AddState> stlist = new List<AddState>();
            AddState outobject = new AddState();
            GetState inobject = new GetState();
            inobject.CountryCode = "NP";
            stlist = RepCRUD<GetState, AddState>.GetRecordList(Common.StoreProcedures.sp_Province_Get, inobject, outobject);
            List<SelectListItem> stateList = (from p in stlist.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = p.Province,
                                                  Value = p.ProvinceCode.ToString()
                                              }).ToList();
            return CreateDropdown(StateId.ToString(), stateList, "Select State");
        }
        /*public static List<SelectListItem> GetSelectList_StateNotification(int StateId)
        {
            List<AddState> stlist = new List<AddState>();
            AddState outobject = new AddState();
            GetState inobject = new GetState();
            inobject.CountryCode = "NP";
            stlist = RepCRUD<GetState, AddState>.GetRecordList(Common.StoreProcedures.sp_Province_Get, inobject, outobject);
            List<SelectListItem> stateList = (from p in stlist.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = p.Province,
                                                  Value = p.ProvinceCode.ToString()
                                              }).ToList();

            var optionList = new List<SelectListItem>();
            optionList.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0",
                Selected = true
            });

            if (StateId > 0)
            {
                foreach (var itm in stateList)
                {
                    string stringValue = itm.Text;
                    if (itm.Value ==Convert.ToString( StateId))
                    {
                        optionList.Add(new SelectListItem { Text = stringValue, Value = itm.Value.ToString(), Selected = true });
                    }
                    else
                    {
                        optionList.Add(new SelectListItem { Text = stringValue, Value = itm.Value.ToString() });
                    }
                }
                return optionList;
            }

           
            return CreateDropdown(StateId.ToString(), stateList, "Select State");
        }
*/
        public static List<SelectListItem> GetSelectList_Country(int CountryId)
        {
            List<AddCountry> ctlist = new List<AddCountry>();
            AddCountry outobject = new AddCountry();
            GetCountry inobject = new GetCountry();
            ctlist = RepCRUD<GetCountry, AddCountry>.GetRecordList(Common.StoreProcedures.sp_Country_Get, inobject, outobject);
            List<SelectListItem> countryList = (from p in ctlist.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.CountryName,
                                                    Value = p.Id.ToString()
                                                }).ToList();
            return CreateDropdown(CountryId.ToString(), countryList, "Select Country");
        }

        public static List<SelectListItem> GetSelectList_District(int StateId, int DistrictId)
        {
            List<AddDistrict> distlist = new List<AddDistrict>();
            AddDistrict outobject = new AddDistrict();
            GetDistrict inobject = new GetDistrict();
            if (StateId != 0)
            {
                inobject.ProvinceCode = StateId.ToString();
            }
            distlist = RepCRUD<GetDistrict, AddDistrict>.GetRecordList(Common.StoreProcedures.sp_District_Get, inobject, outobject);
            List<SelectListItem> districtList = (from p in distlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.District,
                                                     Value = p.DistrictCode.ToString()
                                                 }).ToList();
            return CreateDropdown(DistrictId.ToString(), districtList, "Select District");

        }

        public static List<SelectListItem> GetSelectList_Municipality(int DistrictId, int MunicipalityId)
        {
            List<AddLocalLevel> locallist = new List<AddLocalLevel>();
            AddLocalLevel outobject = new AddLocalLevel();
            GetLocalLevel inobject = new GetLocalLevel();
            if (DistrictId != 0)
            {
                inobject.DistrictCode = DistrictId.ToString();
            }
            locallist = RepCRUD<GetLocalLevel, AddLocalLevel>.GetRecordList(Common.StoreProcedures.sp_LocalLevel_Get, inobject, outobject);
            List<SelectListItem> locallevelList = (from p in locallist.AsEnumerable()
                                                   select new SelectListItem
                                                   {
                                                       Text = p.LocalLevel,
                                                       Value = p.LocalLevelCode.ToString()
                                                   }).ToList();

            return CreateDropdown(MunicipalityId.ToString(), locallevelList, "Select Municipality");
        }

        public static List<SelectListItem> GetSelectList_CurrentDistrict(AddUser model)
        {
            List<AddDistrict> distlist = new List<AddDistrict>();
            AddDistrict outobject = new AddDistrict();
            GetDistrict inobject = new GetDistrict();
            if (model.CurrentStateId != 0)
            {
                inobject.ProvinceCode = model.CurrentStateId.ToString();
            }
            distlist = RepCRUD<GetDistrict, AddDistrict>.GetRecordList(Common.StoreProcedures.sp_District_Get, inobject, outobject);
            List<SelectListItem> districtList = (from p in distlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.District,
                                                     Value = p.DistrictCode.ToString()
                                                 }).ToList();

            return CreateDropdown(model.CurrentDistrictId.ToString(), districtList, "Select Current District");
        }
        public static List<SelectListItem> GetSelectList_CurrentMunicipality(AddUser model)
        {
            List<AddLocalLevel> locallist = new List<AddLocalLevel>();
            AddLocalLevel outobject = new AddLocalLevel();
            GetLocalLevel inobject = new GetLocalLevel();
            if (model.CurrentDistrictId != 0)
            {
                inobject.DistrictCode = model.CurrentDistrictId.ToString();
            }
            locallist = RepCRUD<GetLocalLevel, AddLocalLevel>.GetRecordList(Common.StoreProcedures.sp_LocalLevel_Get, inobject, outobject);
            List<SelectListItem> locallevelList = (from p in locallist.AsEnumerable()
                                                   select new SelectListItem
                                                   {
                                                       Text = p.LocalLevel,
                                                       Value = p.LocalLevelCode.ToString()
                                                   }).ToList();
            return CreateDropdown(model.CurrentMunicipalityId.ToString(), locallevelList, "Select Current Municipality");
        }
        public static List<SelectListItem> GetSelectList_Districtonchange(string StateId)
        {
            List<AddDistrict> distlist = new List<AddDistrict>();
            AddDistrict outobject = new AddDistrict();
            GetDistrict inobject = new GetDistrict();
            if (StateId != "0")
            {
                inobject.ProvinceCode = StateId;
            }
            distlist = RepCRUD<GetDistrict, AddDistrict>.GetRecordList(Common.StoreProcedures.sp_District_Get, inobject, outobject);
            List<SelectListItem> districtList = (from p in distlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.District,
                                                     Value = p.DistrictCode.ToString()
                                                 }).ToList();
            return CreateDropdown(StateId, districtList, "Select  District");

        }
        public static List<SelectListItem> CreateDropdown(string selectedValue, List<SelectListItem> stateList, string DefaultMessage)
        {
            var optionList = new List<SelectListItem>();
            optionList.Add(new SelectListItem
            {
                Text = DefaultMessage,
                Value = "0",
                Selected = true
            });

            foreach (var itm in stateList)
            {
                string stringValue = itm.Text;
                if (itm.Value == selectedValue)
                {
                    optionList.Add(new SelectListItem { Text = stringValue, Value = itm.Value.ToString(), Selected = true });
                }
                else
                {
                    optionList.Add(new SelectListItem { Text = stringValue, Value = itm.Value.ToString() });
                }
            }
            return optionList;
        }
        public static List<SelectListItem> GetSelectList_Municipalityonchange(string DistrictId)
        {
            List<AddLocalLevel> locallist = new List<AddLocalLevel>();
            AddLocalLevel outobject = new AddLocalLevel();
            GetLocalLevel inobject = new GetLocalLevel();
            if (DistrictId != "0")
            {
                inobject.DistrictCode = DistrictId;
            }
            locallist = RepCRUD<GetLocalLevel, AddLocalLevel>.GetRecordList(Common.StoreProcedures.sp_LocalLevel_Get, inobject, outobject);
            List<SelectListItem> locallevelList = (from p in locallist.AsEnumerable()
                                                   select new SelectListItem
                                                   {
                                                       Text = p.LocalLevel,
                                                       Value = p.LocalLevelCode.ToString()
                                                   }).ToList();
            return CreateDropdown(DistrictId, locallevelList, "Select  Municipality");

        }

        public static List<SelectListItem> GetSelectList_Providerchange(string ProviderId)
        {
            List<AddProviderLogoList> distlist = new List<AddProviderLogoList>();
            AddProviderLogoList outobject = new AddProviderLogoList();
            GetProviderLogoList inobject = new GetProviderLogoList();
            if (ProviderId != "0")
            {
                inobject.ProviderServiceCategoryId = Convert.ToInt32(ProviderId);
                inobject.IsActive = 1;
            }
            distlist = RepCRUD<GetProviderLogoList, AddProviderLogoList>.GetRecordList(Common.StoreProcedures.sp_ProviderLogoList_Get, inobject, outobject);
            distlist = distlist.Where(c => c.ProviderTypeId != ((int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events).ToString()).ToList();
            List<SelectListItem> ProviderList = (from p in distlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     // Text = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(p.ProviderTypeId)).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                                                     //                                                     Text = p.ServiceAPIName,
                                                     Text = String.IsNullOrEmpty(p.ServiceAPIName) ? p.ProviderServiceName : p.ServiceAPIName,
                                                     Value = p.ProviderTypeId.ToString()
                                                 }).ToList();

            return CreateDropdown(ProviderId, ProviderList, "Select Provider Services");
        }
        public static List<SelectListItem> GetSelectList_IssueDistrict(AddUser model)
        {
            List<AddDistrict> distlist = new List<AddDistrict>();
            AddDistrict outobject = new AddDistrict();
            GetDistrict inobject = new GetDistrict();
            if (model.IssueFromStateID != 0)
            {
                inobject.ProvinceCode = model.IssueFromStateID.ToString();
            }
            distlist = RepCRUD<GetDistrict, AddDistrict>.GetRecordList(Common.StoreProcedures.sp_District_Get, inobject, outobject);
            List<SelectListItem> districtList = (from p in distlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.District,
                                                     Value = p.DistrictCode.ToString()
                                                 }).ToList();

            return CreateDropdown(model.IssueFromDistrictID.ToString(), districtList, "Select District");
        }
        public static List<SelectListItem> GetSelectList_MerchantOrderSign()
        {
            var signlist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddMerchantOrders.MerchantOrderSign)))
            {
                string stringValue = Enum.GetName(typeof(AddMerchantOrders.MerchantOrderSign), value).Replace("_", " ");
                signlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }

            return signlist;
        }

        public static List<SelectListItem> GetSelectList_BankList(string BANK_CD)
        {
            List<AddBankList> ctlist = new List<AddBankList>();
            AddBankList outobject = new AddBankList();
            GetBankList inobject = new GetBankList();
            ctlist = RepCRUD<GetBankList, AddBankList>.GetRecordList(Common.StoreProcedures.sp_BankList_Get, inobject, outobject);
            List<SelectListItem> bankList = (from p in ctlist.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.BANK_NAME,
                                                 Value = p.BANK_CD.ToString()
                                             }).ToList();
            return CreateDropdown(BANK_CD, bankList, "Select Bank");
        }
        public static List<SelectListItem> GetSelectList_BankList_NPS(string BANK_CD)
        {
            List<AddBankListNps> ctlist = new List<AddBankListNps>();
            AddBankListNps outobject = new AddBankListNps();
            DataTable dt = outobject.GetList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                outobject = new AddBankListNps();
                outobject.BANK_NAME = dt.Rows[i]["BANK_NAME"].ToString();
                outobject.BANK_CD = dt.Rows[i]["BANK_CD"].ToString();
                ctlist.Add(outobject);
            }
            List<SelectListItem> bankList = (from p in ctlist.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.BANK_NAME,
                                                 Value = p.BANK_CD.ToString()
                                             }).ToList();
            return CreateDropdown(BANK_CD, bankList, "Select Bank");
        }
        public static List<SelectListItem> GetSelectList_MerchantList(string MerchantId)
        {
            List<AddMerchant> ctlist = new List<AddMerchant>();
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            ctlist = RepCRUD<GetMerchant, AddMerchant>.GetRecordList(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            List<SelectListItem> merchantList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.FirstName + " " + p.LastName,
                                                     Value = p.MerchantUniqueId.ToString()
                                                 }).ToList();
            return CreateDropdown(MerchantId, merchantList, "Select Merchant");
        }
        public static List<SelectListItem> GetSelectList_MerchantType(AddMerchant model)
        {
            var type = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddMerchant.MerChantType)))
            {
                string stringValue = Enum.GetName(typeof(AddMerchant.MerChantType), value).Replace("_", " ");
                if (value == model.MerchantType)
                {
                    type.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    type.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }
            return type;
        }

        public static List<SelectListItem> GetSelectList_KhaltiEnumMerchantTxn()
        {

            var khaltienum = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(VendorApi_CommonHelper.KhaltiAPIName)).Cast<VendorApi_CommonHelper.KhaltiAPIName>().Where(x => x != VendorApi_CommonHelper.KhaltiAPIName.Merchant_Account_CreditDebit && x != VendorApi_CommonHelper.KhaltiAPIName.Merchant_Account_Validation
            && x != VendorApi_CommonHelper.KhaltiAPIName.Merchant_Withdrawal))
            {
                string stringValue = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), value).Replace("_", " ");
                //int val = stringValue.IndexOf("ntc");
                if (stringValue.Contains("Merchant"))
                {
                    khaltienum.Add(new SelectListItem { Text = stringValue.Replace("khalti", "").ToUpper(), Value = value.ToString() });
                }
            }
            khaltienum = khaltienum.OrderBy(x => x.Text).ToList();
            return CreateDropdown("", khaltienum, "Select Type");
            //return khaltienum;
        }

        public static List<SelectListItem> GetSelectList_KhaltiEnumMerchantOrdrs()
        {

            var khaltienum = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(VendorApi_CommonHelper.KhaltiAPIName)).Cast<VendorApi_CommonHelper.KhaltiAPIName>().Where(x => x != VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load
            && x != VendorApi_CommonHelper.KhaltiAPIName.Merchant_Load && x != VendorApi_CommonHelper.KhaltiAPIName.Merchant_Account_Validation))
            {
                string stringValue = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), value).Replace("_", " ");
                //int val = stringValue.IndexOf("ntc");
                if (stringValue.Contains("Merchant"))
                {
                    khaltienum.Add(new SelectListItem { Text = stringValue.Replace("khalti", "").ToUpper(), Value = value.ToString() });
                }
            }
            khaltienum = khaltienum.OrderBy(x => x.Text).ToList();
            return CreateDropdown("", khaltienum, "Select Type");
            //return khaltienum;
        }

        public static List<SelectListItem> GetSelectList_MerchantWithdrawalRequestType()
        {
            var statuslist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType)))
            {
                string stringValue = Enum.GetName(typeof(AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType), value).Replace("_", " ");
                statuslist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
            }
            return CreateDropdown("", statuslist, "Select Type");
            //return signlist;
        }

        public static List<SelectListItem> GetSelectList_TicketPriority()
        {
            var prioritylist = new List<SelectListItem>();

            foreach (int value in Enum.GetValues(typeof(AddTicket.Priorities)))
            {
                string stringValue = Enum.GetName(typeof(AddTicket.Priorities), value).Replace("_", " ");

                prioritylist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });

            }

            return CreateDropdown("", prioritylist, "Select Priority");
        }

        public static List<SelectListItem> GetSelectList_TicketCategory()
        {
            List<AddTicketCategory> categorylist = new List<AddTicketCategory>();
            AddTicketCategory outobject = new AddTicketCategory();
            GetTicketCategory inobject = new GetTicketCategory();
            inobject.CheckDelete = 0;
            categorylist = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecordList(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);
            List<SelectListItem> CategoryList = (from p in categorylist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.CategoryName,
                                                     Value = p.Id.ToString()
                                                 }).ToList();


            return CreateDropdown("", CategoryList, "Select Category");
        }


        public void AccessRolesAuthentication()
        {
            MyPayEntities context = new MyPayEntities();
            if (HttpContext.Current.Session["AdminRole"] != null && HttpContext.Current.Session["AdminRole"].ToString() != "0" && HttpContext.Current.Session["AdminRole"].ToString() != "1")
            {
                string currenturl = HttpContext.Current.Request.Url.AbsolutePath;
                var RoleId = int.Parse(HttpContext.Current.Session["AdminRole"].ToString());
                var menus = (from m in context.Menus
                             join ma in context.MenuAssigns on m.Id equals ma.MenuId
                             where (m.IsActive == true && m.IsDeleted == false && ma.RoleId == RoleId && m.Url.ToLower() == (currenturl.ToLower()))
                             select new
                             {
                                 m.Id,
                                 m.Icon,
                                 m.IsActive,
                                 m.IsDeleted,
                                 m.MenuName,
                                 m.ParentId,
                                 m.Url,
                                 ma.MenuId,
                                 ma.RoleId
                             }).FirstOrDefault();
                if (menus == null)
                {
                    if (currenturl.ToLower() == "/adminlogin/dashboard")
                    {
                        HttpContext.Current.Response.Redirect("/Adminlogin/DashboardHome");
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("/AccessDenied");
                    }
                }
                else if (HttpContext.Current.Session["PasswordStatus"].ToString().ToLower() == "true")
                {
                    HttpContext.Current.Response.Redirect("/AdminLogin/ChangePassword");
                }

            }
            else if (HttpContext.Current.Session["AdminRole"] == null || HttpContext.Current.Session["AdminRole"].ToString() == "0")
            {
                HttpContext.Current.Response.Redirect("/AdminLogin");
            }
        }

        public static List<SelectListItem> GetSelectList_AgentGenderType(AddAgent model)
        {
            var gendertypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddAgent.GenderTypes)))
            {
                string stringValue = Enum.GetName(typeof(AddAgent.GenderTypes), value).ToString().Replace("_", " ");
                if (value == Convert.ToInt32(model.GenderId))
                {
                    gendertypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    gendertypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return gendertypelist;
        }
        public static List<SelectListItem> GetSelectList_AgentMaritalStatusType(AddAgent model)
        {
            var maritaltypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddAgent.meritalstatus)))
            {
                string stringValue = Enum.GetName(typeof(AddAgent.meritalstatus), value).ToString().Replace("_", " ");
                if (value == Convert.ToInt32(model.MaritalStatusId))
                {
                    maritaltypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    maritaltypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return maritaltypelist;
        }
        public static List<SelectListItem> GetSelectList_AgentNationalityType(AddAgent model)
        {
            var nationalitytypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddAgent.nationality)))
            {
                string stringValue = Enum.GetName(typeof(AddAgent.nationality), value).ToString().Replace("_", " ");
                if (value == Convert.ToInt32(model.NationalityId))
                {
                    nationalitytypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    nationalitytypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return nationalitytypelist;
        }
        public static List<SelectListItem> GetSelectList_AgentOccupation(AddAgent model)
        {
            List<AddOccupation> occulist = new List<AddOccupation>();
            AddOccupation outobject = new AddOccupation();
            GetOccupation inobject = new GetOccupation();
            inobject.CheckDelete = 0;
            occulist = RepCRUD<GetOccupation, AddOccupation>.GetRecordList("sp_Occupation_Get", inobject, outobject);
            List<SelectListItem> occupationList = (from p in occulist.AsEnumerable()
                                                   select new SelectListItem
                                                   {
                                                       Text = p.CategoryName,
                                                       Value = p.Id.ToString()
                                                   }).ToList();


            return CreateDropdown(model.Occupation, occupationList, "Select Occupation");
        }

        public static List<SelectListItem> GetSelectList_AgentDocumentType(AddAgent model)
        {
            var documenttypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddAgent.Document)))
            {
                string stringValue = Enum.GetName(typeof(AddAgent.Document), value).ToString().Replace("_", " ");
                if (value == Convert.ToInt32(model.DocumentTypeId))
                {
                    documenttypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    documenttypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return documenttypelist;
        }
        public static List<SelectListItem> GetSelectList_AgentOrganizationPANVAT(AddAgent model)
        {
            var PANVATtypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddAgent.SelectPANVAT)))
            {
                string stringValue = Enum.GetName(typeof(AddAgent.SelectPANVAT), value).ToString().Replace("_", " ");
                if (value == Convert.ToInt32(model.OrganizationPAN_VATId))
                {
                    PANVATtypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    PANVATtypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return PANVATtypelist;
        }
        public static List<SelectListItem> GetSelectList_AgentBankList(string BANK_CD, string flag)
        {
            List<AddBankList> ctlist = new List<AddBankList>();
            AddBankList outobject = new AddBankList();
            GetBankList inobject = new GetBankList();
            inobject.flag = flag;
            inobject.BANK_CD = BANK_CD;
            ctlist = RepCRUD<GetBankList, AddBankList>.GetRecordList(Common.StoreProcedures.sp_Get_AgentBankList, inobject, outobject);
            if (flag.ToLower() == "branchname")
            {

                List<SelectListItem> bankList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.BRANCH_NAME,
                                                 }).ToList();


                return bankList;
            }
            else
            {
                List<SelectListItem> bankList = (from p in ctlist.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.BANK_NAME,
                                                     Value = p.BANK_CD.ToString()
                                                 }).ToList();
                return CreateDropdown(BANK_CD, bankList, "Select Bank");
            }


        }

        public static List<SelectListItem> GetSelectList_AgentPEP(AddAgent model)
        {
            var gendertypelist = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AddAgent.PEPType)))
            {
                string stringValue = Enum.GetName(typeof(AddAgent.PEPType), value).ToString().Replace("_", " ");
                if (value == Convert.ToInt32(model.PEPId))
                {
                    gendertypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    gendertypelist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return gendertypelist;
        }
        public static List<SelectListItem> GetSelectList_AgentCategoryStatus(AgentCategory model)
        {
            var AgentCategorystatus = new List<SelectListItem>();
            foreach (int value in Enum.GetValues(typeof(AgentCategory.CategoryStatus)))
            {
                string stringValue = Enum.GetName(typeof(AgentCategory.CategoryStatus), value).ToString().Replace("_", " ");
                if (value == Convert.ToInt32(model.Status))
                {
                    AgentCategorystatus.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    AgentCategorystatus.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return AgentCategorystatus;
        }


        public static List<SelectListItem> GetSelectList_AgentCategory(AgentCategory model)
        {

            AgentCategory w = new AgentCategory();
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            DataTable dt = w.GetAgentCategoryData("dc", "", "", 1, 1, "");
            List<AgentCategory> trans = new List<AgentCategory>();
            if (dt.Rows.Count > 0)
            {
                trans = (from DataRow row in dt.Rows

                         select new AgentCategory
                         {
                             Category = row["Category"].ToString(),
                             AgentCategoryId = row["CategoryId"].ToString(),

                         }).ToList();
            }

            List<SelectListItem> CounterList = (from p in trans.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.Category,
                                                    Value = p.AgentCategoryId.ToString()
                                                }).ToList();
            //CategoryList = CommonHelpers.CreateDropdown("0", CounterList, "Select");
            

            return CreateDropdown(model.AgentCategoryId, CounterList, "Select Category");
        }
        public CommonDBResonse ExecuteProcedureGetValueBusSewa(string Procedure, Hashtable HT)
        {
            CommonDBResonse obj = new CommonDBResonse();
            string result = "";
            string HTString = string.Empty;
            string HTStrings = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString());
            if (Procedure.ToLower().IndexOf("remittance") > 0)
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstringRemittance"].ToString());
            }
            SqlCommand cmd = new SqlCommand();
            string key = null;
            cmd = new SqlCommand(Procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                foreach (string key_loopVariable in HT.Keys)
                {
                    key = key_loopVariable;
                    cmd.Parameters.AddWithValue("@" + key, HT[key]);

                    HTString += $"@{key}: '{HT[key]}', ";
                    HTStrings += $"@{key}= '{HT[key]}', ";
                }
                SqlParameter retval = new SqlParameter("@OUTPUT", SqlDbType.VarChar, 200);
                retval.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(retval);
                cmd.CommandTimeout = 0;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        obj.code = reader.GetString(0); // Assuming the ID is an integer
                        obj.Message = reader.GetString(1); // Assuming the message is a string
                        if (Procedure == "sp_BusDetail" || Procedure == "sp_TouristBus_Detail")
                        {
                            if (reader.GetString(2).ToLower() != "empty")
                            {
                                obj.Id = !string.IsNullOrEmpty(reader.GetString(2)) ? reader.GetString(2) : "";
                            }

                        }


                    }
                }
                // cmd.ExecuteNonQuery();
                return obj;
            }
            catch (Exception ex)
            {
                Common.AddLogs($"Exception occured on {Common.fnGetdatetime()} in {Procedure} with Parameters as : {HTString}. Exception Details: {ex.Message}", false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                con.Close();
                cmd.Dispose();
                result = "0";
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return obj;
        }
    }

    public class ValidationHelper
    {
        public bool checkBalance = false;
        public bool checkToken = false;
        public bool checkPIN = false;
        public bool checkUserToken = false;
        public bool checkFailedTxns = false;
        public bool checkDuplicateTxn = false;
    }


}