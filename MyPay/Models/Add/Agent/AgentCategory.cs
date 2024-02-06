using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections;
using MyPay.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MyPay.Models.Add.Agent
{
    public class AgentCategory
    {
        public enum CategoryStatus
        {
            Not_Selected = 0,
            Enable = 1,
            Disable = 2,
        }
        public string AgentCategoryId { get; set; }
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "Input string contain alphabet")]
        public string Category { get; set; }
        public string Status { get; set; } 
        public string StatusId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreatedDateDt { get; set; }
        public string UpdatedDateDt { get; set; }
        public string Sno { get; set; }
        public string NoOfAssignedAgent { get; set; }
        public string totalnumber { get; set; } 
        public string flag { get; set; }

        public DataTable GetAgentCategoryData(string flag, string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("flag", flag);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("Category", Category);
                HT.Add("Status", StatusId);;
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                dt = obj.GetDataFromStoredProcedure("Usp_AgentCategory", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

        public CommonDBResonse Add()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {         
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("Usp_AgentCategory", HT);
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
            if (flag=="d") 
            {
                HT.Add("Category", AgentCategoryId);
            }
            else
            {
                HT.Add("Category", Category);
                HT.Add("Status", StatusId);
            }
            //HT.Add("IpAddress", IpAddress);
            //HT.Add("UserId", UserId);         
            return HT;
        }
    }


}