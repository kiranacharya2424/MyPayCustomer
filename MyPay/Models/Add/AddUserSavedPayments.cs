using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{

    public class AddUserSavedPayments : CommonAdd
    {
        bool DataRecieved = false;
        #region "Properties" 
        private Int32 _ServiceID = 0;
        public Int32 ServiceID
        {
            get { return _ServiceID; }
            set { _ServiceID = value; }
        }
        private String _MobileNumber = string.Empty;
        public String MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }
        private Decimal _Amount = 0;
        public Decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private String _SubscriberID = string.Empty;
        public String SubscriberID
        {
            get { return _SubscriberID; }
            set { _SubscriberID = value; }
        }
        private String _CasID = string.Empty;
        public String CasID
        {
            get { return _CasID; }
            set { _CasID = value; }
        }
        private String _PackageID = string.Empty;
        public String PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }
        private String _PackageName = string.Empty;
        public String PackageName
        {
            get { return _PackageName; }
            set { _PackageName = value; }
        }
        private String _CustomerID = string.Empty;
        public String CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        private String _CustomerName = string.Empty;
        public String CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private String _OldWardNumber = string.Empty;
        public String OldWardNumber
        {
            get { return _OldWardNumber; }
            set { _OldWardNumber = value; }
        }
        private String _STB = string.Empty;
        public String STB
        {
            get { return _STB; }
            set { _STB = value; }
        }
        private String _UserName = string.Empty;
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private String _FullName = string.Empty;
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        private String _LandlineNumber = string.Empty;
        public String LandlineNumber
        {
            get { return _LandlineNumber; }
            set { _LandlineNumber = value; }
        }
        private String _Address = string.Empty;
        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private String _CounterID = string.Empty;
        public String CounterID
        {
            get { return _CounterID; }
            set { _CounterID = value; }
        }
        private String _CounterName = string.Empty;
        public String CounterName
        {
            get { return _CounterName; }
            set { _CounterName = value; }
        }
        private String _ScNumber = string.Empty;
        public String ScNumber
        {
            get { return _ScNumber; }
            set { _ScNumber = value; }
        }
        private String _ConsumerID = string.Empty;
        public String ConsumerID
        {
            get { return _ConsumerID; }
            set { _ConsumerID = value; }
        }
        private String _SubscriptionID = string.Empty;
        public String SubscriptionID
        {
            get { return _SubscriptionID; }
            set { _SubscriptionID = value; }
        }
        private String _SubscriptionName = string.Empty;
        public String SubscriptionName
        {
            get { return _SubscriptionName; }
            set { _SubscriptionName = value; }
        }
        private String _AcceptanceNo = string.Empty;
        public String AcceptanceNo
        {
            get { return _AcceptanceNo; }
            set { _AcceptanceNo = value; }
        }
        private String _PolicyNumber = string.Empty;
        public String PolicyNumber
        {
            get { return _PolicyNumber; }
            set { _PolicyNumber = value; }
        }
        private String _DateOfBirth = string.Empty;
        public String DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        private String _InsuranceID = string.Empty;
        public String InsuranceID
        {
            get { return _InsuranceID; }
            set { _InsuranceID = value; }
        }
        private String _InsuranceName = string.Empty;
        public String InsuranceName
        {
            get { return _InsuranceName; }
            set { _InsuranceName = value; }
        }
        private String _DebitNoteNo = string.Empty;
        public String DebitNoteNo
        {
            get { return _DebitNoteNo; }
            set { _DebitNoteNo = value; }
        }
        private String _Email = string.Empty;
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private String _PolicyType = string.Empty;
        public String PolicyType
        {
            get { return _PolicyType; }
            set { _PolicyType = value; }
        }
        private String _Branch = string.Empty;
        public String Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }
        private String _PolicyCategory = string.Empty;
        public String PolicyCategory
        {
            get { return _PolicyCategory; }
            set { _PolicyCategory = value; }
        }
        private String _PolicyDescription = string.Empty;
        public String PolicyDescription
        {
            get { return _PolicyDescription; }
            set { _PolicyDescription = value; }
        }
        private String _ChitNumber = string.Empty;
        public String ChitNumber
        {
            get { return _ChitNumber; }
            set { _ChitNumber = value; }
        }
        private String _FiscalYearID = string.Empty;
        public String FiscalYearID
        {
            get { return _FiscalYearID; }
            set { _FiscalYearID = value; }
        }
        private String _FiscalYearValue = string.Empty;
        public String FiscalYearValue
        {
            get { return _FiscalYearValue; }
            set { _FiscalYearValue = value; }
        }
        private String _ProvinceID = string.Empty;
        public String ProvinceID
        {
            get { return _ProvinceID; }
            set { _ProvinceID = value; }
        }
        private String _ProvinceName = string.Empty;
        public String ProvinceName
        {
            get { return _ProvinceName; }
            set { _ProvinceName = value; }
        }
        private String _DistrictID = string.Empty;
        public String DistrictID
        {
            get { return _DistrictID; }
            set { _DistrictID = value; }
        }
        private String _DistrictValue = string.Empty;
        public String DistrictValue
        {
            get { return _DistrictValue; }
            set { _DistrictValue = value; }
        }
        private String _BankID = string.Empty;
        public String BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }
        private String _BankName = string.Empty;
        public String BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        private String _CreditCardNumber = string.Empty;
        public String CreditCardNumber
        {
            get { return _CreditCardNumber; }
            set { _CreditCardNumber = value; }
        }
        private String _CreditCardOwner = string.Empty;
        public String CreditCardOwner
        {
            get { return _CreditCardOwner; }
            set { _CreditCardOwner = value; }
        }
        private Int32 _Take = 0;
        public Int32 Take
        {
            get { return _Take; }
            set { _Take = value; }
        }
        private Int32 _Skip = 0;
        public Int32 Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
        private Int32 _CheckDelete = 2;
        public Int32 CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }
        private Int32 _CheckActive = 2;
        public Int32 CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }
        private Int32 _CheckApprovedByAdmin = 2;
        public Int32 CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }
        private String _CheckCreatedDate = string.Empty;
        public String CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }
        private String _StartDate = string.Empty;
        public String StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private String _EndDate = string.Empty;
        public String EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private Int64 _MemberID = 0;
        public Int64 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        private String _PaymentName = string.Empty;
        public String PaymentName
        {
            get { return _PaymentName; }
            set { _PaymentName = value; }
        }
        private Int32 _IsSchedulePayment = 0;
        public Int32 IsSchedulePayment
        {
            get { return _IsSchedulePayment; }
            set { _IsSchedulePayment = value; }
        }
        private DateTime _ScheduleDate = System.DateTime.UtcNow;
        public DateTime ScheduleDate
        {
            get { return _ScheduleDate; }
            set { _ScheduleDate = value; }
        }
        private string _CheckScheduleDate = string.Empty;
        public string CheckScheduleDate
        {
            get { return _CheckScheduleDate; }
            set { _CheckScheduleDate = value; }
        }
        private Int32 _PaymentCycle = 0;
        public Int32 PaymentCycle
        {
            get { return _PaymentCycle; }
            set { _PaymentCycle = value; }
        }
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        #endregion

        #region "Add Delete Update Methods" 
        public bool Add()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_UserSavedPayments_AddNew", HT);
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

        public bool Update()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_UserSavedPayments_Update", HT);
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

        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("ServiceID", ServiceID);
            HT.Add("MobileNumber", MobileNumber);
            HT.Add("Amount", Amount);
            HT.Add("SubscriberID", SubscriberID);
            HT.Add("CasID", CasID);
            HT.Add("PackageID", PackageID);
            HT.Add("PackageName", PackageName);
            HT.Add("CustomerID", CustomerID);
            HT.Add("CustomerName", CustomerName);
            HT.Add("OldWardNumber", OldWardNumber);
            HT.Add("STB", STB);
            HT.Add("UserName", UserName);
            HT.Add("FullName", FullName);
            HT.Add("LandlineNumber", LandlineNumber);
            HT.Add("Address", Address);
            HT.Add("CounterID", CounterID);
            HT.Add("CounterName", CounterName);
            HT.Add("ScNumber", ScNumber);
            HT.Add("ConsumerID", ConsumerID);
            HT.Add("SubscriptionID", SubscriptionID);
            HT.Add("SubscriptionName", SubscriptionName);
            HT.Add("AcceptanceNo", AcceptanceNo);
            HT.Add("PolicyNumber", PolicyNumber);
            HT.Add("DateOfBirth", DateOfBirth);
            HT.Add("InsuranceID", InsuranceID);
            HT.Add("InsuranceName", InsuranceName);
            HT.Add("DebitNoteNo", DebitNoteNo);
            HT.Add("Email", Email);
            HT.Add("PolicyType", PolicyType);
            HT.Add("Branch", Branch);
            HT.Add("PolicyCategory", PolicyCategory);
            HT.Add("PolicyDescription", PolicyDescription);
            HT.Add("ChitNumber", ChitNumber);
            HT.Add("FiscalYearID", FiscalYearID);
            HT.Add("FiscalYearValue", FiscalYearValue);
            HT.Add("ProvinceID", ProvinceID);
            HT.Add("ProvinceName", ProvinceName);
            HT.Add("DistrictID", DistrictID);
            HT.Add("DistrictValue", DistrictValue);
            HT.Add("BankID", BankID);
            HT.Add("BankName", BankName);
            HT.Add("CreditCardNumber", CreditCardNumber);
            HT.Add("CreditCardOwner", CreditCardOwner);
            HT.Add("MemberID", MemberID);
            HT.Add("PaymentName", PaymentName);
            HT.Add("IsSchedulePayment", IsSchedulePayment);
            HT.Add("ScheduleDate", ScheduleDate);
            HT.Add("PaymentCycle", PaymentCycle);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);

            return HT;
        }
        #endregion

        #region "Get Methods" 
        public System.Data.DataTable GetList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("ServiceID", ServiceID);
                HT.Add("MobileNumber", MobileNumber);
                HT.Add("Amount", Amount);
                HT.Add("SubscriberID", SubscriberID);
                HT.Add("CasID", CasID);
                HT.Add("PackageID", PackageID);
                HT.Add("PackageName", PackageName);
                HT.Add("CustomerID", CustomerID);
                HT.Add("CustomerName", CustomerName);
                HT.Add("OldWardNumber", OldWardNumber);
                HT.Add("STB", STB);
                HT.Add("UserName", UserName);
                HT.Add("FullName", FullName);
                HT.Add("LandlineNumber", LandlineNumber);
                HT.Add("Address", Address);
                HT.Add("CounterID", CounterID);
                HT.Add("CounterName", CounterName);
                HT.Add("ScNumber", ScNumber);
                HT.Add("ConsumerID", ConsumerID);
                HT.Add("SubscriptionID", SubscriptionID);
                HT.Add("SubscriptionName", SubscriptionName);
                HT.Add("AcceptanceNo", AcceptanceNo);
                HT.Add("PolicyNumber", PolicyNumber);
                HT.Add("DateOfBirth", DateOfBirth);
                HT.Add("InsuranceID", InsuranceID);
                HT.Add("InsuranceName", InsuranceName);
                HT.Add("DebitNoteNo", DebitNoteNo);
                HT.Add("Email", Email);
                HT.Add("PolicyType", PolicyType);
                HT.Add("Branch", Branch);
                HT.Add("PolicyCategory", PolicyCategory);
                HT.Add("PolicyDescription", PolicyDescription);
                HT.Add("ChitNumber", ChitNumber);
                HT.Add("FiscalYearID", FiscalYearID);
                HT.Add("FiscalYearValue", FiscalYearValue);
                HT.Add("ProvinceID", ProvinceID);
                HT.Add("ProvinceName", ProvinceName);
                HT.Add("DistrictID", DistrictID);
                HT.Add("DistrictValue", DistrictValue);
                HT.Add("BankID", BankID);
                HT.Add("BankName", BankName);
                HT.Add("CreditCardNumber", CreditCardNumber);
                HT.Add("CreditCardOwner", CreditCardOwner);
                HT.Add("MemberID", MemberID);
                HT.Add("PaymentName", PaymentName);
                HT.Add("IsSchedulePayment", IsSchedulePayment);
                HT.Add("CheckScheduleDate", CheckScheduleDate);
                HT.Add("PaymentCycle", PaymentCycle);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_UserSavedPayments_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public bool GetRecord()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("ServiceID", ServiceID);
                HT.Add("MobileNumber", MobileNumber);
                HT.Add("Amount", Amount);
                HT.Add("SubscriberID", SubscriberID);
                HT.Add("CasID", CasID);
                HT.Add("PackageID", PackageID);
                HT.Add("PackageName", PackageName);
                HT.Add("CustomerID", CustomerID);
                HT.Add("CustomerName", CustomerName);
                HT.Add("OldWardNumber", OldWardNumber);
                HT.Add("STB", STB);
                HT.Add("UserName", UserName);
                HT.Add("FullName", FullName);
                HT.Add("LandlineNumber", LandlineNumber);
                HT.Add("Address", Address);
                HT.Add("CounterID", CounterID);
                HT.Add("CounterName", CounterName);
                HT.Add("ScNumber", ScNumber);
                HT.Add("ConsumerID", ConsumerID);
                HT.Add("SubscriptionID", SubscriptionID);
                HT.Add("SubscriptionName", SubscriptionName);
                HT.Add("AcceptanceNo", AcceptanceNo);
                HT.Add("PolicyNumber", PolicyNumber);
                HT.Add("DateOfBirth", DateOfBirth);
                HT.Add("InsuranceID", InsuranceID);
                HT.Add("InsuranceName", InsuranceName);
                HT.Add("DebitNoteNo", DebitNoteNo);
                HT.Add("Email", Email);
                HT.Add("PolicyType", PolicyType);
                HT.Add("Branch", Branch);
                HT.Add("PolicyCategory", PolicyCategory);
                HT.Add("PolicyDescription", PolicyDescription);
                HT.Add("ChitNumber", ChitNumber);
                HT.Add("FiscalYearID", FiscalYearID);
                HT.Add("FiscalYearValue", FiscalYearValue);
                HT.Add("ProvinceID", ProvinceID);
                HT.Add("ProvinceName", ProvinceName);
                HT.Add("DistrictID", DistrictID);
                HT.Add("DistrictValue", DistrictValue);
                HT.Add("BankID", BankID);
                HT.Add("BankName", BankName);
                HT.Add("CreditCardNumber", CreditCardNumber);
                HT.Add("CreditCardOwner", CreditCardOwner);
                HT.Add("MemberID", MemberID);
                HT.Add("PaymentName", PaymentName);
                HT.Add("IsSchedulePayment", IsSchedulePayment);
                HT.Add("CheckScheduleDate", CheckScheduleDate);
                HT.Add("PaymentCycle", PaymentCycle);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_UserSavedPayments_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ServiceID = Convert.ToInt32(dt.Rows[0]["ServiceID"].ToString());
                    MobileNumber = Convert.ToString(dt.Rows[0]["MobileNumber"].ToString());
                    Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    SubscriberID = Convert.ToString(dt.Rows[0]["SubscriberID"].ToString());
                    CasID = Convert.ToString(dt.Rows[0]["CasID"].ToString());
                    PackageID = Convert.ToString(dt.Rows[0]["PackageID"].ToString());
                    PackageName = Convert.ToString(dt.Rows[0]["PackageName"].ToString());
                    CustomerID = Convert.ToString(dt.Rows[0]["CustomerID"].ToString());
                    CustomerName = Convert.ToString(dt.Rows[0]["CustomerName"].ToString());
                    OldWardNumber = Convert.ToString(dt.Rows[0]["OldWardNumber"].ToString());
                    STB = Convert.ToString(dt.Rows[0]["STB"].ToString());
                    UserName = Convert.ToString(dt.Rows[0]["UserName"].ToString());
                    FullName = Convert.ToString(dt.Rows[0]["FullName"].ToString());
                    LandlineNumber = Convert.ToString(dt.Rows[0]["LandlineNumber"].ToString());
                    Address = Convert.ToString(dt.Rows[0]["Address"].ToString());
                    CounterID = Convert.ToString(dt.Rows[0]["CounterID"].ToString());
                    CounterName = Convert.ToString(dt.Rows[0]["CounterName"].ToString());
                    ScNumber = Convert.ToString(dt.Rows[0]["ScNumber"].ToString());
                    ConsumerID = Convert.ToString(dt.Rows[0]["ConsumerID"].ToString());
                    SubscriptionID = Convert.ToString(dt.Rows[0]["SubscriptionID"].ToString());
                    SubscriptionName = Convert.ToString(dt.Rows[0]["SubscriptionName"].ToString());
                    AcceptanceNo = Convert.ToString(dt.Rows[0]["AcceptanceNo"].ToString());
                    PolicyNumber = Convert.ToString(dt.Rows[0]["PolicyNumber"].ToString());
                    DateOfBirth = Convert.ToString(dt.Rows[0]["DateOfBirth"].ToString());
                    InsuranceID = Convert.ToString(dt.Rows[0]["InsuranceID"].ToString());
                    InsuranceName = Convert.ToString(dt.Rows[0]["InsuranceName"].ToString());
                    DebitNoteNo = Convert.ToString(dt.Rows[0]["DebitNoteNo"].ToString());
                    Email = Convert.ToString(dt.Rows[0]["Email"].ToString());
                    PolicyType = Convert.ToString(dt.Rows[0]["PolicyType"].ToString());
                    Branch = Convert.ToString(dt.Rows[0]["Branch"].ToString());
                    PolicyCategory = Convert.ToString(dt.Rows[0]["PolicyCategory"].ToString());
                    PolicyDescription = Convert.ToString(dt.Rows[0]["PolicyDescription"].ToString());
                    ChitNumber = Convert.ToString(dt.Rows[0]["ChitNumber"].ToString());
                    FiscalYearID = Convert.ToString(dt.Rows[0]["FiscalYearID"].ToString());
                    FiscalYearValue = Convert.ToString(dt.Rows[0]["FiscalYearValue"].ToString());
                    ProvinceID = Convert.ToString(dt.Rows[0]["ProvinceID"].ToString());
                    ProvinceName = Convert.ToString(dt.Rows[0]["ProvinceName"].ToString());
                    DistrictID = Convert.ToString(dt.Rows[0]["DistrictID"].ToString());
                    DistrictValue = Convert.ToString(dt.Rows[0]["DistrictValue"].ToString());
                    BankID = Convert.ToString(dt.Rows[0]["BankID"].ToString());
                    BankName = Convert.ToString(dt.Rows[0]["BankName"].ToString());
                    CreditCardNumber = Convert.ToString(dt.Rows[0]["CreditCardNumber"].ToString());
                    CreditCardOwner = Convert.ToString(dt.Rows[0]["CreditCardOwner"].ToString());
                    MemberID = Convert.ToInt64(dt.Rows[0]["MemberID"].ToString());
                    PaymentName = Convert.ToString(dt.Rows[0]["PaymentName"].ToString());
                    IsSchedulePayment = Convert.ToInt32(dt.Rows[0]["IsSchedulePayment"].ToString());
                    ScheduleDate = Convert.ToDateTime(dt.Rows[0]["ScheduleDate"].ToString());
                    PaymentCycle = Convert.ToInt32(dt.Rows[0]["PaymentCycle"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = Convert.ToString(dt.Rows[0]["CreatedByName"].ToString());
                    Sno = Convert.ToString(dt.Rows[0]["Sno"].ToString());
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    UpdatedBy = Convert.ToInt64(dt.Rows[0]["UpdatedBy"].ToString());
                    UpdatedByName = Convert.ToString(dt.Rows[0]["UpdatedByName"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());

                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public System.Data.DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("ServiceID", ServiceID);
                HT.Add("MobileNumber", MobileNumber);
                HT.Add("Amount", Amount);
                HT.Add("SubscriberID", SubscriberID);
                HT.Add("CasID", CasID);
                HT.Add("PackageID", PackageID);
                HT.Add("PackageName", PackageName);
                HT.Add("CustomerID", CustomerID);
                HT.Add("CustomerName", CustomerName);
                HT.Add("OldWardNumber", OldWardNumber);
                HT.Add("STB", STB);
                HT.Add("UserName", UserName);
                HT.Add("FullName", FullName);
                HT.Add("LandlineNumber", LandlineNumber);
                HT.Add("Address", Address);
                HT.Add("CounterID", CounterID);
                HT.Add("CounterName", CounterName);
                HT.Add("ScNumber", ScNumber);
                HT.Add("ConsumerID", ConsumerID);
                HT.Add("SubscriptionID", SubscriptionID);
                HT.Add("SubscriptionName", SubscriptionName);
                HT.Add("AcceptanceNo", AcceptanceNo);
                HT.Add("PolicyNumber", PolicyNumber);
                HT.Add("DateOfBirth", DateOfBirth);
                HT.Add("InsuranceID", InsuranceID);
                HT.Add("InsuranceName", InsuranceName);
                HT.Add("DebitNoteNo", DebitNoteNo);
                HT.Add("Email", Email);
                HT.Add("PolicyType", PolicyType);
                HT.Add("Branch", Branch);
                HT.Add("PolicyCategory", PolicyCategory);
                HT.Add("PolicyDescription", PolicyDescription);
                HT.Add("ChitNumber", ChitNumber);
                HT.Add("FiscalYearID", FiscalYearID);
                HT.Add("FiscalYearValue", FiscalYearValue);
                HT.Add("ProvinceID", ProvinceID);
                HT.Add("ProvinceName", ProvinceName);
                HT.Add("DistrictID", DistrictID);
                HT.Add("DistrictValue", DistrictValue);
                HT.Add("BankID", BankID);
                HT.Add("BankName", BankName);
                HT.Add("CreditCardNumber", CreditCardNumber);
                HT.Add("CreditCardOwner", CreditCardOwner);
                HT.Add("MemberID", MemberID);
                HT.Add("PaymentName", PaymentName);
                HT.Add("IsSchedulePayment", IsSchedulePayment);
                HT.Add("CheckScheduleDate", CheckScheduleDate);
                HT.Add("PaymentCycle", PaymentCycle);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_UserSavedPayments_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_UserSavedPayments_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }
       
        #endregion

    }


}