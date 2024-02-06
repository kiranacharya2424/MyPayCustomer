﻿using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
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
    public class MPCoinsImportController : BaseAdminSessionController
    {
        // GET: MPCoinsImport
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // Post: MPCoinsImport
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
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var excel = new ExcelPackage(fileUpload.InputStream))
                {
                    var tbl = new DataTable();
                    //var ws= excel.Workbook.Worksheets[1];
                    //ExcelWorksheet ws = excel.Workbook.Worksheets[0];
                    var ws = excel.Workbook.Worksheets.First();
                    var hasHeader = true;  // adjust accordingly
                                           // add DataColumns to DataTable
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text
                            : String.Format("Column {0}", firstRowCell.Start.Column));

                    tbl.Columns.Add("Message", typeof(string));

                    // add DataRows to DataTable
                    int startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.NewRow();

                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                        AddUser outuser = new AddUser();
                        GetUser inuser = new GetUser();
                        if (row[1].ToString() != "")
                        {
                            inuser.ContactNumber = row[1].ToString();
                            AddUser resuser = RepCRUD<GetUser, AddUser>.GetRecord(nameof(Common.StoreProcedures.sp_Users_Get), inuser, outuser);
                            if (resuser.Id > 0)
                            {
                                string TransactionUniqueID = string.Empty;
                                Guid TransactionGuid = Guid.NewGuid();
                                TransactionUniqueID = new CommonHelpers().GenerateUniqueId();

                                decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalRewardPoints) + Convert.ToDecimal(row[2].ToString()));
                                RewardPointTransactions res_transaction = new RewardPointTransactions();
                                res_transaction.TransactionUniqueId = TransactionUniqueID;

                                if (!res_transaction.GetRecord())
                                {
                                    res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                                    res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                    res_transaction.Amount = Convert.ToDecimal(row[2].ToString());
                                    res_transaction.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    res_transaction.CreatedByName = Session["AdminUserName"].ToString();
                                    res_transaction.CurrentBalance = WalletBalance;
                                    res_transaction.TransactionUniqueId = TransactionUniqueID;
                                    res_transaction.Remarks = "Gift MPCoins Added";
                                    res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Gift_MPCoins;
                                    res_transaction.Description = "Gift MPCoins Added";
                                    //res_transaction.Purpose = "Agent Cashback";
                                    res_transaction.VendorTransactionId = TransactionUniqueID;
                                    res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                    res_transaction.Reference = TransactionGuid.ToString();
                                    res_transaction.IsApprovedByAdmin = true;
                                    res_transaction.IsActive = true;
                                    //res_transaction.Platform = "Web";
                                    //res_transaction.CardNumber = cardnumber;
                                    //res_transaction.CardType = card_type_name;
                                    //res_transaction.ExpiryDate = req_card_expiry_date;
                                    res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                    //res_transaction.GatewayStatus = decision;
                                    //res_transaction.ServiceCharge = res.ServiceCharges;
                                    //res_transaction.NetAmount = res_transaction.Amount;
                                    //res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                    //res_transaction.VendorType = (int)VendorApi_CommonHelper.VendoTypes.MyPay;
                                    if (res_transaction.Add())
                                    {
                                        AddGiftMPCoinsHistory outobject = new AddGiftMPCoinsHistory();
                                        GetGiftMPCoinsHistory inobject = new GetGiftMPCoinsHistory();
                                        outobject.ContactNumber = resuser.ContactNumber;
                                        outobject.CreatedBy = res_transaction.CreatedBy;
                                        outobject.CreatedByName = res_transaction.CreatedByName;
                                        outobject.IsActive = res_transaction.IsActive;
                                        outobject.IsApprovedByAdmin = res_transaction.IsApprovedByAdmin;
                                        outobject.MemberId = res_transaction.MemberId;
                                        outobject.MemberName = res_transaction.MemberName;
                                        outobject.Prize = res_transaction.Amount;
                                        outobject.Remarks = res_transaction.Remarks;
                                        outobject.Sign = res_transaction.Sign;
                                        outobject.TransactionId = res_transaction.TransactionUniqueId;
                                        outobject.Status = (int)AddGiftMPCoinsHistory.GiftMPCoinsStatus.Success;
                                        Int64 Id = RepCRUD<AddGiftMPCoinsHistory, GetGiftMPCoinsHistory>.Insert(outobject, "giftmpcoinshistory");
                                        string Title = "Gift MPCoins Added";
                                        string Message = "Hello " + resuser.FirstName + "! Gift MPCoins Added Successfully By Transaction ID :" + TransactionUniqueID;
                                        Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.Gift_MPCoins, resuser.MemberId, Title, Message);
                                        Common.AddLogs("Transaction Completed Successfully for Gift MPCoins By This TXNId:" + TransactionUniqueID, true, Convert.ToInt32(AddLog.LogType.RewardPoints), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode, (int)AddLog.LogActivityEnum.Gift_MPCoins);

                                        ViewBag.Message = "Transaction Completed Successfully for Gift MPCoins.";
                                        row[3] = "Success";
                                    }
                                }
                                else
                                {
                                    Common.AddLogs("Gift MPCoins Transaction Already Updated By This TXNId:" + TransactionUniqueID, true, Convert.ToInt32(AddLog.LogType.RewardPoints), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode, (int)AddLog.LogActivityEnum.Gift_MPCoins);

                                    ViewBag.Message = "Transaction Completed Successfully for Gift MPCoins.";
                                }
                            }
                            else
                            {
                                AddGiftMPCoinsHistory outobject = new AddGiftMPCoinsHistory();
                                GetGiftMPCoinsHistory inobject = new GetGiftMPCoinsHistory();
                                outobject.ContactNumber = row[1].ToString();
                                outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                outobject.CreatedByName = Session["AdminUserName"].ToString();
                                outobject.IsActive = true;
                                outobject.IsApprovedByAdmin = true;
                                outobject.Prize = Convert.ToDecimal(row[2].ToString());
                                outobject.Status = outobject.Status = (int)AddGiftMPCoinsHistory.GiftMPCoinsStatus.Failed;
                                outobject.Remarks = "Error: Contact Number does not exist.";
                                Int64 Id = RepCRUD<AddGiftMPCoinsHistory, GetGiftMPCoinsHistory>.Insert(outobject, "giftmpcoinshistory");
                                Common.AddLogs("Gift MPCoins History Error: Contact Number does not exist" + row[1].ToString(), true, Convert.ToInt32(AddLog.LogType.RewardPoints), 0, "", true, resuser.PlatForm, resuser.DeviceCode, (int)AddLog.LogActivityEnum.Gift_MPCoins);

                                row[3] = "Error: Contact Number does not exist";
                            }
                            tbl.Rows.Add(row);
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
            return View();
        }

        // GET: GiftMPCoinsHistoryReport
        [Authorize]
        public ActionResult GiftMPCoinsHistory()
        {
            AddGiftMPCoinsHistory model = new AddGiftMPCoinsHistory();
            return View(model);
        }

        [Authorize]
        public JsonResult GetGiftMPCoinsHistoryLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("ContactNo");
            columns.Add("TransactionId");
            columns.Add("Prize");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("Remarks");
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
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            string MemberId = context.Request.Form["MemberId"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string MemberName = context.Request.Form["MemberName"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];

            List<AddGiftMPCoinsHistory> trans = new List<AddGiftMPCoinsHistory>();

            GetGiftMPCoinsHistory w = new GetGiftMPCoinsHistory();
            w.ContactNumber = ContactNumber;
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.MemberName = MemberName;
            w.TransactionId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.CheckDelete = 0;
            w.CheckActive = 1;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddGiftMPCoinsHistory
                     {
                         Sno = row["Sno"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         Prize = Convert.ToDecimal(row["Prize"].ToString()),
                         TransactionId = row["TransactionId"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"].ToString()),
                         MemberName = row["MemberName"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         StatusName = @Enum.GetName(typeof(AddGiftMPCoinsHistory.GiftMPCoinsStatus), Convert.ToInt64(row["Status"])),
                         CreatedDatedt = row["IndiaDate"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddGiftMPCoinsHistory>> objDataTableResponse = new DataTableResponse<List<AddGiftMPCoinsHistory>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        [HttpPost]
        public ActionResult ExportExcel(Int32 Take, Int32 Skip, string Sort, string SortOrder, Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string Status)
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("ContactNo");
            columns.Add("TransactionId");
            columns.Add("Prize");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("Remarks");
            columns.Add("Status");
            Sort = columns[Convert.ToInt32(Sort)];
            var fileName = "GiftMPCoins-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetGiftMPCoinsHistory w = new GetGiftMPCoinsHistory();
            w.ContactNumber = ContactNo;
            w.MemberId = Convert.ToInt64(MemberId);
            w.MemberName = Name;
            w.TransactionId = TxnId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.Status = Convert.ToInt32(Status);

            DataTable dt = w.GetData(Sort, SortOrder, Skip, Take, "");

            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "IndiaDate", "ContactNumber", "TransactionId", "Prize", "MemberId", "MemberName", "Remarks", "Status");
                //dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["ContactNumber"].ColumnName = "Contact No";
                dt.Columns["TransactionId"].ColumnName = "Transaction Id";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["MemberName"].ColumnName = "Member Name";
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