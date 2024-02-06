using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddDepositOrders : CommonAdd
    {
        #region "Enums"

        public enum DepositStatus
        {
            Success = 1,
            Failed = 2,
            Cancelled = 3,
            Pending = 4,
            Incomplete=5,
            Refund=6
        }

        public enum DepositType
        {
            ConnectIPS = 1,
            Card = 2,
            Bank_Transfer=3,
            Internet_Banking=4,
            Mobile_Banking=5,
            Linked_Bank_Transaction=6,
            CreditCard_Payment=7,
            Linked_Bank_Deposit=8
        }

        
        #endregion

        #region "Properties"

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        //Platform
        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

     
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        //Particulars
        private string _Particulars = string.Empty;
        public string Particulars
        {
            get { return _Particulars; }
            set { _Particulars = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //ServiceCharges
        private decimal _ServiceCharges = 0;
        public decimal ServiceCharges
        {
            get { return _ServiceCharges; }
            set { _ServiceCharges = value; }
        }

        //Currency
        private string _Currency = string.Empty;
        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }

        //RefferalsId
        private string _RefferalsId = string.Empty;
        public string RefferalsId
        {
            get { return _RefferalsId; }
            set { _RefferalsId = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //Type
        private string _TypeMultiple = string.Empty;
        public string TypeMultiple
        {
            get { return _TypeMultiple; }
            set { _TypeMultiple = value; }
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

        //CheckDelete
        private int _CheckDelete = 2;// (int)clsData.BooleanValue.Both;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
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

        //StatusName
        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        //JsonResponse
        private string _JsonResponse = string.Empty;
        public string JsonResponse
        {
            get { return _JsonResponse; }
            set { _JsonResponse = value; }
        }

        //ResponseCode
        private string _ResponseCode = string.Empty;
        public string ResponseCode
        {
            get { return _ResponseCode; }
            set { _ResponseCode = value; }
        }

        //TypeName
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        //CreatedDatedt
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
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

        //TotalSuccess
        private decimal _TotalSuccess = 0;
        public decimal TotalSuccess
        {
            get { return _TotalSuccess; }
            set { _TotalSuccess = value; }
        }

        //TotalFailed
        private decimal _TotalFailed = 0;
        public decimal TotalFailed
        {
            get { return _TotalFailed; }
            set { _TotalFailed = value; }
        }

        //TotalPending
        private decimal _TotalPending = 0;
        public decimal TotalPending
        {
            get { return _TotalPending; }
            set { _TotalPending = value; }
        }
        //FilterTotalCount
        private Int32 _FilterTotalCount = 0;
        public Int32 FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }

        //GatewayType
        private Int32 _GatewayType = 0;
        public Int32 GatewayType
        {
            get { return _GatewayType; }
            set { _GatewayType = value; }
        }
        //GatewayName
        private string _GatewayName = "";
        public string GatewayName
        {
            get { return _GatewayName; }
            set { _GatewayName = value; }
        }
        #endregion

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
                HT.Add("TransactionId", TransactionId);
                HT.Add("RefferalsId", RefferalsId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Status", Status);
                HT.Add("Type", Type);
                HT.Add("TypeMultiple", TypeMultiple);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                dt = obj.GetDataFromStoredProcedure("sp_DepositOrders_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_DepositOrders_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                        dt.Rows[0]["TotalSuccess"] = dtCounter.Rows[0]["TotalSuccess"].ToString();
                        dt.Rows[0]["TotalPending"] = dtCounter.Rows[0]["TotalPending"].ToString();
                        dt.Rows[0]["TotalFailed"] = dtCounter.Rows[0]["TotalFailed"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
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
                HT.Add("MemberId", MemberId);
                HT.Add("TransactionId", TransactionId);
                HT.Add("RefferalsId", RefferalsId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Status", Status);
                HT.Add("Type", Type);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                dt = obj.GetDataFromStoredProcedure("sp_DepositOrders_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

    }
}