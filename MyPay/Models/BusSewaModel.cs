using MyPay.Models.Common;
using MyPay.Models.Request.WebRequest.Common;
using OfficeOpenXml.FormulaParsing.Excel.Operators;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyPay.Models
{
    public class BusSewaModel : WebCommonProp
    {
        public int Reference { get; set; }
        public Int64 MemberId { get; set; }
        public bool IsMobile { get; set; }
        public string DataRecieved { get; set; } 
       
        public CommonDBResonse AddBusDetails(string Value1, string Value2, string value3, string value4, string value5, string Id, string ticket, string DepartureTime, string Operator, string DepartureDate, string BusType, string BusDetailId, string boardingPoints, string Seats, string flag, string status, string TransactionId, string busNo)
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = AddSetObject(Value1, Value2, value3, value4, value5, "",   Id, ticket,  DepartureTime,  Operator,  DepartureDate,  BusType,  boardingPoints,  Seats,  flag,  status,  TransactionId,  busNo);
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_BusDetail", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        //public CommonDBResonse AddBusDetails(string Value1, string Value2, string value3, string value4, string value5)
        //{
        //    CommonDBResonse ResultId = new CommonDBResonse();
        //    try
        //    {
        //        DataRecieved = "false";
        //        CommonHelpers obj = new CommonHelpers();
        //        System.Collections.Hashtable HT = SetObject( Value1,  Value2,  value3,  value4,  value5,  "","","");
        //        ResultId = obj.ExecuteProcedureGetValue("sp_BusDetail", HT);
        //    }
        //    catch (Exception ex)
        //    {
        //        // DataRecieved = "false";
        //    }
        //    return ResultId;
        //}
        public CommonDBResonse AddPassengerInfo(string inputcode, string name, string email, string contactnumber, string id,string busid,string XMLForPayType)
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject(inputcode, name, email, contactnumber, id,"P", busid, XMLForPayType);
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_BusDetail", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public System.Collections.Hashtable AddSetObject(string Value1, string Value2, string value3, string value4, string value5, string value, string Id, string value6, string DepartureTime, string Operator, string DepartureDate, string BusType,  string boardingPoints, string Seats, string flag, string status, string TransactionId, string busNo)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            if (value == "P")
            {
                HT.Add("InputCode", Value1);
                HT.Add("Name", Value2);
                HT.Add("email", value3);
                HT.Add("ContactNumber", value4);
                HT.Add("flag", "PDR");
                HT.Add("id", value5);
                HT.Add("BusDetailId", Id);
                HT.Add("XmlForPassengername", value6);
            }
            else
            {
                HT.Add("DepartureDate", DepartureDate);
                HT.Add("DepartureTime", DepartureTime);
                HT.Add("Operator", Operator);
                HT.Add("BusType", BusType);
                HT.Add("boardingPoints", boardingPoints);
                HT.Add("Seat", Seats);
                HT.Add("status", status);
                HT.Add("TripFrom", Value1);
                HT.Add("TripTo", Value2);
                HT.Add("flag", "TR");
                HT.Add("MemberId", value3);
                HT.Add("platform", value4);
                HT.Add("DeviceId", value5);
                HT.Add("Id", Id);
                HT.Add("TicketSerialNo", value6);
            }

            return HT;
        }

        public CommonDBResonse update(string OperatorContactinfo, string Id, string value, string DepartureTime, string Operator, string DepartureDate, string BusType, string BusDetailId, string boardingPoints,string Seats,string flag,string status,string TransactionId,string busNo)
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = updateSetObject(OperatorContactinfo, Id, value,  DepartureTime,  Operator,  DepartureDate,  BusType,  BusDetailId, boardingPoints, Seats, flag,status, TransactionId, busNo);
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_BusDetail", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public System.Collections.Hashtable SetObject(string Value1, string Value2, string value3, string value4, string value5,string value,string Id,string XMLForPayType)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            //if (value=="P")
            //{
            //    HT.Add("InputCode", Value1);
            //    HT.Add("Name", Value2);
            //    HT.Add("email", value3);
            //    HT.Add("ContactNumber", value4);
            //    HT.Add("flag", "PDR");
            //    HT.Add("id", value5);
            //    HT.Add("BusDetailId", Id);
            //    HT.Add("XmlForPassengername", XMLForPayType);
            //}
            if (value == "P")
            {
                HT.Add("InputCode", Value1);
                HT.Add("Name", Value2);
                HT.Add("email", value3);
                HT.Add("ContactNumber", value4);
                HT.Add("flag", "PDR");
                HT.Add("id", value5);
                HT.Add("BusDetailId", Id);
                HT.Add("PassengerBoardingPoint", XMLForPayType);
            }
            else
            {
                HT.Add("TripFrom", Value1);
                HT.Add("TripTo", Value2);
                HT.Add("flag", "TR");
                HT.Add("MemberId", value3);
                HT.Add("platform", value4) ;
                HT.Add("DeviceId", value5);
            }
                       
            return HT;
        }
        public System.Collections.Hashtable updateSetObject(string OperatorContactinfo, string Id, string value,string DepartureTime, string Operator, string DepartureDate, string BusType, string BusDetailId,string boardingPoints,string Seats,string flag, string status ,string TransactionId, string busNo)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("Id", Id);
            if (flag== "PCR")
            {
                HT.Add("Amount", value);
                HT.Add("TransactionId", TransactionId); 
                HT.Add("BusNo", busNo);
                HT.Add("TicketSerialNo", Operator);
                HT.Add("OperatorContactinfo", OperatorContactinfo);
            }
            else
            {
                HT.Add("TicketSerialNo", value);
            }                       
            HT.Add("DepartureDate", DepartureDate);
            HT.Add("DepartureTime", DepartureTime);
            HT.Add("Operator", Operator);
            HT.Add("BusType", BusType); 
            HT.Add("BusDetailId", BusDetailId);
            HT.Add("boardingPoints", boardingPoints);
            HT.Add("Seat", Seats);
            HT.Add("flag", flag);
            HT.Add("status", status);
            return HT;
        }
    }
    public class GetResponseAPI
    {
        public string message { get; set; }
        public string Id { get; set; }
    }
    public class commonresponsedata
    {
        public bool status { get; set; }
        public string StatusCode { get; set; }
        public int ReponseCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class Route
    {
        public string name { get; set; }  
    }
    
  
    public class GetVendor_API_BusSewa_Routes_Lookup
    {
        public string error { get; set; }
        public string details { get; set; }
        public string errorcode { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public object routes { get; set; }
    }
    #region bus-trips
    public class BusSewaTrip : WebCommonProp
    {
        public string Reference { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string shift { get; set; }
        public string date { get; set; }
        public Int64 MemberId { get; set; }
        public bool IsMobile { get; set; }
    }

    public class TripDatas
    {
        public int status { get; set; }
        public TripDetails[] trips { get; set; }
        public object errorCode { get; set; }
        public object message { get; set; }
    }

    public class TripDetails
    {
        public string id { get; set; }
        public object _operator { get; set; }
        public string date { get; set; }
        public string faceImage { get; set; }
        public string busNo { get; set; }
        public string busType { get; set; }
        public string departureTime { get; set; }
        public string shift { get; set; }
        public int journeyHour { get; set; }
        public string dateEn { get; set; }
        public bool lockStatus { get; set; }
        public bool multiPrice { get; set; }
        public bool touristFlag { get; set; }
        public int inputTypeCode { get; set; }
        public int noOfColumn { get; set; }
        public float rating { get; set; }
        public object[] imgList { get; set; }
        public string[] amenities { get; set; }
        public string[] detailImage { get; set; }
        public float ticketPrice { get; set; }
        public object[] passengerDetail { get; set; }
        public Seatlayout[] seatLayout { get; set; }
        public string operator_name { get; set; }
    }

    public class Seatlayout
    {
        public string displayName { get; set; }
        public string bookingStatus { get; set; }
        public string bookedByCustomer { get; set; }
    }

    #endregion
    #region refresh
    public class BusRefresh : WebCommonProp
    {
        public Int64 MemberId { get; set; }
        public bool IsMobile { get; set; }
        public string Reference { get; set; }
        public string Id { get; set; }

    }
    public class RootobjectRefresh 
    {
        public int status { get; set; }
        public bool lockStatus { get; set; }
        public int noOfColumn { get; set; }
        public object seatLayout { get; set; }
    }
    #endregion
    #region book-seat
    public class BuspaymentConfirmation : WebCommonProp
    {
        public Int64 MemberId { get; set; }
        public bool IsMobile { get; set; }
        public string id { get; set; }
        public string ticketSrlNo { get; set; }
        public string Amount { get; set; }
        public string BusDetailId { get; set; }

    }

    public class Busbookseat : WebCommonProp
    {
        public Int64 MemberId { get; set; }
        public bool IsMobile { get; set; }
        public string Reference { get; set; }
        public object data { get; set; }
        public string Operator { get; set; }
        public string BusType { get; set; }
        public string DepatureDate { get; set; }
        public string DepatureTime { get; set; }
        public string from { get; set; }
        public string to { get; set; }

    }
    public class BusPassengerInfo: WebCommonProp
    {
        public Int64 MemberId { get; set; }
        public bool IsMobile { get; set; }
        public string Reference { get; set; }
        public string inputcode { get; set; }
        public object data { get; set; }
        public string BusDetailId { get; set; }

    }
    public class BusTicketDownload : WebCommonProp
    {
        public Int64 MemberId { get; set; }
        public bool IsMobile { get; set; }
        public string Reference { get; set; }
        public string LogID { get; set; }
        public string VendorApiType { get; set; }

    }
    #endregion

    public class CustomResponseException:Exception
    {
        public bool Status { get; }
        public string StatusCode { get; }
        public int ResponseCode { get; }
        public string Message { get; }
        public object Data { get; }
        public string BusdetailId { get; }

        public CustomResponseException(bool status, string statusCode, int responseCode, string message, object data, string busdetailId)
        {
            Status = status;
            StatusCode = statusCode;
            ResponseCode = responseCode;
            Message = message;
            Data = data;
            BusdetailId = busdetailId;
        }
    }
}