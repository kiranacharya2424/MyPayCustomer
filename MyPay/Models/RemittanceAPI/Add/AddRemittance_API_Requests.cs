using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{

    public class AddRemittance_API_Requests : CommonAdd
    {
        bool DataRecieved = false;

        #region "Enums"
        public enum Remittance_Api_Type
        {
            NotSelected=0,
            AccountValidation = 1,
            BankTransfer = 2,
            CheckStatus = 3,
            BankList = 4,
            ServiceCharge = 5
        }

        public enum Statuses
        {
            Success = 1,
            Pending = 2,
            Failed = 3,
            Queued = 4,
            Processing = 5,
            Expired = 6,
            Error = 7,
            Status_Error = 8,
            Refund = 9
        }

        #endregion
        #region "Properties" 
        private String _Req_Input = string.Empty;
        public String Req_Input
        {
            get { return _Req_Input; }
            set { _Req_Input = value; }
        }
        private String _Res_Output = string.Empty;
        public String Res_Output
        {
            get { return _Res_Output; }
            set { _Res_Output = value; }
        }
        private String _Req_URL = string.Empty;
        public String Req_URL
        {
            get { return _Req_URL; }
            set { _Req_URL = value; }
        }
        private String _Req_ReferenceNo = string.Empty;
        public String Req_ReferenceNo
        {
            get { return _Req_ReferenceNo; }
            set { _Req_ReferenceNo = value; }
        }
        private String _TransactionUniqueId = string.Empty;
        public String TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        private String _VendorTransactionId = string.Empty;
        public String VendorTransactionId
        {
            get { return _VendorTransactionId; }
            set { _VendorTransactionId = value; }
        }
        private String _MerchantUniqueId = string.Empty;
        public String MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }
        private String _MerchantName = string.Empty;
        public String MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }
        private String _OrganizationName = string.Empty;
        public String OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        private String _ContactNo = string.Empty;
        public String ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        private String _Signature = string.Empty;
        public String Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }
        private Int32 _Status = 0;
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private String _Remarks = string.Empty;
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private String _DeviceId = string.Empty;
        public String DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        private String _IpAddress = Common.Common.GetUserIP();
        public String IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        private String _PlatForm = string.Empty;
        public String PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }
        private Int32 _RemittanceApiType = 0;
        public Int32 RemittanceApiType
        {
            get { return _RemittanceApiType; }
            set { _RemittanceApiType = value; }
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
        //IndiaDate
        private String _IndiaDate = string.Empty;
        public String IndiaDate
        {
            get { return _IndiaDate; }
            set { _IndiaDate = value; }
        }
        private String _VendorURL = string.Empty;
        public String VendorURL
        {
            get { return _VendorURL; }
            set { _VendorURL = value; }
        }
        private String _VendorInput = string.Empty;
        public String VendorInput
        {
            get { return _VendorInput; }
            set { _VendorInput = value; }
        }
        private String _VendorOutput = string.Empty;
        public String VendorOutput
        {
            get { return _VendorOutput; }
            set { _VendorOutput = value; }
        }
        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        private string _TotalCount = string.Empty;
        public string TotalCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; }
        }

        private string _RemittanceApiTypeName = string.Empty;
        public string RemittanceApiTypeName
        {
            get { return _RemittanceApiTypeName; }
            set { _RemittanceApiTypeName = value; }
        }

        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Remittance_API_Requests_AddNew", HT);
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Remittance_API_Requests_Update", HT);
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
            HT.Add("Req_Input", Req_Input);
            HT.Add("Res_Output", Res_Output);
            HT.Add("Req_URL", Req_URL);
            HT.Add("Req_ReferenceNo", Req_ReferenceNo);
            HT.Add("TransactionUniqueId", TransactionUniqueId);
            HT.Add("VendorTransactionId", VendorTransactionId);
            HT.Add("MerchantUniqueId", MerchantUniqueId);
            HT.Add("MerchantName", MerchantName);
            HT.Add("OrganizationName", OrganizationName);
            HT.Add("ContactNo", ContactNo);
            HT.Add("Signature", Signature);
            HT.Add("Status", Status);
            HT.Add("Remarks", Remarks);
            HT.Add("DeviceId", DeviceId);
            HT.Add("IpAddress", IpAddress);
            HT.Add("PlatForm", PlatForm);
            HT.Add("RemittanceApiType", RemittanceApiType);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("VendorURL", VendorURL);
            HT.Add("VendorInput", VendorInput);
            HT.Add("VendorOutput", VendorOutput);

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
                HT.Add("Req_Input", Req_Input);
                HT.Add("Res_Output", Res_Output);
                HT.Add("Req_URL", Req_URL);
                HT.Add("Req_ReferenceNo", Req_ReferenceNo);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("MerchantName", MerchantName);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactNo", ContactNo);
                HT.Add("Signature", Signature);
                HT.Add("Status", Status);
                HT.Add("Remarks", Remarks);
                HT.Add("DeviceId", DeviceId);
                HT.Add("IpAddress", IpAddress);
                HT.Add("PlatForm", PlatForm);
                HT.Add("RemittanceApiType", RemittanceApiType);
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

                dt = obj.GetDataFromStoredProcedure("sp_Remittance_API_Requests_Get", HT);
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
                HT.Add("Req_Input", Req_Input);
                HT.Add("Res_Output", Res_Output);
                HT.Add("Req_URL", Req_URL);
                HT.Add("Req_ReferenceNo", Req_ReferenceNo);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("MerchantName", MerchantName);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactNo", ContactNo);
                HT.Add("Signature", Signature);
                HT.Add("Status", Status);
                HT.Add("Remarks", Remarks);
                HT.Add("DeviceId", DeviceId);
                HT.Add("IpAddress", IpAddress);
                HT.Add("PlatForm", PlatForm);
                HT.Add("RemittanceApiType", RemittanceApiType);
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

                dt = obj.GetDataFromStoredProcedure("sp_Remittance_API_Requests_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Req_Input = Convert.ToString(dt.Rows[0]["Req_Input"].ToString());
                    Res_Output = Convert.ToString(dt.Rows[0]["Res_Output"].ToString());
                    Req_URL = Convert.ToString(dt.Rows[0]["Req_URL"].ToString());
                    Req_ReferenceNo = Convert.ToString(dt.Rows[0]["Req_ReferenceNo"].ToString());
                    TransactionUniqueId = Convert.ToString(dt.Rows[0]["TransactionUniqueId"].ToString());
                    VendorTransactionId = Convert.ToString(dt.Rows[0]["VendorTransactionId"].ToString());
                    MerchantUniqueId = Convert.ToString(dt.Rows[0]["MerchantUniqueId"].ToString());
                    MerchantName = Convert.ToString(dt.Rows[0]["MerchantName"].ToString());
                    OrganizationName = Convert.ToString(dt.Rows[0]["OrganizationName"].ToString());
                    ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                    Signature = Convert.ToString(dt.Rows[0]["Signature"].ToString());
                    Status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                    Remarks = Convert.ToString(dt.Rows[0]["Remarks"].ToString());
                    DeviceId = Convert.ToString(dt.Rows[0]["DeviceId"].ToString());
                    IpAddress = Convert.ToString(dt.Rows[0]["IpAddress"].ToString());
                    PlatForm = Convert.ToString(dt.Rows[0]["PlatForm"].ToString());
                    RemittanceApiType = Convert.ToInt32(dt.Rows[0]["RemittanceApiType"].ToString());
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
                    CreatedDateDt = Convert.ToString(dt.Rows[0]["IndiaDate"].ToString());
                    VendorURL = Convert.ToString(dt.Rows[0]["VendorURL"].ToString());
                    VendorInput = Convert.ToString(dt.Rows[0]["VendorInput"].ToString());
                    VendorOutput = Convert.ToString(dt.Rows[0]["VendorOutput"].ToString());

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
                HT.Add("Req_Input", Req_Input);
                HT.Add("Res_Output", Res_Output);
                HT.Add("Req_URL", Req_URL);
                HT.Add("Req_ReferenceNo", Req_ReferenceNo);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("MerchantName", MerchantName);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactNo", ContactNo);
                HT.Add("Signature", Signature);
                HT.Add("Status", Status);
                HT.Add("Remarks", Remarks);
                HT.Add("DeviceId", DeviceId);
                HT.Add("IpAddress", IpAddress);
                HT.Add("PlatForm", PlatForm);
                HT.Add("RemittanceApiType", RemittanceApiType);
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

                dt = obj.GetDataFromStoredProcedure("sp_Remittance_API_Requests_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_Remittance_API_Requests_DatatableCounter", HT);
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