using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MyPay.Controllers
{
    public class AdminTransactionsController : BaseAdminSessionController
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
                         Remarks = row["Remarks"].ToString().Replace("<", ""),
                         Description = row["Description"].ToString().Replace("<", ""),
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
                         TotalCouponDiscount = Convert.ToDecimal(row["TotalCouponDiscount"].ToString()),
                         AdditionalInfo1 = row["AdditionalInfo1"].ToString()
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

        // GET:P2PTransactions
        [Authorize]
        public ActionResult P2PTransfer(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
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
            return View();
        }

        [Authorize]
        public JsonResult GetP2PTransactionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Receiver Name");
            columns.Add("Receiver Wallet Number");
            columns.Add("Sender Name");
            columns.Add("Sender Wallet Number");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("MyPayStatus");
            //columns.Add("UpdateBy");
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
            string ReceiverContactNumber = context.Request.Form["ReceiverContactNumber"];
            string ReceiverName = context.Request.Form["ReceiverName"];
            string DayWise = context.Request.Form["Today"];
            string Sign = context.Request.Form["Sign"];
            string Reference = context.Request.Form["Reference"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone;
            w.TransferType = (int)WalletTransactions.TransferTypes.Sender;
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.RecieverContactNumber = ReceiverContactNumber;
            w.RecieverName = ReceiverName;
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
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
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         UpdateByName = row["UpdateByName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         RecieverName = row["RecieverName"].ToString(),
                         RecieverContactNumber = row["RecieverContactNumber"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         IpAddress = row["IpAddress"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         Reference = row["Reference"].ToString()
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

        // GET: LoadCardRequestTransactions
        [Authorize]
        public ActionResult LoadCardRequest(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
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
            return View();
        }

        [Authorize]
        public JsonResult GetLoadCardRequestTransactionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("ContactNumber");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("CardNumber");
            columns.Add("CardType");
            columns.Add("ExpiryDate");
            columns.Add("SourceType");
            columns.Add("ServiceCharge");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
            //columns.Add("UpdateBy");
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
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string DayWise = context.Request.Form["Today"];
            string Sign = context.Request.Form["Sign"];
            string Reference = context.Request.Form["Reference"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit;
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTransactionId;
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
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
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         UpdateByName = row["UpdateByName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         CardNumber = row["CardNumber"].ToString(),
                         CardType = row["CardType"].ToString(),
                         ExpiryDate = row["ExpiryDate"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         IpAddress = row["IpAddress"].ToString(),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         Reference = row["Reference"].ToString()
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

        // GET: LoadIPSRequestTransactions
        [Authorize]
        public ActionResult LoadIPSRequest(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
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
            return View();
        }

        [Authorize]
        public JsonResult GetLoadIPSRequestTransactionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("ContactNumber");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("SourceType");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
            //columns.Add("UpdateBy");
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
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string DayWise = context.Request.Form["Today"];
            string Sign = context.Request.Form["Sign"];
            string Reference = context.Request.Form["Reference"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_connectips;
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTransactionId;
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
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
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         UpdateByName = row["UpdateByName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         IpAddress = row["IpAddress"].ToString(),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         Reference = row["Reference"].ToString()
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

        // GET: Cashback
        [Authorize]
        public ActionResult Cashback(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
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

            ViewBag.Status = items;

            List<SelectListItem> Services = new List<SelectListItem>();
            Services.Add(new SelectListItem
            {
                Text = "Register",
                Value = "17",
                Selected = true
            });
            Services.Add(new SelectListItem
            {
                Text = "KYC",
                Value = "18"
            });
            Services.Add(new SelectListItem
            {
                Text = "All Service",
                Value = "20"
            });
            ViewBag.ServiceList = Services;

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


            List<SelectListItem> VendorTypes = new List<SelectListItem>();
            VendorTypes.Add(new SelectListItem
            {
                Text = "Khalti ",
                Value = "1",
                Selected = true
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "Prabhu Pay",
                Value = "6"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "FonePay",
                Value = "7"
            });
            ViewBag.VendorTypes = VendorTypes;

            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }

        [Authorize]
        public JsonResult GetCashbackTransactionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("Member Id");
            columns.Add("TransactionId");
            columns.Add("Name");
            columns.Add("ContactNumber");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("Service");
            columns.Add("MyPayStatus");
            columns.Add("AvailableBalance");
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
            string Type = context.Request.Form["Type"];
            string DayWise = context.Request.Form["Today"];
            string Sign = context.Request.Form["Sign"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            if (!String.IsNullOrEmpty(Type) && Type != "0")
            {
                w.Type = Convert.ToInt32(Type);
            }
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
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
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         ContactNumber = row["ContactNumber"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         SignName = row["SignName"].ToString(),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         IpAddress = row["IpAddress"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         VendorType = Convert.ToInt32(row["VendorType"].ToString()),
                         Type = Convert.ToInt32(row["Type"].ToString()),
                         ParentTransactionId = row["ParentTransactionId"].ToString()
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

        // GET: AllServiceTransactions
        [Authorize]
        public ActionResult AllServiceTransactions(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            List<SelectListItem> statusitems = new List<SelectListItem>();
            statusitems.Add(new SelectListItem
            {
                Text = "Select Status",
                Value = "0",
                Selected = true
            });
            statusitems.Add(new SelectListItem
            {
                Text = "Success",
                Value = "1"
            });
            statusitems.Add(new SelectListItem
            {
                Text = "Pending",
                Value = "2"
            });
            statusitems.Add(new SelectListItem
            {
                Text = "Failed",
                Value = "3"
            });
            ViewBag.Status = statusitems;
            List<SelectListItem> VendorTypes = new List<SelectListItem>();
            VendorTypes.Add(new SelectListItem
            {
                Text = "Khalti",
                Value = "1",
                Selected = true
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "Prabhu",
                Value = "6"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "FonePay",
                Value = "7"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "BusSewa",
                Value = "8"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "CableCar",
                Value = "9"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "NCHL",
                Value = "2"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "PLASMATECH",
                Value = "12"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "NCHLQR",
                Value = "10"
            });
            VendorTypes.Add(new SelectListItem
            {
                Text = "Tourist Bus",
                Value = "11"
            });
            ViewBag.VendorTypes = VendorTypes;
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility();
            objRes = objRes.Where(x => x.IsUtility == true).ToList();
            List<SelectListItem> objProviderServiceCategoryList_SelectList = new List<SelectListItem>();
            // **********  ADD DEFAULT VALUE IN DROPDOWN *********** //
            {
                SelectListItem objDefault = new SelectListItem();
                objDefault.Value = "0";
                objDefault.Text = "-- Select Provider --";
                objDefault.Selected = false;
                objProviderServiceCategoryList_SelectList.Add(objDefault);
            }

            for (int i = 0; i < objRes.Count; i++)
            {

                SelectListItem objItem = new SelectListItem();
                objItem.Value = objRes[i].ProviderTypeId.ToString();
                objItem.Text = objRes[i].ProviderName.ToString();
                objProviderServiceCategoryList_SelectList.Add(objItem);


            }
            ViewBag.ServiceList = objProviderServiceCategoryList_SelectList;

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
            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }

        [Authorize]
        public JsonResult GetAllServiceTransactionLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("Name");
            columns.Add("ContactNumber");
            columns.Add("SubscriberNumber");
            columns.Add("Amount");
            columns.Add("Service");
            columns.Add("MyPayStatus");
            columns.Add("AvailableBalance");
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
            string Type = context.Request.Form["Type"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string Name = context.Request.Form["Name"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string DayWise = context.Request.Form["Today"];
            string Reference = context.Request.Form["Reference"];
            string TypeMultiple = context.Request.Form["TypeMultiple"];
            string VendorTypes = context.Request.Form["VendorTypes"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            if (!string.IsNullOrEmpty(TypeMultiple))
            {
                Type = "0";
                w.TypeMultiple = TypeMultiple;
            }
            w.Type = Convert.ToInt32(Type);
            w.ContactNumber = ContactNumber;
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTransactionId;
            w.Reference = Reference;
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
            w.VendorType = Convert.ToInt32(VendorTypes);// (int)VendorApi_CommonHelper.VendoTypes.khalti;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new WalletTransactions
                     {
                         Sno = Convert.ToInt64(row["Sno"]),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         ContactNumber = row["ContactNumber"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         CustomerID = row["CustomerID"].ToString(),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         IpAddress = row["IpAddress"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         Reference = row["Reference"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         CashBack = Convert.ToDecimal(row["CashBack"].ToString()),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         MPCoinsDebit = row["WalletType"].ToString() != "4" ? 0 : Convert.ToDecimal(row["MPCoinsDebit"].ToString()),
                         RewardPoint = row["Type"].ToString() == "20" ? 0 : Convert.ToDecimal(row["RewardPoint"].ToString()),
                         RewardPointBalance = row["Type"].ToString() == "20" ? 0 : (Convert.ToDecimal(row["RewardPointBalance"].ToString()) + Convert.ToDecimal(row["RewardPoint"].ToString())),
                         PreviousRewardPointBalance = Convert.ToDecimal(row["PreviousRewardPointBalance"].ToString()),
                         TotalCoinsCredit = Convert.ToDecimal(row["TotalCoinsCredit"].ToString()),
                         TotalCoinsDebit = Convert.ToDecimal(row["TotalCoinsDebit"].ToString()),
                         Platform = row["Platform"].ToString(),
                         CouponDiscount = Convert.ToDecimal(row["CouponDiscount"].ToString()),
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
        // GET: BankTransfer
        [Authorize]
        public ActionResult BankTransferReport(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
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
            return View();
        }

        [Authorize]
        public JsonResult GetBankTransferLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("InstructionId");
            columns.Add("BatchId");
            columns.Add("ReceiverAccountNo");
            columns.Add("ReceiverName");
            columns.Add("ReceiverBankCode");
            columns.Add("SenderName");
            columns.Add("SenderContactNo");
            columns.Add("Amount");
            columns.Add("ServiceCharge");
            columns.Add("GatewayStatus");
            columns.Add("MyPayStatus");
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
            string Name = context.Request.Form["Name"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string ContactNumber = context.Request.Form["ContactNumber"];
            string DayWise = context.Request.Form["Today"];
            string Sign = context.Request.Form["Sign"];
            string Reference = context.Request.Form["Reference"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.ContactNumber = ContactNumber;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTransactionId;
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
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
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         Reference = row["Reference"].ToString(),
                         RecieverAccountNo = row["RecieverAccountNo"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         SenderAccountNo = row["SenderAccountNo"].ToString(),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         StatusName = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(row["Status"])),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         RecieverBankCode = row["RecieverBankCode"].ToString(),
                         RecieverBranch = row["RecieverBranch"].ToString(),
                         RecieverName = row["RecieverName"].ToString(),
                         TxnInstructionId = row["TxnInstructionId"].ToString(),
                         BatchTransactionId = row["BatchTransactionId"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         SenderBankCode = row["SenderBankCode"].ToString(),
                         SenderBranch = row["SenderBranch"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         SignName = row["SignName"].ToString(),
                         RecieverBankName = row["RecieverBankName"].ToString(),
                         SenderBankName = row["SenderBankName"].ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         IpAddress = row["IpAddress"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString()
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

        // GET: InternetBankingReport
        [Authorize]
        public ActionResult InternetBankingReport(string MemberId, string TransactionId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
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

            return View();
        }

        [Authorize]
        public JsonResult GetInternetBankingLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("ContactNo");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("SenderBankName");
            columns.Add("Service");
            columns.Add("ServiceCharge");
            columns.Add("Cashback");
            columns.Add("Gateway Name");
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
            string Reference = context.Request.Form["Reference"];
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
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Internet_Banking;
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
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
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
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         SenderBankName = row["SenderBankName"].ToString(),
                         IpAddress = row["IpAddress"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         Reference = row["Reference"].ToString()
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

        // GET: ReferAndEarn
        [Authorize]
        public ActionResult ReferAndEarn(string MemberId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
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
            ViewBag.DumpURL = Common.dumpurl;
            return View();
        }

        [Authorize]
        public JsonResult GetReferAndEarnLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Refercode");
            columns.Add("Referred by Name");
            columns.Add("Referred by ContactNo");
            columns.Add("Referrer earning cash");
            //columns.Add("Referrer earning point");
            //columns.Add("Refer code");
            columns.Add("Referred to Name");
            columns.Add("Referred to ContactNo");
            columns.Add("Referee earning cash");
            //columns.Add("Referee earning point");
            columns.Add("Date");
            columns.Add("Cashback applied date");
            columns.Add("Ip Address");
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
            string RefCode = context.Request.Form["RefCode"];
            string RecieverName = context.Request.Form["RecieverName"];
            string MemberName = context.Request.Form["MemberName"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Status = context.Request.Form["Status"];
            string DayWise = context.Request.Form["Today"];
            string ParentTransactionId = context.Request.Form["ParentTransactionId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            GetTransaction inobject_Trans = new GetTransaction();

            List<WalletTransactions> trans = new List<WalletTransactions>();

            WalletTransactions w = new WalletTransactions();
            //if (!string.IsNullOrEmpty(MemberId))
            //{
            //    w.MemberId = Convert.ToInt64(MemberId);
            //}
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.register_cashback;
            w.TransferType = (int)WalletTransactions.TransferTypes.Sender;
            w.RecieverName = RecieverName;
            w.MemberName = MemberName;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.Status = Convert.ToInt32(Status);
            w.RefCode = RefCode;
            w.ParentTransactionId = ParentTransactionId;
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
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         ContactNumber = row["ContactNumber"].ToString(),
                         RecieverName = row["RecieverName"].ToString(),
                         RecieverContactNumber = row["RecieverContactNumber"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         IpAddress = row["IpAddress"].ToString(),
                         RefCode = row["RefCode"].ToString(),
                         ReceiverAmount = Convert.ToDecimal(row["ReceiverAmount"]),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         ParentTransactionId = row["ParentTransactionId"].ToString(),
                         DeviceCode = row["DeviceCode"].ToString()
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

        // GET: MobileBankingReport
        [Authorize]
        public ActionResult MobileBankingReport(string MemberId, string TransactionId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
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

            return View();
        }

        [Authorize]
        public JsonResult GetMobileBankingLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("ContactNo");
            columns.Add("Amount");
            columns.Add("Sign");
            columns.Add("SenderBankName");
            columns.Add("Service");
            columns.Add("ServiceCharge");
            columns.Add("Cashback");
            columns.Add("GatewayName");
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
            string Reference = context.Request.Form["Reference"];
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
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Mobile_Banking;
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
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                         PreviousBalance = Convert.ToDecimal(row["PreviousBalance"]),
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
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         SenderBankName = row["SenderBankName"].ToString(),
                         IpAddress = row["IpAddress"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         Reference = row["Reference"].ToString()
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelCashback(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string Status, string DayWise, string Sign, String Service)
        {
            var fileName = "Cashback-" + DateTime.Now.ToShortDateString() + ".xlsx";

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
        public ActionResult ExportExcelInternetBanking(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string GatewayTxnId, string Status, string DayWise, string Sign, string Reference)
        {
            var fileName = "InternetBanking-" + DateTime.Now.ToShortDateString() + ".xlsx";

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
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Internet_Banking;
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
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "Reference", "MemberName", "ContactNumber", "Amount", "SignName", "SenderBankName", "Type", "ServiceCharge", "CashBack", "VendorType", "GatewayStatus", "StatusName", "UpdateByName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["Reference"].ColumnName = "Tracker Id";
                dt.Columns["MemberName"].ColumnName = "Name";
                dt.Columns["ContactNumber"].ColumnName = "Contact Number";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["SenderBankName"].ColumnName = "Bank Name";
                dt.Columns["Type"].ColumnName = "Service";
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelLoadCard(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string GatewayTxnId, string Status, string DayWise, string Sign, string Reference)
        {
            var fileName = "LoadCard-" + DateTime.Now.ToShortDateString() + ".xlsx";

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
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit;
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
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "Reference", "MemberName", "ContactNumber", "Amount", "SignName", "CardNumber", "CardType", "ExpiryDate", "Type", "VendorType", "GatewayStatus", "StatusName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["Reference"].ColumnName = "Tracker Id";
                dt.Columns["MemberName"].ColumnName = "Name";
                dt.Columns["ContactNumber"].ColumnName = "Contact Number";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["CardNumber"].ColumnName = "Card Number";
                dt.Columns["CardType"].ColumnName = "Card Type";
                dt.Columns["Type"].ColumnName = "Service";
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelLoadIPS(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string GatewayTxnId, string Status, string DayWise, string Sign, string Reference)
        {
            var fileName = "LoadIPS-" + DateTime.Now.ToShortDateString() + ".xlsx";

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
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_connectips;
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
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "Reference", "MemberName", "ContactNumber", "Amount", "SignName", "Type", "VendorType", "GatewayStatus", "StatusName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["Reference"].ColumnName = "Tracker Id";
                dt.Columns["MemberName"].ColumnName = "Name";
                dt.Columns["ContactNumber"].ColumnName = "Contact Number";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["Type"].ColumnName = "Source Type";
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelMobileBanking(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string GatewayTxnId, string Status, string DayWise, string Sign, string Reference)
        {
            var fileName = "MobileBanking-" + DateTime.Now.ToShortDateString() + ".xlsx";

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
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Mobile_Banking;
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
            var errorMessage = "";
            DataTable dt = w.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "Reference", "MemberName", "ContactNumber", "Amount", "SignName", "SenderBankName", "Type", "ServiceCharge", "CashBack", "VendorType", "GatewayStatus", "StatusName", "UpdateByName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["Reference"].ColumnName = "Tracker Id";
                dt.Columns["MemberName"].ColumnName = "Name";
                dt.Columns["ContactNumber"].ColumnName = "Contact Number";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["SenderBankName"].ColumnName = "Bank Name";
                dt.Columns["Type"].ColumnName = "Service";
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
            else
            {
                fileName = "";
                errorMessage = "No data found";
            }

            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage });
        }
        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelP2PTransactions(Int64 MemberId, string ContactNo, string Name, string TxnId, string FromDate, string ToDate, string Status, string DayWise, string Sign, string ReceiverContactNumber, string ReceiverName, string Reference)
        {
            var fileName = "P2PTransactions-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            WalletTransactions w = new WalletTransactions();
            w.Take = 50;
            w.Skip = 0;
            w.MemberId = Convert.ToInt64(MemberId);
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone;
            w.TransferType = (int)WalletTransactions.TransferTypes.Sender;
            w.ContactNumber = ContactNo;
            w.MemberName = Name;
            w.TransactionUniqueId = TxnId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.Status = Convert.ToInt32(Status);
            w.RecieverContactNumber = ReceiverContactNumber;
            w.RecieverName = ReceiverName;
            w.Sign = Convert.ToInt32(Sign);
            w.Reference = Reference;
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
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "UpdatedDateIndiaDate", "MemberId", "TransactionUniqueId", "VendorTransactionId", "Reference", "RecieverName", "RecieverContactNumber", "MemberName", "ContactNumber", "Amount", "SignName", "StatusName", "CurrentBalance", "PreviousBalance", "IpAddress");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["UpdatedDateIndiaDate"].ColumnName = "Updated Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["Reference"].ColumnName = "Tracker Id";
                dt.Columns["RecieverName"].ColumnName = "Receiver Name";
                dt.Columns["RecieverContactNumber"].ColumnName = "Receiver Wallet Number";
                dt.Columns["MemberName"].ColumnName = "Sender Name";
                dt.Columns["ContactNumber"].ColumnName = "Sender Wallet Number";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
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

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelReferandEarn(String ReceiverName, string MemberName, string TxnId, string ParentTxnId, string FromDate, string ToDate, string RefCode, string Status, string DayWise)
        {
            var fileName = "ReferAndEarn-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            WalletTransactions w = new WalletTransactions();
            w.Take = 50;
            w.Skip = 0;
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

        // GET: AdminLoadedWalletReport
        [Authorize]
        public ActionResult AdminLoadedWalletReport(string MemberId, string TransactionId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
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

            return View();
        }

        [Authorize]
        public JsonResult GetAdminLoadedWalletLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("ContactNo");
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
            w.Sign = Convert.ToInt32(Sign);
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.WalletUpdate_By_Admin;
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
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
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
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         IpAddress = row["IpAddress"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString()
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


        // GET: AdminHoldWalletReport
        [Authorize]
        public ActionResult AdminHoldWalletReport(string MemberId, string TransactionId)
        {
            ViewBag.MemberId = string.IsNullOrEmpty(MemberId) ? "" : MemberId;
            ViewBag.TransactionId = string.IsNullOrEmpty(TransactionId) ? "" : TransactionId;
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

            return View();
        }

        [Authorize]
        public JsonResult GetAdminHoldWalletReportLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("Date");
            columns.Add("MemberId");
            columns.Add("TransactionId");
            columns.Add("GatewayTransactionId");
            columns.Add("Name");
            columns.Add("ContactNo");
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
            w.Sign = Convert.ToInt32(Sign);
            w.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Hold_By_Admin;
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
                         VendorType = Convert.ToInt32(row["VendorType"].ToString()),
                         VendorTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt64(row["VendorType"])).ToUpper(),
                         IpAddress = row["IpAddress"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                         ResponseCode = row["ResponseCode"].ToString()
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
        public JsonResult AmountReleaseByAdmin(string MemberId, string TxnId, string Type)
        {
            string msg = "";
            if (MemberId == "")
            {
                MemberId = "0";
            }
            WalletTransactions walletcheck = new WalletTransactions();
            walletcheck.MemberId = Convert.ToInt64(MemberId);
            walletcheck.ParentTransactionId = TxnId;
            walletcheck.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Release_From_Admin;
            if (walletcheck.GetRecord())
            {
                msg = "Already release that amount.";
            }
            else
            {
                WalletTransactions wallet = new WalletTransactions();
                if (MemberId != "")
                {
                    wallet.MemberId = Convert.ToInt64(MemberId);
                }
                wallet.TransactionUniqueId = TxnId;
                if (Type != "")
                {
                    wallet.Type = Convert.ToInt32(Type);
                }
                if (wallet.GetRecord())
                {

                    AddUser outobject = new AddUser();
                    GetUser inobject = new GetUser();
                    if (MemberId != "")
                    {
                        inobject.MemberId = Convert.ToInt64(MemberId);
                    }
                    AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
                    if (model != null && model.Id != 0)
                    {
                        string UserMessage = string.Empty;
                        string Referenceno = new CommonHelpers().GenerateUniqueId();
                        string AdminRemarks = "Amount has been released from admin for Rs." + wallet.Amount;
                        msg = RepTransactions.WalletUpdateFromAdmin(Convert.ToInt64(MemberId), wallet.Amount.ToString(), Referenceno, "4", ref UserMessage, AdminRemarks, TxnId);
                        if (msg.ToString().ToLower() == "success")
                        {
                            wallet.ResponseCode = "1";
                            wallet.Update();
                            ViewBag.SuccessMessage = UserMessage;
                        }
                        else
                        {
                            ViewBag.Message = msg;
                        }
                    }
                    else
                    {
                        ViewBag.Message = "User Not Found";
                    }
                }
                else
                {
                    ViewBag.Message = "record Not found";
                }
            }
            return Json(msg);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ChangeTxnStatus(string Status, string TxnId, string hdnChkRefund, string txtRemarks, string txtGatewayID)
        {
            string msg = "";
            if (string.IsNullOrEmpty(Status) || Status == "0")
            {
                msg = "Please Select status";
            }
            else if (string.IsNullOrEmpty(TxnId) || TxnId == "0")
            {
                msg = "Please Select TxnId";
            }
            else if (string.IsNullOrEmpty(txtRemarks))
            {
                msg = "Please enter Remarks";
            }
            else
            {
                string platform = "web";
                string devicecode = HttpContext.Request.Browser.Type;
                WalletTransactions walletcheck = new WalletTransactions();
                walletcheck.TransactionUniqueId = TxnId;
                if (walletcheck.GetRecord())
                {
                    if (walletcheck.Status != (int)WalletTransactions.Statuses.Pending)
                    {
                        msg = "Transaction is not Pending.";
                    }
                    else if (walletcheck.Type == (int)WalletTransactions.Types.Refund)
                    {
                        msg = "This is Refund Transaction.";
                    }
                    else
                    {
                        if (Status == ((int)WalletTransactions.Statuses.Success).ToString())
                        {
                            if (string.IsNullOrEmpty(txtGatewayID))
                            {
                                msg = "Gateway Transaction ID cannot be empty.";
                            }
                            else
                            {
                                WalletTransactions objWalletTrans = new WalletTransactions();
                                objWalletTrans.VendorTransactionId = txtGatewayID;
                                objWalletTrans.Type = Convert.ToInt32(walletcheck.Type);
                                objWalletTrans.Sign = (int)WalletTransactions.Signs.Debit;
                                objWalletTrans.Status = (int)WalletTransactions.Statuses.Success;
                                if (objWalletTrans.GetRecord())
                                {
                                    msg = "Duplicate Gateway transaction ID";
                                }
                                else
                                {
                                    walletcheck.VendorTransactionId = txtGatewayID;
                                    walletcheck.Status = (int)WalletTransactions.Statuses.Success;
                                    walletcheck.UpdateBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    walletcheck.UpdateByName = Session["AdminUserName"].ToString();
                                    walletcheck.UpdatedDate = DateTime.UtcNow;
                                    walletcheck.Remarks = txtRemarks;
                                    if (walletcheck.VendorType == 12 && walletcheck.Type == 104)
                                    {
                                        if (walletcheck.Updates())
                                        {
                                            Models.Common.Common.AddLogs($"Transaction Id:{walletcheck.TransactionUniqueId} Status is Changed successfully from Pending to Success with Gateway Txn ID as {txtGatewayID}. Action performed by : {Session["AdminUserName"].ToString()} with Remarks {txtRemarks}", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, platform, devicecode, (int)AddLog.LogActivityEnum.ChangeTxnStatus);

                                            if (walletcheck.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay)
                                            {
                                                string BookingID = walletcheck.CustomerID;
                                                Common.AddLogs($"Flight Transaction Change Transaction Status executed on {Common.fnGetdatetime()} for BookingID: {BookingID}", true, Convert.ToInt32(AddLog.LogType.DBLogs), Convert.ToInt64(walletcheck.MemberId), "", true, walletcheck.Platform, walletcheck.DeviceCode);
                                                AddFlightBookingDetails outobjectFlightBooking = new AddFlightBookingDetails();
                                                GetFlightBookingDetails inobjectFlightBooking = new GetFlightBookingDetails();
                                                inobjectFlightBooking.BookingId = Convert.ToInt64(BookingID);
                                                inobjectFlightBooking.CheckFlightBooked = 1;
                                                inobjectFlightBooking.CheckInbound = 0;
                                                inobjectFlightBooking.MemberId = walletcheck.MemberId;
                                                AddFlightBookingDetails res = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, inobjectFlightBooking, outobjectFlightBooking);
                                                if (res != null && res.Id > 0)
                                                {
                                                    res.IsFlightIssued = true;
                                                    bool IsUpdated = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(res, "flightbookingdetails");
                                                    Common.AddLogs($"Flight Transaction Change Transaction Status Success on {Common.fnGetdatetime()} for BookingID: {BookingID}", true, Convert.ToInt32(AddLog.LogType.DBLogs), Convert.ToInt64(walletcheck.MemberId), "", true, walletcheck.Platform, walletcheck.DeviceCode);
                                                }
                                            }
                                            msg = "success";
                                        }
                                        else
                                        {
                                            msg = "Transaction Not updated";
                                        }
                                    }
                                    else
                                    {
                                        if (walletcheck.Update())
                                        {
                                            Models.Common.Common.AddLogs($"Transaction Id:{walletcheck.TransactionUniqueId} Status is Changed successfully from Pending to Success with Gateway Txn ID as {txtGatewayID}. Action performed by : {Session["AdminUserName"].ToString()} with Remarks {txtRemarks}", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, platform, devicecode, (int)AddLog.LogActivityEnum.ChangeTxnStatus);

                                            if (walletcheck.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines)
                                            {
                                                string BookingID = walletcheck.CustomerID;
                                                Common.AddLogs($"Flight Transaction Change Transaction Status executed on {Common.fnGetdatetime()} for BookingID: {BookingID}", true, Convert.ToInt32(AddLog.LogType.DBLogs), Convert.ToInt64(walletcheck.MemberId), "", true, walletcheck.Platform, walletcheck.DeviceCode);
                                                AddFlightBookingDetails outobjectFlightBooking = new AddFlightBookingDetails();
                                                GetFlightBookingDetails inobjectFlightBooking = new GetFlightBookingDetails();
                                                inobjectFlightBooking.BookingId = Convert.ToInt64(BookingID);
                                                inobjectFlightBooking.CheckFlightBooked = 1;
                                                inobjectFlightBooking.CheckInbound = 0;
                                                inobjectFlightBooking.MemberId = walletcheck.MemberId;
                                                AddFlightBookingDetails res = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, inobjectFlightBooking, outobjectFlightBooking);
                                                if (res != null && res.Id > 0)
                                                {
                                                    res.IsFlightIssued = true;
                                                    bool IsUpdated = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(res, "flightbookingdetails");
                                                    Common.AddLogs($"Flight Transaction Change Transaction Status Success on {Common.fnGetdatetime()} for BookingID: {BookingID}", true, Convert.ToInt32(AddLog.LogType.DBLogs), Convert.ToInt64(walletcheck.MemberId), "", true, walletcheck.Platform, walletcheck.DeviceCode);
                                                }
                                            }
                                            msg = "success";
                                        }
                                        else
                                        {
                                            msg = "Transaction Not updated";
                                        }
                                    }
                                }
                            }
                        }
                        else if (Status == ((int)WalletTransactions.Statuses.Failed).ToString())
                        {

                            walletcheck.Status = (int)WalletTransactions.Statuses.Failed;
                            walletcheck.GatewayStatus = WalletTransactions.Statuses.Failed.ToString();
                            walletcheck.UpdateBy = Convert.ToInt64(Session["AdminMemberId"]);
                            walletcheck.UpdateByName = Session["AdminUserName"].ToString();
                            walletcheck.UpdatedDate = DateTime.UtcNow;
                            walletcheck.Remarks = txtRemarks;
                            string ParentTransactionId = walletcheck.TransactionUniqueId;
                            int VendorType = walletcheck.VendorType;

                            if (walletcheck.Update())
                            {
                                if (hdnChkRefund == "1")
                                {
                                    decimal CurrentBalance = 0;
                                    AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                                    GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                                    inobject.MemberId = Convert.ToInt64(walletcheck.MemberId);
                                    AddUserLoginWithPin resGetRecord = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                                    if (resGetRecord != null || resGetRecord.Id != 0)
                                    {
                                        CurrentBalance = resGetRecord.TotalAmount;
                                    }

                                    WalletTransactions objWalletTrans = new WalletTransactions();
                                    objWalletTrans.ParentTransactionId = walletcheck.TransactionUniqueId;
                                    objWalletTrans.Type = Convert.ToInt32(walletcheck.Type);
                                    objWalletTrans.MemberId = Convert.ToInt64(walletcheck.MemberId);
                                    objWalletTrans.Sign = (int)WalletTransactions.Signs.Credit;
                                    objWalletTrans.Status = (int)WalletTransactions.Statuses.Refund;
                                    if (objWalletTrans.GetRecord())
                                    {
                                        msg = "Refund already initiated";
                                    }
                                    else
                                    {
                                        WalletTransactions objWalletTransCheckBankRefund = new WalletTransactions();
                                        objWalletTransCheckBankRefund.ParentTransactionId = walletcheck.ParentTransactionId;
                                        objWalletTransCheckBankRefund.MemberId = Convert.ToInt64(walletcheck.MemberId);
                                        objWalletTransCheckBankRefund.Sign = (int)WalletTransactions.Signs.Credit;
                                        objWalletTransCheckBankRefund.Status = (int)WalletTransactions.Statuses.Refund;
                                        if (!string.IsNullOrEmpty(walletcheck.ParentTransactionId) && objWalletTransCheckBankRefund.GetRecord())
                                        {
                                            msg = "Refund already initiated";
                                        }
                                        else
                                        {
                                            walletcheck.Id = 0;
                                            walletcheck.Sign = (int)WalletTransactions.Signs.Credit;
                                            walletcheck.Amount = walletcheck.Amount + walletcheck.ServiceCharge;
                                            walletcheck.ParentTransactionId = ParentTransactionId;
                                            walletcheck.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
                                            walletcheck.Status = (int)WalletTransactions.Statuses.Refund;
                                            walletcheck.Remarks = "Refund Credit for Failed Transaction " + ParentTransactionId;
                                            walletcheck.Description = "Transaction Failed on " + Common.fnGetdatetime();
                                            walletcheck.CurrentBalance = CurrentBalance + walletcheck.Amount + walletcheck.ServiceCharge;
                                            walletcheck.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                                            walletcheck.CreatedDate = System.DateTime.UtcNow;
                                            walletcheck.UpdatedDate = System.DateTime.UtcNow;
                                            walletcheck.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                            objWalletTrans.VendorType = VendorType;
                                            objWalletTrans.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                            walletcheck.Add();
                                            Common.AddLogs(walletcheck.Remarks + " in TransactionID:" + walletcheck.TransactionUniqueId, true, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(walletcheck.MemberId), "", true, walletcheck.Platform, walletcheck.DeviceCode);
                                            msg = "success";
                                        }
                                    }
                                }
                                else
                                {
                                    msg = "success";
                                }
                                if (msg == "success")
                                {
                                    Models.Common.Common.AddLogs($"Transaction Id:{walletcheck.TransactionUniqueId} Status is Changed successfully from Pending to Failed. Action performed by : {Session["AdminUserName"].ToString()} with Remarks {txtRemarks}", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, platform, devicecode, (int)AddLog.LogActivityEnum.ChangeTxnStatus);
                                }
                            }
                            else
                            {
                                msg = "Transaction not updated.";
                            }
                        }
                    }
                }
            }
            return Json(msg);
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

        [Authorize]
        [HttpGet]
        public ActionResult BalanceHistoryReportReadyOnly()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult BalanceHistoryReportMerchantReadyOnly()
        {
            return View();
        }
    }
}