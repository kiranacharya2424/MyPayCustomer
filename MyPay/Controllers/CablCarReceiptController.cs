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
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using IronBarCode;
using Microsoft.Extensions.Logging;

namespace MyPay.Controllers
{
    public class CableCarReceiptController : Controller
    {

        StringBuilder sb = new StringBuilder();
        // GET: OfferBanners
        [HttpGet]
        public void Index(string refeneceID)
        {
            //string transactionid = Request.QueryString["transactionid"].ToString();

            if (string.IsNullOrEmpty(refeneceID))
            {
                ViewBag.Message = "Please send ReferenceId";
            }
            else
            {
                ExportToPDF(refeneceID);
            }
        }
        public void ExportToPDF(string refeneceID)
        {
            try
            {
                string mystring = System.IO.File.ReadAllText(Server.MapPath("/Templates/PaymentReceipt.html"));
                string body = mystring;
                //CableDownloadVendorResponse CableDownloadVendorResponse = null;
                //var data = VendorApi_CommonHelper.CableCarDownload(refeneceID);
                //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //QRCodeData qrCodeData = qrGenerator.CreateQrCode(d.QRCode, QRCodeGenerator.ECCLevel.Q);
                //QRCode qrCode = new QRCode(qrCodeData);
                //Bitmap qrCodeImage = qrCode.GetGraphic(20);
                //string ImageUrl = "";
                //using (MemoryStream stream = new MemoryStream())
                //{
                //    qrCodeImage.Save(stream, ImageFormat.Png);
                //    byte[] byteImage = stream.ToArray();
                //    ImageUrl = string.Format(Convert.ToBase64String(byteImage));
                //}
                //string qrFilePath = Server.MapPath("~/Content/CableQRCode/");
                //string qrImageName = "qrImage" + "_" + ".png";
                //qrCodeImage.Save(qrFilePath + qrImageName, ImageFormat.Png);
                //////// for bar code
                //GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(data.BarCode, BarcodeWriterEncoding.Code128);
                //barcode.ResizeTo(400, 120);
                //barcode.AddBarcodeValueTextBelowBarcode();
                //// Styling a Barcode and adding annotation text
                //barcode.ChangeBarCodeColor(Color.Black);
                //barcode.SetMargins(10);
                //string qrFilePathbar = Server.MapPath("~/Content/CableBarCode/");
                //string BarImageName = "BarImage" + "_" + ".png";
                //barcode.SaveAsPng(qrFilePathbar + BarImageName);



                //CableDownloadVendorResponse = VendorApi_CommonHelper.CableCarDownload(refeneceID);
                WalletTransactions uw = new WalletTransactions();
                uw.TransactionUniqueId = refeneceID;

                if (uw.GetRecord())
                {

                    if (uw.Type == 121 || uw.Type == 124 || uw.Type == 125 || uw.Type == 126 || uw.Type == 113 || uw.Type == 200 || uw.Type == 300 )
                    {
                        generateNewReceipt(refeneceID);
                        return;
                    }


                    string To = string.Empty;
                    if (uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events)
                    {
                        To = uw.MerchantOrganization;
                    }
                    else if (!string.IsNullOrEmpty(uw.CustomerID) && uw.VendorType == (int)VendorApi_CommonHelper.VendorTypes.khalti)
                    {
                        To = uw.CustomerID;
                        To = To + " " + ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(uw.Type)).ToString().Replace("khalti", " ").Replace("_", " ");
                    }
                    else
                    {
                        To = uw.RecieverName;
                    }
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    To = textInfo.ToTitleCase(To);
                    if (uw.Sign == (int)MyPay.Models.Miscellaneous.WalletTransactions.Signs.Credit)
                    {
                        if (uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.WalletUpdate_By_Admin || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Hold_By_Admin || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Release_From_Admin)
                        {
                            body = body.Replace("##From##", uw.CreatedByName);
                            body = body.Replace("##To##", uw.RecieverName);
                        }
                        else
                        {
                            body = body.Replace("##From##", uw.RecieverName);
                            body = body.Replace("##To##", uw.MemberName);
                        }
                    }
                    else
                    {
                        if (uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.WalletUpdate_By_Admin || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Hold_By_Admin || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Release_From_Admin)
                        {
                            body = body.Replace("##From##", uw.CreatedByName);
                            body = body.Replace("##To##", uw.RecieverName);
                        }
                        else if ((uw.Type) == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut)
                        {
                            body = body.Replace("##From##", uw.MemberName);
                            AddMerchant outobject = new AddMerchant();
                            GetMerchant inobject = new GetMerchant();
                            inobject.MerchantMemberId = uw.MerchantMemberId;
                            AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                            if (model != null && model.Id > 0)
                            {
                                if (!string.IsNullOrEmpty(model.OrganizationName))
                                {
                                    body = body.Replace("##To##", model.OrganizationName);
                                }
                                else
                                {
                                    body = body.Replace("##To##", uw.CustomerID);
                                }
                            }
                        }
                        else
                        {
                            body = body.Replace("##From##", uw.MemberName);
                            body = body.Replace("##To##", To);
                        }
                    }
                    if (uw.WalletType == (int)WalletTransactions.WalletTypes.MPCoins)
                    {
                        body = body.Replace("##mypaycoins_styledisplay##", "inline-block");
                    }
                    else
                    {
                        body = body.Replace("##mypaycoins_styledisplay##", "none");
                    }


                    if (uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NCell || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NTC || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_SMSPack_NCELL || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_SMSPack_NTC || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_VoicePack_NCELL || uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_VoicePack_NTC)
                    {
                        body = body.Replace("##datapack_styledisplay##", "table-row");
                    }
                    else
                    {
                        body = body.Replace("##datapack_styledisplay##", "none");
                    }
                    if (uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea)
                    {
                        body = body.Replace("##electricity_nea_styledisplay##", "table-row-nea");
                    }
                    else
                    {
                        body = body.Replace("##electricity_nea_styledisplay##", "none");
                    }
                    if (uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer)
                    {
                        body = body.Replace("##bankdetails_styledisplay##", "table-row-bank");
                    }
                    else
                    {
                        body = body.Replace("##bankdetails_styledisplay##", "none");
                    }
                    body = body.Replace("##TransactionType##", (uw.Sign == (int)MyPay.Models.Miscellaneous.WalletTransactions.Signs.Credit ? MyPay.Models.Miscellaneous.WalletTransactions.Signs.Credit.ToString() : MyPay.Models.Miscellaneous.WalletTransactions.Signs.Debit.ToString()));
                    body = body.Replace("##LogoImage##", Common.LiveSiteUrl + "/Content/images/logonew.png");
                    body = body.Replace("##LiveUrl##", Common.LiveSiteUrl_User);
                    if (uw.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                    {
                        body = body.Replace("##TransactionId##", uw.Reference);
                    }
                    else
                    {
                        body = body.Replace("##TransactionId##", uw.TransactionUniqueId);
                    }
                    body = body.Replace("##ConsumerTransactionId##", uw.Reference);
                    body = body.Replace("##TransactionStatus##", @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(uw.Status)));
                    //body = body.Replace("##TransactionDate##", Common.fnGetdatetimeFromInput(uw.CreatedDate));
                    body = body.Replace("##TransactionDate##", Common.fnGetdatetimeFromInput(uw.CreatedDate));
                    body = body.Replace("##TransactionService##", @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(uw.Type)).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString());
                    body = body.Replace("##ServiceCharge##", uw.ServiceCharge.ToString());
                    body = body.Replace("##Cashback##", uw.CashBack.ToString());
                    body = body.Replace("##Amount##", uw.Amount.ToString());
                    body = body.Replace("##TotalAmount##", uw.NetAmount.ToString());
                    body = body.Replace("##MyPayCoinsCredited##", uw.RewardPoint.ToString());
                    body = body.Replace("##MyPayCoinsDebited##", uw.MPCoinsDebit.ToString());
                    body = body.Replace("##Date##", Common.fnGetdatetimeFromInput(uw.CreatedDate));
                    if (uw.Type == 20 || uw.Type == 84 || uw.Type == 73)
                    {
                        body = body.Replace("##Remarks##", uw.Description);
                    }
                    else
                    {
                        body = body.Replace("##Remarks##", uw.Remarks);
                    }
                    body = body.Replace("##tel1##", Common.tel1);
                    body = body.Replace("##tel2##", Common.tel2);
                    body = body.Replace("##tel3##", Common.tel3);
                    body = body.Replace("##tel4##", Common.tel4);
                    body = body.Replace("##WebsiteName##", Common.WebsiteName);
                    body = body.Replace("##WebsiteEmail##", Common.WebsiteEmail);

                    if (string.IsNullOrEmpty(uw.VendorJsonLookup))
                    {
                        body = body.Replace("table-row", "none");
                        body = body.Replace("table-row-nea", "none");
                    }
                    else
                    {
                        if ((uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NCell)) || (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NTC)) || (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_VoicePack_NTC)) || (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_VoicePack_NCELL)) || (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_SMSPack_NTC)) || (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_SMSPack_NCELL)))
                        {
                            body = body.Replace("table-row-nea", "none");
                            VendorJsonLookupItems response = JsonConvert.DeserializeObject<VendorJsonLookupItems>(uw.VendorJsonLookup);
                            if (response != null)
                            {
                                body = body.Replace("##ProductType##", response.ProductType);
                                body = body.Replace("##ProductName##", response.ProductName);
                                body = body.Replace("##Validity##", response.Validity);
                            }
                            else
                            {
                                body = body.Replace("table-row", "none");
                            }
                        }
                        else if (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea))
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
                                body = body.Replace("##NEAConsumer_Name##", response.Consumer_Name);
                                body = body.Replace("##NEACustomer_Id##", response.Customer_Id);
                                body = body.Replace("##NEASC_NO##", response.SC_NO);
                                body = body.Replace("##NEACounter##", response.Counter);
                              
                                body = body.Replace("##NEA_DUE_BILLS##", DueBills);
                            }
                            else
                            {
                                body = body.Replace("table-row-nea", "none");
                            }
                        }
                    }
                    if (uw.Type == ((int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer))
                    {
                        body = body.Replace("table-row", "none");
                        body = body.Replace("table-row-nea", "none");
                        body = body.Replace("##BankNAME##", uw.RecieverBankName);
                        body = body.Replace("##BankBRANCHNAME##", uw.RecieverBranchName);
                        body = body.Replace("##BankACCOUNTNO##", uw.RecieverAccountNo);
                    }
                    string filename = DateTime.Now.Ticks.ToString();

                    //Byte[] fileContent = PdfSharpConvert(body);
                    //System.IO.File.WriteAllBytes(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"), fileContent);

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


        void generateNewReceipt(string txnID)
        {
            //resbank = RepCRUD<GetBankTransactions, AddBankTransactions>.GetRecord(Common.Common.StoreProcedures.sp_BankTransactions_Get, inbank, outbank);

            //Receipts resceiptData = RepCRUD

            Receipts receiptData = getRecord(txnID);
            if (receiptData == null)
            {
                return;
            }

            

            startHTMLDocWithBody();
            //generateHeader("12345676", "Roshan Thapa", "90650.00", "Credit card", "SX-0123231233", "1231232132131123");
            generateHeader(receiptData.contactNumber, receiptData.fullname, receiptData.Amount.ToString(), receiptData.TxnType, receiptData.PaidFor, receiptData.TxnID);
            var jsonData = receiptData.table1JSONContent;

            if (jsonData == null)
            {
                return;
            }
            
            JObject jObject = JObject.Parse(jsonData);

            startTable();


            foreach (var data in jObject)
            {
                addTableRow( data.Key.ToString().Trim(), data.Value.ToString());
            }
            
            
            //addTableRow("Key", "value");
            //addTableRow("Key", "value");
            //addTableRow("Key", "value");
            //addTableRow("Key", "value");
            //addTableRow("Key", "value");
            //addTableRow("Key", "value");
            //addTableRow("Key", "value");
            //addTableRow("Key", "value");
            endTable();
            generateFooter();
            endBodyAndHTML();

            //     return response;
            string filename = DateTime.Now.Ticks.ToString();

            //Byte[] fileContent = PdfSharpConvert(body);
            //System.IO.File.WriteAllBytes(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"), fileContent);

            System.IO.File.WriteAllText(Server.MapPath("/Content/TransactionPDF/" + filename + ".html"), sb.ToString());


            FileInfo htmlsource = new FileInfo(Server.MapPath("/Content/TransactionPDF/" + filename + ".html"));
            FileInfo pdfDest = new FileInfo(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"));


            // pdfHTML specific code
            ConverterProperties converterProperties = new ConverterProperties();
            HtmlConverter.ConvertToPdf(htmlsource, pdfDest, converterProperties);

            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=" + uw.TransactionUniqueId + ".pdf");

            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".pdf");


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

        //HTML Creator code added by Roshan
        void startHTMLDocWithBody()
        {
            sb.Append("<html><head><style>.borderedTable, .borderedrow {border: 1px solid black;border-collapse: collapse;}</style></head><body style=\"font-family:calibri\">");
        }


        void generateHeader(string customerID, string fullname, string transactionAmount, string TransactionType, string PaidFor, string TxnId)
        {
            sb.Append("<p style=\"text-align:center; width: 100%;max-width: 700px;\"><img alt=\"\" src=\"https://mypay.com.np/wp-content/uploads/2023/03/mypay.png\" style=\"height:50px; width:150px\" /></p>");
            sb.Append("<p style=\"text-align:center; width: 100%;max-width: 700px;\"> <strong>Electronic Payment Receipt </strong> <br/>");

            sb.Append("Date: <strong>" + DateTime.Now.ToString("dd-MMM-yyyy") + "|" + DateTime.Now.ToString("h:mm tt") + "</strong> </p>");


            sb.Append("<table style=\"width: 100%;max-width: 700px; \">");


            sb.Append("<tr>");
            sb.Append("<td align=\"left\">");
            sb.Append("Wallet ID: <strong> " + customerID + " </strong>");
            sb.Append("</td>");
            sb.Append("<td align=\"right\">");
            sb.Append("Full Name: <strong> " + fullname + " </strong>");
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td align=\"left\">");
            sb.Append("Txn ID: <strong> " + TxnId + " </strong>");
            sb.Append("</td>");
            sb.Append("<td align=\"right\">");
            sb.Append("Type: <strong> " + TransactionType + " </strong>");
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td align=\"left\">");
            sb.Append("To: <strong> " + PaidFor + " </strong>");
            sb.Append("</td>");
            sb.Append("<td align=\"right\">");
            sb.Append("Txn Amount: <strong style=\"color: red\"> " + transactionAmount + " </strong>");
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("<br/>");
        }

        void startTable()
        {
            sb.Append("<table class=\"borderedTable\" style=\"width: 100%;max-width: 700px; table-layout: fixed;\">");
        }

        void addTableRow(string key, string value)
        {
            if (key.ToUpper().Contains("(RED)"))
            {
                key = key.Replace("(RED)", "");
                sb.Append("<tr><td align=\"left\"  class=\"borderedrow\"> " + key + ": </td><td align=\"center\" class=\"borderedrow\"> <strong style=\"color: red\">" + value + "</strong> </td></tr>");
            }
            else if (key.ToUpper().Contains("(GREEN)"))
            {
                key = key.Replace("(GREEN)", "");
                sb.Append("<tr><td align=\"left\"  class=\"borderedrow\"> " + key + ": </td><td align=\"center\" class=\"borderedrow\"> <strong style=\"color: green\">" + value + "</strong> </td></tr>");
            }
            else
            {
                sb.Append("<tr><td align=\"left\"  class=\"borderedrow\"> " + key + ": </td><td align=\"center\" class=\"borderedrow\"> <strong>" + value + "</strong> </td></tr>");
            }
        }


        void endTable()
        {
            sb.Append("</table>");
        }

        void generateFooter()
        {
            sb.Append("<p style=\"text-align:right; width: 100%;max-width: 700px;\"><strong>Disclaimer:</strong> This is system generated electronic payment receipt</p>");
            sb.Append("<p><strong>Contact Us</strong><br/>");
            sb.Append("Toll Free: 1660016200 | Phone No.: 01-5907481/82 | E-mail: support@mypay.com.np</p>");
        }

        void endBodyAndHTML()
        {
            sb.Append("</body></html>");
        }

        public Receipts getRecord(String txnID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

            using (var connection = new SqlConnection(connectionString))
            {

                Receipts receiptData = new Receipts();
                var storedProcedureName = "sp_getReceipt";
                var values = new
                {
                    txnID = txnID,
                };
                try
                {
                    receiptData = connection.QueryFirstOrDefault<Receipts>(storedProcedureName, values, commandType: CommandType.StoredProcedure);
                    //var result = connection.Query<Customer>("GetCustomerByID", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (SqlException sqlEx)
                {

                }
                return receiptData;
            }
        }

    }

}