using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Registration_API_Requests : CommonProp
    {
        public int id { get; set; }
        public int orgId { get; set; }
        public string jsonField { get; set; }
        public string eventName { get; set; }
        public DateTime createdDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool isActive{ get; set; }
        public bool isDeleted { get; set; }
    }
    public class RegistrationForm
    {
        public int id { get; set; }
        public int orgId { get; set; }
        public string jsonField { get; set; }
        public string eventName { get; set; }
        public DateTime createdDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }

}