using DocumentFormat.OpenXml.Office2010.Excel;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.TouristBus
{
    public class GetVendor_API_TouristBus_Lookup_new
    {
        string DataRecieved = "false";
        public CommonDBResonse AddTouristBusDetails(string flag,string MemberId, string Ticketno, string from_Location, string to_Location, string date, string time, string staffnum, string Cashback, string Comission, string Seat, string TotalSeat, string From_BoardingPoint, string BusNo, string CreatedBy, string value1, string value2, string value3, string value4)
        {
            CommonDBResonse ResultId = new CommonDBResonse();

            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = AddSetObject( flag, MemberId, Ticketno, from_Location,  to_Location,  date,  time,  staffnum,  Cashback,  Comission,  Seat,  TotalSeat,  From_BoardingPoint,  BusNo,  CreatedBy,  value1,  value2,  value3,  value4);
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_TouristBus_Detail", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }


        public System.Collections.Hashtable AddSetObject(string flag,string MemberId, string Ticketno, string from_Location, string to_Location, string date, string time, string staffnum, string Cashback, string Comission, string Seat, string TotalSeat, string From_BoardingPoint, string BusNo, string CreatedBy, string value1, string value2, string value3, string value4)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            if (flag == "i")
            {
                HT.Add("MemberId", MemberId);
                HT.Add("Ticketno", Ticketno);
                HT.Add("from_Location", from_Location);
                HT.Add("to_Location", to_Location);
                HT.Add("date", date);
                HT.Add("time", time);
                HT.Add("staffnum", staffnum);
                //HT.Add("Cashback", Cashback);
                //HT.Add("Comission", Comission);
                HT.Add("Seat", Seat);
                HT.Add("TotalSeat", TotalSeat);
                HT.Add("flag", flag);
                HT.Add("BusNo", BusNo);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CompanyName", value1);

            }
            else if (flag == "u") //update passenger detail
            {
                HT.Add("Name", value1);
                HT.Add("Ticketno", value2);
                HT.Add("Pickup_Location", From_BoardingPoint);
                HT.Add("ContactNumber", value3);
                HT.Add("drop_Location", value4);
                HT.Add("BusDetailId", CreatedBy);
                HT.Add("flag", flag);

            }
            else if (flag == "ua")  //update amount
            {
                HT.Add("amount", value1);
                HT.Add("Comission", value2);
                HT.Add("Cashback", value3);
                HT.Add("BusDetailId", value4);                
                HT.Add("flag", flag);

            }
            else if (flag == "us")  //update status
            {
                HT.Add("PaymentStatus", value1);
                HT.Add("Ticketno", value2);
                HT.Add("TransactionId", value3);
                HT.Add("BusDetailId", value4);
                HT.Add("flag", flag);

            }
            return HT;
        }

    }



    public class GetVendor_API_TouristBus_Routes_Response
    {
        public string status { get; set; }
        public RoutesDetail[] data { get; set; }
        public string code { get; set; }
    }

    public class RoutesDetail
    {
        public string dis_name { get; set; }
    }
    public class Tourist_TripDetail
    {
        public string from_location { get; set; }
        public string to_location { get; set; }
        public string date { get; set; }
    }


    public class Tourist_TripDetails_Response
    {
        public string status { get; set; }
        public object data { get; set; }
        public string code { get; set; }
    }

    //public class Tourist_TripDetailData
    //{
    //    public string CompanyName { get; set; }
    //    public string from_id { get; set; }
    //    public string to_id { get; set; }
    //    public string time { get; set; }
    //    public string busno { get; set; }
    //    public string via { get; set; }
    //    public string date { get; set; }
    //    public string type { get; set; }
    //    public string staffnum { get; set; }
    //    public string BUS { get; set; }
    //    public string[] from_location1 { get; set; }
    //    public string TotalSeat { get; set; }
    //    public string Price { get; set; }
    //    public int diff { get; set; }
    //    public string Amenities { get; set; }
    //    public int noofrow { get; set; }
    //    public int noofcolumn { get; set; }
    //    public int Cashback { get; set; }
    //    public string Touristbus { get; set; }
    //    public string Comission { get; set; }
    //    public string[] SeatLayoutt { get; set; }
    //}

    //public class Seatlayoutt
    //{
    //    public string bookingStatus { get; set; }
    //    public string displayName { get; set; }
    //}
    //public class Tourist_BookSeatDetails_Response
    //{
    //    public string status { get; set; }
    //    public object data { get; set; }
    //    public string code { get; set; }
    //}

}