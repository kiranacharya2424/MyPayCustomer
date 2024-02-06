using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{

    public class AddRemittance_API_Requests_VendorType : CommonAdd
    {
        private Int32 _VendorType = 0;
        public Int32 VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }
        private string _Req_Input = string.Empty;
        public string Req_Input
        {
            get { return _Req_Input; }
            set { _Req_Input = value; }
        }
        private string _Res_Output = string.Empty;
        public string Res_Output
        {
            get { return _Res_Output; }
            set { _Res_Output = value; }
        }
        private string _Res_TraceId = string.Empty;
        public string Res_TraceId
        {
            get { return _Res_TraceId; }
            set { _Res_TraceId = value; }
        }
        private string _Req_Khalti_Input = string.Empty;
        public string Req_Khalti_Input
        {
            get { return _Req_Khalti_Input; }
            set { _Req_Khalti_Input = value; }
        }
        private string _Res_Khalti_Output = string.Empty;
        public string Res_Khalti_Output
        {
            get { return _Res_Khalti_Output; }
            set { _Res_Khalti_Output = value; }
        }
        private string _Res_Khalti_ErrorCode = string.Empty;
        public string Res_Khalti_ErrorCode
        {
            get { return _Res_Khalti_ErrorCode; }
            set { _Res_Khalti_ErrorCode = value; }
        }
        private string _Res_Khalti_Pin = string.Empty;
        public string Res_Khalti_Pin
        {
            get { return _Res_Khalti_Pin; }
            set { _Res_Khalti_Pin = value; }
        }
        private string _Res_Khalti_Serail = string.Empty;
        public string Res_Khalti_Serail
        {
            get { return _Res_Khalti_Serail; }
            set { _Res_Khalti_Serail = value; }
        }
        private string _Req_URL = string.Empty;
        public string Req_URL
        {
            get { return _Req_URL; }
            set { _Req_URL = value; }
        }
        private string _Req_Token = string.Empty;
        public string Req_Token
        {
            get { return _Req_Token; }
            set { _Req_Token = value; }
        }
        private string _Req_ReferenceNo = string.Empty;
        public string Req_ReferenceNo
        {
            get { return _Req_ReferenceNo; }
            set { _Req_ReferenceNo = value; }
        }
        private string _Req_Khalti_ReferenceNo = string.Empty;
        public string Req_Khalti_ReferenceNo
        {
            get { return _Req_Khalti_ReferenceNo; }
            set { _Req_Khalti_ReferenceNo = value; }
        }
        private string _Req_Khalti_URL = string.Empty;
        public string Req_Khalti_URL
        {
            get { return _Req_Khalti_URL; }
            set { _Req_Khalti_URL = value; }
        }
        private bool _Res_Khalti_Status = false;
        public bool Res_Khalti_Status
        {
            get { return _Res_Khalti_Status; }
            set { _Res_Khalti_Status = value; }
        }
        private string _Res_Khalti_State = string.Empty;
        public string Res_Khalti_State
        {
            get { return _Res_Khalti_State; }
            set { _Res_Khalti_State = value; }
        }
        private string _Res_Khalti_Message = string.Empty;
        public string Res_Khalti_Message
        {
            get { return _Res_Khalti_Message; }
            set { _Res_Khalti_Message = value; }
        }
        private string _Res_Khalti_Id = string.Empty;
        public string Res_Khalti_Id
        {
            get { return _Res_Khalti_Id; }
            set { _Res_Khalti_Id = value; }
        }
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        private string _IpAddress = string.Empty;
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        private string _PlatForm = string.Empty;
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }
        private int _VendorApiType = 0;
        public int VendorApiType
        {
            get { return _VendorApiType; }
            set { _VendorApiType = value; }
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

        //public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        Common.CommonHelpers obj = new Common.CommonHelpers();
        //        Hashtable HT = new Hashtable();
        //        HT.Add("SearchText", SearchText);
        //        HT.Add("PagingSize", PagingSize);
        //        HT.Add("OffsetValue", OffsetValue);
        //        HT.Add("sortColumn", sortColumn);
        //        HT.Add("sortOrder", sortOrder);
        //        HT.Add("MemberId", MemberId);
        //        HT.Add("CheckDelete", CheckDelete);
        //        HT.Add("CheckActive", CheckActive);
        //        HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
        //        HT.Add("VendorApiType", VendorApiType);
        //        HT.Add("TransactionUniqueId", TransactionUniqueId);
        //        HT.Add("StartDate", StartDate);
        //        HT.Add("EndDate", EndDate);
        //        HT.Add("Res_Khalti_Id", Res_Khalti_Id);
        //        dt = obj.GetDataFromStoredProcedure("sp_VendorAPIRequest_Datatable", HT);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            Common.CommonHelpers obj1 = new Common.CommonHelpers();
        //            DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_VendorAPIRequest_DatatableCounter", HT);
        //            if (dtCounter != null && dtCounter.Rows.Count > 0)
        //            {
        //                dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DataRecieved = false;
        //    }
        //    return dt;
        //}

    }


}