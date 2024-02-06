using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyPay.Models.Add.AddMerchantCommission;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Extensions.Logging;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using static MyPay.Models.Add.AddMerchant;
using static QRCoder.PayloadGenerator.SwissQrCode;
using System.Collections;
using Microsoft.IdentityModel.Tokens;

namespace MyPay.Models.Add
{
    public class AddAgent
    {
        
        public enum GenderTypes
        {
            Not_Selected = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }
        public enum PEPType
        {           
            No = 1,
            Yes = 2,           
        }
        public enum meritalstatus
        {
            Not_Selected = 0,
            Unmarried = 1,
            Married = 2,
            Divorced = 3
        }
        public enum nationality
        {
            Not_Selected = 0,
            Nepalese = 1,
            Others = 2,
            Indian = 3
        }
        public enum Document
        {
            Select = 0,
            NationalID = 5,
            DrivingLicense = 2,
            VoterCard = 3,
            Citizenship = 4,
            Passport = 5,
        }
        public enum SelectPANVAT
        {
            Select=0,
           PAN=1,
           VAT=2
        }
        #region "Properties"

        //MerchantUniqueId  
        public string AgentUniqueId { get; set; }
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
        public string RoleName{ get; set; }
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
        public string NationalIdProofBack{ get; set; }
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
        public string Organizationward { get; set; }
        public string OrganizationStreet { get; set; }
        public string OrganizationHouseNo { get; set; }
        public string OrganizationPAN_VATDocument { get; set; }
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
        public bool IsActive { get; set; } 
        public string CreatedBy { get; set; }  
        public string TotalRewardPoints { get; set; } 
        public string FilterTotalCount { get; set; }
        public string PEPId { get; set; }
        public string PEP { get; set; }
        public string deviceId { get; set; }
        public string flag { get; set; }

        public string DeviceCode { get; set; }            
        public int IsKYCApproved { get; set; }
        public bool IsApprovedByAdmin { get; set; }            
        public string UserId { get; set; }
        public string JwtToken { get; set; }            
        public DateTime LastLoginAttempt { get; set; } 
        public Int64 MemberId { get; set; }
        public DateTime TokenUpdatedTime { get; set; }
        public string AgentCategoryId { get; set; }
        public string AgentCategory { get; set; }
        #endregion
        string DataRecieved = "false";

        public CommonDBResonse Add()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();

                ResultId = obj.ExecuteProcedureGetValueBusSewa("Usp_AgentUser", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public CommonDBResonse update()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("Usp_AgentUser", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("UserName", Username);
            HT.Add("Password", Password);
            HT.Add("AgentCategoryId", AgentCategoryId);
            HT.Add("DeviceID", deviceId);
            HT.Add("Individual", IsPersonalorFirm);
            HT.Add("AgentID", AgentUniqueId);
            HT.Add("Flag", flag);          
            HT.Add("IpAddress", IpAddress);
            HT.Add("UserId", UserId);
            HT.Add("MemberID", MemberId);
            HT.Add("DeviceCode", DeviceCode);
            //HT.Add("LastLoginAttempt", LastLoginAttempt);
            HT.Add("JwtToken", JwtToken); 
            //HT.Add("TokenUpdatedTime", TokenUpdatedTime);
            if (IsPersonalorFirm=="Personal")
            {
              
                HT.Add("FullName", FullName);              
                HT.Add("MobileNo", ContactNumber); 
                HT.Add("Email", EmailID);
                HT.Add("DOB", DOB); 
                HT.Add("Gender", GenderName);
                HT.Add("MaritualStatus", MaritalStatus);
                HT.Add("GrandFatherName", GrandfatherName);
                HT.Add("FatherName", FatherName); 
                HT.Add("MotherName", MotherName);
                HT.Add("Occupation", Occupation); 
                HT.Add("Nationality", Nationality);
                if (MaritalStatus.ToLower()=="married")
                {
                    HT.Add("SpouseName", SpouseName);
                }               
                HT.Add("PermanentAdress", PermanentAddress);
                HT.Add("CurrentAdress", CurrentAddress); 
                HT.Add("PermanentProvince", PermanentState);
                HT.Add("CurrentProvince", CurrentState); 
                HT.Add("PermanentDistrict", PermanentDistrict);
                HT.Add("CurrentDistrict", CurrentDistrict); 
                HT.Add("PermanentMunicipality", PermanentMunicipality);
                HT.Add("CurrentMunicipality", CurrentMunicipality); 
                HT.Add("PermanentHouseNumber", PermanentHouseNo);
                HT.Add("CurrentHouseNumber", CurrentHouseNo); 
                HT.Add("PermanentWard", Permanentward);
                HT.Add("CurrentWard", Currentward); 
                HT.Add("PermanentStreetName", PermanentStreet);
                HT.Add("CurrentStreetName", CurrentStreet); 
                HT.Add("DocumentType", DocumentType);
                HT.Add("DocumentNumber", DocumentNumber); 
                HT.Add("IssueDate", IssueDate);
                if (DocumentType.ToLower() != "citizenship") 
                {
                    HT.Add("ExpiryDate", ExpiryDate);
                }
                else
                {
                    HT.Add("BackImage", NationalIdProofBack);
                }
                
                HT.Add("IssuePlace", IssuePlace);
                HT.Add("FrontImage", NationalIdProofFront); 
                HT.Add("agentimage", AgentImage); 
                HT.Add("PanDocumentImage", PanImage); 
                HT.Add("PanNumber", PanNumber);
                HT.Add("BankName", BankName);
                HT.Add("AccountNumber", AccountNumber);
                HT.Add("AccountName", AccountName); 
                HT.Add("Branch", BranchName); 
                HT.Add("Bankcode", BANK_CD);        
                HT.Add("GenderId", GenderId);
                HT.Add("MaritalStatusId", MaritalStatusId);
                HT.Add("NationalityId", NationalityId);
                HT.Add("CurrentStateId", CurrentStateId);
                HT.Add("CurrentDistrictId", CurrentDistrictId);
                HT.Add("CurrentMunicipalityId", CurrentMunicipalityId);
                HT.Add("PermanentStateId", PermanentStateId);
                HT.Add("PermanentDistrictId", PermanentDistrictId);
                HT.Add("PermanentMunicipalityId", PermanentMunicipalityId);
                HT.Add("OccupationId", OccupationId); 
                //HT.Add("OrganizationDistrictId", OrganizationDistrictId); 
                //HT.Add("OrganizationMunicipalityId", OrganizationMunicipalityId);
                //HT.Add("OrganizationStateId", OrganizationStateId);
                HT.Add("PEP", PEP);
                HT.Add("PEPId", PEPId); 
                HT.Add("DocumentTypeId", DocumentTypeId);

            }
            else
            {                
                HT.Add("FullName", OrganizationFullName);
                HT.Add("MobileNo", OrganizationContactNumber);
                HT.Add("Email", OrganizationEmail);
                HT.Add("OrganizationRegistrationDate", OrganizationRegistrationDate);
                HT.Add("OrganizationRegistrationNumber", OrganizationRegistrationNumber);
                HT.Add("OrganizationPANVAT", OrganizationPAN_VAT);
                HT.Add("OrganizationPANVATNumber", OrganizationPAN_VATNumber);
                HT.Add("OfficeProvince", OrganizationState);
                HT.Add("OfficeDistrict", OrganizationDistrict);
                HT.Add("OfficeMunicipality", OrganizationMunicipality);
                HT.Add("OrganizationDistrictId", OrganizationDistrictId);
                HT.Add("OrganizationMunicipalityId", OrganizationMunicipalityId);
                HT.Add("OrganizationStateId", OrganizationStateId);
                HT.Add("officeWard", Organizationward);
                HT.Add("officeStreetName", OrganizationStreet);
                HT.Add("officeHouseNumber", OrganizationHouseNo);
                HT.Add("RegistrationDocument", OrganizationRegistrationDocument);
                HT.Add("PanDocumentImage", OrganizationPAN_VATDocument);
                HT.Add("DirectorFullName", DirectorFullName);
                HT.Add("DirectorContactNumber", DirectorContactNumber);
                HT.Add("DirectorEmail", DirectorEmail);
                HT.Add("DirectorFrontImage", Directorcitizenfrontimage);
                HT.Add("DirectorBackImage", Directorcitizenbackimage);
                HT.Add("DirectorRecentImage", Directorprofileimage);
                HT.Add("BankName", OrganizationBankName);
                HT.Add("Branch", OrganizationBranchName);
                HT.Add("AccountName", OrganizationAccountName);
                HT.Add("AccountNumber", OrganizationAccountNumber);
                HT.Add("Bankcode", OrganizationBANK_CD);

            }
            
            
            return HT;
        }

        //public bool update()
        //{
        //    try
        //    {
        //        DataRecieved = false;
        //        CommonHelpers obj = new CommonHelpers();
        //        System.Collections.Hashtable HT = UpdateObject();
        //        string ResultId = obj.ExecuteProcedureGetReturnValue("Usp_AgentUserList", HT);
        //        if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
        //        {
        //            AgentId = ResultId;
        //            DataRecieved = true;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DataRecieved = false;
        //    }
        //    return DataRecieved;
        //}

        //public System.Collections.Hashtable UpdateObject()
        //{
        //    System.Collections.Hashtable HT = new System.Collections.Hashtable();
        //    HT.Add("flag", "updateStatus");
        //    HT.Add("AgentUniqueId", AgentUniqueId);
        //    HT.Add("IsActive", IsActive);
        //    return HT;

        //}

    }
}
