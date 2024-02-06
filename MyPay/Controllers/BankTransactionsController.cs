using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class BankTransactionsController : BaseAdminSessionController
    {
        // GET: BankTransactions

        [Authorize]
        public ActionResult Index()
        {
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
            List<SelectListItem> VendoTypes = CommonHelpers.GetSelectList_LinkBankVendoTypes();
            ViewBag.VendoTypes = VendoTypes;
            return View();
        }

        [Authorize]
        public JsonResult GetBankTransactionsLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SrNo");
            columns.Add("CreatedDate");
            columns.Add("UpdatedDate");
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
            string Name = context.Request.Form["MemberName"];
            string TransactionId = context.Request.Form["TransactionId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string DayWise = context.Request.Form["Today"];
            string Status = context.Request.Form["Status"];
            string GatewayTransactionId = context.Request.Form["GatewayTransactionId"];
            string Reference = context.Request.Form["Reference"];
            string VendorType = context.Request.Form["VendorType"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddBankTransactions> trans = new List<AddBankTransactions>();

            GetBankTransactions w = new GetBankTransactions();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.MemberName = Name;
            w.TransactionUniqueId = TransactionId;
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.VendorTransactionId = GatewayTransactionId;
            w.Status = Convert.ToInt32(Status);
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
            w.VendorType = Convert.ToInt32(VendorType);
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddBankTransactions
                     {
                         Sno = row["Sno"].ToString(),
                         MemberId = Convert.ToInt64(row["MemberId"]),
                         MemberName = row["MemberName"].ToString(),
                         TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                         VendorTransactionId = row["VendorTransactionId"].ToString(),
                         SignName = row["SignName"].ToString(),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         CreatedDatedt = row["IndiaDate"].ToString(),
                         SenderAccountNo = row["SenderAccountNo"].ToString(),
                         SenderBranchName = row["SenderBranchName"].ToString(),
                         SenderBankName = row["SenderBankName"].ToString(),
                         // RecieverName = row["RecieverName"].ToString(),
                         ServiceCharge = Convert.ToDecimal(row["ServiceCharge"]),
                         BatchId = row["BatchId"].ToString(),
                         InstructionId = row["InstructionId"].ToString(),
                         Remarks = row["Remarks"].ToString(),
                         CreditStatus = row["CreditStatus"].ToString(),
                         DebitStatus = row["DebitStatus"].ToString(),
                         Purpose = row["Purpose"].ToString(),
                         NetAmount = Convert.ToDecimal(row["NetAmount"].ToString()),
                         GatewayStatus = row["GatewayStatus"].ToString(),
                         TotalCredit = Convert.ToDecimal(row["TotalCredit"].ToString()),
                         TotalDebit = Convert.ToDecimal(row["TotalDebit"].ToString()),
                         AmountSum = Convert.ToDecimal(row["AmountSum"].ToString()),
                         FilterTotalCount = Convert.ToInt32(row["FilterTotalCount"].ToString()),
                         VendorType = Convert.ToInt32(row["VendorType"].ToString()),
                         StatusName = Enum.GetName(typeof(Models.Miscellaneous.WalletTransactions.Statuses), Convert.ToInt32(row["Status"])),
                         VendorTypeName = Enum.GetName(typeof(VendorApi_CommonHelper.VendorTypes), Convert.ToInt32(row["VendorType"])),
                         Sign = Convert.ToInt32(row["Sign"].ToString()),
                         Status = Convert.ToInt32(row["Status"].ToString()),
                         Reference = row["Reference"].ToString(),
                         UpdatedDatedt = row["UpdateIndiaDate"].ToString(),
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddBankTransactions>> objDataTableResponse = new DataTableResponse<List<AddBankTransactions>>
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
        public JsonResult GetBankTransferDepositOrderTxnStatusCheck(string TransactionId, string MemberId)
        {
            string authenticationToken = string.Empty;
            string UserInput = string.Empty;
            string Version = "1.0";
            string DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
            string PlatForm = "Web";
            CipsBatchDetail objRes = new CipsBatchDetail();
            string result = RepNCHL.CipsStatusJSONResponseProcess(TransactionId, MemberId, authenticationToken, Version, DeviceCode, PlatForm, ref objRes);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetConnectIpsStatus(string TransactionId)
        {

            string result = Common.ConnectIPSSuccess(TransactionId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetBankingStatus(string TransactionId)
        {

            string result = Common.BankStatusCheck(TransactionId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetLinkBankStatus(string TransactionId)
        {
            string result = "";
            try
            {

                AddDepositOrders outobject = new AddDepositOrders();
                GetDepositOrders inobject = new GetDepositOrders();
                inobject.TransactionId = TransactionId;
                AddDepositOrders res = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);


                if (res.Id > 0)
                {
                    if (res.GatewayType != (int)VendorApi_CommonHelper.VendorTypes.NCHL)
                    {
                        AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                        GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                        inuser.MemberId = Convert.ToInt64(res.MemberId);
                        AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                        if (resuser.Id > 0)
                        {
                            GetLinkBankTransactionStatus response = RepNps.CheckLoadWalletTransactionStatus(TransactionId);

                            if (response.data != null && response.data.TransactionStatus.ToLower() == "success" && response.code == "0")
                            {
                                AddBankTransactions bankoutuobject = new AddBankTransactions();
                                GetBankTransactions bankinuobject = new GetBankTransactions();
                                if (!string.IsNullOrEmpty(response.data.GatewayTransactionId))
                                {
                                    bankinuobject.VendorTransactionId = response.data.GatewayTransactionId.Trim();
                                }
                                else
                                {
                                    bankinuobject.TransactionUniqueId = TransactionId;
                                }
                                if (res.Type == (int)AddDepositOrders.DepositType.Linked_Bank_Deposit)
                                {
                                    WalletTransactions res_transaction = new WalletTransactions();
                                    res_transaction.ParentTransactionId = TransactionId;
                                    if (!res_transaction.GetRecordCheckExists())
                                    {
                                        res.RefferalsId = response.data.GatewayTransactionId;
                                        res.Particulars = response.message;
                                        res.JsonResponse = JsonConvert.SerializeObject(response);
                                        res.ResponseCode = response.code;
                                        //res.Status = (int)AddDepositOrders.DepositStatus.Refund;
                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(res, "depositorders");
                                        result = VendorApi_CommonHelper.RefundLinkedBankTransfer("", TransactionId, (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank, Convert.ToString((int)WalletTransactions.WalletTypes.Bank), resuser);

                                    }
                                    else
                                    {

                                        res.RefferalsId = response.data.GatewayTransactionId;
                                        res.Particulars = response.message;
                                        res.JsonResponse = JsonConvert.SerializeObject(response);
                                        res.ResponseCode = response.code;
                                        if (res.Status != (int)AddDepositOrders.DepositStatus.Refund)
                                        {
                                            res.Status = (int)AddDepositOrders.DepositStatus.Success;
                                        }

                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(res, "depositorders");

                                        if (res_transaction.GetRecord())
                                        {
                                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                            res_transaction.Update();
                                        }
                                        result = "Already Completed";
                                    }
                                }
                                else
                                {
                                    AddBankTransactions res_rewardpoint = RepCRUD<GetBankTransactions, AddBankTransactions>.GetRecord(nameof(Common.StoreProcedures.sp_BankTransactions_Get), bankinuobject, bankoutuobject);
                                    if (res_rewardpoint.Id > 0)
                                    {
                                        res_rewardpoint.Status = (int)WalletTransactions.Statuses.Success;
                                        res_rewardpoint.ResponseCode = response.code;
                                        res_rewardpoint.CreditStatus = response.message;
                                        res_rewardpoint.DebitStatus = response.message;
                                        res_rewardpoint.GatewayStatus = response.message;
                                        if (RepCRUD<AddBankTransactions, GetBankTransactions>.Update(res_rewardpoint, "banktransactions"))
                                        {
                                            WalletTransactions res_transaction = new WalletTransactions();
                                            res_transaction.ParentTransactionId = TransactionId;
                                            if (!res_transaction.GetRecordCheckExists())
                                            {
                                                res.RefferalsId = response.data.GatewayTransactionId;
                                                res.Particulars = response.message;
                                                res.JsonResponse = JsonConvert.SerializeObject(response);
                                                res.ResponseCode = response.code;
                                                res.Status = (int)AddDepositOrders.DepositStatus.Refund;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(res, "depositorders");
                                                result = VendorApi_CommonHelper.RefundLinkedBankTransfer("", TransactionId, (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank, Convert.ToString((int)WalletTransactions.WalletTypes.Bank), resuser);

                                            }
                                            else
                                            {

                                                res.RefferalsId = response.data.GatewayTransactionId;
                                                res.Particulars = response.message;
                                                res.JsonResponse = JsonConvert.SerializeObject(response);
                                                res.ResponseCode = response.code;
                                                if (res.Status != (int)AddDepositOrders.DepositStatus.Refund)
                                                {
                                                    res.Status = (int)AddDepositOrders.DepositStatus.Success;
                                                }

                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(res, "depositorders");

                                                if (res_transaction.GetRecord())
                                                {
                                                    res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                                    res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                                    res_transaction.Update();
                                                }
                                                result = "Already Completed";
                                            }
                                        }
                                        else
                                        {
                                            result = "Transaction Not Updated";
                                        }
                                    }
                                    else
                                    {

                                        res.RefferalsId = response.data.GatewayTransactionId;
                                        res.Particulars = response.message;
                                        res.JsonResponse = JsonConvert.SerializeObject(response);
                                        res.ResponseCode = response.code;
                                        res.Status = (int)AddDepositOrders.DepositStatus.Refund;
                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(res, "depositorders");

                                        result = VendorApi_CommonHelper.RefundLinkedBankTransfer("", TransactionId, (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank, Convert.ToString((int)WalletTransactions.WalletTypes.Bank), resuser, "", res.TransactionId);

                                    }
                                }
                            }
                            else if (response.code == "1" && response.errors.Count > 0)
                            {
                                if (response.data != null)
                                {
                                    res.RefferalsId = response.data.GatewayTransactionId;
                                }
                                res.Particulars = response.errors[0].error_message;
                                res.JsonResponse = JsonConvert.SerializeObject(response);
                                res.ResponseCode = response.code;
                                res.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(res, "depositorders");
                                result = response.errors[0].error_message;
                            }
                            else if (response.code == "2")
                            {
                                if (response.data != null)
                                {
                                    res.RefferalsId = response.data.GatewayTransactionId;
                                }

                                res.Particulars = response.message;
                                res.JsonResponse = JsonConvert.SerializeObject(response);
                                res.ResponseCode = response.code;
                                res.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(res, "depositorders");
                                result = response.message;
                            }
                            else
                            {
                                result = "Data Not Found";
                            }
                        }
                        else
                        {
                            result = "User Not Found";
                        }
                    }
                    else
                    {
                        result = "Check Status Not Available For NCHL Link Bank Transactions";
                    }
                }
                else
                {
                    result = "Deposit Order Not Found";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportExcel(Int64 MemberId, string MemberName, string TxnId, string FromDate, string ToDate, string GatewayTxnId, string Status, string DayWise, string Reference)
        {
            var fileName = "BankTransactions-" + DateTime.Now.ToShortDateString() + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetBankTransactions w = new GetBankTransactions();

            w.MemberId = Convert.ToInt64(MemberId);
            w.MemberName = MemberName;
            w.TransactionUniqueId = TxnId;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.Status = Convert.ToInt32(Status);
            w.VendorTransactionId = GatewayTxnId;
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
                dt = dt.DefaultView.ToTable(false, "Sno", "IndiaDate", "TransactionUniqueId", "VendorTransactionId", "Reference", "BatchId", "InstructionId", "MemberId", "MemberName", "Amount", "SignName", "Type", "CreditStatus", "DebitStatus", "GatewayStatus", "StatusName", "Purpose", "SenderAccountNo", "SenderBankName", "SenderBranchName", "ServiceCharge", "NetAmount", "Remarks");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["IndiaDate"].ColumnName = "Date";
                dt.Columns["MemberId"].ColumnName = "Member Id";
                dt.Columns["TransactionUniqueId"].ColumnName = "Transaction Id";
                dt.Columns["VendorTransactionId"].ColumnName = "Gateway Txn Id";
                dt.Columns["Reference"].ColumnName = "Tracker Id";
                dt.Columns["BatchId"].ColumnName = "Batch Id";
                dt.Columns["InstructionId"].ColumnName = "Instruction Id";
                dt.Columns["MemberName"].ColumnName = "Member Name";
                dt.Columns["Amount"].ColumnName = "Amount (Rs)";
                dt.Columns["SignName"].ColumnName = "Sign";
                dt.Columns["Type"].ColumnName = "Type";
                dt.Columns["CreditStatus"].ColumnName = "Credit Status";
                dt.Columns["DebitStatus"].ColumnName = "Debit Status";
                dt.Columns["GatewayStatus"].ColumnName = "Gateway Status";
                dt.Columns["StatusName"].ColumnName = "My Pay Status";
                dt.Columns["Purpose"].ColumnName = "Purpose";
                dt.Columns["SenderAccountNo"].ColumnName = "Sender AccountNo";
                dt.Columns["SenderBankName"].ColumnName = "Sender Bank Name";
                dt.Columns["SenderBranchName"].ColumnName = "Sender Branch Name";
                dt.Columns["ServiceCharge"].ColumnName = "Service Charge";
                dt.Columns["NetAmount"].ColumnName = "Net Amount (Rs)";
                dt.Columns["Remarks"].ColumnName = "Remarks";
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

                AddDepositOrders outobject = new AddDepositOrders();
                GetDepositOrders inobject = new GetDepositOrders();
                inobject.TransactionId = TxnId;
                inobject.Status = (int)AddDepositOrders.DepositStatus.Pending;
                AddDepositOrders resDepositOrder = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);

                if (resDepositOrder.Id > 0)
                {
                    AddBankTransactions bankoutuobject = new AddBankTransactions();
                    GetBankTransactions bankinuobject = new GetBankTransactions();
                    bankinuobject.TransactionUniqueId = TxnId;
                    bankinuobject.Status = (int)WalletTransactions.Statuses.Pending;
                    AddBankTransactions res_bankTransaction = RepCRUD<GetBankTransactions, AddBankTransactions>.GetRecord(nameof(Common.StoreProcedures.sp_BankTransactions_Get), bankinuobject, bankoutuobject);
                    if (res_bankTransaction.Id > 0)
                    {
                        if (res_bankTransaction.Status != (int)WalletTransactions.Statuses.Pending)
                        {
                            msg = "Transaction is not Pending.";
                        }
                        else if (res_bankTransaction.Type == (int)WalletTransactions.Types.Refund)
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
                                    AddBankTransactions gateWayoutuobject = new AddBankTransactions();
                                    GetBankTransactions gateWayinuobject = new GetBankTransactions();
                                    gateWayinuobject.VendorTransactionId = txtGatewayID;
                                    gateWayinuobject.Status = (int)WalletTransactions.Statuses.Success;
                                    AddBankTransactions resGateway_bankTransaction = RepCRUD<GetBankTransactions, AddBankTransactions>.GetRecord(nameof(Common.StoreProcedures.sp_BankTransactions_Get), gateWayinuobject, gateWayoutuobject);
                                    if (resGateway_bankTransaction.Id > 0)
                                    {
                                        msg = "Duplicate Gateway transaction ID";
                                    }

                                    else
                                    {

                                        res_bankTransaction.Status = (int)WalletTransactions.Statuses.Success;
                                        res_bankTransaction.VendorTransactionId = txtGatewayID;
                                        res_bankTransaction.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                        res_bankTransaction.UpdatedByName = Session["AdminUserName"].ToString();
                                        res_bankTransaction.UpdatedDate = DateTime.UtcNow;
                                        res_bankTransaction.Remarks = txtRemarks;
                                        if (RepCRUD<AddBankTransactions, GetBankTransactions>.Update(res_bankTransaction, "banktransactions"))
                                        {

                                            resDepositOrder.Status = (int)AddDepositOrders.DepositStatus.Success;
                                            RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDepositOrder, "depositorders");
                                            Models.Common.Common.AddLogs($"Bank Transaction Id:{res_bankTransaction.TransactionUniqueId} Status is Changed successfully from Pending to Success with Gateway Txn ID as {txtGatewayID}. Action performed by : {Session["AdminUserName"].ToString()} with Remarks {txtRemarks}", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, platform, devicecode, (int)AddLog.LogActivityEnum.ChangeTxnStatus);
                                            msg = "success";
                                        }
                                    }
                                }
                            }
                            else if (Status == ((int)WalletTransactions.Statuses.Failed).ToString())
                            {
                                res_bankTransaction.Status = (int)WalletTransactions.Statuses.Failed;
                                res_bankTransaction.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                res_bankTransaction.UpdatedByName = Session["AdminUserName"].ToString();
                                res_bankTransaction.UpdatedDate = DateTime.UtcNow;
                                res_bankTransaction.Remarks = txtRemarks;
                                if (RepCRUD<AddBankTransactions, GetBankTransactions>.Update(res_bankTransaction, "banktransactions"))
                                {

                                    resDepositOrder.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                    RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDepositOrder, "depositorders");
                                    Models.Common.Common.AddLogs($"Bank Transaction Id:{res_bankTransaction.TransactionUniqueId} Status is Changed successfully from Pending to Failed . Action performed by : {Session["AdminUserName"].ToString()} with Remarks {txtRemarks}", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString(), false, platform, devicecode, (int)AddLog.LogActivityEnum.ChangeTxnStatus);
                                    msg = "success";
                                }
                            }

                        }
                    }

                    else
                    {
                        msg = "Bank Transaction Not Found";
                    }
                }
                else
                {
                    msg = "Deposit Order Not Found";
                }
            }
            return Json(msg);
        }
    }
}