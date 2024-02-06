using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddFlightPassengersDetails : CommonAdd
    {
        private Int64 _BookingId = 0;
        public Int64 BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }
        private string _Firstname = string.Empty;
        public string Firstname
        {
            get { return _Firstname; }
            set { _Firstname = value; }
        }
        private string _Lastname = string.Empty;
        public string Lastname
        {
            get { return _Lastname; }
            set { _Lastname = value; }
        }
        private string _Type = string.Empty;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Gender = string.Empty;
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private string _TicketNo = string.Empty;
        public string TicketNo
        {
            get { return _TicketNo; }
            set { _TicketNo = value; }
        }

        private string _InboundTicketNo = string.Empty;
        public string InboundTicketNo
        {
            get { return _InboundTicketNo; }
            set { _InboundTicketNo = value; }
        }

        private string _BarCode = string.Empty;
        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }
        private string _InboundBarCode = string.Empty;
        public string InboundBarCode
        {
            get { return _InboundBarCode; }
            set { _InboundBarCode = value; }
        }
        private string _Nationality = string.Empty;
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }

        private int _CheckDelete = 2;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }

        }

        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private string _FlightId = string.Empty;
        public string FlightId
        {
            get { return _FlightId; }
            set { _FlightId = value; }
        }
    }
}