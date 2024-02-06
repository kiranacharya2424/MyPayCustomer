using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.Get
{
    public class GetAgent:CommonGet
    {
        #region "Properties"

        //MerchantUniqueId  
        public string AgentUniqueId { get; set; }
        public string flag { get; set; }
        public string AgentId { get; set; }
        public string IsPersonalorFirm { get; set; }
        public string OrganizationName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+(\\s\\({0,1}[A-Za-z]+\\.{0,1}[A-Za-z]*\\.{0,1}\\){0,1})+$", ErrorMessage = "Invalid full name")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression("^[9][0-9]*$", ErrorMessage = "Phone Number Start With 9")]
        [MaxLength(10, ErrorMessage = "Mobile Number Max Length is Invalid"), MinLength(10, ErrorMessage = "Mobile Number Minimum Length is Invalid")]
        public string ContactNumber { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9_.+-])+\\@(([a-zA-Z0-9-])+\\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid email")]
        public string EmailID { get; set; }
        [Required]
        public string DOB { get; set; }
        [Required]
        public string GenderId { get; set; }
        public string GenderName { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        public string MaritalStatusId { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+(\\s\\({0,1}[A-Za-z]+\\.{0,1}[A-Za-z]*\\.{0,1}\\){0,1})+$", ErrorMessage = "Invalid grandfather name")]

        public string GrandfatherName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+(\\s\\({0,1}[A-Za-z]+\\.{0,1}[A-Za-z]*\\.{0,1}\\){0,1})+$", ErrorMessage = "Invalid father name")]
        public string FatherName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+(\\s\\({0,1}[A-Za-z]+\\.{0,1}[A-Za-z]*\\.{0,1}\\){0,1})+$", ErrorMessage = "Invalid mother name")]
        public string MotherName { get; set; }
        [Required]
        public string Occupation { get; set; }
        public string OccupationId { get; set; }
        [RegularExpression("^[A-Za-z]+(\\s\\({0,1}[A-Za-z]+\\.{0,1}[A-Za-z]*\\.{0,1}\\){0,1})+$", ErrorMessage = "Invalid spouse name")]
        public string SpouseName { get; set; }
        [Required]
        public string NationalityId { get; set; }
        public string Nationality { get; set; }
        public string Image { get; set; }
        [Required]
        public string PermanentAddress { get; set; }
        [Required]
        public string CurrentAddress { get; set; }
        [Required]
        public string PermanentDistrictId { get; set; }
        public string PermanentDistrict { get; set; }
        [Required]
        public string CurrentDistrictId { get; set; }
        public string CurrentDistrict { get; set; }
        [Required]
        public string PermanentMunicipalityId { get; set; }
        public string PermanentMunicipality { get; set; }
        [Required]
        public string CurrentMunicipalityId { get; set; }
        public string CurrentMunicipality { get; set; }
        [Required]
        public string Permanentward { get; set; }
        [Required]
        public string Currentward { get; set; }

        public string PermanentStreet { get; set; }

        public string CurrentStreet { get; set; }

        public string PermanentHouseNo { get; set; }

        public string CurrentHouseNo { get; set; }
        [Required]
        public string PermanentStateId { get; set; }
        public string PermanentState { get; set; }
        [Required]
        public string CurrentStateId { get; set; }
        public string CurrentState { get; set; }
        public string CreatedDateDt { get; set; }
        public string UpdatedDateDt { get; set; }
        public string TotalUserCount { get; set; }
        public string SuccessURL { get; set; }
        public string CancelURL { get; set; }
        public string WebsiteURL { get; set; }
        public string IpAddress { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Int64 MerchantMemberId { get; set; }
        public string AgentTotalAmount { get; set; }
        public string AgentIpAddress { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        [Required]
        public string DocumentNumber { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        public string IssueDate { get; set; }
        [Required]
        public string IssuePlace { get; set; }

        public string NationalIdProofFront { get; set; }
        public string NationalIdProofBack { get; set; }
        public string AgentImage { get; set; }
        public string PanNumber { get; set; }

        public string PanImage { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }


        [Required]
        public string OrganizationFullName { get; set; }
        [Required]
        [RegularExpression("^[9][0-9]*$", ErrorMessage = "Organization contact number start with 9")]
        [MaxLength(10, ErrorMessage = "Organization contact number max length is invalid"), MinLength(10, ErrorMessage = "Organization contact number minimum length is invalid")]

        public string OrganizationContactNumber { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9_.+-])+\\@(([a-zA-Z0-9-])+\\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid organization email")]
        public string OrganizationEmail { get; set; }
        [Required]
        public string OrganizationRegistrationDate { get; set; }
        [Required]
        public string OrganizationRegistrationNumber { get; set; }
        [Required]
        public string OrganizationPAN_VATNumber { get; set; }
        public string OrganizationPAN_VAT { get; set; }
        [Required]
        public string OrganizationPAN_VATId { get; set; }
        public string OrganizationBankName { get; set; }
        public string OrganizationBranchName { get; set; }
        public string OrganizationAccountNumber { get; set; }
        public string OrganizationAccountName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+(\\s\\({0,1}[A-Za-z]+\\.{0,1}[A-Za-z]*\\.{0,1}\\){0,1})+$", ErrorMessage = "Invalid director full name")]
        public string DirectorFullName { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9_.+-])+\\@(([a-zA-Z0-9-])+\\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid director email")]
        public string DirectorEmail { get; set; }
        [Required]
        [RegularExpression("^[9][0-9]*$", ErrorMessage = "Director contact number start with 9")]
        [MaxLength(10, ErrorMessage = "Director contact number max length is invalid"), MinLength(10, ErrorMessage = "Director contact number minimum length is invalid")]

        public string DirectorContactNumber { get; set; }

        public string Directorcitizenfrontimage { get; set; }

        public string Directorcitizenbackimage { get; set; }

        public string Directorprofileimage { get; set; }
        [Required]
        public string OrganizationDistrictId { get; set; }
        public string OrganizationDistrict { get; set; }
        [Required]
        public string OrganizationMunicipalityId { get; set; }
        public string OrganizationMunicipality { get; set; }
        [Required]
        public string OrganizationStateId { get; set; }
        public string OrganizationState { get; set; }
        [Required]
        public string Organizationward { get; set; }
        public string OrganizationStreet { get; set; }
        public string OrganizationHouseNo { get; set; }
        [Required]
        public string OrganizationPAN_VATDocument { get; set; }
        [Required]
        public string OrganizationRegistrationDocument { get; set; }
        public string BANK_CD { get; set; }
        public string OrganizationBANK_CD { get; set; }
        public string NoOfAgent { get; set; }
        public string NoOfUser { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Sno { get; set; }
        public string AvailableBalance { get; set; }
        public string CommissionBalance { get; set; }
        public string AvailableCoins { get; set; }
        public string Status { get; set; }
        public string FilterTotalCount { get; set; }
        public bool IsActive { get; set; }

        #endregion

        public DataTable GetAgentData(string flag,string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("flag", flag);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("FullName", FullName);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("EmailID", EmailID);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("AgentUniqueId", AgentUniqueId); 
                HT.Add("IsActive", "");
                dt = obj.GetDataFromStoredProcedure("Usp_AgentUserList", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        public DataTable UpdateData()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("flag", "updateStatus");
                HT.Add("AgentUniqueId", AgentUniqueId);
                HT.Add("IsActive", IsActive);
                dt = obj.GetDataFromStoredProcedure("Usp_AgentUserList", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
    }
}
