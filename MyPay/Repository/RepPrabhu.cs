using log4net;
using MyPay.API.Models.Response;
using MyPay.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MyPay.Repository
{
   
    public class RepPrabhu
    {
        //public static string username = "mypay";
        //public static string password = "]A7%pEEu77";
        public static string username = "MYPAY789";
        public static string password = "A?p)M[&2GjM";
        public static string username_localhost = "bussewa";
        public static string password_localhost = "bussewa123";
        public static string url = "https://merchant.prabhupay.com/Api/Utility.svc?wsdl";
        public static string url_localhost = "https://testmerchant.prabhupay.com/Api/Utility.svc?wsdl";

        private static ILog log = LogManager.GetLogger(typeof(RepPrabhu));
        public static string SOAPManual(string type,XmlDocument xmldoc)
        {
            string result = "";

            try
            {
                //string url = "https://testmerchant.prabhupay.com/Api/Utility.svc?wsdl";
                //string url = "https://merchant.prabhupay.com/Api/Utility.svc?wsdl";

                const string action="POST";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                XmlDocument SOAPReqBody = new XmlDocument();
                //SOAP Body Request  
                //                SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""UTF - 8""?>
                //<soapenv:Envelope xmlns:soapenv = ""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem = ""http://tempuri.org/"" xmlns:util = ""http://schemas.datacontract.org/2004/07/Utility.API"">
                //<soapenv:Header/>
                //<soapenv:Body>
                //<tem:CreditCardIssuerList>
                //<tem:CreditCardIssuerListRequest>
                //<util:UserName>mypay</util:UserName>
                //<util:Password>]A7%pEEu77</util:Password>      
                //</tem:CreditCardIssuerListRequest>       
                //</tem:CreditCardIssuerList>        
                // </soapenv:Body>
                //</soapenv:Envelope>");


                //                using (Stream stream = webRequest.GetRequestStream())
                //                {
                //                    SOAPReqBody.Save(stream);
                //                }

                //Uncomment next line to simulate timeout (dafault timeout happens at 100 seconds)
                //url = "https://httpstat.us/200?sleep=120000";
                
                
                HttpWebRequest webRequest = CreateWebRequest(url, action,type);
                //webRequest.Timeout = 5;

                InsertSoapEnvelopeIntoWebRequest(xmldoc, webRequest);


                using (WebResponse response = webRequest.GetResponse())
                {

                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        result = rd.ReadToEnd();
                    }
                }


            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action,string type)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", "http://tempuri.org/IUtility/"+type);
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Accept = "text/xml";
            

            webRequest.Method = action;
            return webRequest;
        }
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
        public static XmlDocument MakeDocCreditCardIsuerList()
        {            
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlElement soapNode = doc.CreateElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
            soapNode.SetAttribute("xmlns:tem", "http://tempuri.org/");
            soapNode.SetAttribute("xmlns:util", "http://schemas.datacontract.org/2004/07/Utility.API");
            doc.AppendChild(soapNode);
            XmlElement soapHeader = doc.CreateElement("soapenv", "Header",doc.DocumentElement.NamespaceURI);
            soapNode.AppendChild(soapHeader);

            XmlElement soapBody = doc.CreateElement("soapenv", "Body", doc.DocumentElement.NamespaceURI);
            soapNode.AppendChild(soapBody);

            XmlElement GetEXRate = doc.CreateElement("tem", "CreditCardIssuerList", doc.DocumentElement.NamespaceURI);
            soapBody.AppendChild(GetEXRate);

            XmlElement GetEXRateData = doc.CreateElement("tem", "CreditCardIssuerListRequest", doc.DocumentElement.NamespaceURI);
            GetEXRate.AppendChild(GetEXRateData);

            XmlElement reusername = doc.CreateElement("util", "UserName", doc.DocumentElement.NamespaceURI);
            reusername.AppendChild(doc.CreateTextNode(username));
            GetEXRateData.AppendChild(reusername);


            XmlElement repassword = doc.CreateElement("util", "Password", doc.DocumentElement.NamespaceURI);
            repassword.AppendChild(doc.CreateTextNode(password));
            GetEXRateData.AppendChild(repassword);

            doc.InnerXml = doc.InnerXml.Replace("xmlns:tem=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            doc.InnerXml = doc.InnerXml.Replace("xmlns:util=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            //string xmlpath = HttpContext.Current.Server.MapPath("/Admin/Files/AustracReporting/NABIL.xml").ToString();
            //doc.Save(xmlpath);
            return doc;
        }

        public static XmlDocument MakeDocCreditCardCharge(string code,string amount)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlElement soapNode = doc.CreateElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
            soapNode.SetAttribute("xmlns:tem", "http://tempuri.org/");
            soapNode.SetAttribute("xmlns:util", "http://schemas.datacontract.org/2004/07/Utility.API");
            doc.AppendChild(soapNode);
            XmlElement soapHeader = doc.CreateElement("soapenv", "Header", doc.DocumentElement.NamespaceURI);
            soapNode.AppendChild(soapHeader);

            XmlElement soapBody = doc.CreateElement("soapenv", "Body", doc.DocumentElement.NamespaceURI);
            soapNode.AppendChild(soapBody);

            XmlElement GetEXRate = doc.CreateElement("tem", "CreditCardCharge", doc.DocumentElement.NamespaceURI);
            soapBody.AppendChild(GetEXRate);

            XmlElement GetEXRateData = doc.CreateElement("tem", "CreditCardChargeRequest", doc.DocumentElement.NamespaceURI);
            GetEXRate.AppendChild(GetEXRateData);

            XmlElement reusername = doc.CreateElement("util", "UserName", doc.DocumentElement.NamespaceURI);
            reusername.AppendChild(doc.CreateTextNode(username));
            GetEXRateData.AppendChild(reusername);


            XmlElement repassword = doc.CreateElement("util", "Password", doc.DocumentElement.NamespaceURI);
            repassword.AppendChild(doc.CreateTextNode(password));
            GetEXRateData.AppendChild(repassword);

            XmlElement IssuerCode = doc.CreateElement("util", "IssuerCode", doc.DocumentElement.NamespaceURI);
            IssuerCode.AppendChild(doc.CreateTextNode(code));
            GetEXRateData.AppendChild(IssuerCode);

            XmlElement Amount = doc.CreateElement("util", "Amount", doc.DocumentElement.NamespaceURI);
            Amount.AppendChild(doc.CreateTextNode(amount));
            GetEXRateData.AppendChild(Amount);

            doc.InnerXml = doc.InnerXml.Replace("xmlns:tem=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            doc.InnerXml = doc.InnerXml.Replace("xmlns:util=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            //string xmlpath = HttpContext.Current.Server.MapPath("/Admin/Files/AustracReporting/NABIL.xml").ToString();
            //doc.Save(xmlpath);
            return doc;
        }

        public static XmlDocument MakeDocBillPayment(string amount, string code, string subscriber, string txnid, string extrafield1, string extrafield2, string extrafield3, string extrafield4, string extrafield5, string extrafield6, string extrafield7, string extrafield8)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlElement soapNode = doc.CreateElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
            soapNode.SetAttribute("xmlns:tem", "http://tempuri.org/");
            soapNode.SetAttribute("xmlns:util", "http://schemas.datacontract.org/2004/07/Utility.API");
            doc.AppendChild(soapNode);
            XmlElement soapHeader = doc.CreateElement("soapenv", "Header", doc.DocumentElement.NamespaceURI);
            soapNode.AppendChild(soapHeader);

            XmlElement soapBody = doc.CreateElement("soapenv", "Body", doc.DocumentElement.NamespaceURI);
            soapNode.AppendChild(soapBody);

            XmlElement GetEXRate = doc.CreateElement("tem", "BillPayment", doc.DocumentElement.NamespaceURI);
            soapBody.AppendChild(GetEXRate);

            XmlElement GetEXRateData = doc.CreateElement("tem", "BillPaymentRequest", doc.DocumentElement.NamespaceURI);
            GetEXRate.AppendChild(GetEXRateData);

            XmlElement reusername = doc.CreateElement("util", "UserName", doc.DocumentElement.NamespaceURI);
            reusername.AppendChild(doc.CreateTextNode(username));
            GetEXRateData.AppendChild(reusername);


            XmlElement repassword = doc.CreateElement("util", "Password", doc.DocumentElement.NamespaceURI);
            repassword.AppendChild(doc.CreateTextNode(password));
            GetEXRateData.AppendChild(repassword);

            XmlElement OperatorCode = doc.CreateElement("util", "OperatorCode", doc.DocumentElement.NamespaceURI);
            OperatorCode.AppendChild(doc.CreateTextNode(code));
            GetEXRateData.AppendChild(OperatorCode);

            XmlElement Subscriber = doc.CreateElement("util", "Subscriber", doc.DocumentElement.NamespaceURI);
            Subscriber.AppendChild(doc.CreateTextNode(subscriber));
            GetEXRateData.AppendChild(Subscriber);

            XmlElement Amount = doc.CreateElement("util", "Amount", doc.DocumentElement.NamespaceURI);
            Amount.AppendChild(doc.CreateTextNode(amount));
            GetEXRateData.AppendChild(Amount);

            XmlElement PartnerTxnId = doc.CreateElement("util", "PartnerTxnId", doc.DocumentElement.NamespaceURI);
            PartnerTxnId.AppendChild(doc.CreateTextNode(txnid));
            GetEXRateData.AppendChild(PartnerTxnId);

            XmlElement ExtraField1 = doc.CreateElement("util", "ExtraField1", doc.DocumentElement.NamespaceURI);
            ExtraField1.AppendChild(doc.CreateTextNode(extrafield1));
            GetEXRateData.AppendChild(ExtraField1);

            XmlElement ExtraField2 = doc.CreateElement("util", "ExtraField2", doc.DocumentElement.NamespaceURI);
            ExtraField2.AppendChild(doc.CreateTextNode(extrafield2));
            GetEXRateData.AppendChild(ExtraField2);

            XmlElement ExtraField3 = doc.CreateElement("util", "ExtraField3", doc.DocumentElement.NamespaceURI);
            ExtraField3.AppendChild(doc.CreateTextNode(extrafield3));
            GetEXRateData.AppendChild(ExtraField3);

            XmlElement ExtraField4 = doc.CreateElement("util", "ExtraField4", doc.DocumentElement.NamespaceURI);
            ExtraField4.AppendChild(doc.CreateTextNode(extrafield4));
            GetEXRateData.AppendChild(ExtraField4);


            XmlElement ExtraField5 = doc.CreateElement("util", "ExtraField5", doc.DocumentElement.NamespaceURI);
            ExtraField5.AppendChild(doc.CreateTextNode(extrafield5));
            GetEXRateData.AppendChild(ExtraField5);

            XmlElement ExtraField6 = doc.CreateElement("util", "ExtraField6", doc.DocumentElement.NamespaceURI);
            ExtraField6.AppendChild(doc.CreateTextNode(extrafield6));
            GetEXRateData.AppendChild(ExtraField6);

            XmlElement ExtraField7 = doc.CreateElement("util", "ExtraField7", doc.DocumentElement.NamespaceURI);
            ExtraField7.AppendChild(doc.CreateTextNode(extrafield7));
            GetEXRateData.AppendChild(ExtraField7);

            XmlElement ExtraField8 = doc.CreateElement("util", "ExtraField8", doc.DocumentElement.NamespaceURI);
            ExtraField8.AppendChild(doc.CreateTextNode(extrafield8));
            GetEXRateData.AppendChild(ExtraField8);

            doc.InnerXml = doc.InnerXml.Replace("xmlns:tem=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            doc.InnerXml = doc.InnerXml.Replace("xmlns:util=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            //string xmlpath = HttpContext.Current.Server.MapPath("/Admin/Files/AustracReporting/NABIL.xml").ToString();
            //doc.Save(xmlpath);
            return doc;
        }
        public static void CheckCrediCardTransactionStatus()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://apitest.cybersource.com/tss/v2/searches"))
                {
                    request.Headers.TryAddWithoutValidation("Host", "apitest.cybersource.com");
                    request.Headers.TryAddWithoutValidation("Date", "Thu, 28 Jan 2021 17:22:18 GMT");
                    request.Headers.TryAddWithoutValidation("Digest", "SHA-256=EP+7gEEDZjwwmMt0/17wLSylagLmaCj30fam5v4xHPU=");
                    request.Headers.TryAddWithoutValidation("v-c-merchant-id", "100710070000069");
                    request.Headers.TryAddWithoutValidation("Signature", "keyid=\"1fe8e389-935d-4074-9b9a-9044fa5fc210\", algorithm=\"HmacSHA256\", headers=\"host date (request-target) digest v-c-merchant-id\", signature=\"IOc3lnlCslyVYqK4Gk3JFtoBaB1IWGsebCUT+aJ1lew=\"");

                    request.Content = new StringContent("{\n\"save\": \"true\",\n\"name\": \"NicAsia\",\n\"timezone\": \"Asia/Kathmandu\",\n\"query\": \"clientReferenceInformation.code:3705 AND submitTimeUtc:[NOW/DAY-7DAYS TO NOW/DAY+1DAY}\",\n\"offset\": \"0\",\n\"limit\": \"100\",\n\"sort\": \"id:asc,submitTimeUtc:asc\"\n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response =   httpClient.SendAsync(request).Result;
                }
            }
        }
        public static List<GetCreditCardIssuerList> GetCrediCardIssuerList()
        {
            List<GetCreditCardIssuerList> bllist = new List<GetCreditCardIssuerList>();

            XmlDocument x = MakeDocCreditCardIsuerList();
            try
            {

                string rep = SOAPManual("CreditCardIssuerList", x);
                rep = rep.Replace("a:", "");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rep);
                if (doc.FirstChild.LastChild.LastChild.LastChild.LastChild.HasChildNodes)
                {


                    XmlNodeList titles = doc.FirstChild.LastChild.LastChild.LastChild.LastChild.ChildNodes;

                    foreach (XmlNode node1 in titles)
                    {
                        GetCreditCardIssuerList bl = new GetCreditCardIssuerList();
                        bl.Code = node1["Code"].InnerText;
                        bl.Name = node1["Name"].InnerText;
                        bl.Logo = node1["Logo"].InnerText;

                        bllist.Add(bl);
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            return bllist;
        }


        public static GetCreditCardCharge GetCreditCardCharge( string amount,string code)
        {
            GetCreditCardCharge bl = new GetCreditCardCharge();

            XmlDocument x = MakeDocCreditCardCharge(code,amount);
            try
            {

                string rep = SOAPManual("CreditCardCharge", x);
                rep = rep.Replace("a:", "");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rep);
                if (doc.FirstChild.LastChild.LastChild.HasChildNodes)
                {


                    XmlNodeList titles = doc.FirstChild.LastChild.LastChild.ChildNodes;

                    foreach (XmlNode node1 in titles)
                    {
                        bl.Code = node1["Code"].InnerText;
                        bl.Message = node1["Message"].InnerText;
                        bl.SCharge = node1["SCharge"].InnerText;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return bl;
        }

        public static GetCreditCardTransactionId MakeBillPayment(string amount, string code,string subscriber,string txnid,string extrafield1,string extrafield2,string extrafield3,string extrafield4,string extrafield5,string extrafield6,string extrafield7 ,string extrafield8)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RepPrabhu MakeBillPayment start" + Environment.NewLine);

            GetCreditCardTransactionId bl = new GetCreditCardTransactionId();

            log.Info($"{System.DateTime.Now.ToString()} inside RepPrabhu MakeBillPayment MakeDocBillPayment start" + Environment.NewLine);
            XmlDocument x = MakeDocBillPayment( amount,  code,  subscriber,  txnid,  extrafield1,  extrafield2,  extrafield3,  extrafield4,  extrafield5,  extrafield6,  extrafield7,  extrafield8);
            log.Info($"{System.DateTime.Now.ToString()} inside RepPrabhu MakeBillPayment MakeDocBillPayment complete" + Environment.NewLine);
            
            try
            {
                string rep = SOAPManual("BillPayment", x);
                rep = rep.Replace("a:", "");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rep);
                if (doc.FirstChild.LastChild.LastChild.HasChildNodes)
                {
                    XmlNodeList titles = doc.FirstChild.LastChild.LastChild.ChildNodes;

                    foreach (XmlNode node1 in titles)
                    {
                        bl.Code = node1["Code"].InnerText;
                        bl.Message = node1["Message"].InnerText;
                        bl.TransactionId = node1["TransactionId"].InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} RepPrabhu MakeBillPayment {ex.ToString()} " + Environment.NewLine);
            }
            log.Info($"{System.DateTime.Now.ToString()} RepPrabhu MakeBillPayment complete" + Environment.NewLine);
            return bl;
        }
    }
}
