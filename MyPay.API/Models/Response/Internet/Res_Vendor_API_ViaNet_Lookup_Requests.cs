using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.ViaNet
{
    public class Res_Vendor_API_ViaNet_Lookup_Requests : CommonResponse
    {
        // session_id
        private string _SessionID = string.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        // customer_name
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        } 
        // CustomerID
        private string _CustomerID = string.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        // ViaNet_data
        private List<ViaNet_Data> _Vianet_bills = new List<ViaNet_Data>();
        public  List<ViaNet_Data> Vianet_Bills
        {

            get { return _Vianet_bills; }

            set { _Vianet_bills = value; }
        }
    }
    public class ViaNet_Data
    {
        // name
        private string _PaymentID = string.Empty;
        public string PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }
        private string _BillDate = string.Empty;
        public string BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        }
        private string _Service_Details = string.Empty;
        public string Service_Details
        {
            get { return _Service_Details; }
            set { _Service_Details = value; }
        }
        private string _Service_Name = string.Empty;
        public string Service_Name
        {
            get { return _Service_Name; }
            set { _Service_Name = value; }
        } 
        private string _BillAmount = string.Empty;
        public string BillAmount
        {
            get { return _BillAmount; }
            set { _BillAmount = value; }
        }
    }
}