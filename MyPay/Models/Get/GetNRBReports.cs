using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetNRBReports  
    {

        private int _Annexture = 0;
        public int Annexture
        {
            get { return _Annexture; }
            set { _Annexture = value; }
        }

        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private decimal _StartAmount = 0;
        public decimal StartAmount
        {
            get { return _StartAmount; }
            set { _StartAmount = value; }
        }
        private decimal _EndAmount = 0;
        public decimal EndAmount
        {
            get { return _EndAmount; }
            set { _EndAmount = value; }
        }

        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }


        public DataTable GetData(string Annexture, string MemberId, string Type, string StartAmount, string EndAmount, string StartDate, string EndDate)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("Type", Type);
                HT.Add("StartAmount", StartAmount);
                HT.Add("EndAmount", EndAmount);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Annexture", Annexture); 
                dt = obj.GetDataFromStoredProcedure("sp_NRB_Get", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

    }
}