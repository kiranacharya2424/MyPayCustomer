using ClosedXML.Excel;
using DeviceId;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace MyPay.Controllers
{
    public class FlightReportController : Controller
    {
        // GET: FlightReport
        [Authorize]
        public ActionResult Index(string MemberId, string BookingId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.BookingId = string.IsNullOrEmpty(BookingId) ? "" : BookingId;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Trip Type",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "OneWay Trip",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Round Trip",
                Value = "2"
            });
            ViewBag.TripType = items;

            return View();
        }

        [Authorize]
        public JsonResult GetFlightBookingLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("BookingDate");
            columns.Add("MemberId");
            columns.Add("BookingId");
            columns.Add("TripType");
            columns.Add("FlightType");
            columns.Add("ContactName");
            columns.Add("ContactPhone");
            columns.Add("FlightIssued");
            columns.Add("FlightBooked");
            columns.Add("Refundable");
            columns.Add("PnrNumber");
            columns.Add("Pax");
            columns.Add("Flightno");
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
            string BookingId = context.Request.Form["BookingId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string TripType = context.Request.Form["TripType"];
            string PnrNumber = context.Request.Form["PnrNumber"];
            string flightno = context.Request.Form["Flightno"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddFlightBookingDetails> trans = new List<AddFlightBookingDetails>();

            GetFlightBookingDetails w = new GetFlightBookingDetails();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            if (!string.IsNullOrEmpty(BookingId))
            {
                w.BookingId = Convert.ToInt64(BookingId);
            }
            w.CheckFlightBooked = 1;
            w.CheckInbound = 0;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.PnrNumber = PnrNumber;
            w.Flightno = flightno;
            if (TripType == "1")
            {
                w.TripType = "O";
            }
            else if (TripType == "2")
            {
                w.TripType = "R";
            }
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows
                     select new AddFlightBookingDetails
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         BookingId = Convert.ToInt64(row["BookingId"]),
                         Adult = Convert.ToInt32(row["Adult"]),
                         Aircrafttype = row["Aircrafttype"].ToString(),
                         Airlinename = row["Airlinename"].ToString(),
                         Adultfare = Convert.ToDecimal(row["Adultfare"]),
                         Childfare = Convert.ToDecimal(row["Childfare"]),
                         Infantfare = Convert.ToDecimal(row["Infantfare"]),
                         Faretotal = Convert.ToDecimal(row["Faretotal"]),
                         CreatedDatedt = row["CreatedDateDt"].ToString(),
                         TripType = row["TripTypeName"].ToString(),
                         FlightType = row["FlightTypeName"].ToString(),
                         Child = Convert.ToInt32(row["Child"]),
                         Departure = row["Departure"].ToString(),
                         Departuretime = row["Departuretime"].ToString(),
                         Arrival = row["Arrival"].ToString(),
                         Arrivaltime = row["Arrivaltime"].ToString(),
                         Tax = Convert.ToDecimal(row["Tax"].ToString()),
                         Flightclasscode = row["Flightclasscode"].ToString(),
                         Flightno = row["Flightno"].ToString(),
                         Freebaggage = row["Freebaggage"].ToString(),
                         Flightdatedt = row["Flightdatedt"].ToString(),
                         ContactName = row["ContactName"].ToString(),
                         ContactPhone = row["ContactPhone"].ToString(),
                         IsFlightBooked = Convert.ToBoolean(row["IsFlightBooked"].ToString()),
                         IsInbound = Convert.ToBoolean(row["IsInbound"].ToString()),
                         IsFlightIssued = Convert.ToBoolean(row["IsFlightIssued"].ToString()),
                         Refundable = Convert.ToBoolean(row["Refundable"].ToString()),
                         PnrNumber = row["PnrNumber"].ToString(),
                         Pax = row["paxCount"].ToString(),
                         BookingCreateddt = row["BookingCreateddt"].ToString(),
                         Fuelsurcharge = Convert.ToDecimal(row["Fuelsurcharge"].ToString()),
                         Resfare = Convert.ToDecimal(row["Resfare"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddFlightBookingDetails>> objDataTableResponse = new DataTableResponse<List<AddFlightBookingDetails>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [Authorize]
        public JsonResult GetFlightBookingDetail(Int64 BookingId)
        {
            AddFlightBookingDetails outobject = new AddFlightBookingDetails();
            GetFlightBookingDetails inobject = new GetFlightBookingDetails();
            inobject.BookingId = BookingId;
            inobject.CheckFlightBooked = 1;
            List<AddFlightBookingDetails> list = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecordList(Models.Common.Common.StoreProcedures.sp_FlightBookingDetails_Get, inobject, outobject);
            return Json(list);

        }

        [Authorize]
        public JsonResult GetFlightTxnDetail(string BookingId)
        {
            AddTransaction outobject = new AddTransaction();
            GetTransaction inobject = new GetTransaction();
            inobject.CustomerID = BookingId;
            inobject.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
            AddTransaction res = RepCRUD<GetTransaction, AddTransaction>.GetRecord(Models.Common.Common.StoreProcedures.sp_WalletTransactions_Get, inobject, outobject);

            return Json(res);
        }



        [Authorize]
        public JsonResult GetFlightIssuedStatusCheck(string BookingId)
        {
            AddTransaction outobject = new AddTransaction();
            GetTransaction inobject = new GetTransaction();
            inobject.CustomerID = BookingId;
            inobject.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
            AddTransaction res = RepCRUD<GetTransaction, AddTransaction>.GetRecord(Models.Common.Common.StoreProcedures.sp_WalletTransactions_Get, inobject, outobject);
            if (res != null && res.Id > 0)
            {
                GetVendor_API_Airlines_CheckStatus objRes = new GetVendor_API_Airlines_CheckStatus();
                string msg = RepKhalti.RequestServiceGroup_AIRLINES_FLIGHT_STATUS(res.MemberId, res.Reference, "1", res.DeviceCode, res.Platform, ref objRes);
                res.Remarks = msg;
            }
            return Json(res);
        }

        // GET: FlightPassengerDetail
        [Authorize]
        public ActionResult FlightPassengerDetail(string BookingId)
        {
            ViewBag.BookingId = string.IsNullOrEmpty(BookingId) ? "" : BookingId;

            return View();
        }

        [Authorize]
        public JsonResult GetFlightPassengerLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("BookingId");
            columns.Add("FirstName");
            columns.Add("LastName");
            columns.Add("Type");
            columns.Add("Gender");
            columns.Add("Nationality");
            columns.Add("TicketNo");
            columns.Add("InboundTicketNo");
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
            string FirstName = context.Request.Form["FirstName"];
            string LastName = context.Request.Form["LastName"];
            string BookingId = context.Request.Form["BookingId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddFlightPassengersDetails> trans = new List<AddFlightPassengersDetails>();

            GetFlightPassengersDetails w = new GetFlightPassengersDetails();
            if (!string.IsNullOrEmpty(BookingId))
            {
                w.BookingId = Convert.ToInt64(BookingId);
            }
            w.FirstName = FirstName;
            w.LastName = LastName;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows
                     select new AddFlightPassengersDetails
                     {
                         Sno = row["Sno"].ToString(),
                         BookingId = Convert.ToInt64(row["BookingId"]),
                         Firstname = row["Firstname"].ToString(),
                         Lastname = row["Lastname"].ToString(),
                         Type = row["Type"].ToString(),
                         Gender = row["Gender"].ToString(),
                         Nationality = row["Nationality"].ToString(),
                         TicketNo = row["TicketNo"].ToString(),
                         InboundTicketNo = row["InboundTicketNo"].ToString(),
                         CreatedDatedt = row["CreatedDateDt"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddFlightPassengersDetails>> objDataTableResponse = new DataTableResponse<List<AddFlightPassengersDetails>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };
            return Json(objDataTableResponse);
        }


        [Authorize]
        public ActionResult GetFlightTicketDownload(string BookingId)
        {
            var result = string.Empty;
            AddTransaction outobject = new AddTransaction();
            GetTransaction inobject = new GetTransaction();
            inobject.CustomerID = BookingId;

            GetApiFlightSwitchSettings inobj = new GetApiFlightSwitchSettings();
            inobj.IsActive = 1;
            GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobj.IsActive.ToString());

            if (resSwitchType.FlightSwitchType == 1)
            {
                inobject.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
            }
            else
            {
                inobject.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay;
            }

            GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
            AddTransaction res = RepCRUD<GetTransaction, AddTransaction>.GetRecord(Models.Common.Common.StoreProcedures.sp_WalletTransactions_Get, inobject, outobject);
            if (res != null && res.Id > 0)
            {
                AddFlightBookingDetails outobjectFlightBookingDetails = new AddFlightBookingDetails();
                GetFlightBookingDetails inobjectFlightBookingDetails = new GetFlightBookingDetails();
                inobjectFlightBookingDetails.MemberId = Convert.ToInt64(res.MemberId);
                inobjectFlightBookingDetails.CheckFlightBooked = 1;
                inobjectFlightBookingDetails.BookingId = Convert.ToInt64(res.CustomerID);
                inobjectFlightBookingDetails.CheckInbound = 0;
                AddFlightBookingDetails resFlightBookingDetails = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, inobjectFlightBookingDetails, outobjectFlightBookingDetails);
                if (resFlightBookingDetails != null && resFlightBookingDetails.Id > 0)
                {

                    string deviceId = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();

                    string msg = RepKhalti.RequestServiceGroup_AIRLINES_DOWNLOAD_TICKET(res.MemberId, res.Reference, resFlightBookingDetails.LogIDs, "1", deviceId, res.Platform, false, "", ref objRes);

                    if (msg.ToLower() == "success")
                    {
                        GetVendor_API_Airlines_Lookup obj = JsonConvert.DeserializeObject<GetVendor_API_Airlines_Lookup>(JsonConvert.SerializeObject(objRes));
                        string pathName = Common.LiveSiteUrl + obj.FilePath;
                        string fileName = Path.GetFileName(pathName);
                        try
                        {
                            using (WebClient client = new WebClient())
                            {
                                byte[] fileBytes = client.DownloadData(pathName);
                                return File(fileBytes, "application/pdf", fileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            TempData["Message"] = ex.Message;
                            
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Invalid API Request.";
                      
                    }
                }
                else
                {
                    TempData["Message"] = "Flight Not Issued.";
                   
                }
            }
            TempData["Message"] = "Record Not Found. Flight Not Issued or Something Went Wrong.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcel(Int32 Take, Int32 Skip, string Sort, string SortOrder, Int64 MemberId, Int64 BookingId, string FromDate, string ToDate, string TripType)
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("BookingDate");
            columns.Add("MemberId");
            columns.Add("BookingId");
            columns.Add("TripType");
            columns.Add("FlightType");
            columns.Add("ContactName");
            columns.Add("ContactPhone");
            columns.Add("FlightIssued");
            columns.Add("FlightBooked");
            columns.Add("Refundable");
            columns.Add("PnrNumber");
           // columns.Add("Flightno");
            Sort = columns[Convert.ToInt32(Sort)];
            var fileName = "FlightReport-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetFlightBookingDetails w = new GetFlightBookingDetails();
            w.MemberId = Convert.ToInt64(MemberId);
            w.BookingId = Convert.ToInt64(BookingId);

            w.CheckFlightBooked = 1;
            w.CheckInbound = 0;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            if (TripType == "1")
            {
                w.TripType = "O";
            }
            else if (TripType == "2")
            {
                w.TripType = "R";
            }
            DataTable dt = w.GetData(Sort, SortOrder, Skip, Take, "");

            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "BookingCreateddt", "MemberId", "BookingId", "TripTypeName", "FlightTypeName", "ContactName", "ContactPhone", "FlightIssuedStatus", "FlightBookingStatus", "RefundableName", "PnrNumber");
                //dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["BookingCreateddt"].ColumnName = "Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["BookingId"].ColumnName = "Booking Id";
                dt.Columns["TripTypeName"].ColumnName = "Trip Type";
                dt.Columns["FlightTypeName"].ColumnName = "Flight Type";
                dt.Columns["ContactName"].ColumnName = "Contact Name";
                dt.Columns["ContactPhone"].ColumnName = "Contact Phone";
                dt.Columns["FlightIssuedStatus"].ColumnName = "Flight Issued Status";
                dt.Columns["FlightBookingStatus"].ColumnName = "Flight Booking Status";
               // dt.Columns["PnrNumber"].ColumnName = "Pnr Number";
                dt.Columns["Flightno"].ColumnName = "Flight Number";
                dt.Columns["RefundableName"].ColumnName = "Refundable";
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "User(" + DateTime.Now.ToString("MMM") + ")");
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    ws.Columns().AdjustToContents();  // Adjust column width
                    ws.Rows().AdjustToContents();
                    wb.SaveAs(fullPath);
                }
            }
            var errorMessage = "you can return the errors here!";
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Download(string fileName)
        {
            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }
    }
}