using log4net;
using Microsoft.Ajax.Utilities;
using MyPay.API.Models;
using MyPay.API.Models.Airlines;
using MyPay.API.Models.PlasmaTech;
using MyPay.API.Models.Request.PlasmaTech;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;
//using System.Web.Mvc;
//using System.Web.Mvc;
using System.Windows;
using System.Windows.Interop;
using FlightInbound = MyPay.API.Models.Airlines.FlightInbound;
using FlightOutbound = MyPay.API.Models.Airlines.FlightOutbound;
using FligthSectors = MyPay.API.Models.Airlines.FligthSectors;

namespace MyPay.API.Controllers
{

        public class FlightTicketDownloadController : Controller
    {

        string ApiResponse = string.Empty;

        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_AirlinesController));

        RequestAPI_PlasmaTechController _RequestAPI_PlasmaTechController = new RequestAPI_PlasmaTechController();



        [HttpPost]
        public void Index(Req_Vendor_API_Airlines_DownloadTicket_Requests user)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            Res_Vendor_API_Airlines_Download_Ticket_Requests result = new Res_Vendor_API_Airlines_Download_Ticket_Requests();
           // var response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);

           // var userInput = getRawPostData().Result;


            if (user.LogID.StartsWith("Plasma_"))
            {
                var flightID = user.LogID.Replace("Plasma_", "");
                string fullpath = string.Empty;

                AddFlightBookingDetails outobject = new AddFlightBookingDetails();
                GetFlightBookingDetails_Plasma inobject = new GetFlightBookingDetails_Plasma();

                inobject.MemberId = Convert.ToInt64(user.MemberId);
                inobject.Flightid = flightID;
                inobject.LogIDs = user.LogID;

                AddFlightBookingDetails resGetRecord = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord("sp_FlightBookingDetails_Get_plasma", inobject, outobject);
                GetVendor_API_Airlines_Lookup objResDownload = VendorApi_CommonHelper.RequestPlasmaAirlines_DOWNLOAD_TICKET(resGetRecord.BookingId, user.MemberId, flightID, resGetRecord.ContactName, resGetRecord.ContactPhone, resGetRecord.ContactEmail, "/Content/FlightTicketPDF", ref fullpath);


                ///Content/FlightTicketPDF/



                //HttpResponseMessage result2 = new HttpResponseMessage(HttpStatusCode.OK);
                //var stream2 = new FileStream(path, FileMode.Open);
                //result2.Content = new StreamContent(stream2);
                //result2.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //result2.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
                //result2.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                //result2.Content.Headers.ContentLength = stream2.Length;
                ////new HttpResponse().write
                //return result2;

                try
                {
                    var filename = Path.GetFileName(fullpath);
                    var path = System.Web.HttpContext.Current.Server.MapPath("~/Content/FlightTicketPDF/" + filename);

                    log.Info("TICKET PATH:" + path);

                    //HttpResponseMessage result2 = new HttpResponseMessage(HttpStatusCode.OK);
                    //var stream2 = new FileStream(path, FileMode.Open);
                    //result2.Content = new StreamContent(stream2);
                    //result2.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");//("attachment");
                    //result2.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
                    //result2.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    //result2.Content.Headers.ContentLength = stream2.Length;
                    //result2.Content.Headers.Add("myHeader", path);
                    ////new HttpResponse().write
                    //return result2;


                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(path));
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.WriteFile(Server.MapPath("/Content/FlightTicketPDF/" + filename ));
                    //Response.End();
                    Response.Flush(); // Sends all currently buffered output to the client.
                    Response.SuppressContent = true;
                    return;
                }
                catch (Exception ex)
                {
                    log.Info("TICKET ERROR: " + ex.ToString());
                    throw;
                }

               





                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=" + uw.TransactionUniqueId + ".pdf");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.WriteFile(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"));
                ////Response.End();
                //Response.Flush(); // Sends all currently buffered output to the client.
                //Response.SuppressContent = true;

                //result.FilePath = Common.LiveApiUrl + objResDownload.FilePath;
                //result.status = true;
                //result.ReponseCode = 1;
                //result.responseMessage = "Success";
                //result.Message = "Success";

                //return Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                log.Info($"{System.DateTime.Now.ToString()} inside download-flight-ticket" + Environment.NewLine);
            }

            CommonResponse cres = new CommonResponse();

            
            try
            {
               
                        GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_DOWNLOAD_TICKET(user.MemberId, user.Reference, user.LogID, user.Version, user.DeviceCode, user.PlatForm, false, "", ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            ///Content/FlightTicketPDF/
                            var filename = Path.GetFileName(objRes.FilePath);
                            var path = System.Web.HttpContext.Current.Server.MapPath("~/Content/FlightTicketPDF/" + filename);


                            //HttpResponseMessage result2 = new HttpResponseMessage(HttpStatusCode.OK);
                            //var stream2 = new FileStream(path, FileMode.Open);
                            //result2.Content = new StreamContent(stream2);
                            //result2.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                            //result2.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
                            //result2.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                            //result2.Content.Headers.ContentLength = stream2.Length;
                            //return result2;

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(path));
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.WriteFile(Server.MapPath("/Content/FlightTicketPDF/" + filename ));
                    //Response.End();
                    Response.Flush(); // Sends all currently buffered output to the client.
                    Response.SuppressContent = true;


                    //result.ReponseCode = 1;
                    //result.FilePath = objRes.FilePath;
                    //result.status = objRes.status;
                    //result.Message = "success";
                    //response.StatusCode = HttpStatusCode.Accepted;
                    //response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                }

            }
                
            
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                log.Error($"{System.DateTime.Now.ToString()} download-flight-ticket {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} download-flight-ticket {ex.ToString()} " + Environment.NewLine);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}