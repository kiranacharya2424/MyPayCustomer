using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPay.Models.Miscellaneous;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iText.Html2pdf;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace MyPay.Controllers
{
    public class NEAElectricityTransactionReceiptController : Controller
    {
        // GET: OfferBanners
        [HttpGet]
        public void Index(string transactionid)
        {
            //string transactionid = Request.QueryString["transactionid"].ToString();

             if (string.IsNullOrEmpty(transactionid))
            {
                ViewBag.Message = "Please send TransactionId";
            }
            else
            {
                ExportToPDF(transactionid);
            }
        }

        public void ExportToPDF(string transactionid)
        {
            try
            {
                string mystring = System.IO.File.ReadAllText(Server.MapPath("/Templates/NEADetail.html"));
                string body = mystring;
                WalletTransactions uw = new WalletTransactions();
                uw.TransactionUniqueId = transactionid;
                if (uw.GetRecord())
                {
                    string BaseURL = Common.LiveSiteUrl;
                    if(!Common.ApplicationEnvironment.IsProduction)
                    {
                        BaseURL = Common.TestSiteUrl;
                    }
                    body = body.Replace("##LogoImage##", BaseURL + "/Content/images/logonew.png");
                    body = body.Replace("##NEA_IMAGE_ICON##", BaseURL + "/Content/assets/Images/ProviderLogos/Electricity/nealogo.png");
                    body = body.Replace("##LiveUrl##", Common.LiveSiteUrl_User);
                    body = body.Replace("##TxnId##", uw.TransactionUniqueId);
                    body = body.Replace("##TxnStatus##", @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(uw.Status)));
                    body = body.Replace("##TransactionDate##", (uw.CreatedDatedt));
                    body = body.Replace("##ServiceCharge##", uw.ServiceCharge.ToString());
                    body = body.Replace("##Date##", Common.fnGetdatetimeFromInput(uw.CreatedDate));
                    body = body.Replace("##Remarks##", uw.Description);

                    if (uw.Status == 1)
                    {
                        body = body.Replace("##TXNCOLOR##", "green");
                    }
                    else if (uw.Status == 2)
                    {
                        body = body.Replace("##TXNCOLOR##", "orange");
                    }
                    else if (uw.Status == 3)
                    {
                        body = body.Replace("##TXNCOLOR##", "red");
                    }
                    else
                    {
                        body = body.Replace("##TXNCOLOR##", "darkyellow");
                    }


                    if (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea))
                    {
                        body = body.Replace("table-row", "none");
                        uw.VendorJsonLookup = uw.VendorJsonLookup.Replace("\\", "");
                        uw.VendorJsonLookup = uw.VendorJsonLookup.Replace("\\/", "/");
                        uw.VendorJsonLookup = uw.VendorJsonLookup.Replace("\"[", "[");
                        uw.VendorJsonLookup = uw.VendorJsonLookup.Replace("]\"", "]");

                        VendorJsonLookupItemsElecticityNEA response = JsonConvert.DeserializeObject<VendorJsonLookupItemsElecticityNEA>(uw.VendorJsonLookup);
                        if (response != null)
                        {
                            string DueBills = String.Empty;
                            body = body.Replace("##CustomerName##", response.Consumer_Name);
                            body = body.Replace("##ConsumerId##", response.Customer_Id);
                            body = body.Replace("##SCNumber##", response.SC_NO);
                            if (response.Counter != null && response.Counter.IndexOf(":") > 0)
                            {
                                body = body.Replace("##Counter##", response.Counter.Split(':')[1]);
                            }
                            else
                            {
                                body = body.Replace("##Counter##", response.Counter);
                            }
                            body = body.Replace("##PaidDate##", uw.CreatedDate.ToString("dd MMM yyyy"));
                            body = body.Replace("##PaidBy##", uw.ContactNumber);
                            body = body.Replace("##TotalDueAmount##", response.Total_Due_Amount);

                            //decimal AdvancedPayment = uw.Amount - Convert.ToDecimal(response.Total_Due_Amount)+uw.CouponDiscount;
                            //body = body.Replace("##NEAAdvance##", AdvancedPayment.ToString());
                            body = body.Replace("##ServiceCharge##", uw.ServiceCharge.ToString());
                            body = body.Replace("##TotalBillAmount##", uw.NetAmount.ToString());

                            for (int i = 0; i < response.Due_Bills.Count; i++)
                            {
                                DueBills += "<tr>";
                                DueBills += "<td style = 'color: #333; font-size: 14px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 18px;'>"+ response.Due_Bills[i].Due_Bill_Of + " </td>";
                                DueBills += "<td style = 'color: #333; font-size: 14px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 18px;'>" + response.Due_Bills[i].Bill_Amount + "</td>";
                                DueBills += "<td style = 'color: #333; font-size: 14px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 18px;'>" + response.Due_Bills[i].Status + "</td>";
                                DueBills += "<td style = 'color: #333; font-size: 14px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 18px; border-right: 0;'>" + response.Due_Bills[i].Payable_Amount + "</td>";
                                DueBills += "</tr> ";

                            }
                            body = body.Replace("##DueBillsRow##", DueBills);
                        }
                        else
                        {
                            body = body.Replace("table-row-nea", "none");
                        }
                    }
                    string filename = DateTime.Now.Ticks.ToString();
                    System.IO.File.WriteAllText(Server.MapPath("/Content/TransactionPDF/" + filename + ".html"), body);

                    FileInfo htmlsource = new FileInfo(Server.MapPath("/Content/TransactionPDF/" + filename + ".html"));
                    FileInfo pdfDest = new FileInfo(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"));

                    // pdfHTML specific code
                    ConverterProperties converterProperties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(htmlsource, pdfDest, converterProperties);

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + uw.TransactionUniqueId + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.WriteFile(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"));
                    //Response.End();
                    Response.Flush(); // Sends all currently buffered output to the client.
                    Response.SuppressContent = true;

                    if (System.IO.File.Exists(Server.MapPath("/Content/TransactionPDF/" + filename + ".html")))
                    {
                        System.IO.File.Delete(Server.MapPath("/Content/TransactionPDF/" + filename + ".html"));
                    }
                    if (System.IO.File.Exists(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf")))
                    {
                        System.IO.File.Delete(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"));
                    }
                }
                else
                {
                    Models.Common.Common.AddLogs("Transaction Template Not Found", true, Convert.ToInt32(AddLog.LogType.ApiRequests), Common.CreatedBy, Common.CreatedByName, false, "web", "", 0, Common.CreatedBy, Common.CreatedByName);
                    return;
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs(ex.ToString(), true, Convert.ToInt32(AddLog.LogType.ApiRequests), Common.CreatedBy, Common.CreatedByName, false, "web", "", 0, Common.CreatedBy, Common.CreatedByName);

            }
        }

        public static Byte[] PdfSharpConvert(String html)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

    }
}