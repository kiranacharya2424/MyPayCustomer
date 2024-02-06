using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Models.Add.Agent
{
    public class AgentCommission
    {
        public enum KycTypes
        {
            All = 0,
            Verified = 1
        }

        public enum GenderTypes
        {
            All = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }

        public enum ScheduleStatuses
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }
        public string AgentCategory { get; set; }
        public Int64 resultId { get; set; }
        public string AgentCategoryId { get; set; }
        public string ProviderType { get; set; }
        public string ServiceId { get; set; } 
        public List<SelectListItem> ProviderservicceList { get; set; } 
        public string flag { get; set; }         
        public int KycType { get; set; }      
        public int GenderType { get; set; }
        //public KycTypes KycTypeEnum { get; set; }
        //public GenderTypes GenderTypeEnum { get; set; }
        public ScheduleStatuses EnumScheduleStatus { get; set; }

        public Int64 Id { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal FixedCommission { get; set; }
        public decimal PercentageCommission { get; set; }
        public decimal MinimumAmountSC { get; set; }
        public decimal MaximumAmountSC { get; set; }
        public decimal PercentageRewardPoints { get; set; }
        public decimal PercentageRewardPointsDebit { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; } 
        public string FromDateDT { get; set; }
        public string ToDateDT { get; set; }
        public decimal MaximumAllowed { get; set; }

        public decimal MinimumAllowed { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal MaximumAllowedSC { get; set; }
        public decimal MinimumAllowedSC { get; set; }
        public int Status { get; set; }
        public string ServiceName { get; set; }
        public int Type { get; set; }
        public string GenderTypeName { get; set; }
        public string KycTypeName { get; set; }
        public string Sno { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedDateDt { get; set; }
        public string UpdatedDateDt { get; set; }
        public string CheckActive { get; set; }
        public string CheckApprovedByAdmin { get; set; }
        public int CheckDelete { get; set; }
        public string ScheduleStatus { get; set; }
        public string ButtonType { get; set; }
        public Int64 CreatedBy { get; set; }
        public Int64 UpdatedBy { get; set; }
        public bool DataRecieved { get; set; }
        public string Running { get; set; }
        public string Scheduled { get; set; }
        public string Expired { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public string CreatedByName { get; set; }
        public decimal ChildTxnRate { get; set; }
        public decimal ChildTxnMinAmt { get; set; }
        public decimal ChildTxnMaxAmt { get; set; }
        public decimal MonthlyMinAmt { get; set; }
        public decimal MonthlyMaxAmt { get; set; }
        public decimal MonthlyBonus { get; set; }
        public bool IsDeleted { get; set; }

        //Running




        //public DataTable GetAgentCategoryData(string flag, string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        Common.CommonHelpers obj = new Common.CommonHelpers();
        //        Hashtable HT = new Hashtable();
        //        HT.Add("flag", flag);
        //        HT.Add("PagingSize", PagingSize);
        //        HT.Add("OffsetValue", OffsetValue);
        //        HT.Add("sortColumn", sortColumn);
        //        HT.Add("sortOrder", sortOrder);
        //        HT.Add("Category", Category);
        //        HT.Add("Status", StatusId); ;
        //        HT.Add("StartDate", StartDate);
        //        HT.Add("EndDate", EndDate);
        //        dt = obj.GetDataFromStoredProcedure("Usp_AgentCategory", HT);
        //    }
        //    catch (Exception ex)
        //    {
        //        //DataRecieved = false;
        //    }
        //    return dt;
        //}
        public float CountAgentCommissionCheck(string serviceId, string AgentCategoryId,string serviceCategory, int GenderType, int KycType, decimal MinimumAmount, decimal MaximumAmount, Int64 Id,string fromdate , string todate)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(Id) from AgentCommission with(nolock) where Id != '" + Id.ToString() + "' and  ServiceId='" + serviceId + "'   and  CategoryId='" + AgentCategoryId + "'and  ServiceCategory='" + serviceCategory + "'  " +
                    "  and ((MinimumAmount between " + MinimumAmount + " and " + MaximumAmount + " ) or ((maximumamount between " + MinimumAmount + " and " + MaximumAmount + " ))) and IsActive=1 and IsDeleted=0 and" +
                    " ((cast( '" + fromdate + "' as date) between cast(FromDate as date) and  cast(ToDate as date))\r\nOr \r\n(cast( '" + todate + "' as date) between cast(FromDate as date) and  cast(ToDate as date))\r\n) ";
                //string str = "select count(Id) from Commission where Id != '" + Id.ToString() + "' and  ServiceId='" + serviceId + "'  and ((MinimumAmount between " + MinimumAmount + " and " + MaximumAmount + " ) or ((maximumamount between " + MinimumAmount + " and " + MaximumAmount + " ) and IsActive=1 and IsDeleted=0))";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (float.Parse(Result));
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public CommonDBResonse AddAgentCommission()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("Usp_AgentCommission", HT);
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
            if (flag=="u")
            {
                HT.Add("CategoryId", AgentCategoryId);
                HT.Add("Id", Id);
                HT.Add("ServiceCategoryId", ProviderType);
                HT.Add("MinimumAmount", MinimumAmount);
                HT.Add("MaximumAmount", MaximumAmount);
                HT.Add("FixedCommission", FixedCommission);
                HT.Add("PercentageCommission", PercentageCommission);
                HT.Add("PercentageRewardPoints", PercentageRewardPoints);
                HT.Add("PercentageRewardPointsDebit", PercentageRewardPointsDebit);
                HT.Add("FromDate", FromDate);
                HT.Add("ToDate", ToDate);
                HT.Add("MinimumAllowed", MinimumAllowed);
                HT.Add("MaximumAllowed", MaximumAllowed);
                HT.Add("ServiceId", ServiceId);
                HT.Add("ServiceCharge", ServiceCharge);
                HT.Add("MinimumAllowedSC", MinimumAllowedSC);
                HT.Add("MaximumAllowedSC", MaximumAllowedSC);
                HT.Add("ChildTxnRate", ChildTxnRate);
                HT.Add("ChildTxnMinAmt", ChildTxnMinAmt);
                HT.Add("ChildTxnMaxAmt", ChildTxnMaxAmt);
                HT.Add("MonthlyMinAmt", MonthlyMinAmt);
                HT.Add("MonthlyMaxAmt", MonthlyMaxAmt);
                HT.Add("MonthlyBonus", MonthlyBonus);
            }
            else if (flag == "d")
            {
                HT.Add("Id", Id);
                HT.Add("IsDeleted", IsDeleted);
            }
            else if (flag == "us")
            {
                HT.Add("Id", Id);
                HT.Add("IsActive", IsActive);
            }
            else
            {
                HT.Add("CategoryId", AgentCategoryId);
                HT.Add("ServiceCategoryId", ProviderType);
                HT.Add("MinimumAmount", MinimumAmount);
                HT.Add("MaximumAmount", MaximumAmount);
                HT.Add("FixedCommission", FixedCommission);
                HT.Add("PercentageCommission", PercentageCommission);
                HT.Add("PercentageRewardPoints", PercentageRewardPoints);
                HT.Add("PercentageRewardPointsDebit", PercentageRewardPointsDebit);
                HT.Add("FromDate", FromDate);
                HT.Add("ToDate", ToDate);
                HT.Add("MinimumAllowed", MinimumAllowed);
                HT.Add("MaximumAllowed", MaximumAllowed);
                HT.Add("ServiceId", ServiceId);
                HT.Add("Type", Type);
                HT.Add("IsActive", IsActive);
                HT.Add("Status", Status);
                HT.Add("IsApprovedByAdmin", IsApprovedByAdmin);
                HT.Add("IsDeleted", IsDeleted);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("KycType", KycType);
                HT.Add("GenderType", GenderType);
                HT.Add("ServiceCharge", ServiceCharge);
                HT.Add("MinimumAllowedSC", MinimumAllowedSC);
                HT.Add("MaximumAllowedSC", MaximumAllowedSC);
                HT.Add("ChildTxnRate", ChildTxnRate);
                HT.Add("ChildTxnMinAmt", ChildTxnMinAmt);
                HT.Add("ChildTxnMaxAmt", ChildTxnMaxAmt);
                HT.Add("MonthlyMinAmt", MonthlyMinAmt);
                HT.Add("MonthlyMaxAmt", MonthlyMaxAmt);
                HT.Add("MonthlyBonus", MonthlyBonus);
            }
            
            //HT.Add("IpAddress", IpAddress);
            //HT.Add("UserId", UserId);         
            return HT;
        }

        public DataTable GetAgentCommissionData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("flag", flag);
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("Id", Id);
                HT.Add("MinimumAmount", MinimumAmount);
                HT.Add("MaximumAmount", MaximumAmount);
                HT.Add("MinimumAmountSC", MinimumAmountSC);
                HT.Add("MaximumAmountSC", MaximumAmountSC);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("ServiceId", ServiceId);
                HT.Add("GenderType", GenderType);
                HT.Add("KycType", KycType);
                HT.Add("Running", Running);
                HT.Add("Scheduled", Scheduled);
                HT.Add("Expired", Expired);    
                HT.Add("CategoryId", AgentCategoryId);
                HT.Add("ServiceCategoryId", ProviderType);
                HT.Add("ButtonType", ButtonType);
                dt = obj.GetDataFromStoredProcedure("Usp_AgentCommission", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }




    }
}