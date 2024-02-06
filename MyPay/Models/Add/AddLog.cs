using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddLog : CommonAdd
    {
        #region "Enum"
        public enum LogType
        {
            Transaction = 1,
            User = 2,
            Kyc = 3,
            Rates = 4,
            Utility = 5,
            AgentCollector = 6,
            AgentPayout = 7,
            Ticket = 8,
            Wallet = 9,
            Maintenance = 10,
            ApiRequests = 11,
            BankTransfer = 12,
            ConnectIps_Deposit = 13,
            CardPayment = 14,
            Voting = 15,
            Linked_BankTransfer = 16,
            Internet_Banking = 17,
            Mobile_Banking = 18,
            Cashback = 19,
            Credit_Card_Payment = 20,
            DBLogs = 21,
            DLRLookup = 22,
            Merchant = 23,
            RewardPoints = 24,
            ExcelNotification = 25,
            BalanceHistory = 26,
            LinkBankAcccount = 27,
            Remittance = 28,
            MerchantBankTransfer = 29,
            ProviderService = 30,
            VersionUpdate = 31,
            DealsandOffers = 32,
            Agent = 33
        }

        public enum LogActivityEnum
        {
            Admin_Wallet_Update = 1,
            Complete_User_Profile = 2,
            Login_With_Pin = 3,
            Email = 4,
            SMS = 5,
            Password_Reset = 6,
            Password_Reset_Admin = 7,
            User_Register = 8,
            User_Profile_Update = 9,
            Created_Pin = 10,
            Register_Verification = 11,
            Login_Admin = 12,
            Logout_Admin = 13,
            Login_user = 14,
            Logout_user = 15,
            Cashback = 16,
            Fund_Transfer = 17,
            Role_Assign = 18,
            Registration_Commission = 19,
            KYC_Commission = 20,
            First_Transaction_Commission = 21,
            Gift_Cashback = 22,
            User_Export = 23,
            Transaction_Export = 24,
            MyPay_Notification = 25,
            ChangeTxnStatus = 26,
            MerchantTransactions = 27,
            Login_Merchant = 28,
            Logout_Merchant = 29,
            Admin_RewardPoints_Update = 30,
            EOD_Update = 31,
            Gift_MPCoins = 32,
            FonePayTransactions = 33,
            Flight_Airlines = 34,
            Reset_Merchant_Keys = 35,
            Approve_Withdrawal_Requests = 36,
            Login_Remittance = 37,
            Logout_Remittance = 38,
            Validate_Account = 39,
            Remittance_Assign_Currency = 40,
            BankPostData = 41,
            Voting_Export = 42,
            MerchantCommission = 43,
            ProviderService_Status_Change = 44
        }
        #endregion

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //LogActivity
        private int _LogActivity = 0;
        public int LogActivity
        {
            get { return _LogActivity; }
            set { _LogActivity = value; }
        }
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        //DeviceCode
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        //Platform
        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        //IsMobile
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }

        //Action
        private string _Action = string.Empty;
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        //UserId
        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        //OldUserStatusName
        private string _OldUserStatusName = string.Empty;
        public string OldUserStatusName
        {
            get { return _OldUserStatusName; }
            set { _OldUserStatusName = value; }
        }
        //UserTypeName
        private string _UserTypeName = string.Empty;
        public string UserTypeName
        {
            get { return _UserTypeName; }
            set { _UserTypeName = value; }
        }


        //OldUserStatus
        private int _OldUserStatus = 0;
        public int OldUserStatus
        {
            get { return _OldUserStatus; }
            set { _OldUserStatus = value; }
        }
        //UserType
        private int _UserType = 0;
        public int UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //IsAdmin
        private bool _IsAdmin = false;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        //UserMemberId
        private Int64 _UserMemberId = 0;
        public Int64 UserMemberId
        {
            get { return _UserMemberId; }
            set { _UserMemberId = value; }
        }

        //CheckDelete
        private int _CheckDelete = 2;// (int)clsData.BooleanValue.Both;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        //EndDate 
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        //StartDate 
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }

        }

        //TypeName
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("MemberId", MemberId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Type", Type);
                HT.Add("UserId", UserId);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("LogActivity", LogActivity);
                HT.Add("UserType", UserType);
                HT.Add("OldUserStatus", OldUserStatus);
                HT.Add("ContactNumber", ContactNumber);
                dt = obj.GetDataFromStoredProcedure("sp_Logs_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_Logs_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                        //dt.Rows[0]["TotalSuccess"] = dtCounter.Rows[0]["TotalSuccess"].ToString();
                        //dt.Rows[0]["TotalPending"] = dtCounter.Rows[0]["TotalPending"].ToString();
                        //dt.Rows[0]["TotalFailed"] = dtCounter.Rows[0]["TotalFailed"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

    }
}