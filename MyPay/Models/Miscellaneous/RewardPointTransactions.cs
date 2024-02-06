using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{
    public class RewardPointTransactions
    {
        #region "Enums"
        public enum Signs
        {
            Credit = 1,
            Debit = 2
        }

        public enum RewardTypes
        {
            Registration = 1,
            Kyc = 2,
            Transaction = 3,
            Redeem = 47,
            MPCoinsUpdate_By_Admin = 4,
            Gift_MPCoins = 87
        }

        public enum Types
        {
            Paused = 1,
            Cancelled = 2,
            Completed = 3,
            Rejected = 4,
            Reciept_Unverified = 5,
            Recieved = 6,
            Started = 7,
            Pending = 8,
            Amount_NotMatched = 9,
            Refund = 10
        }
        #endregion"

        #region "Variables"
        private string filename = "RewardPointTransactions.cs";
        #endregion

        #region "Properties"
        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
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
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //MemberName
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        //SignName
        private string _SignName = string.Empty;
        public string SignName
        {
            get { return _SignName; }
            set { _SignName = value; }
        }

        //TypeName
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        //Sign
        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        //Sign
        private int _recordTotal = 0;
        public int recordTotal
        {
            get { return _recordTotal; }
            set { _recordTotal = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

   

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

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

        //CreatedBy
        private Int64 _CreatedBy = 0;
        public Int64 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        //CreatedByName
        private string _CreatedByName = string.Empty;
        public string CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }

        //StartDate 
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //EndDate 
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
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

        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
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

        //CheckCreatedDate
        private string _CheckCreatedDate = string.Empty;
        public string CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        //Sno
        private Int64 _Sno = 0;
        public Int64 Sno
        {
            get { return _Sno; }
            set { _Sno = value; }
        }

        //CreatedDatedt
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        //VendorServiceID
        private int _VendorServiceID = 0;
        public int VendorServiceID
        {
            get { return _VendorServiceID; }
            set { _VendorServiceID = value; }
        }

        //VendorServiceName 
        private string _VendorServiceName = string.Empty;
        public string VendorServiceName
        {
            get { return _VendorServiceName; }
            set { _VendorServiceName = value; }
        }
        #endregion

        #region "Add Delete Update Methods"
        public bool Add()
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = SetObject();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RewardPointTransactions_AddNew", HT);
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
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RewardPointTransactions_Update", HT);
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
            Ht.Add("MemberId", MemberId);
            Ht.Add("MemberName", MemberName);
            Ht.Add("Sign", Sign);
            Ht.Add("Amount", Amount);
            Ht.Add("Description", Description);
            Ht.Add("Type", Type);
            Ht.Add("Reference", Reference);
            Ht.Add("Remarks", Remarks);
            Ht.Add("CurrentBalance", CurrentBalance);
            Ht.Add("TransactionUniqueId", TransactionUniqueId);
            Ht.Add("VendorTransactionId", VendorTransactionId);
            Ht.Add("ParentTransactionId", ParentTransactionId);
            Ht.Add("Status", Status);
            Ht.Add("CreatedDate", CreatedDate);
            Ht.Add("UpdatedDate", UpdatedDate);
            Ht.Add("IsDeleted", IsDeleted);
            Ht.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            Ht.Add("IsActive", IsActive);
            Ht.Add("CreatedBy", CreatedBy);
            Ht.Add("CreatedByName", CreatedByName);
            Ht.Add("VendorServiceID", VendorServiceID);
            return Ht;
        }
        #endregion

        #region "Get Methods"
        public DataTable GetList()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberId", MemberId);
                HT.Add("MemberName", MemberName);
                HT.Add("Sign", Sign);
                HT.Add("Amount", Amount);
                HT.Add("Type", Type);
                HT.Add("Reference", Reference);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Status", Status);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("VendorServiceID", VendorServiceID);
                dt = obj.GetDataFromStoredProcedure("sp_RewardPointTransactions_Get", HT);
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
            DataRecieved = false;
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberId", MemberId);
                HT.Add("MemberName", MemberName);
                HT.Add("Sign", Sign);
                HT.Add("Amount", Amount);
                HT.Add("Type", Type);
                HT.Add("Reference", Reference);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Status", Status);
                HT.Add("Take", 1);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("VendorServiceID", VendorServiceID);
                dt = obj.GetDataFromStoredProcedure("sp_RewardPointTransactions_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"].ToString());
                    MemberName = dt.Rows[0]["MemberName"].ToString();
                    Sign = Convert.ToInt32(dt.Rows[0]["Sign"].ToString());
                    Type = Convert.ToInt32(dt.Rows[0]["Type"].ToString());
                    Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    CurrentBalance = Convert.ToDecimal(dt.Rows[0]["CurrentBalance"].ToString());
                    Description = dt.Rows[0]["Description"].ToString();
                    Remarks = dt.Rows[0]["Remarks"].ToString();
                    Reference = dt.Rows[0]["Reference"].ToString();
                    TransactionUniqueId = dt.Rows[0]["TransactionUniqueId"].ToString();
                    VendorTransactionId = dt.Rows[0]["VendorTransactionId"].ToString();
                    ParentTransactionId = dt.Rows[0]["ParentTransactionId"].ToString();
                    Status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = dt.Rows[0]["CreatedByName"].ToString();
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    VendorServiceID = Convert.ToInt32(dt.Rows[0]["VendorServiceID"].ToString());
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
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
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("MemberName", MemberName);
                HT.Add("Sign", Sign);
                HT.Add("VendorServiceID", VendorServiceID);
                dt = obj.GetDataFromStoredProcedure("sp_RewardPointTransactions_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_RewardPointTransactions_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                        dt.Rows[0]["AmountSum"] = dtCounter.Rows[0]["AmountSum"].ToString();
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


