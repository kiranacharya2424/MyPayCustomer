
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iTextSharp;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class EstatementPDFController : Controller
    {
        // GET: EstatementPDF
        [HttpGet]
        public void Index(string Token)
        {
            if (string.IsNullOrEmpty(Token))
            {
                ViewBag.Message = "Please send Token";
            }
            else
            {
                string[] splitvalues = Token.Split('-');
                if (splitvalues[0] != "" && splitvalues[1] != "")
                {
                    ExportToPDF(splitvalues[0], splitvalues[1]);
                }
                else
                {
                    ViewBag.Message = "Invalid Token";
                }
            }
        }
        public void ExportToPDF(string MemberId, string Token)
        {
            try
            {
                string msg = "";

                string mystring = System.IO.File.ReadAllText(HttpContext.Server.MapPath("/Templates/UserElectronicStatementPDF.html"));
                string body = mystring;
                AddEstatementPDFToken outobject = new AddEstatementPDFToken();
                GetEstatementPDFToken inobject = new GetEstatementPDFToken();
                inobject.MemberId = Convert.ToInt64(MemberId);
                inobject.Token = Token;
                AddEstatementPDFToken res = RepCRUD<GetEstatementPDFToken, AddEstatementPDFToken>.GetRecord(Common.StoreProcedures.sp_EstatementPDFToken_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {

                    WalletTransactions uw = new WalletTransactions();
                    uw.MemberId = Convert.ToInt64(MemberId);
                    uw.Year = res.Year;
                    uw.ThreeMonth = res.Month;
                    uw.StartDate = res.FromDate;
                    uw.EndDate = res.ToDate;
                    DataTable dt = new DataTable();
                    dt = uw.GetEStatementList();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string To = string.Empty;

                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                        To = textInfo.ToTitleCase(To);
                        body = body.Replace("##WalletName##", dt.Rows[0]["MemberName"].ToString());
                        body = body.Replace("##WalletId##", dt.Rows[0]["ContactNumber"].ToString());


                        if (res.FromDate != "" && res.ToDate != "")
                        {
                            body = body.Replace("##FromDate##", Convert.ToDateTime(res.FromDate).ToString("dd MMMM yyyy"));
                            body = body.Replace("##ToDate##", Convert.ToDateTime(res.ToDate).ToString("dd MMMM yyyy"));
                        }
                        else
                        {
                            body = body.Replace("##FromDate##", "1 " + dt.Rows[0]["monthname"].ToString() + ", " + res.Year);
                            if (res.Month == "2")
                            {
                                body = body.Replace("##ToDate##", "28 " + dt.Rows[0]["monthname"].ToString() + ", " + res.Year);
                            }
                            else if (res.Month == "1" || res.Month == "3" || res.Month == "5" || res.Month == "7" || res.Month == "8" || res.Month == "10" || res.Month == "12")
                            {
                                body = body.Replace("##ToDate##", "31 " + dt.Rows[0]["monthname"].ToString() + ", " + res.Year);
                            }
                            else if (res.Month == "4" || res.Month == "6" || res.Month == "9" || res.Month == "11")
                            {
                                body = body.Replace("##ToDate##", "30 " + dt.Rows[0]["monthname"].ToString() + ", " + res.Year);
                            }
                        }
                        string str = "";
                        str += "<tr>";
                        str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: right; width: 3%;'> 1 </td>";
                        str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: left; width: 12%;'>";
                        str += "<div>" + dt.Rows[0]["CreatedDate"].ToString() + "</div>";
                        //str += "<div> 11:05:25 </div>";
                        str += "</td>";
                        str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px;  text-align: center; width: 13%;'></ td >";
                        str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: left; width: 37%;'> Closing Balance……….</ td >";
                        str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: center; width: 10%;'> -</ td >";
                        str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: center; width: 10%;'> -</td>";
                        str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 400; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; border-right: 0;text-align: center; width: 15%;'>" + dt.Rows[0]["CurrentBalance"].ToString() + "</td>";
                        str += "</tr>";
                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            str += "<tr>";
                            str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: right; width: 3%;'> " + (i + 2) + " </td>";
                            str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: left; width: 12%;'>";
                            str += "<div> " + dt.Rows[i]["CreatedDate"].ToString() + " </div></td>";
                            str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px;  text-align: center; width: 13%;'>" + dt.Rows[i]["TransactionUniqueId"].ToString() + "</td>";

                            if (dt.Rows[i]["Type"].ToString() == "20" || dt.Rows[i]["Type"].ToString() == "84")
                            {
                                str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: left; width: 37%;'>" + dt.Rows[i]["Description"].ToString().Replace("<", "") + "</td>";
                            }
                            else
                            {
                                str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: left; width: 37%;'>" + dt.Rows[i]["Remarks"].ToString().Replace("<", "") + "</td>";
                            }
                            if (Convert.ToInt32(dt.Rows[i]["Sign"]) == (int)MyPay.Models.Miscellaneous.WalletTransactions.Signs.Debit)
                            {
                                str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: center; width: 10%;'>" + dt.Rows[i]["Amount"].ToString() + "</td>";
                            }
                            else
                            {
                                str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: center; width: 10%;'>-</td>";
                            }
                            if (Convert.ToInt32(dt.Rows[i]["Sign"].ToString()) == (int)MyPay.Models.Miscellaneous.WalletTransactions.Signs.Credit)
                            {
                                str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: center; width: 10%;'>" + dt.Rows[i]["Amount"].ToString() + "</td>";
                            }
                            else
                            {
                                str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; text-align: center; width: 10%;'>-</td>";
                            }

                            str += "<td style = 'color: #111; font-size: 12px; margin:0px; font-weight: 100; font-family: Arial, Helvetica, sans-serif; padding:0px 0px 0px 0px; line-height: 15px; border-right: 0;text-align: center; width: 15%;'> " + dt.Rows[i]["CurrentBalance"].ToString() + "</td>";
                            str += "</tr>";
                        }

                        body = body.Replace("##LogoImage##", Common.LiveSiteUrl + "/Content/images/logonew.png");
                        body = body.Replace("##LiveUrl##", Common.LiveSiteUrl_User);

                        body = body.Replace("##TransactionList##", str);


                        string filename = Token;

                        System.IO.File.WriteAllText(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".html"), body);


                        FileInfo htmlsource = new FileInfo(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".html"));
                        FileInfo pdfDest = new FileInfo(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".pdf"));

                        string sourcePath = HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".pdf");

                        PdfDocument pdfDocument = new PdfDocument(new PdfWriter(sourcePath));
                        pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

                        ConverterProperties converterProperties = new ConverterProperties();
                        HtmlConverter.ConvertToPdf(body, pdfDocument, converterProperties);

                        HttpContext.Response.ContentType = "application/pdf";
                        HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".pdf");
                        HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        HttpContext.Response.WriteFile(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".pdf"));
                        //Response.End();
                        HttpContext.Response.Flush(); // Sends all currently buffered output to the client.
                        HttpContext.Response.SuppressContent = true;

                        if (System.IO.File.Exists(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".html")))
                        {
                            System.IO.File.Delete(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".html"));
                        }
                        if (System.IO.File.Exists(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".pdf")))
                        {
                            System.IO.File.Delete(HttpContext.Server.MapPath("/Content/EstatementPDF/" + filename + ".pdf"));
                        }

                        //GetEstatementPDFToken obj = new GetEstatementPDFToken();
                        //obj.MemberId = Convert.ToInt64(MemberId);
                        //obj.Token = Token;
                        //obj.Delete();

                        msg = "success";
                    }
                    else
                    {
                        msg = "No Transaction found for this month.";
                    }
                }
                else
                {
                    Models.Common.Common.AddLogs("Invalid Token : EstatementPDF", true, Convert.ToInt32(AddLog.LogType.ApiRequests), Common.CreatedBy, Common.CreatedByName, false, "web", "", 0, Common.CreatedBy, Common.CreatedByName);
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