using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.SessionState;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Repository;
using System.Web.Http;

namespace MyPay
{
    /// <summary>
    /// Summary description for UploadImage
    /// </summary>
    /// 
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UploadImage : IHttpHandler, IRequiresSessionState
    {

      //  [HttpPost]
        public void ProcessRequest(HttpContext context)
        {
            bool checkflag = false;
            try
            {
                if (context.Request.Form["Method"].ToString() == "UploadImage")
                {
                    UploadImageData(context);
                }
                else
                {
                    if (context.Request.Files.Count > 0)
                    {
                        string uploadfilename = "";
                        string guidfile = "";
                        string proof = "";
                        string additonalproof = "";
                        string proofname = "";
                        string additonalproofname = "";
                        string addressproofname = "";
                        string addressproofext = "";
                        string additonalproofext = "";
                        string proofext = "";
                        int count = 0;
                        string userimage = "";
                        string userimagename = "";
                        string userimagetext = "";
                        if (context.Request.Form["MemberId"] == null || context.Request.Form["MemberId"].ToString() == "")
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Write("MemberId!@!!@!");
                            return;
                        }
                        else if (context.Request.Form["Type"] == null || context.Request.Form["Type"].ToString() == "")
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Write("Type!@!!@!");
                            return;
                        }
                        else if (context.Request.Form["UserId"] == null || context.Request.Form["UserId"].ToString() == "")
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Write("UserId!@!!@!");
                            return;
                        }
                        else if (context.Request.Form["Password"] == null || context.Request.Form["Password"].ToString() == "")
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Write("Password!@!!@!");
                            return;
                        }
                        AddUserAuthorization outobjectauth = new AddUserAuthorization();
                        GetUserAuthorization inobjectauth = new GetUserAuthorization();
                        inobjectauth.UserName = context.Request.Form["UserId"].ToString();
                        AddUserAuthorization resauth = RepCRUD<GetUserAuthorization, AddUserAuthorization>.GetRecord("sp_UserAuthorization_Get", inobjectauth, outobjectauth);
                        if (resauth != null && resauth.Id != 0)
                        {
                            if (Common.DecryptString(resauth.Password) != context.Request.Form["Password"].ToString())
                            {
                                //cres = CommonApiMethod.ReturnBadRequestMessage("Invalid Token");
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.Write("Password Not Match!@!!@!");
                                return;
                            }
                            else if (resauth.IPAddress != Common.GetUserIP() && (resauth.IPAddress != Common.GetServerIPAddress()))
                            {
                                //Common.AddLogs($"GetUserIP: {Common.GetUserIP()} GetServerIPAddress: {Common.GetServerIPAddress()}", false, 0, 0, "", false, "", "", 0, Common.CreatedBy, Common.CreatedByName);
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.Write("Invalid Ip Address!@!!@!");
                                return;
                            }

                        }
                        AddUser outobject = new AddUser();
                        GetUser inobject = new GetUser();
                        inobject.MemberId = Convert.ToInt64(context.Request.Form["MemberId"].ToString());
                        inobject.CheckDelete = 0;
                        AddUser res = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_GetAdmin", inobject, outobject);

                        if (res != null && res.Id != 0)
                        {
                            if (res.IsKYCApproved == (int)AddUser.kyc.Verified)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.Write("Your kyc already approved!@!" + res.NationalIdProofFront + "!@!" + res.NationalIdProofBack + "!@!" + res.IsKYCApproved.ToString() + "!@!" + res.UserImage.ToString());
                                return;
                            }
                            //else if (res.IsKYCApproved == (int)AddUser.kyc.Pending)
                            //{
                            //    context.Response.StatusCode = (int)HttpStatusCode.OK;
                            //    context.Response.Write("Your kyc is under review please wait some time!@!" + res.NationalIdProofFront + "!@!" + res.NationalIdProofBack + "!@!" + res.IsKYCApproved.ToString());
                            //    return;
                            //}
                            if (context.Request.Form["Gender"] != null && context.Request.Form["Gender"].ToString() != "")
                            {
                                res.Gender = Convert.ToInt32(context.Request.Form["Gender"]);
                            }
                            if (context.Request.Form["occupation"] != null && context.Request.Form["occupation"].ToString() != "")
                            {
                                res.EmployeeType = Convert.ToInt32(context.Request.Form["occupation"]);
                            }

                            foreach (string s in context.Request.Files)
                            {
                                HttpPostedFile file = context.Request.Files[s];
                                string fileName = file.FileName;
                                string fileExtension = file.ContentType;
                                int TenMegaBytes = 10 * 1024 * 1024;
                                if (file.ContentLength < TenMegaBytes)
                                {
                                    if (!string.IsNullOrEmpty(fileName))
                                    {

                                        string file_ext = Path.GetExtension(fileName).ToString().ToLower();
                                        if (file_ext == ".jpeg" || file_ext == ".jpg" || file_ext == ".png" || file_ext == ".gif" || file_ext == ".bmp")
                                        {
                                            Guid gupload = Guid.NewGuid();
                                            guidfile = gupload.ToString();
                                            uploadfilename = guidfile + file_ext;

                                            //S3Upload.UploadFileAsync(file.InputStream);
                                            var path = HttpContext.Current.Server.MapPath("~/UserDocuments/Images/");
                                            file.SaveAs(path + uploadfilename);


                                            using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/UserDocuments/Images/") + uploadfilename))
                                            {
                                                using (MemoryStream m = new MemoryStream())
                                                {
                                                    try
                                                    {
                                                        image.Save(m, image.RawFormat);
                                                        byte[] imageBytes = m.ToArray();
                                                        if (context.Request.Form["Type"].ToString().ToLower() == "front")
                                                        {
                                                            proofname = guidfile;
                                                            proofext = file_ext;
                                                            proof = Convert.ToBase64String(imageBytes);
                                                            //if (Convert.ToInt32(context.Request.Form["ProofType"].ToString()) == (int)AddUser.ProofTypes.Passport)
                                                            //{
                                                            //    count = 2;
                                                            //}
                                                            //else
                                                            //{
                                                            //    count = 1;
                                                            //}

                                                        }
                                                        else if (context.Request.Form["Type"].ToString().ToLower() == "back")
                                                        {
                                                            additonalproofext = file_ext;
                                                            additonalproofname = guidfile;
                                                            additonalproof = Convert.ToBase64String(imageBytes);
                                                            count = 2;
                                                        }
                                                        else if (context.Request.Form["Type"].ToString().ToLower() == "selfie")
                                                        {
                                                            userimagetext = file_ext;
                                                            userimagename = guidfile;
                                                            userimage = Convert.ToBase64String(imageBytes);
                                                            HttpContext.Current.Session["MyPayUserImage"] = "/UserDocuments/Images/" + uploadfilename;
                                                            //addressproofext = file_ext;
                                                            //addressproofname = guidfile;
                                                            //addressproof = Convert.ToBase64String(imageBytes);
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                                        context.Response.Write(ex.Message + "!@!" + res.NationalIdProofFront + "!@!" + res.NationalIdProofBack + "!@!" + res.IsKYCApproved.ToString());
                                                    }
                                                }
                                            }
                                            checkflag = true;
                                        }
                                        else
                                        {
                                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                                            context.Response.Write("Extension Format is wrong only(jpeg,jpg,png,gif,bmp) are allowed.!@!!@!");
                                        }

                                    }
                                    else
                                    {
                                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                        context.Response.Write("FileName Not Found!@!!@!");

                                    }
                                }
                                else
                                {
                                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    context.Response.Write("Size(" + (file.ContentLength / 2048).ToString() + ") Should Not Be More Than 10 MB!@!!@!");

                                }
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Write("User Not Found!@!!@!");

                        }

                        if (checkflag)
                        {
                            //RepFrankie.createentity(Convert.ToInt64(usr.MemberId), proofname, proof, proofext, additonalproof, additonalproofname, additonalproofext, addressproof, addressproofname, addressproofext, Convert.ToInt32(context.Request.Form["ProofType"].ToString()));
                            if (!string.IsNullOrEmpty(proofname))
                            {
                                res.NationalIdProofFront = proofname + proofext;
                            }
                            if (!string.IsNullOrEmpty(additonalproofname))
                            {
                                res.NationalIdProofBack = additonalproofname + additonalproofext;
                            }

                            if (!string.IsNullOrEmpty(userimagename))
                            {
                                res.UserImage = userimagename + userimagetext;
                            }

                            AddUserDocuments ud = new AddUserDocuments();
                            GetUserDocuments udin = new GetUserDocuments();
                            udin.MemberId = res.MemberId;
                            AddUserDocuments udres = RepCRUD<GetUserDocuments, AddUserDocuments>.GetRecord(Common.StoreProcedures.sp_UserDocuments_Get, udin, ud);
                            Int64 Id = 0;
                            bool updatestatus = false;
                            if (udres != null && udres.Id != 0)
                            {

                                if (Convert.ToInt32(context.Request.Form["ProofType"].ToString()) == (int)AddUser.ProofTypes.Passport)
                                {
                                    res.ProofType = (int)AddUser.ProofTypes.Passport;
                                    ud.selected_type = (int)AddUser.ProofTypes.Passport;
                                }
                                else if (Convert.ToInt32(context.Request.Form["ProofType"].ToString()) == (int)AddUser.ProofTypes.Driving_Licence)
                                {
                                    res.ProofType = (int)AddUser.ProofTypes.Driving_Licence;
                                    ud.selected_type = (int)AddUser.ProofTypes.Driving_Licence;
                                }
                                else if (Convert.ToInt32(context.Request.Form["ProofType"].ToString()) == (int)AddUser.ProofTypes.Voter_Id)
                                {
                                    res.ProofType = (int)AddUser.ProofTypes.Voter_Id;
                                    ud.selected_type = (int)AddUser.ProofTypes.Voter_Id;
                                }
                                else if (Convert.ToInt32(context.Request.Form["ProofType"].ToString()) == (int)AddUser.ProofTypes.Citizenship)
                                {
                                    res.ProofType = (int)AddUser.ProofTypes.Citizenship;
                                    ud.selected_type = (int)AddUser.ProofTypes.Citizenship;
                                }
                                ud.document_proof = proofname + proofext;
                                ud.additionalproof = additonalproofname + additonalproofext;
                                ud.addressproof = userimagename + userimagetext;
                                ud.MemberId = Convert.ToInt64(res.MemberId);
                                ud.first_name = res.FirstName;
                                ud.last_name = res.LastName;

                                ud.dob = Convert.ToString(res.DateofBirth);
                                ud.email = res.Email;
                                ud.country = res.CountryId.ToString();
                                ud.IsActive = true;
                                ud.ReasonType = (int)AddUserDocuments.ReasonTypes.Pending;
                                ud.CreatedBy = Convert.ToInt64(resauth.MemberId);
                                ud.CreatedByName = resauth.UserName;
                                Id = RepCRUD<AddUserDocuments, GetUserDocuments>.Insert(ud, "userdocuments");

                            }
                            else
                            {
                                if (context.Request.Form["Type"].ToString().ToLower() == "front")
                                {
                                    ud.document_proof = proofname + proofext;
                                }
                                else if (context.Request.Form["Type"].ToString().ToLower() == "back")
                                {
                                    ud.additionalproof = additonalproofname + additonalproofext;
                                }
                                else if (context.Request.Form["Type"].ToString().ToLower() == "selfie")
                                {
                                    ud.addressproof = additonalproofname + additonalproofext;
                                }
                                updatestatus = RepCRUD<AddUserDocuments, GetUserDocuments>.Update(ud, "userdocuments");
                            }
                            if (Id > 0 || updatestatus)
                            {

                                //res.IsKYCApproved = (int)AddUser.kyc.Pending;
                                bool status = RepCRUD<AddUser, GetUser>.Update(res, "user");
                                if (status)
                                {
                                    //HttpContext.Current.Session["IsKycApproved"] = res.IsKYCApproved;
                                    //HttpContext.Current.Session["DocumentReason"] = res.DocumentReason;
                                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    context.Response.Write("Success!@!" + res.NationalIdProofFront + "!@!" + res.NationalIdProofBack + "!@!" + res.IsKYCApproved.ToString() + "!@!" + res.UserImage.ToString());
                                }
                                else
                                {
                                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    context.Response.Write("Something Went Wrong.Try Again Later!@!" + res.NationalIdProofFront + "!@!" + res.NationalIdProofBack + "!@!" + res.IsKYCApproved.ToString());
                                }
                            }
                            else
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.Write("Something Went Wrong.Try Again Later!@!" + res.NationalIdProofFront + "!@!" + res.NationalIdProofBack + "!@!" + res.IsKYCApproved.ToString());
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Write("Something Went Wrong.Try Again Later!@!" + res.NationalIdProofFront + "!@!" + res.NationalIdProofBack + "!@!" + res.IsKYCApproved.ToString());
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.Write("File Not Found!@!!@!");
                    }
                }
            }
            catch (Exception ac)
            {
                context.Response.Write(ac.Message);
                context.Response.Write(ac.Message + "!@!!@!");
            }
        }
        public void UploadImageData(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    foreach (string s in context.Request.Files)
                    {
                        HttpPostedFile file = context.Request.Files[s];
                        string fileName = file.FileName;
                        string fileExtension = file.ContentType;

                        if (!string.IsNullOrEmpty(fileName))
                        {
                            string foldername = context.Request.Form["foldername"].ToString();
                            string transactionid = "";
                            if (!string.IsNullOrEmpty(context.Request.Form["transactionid"]))
                            {
                                transactionid = context.Request.Form["transactionid"].ToString();
                            }
                            string file_ext = Path.GetExtension(fileName).ToString().ToLower();
                            if (file_ext == ".jpeg" || file_ext == ".jpg" || file_ext == ".png" || file_ext == ".gif" || file_ext == ".bmp" || foldername == "TicketsImages")
                            {
                                if (foldername == "UploadReceipt")
                                {
                                    //WalletTransactions w = new WalletTransactions();
                                    //w.TransactionUniqueId = transactionid;
                                    //if (w.GetRecord())
                                    //{
                                    //    w.UploadReceipt = transactionid + file_ext;
                                    //    w.UpdatedDate = DateTime.UtcNow;
                                    //    //w.Type = (int)WalletTransactions.Types.Not_Started;
                                    //    if (w.Update())
                                    //    {
                                    //        if (File.Exists(HttpContext.Current.Server.MapPath("~/Admin/Images/" + foldername + "/") + w.UploadReceipt))
                                    //        {
                                    //            File.Delete(HttpContext.Current.Server.MapPath("~/Admin/Images/" + foldername + "/") + w.UploadReceipt);
                                    //        }
                                    //        file.SaveAs(HttpContext.Current.Server.MapPath("~/Admin/Images/" + foldername + "/") + w.UploadReceipt);
                                    //        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    //        context.Response.Write("Success");
                                    //    }
                                    //    else
                                    //    {
                                    //        context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    //        context.Response.Write("SomeThing Went Wrong Try Again Later");
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    //    context.Response.Write("Transaction Id Not Found");
                                    //}

                                }
                                else
                                {
                                    string uploadfilename = DateTime.Now.ToFileTimeUtc().ToString();
                                    uploadfilename = uploadfilename + file_ext;
                                    file.SaveAs(HttpContext.Current.Server.MapPath("~/Images/" + foldername + "/") + uploadfilename);
                                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    context.Response.Write("Success!@!" + uploadfilename);
                                }
                            }
                            else
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.Write("File Format is wrong only(jpeg,jpg,png,gif,bmp) are allowed.!@!!@!");
                            }

                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.Write("File Not Found!@!!@!");
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.Write("File Not Found!@!!@!");
                }
            }
            catch (Exception ac)
            {
                context.Response.Write(ac.Message);
                context.Response.Write(ac.Message + "!@!!@!");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}