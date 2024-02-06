using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{

    public class VotingLists : CommonAdd
    {
        bool DataRecieved = false;
        #region "Properties" 
        private Int64 _VotingCompetitionId = 0;
        public Int64 VotingCompetitionId
        {
            get { return _VotingCompetitionId; }
            set { _VotingCompetitionId = value; }
        }
        private String _VotingCandidateUniqueId = string.Empty;
        public String VotingCandidateUniqueId
        {
            get { return _VotingCandidateUniqueId; }
            set { _VotingCandidateUniqueId = value; }
        }
        private String _VotingCandidateName = string.Empty;
        public String VotingCandidateName
        {
            get { return _VotingCandidateName; }
            set { _VotingCandidateName = value; }
        }
        private Int64 _VotingPackageID = 0;
        public Int64 VotingPackageID
        {
            get { return _VotingPackageID; }
            set { _VotingPackageID = value; }
        }
        private Int32 _NoofVotes = 0;
        public Int32 NoofVotes
        {
            get { return _NoofVotes; }
            set { _NoofVotes = value; }
        }
        private Int64 _MemberID = 0;
        public Int64 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        private String _MemberContactNumber = string.Empty;
        public String MemberContactNumber
        {
            get { return _MemberContactNumber; }
            set { _MemberContactNumber = value; }
        }
        private String _MemberName = string.Empty;
        public String MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        private String _PlatForm = string.Empty;
        public String PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }
        private String _DeviceCode = string.Empty;
        public String DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }
        private String _IpAddress = string.Empty;
        public String IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        private String _TransactionUniqueId = string.Empty;
        public String TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        private Decimal _Amount = 0;
        public Decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private Int32 _FreeVotes = 0;
        public Int32 FreeVotes
        {
            get { return _FreeVotes; }
            set { _FreeVotes = value; }
        }
        private Int32 _PaidVotes = 0;
        public Int32 PaidVotes
        {
            get { return _PaidVotes; }
            set { _PaidVotes = value; }
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_VotingList_AddNew", HT);
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_VotingList_Update", HT);
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
            HT.Add("VotingCompetitionId", VotingCompetitionId);
            HT.Add("VotingCandidateUniqueId", VotingCandidateUniqueId);
            HT.Add("VotingCandidateName", VotingCandidateName);
            HT.Add("VotingPackageID", VotingPackageID);
            HT.Add("NoofVotes", NoofVotes);
            HT.Add("MemberID", MemberID);
            HT.Add("MemberContactNumber", MemberContactNumber);
            HT.Add("MemberName", MemberName);
            HT.Add("PlatForm", PlatForm);
            HT.Add("DeviceCode", DeviceCode);
            HT.Add("IpAddress", IpAddress);
            HT.Add("TransactionUniqueId", TransactionUniqueId);
            HT.Add("Amount", Amount);
            HT.Add("FreeVotes", FreeVotes);
            HT.Add("PaidVotes", PaidVotes);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
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
                HT.Add("VotingCompetitionId", VotingCompetitionId);
                HT.Add("VotingCandidateUniqueId", VotingCandidateUniqueId);
                HT.Add("VotingCandidateName", VotingCandidateName);
                HT.Add("VotingPackageID", VotingPackageID);
                HT.Add("NoofVotes", NoofVotes);
                HT.Add("MemberID", MemberID);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("MemberName", MemberName);
                HT.Add("PlatForm", PlatForm);
                HT.Add("DeviceCode", DeviceCode);
                HT.Add("IpAddress", IpAddress);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("Amount", Amount);
                HT.Add("FreeVotes", FreeVotes);
                HT.Add("PaidVotes", PaidVotes);
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

                dt = obj.GetDataFromStoredProcedure("sp_VotingList_Get", HT);
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
                HT.Add("VotingCompetitionId", VotingCompetitionId);
                HT.Add("VotingCandidateUniqueId", VotingCandidateUniqueId);
                HT.Add("VotingCandidateName", VotingCandidateName);
                HT.Add("VotingPackageID", VotingPackageID);
                HT.Add("NoofVotes", NoofVotes);
                HT.Add("MemberID", MemberID);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("MemberName", MemberName);
                HT.Add("PlatForm", PlatForm);
                HT.Add("DeviceCode", DeviceCode);
                HT.Add("IpAddress", IpAddress);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("Amount", Amount);
                HT.Add("FreeVotes", FreeVotes);
                HT.Add("PaidVotes", PaidVotes);
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

                dt = obj.GetDataFromStoredProcedure("sp_VotingList_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    VotingCompetitionId = Convert.ToInt64(dt.Rows[0]["VotingCompetitionId"].ToString());
                    VotingCandidateUniqueId = Convert.ToString(dt.Rows[0]["VotingCandidateUniqueId"].ToString());
                    VotingCandidateName = Convert.ToString(dt.Rows[0]["VotingCandidateName"].ToString());
                    VotingPackageID = Convert.ToInt64(dt.Rows[0]["VotingPackageID"].ToString());
                    NoofVotes = Convert.ToInt32(dt.Rows[0]["NoofVotes"].ToString());
                    MemberID = Convert.ToInt64(dt.Rows[0]["MemberID"].ToString());
                    MemberContactNumber = Convert.ToString(dt.Rows[0]["MemberContactNumber"].ToString());
                    MemberName = Convert.ToString(dt.Rows[0]["MemberName"].ToString());
                    PlatForm = Convert.ToString(dt.Rows[0]["PlatForm"].ToString());
                    DeviceCode = Convert.ToString(dt.Rows[0]["DeviceCode"].ToString());
                    IpAddress = Convert.ToString(dt.Rows[0]["IpAddress"].ToString());
                    TransactionUniqueId = Convert.ToString(dt.Rows[0]["TransactionUniqueId"].ToString());
                    Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    FreeVotes = Convert.ToInt32(dt.Rows[0]["FreeVotes"].ToString());
                    PaidVotes = Convert.ToInt32(dt.Rows[0]["PaidVotes"].ToString());
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
                HT.Add("VotingCompetitionId", VotingCompetitionId);
                HT.Add("VotingCandidateUniqueId", VotingCandidateUniqueId);
                HT.Add("VotingCandidateName", VotingCandidateName);
                HT.Add("VotingPackageID", VotingPackageID);
                HT.Add("NoofVotes", NoofVotes);
                HT.Add("MemberID", MemberID);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("MemberName", MemberName);
                HT.Add("PlatForm", PlatForm);
                HT.Add("DeviceCode", DeviceCode);
                HT.Add("IpAddress", IpAddress);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("Amount", Amount);
                HT.Add("FreeVotes", FreeVotes);
                HT.Add("PaidVotes", PaidVotes);
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

                dt = obj.GetDataFromStoredProcedure("sp_VotingList_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_VotingList_DatatableCounter", HT);
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

        public bool VotingSumUpdate()
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("VotingCompetitionID", VotingCompetitionId);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_VotingSum_Get", HT);
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
        #endregion

    }


}