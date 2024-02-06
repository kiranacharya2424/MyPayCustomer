using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVotingList:CommonGet
    {
        #region "Properties"

        //VotingCompetitionID
        private Int64 _VotingCompetitionID = 0;
        public Int64 VotingCompetitionID
        {
            get { return _VotingCompetitionID; }
            set { _VotingCompetitionID = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId=String.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        //PackageID
        private Int64 _VotingPackageID =0;
        public Int64 VotingPackageID
        {
            get { return _VotingPackageID; }
            set { _VotingPackageID = value; }
        }
        //VotingCandidateUniqueId
        private string _VotingCandidateUniqueId = string.Empty;
        public string VotingCandidateUniqueId
        {
            get { return _VotingCandidateUniqueId; }
            set { _VotingCandidateUniqueId = value; }
        }

        //VotingCandidateName
        private string _VotingCandidateName = string.Empty;
        public string VotingCandidateName
        {
            get { return _VotingCandidateName; }
            set { _VotingCandidateName = value; }
        }

        //NoofVotes
        private int _NoofVotes = 0;
        public int NoofVotes
        {
            get { return _NoofVotes; }
            set { _NoofVotes = value; }
        }
       
        //MemberID
        private Int64 _MemberID = 0;
        public Int64 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        //MemberContactNumber
        private string _MemberContactNumber = string.Empty;
        public string MemberContactNumber
        {
            get { return _MemberContactNumber; }
            set { _MemberContactNumber = value; }
        }

        //MemberName
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
   
        //UpdatedByName
        private string _UpdatedByName = string.Empty;
        public string UpdatedByName
        {
            get { return _UpdatedByName; }
            set { _UpdatedByName = value; }
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
        private int _CheckRunning = 0;
        public int CheckRunning
        {
            get { return _CheckRunning; }
            set { _CheckRunning = value; }
        }
        #endregion

        #region GetMethods

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
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("MemberID", MemberID);
                HT.Add("MemberName", MemberName);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("VotingCandidateName", VotingCandidateName);
                HT.Add("VotingCompetitionID", VotingCompetitionID);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckRunning", CheckRunning);
                dt = obj.GetDataFromStoredProcedure("sp_VotingList_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_VotingList_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

        public DataTable GetList()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberID", MemberID);
                HT.Add("MemberName", MemberName);
                HT.Add("VotingCompetitionID", VotingCompetitionID);
                HT.Add("VotingCandidateName", VotingCandidateName);
                HT.Add("VotingCandidateUniqueId", VotingCandidateUniqueId);
                HT.Add("NoofVotes", NoofVotes);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("VotingPackageID", VotingPackageID);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("UpdatedByName", UpdatedByName);                
                dt = obj.GetDataFromStoredProcedure("sp_VotingList_Get", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }


        public DataTable GetDataDump()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("MemberID", MemberID);
                HT.Add("MemberName", MemberName);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("VotingCandidateName", VotingCandidateName);
                HT.Add("VotingCompetitionID", VotingCompetitionID);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                dt = obj.GetDataFromStoredProcedure("sp_VotingListDump_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        #endregion
    }
}