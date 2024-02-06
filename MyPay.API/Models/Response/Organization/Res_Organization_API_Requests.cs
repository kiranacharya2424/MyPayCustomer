using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_Organization_API_Requests
    {
        public int id { get; set; }
        public int orgId { get; set; }
        public string jsonField { get; set; }
        public string orgFormTypeName { get; set; }
        public bool isDeleted { get; set; }
        public Tbl_Organizer tbl_organizer { get; set; }
    }
    public class Tbl_Organizer
    {

        public int Id { get; set; }
        private string _OrganizationName = string.Empty;
        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        public string FullAddress { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RegistrationNumber { get; set; }
        public string DirectorName { get; set; }
        public string VatPanNumber { get; set; }
        [NotMapped]
        public string VatPanImage { get; set; }
        public string Images { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedLocalDate { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        
    }
}