using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;

namespace MyPay.Controllers
{
    public class AdminTransactionsReadOnlyController : BaseAdminSessionController
    {
        // GET: AdminTransactions
        [Authorize]
        public ActionResult Index(string MemberId, string TransactionId, string Reference, string ParentTransactionId, string GatewayTransactionId, string SubscriberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
            ViewBag.Reference = string.IsNullOrEmpty(Reference) ? "" : Reference;
            //ViewBag.ParentTransactionId = string.IsNullOrEmpty(ParentTransactionId) ? "" : ParentTransactionId;
            ViewBag.GatewayTransactionId = string.IsNullOrEmpty(GatewayTransactionId) ? "" : GatewayTransactionId;
            ViewBag.ParentTransactionId = string.IsNullOrEmpty(ParentTransactionId) ? "" : ParentTransactionId;
            ViewBag.SubscriberId = string.IsNullOrEmpty(SubscriberId) ? "" : SubscriberId;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Success",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Pending",
                Value = "2"
            });
            items.Add(new SelectListItem
            {
                Text = "Failed",
                Value = "3"
            });
            items.Add(new SelectListItem
            {
                Text = "Refund",
                Value = "9"
            });
            items.Add(new SelectListItem
            {
                Text = "Error",
                Value = "7"
            });
            ViewBag.Status = items;

            List<SelectListItem> items_date = new List<SelectListItem>();
            items_date.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0",
                Selected = true
            });
            items_date.Add(new SelectListItem
            {
                Text = "Today",
                Value = "1"
            });
            items_date.Add(new SelectListItem
            {
                Text = "Weekly",
                Value = "2"
            });
            items_date.Add(new SelectListItem
            {
                Text = "Monthly",
                Value = "3"
            });
            ViewBag.DayWise = items_date;

            List<SelectListItem> sign = new List<SelectListItem>();
            sign.Add(new SelectListItem
            {
                Text = "Select Sign",
                Value = "0",
                Selected = true
            });
            sign.Add(new SelectListItem
            {
                Text = "Credit",
                Value = "1"
            });
            sign.Add(new SelectListItem
            {
                Text = "Debit",
                Value = "2"
            });
            ViewBag.Sign = sign;
            WalletTransaction model = new WalletTransaction();

            List<SelectListItem> khaltienum = CommonHelpers.GetSelectList_KhaltiEnum(model);
            ViewBag.Type = khaltienum;

            //List<SelectListItem> walletenum = CommonHelpers.GetSelectList_WalletTypeEnum();

            List<SelectListItem> wallet = new List<SelectListItem>();
            wallet.Add(new SelectListItem
            {
                Text = "Select WalletType",
                Value = "0",
                Selected = true
            });
            wallet.Add(new SelectListItem
            {
                Text = "Wallet",
                Value = "1"
            });
            wallet.Add(new SelectListItem
            {
                Text = "Bank",
                Value = "2"
            });
            wallet.Add(new SelectListItem
            {
                Text = "Card",
                Value = "3"
            });
            wallet.Add(new SelectListItem
            {
                Text = "MPCoins",
                Value = "4"
            });
            ViewBag.WalletType = wallet;



            List<SelectListItem> VendoTypes = CommonHelpers.GetSelectList_VendoTypes();
            ViewBag.VendoTypes = VendoTypes;

            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }

        [Authorize]
        public JsonResult GetTransactionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("RequestId");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("Service");
            columns.Add("ServiceCharge");
            columns.Add("Cashback");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
            columns.Add("UpdateBy");
            columns.Add("AvailableBalance");
            columns.Add("PreviousBalance");
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
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string DayWise = context.Request.Form["Today"];
            string Sign = context.Request.Form["Sign"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            string Reference = context.Request.Form["Reference"];
            string CustomerId = context.Request.Form["CustomerId"];
            string Type = context.Request.Form["Type"];
            string TypeMultiple = context.Request.Form["TypeMultiple"];
            string WalletType = context.Request.Form["WalletType"];
            string VendorType = context.Request.Form["VendorType"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTransactionId;
            w.ParentTransactionId = ParentTransactionId;
            w.Reference = Reference;
            w.Sign = Convert.ToInt32(Sign);
            w.CustomerID = CustomerId;
            w.VendorType = Convert.ToInt32(VendorType);
            if (!string.IsNullOrEmpty(Type))
            {
                w.Type = Convert.ToInt32(Type);
            }
            if (!string.IsNullOrEmpty(TypeMultiple))
            {
                Type = "0";
                w.TypeMultiple = TypeMultiple;
            }
            if (!string.IsNullOrEmpty(WalletType))
            {
                if (WalletType == "1")
                {
                    w.WalletTypeMultiple = "0,1";
                }
                else
                {
                    w.WalletType = Convert.ToInt32(WalletType);
                }
            }
            if (DayWise != "" && DayWise != "0")
            {
                if (DayWise == "1")
                {
                    w.Today = "Today";
                }
                else if (DayWise == "2")
                {
                    w.Weekly = "Weekly";
                }
                else if (DayWise == "3")
                {
                    w.Monthly = "Monthly";
                }
                else
                {
                    w.Today = "";
                    w.Monthly = "";
                    w.Weekly = "";
                }
            }
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);
             
            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         ParentTransactionId = row["ParentTransactionId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         Type = Convert.ToInt32(row["Type"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         UpdateByName = row["UpdateByName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         CashBack = Convert.ToDecimal(row["CashBack"]),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         VendorType = Convert.ToInt32(row["VendorType"]),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         IpAddress = row["IpAddress"].ToString(),
                         Remarks = row["Remarks"].ToString().Replace("<",""),
                         Description = row["Description"].ToString().Replace("<",""),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         CustomerID = row["CustomerID"].ToString(),
                         SenderBankName = row["SenderBankName"].ToString(),
                         SenderAccountNo = row["SenderAccountNo"].ToString(),
                         RecieverContactNumber = row["RecieverContactNumber"].ToString(),
                         RecieverBankName = row["RecieverBankName"].ToString(),
                         RecieverAccountNo = row["RecieverAccountNo"].ToString(),
                         VendorJsonLookup = row["VendorJsonLookup"].ToString(),
                         WalletTypeName = @Enum.GetName(typeof(WalletTransactions.WalletTypes), Convert.ToInt64(row["WalletType"])).ToUpper(),
                         MPCoinsDebit = row["WalletType"].ToString() != "4" ? 0 : Convert.ToDecimal(row["MPCoinsDebit"].ToString()),
                         TransactionAmount = Convert.ToDecimal(row["TransactionAmount"].ToString()),
                         RewardPoint = row["Type"].ToString() == "20" ? 0 : Convert.ToDecimal(row["RewardPoint"].ToString()),
                         RewardPointBalance = row["Type"].ToString() == "20" ? 0 : (Convert.ToDecimal(row["RewardPointBalance"].ToString()) + Convert.ToDecimal(row["Status"].ToString() == "9" ? Convert.ToDecimal(row["MPCoinsDebit"].ToString()).ToString() : row["RewardPoint"].ToString())),
                         PreviousRewardPointBalance = Convert.ToDecimal(row["PreviousRewardPointBalance"].ToString()),
                         TotalCoinsCredit = Convert.ToDecimal(row["TotalCoinsCredit"].ToString()),
                         TotalCoinsDebit = Convert.ToDecimal(row["TotalCoinsDebit"].ToString()),
                         TotalServiceCharge = Convert.ToDecimal(row["TotalServiceCharge"].ToString()),
                         CouponCode = Convert.ToString(row["CouponCode"].ToString()),
                         CouponDiscount = Convert.ToDecimal(row["CouponDiscount"].ToString()),
                         Platform = row["Platform"].ToString(),
                         TotalCouponDiscount = Convert.ToDecimal(row["TotalCouponDiscount"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<WalletTransactions>> objDataTableResponse = new DataTableResponse<List<WalletTransactions>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }


        [HttpPost]
        [Authorize]
        public ActionResult ExportExcel(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string GatewayTxnId, string Status, string DayWise, string Sign)
        {
            var fileName = "Transactions-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            WalletTransactions w = new WalletTransactions();

            w.MemberId = Convert.ToInt64(MemberId);
            w.ContactNumber = ContactNo;
            w.MemberName = Name;
            w.TransactionUniqueId = TxnId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTxnId;
            w.Sign = Convert.ToInt32(Sign);
            w.Take = 50;
            w.Skip = 0;
            if (DayWise != "" && DayWise != "0")
            {
                if (DayWise == "1")
                {
                    w.Today = "Today";
                }
                else if (DayWise == "2")
                {
                    w.Weekly = "Weekly";
                }
                else if (DayWise == "3")
                {
                    w.Monthly = "Monthly";
                }
                else
                {
                    w.Today = "";
                    w.Monthly = "";
                    w.Weekly = "";
                }
            }
            DataTable dt = w.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "MemberName", "ContactNumber", "CustomerID", "Amount", "SignName", "Type", "SenderBankName", "SenderAccountNo", "RecieverBankName", "RecieverAccountNo", "ServiceCharge", "CashBack", "VendorType", "GatewayStatus", "StatusName", "UpdateByName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["MemberName"].ColumnName = "Name";
                dt.Columns["ContactNumber"].ColumnName = "Contact No";
                dt.Columns["CustomerID"].ColumnName = "Subscriber Id";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["Type"].ColumnName = "Service";
                dt.Columns["SenderBankName"].ColumnName = "Sender Bank Name";
                dt.Columns["SenderAccountNo"].ColumnName = "Sender AccountNo";
                dt.Columns["RecieverBankName"].ColumnName = "Reciever Bank Name";
                dt.Columns["RecieverAccountNo"].ColumnName = "Reciever AccountNo";
                dt.Columns["ServiceCharge"].ColumnName = "Service Charge";
                dt.Columns["CashBack"].ColumnName = "CashBack";
                dt.Columns["VendorType"].ColumnName = "Gateway Name";
                dt.Columns["GatewayStatus"].ColumnName = "Gateway Status";
                dt.Columns["StatusName"].ColumnName = "My Pay Status";
                dt.Columns["UpdateByName"].ColumnName = "Update By";
                dt.Columns["CurrentBalance"].ColumnName = "Available Balance(Rs)";
                dt.Columns["PreviousBalance"].ColumnName = "Previous Balance(Rs)";
                dt.Columns["IpAddress"].ColumnName = "Ip Address";
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "TxnReport(" + DateTime.Now.ToString("MMM") + ")");
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelAllServiceTxn(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string Status, string DayWise, string Service, string Reference, string VendorType)
        {
            var fileName = "AllServiceTxn-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            WalletTransactions w = new WalletTransactions();
            w.Take = 50;
            w.Skip = 0;
            w.MemberId = Convert.ToInt64(MemberId);
            w.ContactNumber = ContactNo;
            w.MemberName = Name;
            w.TransactionUniqueId = TxnId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.Status = Convert.ToInt32(Status);
            w.Reference = Reference;
            if (!String.IsNullOrEmpty(Service) && Service != "0")
            {
                w.Type = Convert.ToInt32(Service);
            }
            else
            {
                w.Type = -1;
            }
            if (DayWise != "" && DayWise != "0")
            {
                if (DayWise == "1")
                {
                    w.Today = "Today";
                }
                else if (DayWise == "2")
                {
                    w.Weekly = "Weekly";
                }
                else if (DayWise == "3")
                {
                    w.Monthly = "Monthly";
                }
                else
                {
                    w.Today = "";
                    w.Monthly = "";
                    w.Weekly = "";
                }
            }
            w.VendorType = Convert.ToInt32(VendorType);// (int)VendorApi_CommonHelper.VendoTypes.khalti;
            DataTable dt = w.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "MemberName", "ContactNumber", "Reference", "CustomerID", "Amount", "SignName", "Type", "ServiceCharge", "CashBack", "VendorType", "GatewayStatus", "StatusName", "UpdateByName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["MemberName"].ColumnName = "Name";
                dt.Columns["ContactNumber"].ColumnName = "Contact Number";
                dt.Columns["CustomerID"].ColumnName = "Subscriber";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["Type"].ColumnName = "Service";
                dt.Columns["StatusName"].ColumnName = "Txn Status";
                dt.Columns["VendorType"].ColumnName = "Gateway Name";
                dt.Columns["GatewayStatus"].ColumnName = "Gateway Status";
                dt.Columns["CurrentBalance"].ColumnName = "Available Balance(Rs)";
                dt.Columns["PreviousBalance"].ColumnName = "Previous Balance(Rs)";
                dt.Columns["IpAddress"].ColumnName = "Ip Address";

                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "TxnReport(" + DateTime.Now.ToString("MMM") + ")");
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelBankTransfer(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string GatewayTxnId, string Status, string DayWise, string Sign, string Reference)
        {
            var fileName = "BankTransfer-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            WalletTransactions w = new WalletTransactions();
            w.Take = 50;
            w.Skip = 0;
            w.MemberId = Convert.ToInt64(MemberId);
            w.ContactNumber = ContactNo;
            w.MemberName = Name;
            w.TransactionUniqueId = TxnId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTxnId;
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
            if (DayWise != "" && DayWise != "0")
            {
                if (DayWise == "1")
                {
                    w.Today = "Today";
                }
                else if (DayWise == "2")
                {
                    w.Weekly = "Weekly";
                }
                else if (DayWise == "3")
                {
                    w.Monthly = "Monthly";
                }
                else
                {
                    w.Today = "";
                    w.Monthly = "";
                    w.Weekly = "";
                }
            }
            DataTable dt = w.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "Reference", "TxnInstructionId", "BatchTransactionId", "RecieverAccountNo", "RecieverName", "RecieverBankCode", "RecieverBankName", "MemberName", "ContactNumber", "SenderBankName", "Amount", "SignName", "ServiceCharge", "VendorType", "GatewayStatus", "StatusName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["Reference"].ColumnName = "Tracker Id";
                dt.Columns["TxnInstructionId"].ColumnName = "Instruction Id";
                dt.Columns["BatchTransactionId"].ColumnName = "Batch Id";
                dt.Columns["RecieverAccountNo"].ColumnName = "Receiver AccountNo";
                dt.Columns["RecieverName"].ColumnName = "Receiver Name";
                dt.Columns["RecieverBankCode"].ColumnName = "Receiver Bank Code";
                dt.Columns["RecieverBankName"].ColumnName = "Receiver Bank Name";
                dt.Columns["MemberName"].ColumnName = "Sender Name";
                dt.Columns["ContactNumber"].ColumnName = "Sender ContactNo";
                dt.Columns["SenderBankName"].ColumnName = "Sender Bank Name";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["ServiceCharge"].ColumnName = "Service Charge";
                dt.Columns["VendorType"].ColumnName = "Gateway Name";
                dt.Columns["GatewayStatus"].ColumnName = "Gateway Status";
                dt.Columns["StatusName"].ColumnName = "My Pay Status";
                dt.Columns["CurrentBalance"].ColumnName = "Available Balance(Rs)";
                dt.Columns["PreviousBalance"].ColumnName = "Previous Balance(Rs)";
                dt.Columns["IpAddress"].ColumnName = "Ip Address";
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "TxnReport(" + DateTime.Now.ToString("MMM") + ")");
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

       


        //****************Dump Export With Filters*********//


        //ReferAndEarn 
        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelReferAndEarnDump(String ReceiverName, string MemberName, string TxnId, string ParentTxnId, string FromDate, string ToDate, string RefCode, string Status, string DayWise)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(FromDate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(ToDate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                {
                    DateTime d1 = Convert.ToDateTime(FromDate);
                    DateTime d2 = Convert.ToDateTime(ToDate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 5)
                    {
                        errorMessage = "You cannot export report more than 5 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    AddExportData outobject = new AddExportData();
                    GetExportData inobject = new GetExportData();
                    inobject.Type = (int)AddExportData.ExportType.Transaction;
                    inobject.StartDate = DateTime.UtcNow.AddHours(-4).ToString("MM-dd-yyyy hh:mm:ss tt");
                    inobject.EndDate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");
                    //inobject.CheckCreatedDate = DateTime.Now.ToString();
                    AddExportData res = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        errorMessage = "Your File was already generated! You can download the file from Download link. ";
                    }
                }
                if (errorMessage == "")
                {
                    fileName = "ReferAndEarn-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                    //Save the file to server temp folder
                    string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), fileName);

                    //GetUser user = new GetUser();
                    WalletTransactions w = new WalletTransactions();
                    if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                    {
                        w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.register_cashback;
                        w.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                        w.RecieverName = ReceiverName;
                        w.MemberName = MemberName;
                        w.TransactionUniqueId = TxnId;
                        w.StartDate = FromDate;
                        w.EndDate = ToDate;
                        w.Status = Convert.ToInt32(Status);
                        w.RefCode = RefCode;
                        w.ParentTransactionId = ParentTxnId;
                        if (DayWise != "" && DayWise != "0")
                        {
                            if (DayWise == "1")
                            {
                                w.Today = "Today";
                            }
                            else if (DayWise == "2")
                            {
                                w.Weekly = "Weekly";
                            }
                            else if (DayWise == "3")
                            {
                                w.Monthly = "Monthly";
                            }
                            else
                            {
                                w.Today = "";
                                w.Monthly = "";
                                w.Weekly = "";
                            }
                        }
                    }
                    DataTable dt = w.GetList();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt = dt.DefaultView.ToTable(false, "Sno", "TransactionUniqueId", "ParentTransactionId", "RefCode", "MemberName", "ContactNumber", "Amount", "RecieverName", "RecieverContactNumber", "ReceiverAmount", "IndiaDate", "IpAddress", "DeviceCode");
                        dt.Columns["Sno"].ColumnName = "Sr No";
                        dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                        dt.Columns["ParentTransactionId"].ColumnName = "Parent Txn Id";
                        dt.Columns["RefCode"].ColumnName = "Refer code";
                        dt.Columns["MemberName"].ColumnName = "Referred by Name";
                        dt.Columns["ContactNumber"].ColumnName = "Referred by ContactNo";
                        dt.Columns["Amount"].ColumnName = "Referrer earning cash";
                        dt.Columns["RecieverName"].ColumnName = "Referred to Name";
                        dt.Columns["RecieverContactNumber"].ColumnName = "Referred to ContactNo";
                        dt.Columns["ReceiverAmount"].ColumnName = "Referee earning cash";
                        dt.Columns["IndiaDate"].ColumnName = "Refer and earn initiated date";
                        //dt.Columns["IndiaDate"].ColumnName = "Cashback applied date";
                        dt.Columns["IpAddress"].ColumnName = "Ip Address";
                        dt.Columns["DeviceCode"].ColumnName = "Device Id";
                        dt.AcceptChanges();
                        AddExportData outobject_file = new AddExportData();
                        GetExportData inobject_file = new GetExportData();
                        inobject_file.Type = (int)AddExportData.ExportType.Transaction;
                        AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
                        if (res_file != null && res_file.Id != 0)
                        {
                            string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), res_file.FilePath);
                            if (System.IO.File.Exists(oldfilePath))
                            {
                                System.IO.File.Delete(oldfilePath);
                            }
                            res_file.FilePath = fullPath;
                            res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                            if (status)
                            {

                                Common.AddLogs("Refer and Earn Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        else
                        {
                            AddExportData export = new AddExportData();
                            export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            export.CreatedByName = Session["AdminUserName"].ToString();
                            export.FilePath = fullPath;
                            export.Type = (int)AddExportData.ExportType.Transaction;
                            export.IsActive = true;
                            export.IsApprovedByAdmin = true;
                            Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                            if (Id > 0)
                            {
                                Common.AddLogs("Refer and Earn Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "ReferAndEarn(" + DateTime.Now.ToString("MMM") + ")");
                            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Columns().AdjustToContents();  // Adjust column width
                            ws.Rows().AdjustToContents();
                            wb.SaveAs(fullPath);
                        }
                    }
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

        //Settlement
        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelAllTxnDump(string Service, string FromDate, string ToDate, Int64 MemberId, string ContactNo, string Name, string TxnId, string GatewayTxnId, string ParentTxnId, string TrackerId, string SubscriberId, string Status, string DayWise, string Sign)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(FromDate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(ToDate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                {
                    DateTime d1 = Convert.ToDateTime(FromDate);
                    DateTime d2 = Convert.ToDateTime(ToDate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 5)
                    {
                        errorMessage = "You cannot export report more than 5 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    AddExportData outobject = new AddExportData();
                    GetExportData inobject = new GetExportData();
                    inobject.Type = (int)AddExportData.ExportType.Transaction;
                    inobject.StartDate = DateTime.UtcNow.AddHours(-4).ToString("MM-dd-yyyy hh:mm:ss tt");
                    inobject.EndDate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");
                    //inobject.CheckCreatedDate = DateTime.Now.ToString();
                    AddExportData res = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        errorMessage = "Your File was already generated! You can download the file from Download link. ";
                    }
                }
                if (errorMessage == "")
                {
                    fileName = "SettlementReport-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                    //Save the file to server temp folder
                    string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), fileName);

                    //GetUser user = new GetUser();
                    WalletTransactions w = new WalletTransactions();
                    if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                    {
                        w.MemberId = MemberId;
                        w.ContactNumber = ContactNo;
                        w.MemberName = Name;
                        w.TransactionUniqueId = TxnId;
                        w.StartDate = FromDate;
                        w.EndDate = ToDate;
                        w.Status = Convert.ToInt32(Status);
                        w.VendorTransactionId = GatewayTxnId;
                        w.ParentTransactionId = ParentTxnId;
                        w.Reference = TrackerId;
                        w.Sign = Convert.ToInt32(Sign);
                        w.CustomerID = SubscriberId;

                        if (!string.IsNullOrEmpty(Service))
                        {
                            w.TypeMultiple = Service;
                        }
                        if (DayWise != "" && DayWise != "0")
                        {
                            if (DayWise == "1")
                            {
                                w.Today = "Today";
                            }
                            else if (DayWise == "2")
                            {
                                w.Weekly = "Weekly";
                            }
                            else if (DayWise == "3")
                            {
                                w.Monthly = "Monthly";
                            }
                            else
                            {
                                w.Today = "";
                                w.Monthly = "";
                                w.Weekly = "";
                            }
                        }
                    }
                    DataTable dt = w.GetAllTxnDataDump();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddExportData outobject_file = new AddExportData();
                        GetExportData inobject_file = new GetExportData();
                        inobject_file.Type = (int)AddExportData.ExportType.Transaction;
                        AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
                        if (res_file != null && res_file.Id != 0)
                        {
                            string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), res_file.FilePath);
                            if (System.IO.File.Exists(oldfilePath))
                            {
                                System.IO.File.Delete(oldfilePath);
                            }
                            res_file.FilePath = fullPath;
                            res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                            if (status)
                            {

                                Common.AddLogs("Settlement Report Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        else
                        {
                            AddExportData export = new AddExportData();
                            export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            export.CreatedByName = Session["AdminUserName"].ToString();
                            export.FilePath = fullPath;
                            export.Type = (int)AddExportData.ExportType.Transaction;
                            export.IsActive = true;
                            export.IsApprovedByAdmin = true;
                            Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                            if (Id > 0)
                            {
                                Common.AddLogs("Settlement Report Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "SettlementReport(" + DateTime.Now.ToString("MMM") + ")");
                            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Columns().AdjustToContents();  // Adjust column width
                            ws.Rows().AdjustToContents();
                            wb.SaveAs(fullPath);
                        }
                    }
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

        //AllServicesTxn
        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelAllServiceTxnDump(string Service, string FromDate, string ToDate, Int64 MemberId, string ContactNo, string Name, string TxnId, string GatewayTxnId, string TrackerId, string Status, string DayWise, string VendorType)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(FromDate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(ToDate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                {
                    DateTime d1 = Convert.ToDateTime(FromDate);
                    DateTime d2 = Convert.ToDateTime(ToDate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 5)
                    {
                        errorMessage = "You cannot export report more than 5 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    AddExportData outobject = new AddExportData();
                    GetExportData inobject = new GetExportData();
                    inobject.Type = (int)AddExportData.ExportType.Transaction;
                    inobject.StartDate = DateTime.UtcNow.AddHours(-4).ToString("MM-dd-yyyy hh:mm:ss tt");
                    inobject.EndDate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");
                    //inobject.CheckCreatedDate = DateTime.Now.ToString();
                    AddExportData res = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        errorMessage = "Your File was already generated! You can download the file from Download link. ";
                    }
                }
                if (errorMessage == "")
                {
                    fileName = "AllServicesTxn-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                    //Save the file to server temp folder
                    string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), fileName);

                    //GetUser user = new GetUser();
                    WalletTransactions w = new WalletTransactions();
                    if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                    {
                        w.MemberId = MemberId;
                        if (!string.IsNullOrEmpty(Service))
                        {
                            w.TypeMultiple = Service;
                        }
                        w.ContactNumber = ContactNo;
                        w.MemberName = Name;
                        w.TransactionUniqueId = TxnId;
                        w.StartDate = FromDate;
                        w.EndDate = ToDate;
                        w.Status = Convert.ToInt32(Status);
                        w.VendorTransactionId = GatewayTxnId;
                        w.Reference = TrackerId;
                        if (DayWise != "" && DayWise != "0")
                        {
                            if (DayWise == "1")
                            {
                                w.Today = "Today";
                            }
                            else if (DayWise == "2")
                            {
                                w.Weekly = "Weekly";
                            }
                            else if (DayWise == "3")
                            {
                                w.Monthly = "Monthly";
                            }
                            else
                            {
                                w.Today = "";
                                w.Monthly = "";
                                w.Weekly = "";
                            }
                        }
                        w.VendorType = Convert.ToInt32(VendorType);
                    }
                    DataTable dt = w.GetAllServiceTxnList();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddExportData outobject_file = new AddExportData();
                        GetExportData inobject_file = new GetExportData();
                        inobject_file.Type = (int)AddExportData.ExportType.Transaction;
                        AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
                        if (res_file != null && res_file.Id != 0)
                        {
                            string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), res_file.FilePath);
                            if (System.IO.File.Exists(oldfilePath))
                            {
                                System.IO.File.Delete(oldfilePath);
                            }
                            res_file.FilePath = fullPath;
                            res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                            if (status)
                            {

                                Common.AddLogs("All Services Txn Report Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        else
                        {
                            AddExportData export = new AddExportData();
                            export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            export.CreatedByName = Session["AdminUserName"].ToString();
                            export.FilePath = fullPath;
                            export.Type = (int)AddExportData.ExportType.Transaction;
                            export.IsActive = true;
                            export.IsApprovedByAdmin = true;
                            Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                            if (Id > 0)
                            {
                                Common.AddLogs("All Services Txn Report Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "AllServicesTxn(" + DateTime.Now.ToString("MMM") + ")");
                            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Columns().AdjustToContents();  // Adjust column width
                            ws.Rows().AdjustToContents();
                            wb.SaveAs(fullPath);
                        }
                    }
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

        //Cashback Dump
        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelCashbackDump(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string Status, string DayWise, string Sign, String Service)
        {
            var fileName = "";
            var errorMessage = "";
            if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
            {
                if (string.IsNullOrEmpty(FromDate))
                {
                    errorMessage = "Please select Start Date";
                }
                else if (string.IsNullOrEmpty(ToDate))
                {
                    errorMessage = "Please select End Date";
                }
                else if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                {
                    DateTime d1 = Convert.ToDateTime(FromDate);
                    DateTime d2 = Convert.ToDateTime(ToDate);

                    TimeSpan t = d2 - d1;
                    double TotalDays = t.TotalDays;
                    if (TotalDays > 5)
                    {
                        errorMessage = "You cannot export report more than 5 days. So please select StartDate and EndDate accordingly.";
                    }
                }
            }
            if (errorMessage == "")
            {
                if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                {
                    AddExportData outobject = new AddExportData();
                    GetExportData inobject = new GetExportData();
                    inobject.Type = (int)AddExportData.ExportType.Transaction;
                    inobject.StartDate = DateTime.UtcNow.AddHours(-4).ToString("MM-dd-yyyy hh:mm:ss tt");
                    inobject.EndDate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");
                    //inobject.CheckCreatedDate = DateTime.Now.ToString();
                    AddExportData res = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        errorMessage = "Your File was already generated! You can download the file from Download link. ";
                    }
                }
                if (errorMessage == "")
                {
                    fileName = "Cashback-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                    //Save the file to server temp folder
                    string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), fileName);
                    //GetUser user = new GetUser();
                    WalletTransactions w = new WalletTransactions();
                    if (Request.Url.AbsoluteUri.IndexOf(Common.dumpurl) > -1)
                    {
                        w.MemberId = Convert.ToInt64(MemberId);
                        w.ContactNumber = ContactNo;
                        w.MemberName = Name;
                        w.TransactionUniqueId = TxnId;
                        w.StartDate = FromDate;
                        w.EndDate = ToDate;
                        w.Status = Convert.ToInt32(Status);
                        if (!String.IsNullOrEmpty(Service) && Service != "0")
                        {
                            w.Type = Convert.ToInt32(Service);
                        }
                        w.Sign = Convert.ToInt32(Sign);
                        if (DayWise != "" && DayWise != "0")
                        {
                            if (DayWise == "1")
                            {
                                w.Today = "Today";
                            }
                            else if (DayWise == "2")
                            {
                                w.Weekly = "Weekly";
                            }
                            else if (DayWise == "3")
                            {
                                w.Monthly = "Monthly";
                            }
                            else
                            {
                                w.Today = "";
                                w.Monthly = "";
                                w.Weekly = "";
                            }
                        }
                    }
                    DataTable dt = w.GetList();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "MemberName", "ContactNumber", "Amount", "SignName", "Type", "StatusName", "CurrentBalance", "PreviousBalance", "IpAddress");
                        dt.Columns["Sno"].ColumnName = "Sr No";
                        dt.Columns["IndiaDate"].ColumnName = "Date";
                        dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                        dt.Columns["MemberId"].ColumnName = "Member Id";
                        dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                        dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                        dt.Columns["MemberName"].ColumnName = "Name";
                        dt.Columns["ContactNumber"].ColumnName = "Contact Number";
                        dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                        dt.Columns["SignName"].ColumnName = "Sign";
                        dt.Columns["Type"].ColumnName = "Service";
                        dt.Columns["StatusName"].ColumnName = "My Pay Status";
                        dt.Columns["CurrentBalance"].ColumnName = "Available Balance(Rs)";
                        dt.Columns["PreviousBalance"].ColumnName = "Previous Balance(Rs)";
                        dt.Columns["IpAddress"].ColumnName = "Ip Address";
                        dt.AcceptChanges();
                        AddExportData outobject_file = new AddExportData();
                        GetExportData inobject_file = new GetExportData();
                        inobject_file.Type = (int)AddExportData.ExportType.Transaction;
                        AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
                        if (res_file != null && res_file.Id != 0)
                        {
                            string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), res_file.FilePath);
                            if (System.IO.File.Exists(oldfilePath))
                            {
                                System.IO.File.Delete(oldfilePath);
                            }
                            res_file.FilePath = fullPath;
                            res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                            if (status)
                            {

                                Common.AddLogs("Cashback Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        else
                        {
                            AddExportData export = new AddExportData();
                            export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            export.CreatedByName = Session["AdminUserName"].ToString();
                            export.FilePath = fullPath;
                            export.Type = (int)AddExportData.ExportType.Transaction;
                            export.IsActive = true;
                            export.IsApprovedByAdmin = true;
                            Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                            if (Id > 0)
                            {
                                Common.AddLogs("Cashback Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Transaction_Export);
                            }
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "Cashback(" + DateTime.Now.ToString("MMM") + ")");
                            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Columns().AdjustToContents();  // Adjust column width
                            ws.Rows().AdjustToContents();
                            wb.SaveAs(fullPath);
                        }
                    }
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }


        [HttpPost]
        [Authorize]
        public ActionResult DownloadFileName()
        {
            var errorMessage = "";
            AddExportData outobject_file = new AddExportData();
            GetExportData inobject_file = new GetExportData();
            inobject_file.Type = (int)AddExportData.ExportType.Transaction;
            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);

            if (res_file != null && res_file.Id != 0)
            {
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), res_file.FilePath);
                if (System.IO.File.Exists(fullPath))
                {
                    return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
                }
                else
                {
                    errorMessage = "No file found";
                    return Json(new { fileName = "", errorMessage = errorMessage });
                }

            }
            else
            {
                errorMessage = "No file found";
                return Json(new { fileName = "", errorMessage = errorMessage });
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult DownloadFile(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/UserTransaction"), fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                    //System.IO.File.Delete(fullPath);
                    return File(fileByteArray, "application/vnd.ms-excel", fileName);
                }
                else
                {
                    return RedirectToAction("/Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("/Index");
            }
        }



    }
}