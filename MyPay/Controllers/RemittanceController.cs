using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.RemittanceAPI.Add;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MyPay.Controllers
{
    public class RemittanceController : BaseAdminSessionController
    {
        [Authorize]
        [HttpGet]
        public ActionResult Index(string MerchantUniqueId)
        {
            AddRemittanceUser model = new AddRemittanceUser();
            if (MerchantUniqueId != null && MerchantUniqueId != "0")
            {
                AddRemittanceUser outobject = new AddRemittanceUser();
                outobject.MerchantUniqueId = MerchantUniqueId;
                if (outobject.GetRecord())
                {
                    model = outobject;
                }
                ViewBag.Password = MyPay.Models.Common.Common.DecryptionFromKey(model.Password, model.secretkey);
            }
            //List<SelectListItem> MerchantType = CommonHelpers.GetSelectList_MerchantType(model);
            //ViewBag.MerchantType = MerchantType;

            List<SelectListItem> countrylist = new List<SelectListItem>();
            countrylist = CommonHelpers.GetSelectList_Country(Convert.ToInt32(model.CountryId));
            ViewBag.CountryId = countrylist;
            List<SelectListItem> Type = CommonHelpers.GetSelectList_RemittanceType(model);
            ViewBag.Type = Type;

            List<SelectListItem> currencylist = new List<SelectListItem>();
            currencylist = CommonHelpers.GetSelectList_RemittanceSourceCurrency(Convert.ToInt32(model.FromCurrencyId));
            ViewBag.FromCurrencyId = currencylist;

            List<SelectListItem> prooflist = new List<SelectListItem>();
            prooflist = CommonHelpers.GetSelectList_RemittanceProofType(model);

            ViewBag.ProofType = prooflist;
            List<SelectListItem> Addressprooflist = new List<SelectListItem>();
            Addressprooflist = CommonHelpers.GetSelectList_RemittanceAddressProofType(model);
            ViewBag.AddressProofType = Addressprooflist;
            List<SelectListItem> GenderType = CommonHelpers.GetSelectList_RemittanceGenderType(model);
            ViewBag.GenderType = GenderType;

            List<SelectListItem> FeeType = CommonHelpers.GetSelectList_RemittanceFeeType(model);
            ViewBag.FeeType = FeeType;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(AddRemittanceUser model, HttpPostedFileBase MerchantImageFile, HttpPostedFileBase AddressProofImageFile, HttpPostedFileBase IDProofFrontImageFile, HttpPostedFileBase IDProofBackImageFile, HttpPostedFileBase AdditionalDocFile)
        {
            String strPathAndQuery = HttpContext.Request.Url.PathAndQuery;
            String strUrl = HttpContext.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            if (string.IsNullOrEmpty(model.ContactName))
            {
                ViewBag.Message = "Please enter Contact Name";
            }
            else if (!string.IsNullOrEmpty(model.ContactName) && !Regex.Match(model.ContactName, @"^[a-zA-Z ]+$").Success)
            {
                ViewBag.Message = "Contact name allow only alphabets";
            }
            else if (string.IsNullOrEmpty(model.ContactNo))
            {
                ViewBag.Message = "Please enter contact number";
            }
            else if (!string.IsNullOrEmpty(model.ContactNo) && !Regex.Match(model.ContactNo, @"^[0-9]+$").Success)
            {
                ViewBag.Message = "Please enter valid contact number";
            }
            else if ((Convert.ToInt64(model.ContactNo) == 0) || (model.ContactNo.Length > 10))
            {
                ViewBag.Message = "Please enter valid contact number";
            }
            else if (string.IsNullOrEmpty(model.EmailID))
            {
                ViewBag.Message = "Please enter email";
            }
            else if (model.CountryId == 0)
            {
                ViewBag.Message = "Please select country";
            }
            else if (string.IsNullOrEmpty(model.Address))
            {
                ViewBag.Message = "Please enter Address";
            }
            else if (string.IsNullOrEmpty(model.UserName))
            {
                ViewBag.Message = "Please enter UserName.";
            }
            else if (model.UserName.IndexOf(":") >= 0)
            {
                ViewBag.Message = "Username not allowed Colon (:)";
            }
            else if (string.IsNullOrEmpty(model.State))
            {
                ViewBag.Message = "Please enter state name";
            }
            else if (string.IsNullOrEmpty(model.City))
            {
                ViewBag.Message = "Please enter city name";
            }
            else if (Convert.ToInt64(model.GenderType) == 0)
            {
                ViewBag.Message = "Please select  Gender";
            }
            else if (string.IsNullOrEmpty(model.OrganizationName))
            {
                ViewBag.Message = "Please enter organization Name";
            }
            else if (string.IsNullOrEmpty(model.CompanyDirectorname))
            {
                ViewBag.Message = "Please enter Company Directorname name";
            }
            else if (!string.IsNullOrEmpty(model.CompanyDirectorname) && !Regex.Match(model.CompanyDirectorname, @"^[a-zA-Z ]+$").Success)
            {
                ViewBag.Message = "Company Director name allow only alphabets";
            }
            else if (string.IsNullOrEmpty(model.CompanyRegistrationNo))
            {
                ViewBag.Message = "Please enter Company Registration No";
            }
            if (model.FromCurrencyId == 0)
            {
                ViewBag.Message = "Please select From Currency.";
            }
            else if (string.IsNullOrEmpty(model.WebsiteURL))
            {
                ViewBag.Message = "Please enter Website";
            }
            else if (string.IsNullOrEmpty(model.MerchantIpAddress))
            {
                ViewBag.Message = "Please enter IpAddress";
            }

            else if (Convert.ToInt64(model.ProofType) == 0)
            {
                ViewBag.Message = "Please select ProofType";
            }
            else if (string.IsNullOrEmpty(model.DocumentNo))
            {
                ViewBag.Message = "Please enter DocumentNo";
            }

            else if (string.IsNullOrEmpty(model.AddressProofType))
            {
                ViewBag.Message = "Please select Address Proof Type";
            }
            else if ((MerchantImageFile == null) && (string.IsNullOrEmpty(model.Image)))
            {
                ViewBag.Message = "Please select Company Logo Image";
            }
            else if ((AddressProofImageFile == null) && (string.IsNullOrEmpty(model.AddressProofImage)))
            {
                ViewBag.Message = "Please Select Company License  Doc";
            }
            else if ((IDProofFrontImageFile == null) && (string.IsNullOrEmpty(model.IDProofFrontImage)))
            {
                ViewBag.Message = "Please Select ID Proof Front Image";
            }

            else if ((AdditionalDocFile == null) && (string.IsNullOrEmpty(model.AdditionalDoc)))
            {
                ViewBag.Message = "Please Select Doc Image";
            }

            else if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Message = "Please enter password";
            }
            else if (model.Password.Length < 8)
            {
                ViewBag.Message = "Minimum Password length should be 8 characters.";
            }
            else if (model.Password != "")
            {
                if (model.Password.IndexOf(":") >= 0)
                {
                    ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                }
                else
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(model.Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                    }
                }
            }

            if (model.ProofType != (int)AddRemittanceUser.ProofTypes.Driving_Licence || model.ProofType != (int)AddRemittanceUser.ProofTypes.Voter_Id)
            {
                if ((IDProofBackImageFile == null) && (string.IsNullOrEmpty(model.IDProofBackImage)))
                {
                    ViewBag.Message = "Please Select ID Proof Back Image";
                }
            }

            if (!string.IsNullOrEmpty(model.EmailID) && !Regex.Match(model.EmailID, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                ViewBag.Message = "Please enter valid email";
            }
            if (!string.IsNullOrEmpty(model.SuccessURL))
            {
                if (!model.SuccessURL.StartsWith("http://") && !model.SuccessURL.StartsWith("https://"))
                {
                    ViewBag.Message = "Please enter a valid SuccessURL.";
                }
            }
            if (!string.IsNullOrEmpty(model.CancelURL))
            {
                if (!model.CancelURL.StartsWith("http://") && !model.CancelURL.StartsWith("https://"))
                {
                    ViewBag.Message = "Please enter a valid CancelURL.";
                }
            }

            if (!string.IsNullOrEmpty(model.WebsiteURL))
            {
                if (!model.WebsiteURL.StartsWith("http://") && !model.WebsiteURL.StartsWith("https://"))
                {
                    ViewBag.Message = "Please enter a valid WebsiteURL.";
                }
            }
            if (model.FeeType == (int)AddRemittanceUser.RemittanceFeeType.Fixed)
            {
                if (string.IsNullOrEmpty(model.Fee.ToString()))
                {
                    ViewBag.Message = "Please enter Fee.";
                }
            }
            if (string.IsNullOrEmpty(model.MarginConversionRate.ToString()))
            {
                ViewBag.Message = "Please enter Margin Conversion Rate.";
            }
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                model.EmailID = model.EmailID.Replace(" ", "");
                AddRemittanceUser res = new AddRemittanceUser();
                if (model.MerchantUniqueId != "")
                {
                    res.MerchantUniqueId = model.MerchantUniqueId;
                    res.GetRecord();
                    if (res != null && res.Id != 0)
                    {
                        if (res.EmailID != model.EmailID)
                        {
                            AddRemittanceUser outobjectemail = new AddRemittanceUser();
                            outobjectemail.EmailID = model.EmailID;
                            outobjectemail.GetRecord();
                            if (outobjectemail != null && outobjectemail.Id != 0)
                            {
                                ViewBag.Message = "EmailId already exists";
                            }
                        }
                        if (res.OrganizationName != model.OrganizationName)
                        {
                            AddRemittanceUser outobjectorg = new AddRemittanceUser();
                            outobjectorg.OrganizationName = model.OrganizationName;
                            outobjectorg.GetRecord();
                            if (outobjectorg != null && outobjectorg.Id != 0)
                            {
                                ViewBag.Message = "Organization Name already exists";
                            }
                        }
                        if (res.ContactNo != model.ContactNo)
                        {
                            AddRemittanceUser outobjectmobile = new AddRemittanceUser();
                            outobjectmobile.ContactNo = model.ContactNo;
                            outobjectmobile.GetRecord();
                            if (outobjectmobile != null && outobjectmobile.Id != 0)
                            {
                                ViewBag.Message = "Mobile Number already exists";
                            }
                        }
                        if (string.IsNullOrEmpty(ViewBag.Message))
                        {
                            var RemittanceIp = res.MerchantIpAddress;
                            res.Password = Common.EncryptionFromKey(model.Password, res.secretkey);
                            res.ContactName = model.ContactName;
                            res.EmailID = model.EmailID;
                            res.City = model.City;
                            res.State = model.State;
                            res.Address = model.Address;
                            res.ZipCode = model.ZipCode;
                            res.OrganizationName = model.OrganizationName;
                            res.CountryId = model.CountryId;
                            res.CountryName = model.CountryName;
                            res.SuccessURL = model.SuccessURL;
                            res.CancelURL = model.CancelURL;
                            res.WebsiteURL = model.WebsiteURL;
                            res.MerchantIpAddress = model.MerchantIpAddress;
                            res.FromCurrencyId = model.FromCurrencyId;
                            res.FromCurrencyCode = model.FromCurrencyCode;
                            res.CompanyRegistrationNo = model.CompanyRegistrationNo;
                            res.DateOfBirth = model.DateOfBirth;
                            res.GenderType = model.GenderType;
                            res.ProofType = model.ProofType;
                            res.DocumentNo = model.DocumentNo;
                            res.DocumentExpiryDate = model.DocumentExpiryDate;
                            res.AddressProofType = model.AddressProofType;
                            res.OtherProofType = model.OtherProofType;
                            res.AdditionalDoc = model.AdditionalDoc;
                            res.CompanyDirectorname = model.CompanyDirectorname;
                            res.FeeType = model.FeeType;
                            res.Fee = model.Fee;
                            res.MarginConversionRate = model.MarginConversionRate;
                            if (MerchantImageFile != null)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(MerchantImageFile.FileName);
                                string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                                MerchantImageFile.SaveAs(filePath);
                                string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                                res.Image = savefilePath;

                            }
                            if (AddressProofImageFile != null)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(AddressProofImageFile.FileName);
                                string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                                AddressProofImageFile.SaveAs(filePath);
                                string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                                res.AddressProofImage = savefilePath;
                            }
                            if (IDProofFrontImageFile != null)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(IDProofFrontImageFile.FileName);
                                string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                                IDProofFrontImageFile.SaveAs(filePath);
                                string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                                res.IDProofFrontImage = savefilePath;
                            }
                            if (IDProofBackImageFile != null)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(IDProofBackImageFile.FileName);
                                string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                                IDProofBackImageFile.SaveAs(filePath);
                                string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                                res.IDProofBackImage = savefilePath;
                            }
                            if (AdditionalDocFile != null)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(AdditionalDocFile.FileName);
                                string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                                AdditionalDocFile.SaveAs(filePath);
                                string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                                res.AdditionalDoc = savefilePath;
                            }
                            if (Session["AdminMemberId"] != null)
                            {
                                res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                res.UpdatedByName = Session["AdminUserName"].ToString();
                                res.UpdatedDate = DateTime.UtcNow;
                                bool status = res.Update();
                                if (status)
                                {
                                    if (res.MerchantIpAddress != RemittanceIp)
                                    {
                                        AddRemittanceIPAddress RemittanceiPAddress = new AddRemittanceIPAddress();
                                        RemittanceiPAddress.RemittanceMemberId = res.MerchantMemberId;
                                        RemittanceiPAddress.RemittanceUniqueId = res.MerchantUniqueId;
                                        RemittanceiPAddress.RemittanceName = res.ContactName;
                                        RemittanceiPAddress.RemittanceOrganization = res.OrganizationName;
                                        RemittanceiPAddress.IPAddress = res.MerchantIpAddress;
                                        RemittanceiPAddress.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                        RemittanceiPAddress.CreatedByName = Session["AdminUserName"].ToString();
                                        RemittanceiPAddress.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                        RemittanceiPAddress.UpdatedByName = Session["AdminUserName"].ToString();
                                        RemittanceiPAddress.IsActive = true;
                                        if (RemittanceiPAddress.Add())
                                        {
                                            Common.AddLogs("update  RemittanceIPAddress for Remittance (" + res.Id + ")  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);

                                        }
                                        else
                                        {
                                            Common.AddLogs("Failed to update RemittanceIPAddress ", true, (int)AddLog.LogType.Remittance);

                                        }
                                    }

                                    ViewBag.SuccessMessage = "Successfully Updated RemittanceUser.";
                                    Common.AddLogs("Updated RemittanceUser Detail of (MerchantUniqueId:" + res.MerchantUniqueId + "  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);


                                }
                                else
                                {
                                    ViewBag.Message = "Not Updated.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    res.UserName = model.UserName;
                    res.GetRecord();
                    if (res != null && res.Id != 0)
                    {
                        ViewBag.Message = "Username already exists";
                    }
                    else if (model.UserName.IndexOf(":") >= 0)
                    {
                        ViewBag.Message = "Username not allowed Colon (:)";
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddRemittanceUser outobjectemail = new AddRemittanceUser();
                        outobjectemail.EmailID = model.EmailID;
                        outobjectemail.GetRecord();
                        if (outobjectemail != null && outobjectemail.Id != 0)
                        {
                            ViewBag.Message = "EmailId already exists";
                        }
                    }

                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddRemittanceUser outobjectorg = new AddRemittanceUser();
                        outobjectorg.OrganizationName = model.OrganizationName;
                        outobjectorg.GetRecord();
                        if (outobjectorg != null && outobjectorg.Id != 0)
                        {
                            ViewBag.Message = "Organization Name already exists";
                        }
                    }

                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddRemittanceUser outobjectmobile = new AddRemittanceUser();
                        outobjectmobile.ContactNo = model.ContactNo;
                        outobjectmobile.GetRecord();
                        if (outobjectmobile != null && outobjectmobile.Id != 0)
                        {
                            ViewBag.Message = "RemittanceUser Mobile Number already exists";
                        }
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddRemittanceUser outobject = new AddRemittanceUser();
                        string Secretkey = Common.RandomString(16);
                        string RandomUserId = MyPay.Models.Common.Common.RandomNumber(11111111, 99999999).ToString();
                        outobject.MerchantUniqueId = "REM" + RandomUserId;
                        outobject.secretkey = Secretkey;
                        outobject.IsActive = true;
                        outobject.IsApprovedByAdmin = true;
                        outobject.Password = Common.EncryptionFromKey(model.Password, outobject.secretkey);
                        outobject.apikey = Common.EncryptionFromKey(model.UserName + ":" + model.Password + ":" + outobject.MerchantUniqueId, outobject.secretkey);
                        outobject.ContactName = model.ContactName;
                        outobject.ContactNo = model.ContactNo;
                        outobject.Type = model.Type;
                        outobject.EmailID = model.EmailID;
                        outobject.City = model.City;
                        outobject.State = model.State;
                        outobject.Address = model.Address;
                        outobject.UserName = model.UserName;
                        outobject.Address = model.Address;
                        outobject.ZipCode = model.ZipCode;
                        outobject.OrganizationName = model.OrganizationName;
                        outobject.CountryId = model.CountryId;
                        outobject.CountryName = model.CountryName;
                        outobject.IsPasswordReset = false;
                        outobject.SuccessURL = model.SuccessURL;
                        outobject.CancelURL = model.CancelURL;
                        outobject.WebsiteURL = model.WebsiteURL;
                        outobject.FromCurrencyId = model.FromCurrencyId;
                        outobject.FromCurrencyCode = model.FromCurrencyCode;
                        outobject.CompanyRegistrationNo = model.CompanyRegistrationNo;
                        outobject.DateOfBirth = model.DateOfBirth;
                        outobject.GenderType = model.GenderType;
                        outobject.ProofType = model.ProofType;
                        outobject.DocumentNo = model.DocumentNo;
                        outobject.DocumentExpiryDate = model.DocumentExpiryDate;
                        outobject.AddressProofType = model.AddressProofType;
                        outobject.AddressProofImage = model.AddressProofImage;
                        outobject.OtherProofType = model.OtherProofType;
                        outobject.IDProofFrontImage = model.IDProofFrontImage;
                        outobject.IDProofBackImage = model.IDProofBackImage;
                        outobject.AdditionalDoc = model.AdditionalDoc;
                        outobject.CompanyDirectorname = model.CompanyDirectorname;

                        outobject.RoleId = 9;
                        outobject.RoleName = "Remittance";
                        outobject.MerchantIpAddress = model.MerchantIpAddress;
                        outobject.MerchantMemberId = RepRemittance.GetNewRemittanceId();
                        List<string> PublicKeyPair = RepRemittance.GenerateKeyPair_Merchant();
                        outobject.PrivateKey = PublicKeyPair[0].Replace("  ", "\r\n");
                        outobject.PublicKey = PublicKeyPair[1].Replace("ssh-rsa", "").Replace("generated-key", "");
                        outobject.API_User = outobject.UserName;
                        outobject.API_Password = Common.EncryptionFromKey(Common.RandomString(15), outobject.secretkey);
                        outobject.FeeType = model.FeeType;
                        outobject.Fee = model.Fee;
                        outobject.MarginConversionRate = model.MarginConversionRate;
                        if (MerchantImageFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(MerchantImageFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                            MerchantImageFile.SaveAs(filePath);
                            string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                            outobject.Image = savefilePath;
                        }
                        if (AddressProofImageFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(AddressProofImageFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                            AddressProofImageFile.SaveAs(filePath);
                            string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                            outobject.AddressProofImage = savefilePath;
                        }
                        if (IDProofFrontImageFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(IDProofFrontImageFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                            IDProofFrontImageFile.SaveAs(filePath);
                            string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                            outobject.IDProofFrontImage = savefilePath;
                        }
                        if (IDProofBackImageFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(IDProofBackImageFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                            IDProofBackImageFile.SaveAs(filePath);
                            string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                            outobject.IDProofBackImage = savefilePath;
                        }
                        if (AdditionalDocFile != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(AdditionalDocFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/MerchantImages/") + fileName);
                            AdditionalDocFile.SaveAs(filePath);
                            string savefilePath = strUrl + "Images/MerchantImages/" + fileName;
                            outobject.AdditionalDoc = savefilePath;
                        }
                        if (Session["AdminMemberId"] != null)
                        {
                            outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            outobject.CreatedByName = Session["AdminUserName"].ToString();
                            bool IsAdded = outobject.Add();
                            if (IsAdded && outobject.Id > 0)
                            {
                                res = outobject;
                                AddRemittanceUserCurrencies outobj = new AddRemittanceUserCurrencies();
                                outobj.MerchantUniqueId = outobject.MerchantUniqueId;
                                outobj.ContactName = outobject.ContactName;
                                outobj.ContactNo = outobject.ContactNo;
                                outobj.CurrencyID = outobject.FromCurrencyId;
                                outobj.EmailID = outobject.EmailID;
                                outobj.OrganizationName = outobject.OrganizationName;
                                outobj.UserName = outobject.UserName;
                                outobj.IsActive = true;
                                outobj.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                outobj.CreatedByName = Session["AdminUserName"].ToString();
                                outobj.Type = Convert.ToInt32(outobject.Type);
                                outobj.CurrencySymbol = outobject.FromCurrencyCode;
                                outobj.Add();
                                outobj.CurrencyID = 21;
                                outobj.CurrencySymbol = "NPR";
                                outobj.Add();

                                AddRemittanceIPAddress RemittanceiPAddress = new AddRemittanceIPAddress();
                                RemittanceiPAddress.RemittanceMemberId = outobject.MerchantMemberId;
                                RemittanceiPAddress.RemittanceUniqueId = outobject.MerchantUniqueId;
                                RemittanceiPAddress.RemittanceName = outobject.ContactName;
                                RemittanceiPAddress.RemittanceOrganization = outobject.OrganizationName;
                                RemittanceiPAddress.IPAddress = outobject.MerchantIpAddress;
                                RemittanceiPAddress.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                RemittanceiPAddress.CreatedByName = Session["AdminUserName"].ToString();
                                RemittanceiPAddress.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                RemittanceiPAddress.UpdatedByName = Session["AdminUserName"].ToString();
                                RemittanceiPAddress.IsActive = true;
                                if (RemittanceiPAddress.Add())
                                {
                                    Common.AddLogs("Add RemittanceIPAddress for Remittance (" + res.Id + ")  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);

                                }
                                else
                                {
                                    Common.AddLogs("Failed to Add RemittanceIPAddress ", true, (int)AddLog.LogType.Remittance);

                                }

                                ViewBag.SuccessMessage = "Successfully Added Remittance User.";
                                Common.AddLogs("Added Remittance User Detail by(AdminMemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);

                            }
                            else
                            {
                                ViewBag.Message = "Remittance User Not Added.";
                            }
                        }
                    }
                }

                model = res;
            }

            //List<SelectListItem> MerchantType = CommonHelpers.GetSelectList_MerchantType(model);
            //ViewBag.MerchantType = MerchantType;
            List<SelectListItem> currencylist = new List<SelectListItem>();
            currencylist = CommonHelpers.GetSelectList_RemittanceSourceCurrency(Convert.ToInt32(model.FromCurrencyId));
            ViewBag.FromCurrencyId = currencylist;

            List<SelectListItem> prooflist = new List<SelectListItem>();
            prooflist = CommonHelpers.GetSelectList_RemittanceProofType(model);

            ViewBag.ProofType = prooflist;
            List<SelectListItem> Addressprooflist = new List<SelectListItem>();
            Addressprooflist = CommonHelpers.GetSelectList_RemittanceAddressProofType(model);
            ViewBag.AddressProofType = Addressprooflist;
            List<SelectListItem> GenderType = CommonHelpers.GetSelectList_RemittanceGenderType(model);
            ViewBag.GenderType = GenderType;

            List<SelectListItem> FeeType = CommonHelpers.GetSelectList_RemittanceFeeType(model);
            ViewBag.FeeType = FeeType;

            List<SelectListItem> countrylist = new List<SelectListItem>();
            countrylist = CommonHelpers.GetSelectList_Country(Convert.ToInt32(model.CountryId));
            ViewBag.CountryId = countrylist;
            List<SelectListItem> Type = CommonHelpers.GetSelectList_RemittanceType(model);
            ViewBag.Type = Type;
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemittanceList()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetMerchantLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("FullName");
            columns.Add("UserName");
            columns.Add("Contact");
            columns.Add("Email");
            columns.Add("UniqueId");
            columns.Add("Status");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MerchantUniqueId = context.Request.Form["MerchantUniqueId"];
            string ContactNumber = context.Request.Form["ContactNo"];
            string Name = context.Request.Form["Name"];
            string Email = context.Request.Form["Email"];
            string UserName = context.Request.Form["UserName"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceUser> trans = new List<AddRemittanceUser>();

            AddRemittanceUser w = new AddRemittanceUser();
            w.MerchantUniqueId = MerchantUniqueId;
            w.ContactNo = ContactNumber;
            w.ContactName = Name;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.EmailID = Email;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceUser
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantUniqueId = row["MerchantUniqueId"].ToString(),
                         ContactName = row["ContactName"].ToString(),
                         EmailID = row["EmailID"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["IndiaDate"].ToString(),
                         ContactNo = row["ContactNo"].ToString(),
                         UserName = row["UserName"].ToString(),
                         TotalUserCount = dt.Rows[0]["FilterTotalCount"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         OrganizationName = row["OrganizationName"].ToString(),
                         apikey = row["apikey"].ToString(),
                         API_Password = Common.DecryptionFromKey(row["API_Password"].ToString(), row["secretkey"].ToString()),
                         MerchantIpAddress = row["MerchantIpAddress"].ToString(),
                         FeeAccountBalance = Convert.ToDecimal(row["FeeAccountBalance"]),
                         MarginConversionRate = Convert.ToDecimal(row["MarginConversionRate"]),
                         FeeTypeName = @Enum.GetName(typeof(AddRemittanceUser.RemittanceFeeType), Convert.ToInt32(row["FeeType"])),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceUser>> objDataTableResponse = new DataTableResponse<List<AddRemittanceUser>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public JsonResult MerchantBlockUnblock(AddRemittanceUser model)
        {
            AddRemittanceUser res = new AddRemittanceUser();
            res.MerchantUniqueId = model.MerchantUniqueId;
            bool resFlag = res.GetRecord();
            if (resFlag && res != null && res.Id != 0)
            {
                if (res.IsActive)
                {
                    res.IsActive = false;
                }
                else
                {
                    res.IsActive = true;
                }
                bool IsUpdated = res.Update();
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update RemittanceUser";
                    Common.AddLogs("Updated RemittanceUser of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);

                }
                else
                {
                    ViewBag.Message = "Not Updated RemittanceUser";
                    Common.AddLogs("Not Updated (MerchantID: " + res.MerchantUniqueId + " )", true, (int)AddLog.LogType.Remittance);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ResetMerchantKeys(AddRemittanceUser model)
        {
            AddRemittanceUser res = new AddRemittanceUser();
            res.MerchantUniqueId = model.MerchantUniqueId;
            bool resFlag = res.GetRecord();
            if (resFlag && res != null && res.Id != 0)
            {
                string Password = Common.DecryptionFromKey(res.Password, res.secretkey);
                string APIPassword = Common.DecryptionFromKey(res.API_Password, res.secretkey);
                res.secretkey = Common.RandomString(16);
                res.apikey = Common.EncryptionFromKey(res.UserName + ":" + Password + ":" + res.MerchantUniqueId, res.secretkey);
                res.Password = Common.EncryptionFromKey(Password, res.secretkey);
                res.API_Password = Common.EncryptionFromKey(APIPassword, res.secretkey);
                if (Session["AdminMemberId"] != null || !string.IsNullOrEmpty(Session["AdminMemberId"].ToString()))
                {
                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Session["AdminUserName"].ToString();
                }
                else if (Session["RemittanceUniqueId"] != null)
                {
                    res.UpdatedByName = Session["RemittanceUniqueId"].ToString();
                }
                res.UpdatedDate = DateTime.UtcNow;
                bool IsUpdated = res.Update();
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update RemittanceUser";
                    if (Session["AdminMemberId"] != null || !string.IsNullOrEmpty(Session["AdminMemberId"].ToString()))
                    {
                        Common.AddLogs("Updated RemittanceUser Keys of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                    else if (Session["RemittanceUniqueId"] != null)
                    {
                        Common.AddLogs("Updated RemittanceUser Keys of (MerchantID: " + res.MerchantUniqueId + " ) by (Merchant:" + Session["RemittanceUniqueId"] + ")", false, (int)AddLog.LogType.Remittance, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, res.MerchantMemberId, Session["RemittanceUniqueId"].ToString());
                    }
                }
                else
                {
                    ViewBag.Message = "Not Updated RemittanceUser";
                    Common.AddLogs("Not Updated RemittanceUser Keys", true, (int)AddLog.LogType.Remittance, 0, "", false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys);
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ResetMerchantAPIPassword(AddRemittanceUser model)
        {
            AddRemittanceUser res = new AddRemittanceUser();
            res.MerchantUniqueId = model.MerchantUniqueId;
            bool resFlag = res.GetRecord();
            if (resFlag && res != null && res.Id != 0)
            {
                //res.secretkey = Common.RandomString(16);
                res.API_Password = Common.EncryptionFromKey(Common.RandomString(15), res.secretkey);
                if (Session["AdminMemberId"] != null)
                {
                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Session["AdminUserName"].ToString();
                }
                else if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
                {
                    //res.UpdatedBy = Convert.ToInt64(Session["MerchantUniqueId"]);
                    res.UpdatedByName = Session["MerchantUniqueId"].ToString();
                }
                res.UpdatedDate = DateTime.UtcNow;
                bool IsUpdated = res.Update();
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update Merchant";
                    if (Session["AdminMemberId"] != null)
                    {
                        Common.AddLogs("Updated Merchant API Password of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                    else if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
                    {
                        Common.AddLogs("Updated Merchant API Password of (MerchantID: " + res.MerchantUniqueId + " ) by (Merchant:" + Session["MerchantUniqueId"] + ")", false, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, res.MerchantMemberId, Session["MerchantUniqueId"].ToString());
                    }
                }
                else
                {
                    ViewBag.Message = "Not Updated Merchant";
                    Common.AddLogs("Not Updated Merchant API Password", true, (int)AddLog.LogType.Merchant, 0, "", false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys);
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // GET: MerchantTransactions
        [HttpGet]
        [Authorize]
        public ActionResult MerchantTransactions(string MemberId, string TransactionId, string Reference, string ParentTransactionId, string GatewayTransactionId, string SubscriberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
            ViewBag.Reference = string.IsNullOrEmpty(Reference) ? "" : Reference;
            ViewBag.GatewayTransactionId = string.IsNullOrEmpty(GatewayTransactionId) ? "" : GatewayTransactionId;
            ViewBag.ParentTransactionId = string.IsNullOrEmpty(ParentTransactionId) ? "" : ParentTransactionId;
            ViewBag.SubscriberId = string.IsNullOrEmpty(SubscriberId) ? "" : SubscriberId;
            List<SelectListItem> items = new List<SelectListItem>();

            items = CommonHelpers.GetSelectList_MerchantTransactionStatus();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            ViewBag.Status = items;

            List<SelectListItem> sign = new List<SelectListItem>();
            sign.Add(new SelectListItem
            {
                Text = "Select Sign",
                Value = "0",
                Selected = true
            });
            sign.Add(new SelectListItem
            {
                Text = "Credit",
                Value = "1"
            });
            sign.Add(new SelectListItem
            {
                Text = "Debit",
                Value = "2"
            });
            ViewBag.Sign = sign;
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetMerchantTransactionsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("RequestId");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("Service");
            columns.Add("ServiceCharge");
            columns.Add("Cashback");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
            columns.Add("UpdateBy");
            columns.Add("AvailableBalance");
            columns.Add("PreviousBalance");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MemberId = context.Request.Form["MemberId"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string Sign = context.Request.Form["Sign"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            string Reference = context.Request.Form["Reference"];
            string CustomerId = context.Request.Form["CustomerId"];
            string Type = context.Request.Form["Type"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTransactionId;
            w.ParentTransactionId = ParentTransactionId;
            w.Reference = Reference;
            w.Sign = Convert.ToInt32(Sign);
            w.CustomerID = CustomerId;
            w.Type = (int)Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Remittance_Transactions;

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         ParentTransactionId = row["ParentTransactionId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         Type = Convert.ToInt32(row["Type"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         UpdateByName = row["UpdateByName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         CashBack = Convert.ToDecimal(row["CashBack"]),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         VendorType = Convert.ToInt32(row["VendorType"]),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])),
                         IpAddress = row["IpAddress"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         CustomerID = row["CustomerID"].ToString(),
                         SenderBankName = row["SenderBankName"].ToString(),
                         SenderAccountNo = row["SenderAccountNo"].ToString(),
                         RecieverContactNumber = row["RecieverContactNumber"].ToString(),
                         RecieverBankName = row["RecieverBankName"].ToString(),
                         RecieverAccountNo = row["RecieverAccountNo"].ToString(),
                         WalletTypeName = @Enum.GetName(typeof(WalletTransactions.WalletTypes), Convert.ToInt64(row["WalletType"])).ToUpper(),
                         MPCoinsDebit = row["WalletType"].ToString() != "4" ? 0 : Convert.ToDecimal(row["MPCoinsDebit"].ToString()),
                         TransactionAmount = Convert.ToDecimal(row["TransactionAmount"].ToString()),
                         RewardPoint = row["Type"].ToString() == "20" ? 0 : Convert.ToDecimal(row["RewardPoint"].ToString()),
                         RewardPointBalance = Convert.ToDecimal(row["RewardPointBalance"].ToString()) + Convert.ToDecimal(row["RewardPoint"].ToString()),
                         PreviousRewardPointBalance = Convert.ToDecimal(row["PreviousRewardPointBalance"].ToString()),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<WalletTransactions>> objDataTableResponse = new DataTableResponse<List<WalletTransactions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public JsonResult ShowPublicPrivateKeys(AddRemittanceUser model)
        {
            if (Session["AdminMemberId"] != null)
            {
                string privateKeyString = (new CommonHelpers()).GetRemittancePrivateKey(model.MerchantUniqueId).Replace("\\n", Environment.NewLine);

                string publicKeyString = (new CommonHelpers()).GetRemittancePublicKey(model.MerchantUniqueId).Replace("\\n", Environment.NewLine);

                return Json(privateKeyString + "(" + publicKeyString);
            }
            else
            {
                return Json("");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemittanceApiLogsList()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetRemittanceApiLogsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("FullName");
            columns.Add("UserName");
            columns.Add("Contact");
            columns.Add("Email");
            columns.Add("UniqueId");
            columns.Add("Status");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MerchantUniqueId = context.Request.Form["MemberId"];
            string ContactNumber = context.Request.Form["ContactNo"];
            string Name = context.Request.Form["Name"];
            string TransactionUniqueId = context.Request.Form["TransactionUniqueId"];
            string Res_Khalti_Id = context.Request.Form["Res_Khalti_Id"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittance_API_Requests> trans = new List<AddRemittance_API_Requests>();

            AddRemittance_API_Requests w = new AddRemittance_API_Requests();
            w.MerchantUniqueId = MerchantUniqueId;
            w.ContactNo = ContactNumber;
            w.MerchantName = Name;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.VendorTransactionId = Res_Khalti_Id;
            w.TransactionUniqueId = TransactionUniqueId;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittance_API_Requests
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantUniqueId = row["MerchantUniqueId"].ToString(),
                         MerchantName = row["MerchantName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["IndiaDate"].ToString(),
                         ContactNo = row["ContactNo"].ToString(),
                         //UserName = row["UserName"].ToString(),
                         TotalCount = dt.Rows[0]["FilterTotalCount"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         OrganizationName = row["OrganizationName"].ToString(),
                         IpAddress = row["IpAddress"].ToString(),
                         //RemittanceApiTypeName = @Enum.GetName(typeof(AddRemittance_API_Requests.Remittance_Api_Type), Convert.ToInt64(row["RemittanceApiType"])),
                         Status = Convert.ToInt32(row["Status"]),
                         StatusName = @Enum.GetName(typeof(AddRemittance_API_Requests.Statuses), Convert.ToInt64(row["Status"])),
                         RemittanceApiType = Convert.ToInt32(row["RemittanceApiType"]),
                         Req_ReferenceNo = row["Req_ReferenceNo"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         DeviceId = row["DeviceId"].ToString(),
                         PlatForm = row["PlatForm"].ToString(),
                         RemittanceApiTypeName = @Enum.GetName(typeof(AddRemittance_API_Requests.Remittance_Api_Type), Convert.ToInt64(row["RemittanceApiType"]))
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittance_API_Requests>> objDataTableResponse = new DataTableResponse<List<AddRemittance_API_Requests>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpGet]
        [Authorize]
        public ActionResult RemittanceSettlementReport()
        {
            List<SelectListItem> walletEnum = CommonHelpers.GetSelectList_RemittanceWalletType();
            ViewBag.WalletType = walletEnum;

            List<SelectListItem> signEnum = CommonHelpers.GetSelectList_RemittanceTxnSign();
            ViewBag.Sign = signEnum;

            List<SelectListItem> sourceCurrency = CommonHelpers.GetSelectList_RemittanceSourceCurrency();
            ViewBag.SourceCurrency = sourceCurrency;

            List<SelectListItem> destCurrency = CommonHelpers.GetSelectList_RemittanceDestinationCurrency();
            ViewBag.DestinationCurrency = destCurrency;

            return View();
        }



        [HttpGet]
        [Authorize]
        public ActionResult RemittanceFeeAccountTxnReport()
        {
            List<SelectListItem> walletEnum = CommonHelpers.GetSelectList_RemittanceWalletType();
            ViewBag.WalletType = walletEnum;

            List<SelectListItem> signEnum = CommonHelpers.GetSelectList_RemittanceTxnSign();
            ViewBag.Sign = signEnum;

            List<SelectListItem> sourceCurrency = CommonHelpers.GetSelectList_RemittanceSourceCurrency();
            ViewBag.SourceCurrency = sourceCurrency;

            List<SelectListItem> destCurrency = CommonHelpers.GetSelectList_RemittanceDestinationCurrency();
            ViewBag.DestinationCurrency = destCurrency;

            return View();
        }

        [Authorize]
        public JsonResult GetRemittanceSettlementReport()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("RequestId");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("Service");
            columns.Add("ServiceCharge");
            columns.Add("Cashback");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
            columns.Add("UpdateBy");
            columns.Add("AvailableBalance");
            columns.Add("PreviousBalance");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MemberId = context.Request.Form["MemberId"];
            string MerchantId = context.Request.Form["MerchantUniqueId"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string TransactionId = context.Request.Form["TransactionId"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string Sign = context.Request.Form["Sign"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string Reference = context.Request.Form["Reference"];
            string WalletType = context.Request.Form["WalletType"];
            string SourceCurrency = context.Request.Form["SourceCurrency"];
            string DestinationCurrency = context.Request.Form["DestinationCurrency"];
            string SourceCurrencyId = context.Request.Form["SourceCurrencyId"];
            string DestinationCurrencyId = context.Request.Form["DestinationCurrencyId"];
            string IsFeeAccountTransaction = context.Request.Form["IsFeeAccountTransaction"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceTransactions> trans = new List<AddRemittanceTransactions>();

            AddRemittanceTransactions w = new AddRemittanceTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MerchantMemberId = Convert.ToInt64(MemberId);
            }
            w.MerchantUniqueId = MerchantId;
            w.MerchantContactNumber = ContactNumber;
            w.MerchantName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.GatewayTransactionId = GatewayTransactionId;
            w.Reference = Reference;
            w.Sign = Convert.ToInt32(Sign);
            w.ParentTransactionId = ParentTransactionId;
            w.Type = Convert.ToInt32(WalletType);
            if (SourceCurrencyId == "0")
            {
                w.FromCurrency = "";
            }
            else
            {
                w.FromCurrency = SourceCurrency;
            }
            if (DestinationCurrencyId == "0")
            {
                w.ToCurrency = "";
            }
            else
            {
                w.ToCurrency = DestinationCurrency;
            }
            w.IsFeeAccountTransaction = Convert.ToInt32(IsFeeAccountTransaction);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceTransactions
                     {
                         Sno = row["Sno"].ToString(),
                         MerchantMemberId = Convert.ToInt64(row["MerchantMemberId"]),
                         MerchantName = row["MerchantName"].ToString(),
                         MerchantUniqueId = row["MerchantUniqueId"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         ParentTransactionId = row["ParentTransactionId"].ToString(),
                         GatewayTransactionId = row["GatewayTransactionId"].ToString(),
                         SignName = @Enum.GetName(typeof(AddRemittanceTransactions.Signs), Convert.ToInt64(row["Sign"])),
                         FromAmount = Convert.ToDecimal(row["FromAmount"]),
                         ToAmount = Convert.ToDecimal(row["ToAmount"]),
                         FromCurrency = row["FromCurrency"].ToString(),
                         ToCurrency = row["ToCurrency"].ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(AddRemittanceTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         MerchantContactNumber = row["MerchantContactNumber"].ToString(),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         IpAddress = row["IpAddress"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         MerchantAccountNo = row["MerchantAccountNo"].ToString(),
                         MerchantBankName = row["MerchantBankName"].ToString(),
                         MerchantBankCode = row["MerchantBankCode"].ToString(),
                         MerchantBranch = row["MerchantBranch"].ToString(),
                         MerchantBranchName = row["MerchantBranchName"].ToString(),
                         BeneficiaryAccountNo = row["BeneficiaryAccountNo"].ToString(),
                         BeneficiaryBankName = row["BeneficiaryBankName"].ToString(),
                         BeneficiaryBankCode = row["BeneficiaryBankCode"].ToString(),
                         BeneficiaryBranchName = row["BeneficiaryBranchName"].ToString(),
                         BeneficiaryContactNumber = row["BeneficiaryContactNumber"].ToString(),
                         BeneficiaryName = row["BeneficiaryName"].ToString(),
                         WalletType = Convert.ToInt32(row["WalletType"].ToString()),
                         ConversionRate = Convert.ToDecimal(row["ConversionRate"]),
                         ConvertedAmount = Convert.ToDecimal(row["ConvertedAmount"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         WalletTypeName = @Enum.GetName(typeof(AddRemittanceUser.RemittanceType), Convert.ToInt64(row["WalletType"])),
                         TypeName = @Enum.GetName(typeof(AddRemittanceTransactions.WalletTypes), Convert.ToInt64(row["Type"])),
                         NetAmount = Convert.ToDecimal(row["NetAmount"]),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         FeeType = Convert.ToInt32(row["FeeType"]),
                         FeeTypeName = @Enum.GetName(typeof(AddRemittanceUser.RemittanceFeeType), Convert.ToInt32(row["FeeType"]))
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;
            DataTableResponse<List<AddRemittanceTransactions>> objDataTableResponse = new DataTableResponse<List<AddRemittanceTransactions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }
        public string GetVendorApijsonDetails(string transactionid, string type)
        {
            string result = "";
            try
            {
                AddRemittance_API_Requests outobject = new AddRemittance_API_Requests();
                outobject.TransactionUniqueId = transactionid;
                if (outobject.GetRecord())
                {
                    if (type == "mypayreq")
                    {
                        result = outobject.Req_Input;
                    }
                    else if (type == "mypayres")
                    {
                        result = outobject.Res_Output;
                    }
                    else if (type == "vendorreq")
                    {
                        result = outobject.VendorInput;
                    }
                    else if (type == "vendorres")
                    {
                        result = outobject.VendorOutput;
                    }
                    else
                    {
                        result = "";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemittanceUserCurrency(string MerchantUniqueId)
        {
            if (!string.IsNullOrEmpty(MerchantUniqueId))
            {
                ViewBag.MerchantUniqueId = MerchantUniqueId;
                //AddRemittanceCurrencyList list = new AddRemittanceCurrencyList();
                //list.CheckDelete = 0;
                //list.CheckActive = 1;
                //DataTable dt = list.GetList();

                //List<AddRemittanceCurrencyList> currencylist = (List<AddRemittanceCurrencyList>)CommonEntityConverter.DataTableToList<AddRemittanceCurrencyList>(dt);
                //ViewBag.CurrencyList = currencylist;
                AddRemittanceDestinationCurrencyList list = new AddRemittanceDestinationCurrencyList();
                List<SelectListItem> currencylist = new List<SelectListItem>();
                currencylist = CommonHelpers.GetSelectList_RemittanceDestinationCurrency();
                ViewBag.CurrencyList = currencylist;

                AddRemittanceUser usr = new AddRemittanceUser();
                usr.MerchantUniqueId = MerchantUniqueId;
                if (usr.GetRecord())
                {
                    ViewBag.FromCurrency = usr.FromCurrencyCode;
                    ViewBag.MerchantUniqueId = usr.MerchantUniqueId;
                    ViewBag.Type = usr.Type;
                }
                else
                {
                    ViewBag.FromCurrency = "";
                    ViewBag.MerchantUniqueId = "";
                    ViewBag.Type = "";
                    //return RedirectToAction("RemittanceList");
                }

                List<SelectListItem> walletlist = new List<SelectListItem>();
                walletlist = CommonHelpers.GetSelectList_RemittanceWallet(MerchantUniqueId);
                ViewBag.WalletList = walletlist;
                List<SelectListItem> sourcelist = new List<SelectListItem>();
                sourcelist = CommonHelpers.GetSelectList_RemittanceSourceCurrency(usr.FromCurrencyId);
                ViewBag.SourceList = sourcelist;
            }
            else
            {
                ViewBag.MerchantUniqueId = "";
                return RedirectToAction("RemittanceList");
            }
            return View();
        }

        [Authorize]
        public JsonResult GetRemittanceUserCurrency()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("Remittance Id");
            columns.Add("ContactName");
            columns.Add("ContactNumber");
            columns.Add("Email");
            columns.Add("OrganizationName");
            columns.Add("UserName");
            columns.Add("CurrencyId");
            columns.Add("CreatedBy");
            columns.Add("UpdatedBy");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MerchantId = context.Request.Form["MerchantUniqueId"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceUserCurrencies> trans = new List<AddRemittanceUserCurrencies>();

            AddRemittanceUserCurrencies w = new AddRemittanceUserCurrencies();
            w.MerchantUniqueId = MerchantId;
            w.CheckActive = 1;
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceUserCurrencies
                     {
                         ContactName = row["ContactName"].ToString(),
                         MerchantUniqueId = row["MerchantUniqueId"].ToString(),
                         CurrencyName = row["CurrencyName"].ToString(),
                         Image = row["Image"].ToString(),
                         CurrencyID = Convert.ToInt64(row["CurrencyID"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         CountryName = row["CountryName"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         TypeName = @Enum.GetName(typeof(AddRemittanceUser.RemittanceType), Convert.ToInt64(row["Type"])),
                         ODL = Convert.ToDecimal(row["ODL"]),
                         Prefund = Convert.ToDecimal(row["Prefund"]),
                         Id = Convert.ToInt64(row["Id"]),
                         Type = Convert.ToInt32(row["Type"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;
            DataTableResponse<List<AddRemittanceUserCurrencies>> objDataTableResponse = new DataTableResponse<List<AddRemittanceUserCurrencies>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        public JsonResult AssignWalletCurrencies(string CurrencyId, string CurrencyName, string MerchantUniqueId, string Type)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(CurrencyId))
                {
                    result = "Please select Wallet Currency.";
                }
                else
                {
                    AddRemittanceUser outobject = new AddRemittanceUser();
                    outobject.MerchantUniqueId = MerchantUniqueId;
                    if (outobject.GetRecord())
                    {
                        AddRemittanceUserCurrencies checkobj = new AddRemittanceUserCurrencies();
                        checkobj.MerchantUniqueId = MerchantUniqueId;
                        checkobj.CurrencyID = Convert.ToInt64(CurrencyId);
                        //checkobj.CheckActive = 1;
                        //checkobj.CheckDelete = 0;
                        if (checkobj.GetRecord())
                        {
                            if (checkobj.IsActive && !checkobj.IsDeleted)
                            {
                                result = "Selected Wallet Currency Already Assigned.";
                            }
                            if (checkobj.IsDeleted)
                            {
                                checkobj.IsDeleted = false;
                                checkobj.IsActive = true;
                                checkobj.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                checkobj.UpdatedByName = Session["AdminUserName"].ToString();
                                if (checkobj.Update())
                                {
                                    Common.AddLogs("Update Assign Currency(" + CurrencyId + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                    result = "success";
                                }
                                else
                                {
                                    Common.AddLogs("Not update Assign selected currency(" + CurrencyId + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                    result = "Not assign selected currency(" + CurrencyId + ")";
                                }
                            }
                        }
                        else
                        {
                            AddRemittanceUserCurrencies outobj = new AddRemittanceUserCurrencies();
                            outobj.MerchantUniqueId = MerchantUniqueId;
                            outobj.ContactName = outobject.ContactName;
                            outobj.ContactNo = outobject.ContactNo;
                            outobj.CurrencyID = Convert.ToInt64(CurrencyId);
                            outobj.EmailID = outobject.EmailID;
                            outobj.OrganizationName = outobject.OrganizationName;
                            outobj.UserName = outobject.UserName;
                            outobj.IsActive = true;
                            outobj.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            outobj.CreatedByName = Session["AdminUserName"].ToString();
                            outobj.Type = Convert.ToInt32(outobject.Type);
                            outobj.CurrencySymbol = CurrencyName;
                            if (outobj.Add())
                            {
                                ViewBag.SuccessMessage = "Successfully assign selected currency.";
                                Common.AddLogs("Assign Currency(" + CurrencyId + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "success";
                            }
                            else
                            {
                                ViewBag.Message = "Not assign selected currency.";
                                Common.AddLogs("Not Assign selected currency(" + CurrencyId + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "Not assign selected currency(" + CurrencyId + ")";
                            }
                        }
                    }
                    else
                    {
                        result = "Merchant Id not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [Authorize]
        public JsonResult DeleteWalletCurrencies(string Id)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    result = "Please select Currency to Delete.";
                }
                else
                {
                    AddRemittanceUserCurrencies checkobj = new AddRemittanceUserCurrencies();
                    checkobj.Id = Convert.ToInt64(Id);
                    checkobj.CheckActive = 1;
                    checkobj.CheckDelete = 0;
                    if (checkobj.GetRecord())
                    {
                        if (checkobj.ODL == 0 && checkobj.Prefund == 0)
                        {
                            checkobj.IsDeleted = true;
                            checkobj.IsActive = false;
                            checkobj.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            checkobj.UpdatedByName = Session["AdminUserName"].ToString();
                            if (checkobj.Update())
                            {
                                Common.AddLogs("Delete Wallet Currency(" + Id + ") from MerchantId :" + checkobj.MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "success";
                            }
                            else
                            {
                                Common.AddLogs("Failed to delete wallet currency", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "Failed to delete wallet currency";
                            }
                        }
                        else
                        {
                            result = "Your wallet has balance, So You cannot delete this Wallet.";
                        }
                    }
                    else
                    {
                        result = "Record not found";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [Authorize]
        //Disable for Now Select Multiple Currencies
        public string AssignCurrencies(string SelectedCurrencies, string MerchantUniqueId)
        {
            string result = "";
            try
            {
                AddRemittanceUser outobject = new AddRemittanceUser();
                outobject.MerchantUniqueId = MerchantUniqueId;
                if (outobject.GetRecord())
                {
                    if (SelectedCurrencies != "")
                    {
                        string[] splitvalues = SelectedCurrencies.Split(',');
                        if (splitvalues != null)
                        {
                            //int count = 0;
                            foreach (var item in splitvalues)
                            {
                                if (item != "")
                                {
                                    AddRemittanceUserCurrencies outobj = new AddRemittanceUserCurrencies();

                                    outobj.MerchantUniqueId = MerchantUniqueId;
                                    outobj.ContactName = outobject.ContactName;
                                    outobj.ContactNo = outobject.ContactNo;
                                    outobj.CurrencyID = Convert.ToInt64(item);
                                    outobj.EmailID = outobject.EmailID;
                                    outobj.OrganizationName = outobject.OrganizationName;
                                    outobj.UserName = outobject.UserName;
                                    outobj.IsActive = true;
                                    outobj.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    outobj.CreatedByName = Session["AdminUserName"].ToString();

                                    if (outobj.Add())
                                    {
                                        ViewBag.SuccessMessage = "Successfully assign selected currency.";
                                        Common.AddLogs("Assign Currency(" + item + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                        result = "success";
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Not assign selected currency.";
                                        Common.AddLogs("Not Assign selected currency(" + item + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                        result = "Not assign selected currency(" + item + ")";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [Authorize]
        public JsonResult AddWalletFund(string CurrencyId, string MerchantUniqueId, string Type, string Sign, string TxnId, string Remarks, string Amount, string CurrencyName, String ReceiptFileName)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(CurrencyId))
                {
                    result = "Please select Wallet Currency.";
                }
                else if (string.IsNullOrEmpty(Sign))
                {
                    result = "Please select Sign.";
                }
                else if (string.IsNullOrEmpty(TxnId) && Sign == "1")
                {
                    result = "Please enter Txn Id.";
                }
                else if (string.IsNullOrEmpty(Amount))
                {
                    result = "Please enter Amount.";
                }
                else if (!string.IsNullOrEmpty(Amount))
                {
                    if (Convert.ToDecimal(Amount) <= 0)
                    {
                        result = "Please enter valid Amount";
                    }
                }
                else if (Sign == "1")
                {
                    if (string.IsNullOrEmpty(ReceiptFileName))
                    {
                        ViewBag.Message = "Please upload Receipt for Credit Fund.";
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    AddRemittanceUser usr = new AddRemittanceUser();
                    usr.MerchantUniqueId = MerchantUniqueId;
                    if (usr.GetRecord())
                    {
                        if (Sign == "1")
                        {
                            AddRemittanceTransactions checktxn = new AddRemittanceTransactions();
                            checktxn.GatewayTransactionId = TxnId;
                            if (checktxn.GetRecord())
                            {
                                result = "Already used that Txn Id.";
                            }
                        }
                        if (Sign == "2")
                        {
                            AddRemittanceUserCurrencies obj = new AddRemittanceUserCurrencies();
                            obj.CurrencyID = Convert.ToInt64(CurrencyId);
                            obj.MerchantUniqueId = MerchantUniqueId;
                            obj.CheckActive = 1;
                            obj.CheckDelete = 0;
                            if (obj.GetRecord())
                            {
                                if (usr.Type == (int)AddRemittanceUser.RemittanceType.ODL)
                                {
                                    if (obj.ODL < Convert.ToDecimal(Amount))
                                    {
                                        result = "Insufficient Fund in Selected Wallet";
                                        //return Json(result);
                                    }
                                }
                                else if (usr.Type == (int)AddRemittanceUser.RemittanceType.Prefund)
                                {
                                    if (obj.Prefund < Convert.ToDecimal(Amount))
                                    {
                                        result = "Insufficient Fund in Selected Wallet";
                                        //return Json(result);
                                    }
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(result))
                        {
                            AddRemittanceTransactions outobject = new AddRemittanceTransactions();
                            outobject.MerchantUniqueId = MerchantUniqueId;
                            outobject.Sign = Convert.ToInt32(Sign);
                            if (!string.IsNullOrEmpty(Remarks))
                            {
                                outobject.Remarks = Remarks;
                            }
                            else
                            {
                                outobject.Remarks = "Successfully add fund in wallet currency.";
                            }
                            outobject.FromCurrency = CurrencyName;
                            outobject.FromAmount = Convert.ToDecimal(Amount);
                            outobject.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                            outobject.GatewayTransactionId = TxnId;
                            outobject.CurrencyId = Convert.ToInt32(CurrencyId);
                            outobject.WalletType = Convert.ToInt32(usr.Type);
                            outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            outobject.CreatedByName = Session["AdminUserName"].ToString();
                            outobject.Status = (int)AddRemittanceTransactions.Statuses.Success;
                            outobject.MerchantName = usr.ContactName;
                            outobject.MerchantMemberId = usr.MerchantMemberId;
                            outobject.MerchantContactNumber = usr.ContactNo;
                            outobject.IsFeeAccountTransaction = usr.FeeType;
                            outobject.Type = (int)AddRemittanceTransactions.WalletTypes.LoadWallet;
                            outobject.NetAmount = Convert.ToDecimal(Amount);
                            outobject.Platform = VendorApi_CommonHelper.Request_Platform.Web.ToString();
                            if (!string.IsNullOrEmpty(ReceiptFileName) && Sign == "1")
                            {
                                outobject.ReceiptFile = ReceiptFileName;
                            }
                            if (outobject.Add())
                            {
                                ViewBag.SuccessMessage = "Successfully add fund in selected currency.";
                                Common.AddLogs("Successfully add fund in selected Currency(" + CurrencyName + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "success";
                            }
                            else
                            {
                                ViewBag.Message = "Not add fund in selected currency.";
                                Common.AddLogs("Not add fund in selected currency(" + CurrencyName + ") to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "Not add fund in selected currency(" + CurrencyName + ")";
                            }
                        }
                    }
                    else
                    {
                        result = "Merchant Id not found";
                    }

                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        // GET: MerchantBankDetail
        [Authorize]
        [HttpGet]
        public ActionResult AddRemittanceBankDetail(string MerchantId)
        {
            AddRemittanceBankDetail model = new AddRemittanceBankDetail();
            AddRemittanceUser usr = new AddRemittanceUser();
            if (MerchantId != null && MerchantId != "0")
            {
                usr.MerchantUniqueId = MerchantId;
                if (usr.GetRecord())
                {
                    model.MerchantId = MerchantId;
                    model.GetRecord();
                    List<SelectListItem> banklist = new List<SelectListItem>();
                    banklist = CommonHelpers.GetSelectList_BankList(model.BankCode);
                    ViewBag.BankCode = banklist;

                    //List<SelectListItem> merchantlist = new List<SelectListItem>();
                    //merchantlist = CommonHelpers.GetSelectList_MerchantList(model.MerchantId);
                    ViewBag.MerchantId = MerchantId;
                    ViewBag.Name = usr.ContactName;
                }
                else
                {
                    ViewBag.Name = "";
                    ViewBag.BankCode = "";
                    ViewBag.MerchantId = "";
                    return RedirectToAction("RemittanceList");
                }
            }
            else
            {
                ViewBag.Name = "";
                ViewBag.BankCode = "";
                ViewBag.MerchantId = "";
                return RedirectToAction("RemittanceList");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddRemittanceBankDetail(AddRemittanceBankDetail model)
        {
            if (string.IsNullOrEmpty(model.BankName))
            {
                ViewBag.Message = "Please select bank";
            }
            else if (string.IsNullOrEmpty(model.AccountNumber))
            {
                ViewBag.Message = "Please enter account number";
            }
            else if (string.IsNullOrEmpty(model.BankCode))
            {
                ViewBag.Message = "Please select bank";
            }
            else if (string.IsNullOrEmpty(model.MerchantId))
            {
                ViewBag.Message = "Please select Merchant";
            }
            if (model.MerchantId != "")
            {
                AddRemittanceBankDetail inobject = new AddRemittanceBankDetail();
                inobject.MerchantId = model.MerchantId;
                if (inobject.GetRecord())
                {
                    if (inobject.AccountNumber != model.AccountNumber)
                    {
                        AddRemittanceBankDetail inobjectemail = new AddRemittanceBankDetail();
                        inobjectemail.AccountNumber = model.AccountNumber;
                        if (inobjectemail.GetRecord())
                        {
                            ViewBag.Message = "Account Number already exists";
                        }
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        if (inobject.BankCode != model.BankCode)
                        {
                            AddBankList outobjectbank = new AddBankList();
                            GetBankList inobjectbank = new GetBankList();
                            inobjectbank.BANK_CD = model.BankCode;
                            AddBankList resbank = RepCRUD<GetBankList, AddBankList>.GetRecord(Common.StoreProcedures.sp_BankList_Get, inobjectbank, outobjectbank);
                            if (resbank != null && resbank.Id != 0)
                            {
                                inobject.BranchId = resbank.BRANCH_CD;
                                inobject.BranchName = resbank.BRANCH_NAME;
                                inobject.ICON_NAME = resbank.ICON_NAME;
                            }
                        }
                        inobject.AccountNumber = model.AccountNumber;
                        inobject.BankCode = model.BankCode;
                        inobject.BankName = model.BankName;
                        inobject.IsActive = model.IsActive;
                        inobject.IsApprovedByAdmin = model.IsActive;
                        //inobject.IsPrimary = true;
                        inobject.Name = model.Name;

                        if (Session["AdminMemberId"] != null)
                        {
                            inobject.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            inobject.UpdatedByName = Session["AdminUserName"].ToString();
                            inobject.UpdatedDate = DateTime.UtcNow;
                            if (inobject.Update())
                            {
                                ViewBag.SuccessMessage = "Successfully Updated Remittance Bank Detail.";
                                Common.AddLogs("Updated Remittance Bank Detail of (MerchantUniqueId:" + inobject.MerchantId + "  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                return RedirectToAction("RemittanceBankList");
                            }
                            else
                            {
                                ViewBag.Message = "Not Updated.";
                            }
                        }
                    }
                }
                else
                {
                    AddRemittanceBankDetail inobjectemail = new AddRemittanceBankDetail();
                    inobjectemail.AccountNumber = model.AccountNumber;
                    if (inobjectemail.GetRecord())
                    {
                        ViewBag.Message = "Account Number already exists";
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddBankList outobjectbank = new AddBankList();
                        GetBankList inobjectbank = new GetBankList();
                        inobjectbank.BANK_CD = model.BankCode;
                        AddBankList resbank = RepCRUD<GetBankList, AddBankList>.GetRecord(Common.StoreProcedures.sp_BankList_Get, inobjectbank, outobjectbank);
                        if (resbank != null && resbank.Id != 0)
                        {
                            inobject.BranchId = resbank.BRANCH_CD;
                            inobject.BranchName = resbank.BRANCH_NAME;
                            inobject.ICON_NAME = resbank.ICON_NAME;
                        }
                        inobject.MerchantId = model.MerchantId;
                        inobject.AccountNumber = model.AccountNumber;
                        inobject.BankCode = model.BankCode;
                        inobject.BankName = model.BankName;
                        inobject.IsActive = model.IsActive;
                        inobject.IsApprovedByAdmin = model.IsActive;
                        //inobject.IsPrimary = true;
                        inobject.Name = model.Name;

                        if (Session["AdminMemberId"] != null)
                        {
                            inobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            inobject.CreatedByName = Session["AdminUserName"].ToString();
                            if (inobject.Add())
                            {
                                ViewBag.SuccessMessage = "Successfully Added Remittance Bank Detail.";
                                Common.AddLogs("Added Remittance Bank Detail by(AdminMemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                return RedirectToAction("RemittanceBankList");
                            }
                            else
                            {
                                ViewBag.Message = "Not Added.";
                            }
                        }
                    }
                }
            }
            else
            {
                ViewBag.Message = "Please select Merchant";
            }

            //if (model.MerchantId != null && model.MerchantId != "0")
            //{
            //    model.MerchantId = model.MerchantId;
            //    if (model.GetRecord())
            //    {
            //        List<SelectListItem> banklist = new List<SelectListItem>();
            //        banklist = CommonHelpers.GetSelectList_BankList(model.BankCode);
            //        ViewBag.BankCode = banklist;
            //        List<SelectListItem> merchantlist = new List<SelectListItem>();
            //        merchantlist = CommonHelpers.GetSelectList_MerchantList(model.MerchantId);
            //        ViewBag.MerchantId = model.MerchantId;
            //    }
            //}
            //AddRemittanceBankDetail model = new AddRemittanceBankDetail();
            AddRemittanceUser usr = new AddRemittanceUser();
            if (model.MerchantId != null && model.MerchantId != "0")
            {
                usr.MerchantUniqueId = model.MerchantId;
                if (usr.GetRecord())
                {
                    model.MerchantId = model.MerchantId;
                    model.GetRecord();
                    List<SelectListItem> banklist = new List<SelectListItem>();
                    banklist = CommonHelpers.GetSelectList_BankList(model.BankCode);
                    ViewBag.BankCode = banklist;

                    ViewBag.MerchantId = model.MerchantId;
                    ViewBag.Name = usr.ContactName;
                }
                else
                {
                    ViewBag.Name = "";
                    ViewBag.BankCode = "";
                    ViewBag.MerchantId = "";
                    return RedirectToAction("RemittanceList");
                }
            }
            else
            {
                ViewBag.Name = "";
                ViewBag.BankCode = "";
                ViewBag.MerchantId = "";
                return RedirectToAction("RemittanceList");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemittanceBankList(string MerchantId)
        {
            ViewBag.MerchantId = string.IsNullOrEmpty(MerchantId) ? "" : MerchantId;
            return View();
        }

        [Authorize]
        public JsonResult GetRemittanceBankLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Member Id");
            columns.Add("Name");
            columns.Add("Bank Code");
            columns.Add("Bank Name");
            columns.Add("Branch Id");
            columns.Add("Branch Name");
            columns.Add("Account Number");
            //columns.Add("Is Primary");
            columns.Add("Created By");
            columns.Add("Updated By");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MerchantId = context.Request.Form["MerchantId"];
            string Name = context.Request.Form["Name"];
            string BankCode = context.Request.Form["BankCode"];
            string BankName = context.Request.Form["BankName"];
            string BranchName = context.Request.Form["BranchName"];
            string AccountNumber = context.Request.Form["AccountNumber"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["EndDate"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceBankDetail> trans = new List<AddRemittanceBankDetail>();
            AddRemittanceBankDetail w = new AddRemittanceBankDetail();
            w.MerchantId = MerchantId;
            w.Name = Name;
            w.BankCode = BankCode;
            w.BankName = BankName;
            w.BranchName = BranchName;
            w.AccountNumber = AccountNumber;
            w.StartDate = fromdate;
            w.EndDate = todate;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceBankDetail
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         MerchantId = row["MerchantId"].ToString(),
                         Name = row["Name"].ToString(),
                         BankCode = row["BankCode"].ToString(),
                         BankName = row["BankName"].ToString(),
                         BranchId = row["BranchId"].ToString(),
                         BranchName = row["BranchName"].ToString(),
                         AccountNumber = row["AccountNumber"].ToString(),
                         //IsPrimary = Convert.ToBoolean(row["IsPrimary"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         CreatedDateDt = row["IndiaDate"].ToString(),
                         UpdatedDateDt = row["UpdatedIndiaDate"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceBankDetail>> objDataTableResponse = new DataTableResponse<List<AddRemittanceBankDetail>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        // GET: AddRemittanceSourceCurrency
        [Authorize]
        [HttpGet]
        public ActionResult AddRemittanceSourceCurrency()
        {
            AddRemittanceCurrencyList modal = new AddRemittanceCurrencyList();
            List<SelectListItem> currencylist = new List<SelectListItem>();
            currencylist = CommonHelpers.GetSelectList_RemittanceCurrency(Convert.ToInt32(modal.Id));
            ViewBag.CurrencyList = currencylist;
            return View();
        }

        public JsonResult RemittanceSourceCurrency(string CurrencyId, string DestinationCurrencyList)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(CurrencyId))
                {
                    result = "Please select currency.";
                }
                else if (string.IsNullOrEmpty(DestinationCurrencyList))
                {
                    result = "Please select Destination Currency .";
                }
                else
                {
                    AddRemittanceCurrencyList outobject = new AddRemittanceCurrencyList();
                    outobject.Id = Convert.ToInt64(CurrencyId);
                    outobject.CheckActive = 1;
                    outobject.CheckDelete = 0;
                    if (outobject.GetRecord())
                    {
                        AddRemittanceSourceCurrencyList checksource = new AddRemittanceSourceCurrencyList();
                        checksource.CurrencyId = Convert.ToInt32(CurrencyId);
                        checksource.CheckActive = 1;
                        checksource.CheckDelete = 0;
                        if (checksource.GetRecord())
                        {
                            //result = "Source Currency already added.";
                            if (DestinationCurrencyList != "")
                            {
                                string[] splitvalues = DestinationCurrencyList.Split(',');
                                if (splitvalues != null)
                                {
                                    //int count = 0;
                                    foreach (var item in splitvalues)
                                    {
                                        if (item != "")
                                        {
                                            AddRemittanceCurrencyList getname = new AddRemittanceCurrencyList();
                                            getname.Id = Convert.ToInt64(item);
                                            getname.CheckActive = 1;
                                            getname.CheckDelete = 0;
                                            if (getname.GetRecord())
                                            {

                                                AddRemittanceConversionRates checkpair = new AddRemittanceConversionRates();
                                                checkpair.SourceCurrencyUniqueId = CurrencyId;
                                                checkpair.DestinationCurrencyUniqueId = item;
                                                checkpair.CheckActive = 1;
                                                checkpair.CheckDelete = 0;
                                                if (checkpair.GetRecord())
                                                {
                                                    result = "This Currency pair already added";
                                                }
                                                else
                                                {
                                                    string PairRandomId = Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString();

                                                    AddRemittanceConversionRates pair = new AddRemittanceConversionRates();
                                                    pair.SourceCurrencyUniqueId = CurrencyId;
                                                    pair.DestinationCurrencyUniqueId = item;
                                                    pair.ConversionRate = 0;
                                                    pair.InverseRate = 0;
                                                    pair.Markup = 0;
                                                    pair.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                                    pair.CreatedByName = Session["AdminUserName"].ToString();
                                                    pair.IsActive = true;
                                                    pair.IsApprovedByAdmin = true;
                                                    pair.SourceCurrencyName = outobject.CurrencyName;
                                                    pair.DestinationCurrencyName = getname.CurrencyName;
                                                    pair.UniqueId = outobject.Currency + getname.CurrencyName + PairRandomId;
                                                    if (pair.Add())
                                                    {
                                                        Common.AddLogs("Add Pair for Source Currency(" + CurrencyId + ") and Destination Currency(" + item + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                                        result = "success";
                                                    }
                                                    else
                                                    {
                                                        Common.AddLogs("Failed to Add Conversion rate", true, (int)AddLog.LogType.Remittance);
                                                        result = "Failed to add Conversion rate";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            AddRemittanceSourceCurrencyList source = new AddRemittanceSourceCurrencyList();
                            source.CurrencyId = Convert.ToInt32(CurrencyId);
                            source.CurrencyName = outobject.CurrencyName;
                            source.Symbol = outobject.Symbol;
                            string RandomId = Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString();
                            source.UniqueId = outobject.CurrencyName + RandomId;
                            source.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            source.CreatedByName = Session["AdminUserName"].ToString();
                            source.IsActive = true;
                            source.IsApprovedByAdmin = true;
                            if (source.Add())
                            {
                                if (DestinationCurrencyList != "")
                                {
                                    string[] splitvalues = DestinationCurrencyList.Split(',');
                                    if (splitvalues != null)
                                    {
                                        //int count = 0;
                                        foreach (var item in splitvalues)
                                        {
                                            if (item != "")
                                            {
                                                AddRemittanceCurrencyList getname = new AddRemittanceCurrencyList();
                                                getname.Id = Convert.ToInt64(item);
                                                getname.CheckActive = 1;
                                                getname.CheckDelete = 0;
                                                if (getname.GetRecord())
                                                {
                                                    AddRemittanceConversionRates checkpair = new AddRemittanceConversionRates();
                                                    checkpair.SourceCurrencyUniqueId = CurrencyId;
                                                    checkpair.DestinationCurrencyUniqueId = item;
                                                    checkpair.CheckActive = 1;
                                                    checkpair.CheckDelete = 0;
                                                    if (checkpair.GetRecord())
                                                    {
                                                        result = "This Currency Pair already added";
                                                    }
                                                    else
                                                    {
                                                        string PairRandomId = Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString();
                                                        AddRemittanceConversionRates pair = new AddRemittanceConversionRates();
                                                        pair.SourceCurrencyUniqueId = CurrencyId;
                                                        pair.DestinationCurrencyUniqueId = item;
                                                        pair.ConversionRate = 0;
                                                        pair.InverseRate = 0;
                                                        pair.Markup = 0;
                                                        pair.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                                        pair.CreatedByName = Session["AdminUserName"].ToString();
                                                        pair.IsActive = true;
                                                        pair.IsApprovedByAdmin = true;
                                                        pair.SourceCurrencyName = outobject.CurrencyName;
                                                        pair.DestinationCurrencyName = getname.CurrencyName;
                                                        pair.UniqueId = outobject.CurrencyName + getname.CurrencyName + PairRandomId;

                                                        if (pair.Add())
                                                        {
                                                            Common.AddLogs("Add Pair for Source Currency(" + CurrencyId + ") and Destination Currency(" + item + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                                            result = "success";
                                                        }
                                                        else
                                                        {
                                                            Common.AddLogs("Failed to Add Conversion rate", true, (int)AddLog.LogType.Remittance);
                                                            result = "Failed to add Conversion rate";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                ViewBag.SuccessMessage = "Successfully added source currency.";
                                Common.AddLogs("Add Source Currency(" + CurrencyId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                result = "success";
                            }
                            else
                            {
                                ViewBag.Message = "Not assign selected currency.";
                                Common.AddLogs("Failed to Add source currency(" + CurrencyId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                result = "Failed to add source currency(" + CurrencyId + ")";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        public JsonResult RemittanceDestinationCurrency(string CurrencyId)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(CurrencyId))
                {
                    result = "Please select currency.";
                }
                else
                {
                    AddRemittanceCurrencyList outobject = new AddRemittanceCurrencyList();
                    outobject.Id = Convert.ToInt64(CurrencyId);
                    outobject.CheckActive = 1;
                    outobject.CheckDelete = 0;
                    if (outobject.GetRecord())
                    {
                        AddRemittanceDestinationCurrencyList checksource = new AddRemittanceDestinationCurrencyList();
                        checksource.CurrencyId = Convert.ToInt32(CurrencyId);
                        checksource.CheckActive = 1;
                        checksource.CheckDelete = 0;
                        if (checksource.GetRecord())
                        {
                            result = "Destination Currency already added.";
                        }
                        else
                        {
                            AddRemittanceDestinationCurrencyList source = new AddRemittanceDestinationCurrencyList();
                            source.CurrencyId = Convert.ToInt32(CurrencyId);
                            source.CurrencyName = outobject.CurrencyName;
                            source.Symbol = outobject.Symbol;
                            string RandomId = Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString();
                            source.UniqueId = outobject.CurrencyName + RandomId;
                            source.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            source.CreatedByName = Session["AdminUserName"].ToString();
                            source.IsActive = true;
                            source.IsApprovedByAdmin = true;
                            if (source.Add())
                            {
                                Common.AddLogs("Add destination Currency(" + CurrencyId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                result = "success";
                            }
                            else
                            {
                                Common.AddLogs("Failed to Add destination currency(" + CurrencyId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                                result = "Failed to add destination currency(" + CurrencyId + ")";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [Authorize]
        public JsonResult AddFeeBalance(string MerchantUniqueId, string Type, string Sign, string TxnId, string Remarks, string Amount, String ReceiptFileName)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(Sign))
                {
                    result = "Please select Sign.";
                }
                else if (string.IsNullOrEmpty(TxnId) && Sign == "1")
                {
                    result = "Please enter Txn Id.";
                }
                else if (string.IsNullOrEmpty(Amount))
                {
                    result = "Please enter Amount.";
                }
                else if (!string.IsNullOrEmpty(Amount))
                {
                    if (Convert.ToDecimal(Amount) <= 0)
                    {
                        result = "Please enter valid Amount";
                    }
                }
                else if (Sign == "1")
                {
                    if (string.IsNullOrEmpty(ReceiptFileName))
                    {
                        ViewBag.Message = "Please upload Receipt for Credit Fee Balance.";
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    AddRemittanceUser usr = new AddRemittanceUser();
                    usr.MerchantUniqueId = MerchantUniqueId;
                    if (usr.GetRecord())
                    {
                        if (Sign == "1")
                        {
                            AddRemittanceTransactions checktxn = new AddRemittanceTransactions();
                            checktxn.GatewayTransactionId = TxnId;
                            if (checktxn.GetRecord())
                            {
                                result = "Already used that Txn Id.";
                            }
                        }
                        if (Sign == "2")
                        {
                            if (usr.FeeAccountBalance < Convert.ToDecimal(Amount))
                            {
                                result = "Insufficient Fee Balance.";
                            }
                        }
                        if (string.IsNullOrEmpty(result))
                        {
                            AddRemittanceTransactions outobject = new AddRemittanceTransactions();
                            outobject.MerchantUniqueId = MerchantUniqueId;
                            outobject.Sign = Convert.ToInt32(Sign);
                            if (!string.IsNullOrEmpty(Remarks))
                            {
                                outobject.Remarks = Remarks;
                            }
                            else
                            {
                                outobject.Remarks = "Successfully add fee account balance.";
                            }
                            outobject.FromCurrency = usr.FromCurrencyCode;
                            outobject.FromAmount = Convert.ToDecimal(Amount);
                            outobject.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                            outobject.GatewayTransactionId = TxnId;
                            outobject.CurrencyId = Convert.ToInt32(usr.FromCurrencyId);
                            outobject.WalletType = Convert.ToInt32(usr.Type);
                            outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            outobject.CreatedByName = Session["AdminUserName"].ToString();
                            outobject.Status = (int)AddRemittanceTransactions.Statuses.Success;
                            outobject.MerchantName = usr.ContactName;
                            outobject.MerchantMemberId = usr.MerchantMemberId;
                            outobject.MerchantContactNumber = usr.ContactNo;
                            outobject.FeeType = usr.FeeType;
                            outobject.Type = (int)AddRemittanceTransactions.WalletTypes.FeeAccountBalance;
                            outobject.NetAmount = Convert.ToDecimal(Amount);
                            outobject.Platform = VendorApi_CommonHelper.Request_Platform.Web.ToString();
                            if (!string.IsNullOrEmpty(ReceiptFileName) && Sign == "1")
                            {
                                outobject.ReceiptFile = ReceiptFileName;
                            }
                            if (outobject.Add())
                            {
                                ViewBag.SuccessMessage = "Successfully add fee account balance.";
                                Common.AddLogs("Successfully add fee account balance into MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "success";
                            }
                            else
                            {
                                ViewBag.Message = "Not add fee account balance.";
                                Common.AddLogs("Not add fee account balance to MerchantId :" + MerchantUniqueId + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                result = "Not add fee account balance";
                            }
                        }
                    }
                    else
                    {
                        result = "Merchant Id not found";
                    }

                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        // GET: RemittanceConversionRates
        [Authorize]
        [HttpGet]
        public ActionResult RemittanceConversionRates()
        {
            List<SelectListItem> sourcecurrencylist = new List<SelectListItem>();
            sourcecurrencylist = CommonHelpers.GetSelectList_RemittanceSourceCurrency();
            ViewBag.SourceCurrencyList = sourcecurrencylist;

            List<SelectListItem> destinationcurrencylist = new List<SelectListItem>();
            destinationcurrencylist = CommonHelpers.GetSelectList_RemittanceDestinationCurrency();
            ViewBag.DestinationCurrencyList = destinationcurrencylist;
            return View();
        }

        public JsonResult SetRemittanceConversionRate(AddRemittanceConversionRates conversionrates)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    AddRemittanceConversionRates outobject = new AddRemittanceConversionRates();
                    outobject.Id = conversionrates.Id;
                    outobject.CheckActive = 1;
                    outobject.CheckDelete = 0;
                    if (outobject.GetRecord())
                    {
                        outobject.ConversionRate = Convert.ToDecimal(conversionrates.ConversionRate);
                        outobject.InverseRate = Convert.ToDecimal(1 / (conversionrates.ConversionRate == 0 ? 1 : conversionrates.ConversionRate));
                        outobject.Markup = Convert.ToDecimal(conversionrates.Markup);
                        outobject.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        outobject.UpdatedByName = Session["AdminUserName"].ToString();
                        if (outobject.Update())
                        {
                            Common.AddLogs("Update Conversion Rate for UniqueId(" + conversionrates.UniqueId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                            AddRemittanceConversionRatesHistory addRemittanceConversionRatesHistory = new AddRemittanceConversionRatesHistory();
                            addRemittanceConversionRatesHistory.SourceCurrencyUniqueId = outobject.SourceCurrencyUniqueId;
                            addRemittanceConversionRatesHistory.DestinationCurrencyUniqueId = outobject.DestinationCurrencyUniqueId;
                            addRemittanceConversionRatesHistory.ConversionRate = outobject.ConversionRate;
                            addRemittanceConversionRatesHistory.InverseRate = outobject.InverseRate;
                            addRemittanceConversionRatesHistory.Markup = outobject.Markup;
                            addRemittanceConversionRatesHistory.SourceCurrencyName = outobject.SourceCurrencyName;
                            addRemittanceConversionRatesHistory.DestinationCurrencyName = outobject.DestinationCurrencyName;
                            addRemittanceConversionRatesHistory.UniqueId = outobject.UniqueId;
                            addRemittanceConversionRatesHistory.IsAutoUpdated = Convert.ToInt32(outobject.IsAutoUpdated);
                            addRemittanceConversionRatesHistory.IpAddress = Common.GetUserIP();
                            addRemittanceConversionRatesHistory.RemittanceConversionRatesID = outobject.Id;
                            addRemittanceConversionRatesHistory.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            addRemittanceConversionRatesHistory.CreatedByName = Session["AdminUserName"].ToString(); ;
                            addRemittanceConversionRatesHistory.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            addRemittanceConversionRatesHistory.UpdatedByName = Session["AdminUserName"].ToString(); ;
                            addRemittanceConversionRatesHistory.IsDeleted = false;
                            addRemittanceConversionRatesHistory.IsApprovedByAdmin = true;
                            addRemittanceConversionRatesHistory.IsActive = true;
                            if (addRemittanceConversionRatesHistory.Add())
                            {
                                Common.AddLogs("Add Conversion Rate History for UniqueId(" + conversionrates.UniqueId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                            }
                            result = "success";
                        }
                        else
                        {
                            Common.AddLogs("Failed to Update Conversion rate", true, (int)AddLog.LogType.Remittance);
                            result = "Failed to Update Conversion rate";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]
        [Authorize]
        public ActionResult CurrencyConversionHistory()
        {

            return View();
        }

        [Authorize]
        public JsonResult GetCurrencyConversionHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Created Date");
            columns.Add("Source Currency");
            columns.Add("Destination Currency");
            columns.Add("Created By");
            columns.Add("Updated By");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string SourceCurrencyName = context.Request.Form["SourceCurrencyName"];
            string DestinationCurrencyName = context.Request.Form["DestinationCurrencyName"];
            string BankConversionRateCode = context.Request.Form["BankConversionRateCode"];
            string InverseRate = context.Request.Form["InverseRate"];
            string Markup = context.Request.Form["Markup"];

            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceConversionRatesHistory> trans = new List<AddRemittanceConversionRatesHistory>();
            AddRemittanceConversionRatesHistory w = new AddRemittanceConversionRatesHistory();
            w.SourceCurrencyName = SourceCurrencyName;
            w.DestinationCurrencyName = DestinationCurrencyName;
            w.ConversionRate = BankConversionRateCode != "" ? Convert.ToDecimal(BankConversionRateCode) : 0;
            w.InverseRate = InverseRate != "" ? Convert.ToDecimal(InverseRate) : 0;
            w.Markup = Markup != "" ? Convert.ToDecimal(Markup) : 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceConversionRatesHistory
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         SourceCurrencyUniqueId = row["SourceCurrencyUniqueId"].ToString(),
                         DestinationCurrencyUniqueId = row["DestinationCurrencyUniqueId"].ToString(),
                         SourceCurrencyName = row["SourceCurrencyName"].ToString(),
                         DestinationCurrencyName = row["DestinationCurrencyName"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         CreatedDateDt = row["IndiaDate"].ToString(),

                         UniqueId = row["UniqueId"].ToString(),
                         ConversionRate = Convert.ToDecimal(row["ConversionRate"].ToString()),
                         Markup = Convert.ToDecimal(row["Markup"].ToString()),
                         InverseRate = Convert.ToDecimal(row["InverseRate"].ToString()),
                         IpAddress = row["IpAddress"].ToString(),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceConversionRatesHistory>> objDataTableResponse = new DataTableResponse<List<AddRemittanceConversionRatesHistory>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };
            return Json(objDataTableResponse);


        }

        public JsonResult CalculateConvertAmount(string Amount, string SourceCurrencyId, string DestinationCurrencyId)
        {
            string result = "";
            Decimal ConvertedAmount = 0;
            Decimal ReceivedAmount = 0;
            Decimal Fees = 0;
            try
            {
                if (Convert.ToDecimal(Amount) <= 0)
                {
                    result = "Please enter a valid amount";
                }
                if (SourceCurrencyId == "0" || SourceCurrencyId == "")
                {
                    result = "Please select Source Currency";
                }
                if (DestinationCurrencyId == "0" || DestinationCurrencyId == "")
                {
                    result = "Please select Destination Currency";
                }
                if (Convert.ToInt32(SourceCurrencyId) == Convert.ToInt32(DestinationCurrencyId))
                {
                    result = "Please select different destination currency";
                }
                if (string.IsNullOrEmpty(result))
                {
                    AddRemittanceConversionRates outobject = new AddRemittanceConversionRates();
                    outobject.SourceCurrencyUniqueId = SourceCurrencyId;
                    outobject.DestinationCurrencyUniqueId = DestinationCurrencyId;
                    outobject.CheckActive = 1;
                    outobject.CheckDelete = 0;
                    if (outobject.GetRecord())
                    {
                        AddRemittanceCalculateServiceCharge servicecharge = Common.RemittanceCalculateServiceCharge(outobject.SourceCurrencyName, Convert.ToDecimal(Amount), outobject.DestinationCurrencyName);
                        Fees = servicecharge.ServiceCharge;
                        if (Fees == 0)
                        {
                            result = "NoSuccess,Fees not available for Amount that you entered.";
                        }
                        else
                        {
                            ReceivedAmount = (Convert.ToDecimal(Amount) - Fees) * outobject.ConversionRate;
                            ConvertedAmount = (Convert.ToDecimal(Amount) - Fees);
                            result = "success," + Math.Round(ConvertedAmount, 2) + "," + Fees + "," + outobject.ConversionRate + "," + Math.Round(ReceivedAmount, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        public JsonResult SubmitConvertedAmount(string Amount, string SourceCurrencyId, string DestinationCurrencyId, string MerchantUniqueId, string UserType, string Remarks)
        {
            string result = "";
            Decimal ConvertedAmount = 0;
            Decimal Fees = 0;
            Decimal ReceivedAmount = 0;
            try
            {
                if (Convert.ToDecimal(Amount) <= 0)
                {
                    result = "Please enter a valid amount";
                }
                if (SourceCurrencyId == "0" || SourceCurrencyId == "")
                {
                    result = "Please select Source Currency";
                }
                if (DestinationCurrencyId == "0" || DestinationCurrencyId == "")
                {
                    result = "Please select Destination Currency";
                }
                if (Convert.ToInt32(SourceCurrencyId) == Convert.ToInt32(DestinationCurrencyId))
                {
                    result = "Please select different destination currency";
                }
                //if(string.IsNullOrEmpty(Remarks))
                //{
                //    result = "Please enter Remarks";
                //}
                if (string.IsNullOrEmpty(result))
                {
                    AddRemittanceUser usr = new AddRemittanceUser();
                    usr.MerchantUniqueId = MerchantUniqueId;
                    usr.CheckActive = 1;
                    usr.CheckDelete = 0;
                    if (usr.GetRecord())
                    {
                        AddRemittanceConversionRates outobject = new AddRemittanceConversionRates();
                        outobject.SourceCurrencyUniqueId = SourceCurrencyId;
                        outobject.DestinationCurrencyUniqueId = DestinationCurrencyId;
                        outobject.CheckActive = 1;
                        outobject.CheckDelete = 0;
                        if (outobject.GetRecord())
                        {
                            AddRemittanceCalculateServiceCharge servicecharge = Common.RemittanceCalculateServiceCharge(outobject.SourceCurrencyName, Convert.ToDecimal(Amount), outobject.DestinationCurrencyName);
                            Fees = servicecharge.ServiceCharge;
                            if (Fees == 0)
                            {
                                result = "Fees not available for Amount that you entered.";
                            }
                            ReceivedAmount = (Convert.ToDecimal(Amount) - Fees) * outobject.ConversionRate;
                            ConvertedAmount = (Convert.ToDecimal(Amount) - Fees);
                            if (string.IsNullOrEmpty(result))
                            {

                                AddRemittanceUserCurrencies obj = new AddRemittanceUserCurrencies();
                                obj.CurrencyID = Convert.ToInt64(SourceCurrencyId);
                                obj.MerchantUniqueId = MerchantUniqueId;
                                obj.CheckActive = 1;
                                obj.CheckDelete = 0;
                                if (obj.GetRecord())
                                {
                                    if (usr.Type == (int)AddRemittanceUser.RemittanceType.ODL)
                                    {
                                        if (obj.ODL < Convert.ToDecimal(Amount))
                                        {
                                            result = "Insufficient Fund in Selected Wallet";
                                            return Json(result);
                                        }
                                    }
                                    else if (usr.Type == (int)AddRemittanceUser.RemittanceType.Prefund)
                                    {
                                        if (obj.Prefund < Convert.ToDecimal(Amount))
                                        {
                                            result = "Insufficient Fund in Selected Wallet";
                                            return Json(result);
                                        }
                                    }
                                    if (string.IsNullOrEmpty(result))
                                    {
                                        AddRemittanceTransactions txn = new AddRemittanceTransactions();
                                        txn.MerchantUniqueId = MerchantUniqueId;
                                        txn.Sign = (int)AddRemittanceTransactions.Signs.Debit;
                                        if (!string.IsNullOrEmpty(Remarks))
                                        {
                                            txn.Remarks = Remarks;
                                        }
                                        else
                                        {
                                            txn.Remarks = "Successfully Convert Amount from " + outobject.SourceCurrencyName + " to " + outobject.DestinationCurrencyName + ".";
                                        }
                                        txn.Description = "Successfully Convert Amount(" + Amount + ") from " + outobject.SourceCurrencyName + " to " + outobject.DestinationCurrencyName + ".";
                                        txn.FromAmount = Convert.ToDecimal(Amount);
                                        txn.ToAmount = ReceivedAmount;
                                        txn.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                                        txn.GatewayTransactionId = new CommonHelpers().GenerateUniqueId();
                                        txn.Reference = new CommonHelpers().GenerateUniqueId();
                                        txn.CurrencyId = Convert.ToInt32(SourceCurrencyId);
                                        txn.WalletType = Convert.ToInt32(usr.Type);
                                        txn.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                        txn.CreatedByName = Session["AdminUserName"].ToString();
                                        txn.Status = (int)AddRemittanceTransactions.Statuses.Success;
                                        txn.GatewayStatus = AddRemittanceTransactions.Statuses.Success.ToString();
                                        txn.MerchantName = usr.ContactName;
                                        txn.MerchantMemberId = usr.MerchantMemberId;
                                        txn.MerchantContactNumber = usr.ContactNo;
                                        txn.FeeType = usr.FeeType;
                                        txn.ServiceCharge = Fees;
                                        txn.Type = (int)AddRemittanceTransactions.WalletTypes.ConvertWallet;
                                        txn.ConversionRate = outobject.ConversionRate;
                                        txn.ConvertedAmount = ConvertedAmount;
                                        txn.NetAmount = Convert.ToDecimal(Amount);
                                        txn.BaseCurrencyServiceCharge = Fees;

                                        txn.FromCurrency = outobject.SourceCurrencyName;
                                        txn.ToCurrency = outobject.DestinationCurrencyName;

                                        txn.Platform = VendorApi_CommonHelper.Request_Platform.Web.ToString();
                                        if (txn.Add())
                                        {
                                            ViewBag.SuccessMessage = "Successfully Convert Amount from selected  source currency to destination currency.";
                                            Common.AddLogs("Successfully Convert Amount from  " + outobject.SourceCurrencyName + " to " + outobject.DestinationCurrencyName + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                            result = "success";
                                        }
                                        else
                                        {
                                            ViewBag.Message = "Not add fund in selected currency.";
                                            Common.AddLogs("Amount Not Converted");
                                            result = "Amount Not Converted";
                                        }
                                    }
                                }
                                AddRemittanceUserCurrencies objj = new AddRemittanceUserCurrencies();
                                objj.CurrencyID = Convert.ToInt64(DestinationCurrencyId);
                                objj.MerchantUniqueId = MerchantUniqueId;
                                objj.CheckActive = 1;
                                objj.CheckDelete = 0;
                                if (objj.GetRecord())
                                {
                                    //if (usr.Type == (int)AddRemittanceUser.RemittanceType.ODL)
                                    //{
                                    //    if (objj.ODL < Convert.ToDecimal(Amount))
                                    //    {
                                    //        result = "Insufficient Fund in Selected Wallet";
                                    //        return Json(result);
                                    //    }
                                    //}
                                    //else if (usr.Type == (int)AddRemittanceUser.RemittanceType.Prefund)
                                    //{
                                    //    if (objj.Prefund < Convert.ToDecimal(Amount))
                                    //    {
                                    //        result = "Insufficient Fund in Selected Wallet";
                                    //        return Json(result);
                                    //    }
                                    //}
                                    if (string.IsNullOrEmpty(result) || result == "success")
                                    {
                                        AddRemittanceTransactions txnn = new AddRemittanceTransactions();
                                        txnn.MerchantUniqueId = MerchantUniqueId;
                                        txnn.Sign = (int)AddRemittanceTransactions.Signs.Credit;
                                        if (!string.IsNullOrEmpty(Remarks))
                                        {
                                            txnn.Remarks = Remarks;
                                        }
                                        else
                                        {
                                            txnn.Remarks = "Successfully Convert Amount from " + outobject.SourceCurrencyName + " to " + outobject.DestinationCurrencyName + ".";
                                        }
                                        txnn.Description = "Successfully Convert Amount(" + ReceivedAmount + ") from " + outobject.SourceCurrencyName + " to " + outobject.DestinationCurrencyName + ".";
                                        txnn.FromAmount = Convert.ToDecimal(ReceivedAmount);
                                        txnn.ToAmount = Convert.ToDecimal(Amount);
                                        txnn.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                                        txnn.GatewayTransactionId = new CommonHelpers().GenerateUniqueId();
                                        txnn.Reference = new CommonHelpers().GenerateUniqueId();
                                        txnn.CurrencyId = Convert.ToInt32(DestinationCurrencyId);
                                        txnn.WalletType = Convert.ToInt32(usr.Type);
                                        txnn.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                        txnn.CreatedByName = Session["AdminUserName"].ToString();
                                        txnn.Status = (int)AddRemittanceTransactions.Statuses.Success;
                                        txnn.GatewayStatus = AddRemittanceTransactions.Statuses.Success.ToString();
                                        txnn.MerchantName = usr.ContactName;
                                        txnn.MerchantMemberId = usr.MerchantMemberId;
                                        txnn.MerchantContactNumber = usr.ContactNo;
                                        txnn.FeeType = usr.FeeType;
                                        txnn.Type = (int)AddRemittanceTransactions.WalletTypes.ConvertWallet;
                                        txnn.ServiceCharge = Fees;
                                        txnn.ConversionRate = outobject.ConversionRate;
                                        txnn.ConvertedAmount = ReceivedAmount;
                                        txnn.NetAmount = Convert.ToDecimal(Amount);
                                        txnn.BaseCurrencyServiceCharge = Fees;

                                        txnn.ToCurrency = outobject.SourceCurrencyName;
                                        txnn.FromCurrency = outobject.DestinationCurrencyName;
                                        txnn.Platform = VendorApi_CommonHelper.Request_Platform.Web.ToString();
                                        if (txnn.Add())
                                        {
                                            ViewBag.SuccessMessage = "Successfully Convert Amount from selected  source currency to destination currency.";
                                            Common.AddLogs("Successfully Convert Amount from  " + outobject.SourceCurrencyName + " to " + outobject.DestinationCurrencyName + " by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance, Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Remittance_Assign_Currency);
                                            result = "success";
                                        }
                                        else
                                        {
                                            ViewBag.Message = "Amount Not Converted.";
                                            Common.AddLogs("Amount Not Converted");
                                            result = "Amount Not Converted";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        result = "Merchant Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [Authorize]
        public JsonResult GetCurrencyConversionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Created Date");
            columns.Add("Source Currency");
            columns.Add("Destination Currency");
            columns.Add("Created By");
            columns.Add("Updated By");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //string MerchantId = context.Request.Form["MerchantId"];
            //string Name = context.Request.Form["Name"];
            //string BankCode = context.Request.Form["BankCode"];
            //string BankName = context.Request.Form["BankName"];
            //string BranchName = context.Request.Form["BranchName"];
            //string AccountNumber = context.Request.Form["AccountNumber"];
            //string fromdate = context.Request.Form["StartDate"];
            //string todate = context.Request.Form["EndDate"];
            //string RoleId = context.Request.Form["RoleId"];
            //Sorting Direction  
            string SourceCurrencyId = context.Request.Form["CurrencyId"];
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceConversionRates> trans = new List<AddRemittanceConversionRates>();
            AddRemittanceConversionRates w = new AddRemittanceConversionRates();
            w.SourceCurrencyUniqueId = SourceCurrencyId;
            //w.StartDate = fromdate;
            //w.EndDate = todate;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceConversionRates
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         SourceCurrencyUniqueId = row["SourceCurrencyUniqueId"].ToString(),
                         DestinationCurrencyUniqueId = row["DestinationCurrencyUniqueId"].ToString(),
                         SourceCurrencyName = row["SourceCurrencyName"].ToString(),
                         DestinationCurrencyName = row["DestinationCurrencyName"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         UniqueId = row["UniqueId"].ToString(),
                         ConversionRate = Convert.ToDecimal(row["ConversionRate"].ToString()),
                         Markup = Convert.ToDecimal(row["Markup"].ToString()),
                         InverseRate = Convert.ToDecimal(row["InverseRate"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceConversionRates>> objDataTableResponse = new DataTableResponse<List<AddRemittanceConversionRates>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        // GET: AddRemittanceSourceCurrency
        [Authorize]
        [HttpGet]
        public ActionResult AddRemittanceDestinationCurrency()
        {
            AddRemittanceCurrencyList modal = new AddRemittanceCurrencyList();
            List<SelectListItem> currencylist = new List<SelectListItem>();
            currencylist = CommonHelpers.GetSelectList_RemittanceCurrency(Convert.ToInt32(modal.Id));
            ViewBag.CurrencyList = currencylist;
            return View();
        }

        [Authorize]
        public JsonResult GetDestinationCurrenciesLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            //columns.Add("Created Date");
            columns.Add("Source Currency");
            columns.Add("Destination Currency");
            columns.Add("Created By");
            columns.Add("Updated By");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //string MerchantId = context.Request.Form["MerchantId"];
            //string Name = context.Request.Form["Name"];
            //string BankCode = context.Request.Form["BankCode"];
            //string BankName = context.Request.Form["BankName"];
            //string BranchName = context.Request.Form["BranchName"];
            //string AccountNumber = context.Request.Form["AccountNumber"];
            //string fromdate = context.Request.Form["StartDate"];
            //string todate = context.Request.Form["EndDate"];
            //string RoleId = context.Request.Form["RoleId"];
            string SourceCurrencyId = context.Request.Form["CurrencyId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceDestinationCurrencyList> trans = new List<AddRemittanceDestinationCurrencyList>();
            AddRemittanceDestinationCurrencyList w = new AddRemittanceDestinationCurrencyList();
            w.SourceCurrencyId = SourceCurrencyId;
            //w.StartDate = fromdate;
            //w.EndDate = todate;
            DataTable dt = w.GetRemittanceFilteredDestinationCurrencyList();

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceDestinationCurrencyList
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         CurrencyId = Convert.ToInt64(row["CurrencyId"].ToString()),
                         CurrencyName = row["CurrencyName"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceDestinationCurrencyList>> objDataTableResponse = new DataTableResponse<List<AddRemittanceDestinationCurrencyList>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        public JsonResult GetPairedCurrencyLists(string CurrencyId)
        {
            string result = "";
            if (!string.IsNullOrEmpty(CurrencyId))
            {
                AddRemittanceConversionRates obj = new AddRemittanceConversionRates();
                obj.SourceCurrencyUniqueId = CurrencyId;
                obj.CheckActive = 1;
                obj.CheckDelete = 0;
                DataTable dt = obj.GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ListItem i = new ListItem(item["DestinationCurrencyName"].ToString());
                        //drpcountry.Items.Add(i);
                        result += i + ", ";
                    }
                }
            }
            else
            {
                result = "Please select Source Currency";
            }

            return Json(result);

        }

        [HttpGet]
        [Authorize]
        public ActionResult RemittanceWallet(string MerchantUniqueId)
        {
            if (!string.IsNullOrEmpty(MerchantUniqueId))
            {
                ViewBag.MerchantUniqueId = MerchantUniqueId;
                AddRemittanceDestinationCurrencyList list = new AddRemittanceDestinationCurrencyList();
                List<SelectListItem> currencylist = new List<SelectListItem>();
                currencylist = CommonHelpers.GetSelectList_RemittanceDestinationCurrency();
                ViewBag.CurrencyList = currencylist;

                AddRemittanceUser usr = new AddRemittanceUser();
                usr.MerchantUniqueId = MerchantUniqueId;
                if (usr.GetRecord())
                {
                    ViewBag.FromCurrency = usr.FromCurrencyCode;
                    ViewBag.MerchantUniqueId = usr.MerchantUniqueId;
                    ViewBag.Type = @Enum.GetName(typeof(AddRemittanceUser.RemittanceType), Convert.ToInt64(usr.Type));
                    ViewBag.Image = usr.Image;
                    ViewBag.MerchantName = usr.ContactName;
                    ViewBag.MerchantContactNo = usr.ContactNo;
                    ViewBag.MerchantEmailId = usr.EmailID;
                }
                else
                {
                    ViewBag.FromCurrency = "";
                    ViewBag.MerchantUniqueId = "";
                    ViewBag.Type = "";
                }


                AddRemittanceSourceCurrencyList sourceCurrency = new AddRemittanceSourceCurrencyList();
                sourceCurrency.CheckActive = 1;
                sourceCurrency.CheckDelete = 0;
                DataTable dt = sourceCurrency.GetList();
                List<AddRemittanceSourceCurrencyList> ctlist = new List<AddRemittanceSourceCurrencyList>();
                ctlist = (List<AddRemittanceSourceCurrencyList>)CommonEntityConverter.DataTableToList<AddRemittanceSourceCurrencyList>(dt);
                ViewBag.SourceList = ctlist;

                AddRemittanceUserCurrencies WalletCurrency = new AddRemittanceUserCurrencies();
                WalletCurrency.CheckActive = 1;
                WalletCurrency.CheckDelete = 0;
                WalletCurrency.MerchantUniqueId = MerchantUniqueId;
                DataTable dtwallet = WalletCurrency.GetList();
                List<AddRemittanceUserCurrencies> walletlist = new List<AddRemittanceUserCurrencies>();
                walletlist = (List<AddRemittanceUserCurrencies>)CommonEntityConverter.DataTableToList<AddRemittanceUserCurrencies>(dtwallet);
                ViewBag.WalletList = walletlist;
            }
            else
            {
                ViewBag.MerchantUniqueId = "";
                return RedirectToAction("RemittanceList");
            }
            return View();
        }

        [Authorize]
        public JsonResult GetWalletList(string MerchantUniqueId)
        {
            string result = "";
            List<AddRemittanceUserCurrencies> ctlist = new List<AddRemittanceUserCurrencies>();
            if (!string.IsNullOrEmpty(MerchantUniqueId))
            {
                AddRemittanceUserCurrencies obj = new AddRemittanceUserCurrencies();
                obj.MerchantUniqueId = MerchantUniqueId;
                obj.CheckActive = 1;
                obj.CheckDelete = 0;
                DataTable dt = obj.GetList();
                if (dt != null && dt.Rows.Count > 0)
                {

                    ctlist = (List<AddRemittanceUserCurrencies>)CommonEntityConverter.DataTableToList<AddRemittanceUserCurrencies>(dt);
                    //result = ctlist;
                }
            }
            //else
            //{
            //    result = "Merchant Not Found";
            //}

            return Json(ctlist);

        }

        [HttpPost]
        [Authorize]
        public JsonResult AddRemittanceIp(AddRemittanceUser model)
        {
            string result = "";
            AddRemittanceUser outobject = new AddRemittanceUser();
            try
            {
                if (model.MerchantUniqueId != null && model.MerchantUniqueId != "0")
                {
                    outobject.MerchantUniqueId = model.MerchantUniqueId;
                    if (outobject.GetRecord())
                    {
                        AddRemittanceIPAddress remittanceiPAddress = new AddRemittanceIPAddress();
                        remittanceiPAddress.RemittanceMemberId = outobject.MerchantMemberId;
                        remittanceiPAddress.RemittanceUniqueId = outobject.MerchantUniqueId;
                        remittanceiPAddress.RemittanceName = outobject.ContactName;
                        remittanceiPAddress.RemittanceOrganization = outobject.OrganizationName;
                        remittanceiPAddress.IPAddress = model.MerchantIpAddress;
                        remittanceiPAddress.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        remittanceiPAddress.CreatedByName = Session["AdminUserName"].ToString();
                        remittanceiPAddress.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        remittanceiPAddress.UpdatedByName = Session["AdminUserName"].ToString();
                        remittanceiPAddress.IsActive = true;

                        if (remittanceiPAddress.Add())
                        {
                            Common.AddLogs("Add Remittance IPAddress for Remittance (" + outobject.Id + ")  by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);
                            return Json(remittanceiPAddress, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            Common.AddLogs("Failed to Add Remittance IPAddress ", true, (int)AddLog.LogType.Remittance);
                            return Json(null, JsonRequestBehavior.AllowGet);
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public ActionResult RemittanceIPList(string RemittanceId)
        {
            ViewBag.RemittanceId = string.IsNullOrEmpty(RemittanceId) ? "" : RemittanceId;
            return View();
        }
        [Authorize]
        public JsonResult GetRemittanceIpLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Member Id");
            columns.Add("Name");
            columns.Add("Organization");
            columns.Add("Ip Address");
            columns.Add("Created By");
            columns.Add("Updated By");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string RemittanceUniqueId = context.Request.Form["RemittanceId"];
            string Name = context.Request.Form["Name"];
            string Organization = context.Request.Form["Organization"];
            string IpAddress = context.Request.Form["IpAddress"];

            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddRemittanceIPAddress> trans = new List<AddRemittanceIPAddress>();
            GetRemittanceIpAddress w = new GetRemittanceIpAddress();
            w.RemittanceUniqueId = RemittanceUniqueId;
            w.RemittanceName = Name;
            w.RemittanceOrganization = Organization;
            w.IPAddress = IpAddress;
            w.CheckDelete = 0;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddRemittanceIPAddress
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         RemittanceUniqueId = row["RemittanceUniqueId"].ToString(),
                         RemittanceName = row["RemittanceName"].ToString(),
                         RemittanceOrganization = row["RemittanceOrganization"].ToString(),
                         IPAddress = row["IPAddress"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddRemittanceIPAddress>> objDataTableResponse = new DataTableResponse<List<AddRemittanceIPAddress>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        [Authorize]
        public JsonResult RemittanceIpBlockUnblock(AddRemittanceIPAddress model)
        {
            string result = "";
            AddRemittanceIPAddress outobject = new AddRemittanceIPAddress();
            GetRemittanceIpAddress inobject = new GetRemittanceIpAddress();
            inobject.Id = model.Id;
            try
            {
                AddRemittanceIPAddress res = RepCRUD<GetRemittanceIpAddress, AddRemittanceIPAddress>.GetRecord(Common.StoreProcedures.sp_RemittanceIPAddress_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (res.IsActive)
                    {
                        res.IsActive = false;
                    }
                    else
                    {
                        res.IsActive = true;
                    }
                    outobject = res;
                    if (outobject.Update())
                    {
                        ViewBag.SuccessMessage = "Successfully Updated Remittance Ip";
                        Common.AddLogs("Updated RemittanceIp of (RemittanceID: " + res.RemittanceUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Remittance);

                    }
                    else
                    {
                        ViewBag.Message = "Not Updated Merchant Ip";
                        Common.AddLogs("Not Updated Merchant Ip of(MerchantID: " + res.RemittanceUniqueId + " )", true, (int)AddLog.LogType.Remittance);

                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(outobject, JsonRequestBehavior.AllowGet);

        }

    }
}