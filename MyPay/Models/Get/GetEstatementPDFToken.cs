using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetEstatementPDFToken:CommonGet
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }


        public bool Delete()
        {
            bool DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberId", MemberId);
                HT.Add("Token", Token);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckDelete", CheckDelete);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_EstatementPDFToken_Delete", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
                //new ClsException(filename, "Delete", ex);
            }
            return DataRecieved;
        }
    }
}