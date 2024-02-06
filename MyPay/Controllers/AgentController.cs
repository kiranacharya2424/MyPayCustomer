using DocumentFormat.OpenXml.Drawing;
using MyPay.Models.Add;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using static MyPay.Models.Add.AddAgent;
using static MyPay.Models.Add.AddMerchant;
using Path = System.IO.Path;
using MyPay.Models.Get;
using MyPay.Repository;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Reflection.Emit;
using DocumentFormat.OpenXml.Wordprocessing;
using Org.BouncyCastle.Asn1.X509;
using ServiceStack;
using static MyPay.Models.Add.AddUser;
using DeviceId;
using MyPay.Models;
using Microsoft.IdentityModel.Tokens;
using MyPay.Models.Add.Agent;
using System.Runtime.Remoting;
using MyPay.Models.Response;
using Chilkat;
using System.Data.Entity.Infrastructure;
using System.Security.Cryptography;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using Org.BouncyCastle.Ocsp;

namespace MyPay.Controllers
{
    public class AgentController : Controller
    {

        #region Agent List, Add Agent 
        public ActionResult AgentList()
        {

            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult AddAgent(string AgentUniqueId)
        {
            AddAgent obj = new AddAgent();
            if (!string.IsNullOrEmpty(AgentUniqueId))
            {
                if (AgentUniqueId != null)
                {
                    List<AddAgent> trans = new List<AddAgent>();

                    GetAgent w = new GetAgent();
                    if (string.IsNullOrEmpty(AgentUniqueId))
                    {
                        return View();
                    }
                    w.AgentUniqueId = AgentUniqueId;
                    DataTable dt = w.GetAgentData("v", "", "", 0, 0, "");

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        obj.AgentUniqueId = AgentUniqueId;
                        obj.CreatedDateDt = row["CreatedDate"].ToString();
                        obj.UpdatedDateDt = row["UpdatedDate"].ToString();
                        obj.Username = row["UserName"].ToString();
                        obj.AgentCategoryId = row["AgentCategoryId"].ToString();
                        obj.IsPersonalorFirm = row["Individual"].ToString();
                        if (obj.IsPersonalorFirm == "Personal")
                        {
                            obj.FullName = row["FullName"].ToString();
                            obj.EmailID = row["Email"].ToString();
                            obj.ContactNumber = row["MobileNo"].ToString();
                            obj.Status = row["IsActive"].ToString();
                            obj.CreatedBy = row["CreatedBY"].ToString();
                            obj.DOB = Convert.ToDateTime(row["DOB"]).ToString("MM/dd/yyyy");
                            obj.GenderName = row["Gender"].ToString();
                            obj.MotherName = row["MotherName"].ToString();
                            obj.FatherName = row["FatherName"].ToString();
                            obj.GrandfatherName = row["GrandFatherName"].ToString();
                            obj.SpouseName = row["SpouseName"].ToString();
                            obj.MaritalStatus = row["MaritualStatus"].ToString();
                            obj.Nationality = row["Nationality"].ToString();
                            obj.CurrentAddress = row["CurrentAdress"].ToString();
                            obj.PermanentAddress = row["PermanentAdress"].ToString();
                            obj.CurrentDistrict = row["CurrentDistrict"].ToString();
                            obj.PermanentDistrict = row["PermanentDistrict"].ToString();
                            obj.CurrentMunicipality = row["CurrentMunicipality"].ToString();
                            obj.PermanentState = row["PermanentProvince"].ToString();
                            obj.CurrentState = row["CurrentProvince"].ToString();
                            obj.PermanentMunicipality = row["PermanentMunicipality"].ToString();
                            obj.Currentward = row["CurrentWard"].ToString();
                            obj.Permanentward = row["PermanentWard"].ToString();
                            obj.CurrentStreet = row["CurrentStreetName"].ToString();
                            obj.PermanentStreet = row["PermanentStreetName"].ToString();
                            obj.CurrentHouseNo = row["CurrentHouseNumber"].ToString();
                            obj.PermanentHouseNo = row["PermanentHouseNumber"].ToString();
                            obj.PanImage = row["PanDocumentImage"].ToString();
                            obj.GenderId = row["GenderId"].ToString();
                            obj.NationalityId = row["NationalityId"].ToString();
                            obj.CurrentDistrictId = row["CurrentDistrictId"].ToString();
                            obj.CurrentStateId = row["CurrentProvinceId"].ToString();
                            obj.CurrentMunicipalityId = row["CurrentMunicipalityId"].ToString();
                            obj.PermanentStateId = row["PermanentProvinceId"].ToString();
                            obj.PermanentDistrictId = row["PermanentDistrictId"].ToString();
                            obj.PermanentMunicipalityId = row["PermanentMunicipalityId"].ToString();
                            obj.OccupationId = row["OccupationId"].ToString();
                            obj.BankName = row["BankName"].ToString();
                            obj.AccountNumber = row["AccountNumber"].ToString();
                            obj.AccountName = row["AccountName"].ToString();
                            obj.BranchName = row["Branch"].ToString();
                            obj.PEPId = string.IsNullOrEmpty(row["PEPId"].ToString()) ? null : row["PEPId"].ToString();
                            obj.PEP = row["PEP"].ToString();
                            obj.BANK_CD = row["Bankcode"].ToString();
                            obj.AgentImage = row["agentimage"].ToString();
                            obj.NationalIdProofFront = row["FrontImage"].ToString();
                            obj.NationalIdProofBack = row["BackImage"].ToString();
                            obj.DocumentTypeId = row["DocumentTypeId"].ToString();
                            obj.DocumentNumber = row["DocumentNumber"].ToString();
                            obj.IssuePlace = row["IssuePlace"].ToString();
                            obj.Occupation = row["Occupation"].ToString();
                            obj.DocumentType = row["DocumentType"].ToString();
                            obj.IssueDate = Convert.ToDateTime(row["IssueDate"]).ToString("MM/dd/yyyy");
                            obj.ExpiryDate = !string.IsNullOrEmpty(row["ExpiryDate"].ToString()) ? Convert.ToDateTime(row["ExpiryDate"]).ToString("MM/dd/yyyy") : null;
                            obj.PanNumber = row["PanNumber"].ToString();
                            obj.MaritalStatusId = row["MaritualStatusId"].ToString();
                        }
                        else
                        {
                            obj.OrganizationFullName = row["FullName"].ToString();
                            obj.OrganizationContactNumber = row["MobileNo"].ToString();
                            obj.OrganizationEmail = row["Email"].ToString();
                            obj.OrganizationRegistrationDate = Convert.ToDateTime(row["OrganizationRegistrationDate"]).ToString("MM/dd/yyyy");
                            obj.OrganizationRegistrationNumber = row["OrganizationRegistrationNumber"].ToString();
                            obj.OrganizationPAN_VATNumber = row["OrganizationPANVATNumber"].ToString();
                            obj.Organizationward = row["officeWard"].ToString();
                            obj.OrganizationStreet = row["officeStreetName"].ToString();
                            obj.OrganizationHouseNo = row["officeHouseNumber"].ToString();
                            obj.OrganizationPAN_VATDocument = row["PanDocumentImage"].ToString();
                            obj.OrganizationRegistrationDocument = row["RegistrationDocument"].ToString();
                            obj.DirectorFullName = row["DirectorFullName"].ToString();
                            obj.DirectorEmail = row["DirectorEmail"].ToString();
                            obj.DirectorContactNumber = row["DirectorContactNumber"].ToString();
                            obj.Directorcitizenfrontimage = row["DirectorFrontImage"].ToString();
                            obj.Directorcitizenbackimage = row["DirectorBackImage"].ToString();
                            obj.Directorprofileimage = row["DirectorRecentImage"].ToString();
                            obj.OrganizationState = row["OfficeProvince"].ToString();
                            obj.OrganizationDistrict = row["OfficeDistrict"].ToString();
                            obj.OrganizationMunicipality = row["OfficeMunicipality"].ToString();
                            obj.OrganizationDistrictId = row["OfficeDistrictId"].ToString();
                            obj.OrganizationMunicipalityId = row["OfficeMunicipalityId"].ToString();
                            obj.OrganizationStateId = row["OfficeProvinceId"].ToString();
                            obj.OrganizationBankName = row["BankName"].ToString();
                            obj.OrganizationAccountNumber = row["AccountNumber"].ToString();
                            obj.OrganizationAccountName = row["AccountName"].ToString();
                            obj.OrganizationBranchName = row["Branch"].ToString();
                            obj.OrganizationBANK_CD = row["Bankcode"].ToString();
                            obj.OrganizationPAN_VATId = row["OrganizationPANVAT"].ToString() == "PAN" ? "1" : row["OrganizationPANVAT"].ToString() == "VAT" ? "2" : "0";
                        }
                    }

                }


            }

            List<SelectListItem> GenderType = CommonHelpers.GetSelectList_AgentGenderType(obj);
            ViewBag.GenderId = GenderType;
            AgentCategory agentCategory = new AgentCategory();
            //agentCategory.AgentCategoryId = model.AgentCategoryId;
            List<SelectListItem> AgentCategory = CommonHelpers.GetSelectList_AgentCategory(agentCategory);
            ViewBag.AgentCategoryId = AgentCategory;
            List<SelectListItem> DocumentType = CommonHelpers.GetSelectList_AgentDocumentType(obj);
            ViewBag.DocumentTypeId = DocumentType;
            List<SelectListItem> MaritalType = CommonHelpers.GetSelectList_AgentMaritalStatusType(obj);
            ViewBag.MaritalStatusId = MaritalType;
            List<SelectListItem> nationalityType = CommonHelpers.GetSelectList_AgentNationalityType(obj);
            ViewBag.nationalityId = nationalityType;
            List<SelectListItem> OccupationType = CommonHelpers.GetSelectList_AgentOccupation(obj);
            ViewBag.OccupationId = OccupationType;
            List<SelectListItem> permanentstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(obj.PermanentStateId));
            List<SelectListItem> permanentdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(obj.PermanentStateId), Convert.ToInt32(obj.PermanentDistrictId));
            List<SelectListItem> permanentmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(obj.PermanentDistrictId), Convert.ToInt32(obj.PermanentMunicipalityId));
            ViewBag.PermanentStateId = permanentstatelist;
            ViewBag.PermanentDistrictId = permanentdistrictlist;
            ViewBag.PermanentMunicipalityId = permanentmunicipalitylist;
            List<SelectListItem> currentstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(obj.CurrentStateId));
            List<SelectListItem> currentdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(obj.CurrentStateId), Convert.ToInt32(obj.CurrentDistrictId));
            List<SelectListItem> currentmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(obj.CurrentDistrictId), Convert.ToInt32(obj.CurrentMunicipalityId));
            ViewBag.CurrentStateId = currentstatelist;
            ViewBag.CurrentDistrictId = currentdistrictlist;
            ViewBag.CurrentMunicipalityId = currentmunicipalitylist;
            List<SelectListItem> PANVATType = CommonHelpers.GetSelectList_AgentOrganizationPANVAT(obj);
            ViewBag.OrganizationPAN_VATId = PANVATType;

            List<SelectListItem> orgstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(obj.OrganizationStateId));

            List<SelectListItem> orgdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(obj.OrganizationStateId), Convert.ToInt32(obj.OrganizationDistrictId));

            List<SelectListItem> orgmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(obj.OrganizationDistrictId), Convert.ToInt32(obj.OrganizationMunicipalityId));
            ViewBag.OrganizationStateId = orgstatelist;
            ViewBag.OrganizationDistrictId = orgdistrictlist;
            ViewBag.OrganizationMunicipalityId = orgmunicipalitylist;

            AddBankList outobjectbank = new AddBankList();
            GetBankList inobjectbank = new GetBankList();

            List<SelectListItem> resbank = CommonHelpers.GetSelectList_AgentBankList("", "banklist");
            ViewBag.BANK_CD = resbank;
            ViewBag.OrganizationBANK_CD = resbank;
            List<SelectListItem> PEP = CommonHelpers.GetSelectList_AgentPEP(obj);
            ViewBag.PEPId = PEP;
            //else
            //{
            //    List<SelectListItem> GenderType = CommonHelpers.GetSelectList_AgentGenderType(obj);
            //    ViewBag.GenderId = GenderType;
            //    List<SelectListItem> DocumentType = CommonHelpers.GetSelectList_AgentDocumentType(obj);
            //    ViewBag.DocumentTypeId = DocumentType;
            //    List<SelectListItem> MaritalType = CommonHelpers.GetSelectList_AgentMaritalStatusType(obj);
            //    ViewBag.MaritalStatusId = MaritalType;
            //    List<SelectListItem> nationalityType = CommonHelpers.GetSelectList_AgentNationalityType(obj);
            //    ViewBag.nationalityId = nationalityType;
            //    List<SelectListItem> OccupationType = CommonHelpers.GetSelectList_AgentOccupation(obj);
            //    ViewBag.OccupationId = OccupationType;
            //    List<SelectListItem> permanentstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(obj.PermanentStateId));

            //    List<SelectListItem> permanentdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(obj.PermanentStateId), Convert.ToInt32(obj.PermanentDistrictId));

            //    List<SelectListItem> permanentmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(obj.PermanentDistrictId), Convert.ToInt32(obj.PermanentMunicipalityId));
            //    ViewBag.PermanentStateId = permanentstatelist;
            //    ViewBag.PermanentDistrictId = permanentdistrictlist;
            //    ViewBag.PermanentMunicipalityId = permanentmunicipalitylist;

            //    List<SelectListItem> currentstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(obj.CurrentStateId));

            //    List<SelectListItem> currentdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(obj.CurrentStateId), Convert.ToInt32(obj.CurrentDistrictId));

            //    List<SelectListItem> currentmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(obj.CurrentDistrictId), Convert.ToInt32(obj.CurrentMunicipalityId));
            //    ViewBag.CurrentStateId = currentstatelist;
            //    ViewBag.CurrentDistrictId = currentdistrictlist;
            //    ViewBag.CurrentMunicipalityId = currentmunicipalitylist;
            //    List<SelectListItem> PANVATType = CommonHelpers.GetSelectList_AgentOrganizationPANVAT(obj);
            //    ViewBag.OrganizationPAN_VATId = PANVATType;

            //    List<SelectListItem> orgstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(obj.OrganizationStateId));

            //    List<SelectListItem> orgdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(obj.OrganizationStateId), Convert.ToInt32(obj.OrganizationDistrictId));

            //    List<SelectListItem> orgmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(obj.OrganizationDistrictId), Convert.ToInt32(obj.OrganizationMunicipalityId));
            //    ViewBag.OrganizationStateId = orgstatelist;
            //    ViewBag.OrganizationDistrictId = orgdistrictlist;
            //    ViewBag.OrganizationMunicipalityId = orgmunicipalitylist;

            //    AddBankList outobjectbank = new AddBankList();
            //    GetBankList inobjectbank = new GetBankList();

            //    List<SelectListItem> resbank = CommonHelpers.GetSelectList_AgentBankList("", "banklist");
            //    ViewBag.BANK_CD = resbank;
            //    ViewBag.OrganizationBANK_CD = resbank;
            //    List<SelectListItem> PEP = CommonHelpers.GetSelectList_AgentPEP(obj);
            //    ViewBag.PEPId = PEP;
            //}
            return View("AddAgent", obj);
        }
        [HttpPost]
        public ActionResult GetBranchNameFromBank(string Bankid)
        {
            List<SelectListItem> branchnames = CommonHelpers.GetSelectList_AgentBankList(Bankid, "branchname");
            var branchname = branchnames[0].Text;
            return Json(branchname);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddAgent(AddAgent model, HttpPostedFileBase NationalIdProofFrontFile,
                                     HttpPostedFileBase NationalIdProofBackFile,
                                      HttpPostedFileBase AgentImageFile, HttpPostedFileBase PanImageFile,
                                      HttpPostedFileBase OrganizationRegistrationDocumentFile,
                                      HttpPostedFileBase OrganizationPAN_VATDocumentFile,
                                      HttpPostedFileBase DirectorcitizenfrontimageFile,
                                      HttpPostedFileBase DirectorcitizenbackimageFile,
                                      HttpPostedFileBase DirectorprofileimageFile)
        {


            List<SelectListItem> GenderType = CommonHelpers.GetSelectList_AgentGenderType(model);
            ViewBag.GenderId = GenderType;
            AgentCategory agentCategory = new AgentCategory();
            agentCategory.AgentCategoryId = model.AgentCategoryId;
            List<SelectListItem> AgentCategory = CommonHelpers.GetSelectList_AgentCategory(agentCategory);
            ViewBag.AgentCategoryId = AgentCategory;
            List<SelectListItem> DocumentType = CommonHelpers.GetSelectList_AgentDocumentType(model);
            ViewBag.DocumentTypeId = DocumentType;
            List<SelectListItem> MaritalType = CommonHelpers.GetSelectList_AgentMaritalStatusType(model);
            ViewBag.MaritalStatusId = MaritalType;
            List<SelectListItem> nationalityType = CommonHelpers.GetSelectList_AgentNationalityType(model);
            ViewBag.nationalityId = nationalityType;
            List<SelectListItem> OccupationType = CommonHelpers.GetSelectList_AgentOccupation(model);
            ViewBag.OccupationId = OccupationType;
            List<SelectListItem> permanentstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(model.PermanentStateId));

            List<SelectListItem> permanentdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(model.PermanentStateId), Convert.ToInt32(model.PermanentDistrictId));

            List<SelectListItem> permanentmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(model.PermanentDistrictId), Convert.ToInt32(model.PermanentMunicipalityId));
            ViewBag.PermanentStateId = permanentstatelist;
            ViewBag.PermanentDistrictId = permanentdistrictlist;
            ViewBag.PermanentMunicipalityId = permanentmunicipalitylist;

            List<SelectListItem> currentstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(model.CurrentStateId));

            List<SelectListItem> currentdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(model.CurrentStateId), Convert.ToInt32(model.CurrentDistrictId));

            List<SelectListItem> currentmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(model.CurrentDistrictId), Convert.ToInt32(model.CurrentMunicipalityId));
            ViewBag.CurrentStateId = currentstatelist;
            ViewBag.CurrentDistrictId = currentdistrictlist;
            ViewBag.CurrentMunicipalityId = currentmunicipalitylist;
            List<SelectListItem> PANVATType = CommonHelpers.GetSelectList_AgentOrganizationPANVAT(model);
            ViewBag.OrganizationPAN_VATId = PANVATType;

            List<SelectListItem> orgstatelist = CommonHelpers.GetSelectList_State(Convert.ToInt32(model.OrganizationStateId));

            List<SelectListItem> orgdistrictlist = CommonHelpers.GetSelectList_District(Convert.ToInt32(model.OrganizationStateId), Convert.ToInt32(model.OrganizationDistrictId));

            List<SelectListItem> orgmunicipalitylist = CommonHelpers.GetSelectList_Municipality(Convert.ToInt32(model.OrganizationDistrictId), Convert.ToInt32(model.OrganizationMunicipalityId));
            ViewBag.OrganizationStateId = orgstatelist;
            ViewBag.OrganizationDistrictId = orgdistrictlist;
            ViewBag.OrganizationMunicipalityId = orgmunicipalitylist;

            AddBankList outobjectbank = new AddBankList();
            GetBankList inobjectbank = new GetBankList();

            List<SelectListItem> resbank = CommonHelpers.GetSelectList_AgentBankList("", "banklist");
            ViewBag.BANK_CD = resbank;
            ViewBag.OrganizationBANK_CD = resbank;
            List<SelectListItem> PEP = CommonHelpers.GetSelectList_AgentPEP(model);
            ViewBag.PEPId = PEP;
            if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Message = "Please enter password";
                return View(model);
            }
            else if (model.Password.Length < 8)
            {
                ViewBag.Message = "Minimum Password length should be 8 characters.";
                return View(model);
            }
            else if (model.Password != "")
            {
                if (model.Password.IndexOf(":") >= 0)
                {
                    ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                    return View(model);
                }
                else
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(model.Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                        return View(model);
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.EmailID) && !Regex.Match(model.EmailID, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                ViewBag.Message = "Please enter valid email";
                return View(model);
            }
            //string RandomUserId = Common.GetNewAdminLoginId().ToString();
            //model.userId = "user" + RandomUserId;
            model.Password = Common.EncryptString(model.Password);
             model.deviceId = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();
            if (model.IsPersonalorFirm == "Personal")
            {
                ModelState.Remove("OrganizationFullName");
                ModelState.Remove("OrganizationContactNumber");
                ModelState.Remove("OrganizationEmail");
                ModelState.Remove("OrganizationRegistrationDate");
                ModelState.Remove("OrganizationRegistrationNumber");
                ModelState.Remove("OrganizationPAN_VATNumber");
                ModelState.Remove("OrganizationPAN_VATId");
                ModelState.Remove("DirectorFullName");
                ModelState.Remove("DirectorEmail");
                ModelState.Remove("DirectorContactNumber");
                ModelState.Remove("Directorcitizenfrontimage");
                ModelState.Remove("Directorcitizenbackimage");
                ModelState.Remove("Directorprofileimage");
                ModelState.Remove("OrganizationDistrictId");
                ModelState.Remove("OrganizationMunicipalityId");
                ModelState.Remove("OrganizationStateId");
                ModelState.Remove("Organizationward");
                ModelState.Remove("OrganizationPAN_VATDocument");
                ModelState.Remove("OrganizationRegistrationDocument");

                if (model.DocumentTypeId == "4")
                {
                    ModelState.Remove("ExpiryDate");
                    if (NationalIdProofBackFile != null)
                    {
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(NationalIdProofBackFile.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                        NationalIdProofBackFile.SaveAs(filePath);
                        model.NationalIdProofBack = fileName;
                        ModelState.Remove("NationalIdProofBack");
                    }
                }
                else
                {
                    ModelState.Remove("NationalIdProofBack");
                }
                if (NationalIdProofFrontFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(NationalIdProofFrontFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    NationalIdProofFrontFile.SaveAs(filePath);
                    model.NationalIdProofFront = fileName;
                    ModelState.Remove("NationalIdProofFront");
                }

                if (AgentImageFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(AgentImageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    AgentImageFile.SaveAs(filePath);
                    model.AgentImage = fileName;
                    ModelState.Remove("AgentImage");
                }
                if (PanImageFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(PanImageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    PanImageFile.SaveAs(filePath);
                    model.PanImage = fileName;
                    ModelState.Remove("PanImage");
                }

                model.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                model.IsApprovedByAdmin = true;
                model.IpAddress = Common.GetUserIP();
                model.LastLoginAttempt = System.DateTime.UtcNow;
                model.JwtToken = new CommonHelpers().GetJWToken(model.ContactNumber);
                model.TokenUpdatedTime = DateTime.UtcNow.AddMinutes(5);
                model.MemberId = GetNewId();
                model.UserId = "UD" + model.MemberId.ToString();

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(model.AgentUniqueId))
                        {
                            model.flag = "i";
                            CommonDBResonse result = model.Add();
                            if (result.code == "0")
                            {
                                TempData["Message"] = result.Message;
                                ViewBag.SuccessMessage = result.Message;
                                return RedirectToAction("AddAgent");
                            }
                            else
                            {
                                ViewBag.Message = result.Message;

                            }
                        }
                        else
                        {
                            model.flag = "u";
                            CommonDBResonse result = model.update();
                            if (result.code == "0")
                            {
                                TempData["Message"] = result.Message;
                                ViewBag.SuccessMessage = result.Message;
                                return RedirectToAction("AddAgent");
                            }
                            else
                            {
                                ViewBag.Message = result.Message;

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Failed to create agent.";
                    }

                }
                else
                {
                    ViewBag.Message = "Required field are missing.";
                }

            }
            else
            {

                ModelState.Remove("FullName");
                ModelState.Remove("ContactNumber");
                ModelState.Remove("EmailID");
                ModelState.Remove("DOB");
                ModelState.Remove("GenderId");
                ModelState.Remove("MaritalStatusId");
                ModelState.Remove("GrandfatherName");
                ModelState.Remove("FatherName");
                ModelState.Remove("MotherName");
                ModelState.Remove("OccupationId");
                ModelState.Remove("NationalityId");
                ModelState.Remove("PermanentAddress");
                ModelState.Remove("CurrentAddress");
                ModelState.Remove("PermanentDistrictId");
                ModelState.Remove("CurrentDistrictId");
                ModelState.Remove("PermanentMunicipalityId");
                ModelState.Remove("CurrentMunicipalityId");
                ModelState.Remove("Permanentward");
                ModelState.Remove("Currentward");
                ModelState.Remove("PermanentStateId");
                ModelState.Remove("CurrentStateId");
                ModelState.Remove("DocumentTypeId");
                ModelState.Remove("DocumentNumber");
                ModelState.Remove("ExpiryDate");
                ModelState.Remove("IssueDate");
                ModelState.Remove("IssuePlace");
                ModelState.Remove("NationalIdProofFront");
                ModelState.Remove("NationalIdProofBack");
                ModelState.Remove("AgentImage");
                ModelState.Remove("PanNumber");
                ModelState.Remove("PanImage");
                if (OrganizationRegistrationDocumentFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(OrganizationRegistrationDocumentFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    OrganizationRegistrationDocumentFile.SaveAs(filePath);
                    model.OrganizationRegistrationDocument = fileName;
                }
                if (OrganizationPAN_VATDocumentFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(OrganizationPAN_VATDocumentFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    OrganizationPAN_VATDocumentFile.SaveAs(filePath);
                    model.OrganizationPAN_VATDocument = fileName;
                }
                if (DirectorcitizenfrontimageFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(DirectorcitizenfrontimageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    DirectorcitizenfrontimageFile.SaveAs(filePath);
                    model.Directorcitizenfrontimage = fileName;
                }
                if (DirectorcitizenbackimageFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(DirectorcitizenbackimageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    DirectorcitizenbackimageFile.SaveAs(filePath);
                    model.Directorcitizenbackimage = fileName;
                }
                if (DirectorprofileimageFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(DirectorprofileimageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/AgentImages/") + fileName);
                    DirectorprofileimageFile.SaveAs(filePath);
                    model.Directorprofileimage = fileName;
                }

                if (ModelState.IsValid)
                {

                    try
                    {
                        if (string.IsNullOrEmpty(model.AgentUniqueId))
                        {
                            model.flag = "i";
                            CommonDBResonse result = model.Add();
                            if (result.code == "0")
                            {
                                TempData["Message"] = result.Message;
                                ViewBag.SuccessMessage = result.Message;
                                return RedirectToAction("AddAgent");
                            }
                            else
                            {

                                ViewBag.Message = result.Message;

                            }
                        }
                        else
                        {
                            model.flag = "u";
                            CommonDBResonse result = model.update();
                            if (result.code == "0")
                            {
                                TempData["Message"] = result.Message;
                                ViewBag.SuccessMessage = result.Message;
                                return RedirectToAction("AddAgent");
                            }
                            else
                            {

                                ViewBag.Message = result.Message;

                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Failed to create agent";
                    }
                }
                else
                {
                    ViewBag.Message = "Required field are missing.";
                }
            }


            return View("AddAgent", model);
        }



        [Authorize]
        public JsonResult GetAgentLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Full Name");
            columns.Add("Contact Number");
            columns.Add("Category");
            columns.Add("Email");
            columns.Add("Available Balance");
            columns.Add("Commission Balance");
            columns.Add("Available Coins");
            columns.Add("No of Agent");
            columns.Add("No of Users");
            columns.Add("Status");
            columns.Add("Created By");

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
            string FullName = context.Request.Form["FullName"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Email = context.Request.Form["Email"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddAgent> trans = new List<AddAgent>();

            GetAgent w = new GetAgent();
            w.ContactNumber = ContactNumber;
            w.FullName = FullName;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.EmailID = Email;

            DataTable dt = w.GetAgentData("s", sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddAgent
                     {
                         AgentUniqueId = row["ID"].ToString(),
                         Sno = row["Sno"].ToString(),
                         CreatedDateDt = row["CreatedDatedt"].ToString(),
                         UpdatedDateDt = row["UpdatedDatedt"].ToString(),
                         FullName = row["FullName"].ToString(),
                         EmailID = row["Email"].ToString(),
                         ContactNumber = row["MobileNo"].ToString(),
                         AvailableBalance = row["TotalAmount"].ToString(),
                         CommissionBalance = row["CommissionBalance"].ToString(),
                         AvailableCoins = row["AvailableCoins"].ToString(),
                         NoOfAgent = dt.Rows[0]["NoOfAgent"].ToString(),
                         NoOfUser = row["NoOfUser"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         Status = row["IsActive"].ToString(),
                         CreatedBy = row["CreatedBY"].ToString(),
                         TotalRewardPoints = row["TotalRewardPoints"].ToString(),
                         AgentCategory= row["Category"].ToString(),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["totalnumber"]) : 0;

            DataTableResponse<List<AddAgent>> objDataTableResponse = new DataTableResponse<List<AddAgent>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        //[HttpGet]
        //[Authorize]
        //public ActionResult AddAgent(string AgentUniqueId)
        //{

        //}

        [HttpPost]
        [Authorize]
        public JsonResult AgentBlockUnblock(AddAgent model)
        {
            AddAgent outobject = new AddAgent();
            GetAgent w = new GetAgent();
            if (!string.IsNullOrEmpty(model.AgentUniqueId))
            {
                w.AgentUniqueId = model.AgentUniqueId;
            }
            List<AddAgent> trans = new List<AddAgent>();
            DataTable dt = w.GetAgentData("checkdata", "", "", 0, 0, "");
            trans = (from DataRow row in dt.Rows

                     select new AddAgent
                     {
                         AgentUniqueId = row["ID"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         Status = row["IsActive"].ToString()

                     }).ToList();

            if (trans.Count > 0)
            {
                foreach (AddAgent agent in trans)
                {
                    model.IsActive = Convert.ToBoolean(agent.IsActive);
                }
                if (model.IsActive)
                {
                    w.IsActive = false;
                }
                else
                {
                    w.IsActive = true;
                }

                DataTable dt1 = w.UpdateData();
                trans = (from DataRow row in dt1.Rows

                         select new AddAgent
                         {
                             AgentUniqueId = row["Value"].ToString()
                         }).ToList(); ;
                if (trans.Count > 0)
                {
                    ViewBag.SuccessMessage = "Successfully Update Agent";
                    Common.AddLogs("Updated Agent of (AgentID: " + model.AgentUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Agent);

                }
                else
                {
                    ViewBag.Message = "Not Updated Agent";
                    Common.AddLogs("Not Updated (AgentID: " + model.AgentUniqueId + " )", true, (int)AddLog.LogType.Agent);

                }

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //[Authorize]
        //public ActionResult DownloadAgentFileName()
        //{
        //    var errorMessage = "";
        //    AddExportData outobject_file = new AddExportData();
        //    GetExportData inobject_file = new GetExportData();
        //    inobject_file.Type = (int)AddExportData.ExportType.Merchant;

        //    AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
        //    string fullPath = Path.Combine(Server.MapPath("~/ExportData/Agent"), res_file.FilePath);
        //    if (System.IO.File.Exists(fullPath))
        //    {
        //        return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
        //    }
        //    else
        //    {
        //        errorMessage = "No file found";
        //        return Json(new { fileName = "", errorMessage = errorMessage });
        //    }
        //}

        [HttpGet]
        [Authorize]
        public ActionResult CreditDebit(string AgentUniqueId = "0")
        {
            AddAgent model = new AddAgent();
            if (string.IsNullOrEmpty(AgentUniqueId) || AgentUniqueId == "0")
            {
                return RedirectToAction("Index");
            }
            AddAgent outobject = new AddAgent();
            GetAgent inobject = new GetAgent();
            inobject.AgentUniqueId = AgentUniqueId;

            DataTable dt = inobject.GetAgentData("v", "", "", 0, 0, "");
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                model.AgentUniqueId = row["AgentId"].ToString(); ;
                model.AgentId = AgentUniqueId;
                model.CreatedDateDt = row["CreatedDate"].ToString();
                model.FullName = row["FullName"].ToString();
                model.EmailID = row["Email"].ToString();
                model.ContactNumber = row["MobileNo"].ToString();
                model.IsActive = Convert.ToBoolean(row["IsActive"]);
                model.Status = row["IsActive"].ToString();
            }

            if (!string.IsNullOrEmpty(AgentUniqueId) && (model == null || model.AgentId == "0" || AgentUniqueId == ""))
            {
                return RedirectToAction("AgentList");
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreditDebit(string AgentUniqueId, string TransactionAmount, string Type, string TransactionType, string AdminRemarksCD)
        {

            AddAgent model = new AddAgent();
            try
            {
                AddAgent outobject = new AddAgent();
                GetAgent inobject = new GetAgent();
                inobject.AgentUniqueId = AgentUniqueId;
                DataTable dt = inobject.GetAgentData("Getuser", "", "", 0, 0, "");
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    model.AgentUniqueId = row["AgentId"].ToString(); ;
                    model.AgentId = AgentUniqueId;
                    model.CreatedDateDt = row["CreatedDate"].ToString();
                    model.FullName = row["FullName"].ToString();
                    model.EmailID = row["Email"].ToString();
                    model.ContactNumber = row["MobileNo"].ToString();
                    model.IsActive = Convert.ToBoolean(row["IsActive"]);
                    model.Status = row["IsActive"].ToString();
                    model.MemberId = Convert.ToInt64(row["MemberId"].ToString());
                }

                if (model != null && model.AgentId != "0" && !string.IsNullOrEmpty(AgentUniqueId))
                {
                    string UserMessage = string.Empty;
                    string Referenceno = new CommonHelpers().GenerateUniqueId();
                    string msg = "";
                    if (Type == "1")
                    {
                        msg = RepTransactions.WalletUpdateFromAdmin(model.MemberId, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD, "");
                    }
                    if (msg.ToString().ToLower() == "success")
                    {
                        ViewBag.SuccessMessage = UserMessage;
                    }
                    else
                    {
                        ViewBag.Message = msg;
                        Common.AddLogs("Not Updated Merchanat user Wallet", true, (int)AddLog.LogType.Merchant);
                    }
                }
                else
                {
                    ViewBag.Message = "Merchant user Not Found";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }
            return View(model);
        }
        private static Int64 GetNewId()
        {
            Int64 Id = 0;
            MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
            string Result = commonHelpers.GetScalarValueWithValue("SELECT TOP 1 max(MemberId) FROM Users with(nolock)");
            if (!string.IsNullOrEmpty(Result) && Result != "0")
            {
                Id = Convert.ToInt64(Result) + 1;
            }
            else
            {
                Id = MyPay.Models.Common.Common.StartingNumber;
            }
            return Id;
        }


        #endregion

        #region Category List , Add Category
        public ActionResult AgentCategory()
        {
            AgentCategory w = new AgentCategory();
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            DataTable dt = w.GetAgentCategoryData("dc", "", "", 1, 1, "");
            List<AgentCategory> trans = new List<AgentCategory>();
            if (dt.Rows.Count > 0)
            {
                trans = (from DataRow row in dt.Rows

                         select new AgentCategory
                         {
                             Category = row["Category"].ToString(),
                             AgentCategoryId = row["CategoryId"].ToString(),

                         }).ToList();
            }

            List<SelectListItem> CounterList = (from p in trans.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.Category,
                                                    Value = p.AgentCategoryId.ToString()
                                                }).ToList();
            CategoryList = CommonHelpers.CreateDropdown("0", CounterList, "Select");
            ViewBag.Category = CategoryList;
            List<SelectListItem> StatusType = CommonHelpers.GetSelectList_AgentCategoryStatus(w);
            ViewBag.Status = StatusType;
            return View(w);
        }

        [Authorize]
        public JsonResult GetAgentCategory()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Category Name");
            columns.Add("No. of assigned Agent");
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
            string Category = context.Request.Form["Category"];
            string Status = context.Request.Form["Status"];
            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AgentCategory> trans = new List<AgentCategory>();

            AgentCategory w = new AgentCategory();
            w.Category = Category;
            w.StatusId = Status;
            w.StartDate = fromdate;
            w.EndDate = todate;

            DataTable dt = w.GetAgentCategoryData("s", sortColumn, sortDirection, OffsetValue, PagingSize, searchby);
            trans = (from DataRow row in dt.Rows

                     select new AgentCategory
                     {
                         Status = row["Status"].ToString(),
                         Sno = row["Sno"].ToString(),
                         CreatedDateDt = row["CreatedDatedt"].ToString(),
                         UpdatedDateDt = row["UpdatedDatedt"].ToString(),
                         Category = row["Category"].ToString(),
                         AgentCategoryId = row["CategoryId"].ToString(),
                         NoOfAssignedAgent = dt.Rows[0]["NoOfAssignedAgent"].ToString(),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["totalnumber"]) : 0;

            DataTableResponse<List<AgentCategory>> objDataTableResponse = new DataTableResponse<List<AgentCategory>>
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
        public ActionResult AddAgentCategory()
        {
            AgentCategory obj = new AgentCategory();
            List<SelectListItem> StatusType = CommonHelpers.GetSelectList_AgentCategoryStatus(obj);
            ViewBag.StatusId = StatusType;
            return View("AddAgentCategory", obj);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddAgentCategory(AgentCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Convert.ToInt32(model.AgentCategoryId)>0)
                    {


                    }
                    else
                    {
                        model.flag = "i";
                        CommonDBResonse result = model.Add();
                        if (result.code=="0")
                        {
                            TempData["SuccessMessagecategory"] = result.Message;
                        }
                        else
                        {
                            TempData["Messagecategory"] = result.Message; 
                        }
                        return RedirectToAction("AgentCategory");
                    }
                  

                }
                catch (Exception ex)
                {
                    //ViewBag.Message = result.Message;

                }


            }
            //ViewBag.Message = result.Message;
            return View("AddAgentCategory", model);

        }


        [HttpPost]
        [Authorize]
        public JsonResult DeleteAgentCategory(AgentCategory model)
        {
            AgentCategory outobject = new AgentCategory();
            GetAgent w = new GetAgent();
            if (!string.IsNullOrEmpty(model.AgentCategoryId))
            {
                w.AgentUniqueId = model.AgentCategoryId;
            }

            model.flag = "d";
            CommonDBResonse result = model.Add();
            //if (result.code == "0")
            //{
            //    //TempData["SuccessMessagecategory"] = result.Message;
            //}
            //else
            //{
            //   // TempData["Messagecategory"] = result.Message;
            //}
            var message = string.Empty;
            var Id = string.Empty;
            return Json(new { Id = result.code, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region AgentCommission



        //[HttpPost]
        //[Authorize]
        //public JsonResult ViewAgentCategory(AgentCategory model)
        //{
        //    AgentCategory outobject = new AgentCategory();
        //    GetAgent w = new GetAgent();
        //    if (!string.IsNullOrEmpty(model.AgentCategoryId))
        //    {
        //        w.AgentUniqueId = model.AgentCategoryId;
        //    }

        //    model.flag = "d";
        //    CommonDBResonse result = model.Add();
        //    //if (result.code == "0")
        //    //{
        //    //    //TempData["SuccessMessagecategory"] = result.Message;
        //    //}
        //    //else
        //    //{
        //    //   // TempData["Messagecategory"] = result.Message;
        //    //}
        //    var message = string.Empty;
        //    var Id = string.Empty;
        //    return Json(new { Id = result.code, message = result.Message }, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        [Authorize]
        public ActionResult ViewAgentCategory(AgentCategory model)
        {
            AgentCommission obj = new AgentCommission();
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            var ss = model.AgentCategoryId.Split(',');
            model.AgentCategoryId = ss[0];
            model.Category = ss[1];
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            obj.AgentCategoryId = model.AgentCategoryId;
            obj.AgentCategory = model.Category;
            // inobject.RoleId = (int)AddUser.UserRoles.User;
            List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Category ---";
                objDefault.Selected = true;
                objProviderServiceCategoryList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objProviderServiceCategoryList.Count; i++)
            {

                SelectListItem objItem = new SelectListItem();
                objItem.Value = objProviderServiceCategoryList[i].Id.ToString();
                objItem.Text = objProviderServiceCategoryList[i].ProviderCategoryName.ToString();
                objProviderServiceCategoryList_SelectList.Add(objItem);


            }
            ViewBag.ProviderType = objProviderServiceCategoryList_SelectList;

            // **********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN *********** //

            {
                List<SelectListItem> objProviderURLList = new List<SelectListItem>();
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Service ---";
                objDefault.Selected = true;
                objProviderURLList.Add(objDefault);
                ViewBag.SERVICEId = objProviderURLList;
            }
            return View(obj);
        }


        [HttpGet]
        [Authorize]
        public ActionResult EditAgentCategory(AgentCategory model)
        {
            AgentCommission obj = new AgentCommission();
            AddProviderServiceCategoryList outobj = new AddProviderServiceCategoryList();
            var ss= model.AgentCategoryId.Split(',');   
            model.AgentCategoryId = ss[0];  
            model.Category = ss[1];   
            GetProviderServiceCategoryList inobject = new GetProviderServiceCategoryList();
            obj.AgentCategoryId = model.AgentCategoryId;
            obj.AgentCategory = model.Category;
            // inobject.RoleId = (int)AddUser.UserRoles.User;
            List<AddProviderServiceCategoryList> objProviderServiceCategoryList = RepCRUD<GetProviderServiceCategoryList, AddProviderServiceCategoryList>.GetRecordList(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get, inobject, outobj);

            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Category ---";
                objDefault.Selected = true;
                objProviderServiceCategoryList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objProviderServiceCategoryList.Count; i++)
            {

                SelectListItem objItem = new SelectListItem();
                objItem.Value = objProviderServiceCategoryList[i].Id.ToString();
                objItem.Text = objProviderServiceCategoryList[i].ProviderCategoryName.ToString();
                objProviderServiceCategoryList_SelectList.Add(objItem);


            }
            ViewBag.ProviderType = objProviderServiceCategoryList_SelectList;

            // **********  ADD DEFAULT SERVICE ID  VALUE IN DROPDOWN *********** //

            {
                List<SelectListItem> objProviderURLList = new List<SelectListItem>();
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "--- Select Service ---";
                objDefault.Selected = true;
                objProviderURLList.Add(objDefault);
                ViewBag.SERVICEId = objProviderURLList;
            }
            return View(obj);
        }
        public JsonResult GetAgentCommissionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Id");
            columns.Add("Sno");
            columns.Add("ServiceId");
            columns.Add("ServiceName");
            columns.Add("MinimumAmount");
            columns.Add("MaximumAmount");
            //columns.Add("FixedCommission");
            columns.Add("PercentageCommission");
            columns.Add("PercentageRewardPoints");
            columns.Add("PercentageRewardPointsDebit");
            columns.Add("MinimumAllowed");
            columns.Add("MaximumAllowed");
            columns.Add("ServiceCharge");
            columns.Add("MinimumAllowedSC");
            columns.Add("MaximumAllowedSC");
            columns.Add("ChildTxnRate");
            columns.Add("ChildTxnMinAmt");
            columns.Add("ChildTxnMaxAmt");
            columns.Add("MonthlyMinAmt");
            columns.Add("MonthlyMaxAmt");
            columns.Add("MonthlyBonus");

            //columns.Add("GenderTypeName");
            //columns.Add("KycTypeName");
            columns.Add("FromDate");
            columns.Add("ToDate");
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
            string Id = context.Request.Form["Id"];
            string MinimumAmount = context.Request.Form["MinimumAmount"];
            string MaximumAmount = context.Request.Form["MaximumAmount"];
            string FixedCommission = context.Request.Form["FixedCommission"];
            string PercentageCommission = context.Request.Form["PercentageCommission"];
            string PercentageRewardPoints = context.Request.Form["PercentageRewardPoints"];
            string PercentageRewardPointsDebit = context.Request.Form["PercentageRewardPointsDebit"];
            string MinimumAllowed = context.Request.Form["MinimumAllowed"];
            string MaximumAllowed = context.Request.Form["MaximumAllowed"];
            string ServiceCharge = context.Request.Form["ServiceCharge"];
            string MinimumAllowedSC = context.Request.Form["MinimumAllowedSC"];
            string MaximumAllowedSC = context.Request.Form["MaximumAllowedSC"];
            var ss = context.Request.Form["AgentCategoryId"].Split(',');
            string AgentCategoryId = ss[0];
            string AgentCategory = context.Request.Form["AgentCategory"];
            string ProviderType = context.Request.Form["ProviderType"];
            //string GenderType = context.Request.Form["GenderType"];
            //string KycType = context.Request.Form["KycType"];
            string FromDate = context.Request.Form["FromDate"];
            string ToDate = context.Request.Form["ToDate"];
            string ServiceId = context.Request.Form["ServiceId"];
            string ScheduleStatus = context.Request.Form["ScheduleStatus"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetUser inobject_user = new GetUser();
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }

            AgentCommission w = new AgentCommission();
            w.Id =Convert.ToInt64( Id);
            w.AgentCategoryId = AgentCategoryId;
            w.ProviderType = ProviderType;
           // w.ButtonType= context.Request.Form["ButtonType"];
            w.ServiceId = ServiceId;
            w.MinimumAmount = Convert.ToDecimal(MinimumAmount);
            w.MaximumAmount = Convert.ToDecimal(MaximumAmount);
            w.FixedCommission = Convert.ToDecimal(FixedCommission);
            w.PercentageCommission = Convert.ToDecimal(PercentageCommission);
            w.PercentageRewardPoints = Convert.ToDecimal(PercentageRewardPoints);
            w.PercentageRewardPointsDebit = Convert.ToDecimal(PercentageRewardPointsDebit);
            //w.GenderType = Convert.ToInt32(GenderType);
            w.MinimumAllowed = Convert.ToDecimal(MinimumAllowed);
            w.MaximumAllowed = Convert.ToDecimal(MaximumAllowed);
            w.ServiceCharge = Convert.ToDecimal(ServiceCharge);
            w.MinimumAllowedSC = Convert.ToDecimal(MinimumAllowedSC);
            w.MaximumAllowedSC = Convert.ToDecimal(MaximumAllowedSC);
            //w.KycType = Convert.ToInt32(KycType);
            w.FromDate =Convert.ToDateTime( FromDate);
            w.ToDate = Convert.ToDateTime(ToDate);
            w.CheckDelete = 0;
            if (ScheduleStatus != "0")
            {
                if (ScheduleStatus == "1")
                {
                    w.Running = "Running";
                }
                else if (ScheduleStatus == "2")
                {
                    w.Scheduled = "Scheduled";
                }
                else if (ScheduleStatus == "3")
                {
                    w.Expired = "Expired";
                }
            }
            w.flag="s";
            DataTable dt = w.GetAgentCommissionData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);
            List<AgentCommission> objCommission = (List<AgentCommission>)CommonEntityConverter.DataTableToList<AgentCommission>(dt);
            List<SelectListItem> list = CommonHelpers.GetSelectList_Providerchange(ProviderType);
            for (int i = 0; i < objCommission.Count; i++)
            {
                objCommission[i].ServiceName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objCommission[i].ServiceId)).ToString().Replace("khalti_", " ").Replace("_", " ");
                objCommission[i].GenderTypeName = ((AddCommission.GenderTypes)Convert.ToInt32(objCommission[i].GenderType)).ToString();
                objCommission[i].KycTypeName = ((AddCommission.KycTypes)Convert.ToInt32(objCommission[i].KycType)).ToString();
                objCommission[i].ProviderservicceList = list;
                objCommission[i].Id = objCommission[i].Id;
                objCommission[i].Sno = (i + 1).ToString();
            }
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;
            
            
            DataTableResponse<List<AgentCommission>> objDataTableResponse = new DataTableResponse<List<AgentCommission>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objCommission
                
            };
          
            return Json(objDataTableResponse);

        }



        [HttpPost]
        [Authorize]
        public JsonResult AddNewAgentCommissionDataRow(AgentCommission req_ObjCommission_req)
        {

            var context = HttpContext;
            int GenderType = Convert.ToInt32(req_ObjCommission_req.GenderType);
            int KycType = Convert.ToInt32(req_ObjCommission_req.KycType);
            string ServiceId = req_ObjCommission_req.ServiceId;
            string ProviderType = req_ObjCommission_req.ProviderType;
            var ss = req_ObjCommission_req.AgentCategoryId.Split(',');
            req_ObjCommission_req.AgentCategoryId = ss[0];
            req_ObjCommission_req.AgentCategory = ss[1];
            AgentCommission ObjCommission = new AgentCommission();
            //ObjCommission.ServiceId = Convert.ToInt32(ProviderType);
            //ObjCommission.ServiceId = Convert.ToInt32(ServiceId);
            //ObjCommission.GenderType = GenderType;
            //ObjCommission.KycType = KycType;
            //ObjCommission.IsActive = true;
            //ObjCommission.IsDeleted = false;
            req_ObjCommission_req.flag = "i";
            req_ObjCommission_req.FromDate = System.DateTime.UtcNow;
            req_ObjCommission_req.ToDate = System.DateTime.UtcNow;
            req_ObjCommission_req.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            req_ObjCommission_req.CreatedByName = Session["AdminUserName"].ToString();
            CommonDBResonse result = req_ObjCommission_req.AddAgentCommission();
            return Json(ObjCommission, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [Authorize]
        public JsonResult AgentCommissionUpdateCall(AgentCommission req_ObjCommission_req)
        {

            AgentCommission outobject = new AgentCommission();
            var ss = req_ObjCommission_req.AgentCategoryId.Split(',');
            req_ObjCommission_req.AgentCategoryId = ss[0];
            string fromdate =Convert.ToDateTime( req_ObjCommission_req.FromDate).ToString("yyyy-MM-dd");
            string todate = Convert.ToDateTime(req_ObjCommission_req.ToDate).ToString("yyyy-MM-dd");
            float CommissionSlabCount = outobject.CountAgentCommissionCheck(req_ObjCommission_req.ServiceId.ToString(),req_ObjCommission_req.AgentCategoryId, req_ObjCommission_req.ProviderType,req_ObjCommission_req.GenderType, req_ObjCommission_req.KycType, req_ObjCommission_req.MinimumAmount, req_ObjCommission_req.MaximumAmount, req_ObjCommission_req.Id, fromdate, todate);
            if (CommissionSlabCount > 0)
            {
                AgentCommission ObjCommission = new AgentCommission();
                return Json(ObjCommission, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AgentCommission outCommission = new AgentCommission();
                AgentCommission inobjectCommission = new AgentCommission();
                inobjectCommission.Id = Convert.ToInt64(req_ObjCommission_req.Id);


                //AgentCommission resCommission = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobjectCommission, outCommission);
                //inobjectCommission.flag = "gc";
               
                AgentCommission resCommission= new AgentCommission();
                resCommission.ServiceId = req_ObjCommission_req.ServiceId;
                resCommission.GenderType = req_ObjCommission_req.GenderType;
                resCommission.KycType = req_ObjCommission_req.KycType;
                resCommission.MinimumAmount = req_ObjCommission_req.MinimumAmount;
                resCommission.MaximumAmount = req_ObjCommission_req.MaximumAmount;
                resCommission.FixedCommission = req_ObjCommission_req.FixedCommission;
                resCommission.PercentageCommission = req_ObjCommission_req.PercentageCommission;
                resCommission.PercentageRewardPoints = req_ObjCommission_req.PercentageRewardPoints;
                resCommission.PercentageRewardPointsDebit = req_ObjCommission_req.PercentageRewardPointsDebit;
                resCommission.MinimumAllowed = req_ObjCommission_req.MinimumAllowed;
                resCommission.MaximumAllowed = req_ObjCommission_req.MaximumAllowed;
                resCommission.MinimumAllowedSC = req_ObjCommission_req.MinimumAllowedSC;
                resCommission.MaximumAllowedSC = req_ObjCommission_req.MaximumAllowedSC;
                resCommission.ServiceCharge = req_ObjCommission_req.ServiceCharge;
                resCommission.ChildTxnMaxAmt = req_ObjCommission_req.ChildTxnMaxAmt;
                resCommission.ChildTxnMinAmt = req_ObjCommission_req.ChildTxnMinAmt;
                resCommission.ChildTxnRate = req_ObjCommission_req.ChildTxnRate;
                resCommission.MonthlyMinAmt = req_ObjCommission_req.MonthlyMinAmt;
                resCommission.MonthlyMaxAmt = req_ObjCommission_req.MonthlyMaxAmt;
                resCommission.MonthlyBonus = req_ObjCommission_req.MonthlyBonus;
                resCommission.FromDate = Convert.ToDateTime(req_ObjCommission_req.FromDate);
                resCommission.IsActive = true;
                resCommission.IsDeleted = false;
                resCommission.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                resCommission.CreatedByName = Session["AdminUserName"].ToString();
                resCommission.ToDate = Convert.ToDateTime(req_ObjCommission_req.ToDate);
                req_ObjCommission_req.flag = "u";
                CommonDBResonse result = req_ObjCommission_req.AddAgentCommission();

                if (result.code=="0")
                {
                    Common.AddLogs("Successfully Updated Commission(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    //Common.AddLogs("Successfully Add CommissionUpdateHistory(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    resCommission.resultId = req_ObjCommission_req.Id;


                }
                else
                {
                    resCommission.resultId = 0;
                }
                return Json(resCommission, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [Authorize]
        public JsonResult DeleteAgentCommissionDataRow(string Id)
        {
            
            AgentCommission inobject = new AgentCommission();
            inobject.Id = Convert.ToInt64(Id);
            inobject.flag = "d";
            inobject.IsDeleted = true;
            //AddCommission res = RepCRUD<GetCommission, AddCommission>.GetRecord(Common.StoreProcedures.sp_Commission_Get, inobject, outobject);
            CommonDBResonse result = inobject.AddAgentCommission();
            if (result.code == "0")
            {
                Common.AddLogs("Successfully Updated Commission(CommissionId:" + inobject.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                //Common.AddLogs("Successfully Add CommissionUpdateHistory(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                inobject.resultId = Convert.ToInt64( Id);


            }
            else
            {
                inobject.resultId = 0;
            }
            return Json(inobject, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult StatusUpdateAgentCommissionCall(AgentCommission req_ObjCommission_req, string IsActive)
        {
            req_ObjCommission_req.IsActive = Convert.ToBoolean(IsActive);
            req_ObjCommission_req.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            req_ObjCommission_req.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
            req_ObjCommission_req.CreatedByName = Session["AdminUserName"].ToString();
            req_ObjCommission_req.flag = "us";   
            CommonDBResonse result = req_ObjCommission_req.AddAgentCommission();
            //bool IsUpdated = RepCRUD<AddCommission, GetCommission>.Update(objUpdateCommission, "commission");
            if (result.code == "0")
            {
                req_ObjCommission_req.resultId = req_ObjCommission_req.Id;
                Common.AddLogs("Successfully Updated Commission Status(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
            }
            else
            {
                req_ObjCommission_req.resultId = 0;
            }
            return Json(req_ObjCommission_req, JsonRequestBehavior.AllowGet);
        }
        #endregion







        #region Other Commission
        [HttpGet]
        [Authorize]
        public ActionResult OtherCommission(OtherCommission model)
        {
            model.flag = "s";
            DataTable dt = model.GetAgentOtherCommissionData("", "", 0, 0, "");
            if (dt.Rows.Count <= 0)
            {

                OtherCommission ObjCommission = new OtherCommission();
                ObjCommission.flag = "i";
                ObjCommission.createdBy = Convert.ToString(Session["AdminMemberId"]);
                CommonDBResonse result = ObjCommission.AddOtherAgentCommission();
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetOtherCommissionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Sno");
            columns.Add("AgentCreationCommission");
            columns.Add("UserCreationCommission");
            columns.Add("KYCCommission");
            columns.Add("Value");
            columns.Add("MinAmount");
            columns.Add("MaxAmount");
            //columns.Add("FixedCommission");
            columns.Add("CASHINCommission");
            columns.Add("CASHOUTCommission");
            
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
            string MinAmount = context.Request.Form["MinAmount"];
            string OtherCommissionId = context.Request.Form["OtherCommissionId"];
            string MaxAmount = context.Request.Form["MaxAmount"];
            string KYCCommission = context.Request.Form["KYCCommission"];
            string AgentCreationCommission = context.Request.Form["AgentCreationCommission"];
            string UserCreationCommission = context.Request.Form["UserCreationCommission"];
            string Value = context.Request.Form["Value"];
            string CASHINCommission = context.Request.Form["CASHINCommission"];
            string CASHOUTCommission = context.Request.Form["CASHOUTCommission"];
            
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];            
            OtherCommission w = new OtherCommission(); 
            w.OtherCommissionId= Convert.ToInt32(OtherCommissionId);
            w.MinAmount = Convert.ToDecimal(MinAmount);
            w.MaxAmount = Convert.ToDecimal(MaxAmount);
            w.KYCCommission = Convert.ToDecimal(KYCCommission);
            w.UserCreationCommission = Convert.ToDecimal(UserCreationCommission);
            w.AgentCreationCommission = Convert.ToDecimal(AgentCreationCommission);
            w.Value = Convert.ToDecimal(Value);
            //w.GenderType = Convert.ToInt32(GenderType);
            w.CASHINCommission = Convert.ToDecimal(CASHINCommission);
            w.CASHOUTCommission = Convert.ToDecimal(CASHOUTCommission);
          
            w.flag = "s";
            DataTable dt = w.GetAgentOtherCommissionData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);
            List<OtherCommission> objCommission = (List<OtherCommission>)CommonEntityConverter.DataTableToList<OtherCommission>(dt);           
            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;
            DataTableResponse<List<OtherCommission>> objDataTableResponse = new DataTableResponse<List<OtherCommission>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = objCommission

            };
            return Json(objDataTableResponse);

        }


        [HttpPost]
        [Authorize]
        public JsonResult OtherCommissionUpdate(OtherCommission req_ObjCommission_req)
        {

            OtherCommission outobject = new OtherCommission();
           
            float CommissionSlabCount = outobject.CountOtherCommissionCheck();
            if (CommissionSlabCount <=0)
            {
                OtherCommission ObjCommission = new OtherCommission();
                return Json(ObjCommission, JsonRequestBehavior.AllowGet);
            }
            else if (CommissionSlabCount >= 2)
            {
                OtherCommission ObjCommission = new OtherCommission();
                return Json(ObjCommission, JsonRequestBehavior.AllowGet);
            }
            else
            {
                OtherCommission outCommission = new OtherCommission();
                OtherCommission inobjectCommission = new OtherCommission();
                //inobjectCommission.Id = Convert.ToInt64(req_ObjCommission_req.Id);

                OtherCommission resCommission = new OtherCommission();
                resCommission.AgentCreationCommission = req_ObjCommission_req.AgentCreationCommission;
                resCommission.UserCreationCommission = req_ObjCommission_req.UserCreationCommission;
                resCommission.KYCCommission = req_ObjCommission_req.KYCCommission;
                resCommission.Value = req_ObjCommission_req.Value;
                resCommission.MinAmount = req_ObjCommission_req.MinAmount;
                resCommission.MaxAmount = req_ObjCommission_req.MaxAmount;
                resCommission.CASHINCommission = req_ObjCommission_req.CASHINCommission;
                resCommission.CASHOUTCommission = req_ObjCommission_req.CASHOUTCommission;               
                req_ObjCommission_req.flag = "u";
                CommonDBResonse result = req_ObjCommission_req.AddOtherAgentCommission();

                if (result.code == "0")
                {
                    Common.AddLogs("Successfully Updated Commission(CommissionId:" + req_ObjCommission_req.OtherCommissionId + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    //Common.AddLogs("Successfully Add CommissionUpdateHistory(CommissionId:" + req_ObjCommission_req.Id + ")", true, Convert.ToInt32(AddLog.LogType.Rates), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    resCommission.OtherCommissionId = req_ObjCommission_req.OtherCommissionId;


                }
                else
                {
                    resCommission.OtherCommissionId = 0;
                }
                return Json(resCommission, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //[Authorize]
        //public JsonResult AddAgentOtherCommissionDataRow(OtherCommission req_ObjCommission_req)
        //{

        //    var context = HttpContext;
        //    OtherCommission ObjCommission = new OtherCommission(); 
        //    req_ObjCommission_req.flag = "i";
        //    req_ObjCommission_req.createdBy = Convert.ToString(Session["AdminMemberId"]);
        //    CommonDBResonse result = req_ObjCommission_req.AddOtherAgentCommission();
        //    return Json(ObjCommission, JsonRequestBehavior.AllowGet);

        //}



        #endregion



        #region Sample Voucher
        public ActionResult SampleVoucher()
        {
            SampleVoucher w = new SampleVoucher();
            return View("SampleVoucherList", w);
        }

        [Authorize]
        public JsonResult GetSampleVoucher()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");          
            columns.Add("Bank Name");
            columns.Add("Branch");
            columns.Add("Account Name");
            columns.Add("Account Number");
            columns.Add("Created Date");
            columns.Add("Updated Date");

            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[4][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];

            string fromdate = context.Request.Form["StartDate"];
            string todate = context.Request.Form["ToDate"];
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<SampleVoucher> trans = new List<SampleVoucher>();

            SampleVoucher w = new SampleVoucher();
            
            w.StartDate = fromdate;
            w.EndDate = todate;

            DataTable dt = w.GetVoucherData("SVL", sortColumn, sortDirection, OffsetValue, PagingSize, searchby);
            trans = (from DataRow row in dt.Rows

                     select new SampleVoucher
                     {
                         Branch = row["Branch"].ToString(),
                         BankId = dt.Rows[0]["BankId"].ToString(),
                         SampleVoucheId = dt.Rows[0]["SampleVoucheId"].ToString(),
                         Sno = row["Sno"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         AccountName = row["AccountName"].ToString(),
                         AccountNumber = row["AccountNumber"].ToString(),
                         BankName = dt.Rows[0]["BankName"].ToString(),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["totalnumber"]) : 0;

            DataTableResponse<List<SampleVoucher>> objDataTableResponse = new DataTableResponse<List<SampleVoucher>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

      
        public ActionResult AddSampleVoucher(string SampleVoucheId)
        {
            SampleVoucher model = new SampleVoucher();
            if (Convert.ToInt32( SampleVoucheId)>0) 
            {
                model.SampleVoucheId = SampleVoucheId;
                model.flag = "VSV";

                DataTable dt = model.GetVoucherData("VSV", "", "", 0, 0, "");
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    model.SampleVoucheId = model.SampleVoucheId;
                    model.BankId = row["BankId"].ToString();
                    model.BankName = row["BankName"].ToString();
                    model.Branch = row["Branch"].ToString();
                    model.AccountName = row["AccountName"].ToString();
                    model.AccountNumber = row["AccountNumber"].ToString();
                    model.CreatedDateDt = row["CreatedDatedt"].ToString();
                    model.VoucherImage = row["VoucherImage"].ToString();
                }

            }
            return View("AddSampleVoucher", model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddSampleVoucher(SampleVoucher model, HttpPostedFileBase VoucherImageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (VoucherImageFile != null)
                    {
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(VoucherImageFile.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/AgentDocuments/Voucher/Voucher") + fileName);
                        VoucherImageFile.SaveAs(filePath);
                        model.VoucherImage = fileName;
                    }
                    if (Convert.ToInt32(model.SampleVoucheId) > 0)
                    {

                        model.flag = "SVU";
                       
                    }
                    else
                    {
                        model.flag = "SVI";
                        
                    }
                    CommonDBResonse result = model.Add();
                    if (result.code == "0")
                    {
                        TempData["SuccessMessagecategory"] = result.Message;
                    }
                    else
                    {
                        TempData["Messagecategory"] = result.Message;
                    }
                    return RedirectToAction("AddSampleVoucher");

                }
                catch (Exception ex)
                {
                    //ViewBag.Message = result.Message;

                }


            }
            //ViewBag.Message = result.Message;
            return View("AddSampleVoucher", model);

        }


        [HttpPost]
        [Authorize]
        public JsonResult DeleteVoucher(SampleVoucher model)
        {
            SampleVoucher outobject = new SampleVoucher();
            GetAgent w = new GetAgent();
            if (!string.IsNullOrEmpty(model.SampleVoucheId))
            {
                w.AgentUniqueId = model.SampleVoucheId;
            }

            model.flag = "dSV";
            CommonDBResonse result = model.Add();
            //if (result.code == "0")
            //{
            //    //TempData["SuccessMessagecategory"] = result.Message;
            //}
            //else
            //{
            //   // TempData["Messagecategory"] = result.Message;
            //}
            var message = string.Empty;
            var Id = string.Empty;
            return Json(new { Id = result.code, message = result.Message }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult EditVoucher(string SampleVoucheId)
        //{
        //    SampleVoucher model = new SampleVoucher();
        //    model.SampleVoucheId = SampleVoucheId;

        //    model.flag = "VSV";

        //    DataTable dt = model.GetVoucherData("VSV", "", "", 0, 0, "");
        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow row = dt.Rows[0];
        //        model.SampleVoucheId = model.SampleVoucheId;
        //        model.BankId = row["BankId"].ToString();
        //        model.BankName = row["BankName"].ToString();
        //        model.Branch = row["Branch"].ToString();
        //        model.AccountName = row["AccountName"].ToString();
        //        model.AccountNumber = row["AccountNumber"].ToString();
        //        model.CreatedDateDt = row["CreatedDatedt"].ToString();
        //        model.VoucherImage = row["VoucherImage"].ToString();
        //    }

        //    return View("AddSampleVoucher", model);
        //}
        public ActionResult ViewVoucher(string SampleVoucheId)
        {
            SampleVoucher model = new SampleVoucher();
            model.SampleVoucheId= SampleVoucheId;

            model.flag = "VSV";
           
            DataTable dt = model.GetVoucherData("VSV", "", "", 0, 0, "");
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                model.SampleVoucheId = model.SampleVoucheId;
                model.BankId = row["BankId"].ToString();
                model.BankName = row["BankName"].ToString();
                model.Branch = row["Branch"].ToString();
                model.AccountName = row["AccountName"].ToString();
                model.AccountNumber = row["AccountNumber"].ToString();
                model.CreatedDateDt = row["CreatedDatedt"].ToString();
                model.VoucherImage = row["VoucherImage"].ToString();
            }

            return View("ViewVoucher", model);
        }
        #endregion SampleVoucher
    }
}
 