using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.ViaNet
{
    public class Res_Vendor_API_DishHome_Lookup_Requests : CommonResponse
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
        // Package Name
        private string _PackageName = string.Empty;
        public string PackageName
        {
            get { return _PackageName; }
            set { _PackageName = value; }
        }
        // Package Name
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        // DISHHOME_data
        //private List<DishHome_Data> _DishHome_bills = new List<DishHome_Data>();
        //public  List<DishHome_Data> DishHome_Bills
        //{

        //    get { return _DishHome_bills; }

        //    set { _DishHome_bills = value; }
        //}
    }
    //public class DishHome_Data
    //{
    //    // name
    //    private string _PaymentID = string.Empty;
    //    public string PaymentID
    //    {
    //        get { return _PaymentID; }
    //        set { _PaymentID = value; }
    //    }
    //    private string _BillDate = string.Empty;
    //    public string BillDate
    //    {
    //        get { return _BillDate; }
    //        set { _BillDate = value; }
    //    }
    //    private string _Service_Details = string.Empty;
    //    public string Service_Details
    //    {
    //        get { return _Service_Details; }
    //        set { _Service_Details = value; }
    //    }
    //    private string _Service_Name = string.Empty;
    //    public string Service_Name
    //    {
    //        get { return _Service_Name; }
    //        set { _Service_Name = value; }
    //    } 
    //    private string _BillAmount = string.Empty;
    //    public string BillAmount
    //    {
    //        get { return _BillAmount; }
    //        set { _BillAmount = value; }
    //    }
    //}
}