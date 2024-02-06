using Microsoft.Office.Interop.Excel;
using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
namespace MyPay.Models.Add
{
    public class AddUserBasicInfo
    {
   
     
        private string _FirstName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        } 
        private string _LastName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _ContactNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
         
        private Int32 _RoleId = 0;
        public Int32 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
      
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
   
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
         
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
         
        private bool _IsOldUser = false;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsOldUser
        {
            get { return _IsOldUser; }
            set { _IsOldUser = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }
         
        private string _UserId = "";
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        } 
        public bool GetUserInformationBasic()
        {
            try
            {
                DataRecieved = false;
                //if (MemberId != 0)
                {
                    Common.CommonHelpers obj = new Common.CommonHelpers();
                    Hashtable HT = new Hashtable();
                    if (MemberId > 0)
                    {
                        HT.Add("MemberId", MemberId);
                    }
                    HT.Add("ContactNumber", ContactNumber);
                    System.Data.DataTable dt = obj.GetDataFromStoredProcedure(Common.Common.StoreProcedures.sp_Users_GetBasicInfo, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        Id = Convert.ToInt64(row["Id"]);
                        UserId = Convert.ToString(row["UserId"]);
                        FirstName = Convert.ToString(row["FirstName"]);
                        LastName = Convert.ToString(row["LastName"]);
                        ContactNumber = Convert.ToString(row["ContactNumber"]);
                        TotalAmount = Convert.ToDecimal(row["TotalAmount"]);
                        MemberId = Convert.ToInt64(row["MemberId"]);
                        RoleId = Convert.ToInt32(row["RoleId"]);
                        IsOldUser = Convert.ToBoolean(row["IsOldUser"]);
                        DataRecieved = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }


    }



}