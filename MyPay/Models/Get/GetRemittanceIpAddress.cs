using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetRemittanceIpAddress : CommonGet
    {
       
        //RemittanceId
        bool DataRecieved = false;
        
        private Int64 _RemittanceMemberId = 0;
        public Int64 RemittanceMemberId
        {
            get { return _RemittanceMemberId; }
            set { _RemittanceMemberId = value; }
        }
        private String _RemittanceUniqueId = string.Empty;
        public String RemittanceUniqueId
        {
            get { return _RemittanceUniqueId; }
            set { _RemittanceUniqueId = value; }
        }
        private String _RemittanceName = string.Empty;
        public String RemittanceName
        {
            get { return _RemittanceName; }
            set { _RemittanceName = value; }
        }
        private String _RemittanceOrganization = string.Empty;
        public String RemittanceOrganization
        {
            get { return _RemittanceOrganization; }
            set { _RemittanceOrganization = value; }
        }
        private String _IPAddress = Common.Common.GetUserIP();
        public String IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
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
                HT.Add("RemittanceUniqueId", RemittanceUniqueId);
                HT.Add("RemittanceName", RemittanceName);
                HT.Add("RemittanceOrganization", RemittanceOrganization);
                HT.Add("IPAddress", IPAddress);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Take", 10);
                HT.Add("Skip", 0);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
               
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceIPAddress_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_RemittanceIPAddress_DatatableCounter", HT);
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
    }
}
