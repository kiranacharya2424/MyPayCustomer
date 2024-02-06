using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddBalanceHistory:CommonAdd
    {
        #region "enum"
        public enum Types
        {
            User=1,
            Agent=2,
            Merchant=3
        }
        #endregion

        #region "Properties"
        //TotalBalance 
        private decimal _TotalBalance = 0;
        public decimal TotalBalance
        {
            get { return _TotalBalance; }
            set { _TotalBalance = value; }
        }

        //Type 
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //UserCount 
        private Int64 _UserCount = 0;
        public Int64 UserCount
        {
            get { return _UserCount; }
            set { _UserCount = value; }
        }

        //ActiveUser 
        private Int64 _ActiveUser = 0;
        public Int64 ActiveUser
        {
            get { return _ActiveUser; }
            set { _ActiveUser = value; }
        }

        //InActiveUser 
        private Int64 _InActiveUser = 0;
        public Int64 InActiveUser
        {
            get { return _InActiveUser; }
            set { _InActiveUser = value; }
        }

        //TypeName 
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        //CreatedDatedt 
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        //TotalCoinsBalance 
        private decimal _TotalCoinsBalance = 0;
        public decimal TotalCoinsBalance
        {
            get { return _TotalCoinsBalance; }
            set { _TotalCoinsBalance = value; }
        }

        #endregion

        #region "Get Methods"

        public static decimal GetTotalBalance()
        {
            decimal data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "Select isnull(sum(TotalAmount),0) from Users with(nolock)";
                Result = obj.GetScalarValueWithValue(str);
                if(!string.IsNullOrEmpty(Result) && Result!="0" && Result != "0.00")
                {
                    data = Convert.ToDecimal(Result);
                }
                
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public static decimal GetTotalCoinsBalance()
        {
            decimal data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "Select isnull(sum(TotalRewardPoints),0) from Users with(nolock)";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0" && Result != "0.00")
                {
                    data = Convert.ToDecimal(Result);
                }

            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public static Int64 GetTotalUsers()
        {
            Int64 data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "Select isnull(count(Id),0) from Users with(nolock)";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = Convert.ToInt64(Result);
                }

            }
            catch (Exception ex)
            {

            }
            return data;
        }


        public static string UpdateServiceConnection()
        {
            string data = "";
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "Begin Transaction Update AdminLogin Set ServiceStatus='Connected',LastServiceConnected='"+DateTime.UtcNow+"' Commit Transaction";
                Result = obj.GetScalarValueWithValue(str);
               
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = Result;
                }

            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public static string DisconnectedServiceConnection()
        {
            string data = "";
            try
            {

                AddAdmin outobject = new AddAdmin();
                GetAdmin inobject = new GetAdmin();
                inobject.UserId = "admin";
                inobject.CheckDelete = 0;
                inobject.CheckActive = 1;
                AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
                if (res.Id > 0  && res.ServiceStatus!= "DisConnected")
                {
                    int diff2 = Convert.ToInt32((DateTime.UtcNow - res.LastServiceConnected).TotalMinutes);
                    if (diff2 > 5)
                    {
                        CommonHelpers obj = new CommonHelpers();
                        string Result = "";
                        string str = "Begin Transaction Update AdminLogin Set ServiceStatus='DisConnected',LastServiceConnected='" + DateTime.UtcNow + "' Commit Transaction";
                        Result = obj.GetScalarValueWithValue(str);
     
                        if (!string.IsNullOrEmpty(Result) && Result != "0")
                        {
                            data = Result;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public static Int64 GetTotalActiveUsers()
        {
            Int64 data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "Select isnull(count(Id),0) from Users with(nolock) where IsActive=1";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = Convert.ToInt64(Result);
                }

            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public static Int64 GetTotalInActiveUsers()
        {
            Int64 data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "Select isnull(count(Id),0) from Users with(nolock) where IsActive=0";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = Convert.ToInt64(Result);
                }

            }
            catch (Exception ex)
            {

            }
            return data;
        }

        #endregion


    }
}