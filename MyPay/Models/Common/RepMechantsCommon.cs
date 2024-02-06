using iText.Html2pdf;
using Microsoft.IdentityModel.Tokens;
using MyPay.Models.Add;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Models.Common
{
    public class RepMechantsCommon
    {
        public void ExportToPDF(string MerchantContact)
        {
            try
            {
                string mystring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/MerchantQR.html"));
                string body = mystring;

                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.ContactNo = MerchantContact;
                AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    string HOST = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);

                    Byte[] QRCode = Common.GetQRMemberID_Encrypted(Convert.ToString(res.UserMemberId));
                    string QRString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(QRCode));
                    body = body.Replace("##LogoImage##", HOST + "/Content/images/logo-dark-merchant-qr.png");
                    body = body.Replace("##HOST##", HOST);
                    body = body.Replace("##Website##", Common.LiveSiteUrl_User);
                    body = body.Replace("##tel1##", Common.tel1);
                    body = body.Replace("##tel2##", Common.tel2);
                    body = body.Replace("##tel3##", Common.tel3);
                    body = body.Replace("##tel4##", Common.tel4);
                    body = body.Replace("##WebsiteName##", Common.WebsiteName);
                    body = body.Replace("##WebsiteEmail##", Common.WebsiteEmail);
                    body = body.Replace("##QRCode##", QRString);
                    body = body.Replace("##Name##", res.FirstName + " " + res.LastName);
                    body = body.Replace("##ContactNo##", res.ContactNo);
                    body = body.Replace("##EmailID##", res.EmailID);
                    body = body.Replace("##OrganizationName##", res.OrganizationName);
                    body = body.Replace("##Address##", res.Address + " " + res.State + " " + res.City + " " + res.ZipCode);

                    string filename = DateTime.Now.Ticks.ToString();
                    System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".html"), body);

                    FileInfo htmlsource = new FileInfo(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".html"));
                    FileInfo pdfDest = new FileInfo(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".pdf"));

                    // pdfHTML specific code
                    ConverterProperties converterProperties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(htmlsource, pdfDest, converterProperties);

                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + MerchantContact + ".pdf");
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".pdf"));
                    //HttpContext.Current.//Response.End();
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".html")))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".html"));
                    }
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".pdf")))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("/Content/MerchantQR/" + filename + ".pdf"));
                    }
                }

            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs(ex.ToString(), true, Convert.ToInt32(AddLog.LogType.ApiRequests), Common.CreatedBy, Common.CreatedByName, false, "web", "", 0, Common.CreatedBy, Common.CreatedByName);
            }
        }
    }
}