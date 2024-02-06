using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Wordprocessing;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using MyPay.Models.Request.WebRequest.Common;

namespace MyPay.Models.Add
{
    public class AddMyPayments : CommonAdd
    {
        //bool DataRecieved = false;

        #region "Properties" 
        public string JsonData { get; set; }
        //ProviderName
        public string ProviderName { get; set; }
        //ProviderTypeId
        public string ProviderTypeId { get; set; }
        public string memberID { get; set; }


        #endregion

        #region "Add Methods" 
        public CommonDbResponse AddMyPayment()
        {
            CommonDbResponse obj1 = new CommonDbResponse();

            CommonHelpers obj = new CommonHelpers();
            System.Collections.Hashtable HT = SetObject();
            obj1 = obj.ExecuteProcedureGetValue("sp_InsertMyPayments", HT);
            return obj1;
        }
        public CommonDbResponse EditMyPayment()
        {
            CommonDbResponse obj1 = new CommonDbResponse();

            CommonHelpers obj = new CommonHelpers();
            System.Collections.Hashtable HT = SetObject();
            obj1 = obj.ExecuteProcedureGetValue("sp_UpdateMyPayment", HT);
            return obj1;
        }
        public CommonDbResponse DeleteMyPayment()
        {
            CommonDbResponse obj1 = new CommonDbResponse();

            CommonHelpers obj = new CommonHelpers();
            System.Collections.Hashtable HT = DeleteSetObject();
            obj1 = obj.ExecuteProcedureGetValue("sp_DeleteMyPayment", HT);
            return obj1;
        }

        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("Id", Id);
            HT.Add("JsonData", JsonData);
            HT.Add("ProviderName", ProviderName);
            HT.Add("ProviderTypeId", ProviderTypeId);
            HT.Add("memberID", memberID);
            return HT;
        }
        #endregion
        public System.Collections.Hashtable DeleteSetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("Id", Id);
            return HT;
        }
    }
}