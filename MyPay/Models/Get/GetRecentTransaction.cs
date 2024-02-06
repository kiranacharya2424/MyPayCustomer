using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetRecentTransaction : CommonGet
    {
        #region "Properties"

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        //CurrentBalance
        private decimal _CurrentBalance = 0;
        public decimal CurrentBalance
        {
            get { return _CurrentBalance; }
            set { _CurrentBalance = value; }
        }
        //EndDate
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //RecieverId
        private Int64 _RecieverId = 0;
        public Int64 RecieverId
        {
            get { return _RecieverId; }
            set { _RecieverId = value; }
        }
        //MemberName
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //Sign

        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        //StartDate
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private string _sortOrder = string.Empty;
        public string sortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
        private int _OffsetValue = 0;
        public int OffsetValue
        {
            get { return _OffsetValue; }
            set { _OffsetValue = value; }
        } 
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //SearchText
        private string _SearchText = string.Empty;
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }

        //VendorTransactionId
        private string _VendorTransactionId = string.Empty;
        public string VendorTransactionId
        {
            get { return _VendorTransactionId; }
            set { _VendorTransactionId = value; }
        }
        //ParentTransactionId
        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }
        //CreatedByName
        private string _CheckCreatedDate = string.Empty;
        public string CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }

        //ThreeMonth
        private string _ThreeMonth = string.Empty;
        public string ThreeMonth
        {
            get { return _ThreeMonth; }
            set { _ThreeMonth = value; }
        }

        //SixMonth
        private string _SixMonth = string.Empty;
        public string SixMonth
        {
            get { return _SixMonth; }
            set { _SixMonth = value; }
        }

        //Year
        private string _Year = string.Empty;
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        //Today
        private string _Today = string.Empty;
        public string Today
        {
            get { return _Today; }
            set { _Today = value; }
        }

        //Weekly
        private string _Weekly = string.Empty;
        public string Weekly
        {
            get { return _Weekly; }
            set { _Weekly = value; }
        }

        //Monthly
        private string _Monthly = string.Empty;
        public string Monthly
        {
            get { return _Monthly; }
            set { _Monthly = value; }
        }

        //Yesterday
        private string _Yesterday = string.Empty;
        public string Yesterday
        {
            get { return _Yesterday; }
            set { _Yesterday = value; }
        }

        //CustomerId
        private string _CustomerID = string.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        public bool DataRecieved { get; private set; }

        private bool _IsFavourite = false;
        public bool IsFavourite
        {
            get { return _IsFavourite; }
            set { _IsFavourite = value; }
        }
        #endregion

        public DataTable GetRecord()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("sortOrder", sortOrder);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("MemberId", MemberId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Type", Type);
                HT.Add("CustomerID", CustomerID);
                HT.Add("RoleId", RoleId);

                dt = obj.GetDataFromStoredProcedure("sp_RecentTransactions_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Amount = Convert.ToInt64(dt.Rows[0]["Amount"]);
                    CustomerID = Convert.ToString(dt.Rows[0]["CustomerID"]);
                    ContactNumber = Convert.ToString(dt.Rows[0]["ContactNumber"]);
                    IsFavourite = Convert.ToBoolean(dt.Rows[0]["IsFavourite"]);
                    DataRecieved = true;
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
