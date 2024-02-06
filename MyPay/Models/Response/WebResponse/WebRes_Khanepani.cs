using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Khanepani : WebCommonResponse
    {
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        private decimal _ServiceChargeAmount = 0;
        public decimal ServiceChargeAmount
        {
            get { return _ServiceChargeAmount; }
            set { _ServiceChargeAmount = value; }
        }
        private decimal _CashbackAmount = 0;
        public decimal CashbackAmount
        {
            get { return _CashbackAmount; }
            set { _CashbackAmount = value; }
        }
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
    }
    public class service_group_counters_khanepani
    {
        // name
        private string _name = String.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        // value
        private string _value = String.Empty;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

    }

    public class WebResponse_KhanePaniDetails : WebCommonResponse
    {

        // Consumer_Code
        private string _Consumer_Code = string.Empty;
        public string Consumer_Code
        {
            get { return _Consumer_Code; }
            set { _Consumer_Code = value; }
        }
        // consumer_name
        private string _Consumer_Name = string.Empty;
        public string Consumer_Name
        {
            get { return _Consumer_Name; }
            set { _Consumer_Name = value; }
        }
        // Address
        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        // Mobile
        private string _Mobile = string.Empty;
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        // Current_Month_Dues
        private string _Current_Month_Dues = string.Empty;
        public string Current_Month_Dues
        {
            get { return _Current_Month_Dues; }
            set { _Current_Month_Dues = value; }
        }

        // Current_Month_Discount
        private string _Current_month_discount = string.Empty;
        public string Current_month_discount
        {
            get { return _Current_month_discount; }
            set { _Current_month_discount = value; }
        }

        // Current_Month_fine
        private string _Current_Month_fine = string.Empty;
        public string Current_Month_fine
        {
            get { return _Current_Month_fine; }
            set { _Current_Month_fine = value; }
        }
        // Total_credit_sales_amount
        private string _Total_credit_sales_amount = string.Empty;
        public string Total_credit_sales_amount
        {
            get { return _Total_credit_sales_amount; }
            set { _Total_credit_sales_amount = value; }
        }
        // Total_advance_amount
        private string _Total_advance_amount = string.Empty;
        public string Total_advance_amount
        {
            get { return _Total_advance_amount; }
            set { _Total_advance_amount = value; }
        }
        // Previous_dues
        private string _Previous_dues = string.Empty;
        public string Previous_dues
        {
            get { return _Previous_dues; }
            set { _Previous_dues = value; }
        }

        // Total_Dues
        private string _Total_Dues = string.Empty;
        public string Total_Dues
        {
            get { return _Total_Dues; }
            set { _Total_Dues = value; }
        }
        // Minimum_Payable_Amount
        private string _Minimum_Payable_Amount = string.Empty;
        public string Minimum_Payable_Amount
        {
            get { return _Minimum_Payable_Amount; }
            set { _Minimum_Payable_Amount = value; }
        }

    }
}