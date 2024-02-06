using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.PlasmaTech 
{
    public class Res_Vendor_API_Check_Balance_Requests : CommonResponse
    {
        public AirlineBalance Data = new AirlineBalance();
        //public PlasmaBalance data
        //{
        //    get { return Data; }
        //    set { Data = value; }
        //}
    }
    public class PlasmaBalance
    {
        public AirlineBalance Balance = new AirlineBalance();
        //public AirlineBalance _Balance
        //{
        //    get { return Balance; }
        //    set { Balance = value; }
        //}
    }
    public class AirlineBalance
    {
        private Airline _Airline = new Airline();
        public Airline Airline
        {
            get { return _Airline; }
            set { _Airline = value; }
        }
    }
    public class Airline
    {
        private string _AirlineName = string.Empty;
        public string AirlineName
        {
            get { return _AirlineName; }
            set { _AirlineName = value; }
        }
        private string _AgencyName = string.Empty;
        public string AgencyName
        {
            get { return _AgencyName; }
            set { _AgencyName = value; }
        }
        private string _BalanceAmount = string.Empty;
        public string BalanceAmount
        {
            get { return _BalanceAmount; }
            set { _BalanceAmount = value; }
        }

    }
}