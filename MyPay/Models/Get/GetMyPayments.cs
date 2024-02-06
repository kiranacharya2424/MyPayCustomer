using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetMyPayments 
    {


        #region "Properties" 

        public int Id { get; set; }
        public string JsonData { get; set; }
        //ProviderName
        public string ProviderName { get; set; }
        //ProviderTypeId
        public string ProviderTypeId { get; set; }
        public string memberID { get; set; }


        #endregion

        #region GetMethods

        public DataTable Get(string ProviderTypeId, string memberID)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("ProviderTypeId", ProviderTypeId);
                HT.Add("memberID", memberID);
                dt = obj.GetDataFromStoredProcedure("[sp_GetMyPayments]", HT);

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