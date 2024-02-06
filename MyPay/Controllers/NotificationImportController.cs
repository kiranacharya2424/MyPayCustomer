using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class NotificationImportController : BaseAdminSessionController
    {
        // GET: NotificationImport
        public ActionResult Index()
        {
            return View();
        }

        // Post: NotificationImport
        [HttpPost]
        [Authorize]
        public ActionResult Index(HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.Message = "You did not specify a file to upload.";
            }
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                var excelfilename = fileUpload.FileName.Split('.');
                var fileName = excelfilename[0] + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xlsx";

                //Save the file to server temp folder
                string FullPath = Path.Combine(Server.MapPath("~/Content/NotificationExcelImport/"), fileName);
                fileUpload.SaveAs(FullPath);

                string ExcelURL = string.Empty;
                if(Common.ApplicationEnvironment.IsProduction)
                {
                    ExcelURL = Common.LiveSiteUrl + "/Content/NotificationExcelImport/" + fileName;
                }
                else
                {
                    ExcelURL = Common.TestSiteUrl + "/Content/NotificationExcelImport/" + fileName;
                }
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var excel = new ExcelPackage(fileUpload.InputStream))
                {
                    var tbl = new DataTable();
                    //var ws= excel.Workbook.Worksheets[1];
                    //ExcelWorksheet ws = excel.Workbook.Worksheets[0];
                    var ws = excel.Workbook.Worksheets.First();

                    var rowscount = ws.Dimension.End.Row;
                    if (rowscount <= 1000)
                    {
                        var hasHeader = true;  // adjust accordingly

                        // add DataColumns to DataTable
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text
                                : String.Format("Column {0}", firstRowCell.Start.Column));

                        tbl.Columns.Add("ReturnMessage", typeof(string));
                        List<AddNotification> resNotificationlist = new List<AddNotification>();
                        string Title = "";
                        string Message = "";
                        string TitlePre = "";
                        string MessagePre = "";
                        string NotificationType = "";
                        string DeviceCodeCSV = "";
                        // add DataRows to DataTable
                        int startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            DataRow row = tbl.NewRow();

                            foreach (var cell in wsRow)
                            {
                                if (tbl.Columns.Count > (cell.Start.Column - 1))
                                {
                                    row[cell.Start.Column - 1] = cell.Text;
                                }
                            }
                            if (row[1].ToString().Trim() != string.Empty)
                            {
                                Title = row[2].ToString();
                                Message = row[3].ToString();
                                if (!string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Message))
                                {
                                    if (!string.IsNullOrEmpty(TitlePre) && !string.IsNullOrEmpty(MessagePre))
                                    {
                                        if (Title != TitlePre || Message != MessagePre)
                                        {
                                            ViewBag.Message = "Please note that Notification Title and Message should be same in a Notification Excel import";
                                        }
                                    }
                                }
                                else
                                {
                                    ViewBag.Message = "Title or Message cannot be blank.";
                                }
                                if (string.IsNullOrEmpty(ViewBag.Message))
                                {
                                    NotificationType = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Excel_Notification);
                                    if (row[1].ToString() != "")
                                    {

                                        AddNotification res_notification = new AddNotification(); 
                                        AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                                        GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                                        inuser.ContactNumber = row[1].ToString();
                                        AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                                        if (resuser.Id > 0)
                                        {
                                            DeviceCodeCSV += resuser.DeviceCode + ",";
                                            res_notification.MemberId = resuser.MemberId;
                                            res_notification.Title = row[2].ToString();
                                            res_notification.NotificationMessage = row[3].ToString();
                                            if (!string.IsNullOrEmpty(resuser.DeviceCode))
                                            {
                                                res_notification.SentStatus = 1;
                                            }
                                            else
                                            {
                                                res_notification.SentStatus = 0;
                                            }

                                            res_notification.CreatedDate = DateTime.UtcNow;
                                            res_notification.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                            res_notification.CreatedByName = Session["AdminUserName"].ToString();
                                            res_notification.IsApprovedByAdmin = true;
                                            res_notification.IsActive = true;
                                            res_notification.NotificationType = (int)VendorApi_CommonHelper.KhaltiAPIName.Excel_Notification;
                                            res_notification.NotificationDescription = "Bulk notification insert by " + Session["AdminUserName"].ToString();

                                            if (!string.IsNullOrEmpty(resuser.DeviceCode))
                                            {
                                                row[4] = "Success";
                                            }
                                            else
                                            {
                                                row[4] = "User Logged Out From Device.";
                                            }

                                        }
                                        else
                                        {
                                            row[4] = "Error: Contact Number does not exist";
                                        }
                                        tbl.Rows.Add(row);
                                        resNotificationlist.Add(res_notification);
                                        TitlePre = Title;
                                        MessagePre = Message;
                                    }
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(ViewBag.Message))
                        {
                            if (resNotificationlist.Count > 0)
                            {
                                MyPay.Models.Common.Notifications.SentNotifications.ExecuteBulkExcelNotificationFromAdmin(Title, Message, NotificationType, DeviceCodeCSV);
                                Int64 Id = RepCRUD<AddNotification, GetNotification>.InsertList(resNotificationlist, "notification");
                                if (Id > 0)
                                {
                                    Common.AddLogs("Bulk Notification From Excel Import done by " + Session["AdminUserName"].ToString() + " at " + DateTime.UtcNow + ". Excel sheet file : " + ExcelURL, true, Convert.ToInt32(AddLog.LogType.ExcelNotification), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), true, "web", "", (int)AddLog.LogActivityEnum.MyPay_Notification);
                                }
                                else
                                {
                                    Common.AddLogs("Bulk Notification Not Send Successfully", true, Convert.ToInt32(AddLog.LogType.ExcelNotification), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), true, "web", "", (int)AddLog.LogActivityEnum.MyPay_Notification);
                                }
                            }
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                string filename = DateTime.Now.ToShortDateString().Replace("/", "");
                                var wss = wb.Worksheets.Add(tbl, "Report(" + DateTime.Now.ToShortDateString().Replace("/", "") + ")");
                                wss.Columns().AdjustToContents();  // Adjust column width
                                wss.Rows().AdjustToContents();

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToShortDateString().Replace("/", "") + ".xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Excel file does not contains more than 1000 records. Please upload another file with max. 1000 records.";
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult DownloadSampleFormat(string fileName)
        {
            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/Content/NotificationImportSampleFormat/"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }
    }
}