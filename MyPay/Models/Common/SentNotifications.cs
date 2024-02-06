using MyPay.Models.Add;
using MyPay.Models.Get;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;

namespace MyPay.Models.Common.Notifications
{
    public class SentNotifications
    {


        #region "Enums"

        public enum StatusType
        {
            NotRegisterd = 1,
            Opened = 2,
            Sent = 3
        }

        public enum NotificationType
        {
            MobileApp = 1,
            Admin = 2
        }
        #endregion

        #region "Properties"
        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //Image
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        //Url
        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        //Take
        private int? _Take = 0;
        public int? Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private int? _Skip = 0;
        public int? Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }

        //CreatedDate
        private DateTime? _CreatedDate = DateTime.UtcNow;
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        //UpdatedDate
        private DateTime? _UpdatedDate = DateTime.UtcNow;
        public DateTime? UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        //CheckDelete
        private int? _CheckDelete = 2;
        public int? CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CheckActive
        private int? _CheckActive = 2;
        public int? CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int? _CheckApprovedByadmin = 2;
        public int? CheckApprovedByadmin
        {
            get { return _CheckApprovedByadmin; }
            set { _CheckApprovedByadmin = value; }
        }

        //IsDeleted
        private bool _IsDeleted = false;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        //IsApprovedByAdmin
        private bool _IsApprovedByAdmin = false;
        public bool IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
        }

        //IsActive
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //MessageId
        private Int64 _MessageId = 0;
        public Int64 MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }

        //Status
        private Int64 _Status = 0;
        public Int64 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //UserId
        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        //EndTime
        private DateTime? _EndTime = DateTime.UtcNow;
        public DateTime? EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        //DeviceCode
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //Email
        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        //Email
        private string _PlatForm = string.Empty;
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        #endregion

        #region "Add Delete Update Methods"
        public Int64 Add(string authenticationToken, AddNotification model)
        {
            try
            {
                //Hashtable HT = SetObject();
                //MyPay.Models.Common.CommonHelpers obj = new MyPay.Models.Common.CommonHelpers();
                //string ResultId = obj.ExecuteProcedureGetReturnValue("sp_SentNotifications_AddNew", HT);

                Int64 ResultId = 0;
                AddNotification res = new AddNotification();
                res.Title = model.Title;
                res.NotificationDescription = model.NotificationDescription;
                res.NotificationMessage = model.NotificationMessage;
                res.IsActive = model.IsActive;
                res.MemberId = model.MemberId;
                res.NotificationType = Convert.ToInt32(model.NotificationType);
                res.SentStatus = 0;
                res.ReadStatus = 0;
                res.IsActive = true;
                res.IpAddress = Common.GetUserIP();
                res.CreatedDate = System.DateTime.UtcNow;
                res.UpdatedDate = System.DateTime.UtcNow;
                if (!string.IsNullOrEmpty(authenticationToken))
                {
                    res.CreatedBy = Common.GetCreatedById(authenticationToken);
                    res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                }
                else
                {
                    res.CreatedBy = model.CreatedBy;
                    res.CreatedByName = model.CreatedByName;
                }
                Int64 Id = RepCRUD<AddNotification, GetNotification>.Insert(res, "notification");
                ResultId = Id;
                return ResultId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool Update()
        {
            try
            {
                Hashtable HT = SetObject();
                HT.Add("Id", Id);
                MyPay.Models.Common.CommonHelpers obj = new MyPay.Models.Common.CommonHelpers();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_SentNotifications_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public Hashtable SetObject()
        {
            Hashtable Ht = new Hashtable();
            Ht.Add("Status", Status);
            Ht.Add("CreatedDate", CreatedDate);
            Ht.Add("UpdatedDate", UpdatedDate);
            Ht.Add("IsDeleted", IsDeleted);
            Ht.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            Ht.Add("IsActive", IsActive);
            Ht.Add("MessageId", MessageId);
            Ht.Add("MemberId", MemberId);
            return Ht;
        }
        #endregion

        #region "Get Methods"
        public DataTable GetList()
        {
            DataTable dt = new DataTable();
            try
            {
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("IsDeleted", CheckDelete);
                HT.Add("IsActive", CheckActive);
                HT.Add("IsApprovedByAdmin", CheckApprovedByadmin);
                HT.Add("MessageId", MessageId);
                HT.Add("MemberId", MemberId);
                HT.Add("UserId", UserId);
                HT.Add("PlatForm", PlatForm);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("UserName", UserName);
                HT.Add("Email", Email);
                HT.Add("FirstName", FirstName);
                HT.Add("Status", Status);
                MyPay.Models.Common.CommonHelpers obj = new MyPay.Models.Common.CommonHelpers();
                dt = obj.GetDataFromStoredProcedure("sp_SentNotifications_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }



        public bool GetRecord()
        {
            DataTable dt = new DataTable();
            try
            {
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("Take", 1);
                HT.Add("Skip", Skip);
                HT.Add("IsDeleted", CheckDelete);
                HT.Add("IsActive", CheckActive);
                HT.Add("IsApprovedByAdmin", CheckApprovedByadmin);
                HT.Add("MessageId", MessageId);
                HT.Add("MemberId", MemberId);
                HT.Add("UserId", UserId);
                HT.Add("PlatForm", PlatForm);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("UserName", UserName);
                HT.Add("Email", Email);
                HT.Add("FirstName", FirstName);
                HT.Add("Status", Status);
                MyPay.Models.Common.CommonHelpers obj = new MyPay.Models.Common.CommonHelpers();
                dt = obj.GetDataFromStoredProcedure("sp_SentNotifications_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    Title = dt.Rows[0]["Title"].ToString();
                    Description = dt.Rows[0]["Description"].ToString();
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    UserId = dt.Rows[0]["UserId"].ToString();
                    Email = dt.Rows[0]["Email"].ToString();
                    DeviceCode = dt.Rows[0]["DeviceCode"].ToString();
                    ContactNumber = dt.Rows[0]["ContactNumber"].ToString();
                    FirstName = dt.Rows[0]["FirstName"].ToString();
                    LastName = dt.Rows[0]["LastName"].ToString();
                    PlatForm = dt.Rows[0]["Platform"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["MemberId"].ToString()))
                    {
                        MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["MessageId"].ToString()))
                    {
                        MessageId = Convert.ToInt64(dt.Rows[0]["MessageId"].ToString());
                    }
                    Status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public Int64 TotalDataCount(SentNotifications user)
        {
            Int64 data = 0;
            try
            {
                string Result = "";
                string str = "select count(S.Id) from SentNotifications S with(nolock) inner join Users U on U.MemberId=S.MemberId where ";
                if (user.CheckDelete != 2)
                {
                    str += "S.IsDeleted =" + user.CheckDelete;
                }
                else
                {
                    str += "S.IsDeleted =0";
                }
                if (user.CheckActive != 2)
                {
                    str += " AND S.IsActive =" + user.CheckActive;
                }
                if (user.CheckApprovedByadmin != 2)
                {
                    str += " AND U.IsApprovedByAdmin =" + user.CheckApprovedByadmin;
                }
                if (user.Status != 0)
                {
                    str += " AND S.Status =" + user.Status;
                }
                if (user.MemberId != 0)
                {
                    str += " AND S.MemberId =" + user.MemberId;
                }
                if (user.MessageId != 0)
                {
                    str += " AND S.MessageId =" + user.MessageId;
                }
                if (user.UserName != "")
                {
                    str += " AND U.UserName ='" + user.MessageId + "'";
                }
                if (user.Email != "")
                {
                    str += " AND U.Email ='" + user.Email + "'";
                }
                if (user.FirstName != "")
                {
                    str += " AND U.FirstName like'%" + user.FirstName + "%'";
                }
                if (user.ContactNumber != "")
                {
                    str += " AND U.ContactNumber ='" + user.ContactNumber + "'";
                }
                if (user.PlatForm != "")
                {
                    str += " AND U.PlatForm ='" + user.PlatForm + "'";
                }
                MyPay.Models.Common.CommonHelpers obj = new MyPay.Models.Common.CommonHelpers();
                Result = obj.GetScalarValueWithValue(str);

                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (Convert.ToInt32(Result));
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return data;
        }


        public static string GetdeviceIds(string type = "", int index = 0)
        {
            string data = "";
            try
            {
                string Result = "";
                string str = string.Format("SELECT STUFF( (SELECT ',' + s.DeviceCode  FROM Users s Where s.DeviceCode <> ''ORDER BY s.MemberId OFFSET " + (index * 1000) + " ROWS FETCH NEXT 1000 ROWS ONLY FOR XML PATH('')), 1, 1, '') AS Users with(nolock)");


                MyPay.Models.Common.CommonHelpers obj = new MyPay.Models.Common.CommonHelpers();
                Result = obj.GetScalarValueWithValue(str);

                if (!string.IsNullOrEmpty(Result))
                {
                    data = Result;
                }
            }
            catch (Exception ex)
            {


            }
            return data;
        }

        #endregion

        public static async void SendNotifications(string requestXml, string type)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                byte[] bytes = null;

                //string SERVER_API_KEY = "AAAAk6bJ8JQ:APA91bFdMcMVxyoNZpbcNKr-bMqA4pA4hoSb25DeCZAss0u2CZW68q6QWp9NwIiVdPTQ2vKr8WUpv6ZAxPaeranYsHmqDx8ME0yltyqMjsYEeexpP4bHN5drmYk87TJ-AHYD_IzBiwOc";
                //string SENDER_ID = "634158444692";

                string SERVER_API_KEY = "AAAAg4ThYX8:APA91bE2MNNS1YkfWdlvsJzoDHZ4eElfrewRdFKM9nqW4PtMWDcdZcA3q7R2WT9folZ-GtnuRgR7iSvJSUFp1FSXtob1pBF6Cjl30qXyIHU8ta7KskDEvNf9WHamMsJxfw1Terw4DrhQ";
                string SENDER_ID = "564870078847";
                bytes = System.Text.Encoding.UTF8.GetBytes(requestXml);
                request.ContentType = "application/json; encoding='utf-8'";

                request.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                request.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.Timeout = Timeout.Infinite;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Flush();
                requestStream.Close();

                HttpWebResponse response = default(HttpWebResponse);
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    // return responseStr;
                }
            }
            catch (Exception ex)
            {


            }
            //return null;
        }

        public static string SendNotificationssingle(string requestXml, string type)
        {
            try
            {


                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                byte[] bytes = null;

                //string SERVER_API_KEY = "AAAAk6bJ8JQ:APA91bFdMcMVxyoNZpbcNKr-bMqA4pA4hoSb25DeCZAss0u2CZW68q6QWp9NwIiVdPTQ2vKr8WUpv6ZAxPaeranYsHmqDx8ME0yltyqMjsYEeexpP4bHN5drmYk87TJ-AHYD_IzBiwOc";
                //string SENDER_ID = "634158444692";

                string SERVER_API_KEY = "AAAAg4ThYX8:APA91bE2MNNS1YkfWdlvsJzoDHZ4eElfrewRdFKM9nqW4PtMWDcdZcA3q7R2WT9folZ-GtnuRgR7iSvJSUFp1FSXtob1pBF6Cjl30qXyIHU8ta7KskDEvNf9WHamMsJxfw1Terw4DrhQ";
                string SENDER_ID = "564870078847";
                bytes = System.Text.Encoding.UTF8.GetBytes(requestXml);
                request.ContentType = "application/json; encoding='utf-8'";

                request.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                request.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.Timeout = Timeout.Infinite;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Flush();
                requestStream.Close();

                HttpWebResponse response = default(HttpWebResponse);
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }
        public static void ExecuteNotification(string authenticationToken, AddNotification objNotification, string InactiveDeviceCode = "")
        {
            try
            {
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                string MemberId = Convert.ToString(objNotification.MemberId);
                inobject.MemberId = Convert.ToInt64(MemberId);
                AddUserLoginWithPin model = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin , inobject, outobject);
                if (!string.IsNullOrEmpty(MemberId) && (model == null || model.Id == 0 || MemberId == "0"))
                {
                    return;
                }
                else
                {
                    // For Testing
                    // model.DeviceCode = "fGuaQBysTeybWwe-3v2Ofo:APA91bF36AwlrjkPDJ0KyqyQ71OovTwS91nGPO7b6yduifrWWmsDFmXXj85jqAg-EFdhA7PM74ItdrJtWT4a2GSjfDudgNzUFalACZxppM-Et5cG6t34BLUnC2ntaE7aYodv2iCaqziQ";
                    // foreach (DataRow item in dt.Rows)
                    {
                        SentNotifications st = new SentNotifications();
                        Int64 NotificationID = st.Add(authenticationToken, objNotification);
                        if (NotificationID > 0)
                        {



                            ArrayList list = new ArrayList();
                            string json = String.Empty;
                            if (model.PlatForm.ToLower() == "ios")
                            {
                                FcmNotificationForIOS fcm = new FcmNotificationForIOS();
                                fcm.notification.title = objNotification.Title;
                                fcm.notification.body = objNotification.NotificationMessage;
                                if (string.IsNullOrEmpty(InactiveDeviceCode))
                                {
                                    list.Add(model.DeviceCode);
                                }
                                else
                                {
                                    list.Add(InactiveDeviceCode);
                                }
                                fcm.registration_ids = "##Regids##";

                                json = JsonConvert.SerializeObject(fcm);
                            }
                            else
                            {
                                FcmNotification fcm = new FcmNotification();
                                fcm.data.title = objNotification.Title;
                                fcm.data.text = objNotification.NotificationMessage;
                                if (string.IsNullOrEmpty(InactiveDeviceCode))
                                {
                                    list.Add(model.DeviceCode);
                                }
                                else
                                {
                                    list.Add(InactiveDeviceCode);
                                }
                                fcm.registration_ids = "##Regids##";

                                json = JsonConvert.SerializeObject(fcm);
                            }
                            json = json.Replace("##Regids##", JsonConvert.SerializeObject(list));
                            json = json.Replace("\"[", "[").Replace("]\"", "]");
                            string data = "{";
                            if (!string.IsNullOrEmpty(objNotification.Title))
                            {
                                data += "\"title\": \"" + objNotification.Title + "\",";
                            }
                            if (!string.IsNullOrEmpty(objNotification.NotificationMessage))
                            {
                                data += "\"text\": \"" + objNotification.NotificationMessage + "\",";
                            }
                            if (!string.IsNullOrEmpty(objNotification.NotificationType.ToString()))
                            {
                                data += "\"notificationtype\": \"" + objNotification.NotificationType.ToString() + "\",";
                            }
                            //if (!string.IsNullOrEmpty(objNotification.NotificationRedirectType.ToString()))
                            //{
                            //    data += "\"notificationredirecttype\": \"" + objNotification.NotificationRedirectType.ToString() + "\",";
                            //}
                            //if (!string.IsNullOrEmpty(objNotification.NotificationImage.ToString()))
                            //{
                            //    data += "\"notificationimage\": \"" + objNotification.NotificationImage.ToString() + "\",";
                            //}
                            //if (!string.IsNullOrEmpty(usr.Url))
                            //{
                            //    data += "\"url\": \"" + usr.Url + "\",";
                            //}
                            //if (!string.IsNullOrEmpty(usr.WebUrl))
                            //{
                            //    data += "\"WebUrl\": \"" + usr.WebUrl + "\",";
                            //}
                            //if (!string.IsNullOrEmpty(usr.MatchName))
                            //{
                            //    data += "\"MatchName\": \"" + usr.MatchName + "\",";
                            //}
                            //if (!string.IsNullOrEmpty(usr.MatchUniqueId))
                            //{
                            //    data += "\"MatchUniqueId\": \"" + usr.MatchUniqueId + "\",";
                            //    data += "\"EndTime\": \"" + Convert.ToDateTime(usr.EndTime).ToString("MMM dd, yyyy HH:mm:ss") + "\",";
                            //}
                            //if (!string.IsNullOrEmpty(usr.LeagueUniqueId))
                            //{
                            //    data += "\"LeagueUniqueId\": \"" + usr.LeagueUniqueId + "\",";
                            //}

                            data += "\"id\": " + NotificationID;
                            data += "}";
                            json = json.Replace("\"##data##\"", data);
                            json = json.Replace("\"##notification##\"", data);
                            json = json.Replace("\"##notificationtype##\"", objNotification.NotificationType.ToString());
                            json = json.Replace("\"##notificationredirecttype##\"", objNotification.NotificationRedirectType.ToString());


                            string apiresult = SentNotifications.SendNotificationssingle(json, "android");
                            FcmResponse response = JsonConvert.DeserializeObject<FcmResponse>(apiresult);
                            if (response.success == 1)
                            {
                                st.Status = (int)SentNotifications.StatusType.Sent;
                            }
                            else
                            {
                                st.Status = (int)SentNotifications.StatusType.NotRegisterd;
                            }
                            AddNotification objoutNotification = new AddNotification();
                            GetNotification objInNotification = new GetNotification();
                            objInNotification.Id = NotificationID;

                            AddNotification res = RepCRUD<GetNotification, AddNotification>.GetRecord(Common.StoreProcedures.sp_Notification_Get, objInNotification, objoutNotification);
                            if (res != null && res.Id != 0)
                            {
                                res.SentStatus = 1;
                                res.FireBaseRequest = json;
                                res.FireBaseResponse = apiresult;
                                res.NotificationType = objNotification.NotificationType;
                                res.UpdatedDate = System.DateTime.UtcNow;
                                bool UpdateNotification = RepCRUD<AddNotification, GetNotification>.Update(res, "notification");
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void ExecuteBulkNotificationFromAdmin(Int64 NotificationCampaignId, string DeviceCodeCSV, string Province, string District)
        {
            try
            {
                if (!string.IsNullOrEmpty(DeviceCodeCSV))
                {
                    AddNotificationCampaign outobject = new AddNotificationCampaign();
                    GetNotificationCampaign inobject = new GetNotificationCampaign();
                    inobject.Id = NotificationCampaignId;
                    AddNotificationCampaign objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
                    if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0)
                    {

                        ArrayList list = new ArrayList();
                        string json = String.Empty;
                        FcmNotification fcm = new FcmNotification();
                        fcm.data.title = objUpdateNotificationCampaign.Title;
                        fcm.data.text = objUpdateNotificationCampaign.NotificationMessage;
                        fcm.data.body = objUpdateNotificationCampaign.NotificationMessage;
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationImage))
                        {
                            if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                            {
                                fcm.data.image = (Common.LiveSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                            else
                            {
                                fcm.data.image = (Common.TestSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                        }
                        else
                        {
                            fcm.data.image = string.Empty;
                            fcm.notification.image = string.Empty;
                        }


                        fcm.notification.title = objUpdateNotificationCampaign.Title;
                        fcm.notification.text = objUpdateNotificationCampaign.NotificationMessage;
                        fcm.notification.body = objUpdateNotificationCampaign.NotificationMessage;

                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationImage))
                        {
                            if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                            {
                                fcm.notification.image = (Common.LiveSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                            else
                            {
                                fcm.notification.image = (Common.TestSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                        }
                        else
                        {
                            fcm.data.image = string.Empty;
                            fcm.notification.image = string.Empty;
                        }
                        DeviceCodeCSV = DeviceCodeCSV.Trim();
                        string[] devicecodes = DeviceCodeCSV.Split(',');
                        if (!string.IsNullOrEmpty(DeviceCodeCSV))
                        {
                            foreach (var item in devicecodes)
                            {
                                list.Add(item.Trim());
                            }

                        }
                        fcm.registration_ids = "##Regids##";

                        json = JsonConvert.SerializeObject(fcm);
                        json = json.Replace("##Regids##", JsonConvert.SerializeObject(list));
                        json = json.Replace("\"[", "[").Replace("]\"", "]");
                        string data = "{";
                        if (objUpdateNotificationCampaign.Id > 0)
                        {
                            data += "\"campaignid\": \"" + objUpdateNotificationCampaign.Id + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.URL.ToString()))
                        {
                            data += "\"URL\": \"" + objUpdateNotificationCampaign.URL.ToString() + "\",";
                        }
                       /* if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.Province))
                        {
                            data += "\"Province\": \"" + objUpdateNotificationCampaign.Province + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.District))
                        {
                            data += "\"District\": \"" + objUpdateNotificationCampaign.District + "\",";
                        }*/
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.Title))
                        {
                            data += "\"title\": \"" + objUpdateNotificationCampaign.Title + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationMessage))
                        {
                            data += "\"text\": \"" + objUpdateNotificationCampaign.NotificationMessage + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationType.ToString()))
                        {
                            data += "\"notificationtype\": \"" + objUpdateNotificationCampaign.NotificationType.ToString() + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationRedirectType.ToString()))
                        {
                            data += "\"notificationredirecttype\": \"" + objUpdateNotificationCampaign.NotificationRedirectType.ToString() + "\",";
                        }
                       
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationImage.ToString()))
                        {
                            if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                            {
                                data += "\"notificationimage\": \"" + (Common.LiveSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString()) + "\",";
                            }
                            else
                            {
                                data += "\"notificationimage\": \"" + (Common.TestSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString()) + "\",";
                            }
                        }

                        data += "\"id\": " + objUpdateNotificationCampaign.Id;
                        data += "}";
                        json = json.Replace("\"##data##\"", data);
                        json = json.Replace("\"##notification##\"", data);
                        json = json.Replace("\"##ExternalURL##\"", "\"" + objUpdateNotificationCampaign.URL + "\"");
                        json = json.Replace("\"##notificationtype##\"", objUpdateNotificationCampaign.NotificationType.ToString());
                        json = json.Replace("\"##notificationredirecttype##\"", objUpdateNotificationCampaign.NotificationRedirectType.ToString());

                        //var t1 = System.Threading.Tasks.Task.Run(() => SentNotifications.SendNotifications(json, "android"));
                        //t1.Wait();
                        //string apiresult = t1.ToString();

                        //uncomment before pushing code
                        Task.Factory.StartNew(() => SentNotifications.SendNotifications(json, "android"));

                        //string apiresult = SentNotifications.SendNotifications(json, "android");
                        //FcmResponse response = JsonConvert.DeserializeObject<FcmResponse>(apiresult);
                        //if (response.success == 1)
                        //{
                        //    Common.AddLogs($"Bulk Notification Sent(ID: " + NotificationCampaignId + ")" + apiresult, true, Convert.ToInt32(AddLog.LogType.User), Common.CreatedBy, Common.CreatedByName, false, "Web", "", 0, Common.CreatedBy, Common.CreatedByName);
                        //}
                        //else
                        //{
                        //    Common.AddLogs($"Bulk Notification Response Failed(ID: " + NotificationCampaignId + ") " + apiresult, true, Convert.ToInt32(AddLog.LogType.User), Common.CreatedBy, Common.CreatedByName, false, "Web", "", 0, Common.CreatedBy, Common.CreatedByName);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ExecuteBulkExcelNotificationFromAdmin(string Title, string Message, string NotificationType, string DeviceCodeCSV)
        {
            try
            {
                if (!string.IsNullOrEmpty(DeviceCodeCSV))
                {
                    //AddNotificationCampaign outobject = new AddNotificationCampaign();
                    //GetNotificationCampaign inobject = new GetNotificationCampaign();
                    //inobject.Id = NotificationCampaignId;
                    //AddNotificationCampaign objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
                    //if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0)
                    //{
                    ArrayList list = new ArrayList();
                    string json = String.Empty;
                    FcmNotification fcm = new FcmNotification();
                    fcm.data.title = Title;
                    fcm.data.text = Message;
                    fcm.data.body = Message;

                    fcm.data.image = string.Empty;
                    fcm.notification.image = string.Empty;


                    fcm.notification.title = Title;
                    fcm.notification.text = Message;
                    fcm.notification.body = Message;

                    DeviceCodeCSV = DeviceCodeCSV.Trim();
                    string[] devicecodes = DeviceCodeCSV.Split(',');
                    if (!string.IsNullOrEmpty(DeviceCodeCSV))
                    {
                        foreach (var item in devicecodes)
                        {
                            list.Add(item.Trim());
                        }

                    }
                    fcm.registration_ids = "##Regids##";

                    json = JsonConvert.SerializeObject(fcm);
                    json = json.Replace("##Regids##", JsonConvert.SerializeObject(list));
                    json = json.Replace("\"[", "[").Replace("]\"", "]");
                    string data = "{";
                    //if (objUpdateNotificationCampaign.Id > 0)
                    //{
                    //    data += "\"campaignid\": \"" + objUpdateNotificationCampaign.Id + "\",";
                    //}
                    if (!string.IsNullOrEmpty(Title))
                    {
                        data += "\"title\": \"" + Title + "\",";
                    }
                    if (!string.IsNullOrEmpty(Message))
                    {
                        data += "\"text\": \"" + Message + "\",";
                    }
                    if (!string.IsNullOrEmpty(NotificationType))
                    {
                        data += "\"notificationtype\": \"" + NotificationType + "\",";
                    }
                    if (!string.IsNullOrEmpty(NotificationType.ToString()))
                    {
                        data += "\"notificationredirecttype\": \"" + NotificationType + "\",";
                    }

                    //data += "\"id\": " + objUpdateNotificationCampaign.Id;
                    data += "}";
                    json = json.Replace("\"##data##\"", data);
                    json = json.Replace("\"##notification##\"", data);
                    json = json.Replace("\"##notificationtype##\"", NotificationType);
                    json = json.Replace("\"##notificationredirecttype##\"", NotificationType);

                    //var t1 = System.Threading.Tasks.Task.Run(() => SentNotifications.SendNotifications(json, "android"));
                    //t1.Wait();
                    //string apiresult = t1.ToString();
                    Task.Factory.StartNew(() => SentNotifications.SendNotifications(json, "android"));
                    //string apiresult = SentNotifications.SendNotifications(json, "android");
                    //FcmResponse response = JsonConvert.DeserializeObject<FcmResponse>(apiresult);
                    //if (response.success == 1)
                    //{
                    //    Common.AddLogs($"Bulk Notification Sent(ID: " + NotificationCampaignId + ")" + apiresult, true, Convert.ToInt32(AddLog.LogType.User), Common.CreatedBy, Common.CreatedByName, false, "Web", "", 0, Common.CreatedBy, Common.CreatedByName);
                    //}
                    //else
                    //{
                    //    Common.AddLogs($"Bulk Notification Response Failed(ID: " + NotificationCampaignId + ") " + apiresult, true, Convert.ToInt32(AddLog.LogType.User), Common.CreatedBy, Common.CreatedByName, false, "Web", "", 0, Common.CreatedBy, Common.CreatedByName);
                    //}
                    ////}
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void ExecuteBulkNotificationCampaignExcelFromAdmin(Int64 NotificationCampaignId, string DeviceCodeCSV)
        {
            try
            {
                if (!string.IsNullOrEmpty(DeviceCodeCSV))
                {
                    AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
                    GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
                    inobject.Id = NotificationCampaignId;
                    AddNotificationCampaignExcel objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
                    if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0)
                    {

                        ArrayList list = new ArrayList();
                        string json = String.Empty;
                        FcmNotification fcm = new FcmNotification();
                        fcm.data.title = objUpdateNotificationCampaign.Title;
                        fcm.data.text = objUpdateNotificationCampaign.NotificationMessage;
                        fcm.data.body = objUpdateNotificationCampaign.NotificationMessage;
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationImage))
                        {
                            if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                            {
                                fcm.data.image = (Common.LiveSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                            else
                            {
                                fcm.data.image = (Common.TestSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                        }
                        else
                        {
                            fcm.data.image = string.Empty;
                            fcm.notification.image = string.Empty;
                        }


                        fcm.notification.title = objUpdateNotificationCampaign.Title;
                        fcm.notification.text = objUpdateNotificationCampaign.NotificationMessage;
                        fcm.notification.body = objUpdateNotificationCampaign.NotificationMessage;

                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationImage))
                        {
                            if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                            {
                                fcm.notification.image = (Common.LiveSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                            else
                            {
                                fcm.notification.image = (Common.TestSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString());
                            }
                        }
                        else
                        {
                            fcm.data.image = string.Empty;
                            fcm.notification.image = string.Empty;
                        }
                        DeviceCodeCSV = DeviceCodeCSV.Trim();
                        string[] devicecodes = DeviceCodeCSV.Split(',');
                        if (!string.IsNullOrEmpty(DeviceCodeCSV))
                        {
                            foreach (var item in devicecodes)
                            {
                                list.Add(item.Trim());
                            }

                        }
                        fcm.registration_ids = "##Regids##";

                        json = JsonConvert.SerializeObject(fcm);
                        json = json.Replace("##Regids##", JsonConvert.SerializeObject(list));
                        json = json.Replace("\"[", "[").Replace("]\"", "]");
                        string data = "{";
                        if (objUpdateNotificationCampaign.Id > 0)
                        {
                            data += "\"campaignid\": \"" + objUpdateNotificationCampaign.Id + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.Title))
                        {
                            data += "\"title\": \"" + objUpdateNotificationCampaign.Title + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationMessage))
                        {
                            data += "\"text\": \"" + objUpdateNotificationCampaign.NotificationMessage + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationType.ToString()))
                        {
                            data += "\"notificationtype\": \"" + objUpdateNotificationCampaign.NotificationType.ToString() + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationRedirectType.ToString()))
                        {
                            data += "\"notificationredirecttype\": \"" + objUpdateNotificationCampaign.NotificationRedirectType.ToString() + "\",";
                        }
                        if (!string.IsNullOrEmpty(objUpdateNotificationCampaign.NotificationImage.ToString()))
                        {
                            if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                            {
                                data += "\"notificationimage\": \"" + (Common.LiveSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString()) + "\",";
                            }
                            else
                            {
                                data += "\"notificationimage\": \"" + (Common.TestSiteUrl + "Images/NotificationImages/" + objUpdateNotificationCampaign.NotificationImage.ToString()) + "\",";
                            }
                        }

                        data += "\"id\": " + objUpdateNotificationCampaign.Id;
                        data += "}";
                        json = json.Replace("\"##data##\"", data);
                        json = json.Replace("\"##notification##\"", data);
                        json = json.Replace("\"##notificationtype##\"", objUpdateNotificationCampaign.NotificationType.ToString());
                        json = json.Replace("\"##notificationredirecttype##\"", objUpdateNotificationCampaign.NotificationRedirectType.ToString());

                        //var t1 = System.Threading.Tasks.Task.Run(() => SentNotifications.SendNotifications(json, "android"));
                        //t1.Wait();
                        //string apiresult = t1.ToString();
                        Task.Factory.StartNew(() => SentNotifications.SendNotifications(json, "android"));
                        //string apiresult = SentNotifications.SendNotifications(json, "android");
                        //FcmResponse response = JsonConvert.DeserializeObject<FcmResponse>(apiresult);
                        //if (response.success == 1)
                        //{
                        //    Common.AddLogs($"Bulk Notification Sent(ID: " + NotificationCampaignId + ")" + apiresult, true, Convert.ToInt32(AddLog.LogType.User), Common.CreatedBy, Common.CreatedByName, false, "Web", "", 0, Common.CreatedBy, Common.CreatedByName);
                        //}
                        //else
                        //{
                        //    Common.AddLogs($"Bulk Notification Response Failed(ID: " + NotificationCampaignId + ") " + apiresult, true, Convert.ToInt32(AddLog.LogType.User), Common.CreatedBy, Common.CreatedByName, false, "Web", "", 0, Common.CreatedBy, Common.CreatedByName);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class FcmNotificationForIOS
    {
        //notification
        private NotificationList _notification = new NotificationList();
        public NotificationList notification
        {
            get { return _notification; }
            set { _notification = value; }
        }

        //priority
        private string _priority = "high";
        public string priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        //data
        private string _data = "##data##";
        public string data
        {
            get { return _data; }
            set { _data = value; }
        }

        //priority
        private string _registration_ids = string.Empty;
        public string registration_ids
        {
            get { return _registration_ids; }
            set { _registration_ids = value; }
        }
    }


    public class FcmNotification
    {
        //notification
        private NotificationList _data = new NotificationList();
        public NotificationList data
        {
            get { return _data; }
            set { _data = value; }
        }
        //notification
        private NotificationList _notification = new NotificationList();
        public NotificationList notification
        {
            get { return _notification; }
            set { _notification = value; }
        }

        //priority
        private string _priority = "high";
        public string priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        //data
        private string _notificationdata = "##data##";
        public string notificationdata
        {
            get { return _notificationdata; }
            set { _notificationdata = value; }
        }

        //priority
        private string _registration_ids = string.Empty;
        public string registration_ids
        {
            get { return _registration_ids; }
            set { _registration_ids = value; }
        }
    }
    public class FcmdataNotification
    {

        //priority
        private string _priority = "high";
        public string priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        //data
        private string _data = "##data##";
        public string data
        {
            get { return _data; }
            set { _data = value; }
        }

        //priority
        private string _registration_ids = string.Empty;
        public string registration_ids
        {
            get { return _registration_ids; }
            set { _registration_ids = value; }
        }
    }

    public class NotificationList
    {
        //title
        private string _title = string.Empty;
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        //text
        private string _text = string.Empty;
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        //body
        private string _body = string.Empty;
        public string body
        {
            get { return _body; }
            set { _body = value; }
        }

        //sound
        private string _sound = "default";
        public string sound
        {
            get { return _sound; }
            set { _sound = value; }
        }

        //badge
        private string _badge = "1";
        public string badge
        {
            get { return _badge; }
            set { _badge = value; }
        }

        //color
        private string _color = "#990000";
        public string color
        {
            get { return _color; }
            set { _color = value; }
        }

        //notificationtype
        private string _notificationtype = "##notificationtype##";
        public string notificationtype
        {
            get { return _notificationtype; }
            set { _notificationtype = value; }
        }
        //notificationredirecttype
        private string _notificationredirecttype = "##notificationredirecttype##";
        public string notificationredirecttype
        {
            get { return _notificationredirecttype; }
            set { _notificationredirecttype = value; }
        }
        public string URL = "##ExternalURL##";
        //image
        private string _image = string.Empty;
        public string image
        {
            get { return _image; }
            set { _image = value; }
        }
    }

    public class FcmResponse
    {
        //title
        private string _multicast_id = string.Empty;
        public string multicast_id
        {
            get { return _multicast_id; }
            set { _multicast_id = value; }
        }

        //success
        private int _success = 0;
        public int success
        {
            get { return _success; }
            set { _success = value; }
        }

        //failure
        private int _failure = 0;
        public int failure
        {
            get { return _failure; }
            set { _failure = value; }
        }


        //canonical_ids
        private int _canonical_ids = 0;
        public int canonical_ids
        {
            get { return _canonical_ids; }
            set { _canonical_ids = value; }
        }

        ////results
        //private ResultList _results = new ResultList();
        //public ResultList results
        //{
        //    get { return _results; }
        //    set { _results = value; }
        //}

    }

    public class ResultList
    {
        //results
        private string _message_id = string.Empty;
        public string message_id
        {
            get { return _message_id; }
            set { _message_id = value; }
        }

        //results
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}