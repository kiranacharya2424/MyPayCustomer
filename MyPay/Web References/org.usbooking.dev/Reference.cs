﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace MyPay.org.usbooking.dev {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    using System.Text;
    using System.Net;
    using System.IO;
    using System.Xml;
    using Newtonsoft.Json;
    using System.Xml.Linq;
    using System.Linq;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="UnitedSolutionsPortBinding", Namespace="http://booking.us.org/")]
    public partial class UnitedSolutionsService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetFlightDetailOperationCompleted;
        
        private System.Threading.SendOrPostCallback IssueTicketB2BOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckBalanceOperationCompleted;
        
        private System.Threading.SendOrPostCallback SalesReportOperationCompleted;
        
        private System.Threading.SendOrPostCallback SectorCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback FlightAvailabilityOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReservationOperationCompleted;
        
        private System.Threading.SendOrPostCallback IssueTicketOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetItineraryOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public UnitedSolutionsService() {
            this.Url = global::MyPay.Properties.Settings.Default.MyPay_org_usbooking_dev_UnitedSolutionsService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetFlightDetailCompletedEventHandler GetFlightDetailCompleted;
        
        /// <remarks/>
        public event IssueTicketB2BCompletedEventHandler IssueTicketB2BCompleted;
        
        /// <remarks/>
        public event CheckBalanceCompletedEventHandler CheckBalanceCompleted;
        
        /// <remarks/>
        public event SalesReportCompletedEventHandler SalesReportCompleted;
        
        /// <remarks/>
        public event SectorCodeCompletedEventHandler SectorCodeCompleted;
        
        /// <remarks/>
        public event FlightAvailabilityCompletedEventHandler FlightAvailabilityCompleted;
        
        /// <remarks/>
        public event ReservationCompletedEventHandler ReservationCompleted;
        
        /// <remarks/>
        public event IssueTicketCompletedEventHandler IssueTicketCompleted;
        
        /// <remarks/>
        public event GetItineraryCompletedEventHandler GetItineraryCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GetFlightDetail([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strUserId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strFlightId) {
            object[] results = this.Invoke("GetFlightDetail", new object[] {
                        strUserId,
                        strFlightId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlightDetailAsync(string strUserId, string strFlightId) {
            this.GetFlightDetailAsync(strUserId, strFlightId, null);
        }
        
        /// <remarks/>
        public void GetFlightDetailAsync(string strUserId, string strFlightId, object userState) {
            if ((this.GetFlightDetailOperationCompleted == null)) {
                this.GetFlightDetailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlightDetailOperationCompleted);
            }
            this.InvokeAsync("GetFlightDetail", new object[] {
                        strUserId,
                        strFlightId}, this.GetFlightDetailOperationCompleted, userState);
        }
        
        private void OnGetFlightDetailOperationCompleted(object arg) {
            if ((this.GetFlightDetailCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlightDetailCompleted(this, new GetFlightDetailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IssueTicketB2B([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strFlightId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strReturnFlightId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strContactName, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strContactEmail, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strContactMobile, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strPassengerDetail) {
            object[] results = this.Invoke("IssueTicketB2B", new object[] {
                        strFlightId,
                        strReturnFlightId,
                        strContactName,
                        strContactEmail,
                        strContactMobile,
                        strPassengerDetail});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void IssueTicketB2BAsync(string strFlightId, string strReturnFlightId, string strContactName, string strContactEmail, string strContactMobile, string strPassengerDetail) {
            this.IssueTicketB2BAsync(strFlightId, strReturnFlightId, strContactName, strContactEmail, strContactMobile, strPassengerDetail, null);
        }
        
        /// <remarks/>
        public void IssueTicketB2BAsync(string strFlightId, string strReturnFlightId, string strContactName, string strContactEmail, string strContactMobile, string strPassengerDetail, object userState) {
            if ((this.IssueTicketB2BOperationCompleted == null)) {
                this.IssueTicketB2BOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIssueTicketB2BOperationCompleted);
            }
            this.InvokeAsync("IssueTicketB2B", new object[] {
                        strFlightId,
                        strReturnFlightId,
                        strContactName,
                        strContactEmail,
                        strContactMobile,
                        strPassengerDetail}, this.IssueTicketB2BOperationCompleted, userState);
        }
        
        private void OnIssueTicketB2BOperationCompleted(object arg) {
            if ((this.IssueTicketB2BCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IssueTicketB2BCompleted(this, new IssueTicketB2BCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CheckBalance([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strUserId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strAirlineId) {
            object[] results = this.Invoke("CheckBalance", new object[] {
                        strUserId,
                        strAirlineId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CheckBalanceAsync(string strUserId, string strAirlineId) {
            this.CheckBalanceAsync(strUserId, strAirlineId, null);
        }
        
        /// <remarks/>
        public void CheckBalanceAsync(string strUserId, string strAirlineId, object userState) {
            if ((this.CheckBalanceOperationCompleted == null)) {
                this.CheckBalanceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckBalanceOperationCompleted);
            }
            this.InvokeAsync("CheckBalance", new object[] {
                        strUserId,
                        strAirlineId}, this.CheckBalanceOperationCompleted, userState);
        }
        
        private void OnCheckBalanceOperationCompleted(object arg) {
            if ((this.CheckBalanceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckBalanceCompleted(this, new CheckBalanceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SalesReport([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strUserId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strPassword, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strAgencyId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strFromDate, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strToDate) {
            object[] results = this.Invoke("SalesReport", new object[] {
                        strUserId,
                        strPassword,
                        strAgencyId,
                        strFromDate,
                        strToDate});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SalesReportAsync(string strUserId, string strPassword, string strAgencyId, string strFromDate, string strToDate) {
            this.SalesReportAsync(strUserId, strPassword, strAgencyId, strFromDate, strToDate, null);
        }
        
        /// <remarks/>
        public void SalesReportAsync(string strUserId, string strPassword, string strAgencyId, string strFromDate, string strToDate, object userState) {
            if ((this.SalesReportOperationCompleted == null)) {
                this.SalesReportOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSalesReportOperationCompleted);
            }
            this.InvokeAsync("SalesReport", new object[] {
                        strUserId,
                        strPassword,
                        strAgencyId,
                        strFromDate,
                        strToDate}, this.SalesReportOperationCompleted, userState);
        }
        
        private void OnSalesReportOperationCompleted(object arg) {
            if ((this.SalesReportCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SalesReportCompleted(this, new SalesReportCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace= "http://booking.us.org/", ResponseNamespace= "http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SectorCode([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strUserId) {
            object[] results = this.Invoke("SectorCode", new object[] {
                        strUserId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SectorCodeAsync(string strUserId) {
            this.SectorCodeAsync(strUserId, null);
        }
        
        /// <remarks/>
        public void SectorCodeAsync(string strUserId, object userState) {
            if ((this.SectorCodeOperationCompleted == null)) {
                this.SectorCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSectorCodeOperationCompleted);
            }
            this.InvokeAsync("SectorCode", new object[] {
                        strUserId}, this.SectorCodeOperationCompleted, userState);
        }
        
        private void OnSectorCodeOperationCompleted(object arg) {
            if ((this.SectorCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SectorCodeCompleted(this, new SectorCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FlightAvailability([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strUserId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strPassword, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strAgencyId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strSectorFrom, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strSectorTo, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strFlightDate, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strReturnDate, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strTripType, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strNationality, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string intAdult, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string intChild, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strClientIP) {
            object[] results = this.Invoke("FlightAvailability", new object[] {
                        strUserId,
                        strPassword,
                        strAgencyId,
                        strSectorFrom,
                        strSectorTo,
                        strFlightDate,
                        strReturnDate,
                        strTripType,
                        strNationality,
                        intAdult,
                        intChild,
                        strClientIP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FlightAvailabilityAsync(string strUserId, string strPassword, string strAgencyId, string strSectorFrom, string strSectorTo, string strFlightDate, string strReturnDate, string strTripType, string strNationality, string intAdult, string intChild, string strClientIP) {
            this.FlightAvailabilityAsync(strUserId, strPassword, strAgencyId, strSectorFrom, strSectorTo, strFlightDate, strReturnDate, strTripType, strNationality, intAdult, intChild, strClientIP, null);
        }
        
        /// <remarks/>
        public void FlightAvailabilityAsync(string strUserId, string strPassword, string strAgencyId, string strSectorFrom, string strSectorTo, string strFlightDate, string strReturnDate, string strTripType, string strNationality, string intAdult, string intChild, string strClientIP, object userState) {
            if ((this.FlightAvailabilityOperationCompleted == null)) {
                this.FlightAvailabilityOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFlightAvailabilityOperationCompleted);
            }
            this.InvokeAsync("FlightAvailability", new object[] {
                        strUserId,
                        strPassword,
                        strAgencyId,
                        strSectorFrom,
                        strSectorTo,
                        strFlightDate,
                        strReturnDate,
                        strTripType,
                        strNationality,
                        intAdult,
                        intChild,
                        strClientIP}, this.FlightAvailabilityOperationCompleted, userState);
        }
        
        private void OnFlightAvailabilityOperationCompleted(object arg) {
            if ((this.FlightAvailabilityCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FlightAvailabilityCompleted(this, new FlightAvailabilityCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Reservation([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strFlightId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strReturnFlightId) {
            object[] results = this.Invoke("Reservation", new object[] {
                        strFlightId,
                        strReturnFlightId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ReservationAsync(string strFlightId, string strReturnFlightId) {
            this.ReservationAsync(strFlightId, strReturnFlightId, null);
        }
        
        /// <remarks/>
        public void ReservationAsync(string strFlightId, string strReturnFlightId, object userState) {
            if ((this.ReservationOperationCompleted == null)) {
                this.ReservationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReservationOperationCompleted);
            }
            this.InvokeAsync("Reservation", new object[] {
                        strFlightId,
                        strReturnFlightId}, this.ReservationOperationCompleted, userState);
        }
        
        private void OnReservationOperationCompleted(object arg) {
            if ((this.ReservationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReservationCompleted(this, new ReservationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IssueTicket([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strFlightId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strReturnFlightId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strContactName, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strContactEmail, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strContactMobile, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strPassengerDetail) {
            object[] results = this.Invoke("IssueTicket", new object[] {
                        strFlightId,
                        strReturnFlightId,
                        strContactName,
                        strContactEmail,
                        strContactMobile,
                        strPassengerDetail});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void IssueTicketAsync(string strFlightId, string strReturnFlightId, string strContactName, string strContactEmail, string strContactMobile, string strPassengerDetail) {
            this.IssueTicketAsync(strFlightId, strReturnFlightId, strContactName, strContactEmail, strContactMobile, strPassengerDetail, null);
        }
        
        /// <remarks/>
        public void IssueTicketAsync(string strFlightId, string strReturnFlightId, string strContactName, string strContactEmail, string strContactMobile, string strPassengerDetail, object userState) {
            if ((this.IssueTicketOperationCompleted == null)) {
                this.IssueTicketOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIssueTicketOperationCompleted);
            }
            this.InvokeAsync("IssueTicket", new object[] {
                        strFlightId,
                        strReturnFlightId,
                        strContactName,
                        strContactEmail,
                        strContactMobile,
                        strPassengerDetail}, this.IssueTicketOperationCompleted, userState);
        }
        
        private void OnIssueTicketOperationCompleted(object arg) {
            if ((this.IssueTicketCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IssueTicketCompleted(this, new IssueTicketCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://booking.us.org/", ResponseNamespace="http://booking.us.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GetItinerary([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strPnoNo, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strTicketNo, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string strAgencyId) {
            object[] results = this.Invoke("GetItinerary", new object[] {
                        strPnoNo,
                        strTicketNo,
                        strAgencyId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetItineraryAsync(string strPnoNo, string strTicketNo, string strAgencyId) {
            this.GetItineraryAsync(strPnoNo, strTicketNo, strAgencyId, null);
        }
        
        /// <remarks/>
        public void GetItineraryAsync(string strPnoNo, string strTicketNo, string strAgencyId, object userState) {
            if ((this.GetItineraryOperationCompleted == null)) {
                this.GetItineraryOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetItineraryOperationCompleted);
            }
            this.InvokeAsync("GetItinerary", new object[] {
                        strPnoNo,
                        strTicketNo,
                        strAgencyId}, this.GetItineraryOperationCompleted, userState);
        }
        
        private void OnGetItineraryOperationCompleted(object arg) {
            if ((this.GetItineraryCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetItineraryCompleted(this, new GetItineraryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void GetFlightDetailCompletedEventHandler(object sender, GetFlightDetailCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlightDetailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlightDetailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void IssueTicketB2BCompletedEventHandler(object sender, IssueTicketB2BCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IssueTicketB2BCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IssueTicketB2BCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void CheckBalanceCompletedEventHandler(object sender, CheckBalanceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckBalanceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckBalanceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void SalesReportCompletedEventHandler(object sender, SalesReportCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SalesReportCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SalesReportCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void SectorCodeCompletedEventHandler(object sender, SectorCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SectorCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SectorCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void FlightAvailabilityCompletedEventHandler(object sender, FlightAvailabilityCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FlightAvailabilityCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FlightAvailabilityCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void ReservationCompletedEventHandler(object sender, ReservationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReservationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReservationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void IssueTicketCompletedEventHandler(object sender, IssueTicketCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IssueTicketCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IssueTicketCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void GetItineraryCompletedEventHandler(object sender, GetItineraryCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetItineraryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetItineraryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591