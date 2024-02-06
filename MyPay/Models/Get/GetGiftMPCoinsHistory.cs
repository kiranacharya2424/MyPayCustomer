using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetGiftMPCoinsHistory:CommonGet
    {
        #region "Properties"
        //ContactNumber
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Contact Number")]
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //Prize
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Prize")]
        private decimal _Prize = 0;
        public decimal Prize
        {
            get { return _Prize; }
            set { _Prize = value; }
        }

        //TransactionId
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Transaction Id")]
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //MemberId
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "MemberId")]
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Status
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Status")]
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //MemberName
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Member Name")]
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
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


        #endregion

        #region "GetMethods"
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
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Status", Status);
                HT.Add("Prize", Prize);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("MemberName", MemberName);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                dt = obj.GetDataFromStoredProcedure("sp_GiftMPCoinsHistory_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_GiftMPCoinsHistory_DatatableCounter", HT);
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
        #endregion
    }
}