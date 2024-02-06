using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;
using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add.Agent
{
    public class SampleVoucher
    {
        public string SampleVoucheId { get; set; }
        public string flag { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string VoucherImage { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; } 
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreatedDateDt { get; set; }
        public string UpdatedDateDt { get; set; }
        public string Sno { get; set; }
        public DataTable GetVoucherData(string flag, string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
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
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("SampleVoucheId", SampleVoucheId);
                dt = obj.GetDataFromStoredProcedure("sp_VoucherRequest", HT);
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
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_VoucherRequest", HT);
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
            if (flag.ToLower() =="svi"  ) {
                HT.Add("BankId", BankId);
                HT.Add("BankName", BankName);
                HT.Add("Branch", Branch);
                HT.Add("AccountName", AccountName);
                HT.Add("AccountNumber", AccountNumber);
                HT.Add("VoucherImage", VoucherImage);
                HT.Add("CreatedBy", CreatedBy);
            }
            else if (flag.ToLower() == "dsv")
            {
                HT.Add("SampleVoucheId", SampleVoucheId);
            
            }
            else if (flag.ToLower() == "svu")
            {
                HT.Add("BankName", BankName);
                HT.Add("SampleVoucheId", SampleVoucheId);
                HT.Add("Branch", Branch);
                HT.Add("AccountName", AccountName);
                HT.Add("AccountNumber", AccountNumber);
                HT.Add("VoucherImage", VoucherImage);
                HT.Add("UpdatedBy", UpdatedBy);
            }
            
                          
               
                 
            return HT;
        }

    }
}