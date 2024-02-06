using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetFeaturedPartners 
    {
        #region "Properties"

        //Running
        //public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string Image { get; set; }
        public string WebsiteUrl { get; set; }
        public int SortOrder { get; set; }
        public bool IsFeaturedPartner { get; set; }


        #endregion

        #region GetMethods

        public DataTable Get()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
               
                dt = obj.GetDataFromStoredProcedure("sp_GetMerchantSortOrder", HT);
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