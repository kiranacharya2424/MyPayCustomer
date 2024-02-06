using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetSettings:CommonGet
    {
        #region "Properties"

        //RegistrationCommission
        private decimal _RegistrationCommission = 0;
        public decimal RegistrationCommission
        {
            get { return _RegistrationCommission; }
            set { _RegistrationCommission = value; }
        }

        //KYCCommission
        private decimal _KYCCommission = 0;
        public decimal KYCCommission
        {
            get { return _KYCCommission; }
            set { _KYCCommission = value; }
        }

        //TransactionCommission
        private decimal _TransactionCommission = 0;
        public decimal TransactionCommission
        {
            get { return _TransactionCommission; }
            set { _TransactionCommission = value; }
        }


        //RegistrationRewardPoint
        private decimal _RegistrationRewardPoint = 0;
        public decimal RegistrationRewardPoint
        {
            get { return _RegistrationRewardPoint; }
            set { _RegistrationRewardPoint = value; }
        }

        //KYCRewardPoint
        private decimal _KYCRewardPoint = 0;
        public decimal KYCRewardPoint
        {
            get { return _KYCRewardPoint; }
            set { _KYCRewardPoint = value; }
        }

        //TransactionRewardPoint
        private decimal _TransactionRewardPoint = 0;
        public decimal TransactionRewardPoint
        {
            get { return _TransactionRewardPoint; }
            set { _TransactionRewardPoint = value; }
        }

        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
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
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("Type", Type);
                dt = obj.GetDataFromStoredProcedure("sp_Settings_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }


    }
}