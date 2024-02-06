using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{

    public class AddRemittanceUser : CommonAdd
    {
        bool DataRecieved = false;
        #region "Enum"
        public enum RemittanceType
        {
            Prefund = 1,
            ODL = 2

        }
        public enum RemittanceFeeType
        {
            Slab = 0,
            Fixed = 1
        }
        public enum ProofTypes
        {
            Passport = 1,
            Driving_Licence = 2,
            Voter_Id = 3,
            Citizenship = 4,
            National_IdCard = 5
        }
        public enum AddressProofTypes
        {
            Bank_Statement = 1,
            Driving_Licence = 2,
            Teleco_Bill = 3,
            Other = 4
        }
        public enum GenderTypes
        {
            //All = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }
        #endregion
        #region "Properties" 
        private Int32 _Type = 0;
        public Int32 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private String _MerchantUniqueId = string.Empty;
        public String MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }
        private String _OrganizationName = string.Empty;
        public String OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        private String _ContactName = string.Empty;
        public String ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        private String _EmailID = string.Empty;
        public String EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }
        private String _ContactNo = string.Empty;
        public String ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        private String _UserName = string.Empty;
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private String _Password = string.Empty;
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private bool _IsPasswordReset = false;
        public bool IsPasswordReset
        {
            get { return _IsPasswordReset; }
            set { _IsPasswordReset = value; }
        }
        private String _apikey = string.Empty;
        public String apikey
        {
            get { return _apikey; }
            set { _apikey = value; }
        }
        private String _secretkey = string.Empty;
        public String secretkey
        {
            get { return _secretkey; }
            set { _secretkey = value; }
        }
        private String _Image = string.Empty;
        public String Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        private String _Address = string.Empty;
        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private String _City = string.Empty;
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        private String _State = string.Empty;
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }
        private Int64 _CountryId = 0;
        public Int64 CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        private String _CountryName = string.Empty;
        public String CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }
        private String _ZipCode = string.Empty;
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        private String _SuccessURL = string.Empty;
        public String SuccessURL
        {
            get { return _SuccessURL; }
            set { _SuccessURL = value; }
        }
        private String _CancelURL = string.Empty;
        public String CancelURL
        {
            get { return _CancelURL; }
            set { _CancelURL = value; }
        }
        private String _IpAddress = Common.Common.GetUserIP();
        public String IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        private String _WebsiteURL = string.Empty;
        public String WebsiteURL
        {
            get { return _WebsiteURL; }
            set { _WebsiteURL = value; }
        }
        private Int64 _RoleId = 0;
        public Int64 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private String _RoleName = string.Empty;
        public String RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        private Int64 _MerchantMemberId = 0;
        public Int64 MerchantMemberId
        {
            get { return _MerchantMemberId; }
            set { _MerchantMemberId = value; }
        }
        private String _MerchantIpAddress = string.Empty;
        public String MerchantIpAddress
        {
            get { return _MerchantIpAddress; }
            set { _MerchantIpAddress = value; }
        }
        private String _PublicKey = string.Empty;
        public String PublicKey
        {
            get { return _PublicKey; }
            set { _PublicKey = value; }
        }
        private String _PrivateKey = string.Empty;
        public String PrivateKey
        {
            get { return _PrivateKey; }
            set { _PrivateKey = value; }
        }
        private String _API_User = string.Empty;
        public String API_User
        {
            get { return _API_User; }
            set { _API_User = value; }
        }
        private String _API_Password = string.Empty;
        public String API_Password
        {
            get { return _API_Password; }
            set { _API_Password = value; }
        }
        private Int32 _Take = 0;
        public Int32 Take
        {
            get { return _Take; }
            set { _Take = value; }
        }
        private Int32 _Skip = 0;
        public Int32 Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
        private Int32 _CheckDelete = 2;
        public Int32 CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }
        private Int32 _CheckActive = 2;
        public Int32 CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }
        private Int32 _CheckApprovedByAdmin = 2;
        public Int32 CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }
        private String _CheckCreatedDate = string.Empty;
        public String CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }
        private String _StartDate = string.Empty;
        public String StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private String _EndDate = string.Empty;
        public String EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }
        private string _TotalUserCount = string.Empty;
        public string TotalUserCount
        {
            get { return _TotalUserCount; }
            set { _TotalUserCount = value; }
        }

        private int _FromCurrencyId = 0;
        public int FromCurrencyId
        {
            get { return _FromCurrencyId; }
            set { _FromCurrencyId = value; }
        }

        private string _FromCurrencyCode = string.Empty;
        public string FromCurrencyCode
        {
            get { return _FromCurrencyCode; }
            set { _FromCurrencyCode = value; }
        }
        private string _CompanyRegistrationNo = string.Empty;
        public string CompanyRegistrationNo
        {
            get { return _CompanyRegistrationNo; }
            set { _CompanyRegistrationNo = value; }
        }

        private DateTime _DateOfBirth = System.DateTime.UtcNow;
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type")]
        private int _GenderType = 0;
        public int GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type Name")]
        private string _GenderTypeName = string.Empty;
        public string GenderTypeName
        {
            get { return _GenderTypeName; }
            set { _GenderTypeName = value; }
        }
        private GenderTypes _GenderTypeEnum = 0;
        public GenderTypes GenderTypeEnum
        {
            get { return _GenderTypeEnum; }
            set { _GenderTypeEnum = value; }
        }
        private Int32 _ProofType = 0;
        public Int32 ProofType
        {
            get { return _ProofType; }
            set { _ProofType = value; }
        }

        private string _DocumentNo = string.Empty;
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        private DateTime _DocumentExpiryDate = System.DateTime.UtcNow;
        public DateTime DocumentExpiryDate
        {
            get { return _DocumentExpiryDate; }
            set { _DocumentExpiryDate = value; }
        }
        private string _AddressProofType = string.Empty;
        public string AddressProofType
        {
            get { return _AddressProofType; }
            set { _AddressProofType = value; }
        }
        private string _AddressProofImage = string.Empty;
        public string AddressProofImage
        {
            get { return _AddressProofImage; }
            set { _AddressProofImage = value; }
        }
        private string _OtherProofType = string.Empty;
        public string OtherProofType
        {
            get { return _OtherProofType; }
            set { _OtherProofType = value; }
        }
        private string _IDProofFrontImage = string.Empty;
        public string IDProofFrontImage
        {
            get { return _IDProofFrontImage; }
            set { _IDProofFrontImage = value; }
        }
        private string _AdditionalDoc = string.Empty;
        public string AdditionalDoc
        {
            get { return _AdditionalDoc; }
            set { _AdditionalDoc = value; }
        }
        private string _IDProofBackImage = string.Empty;
        public string IDProofBackImage
        {
            get { return _IDProofBackImage; }
            set { _IDProofBackImage = value; }
        }
        private string _CompanyDirectorname = string.Empty;
        public string CompanyDirectorname
        {
            get { return _CompanyDirectorname; }
            set { _CompanyDirectorname = value; }
        }
        private decimal _Fee = 0;
        public decimal Fee
        {
            get { return _Fee; }
            set { _Fee = value; }
        }
        private int _FeeType = 0;
        public int FeeType
        {
            get { return _FeeType; }
            set { _FeeType = value; }
        }
        private decimal _FeeAccountBalance = 0;
        public decimal FeeAccountBalance
        {
            get { return _FeeAccountBalance; }
            set { _FeeAccountBalance = value; }
        }
        private decimal _MarginConversionRate = 0;
        public decimal MarginConversionRate
        {
            get { return _MarginConversionRate; }
            set { _MarginConversionRate = value; }
        }
        private string _FeeTypeName = string.Empty;
        public string FeeTypeName
        {
            get { return _FeeTypeName; }
            set { _FeeTypeName = value; }
        }
        #endregion

        #region "Add Delete Update Methods" 
        public bool Add()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceUser_AddNew", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool Update()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceUser_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("MerchantUniqueId", MerchantUniqueId);
            HT.Add("OrganizationName", OrganizationName);
            HT.Add("ContactName", ContactName);
            HT.Add("EmailID", EmailID);
            HT.Add("ContactNo", ContactNo);
            HT.Add("UserName", UserName);
            HT.Add("Password", Password);
            HT.Add("IsPasswordReset", IsPasswordReset);
            HT.Add("apikey", apikey);
            HT.Add("secretkey", secretkey);
            HT.Add("Image", Image);
            HT.Add("Address", Address);
            HT.Add("City", City);
            HT.Add("State", State);
            HT.Add("CountryId", CountryId);
            HT.Add("CountryName", CountryName);
            HT.Add("ZipCode", ZipCode);
            HT.Add("SuccessURL", SuccessURL);
            HT.Add("CancelURL", CancelURL);
            HT.Add("IpAddress", IpAddress);
            HT.Add("WebsiteURL", WebsiteURL);
            HT.Add("RoleId", RoleId);
            HT.Add("RoleName", RoleName);
            HT.Add("MerchantMemberId", MerchantMemberId);
            HT.Add("MerchantIpAddress", MerchantIpAddress);
            HT.Add("PublicKey", PublicKey);
            HT.Add("PrivateKey", PrivateKey);
            HT.Add("API_User", API_User);
            HT.Add("API_Password", API_Password);
            HT.Add("Type", Type);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("IsDeleted", IsDeleted);
            HT.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            HT.Add("IsActive", IsActive);
            HT.Add("FromCurrencyId", FromCurrencyId);
            HT.Add("FromCurrencyCode", FromCurrencyCode);
            HT.Add("CompanyRegistrationNo", CompanyRegistrationNo);
            HT.Add("DateOfBirth", DateOfBirth);
            HT.Add("Gender", GenderType.ToString());
            HT.Add("ProofType", ProofType.ToString());
            HT.Add("DocumentNo", DocumentNo);
            HT.Add("DocumentExpiryDate", DocumentExpiryDate);
            HT.Add("AddressProofType", AddressProofType);
            HT.Add("AddressProofImage", AddressProofImage);
            HT.Add("OtherProofType", OtherProofType);
            HT.Add("IDProofFrontImage", IDProofFrontImage);
            HT.Add("AdditionalDoc", AdditionalDoc);
            HT.Add("IDProofBackImage", IDProofBackImage);
            HT.Add("CompanyDirectorName", CompanyDirectorname);
            HT.Add("FeeType", FeeType);
            HT.Add("Fee", Fee);


            return HT;
        }
        #endregion

        #region "Get Methods" 
        public System.Data.DataTable GetList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactName", ContactName);
                HT.Add("EmailID", EmailID);
                HT.Add("ContactNo", ContactNo);
                HT.Add("UserName", UserName);
                HT.Add("Password", Password);
                HT.Add("IsPasswordReset", IsPasswordReset);
                HT.Add("apikey", apikey);
                HT.Add("secretkey", secretkey);
                HT.Add("Image", Image);
                HT.Add("Address", Address);
                HT.Add("City", City);
                HT.Add("State", State);
                HT.Add("CountryId", CountryId);
                HT.Add("CountryName", CountryName);
                HT.Add("ZipCode", ZipCode);
                HT.Add("SuccessURL", SuccessURL);
                HT.Add("CancelURL", CancelURL);
                HT.Add("IpAddress", IpAddress);
                HT.Add("WebsiteURL", WebsiteURL);
                HT.Add("RoleId", RoleId);
                HT.Add("RoleName", RoleName);
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantIpAddress", MerchantIpAddress);
                HT.Add("PublicKey", PublicKey);
                HT.Add("PrivateKey", PrivateKey);
                HT.Add("API_User", API_User);
                HT.Add("API_Password", API_Password);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);
                HT.Add("Type", Type);
                HT.Add("FromCurrencyCode", FromCurrencyCode);
                HT.Add("FromCurrencyId", FromCurrencyId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceUser_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public bool GetRecord()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactName", ContactName);
                HT.Add("EmailID", EmailID);
                HT.Add("ContactNo", ContactNo);
                HT.Add("UserName", UserName);
                HT.Add("Password", Password);
                HT.Add("IsPasswordReset", IsPasswordReset);
                HT.Add("apikey", apikey);
                HT.Add("secretkey", secretkey);
                HT.Add("Image", Image);
                HT.Add("Address", Address);
                HT.Add("City", City);
                HT.Add("State", State);
                HT.Add("CountryId", CountryId);
                HT.Add("CountryName", CountryName);
                HT.Add("ZipCode", ZipCode);
                HT.Add("SuccessURL", SuccessURL);
                HT.Add("CancelURL", CancelURL);
                HT.Add("IpAddress", IpAddress);
                HT.Add("WebsiteURL", WebsiteURL);
                HT.Add("RoleId", RoleId);
                HT.Add("RoleName", RoleName);
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantIpAddress", MerchantIpAddress);
                HT.Add("PublicKey", PublicKey);
                HT.Add("PrivateKey", PrivateKey);
                HT.Add("API_User", API_User);
                HT.Add("API_Password", API_Password);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);
                HT.Add("Type", Type);
                HT.Add("FromCurrencyCode", FromCurrencyCode);
                HT.Add("FromCurrencyId", FromCurrencyId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceUser_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MerchantUniqueId = Convert.ToString(dt.Rows[0]["MerchantUniqueId"].ToString());
                    OrganizationName = Convert.ToString(dt.Rows[0]["OrganizationName"].ToString());
                    ContactName = Convert.ToString(dt.Rows[0]["ContactName"].ToString());
                    EmailID = Convert.ToString(dt.Rows[0]["EmailID"].ToString());
                    ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                    UserName = Convert.ToString(dt.Rows[0]["UserName"].ToString());
                    Password = Convert.ToString(dt.Rows[0]["Password"].ToString());
                    IsPasswordReset = Convert.ToBoolean(dt.Rows[0]["IsPasswordReset"].ToString());
                    apikey = Convert.ToString(dt.Rows[0]["apikey"].ToString());
                    secretkey = Convert.ToString(dt.Rows[0]["secretkey"].ToString());
                    Image = Convert.ToString(dt.Rows[0]["Image"].ToString());
                    Address = Convert.ToString(dt.Rows[0]["Address"].ToString());
                    City = Convert.ToString(dt.Rows[0]["City"].ToString());
                    State = Convert.ToString(dt.Rows[0]["State"].ToString());
                    CountryId = Convert.ToInt64(dt.Rows[0]["CountryId"].ToString());
                    CountryName = Convert.ToString(dt.Rows[0]["CountryName"].ToString());
                    ZipCode = Convert.ToString(dt.Rows[0]["ZipCode"].ToString());
                    SuccessURL = Convert.ToString(dt.Rows[0]["SuccessURL"].ToString());
                    CancelURL = Convert.ToString(dt.Rows[0]["CancelURL"].ToString());
                    IpAddress = Convert.ToString(dt.Rows[0]["IpAddress"].ToString());
                    WebsiteURL = Convert.ToString(dt.Rows[0]["WebsiteURL"].ToString());
                    RoleId = Convert.ToInt64(dt.Rows[0]["RoleId"].ToString());
                    RoleName = Convert.ToString(dt.Rows[0]["RoleName"].ToString());
                    MerchantMemberId = Convert.ToInt64(dt.Rows[0]["MerchantMemberId"].ToString());
                    MerchantIpAddress = Convert.ToString(dt.Rows[0]["MerchantIpAddress"].ToString());
                    PublicKey = Convert.ToString(dt.Rows[0]["PublicKey"].ToString());
                    PrivateKey = Convert.ToString(dt.Rows[0]["PrivateKey"].ToString());
                    API_User = Convert.ToString(dt.Rows[0]["API_User"].ToString());
                    API_Password = Convert.ToString(dt.Rows[0]["API_Password"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = Convert.ToString(dt.Rows[0]["CreatedByName"].ToString());
                    Sno = Convert.ToString(dt.Rows[0]["Sno"].ToString());
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    UpdatedBy = Convert.ToInt64(dt.Rows[0]["UpdatedBy"].ToString());
                    UpdatedByName = Convert.ToString(dt.Rows[0]["UpdatedByName"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    Type = Convert.ToInt32(dt.Rows[0]["Type"].ToString());
                    FromCurrencyId = Convert.ToInt32(dt.Rows[0]["FromCurrencyId"].ToString());
                    FromCurrencyCode = dt.Rows[0]["FromCurrencyCode"].ToString();
                    CompanyRegistrationNo = dt.Rows[0]["CompanyRegistrationNo"].ToString();
                    DateOfBirth = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString());
                    GenderType = Convert.ToInt32(dt.Rows[0]["Gender"].ToString());
                    ProofType =Convert.ToInt32(dt.Rows[0]["ProofType"]);
                    DocumentNo = dt.Rows[0]["DocumentNo"].ToString();
                    DocumentExpiryDate = Convert.ToDateTime(dt.Rows[0]["DocumentExpiryDate"].ToString());
                    AddressProofType = dt.Rows[0]["AddressProofType"].ToString();
                    AddressProofImage = dt.Rows[0]["AddressProofImage"].ToString();
                    OtherProofType = dt.Rows[0]["OtherProofType"].ToString();
                    IDProofFrontImage = dt.Rows[0]["IDProofFrontImage"].ToString();
                    AdditionalDoc = dt.Rows[0]["AdditionalDoc"].ToString();
                    IDProofBackImage = dt.Rows[0]["IDProofBackImage"].ToString();
                    CompanyDirectorname = dt.Rows[0]["CompanyDirectorname"].ToString();
                    FeeType = Convert.ToInt32(dt.Rows[0]["FeeType"].ToString());
                    Fee = Convert.ToDecimal(dt.Rows[0]["Fee"].ToString());
                    FeeAccountBalance = Convert.ToDecimal(dt.Rows[0]["FeeAccountBalance"].ToString());
                    MarginConversionRate = Convert.ToDecimal(dt.Rows[0]["MarginConversionRate"].ToString());
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public System.Data.DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactName", ContactName);
                HT.Add("EmailID", EmailID);
                HT.Add("ContactNo", ContactNo);
                HT.Add("UserName", UserName);
                HT.Add("Password", Password);
                HT.Add("IsPasswordReset", IsPasswordReset);
                HT.Add("apikey", apikey);
                HT.Add("secretkey", secretkey);
                HT.Add("Image", Image);
                HT.Add("Address", Address);
                HT.Add("City", City);
                HT.Add("State", State);
                HT.Add("CountryId", CountryId);
                HT.Add("CountryName", CountryName);
                HT.Add("ZipCode", ZipCode);
                HT.Add("SuccessURL", SuccessURL);
                HT.Add("CancelURL", CancelURL);
                HT.Add("IpAddress", IpAddress);
                HT.Add("WebsiteURL", WebsiteURL);
                HT.Add("RoleId", RoleId);
                HT.Add("RoleName", RoleName);
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantIpAddress", MerchantIpAddress);
                HT.Add("PublicKey", PublicKey);
                HT.Add("PrivateKey", PrivateKey);
                HT.Add("API_User", API_User);
                HT.Add("API_Password", API_Password);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);
                HT.Add("Type", Type);

                dt = obj.GetDataFromStoredProcedure_Remittance("sp_RemittanceUser_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure_Remittance("sp_RemittanceUser_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }
        #endregion

    }


}