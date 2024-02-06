using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace MyPay.Models.Add.Agent
{
    public class OtherCommission
    {
        public int  OtherCommissionId { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public decimal KYCCommission { get; set; }
        public decimal AgentCreationCommission { get; set; }
        public decimal UserCreationCommission { get; set; }
        public decimal Value { get; set; }
        public decimal CASHINCommission { get; set; }
        public decimal CASHOUTCommission { get; set; } 
        public string flag { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string Sno { get; set; }

        bool DataRecieved=false;
        public CommonDBResonse AddOtherAgentCommission()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("Usp_AgentOtherCommission", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }


        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("Flag", flag);            
                //HT.Add("OtherCommissionId", OtherCommissionId);
                HT.Add("MinAmount", MinAmount);
                HT.Add("MaxAmount", MaxAmount);
                HT.Add("CASHINCommission", CASHINCommission);
                HT.Add("CASHOUTCommission", CASHOUTCommission);
                HT.Add("Value", Value);
                HT.Add("KYCCommission", KYCCommission);
                HT.Add("AgentCreationCommission", AgentCreationCommission);
                HT.Add("UserCreationCommission", UserCreationCommission);
                
            if (flag.ToLower()=="u")
            {
                HT.Add("UpdatedBy", createdBy); 
            }
            else if(flag.ToLower() == "i")
            {
                HT.Add("createdBy", createdBy);
            }
            return HT;
        }

        public DataTable GetAgentOtherCommissionData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("flag", flag);
                HT.Add("OtherCommissionId", OtherCommissionId);                
                dt = obj.GetDataFromStoredProcedure("Usp_AgentOtherCommission", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }



        public float CountOtherCommissionCheck()
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(OtherCommissionId) from AgentOtherCommission with(nolock)  ";
                //string str = "select count(Id) from Commission where Id != '" + Id.ToString() + "' and  ServiceId='" + serviceId + "'  and ((MinimumAmount between " + MinimumAmount + " and " + MaximumAmount + " ) or ((maximumamount between " + MinimumAmount + " and " + MaximumAmount + " ) and IsActive=1 and IsDeleted=0))";
                Result = obj.GetScalarValueWithValue(str);               
                data = (float.Parse(Result));                
            }
            catch (Exception ex)
            {

            }
            return data;
        }
    }
}