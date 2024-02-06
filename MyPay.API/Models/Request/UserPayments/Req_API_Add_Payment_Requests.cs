using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_API_Add_Payment_Requests : CommonProp
    {
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
        
        private Int32 _PaymentCycle = 0;
        public Int32 PaymentCycle
        {
            get { return _PaymentCycle; }
            set { _PaymentCycle = value; }
        }
     }
}