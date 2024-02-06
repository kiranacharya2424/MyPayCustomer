using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.VendorAPI.Get._NICASIA;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class UserAuthorizationsController : BaseUserAuthorizationSessionController
    {
        // GET: UserAuthorizations
        public ActionResult Index(int amount, string remarks, string particulars, string MemberId, decimal servicecharge, string PlatForm = "android")
        {
            string msg = "";


            Add_ConnectIPS obj = new Add_ConnectIPS();
            if (amount <= 0)
            {
                ViewBag.Message = "Amount should be greater than 0";
                return View(obj);
            }
            if (servicecharge < 0)
            {
                ViewBag.Message = "Service charge should be greater than 0";
                return View(obj);
            }
            AddUserLoginWithPin outuser = new AddUserLoginWithPin();
            GetUserLoginWithPin inuser = new GetUserLoginWithPin();
            inuser.MemberId = Convert.ToInt64(MemberId);
            AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
            if (resuser.Id > 0)
            {
                msg = Common.CheckTransactionLimit((int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_connectips, (int)AddTransactionLimit.TransactionTransferTypeEnum.Load_Wallet_From_Bank, resuser.MemberId, Convert.ToDecimal(amount) + servicecharge).ToLower();
                if (msg == "success")
                {
                    Guid g = Guid.NewGuid();

                    obj.APPID = RepNCHL.APPID;
                    obj.MERCHANTID = RepNCHL.MERCHANTID;
                    obj.APPNAME = RepNCHL.APPNAME;
                    obj.TXNID = DateTime.UtcNow.ToString("ddMMyyyyhhmmssmsff");
                    obj.TXNDATE = DateTime.UtcNow.ToString("dd-MM-yyyy");
                    obj.TXNCRNCY = "NPR";
                    obj.TXNAMT = Convert.ToInt32((amount + servicecharge) * 100);
                    obj.REFERENCEID = MemberId;
                    obj.REMARKS = particulars;
                    obj.PARTICULARS = "Connect IPS Payment Initiate";
                    obj.TOKEN = Common.GenerateConnectIPSToken("MERCHANTID=" + obj.MERCHANTID + ",APPID=" + obj.APPID + ",APPNAME=" + obj.APPNAME + ",TXNID=" + obj.TXNID + ",TXNDATE=" + obj.TXNDATE + ",TXNCRNCY=" + obj.TXNCRNCY + ",TXNAMT=" + obj.TXNAMT.ToString() + ",REFERENCEID=" + obj.REFERENCEID + ",REMARKS=" + obj.REMARKS + ",PARTICULARS=" + obj.PARTICULARS + ",TOKEN=TOKEN");
                    Common.AddLogs("GenerateConnectIPSToken TXNId:" + obj.TXNID + JsonConvert.SerializeObject(obj), false, Convert.ToInt32(AddLog.LogType.DBLogs), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                    AddDepositOrders outobjectver = new AddDepositOrders();
                    outobjectver.Amount = Convert.ToDecimal(amount);
                    outobjectver.CreatedBy = 10000;
                    outobjectver.CreatedByName = "Admin";
                    outobjectver.TransactionId = obj.TXNID;
                    outobjectver.RefferalsId = obj.TXNID;
                    outobjectver.MemberId = Convert.ToInt64(MemberId);
                    outobjectver.Remarks = obj.REMARKS;
                    outobjectver.Type = (int)AddDepositOrders.DepositType.ConnectIPS;
                    outobjectver.Particulars = obj.PARTICULARS;
                    outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                    outobjectver.IsActive = true;
                    outobjectver.ServiceCharges = servicecharge;
                    outobjectver.IsApprovedByAdmin = true;
                    outobjectver.Platform = PlatForm;
                    outobjectver.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                    outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;
                    outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NCHL.ToString();
                    Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");
                    if (Id > 0)
                    {
                        Common.AddLogs("Add DepositOrder By This TXNId:" + obj.TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);


                    }
                }
                else
                {
                    ViewBag.Message = msg;
                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                }
            }
            else
            {
                Common.AddLogs("MemberId Not Found", false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                ViewBag.Message = "MemberId Not Found";
            }
            //string result = RepNCHL.ConnectRequest(MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), "NPR", MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), 1000, "TEST", "TEST");
            //result = result.Replace("/connectipswebgw/", RepNCHL.NCHLApiUrl + "connectipswebgw/");
            ////Common.createSha256("data to be signed");
            return View(obj);
        }

        public ActionResult Banking(string amount, string remarks, string particulars, string code, string MemberId, decimal servicecharge, int Type = 0, string PlatForm = "android")
        {
            string msg = "";
            AddBankingNPS obj = new AddBankingNPS();
            if (Convert.ToInt32(amount) <= 0)
            {
                ViewBag.Message = "Amount should be greater than 0";
                return View(obj);
            }
            if (servicecharge < 0)
            {
                ViewBag.Message = "Service charge should be greater than 0";
                return View(obj);
            }
            AddUserLoginWithPin outuser = new AddUserLoginWithPin();
            GetUserLoginWithPin inuser = new GetUserLoginWithPin();
            inuser.MemberId = Convert.ToInt64(MemberId);
            AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
            if (resuser.Id > 0)
            {
                msg = Common.CheckTransactionLimit((int)VendorApi_CommonHelper.KhaltiAPIName.Mobile_Banking, (int)AddTransactionLimit.TransactionTransferTypeEnum.Load_Wallet_From_Bank, resuser.MemberId, Convert.ToDecimal(amount) + servicecharge).ToLower();
                if (msg == "success")
                {
                    string TransactionId = new CommonHelpers().GenerateUniqueId();
                    GetProcessId pro = RepNps.GetProcessId(Convert.ToDecimal(Convert.ToDecimal(amount).ToString("f2")) + servicecharge, TransactionId);
                    if (!string.IsNullOrEmpty(pro.data.ProcessId))
                    {


                        obj.MerchantId = RepNps.MerchantId;
                        obj.MerchantName = RepNps.MerchantName;
                        obj.Amount = Convert.ToDecimal(Convert.ToDecimal(amount).ToString("f2")) + servicecharge;
                        obj.MerchantTxnId = TransactionId;
                        obj.TransactionRemarks = "Banking Payment Initiate";
                        obj.InstrumentCode = /*"NICENPKA"*/code;
                        obj.ProcessId = pro.data.ProcessId;
                        //obj.Signature = RepNps.HMACSHA512(obj.Amount+obj.InstrumentCode+obj.MerchantId+obj.MerchantName+obj.MerchantTxnId+obj.ProcessId+obj.TransactionRemarks);

                        AddDepositOrders outobjectver = new AddDepositOrders();
                        outobjectver.Amount = Convert.ToDecimal(amount) + servicecharge;
                        outobjectver.CreatedBy = 10000;
                        outobjectver.CreatedByName = "Admin";
                        outobjectver.TransactionId = TransactionId;
                        outobjectver.RefferalsId = pro.data.ProcessId;
                        outobjectver.MemberId = Convert.ToInt64(MemberId);
                        outobjectver.Remarks = particulars;
                        if (Type == 1)
                        {
                            outobjectver.Type = (int)AddDepositOrders.DepositType.Internet_Banking;
                        }
                        else
                        {
                            outobjectver.Type = (int)AddDepositOrders.DepositType.Mobile_Banking;
                        }
                        outobjectver.Particulars = particulars;
                        outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                        outobjectver.IsActive = true;
                        outobjectver.ServiceCharges = servicecharge;
                        outobjectver.IsApprovedByAdmin = true;
                        outobjectver.Platform = PlatForm;
                        outobjectver.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NPS;
                        outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NPS.ToString();
                        Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");
                        if (Id > 0)
                        {
                            if (Type == 1)
                            {
                                Common.AddLogs("Add DepositOrder By This TXNId:" + TransactionId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                            }
                            else
                            {
                                Common.AddLogs("Add DepositOrder By This TXNId:" + TransactionId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                            }
                        }
                        else
                        {
                            if (Type == 1)
                            {
                                Common.AddLogs("Add DepositOrder Not Addedd By This TXNId:" + TransactionId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                            }
                            else
                            {
                                Common.AddLogs("Add DepositOrder Not Addedd By This TXNId:" + TransactionId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                            }

                        }
                    }
                    else
                    {
                        if (Type == 1)
                        {
                            Common.AddLogs("ProcessId Not Generated By This TXNId:" + TransactionId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                        }
                        else
                        {
                            Common.AddLogs("ProcessId Not Generated By This TXNId:" + TransactionId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                        }
                    }
                }
                else
                {
                    ViewBag.Message = msg;
                    if (Type == 1)
                    {
                        Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                    }
                    else
                    {
                        Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                    }
                }
            }
            else
            {
                if (Type == 1)
                {
                    Common.AddLogs("MemberId Not Found", false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                }
                else
                {
                    Common.AddLogs("MemberId Not Found", false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                }
                ViewBag.Message = "MemberId Not Found";
            }
            //string result = RepNCHL.ConnectRequest(MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), "NPR", MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), 1000, "TEST", "TEST");
            //result = result.Replace("/connectipswebgw/", RepNCHL.NCHLApiUrl + "connectipswebgw/");
            ////Common.createSha256("data to be signed");
            return View(obj);
        }

        public ActionResult CardSuccess()
        {
            string auth_trans_ref_no = Request.Form["auth_trans_ref_no"];
            string reference_number = Request.Form["req_reference_number"];
            string access_key = Request.Form["req_access_key"];
            string profile_id = Request.Form["req_profile_id"];
            string transaction_uuid = Request.Form["req_transaction_uuid"];
            string amount = Request.Form["req_amount"];
            string decision = Request.Form["decision"];
            string reason_code = Request.Form["reason_code"];
            string transaction_id = Request.Form["transaction_id"];
            string message = Request.Form["message"];
            string cardnumber = Request.Form["req_card_number"];
            string req_card_expiry_date = Request.Form["req_card_expiry_date"];
            string card_type_name = Request.Form["card_type_name"];
            if (!string.IsNullOrEmpty(reason_code) && !string.IsNullOrEmpty(decision) && !string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(auth_trans_ref_no) && !string.IsNullOrEmpty(reference_number) && !string.IsNullOrEmpty(access_key) && !string.IsNullOrEmpty(profile_id) && !string.IsNullOrEmpty(transaction_uuid))
            {
                if (access_key == Common.NICAccess_Key && profile_id == Common.NICprofile_id)
                {

                    AddDepositOrders outobject = new AddDepositOrders();
                    GetDepositOrders inobject = new GetDepositOrders();
                    inobject.RefferalsId = auth_trans_ref_no;
                    AddDepositOrders res = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
                    if (res.Id > 0)
                    {
                        AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                        GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                        inuser.MemberId = Convert.ToInt64(res.MemberId);
                        AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                        if (resuser.Id > 0)
                        {
                            if (!string.IsNullOrEmpty(transaction_id))
                            {

                                if (decision != "ACCEPT")
                                {
                                    outobject.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                }
                                outobject.Particulars = message;
                                outobject.ResponseCode = reason_code;
                                if (RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders"))
                                {

                                    decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) + (Convert.ToDecimal(amount) - res.ServiceCharges));
                                    WalletTransactions res_transaction = new WalletTransactions();
                                    res_transaction.VendorTransactionId = transaction_id;
                                    if (decision == "ACCEPT" && reason_code == "100")
                                    {
                                        if (!res_transaction.GetRecordCheckExists())
                                        {
                                            res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.ContactNumber = resuser.ContactNumber;
                                            res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            res_transaction.Amount = Convert.ToDecimal(amount) - res.ServiceCharges;
                                            res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            res_transaction.CurrentBalance = WalletBalance;
                                            res_transaction.CreatedBy = res.CreatedBy;
                                            res_transaction.CreatedByName = res.CreatedByName;
                                            res_transaction.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                                            res_transaction.Remarks = "Wallet Credited Successfully";
                                            res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit;
                                            res_transaction.Description = outobject.Particulars;
                                            res_transaction.Purpose = res.Remarks;
                                            res_transaction.Reference = auth_trans_ref_no;
                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                            res_transaction.Reference = transaction_uuid;
                                            res_transaction.IsApprovedByAdmin = true;
                                            res_transaction.IsActive = true;
                                            res_transaction.CardNumber = cardnumber;
                                            res_transaction.CardType = card_type_name;
                                            res_transaction.ExpiryDate = req_card_expiry_date;
                                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                            res_transaction.GatewayStatus = decision;
                                            res_transaction.ServiceCharge = res.ServiceCharges;
                                            res_transaction.NetAmount = res_transaction.Amount + res_transaction.ServiceCharge;
                                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                            res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NIS_Asia;
                                            if (res_transaction.Add())
                                            {
                                                //if(resuser.RefId!=0)
                                                //{
                                                //    Common.FirstTransactionCommisipon(resuser, res, res_transaction.TransactionUniqueId);
                                                //}

                                                outobject.Status = (int)AddDepositOrders.DepositStatus.Success;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders");


                                                string Title = "Card Payment";
                                                string Message = "Hello " + resuser.FirstName + "! Card Payment Transaction Completed Successfully By Transaction ID :" + transaction_uuid;
                                                Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit, res.MemberId, Title, Message);
                                                Common.AddLogs("Wallet Credited Successfully By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                ViewBag.Message = "Transaction Completed Successfully";
                                            }
                                        }
                                        else
                                        {
                                            Common.AddLogs("Transaction Already Updated By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                            ViewBag.Message = "Transaction Completed Successfully";
                                        }
                                    }
                                    else
                                    {
                                        Common.AddLogs(message + " By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                        ViewBag.Message = message;
                                    }


                                }
                                else
                                {
                                    Common.AddLogs("Deposit Order Not Updated By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);


                                    ViewBag.Message = "Transaction Not Updated";
                                }
                            }
                            else
                            {
                                Common.AddLogs("Transaction Not Found By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                ViewBag.Message = "Transaction Not Found";
                            }
                        }
                        else
                        {
                            Common.AddLogs("User Not Found By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                            ViewBag.Message = "User Not Found";
                        }
                    }
                    else
                    {

                        ViewBag.Message = "Order Not Found";
                    }

                }
                else
                {
                    ViewBag.Message = "Not Valid Transaction";
                }
            }
            else
            {
                ViewBag.Message = "Transaction Reponse Not Valid";
            }

            if (!ViewBag.Message.ToString().ToLower().Contains("success"))
            {
                return RedirectToAction("Cancel", new { msg = ViewBag.Message });
            }
            return View();
        }

        public ActionResult Card(decimal amount, string remarks, string particulars, string MemberId, decimal servicecharge, string PlatForm = "android")
        {
            string msg = "";
            Req_NIC_Card obj = new Req_NIC_Card();
            if (amount <= 0)
            {
                ViewBag.Message = "Amount should be greater than 0";
                return View(obj);
            }
            if (servicecharge <= 0)
            {
                ViewBag.Message = "Service charge should be greater than 0";
                return View(obj);
            }
            AddUser outuser = new AddUser();
            GetUser inuser = new GetUser();
            inuser.MemberId = Convert.ToInt64(MemberId);
            AddUser resuser = RepCRUD<GetUser, AddUser>.GetRecord(nameof(Common.StoreProcedures.sp_Users_Get), inuser, outuser);
            if (resuser.Id > 0)
            {
                msg = Common.CheckTransactionLimit((int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit, (int)AddTransactionLimit.TransactionTransferTypeEnum.Load_via_Card, resuser.MemberId, Convert.ToDecimal(amount) + servicecharge).ToLower();
                if (msg == "success")
                {
                    Guid g = Guid.NewGuid();

                    obj.access_key = Common.NICAccess_Key;
                    obj.profile_id = Common.NICprofile_id;
                    obj.transaction_uuid = g.ToString();
                    obj.signed_field_names = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,auth_trans_ref_no,transaction_type,reference_number,amount,currency,payment_method,bill_to_forename,bill_to_surname,bill_to_email,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_address_state,bill_to_address_country,bill_to_address_postal_code";
                    obj.unsigned_field_names = "card_type,card_number,card_expiry_date";
                    obj.signed_date_time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    obj.locale = "en";
                    obj.auth_trans_ref_no = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + Common.RandomNumber(1111, 9999);
                    obj.amount = amount + servicecharge;
                    obj.bill_to_forename = resuser.FirstName;
                    obj.bill_to_surname = resuser.LastName;
                    obj.bill_to_email = resuser.Email;
                    obj.bill_to_phone = resuser.ContactNumber;
                    obj.bill_to_address_line1 = resuser.Address;
                    obj.bill_to_address_city = resuser.City;
                    obj.bill_to_address_state = resuser.State;
                    obj.bill_to_address_postal_code = resuser.ZipCode;
                    obj.bill_to_address_country = "NP";
                    obj.transaction_type = "sale";
                    obj.reference_number = MemberId.ToString();
                    obj.currency = "NPR";
                    obj.payment_method = "card";
                    obj.signature = Common.GenerateHashNIC("access_key=" + obj.access_key + ",profile_id=" + obj.profile_id + ",transaction_uuid=" + obj.transaction_uuid + ",signed_field_names=" + obj.signed_field_names + ",unsigned_field_names=" + obj.unsigned_field_names + ",signed_date_time=" + obj.signed_date_time + ",locale=" + obj.locale + ",auth_trans_ref_no=" + obj.auth_trans_ref_no + ",transaction_type=" + obj.transaction_type + ",reference_number=" + obj.reference_number + ",amount=" + obj.amount + ",currency=" + obj.currency + ",payment_method=" + obj.payment_method + ",bill_to_forename=" + obj.bill_to_forename + ",bill_to_surname=" + obj.bill_to_surname + ",bill_to_email=" + obj.bill_to_email + ",bill_to_phone=" + obj.bill_to_phone + ",bill_to_address_line1=" + obj.bill_to_address_line1 + ",bill_to_address_city=" + obj.bill_to_address_city + ",bill_to_address_state=" + obj.bill_to_address_state + ",bill_to_address_country=" + obj.bill_to_address_country + ",bill_to_address_postal_code=" + obj.bill_to_address_postal_code);
                    obj.card_type = "001";
                    obj.card_number = "";
                    obj.card_expiry_date = "";

                    AddDepositOrders outobjectver = new AddDepositOrders();
                    outobjectver.Amount = amount;
                    outobjectver.CreatedBy = 10000;
                    outobjectver.CreatedByName = "Admin";
                    outobjectver.TransactionId = obj.transaction_uuid;
                    outobjectver.RefferalsId = obj.auth_trans_ref_no;
                    outobjectver.MemberId = Convert.ToInt64(MemberId);
                    outobjectver.Type = (int)AddDepositOrders.DepositType.Card;
                    outobjectver.Remarks = particulars;
                    outobjectver.Particulars = "Card Payment Initiate";
                    outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                    outobjectver.IsActive = true;
                    outobjectver.IsApprovedByAdmin = true;
                    outobjectver.ServiceCharges = servicecharge;
                    outobjectver.Platform = PlatForm;
                    outobjectver.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;

                    outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NIS_Asia;
                    outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NIS_Asia.ToString();

                    Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");
                    if (Id > 0)
                    {
                        Common.AddLogs("Add DepositOrders By This TXNId:" + obj.transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                    }
                }
                else
                {
                    ViewBag.Message = msg;
                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                }
            }
            else
            {
                ViewBag.Message = "User Not Found ";
                Common.AddLogs("User Not Found ", false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

            }
            //string result = RepNCHL.ConnectRequest(MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), "NPR", MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), 1000, "TEST", "TEST");
            //result = result.Replace("/connectipswebgw/", RepNCHL.NCHLApiUrl + "connectipswebgw/");
            ////Common.createSha256("data to be signed");
            return View(obj);
        }


        /// <summary>
        /// Payment With Card..
        /// </summary>
        /// <param name="TXNID"></param>
        /// <returns></returns>
        /// 
        public ActionResult CardPaymentSuccess()
        {
            string auth_trans_ref_no = Request.Form["auth_trans_ref_no"];
            string reference_number = Request.Form["req_reference_number"];
            string access_key = Request.Form["req_access_key"];
            string profile_id = Request.Form["req_profile_id"];
            string transaction_uuid = Request.Form["req_transaction_uuid"];
            string amount = Request.Form["req_amount"];
            string decision = Request.Form["decision"];
            string reason_code = Request.Form["reason_code"];
            string transaction_id = Request.Form["transaction_id"];
            string message = Request.Form["message"];
            string cardnumber = Request.Form["req_card_number"];
            string req_card_expiry_date = Request.Form["req_card_expiry_date"];
            string card_type_name = Request.Form["card_type_name"];
            if (!string.IsNullOrEmpty(reason_code) && !string.IsNullOrEmpty(decision) && !string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(auth_trans_ref_no) && !string.IsNullOrEmpty(reference_number) && !string.IsNullOrEmpty(access_key) && !string.IsNullOrEmpty(profile_id) && !string.IsNullOrEmpty(transaction_uuid))
            {
                if (access_key == Common.NICAccess_Key && profile_id == Common.NICprofile_id)
                {

                    AddDepositOrders outobject = new AddDepositOrders();
                    GetDepositOrders inobject = new GetDepositOrders();
                    inobject.RefferalsId = auth_trans_ref_no;
                    AddDepositOrders res = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
                    if (res.Id > 0)
                    {
                        AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                        GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                        inuser.MemberId = Convert.ToInt64(res.MemberId);
                        AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                        if (resuser.Id > 0)
                        {
                            if (!string.IsNullOrEmpty(transaction_id))
                            {

                                if (decision != "ACCEPT")
                                {
                                    outobject.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                }
                                outobject.Particulars = message;
                                outobject.ResponseCode = reason_code;
                                if (RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders"))
                                {

                                    decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) + (Convert.ToDecimal(amount) - res.ServiceCharges));
                                    WalletTransactions res_transaction = new WalletTransactions();
                                    res_transaction.VendorTransactionId = transaction_id;
                                    if (decision == "ACCEPT" && reason_code == "100")
                                    {
                                        if (!res_transaction.GetRecordCheckExists())
                                        {
                                            res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.ContactNumber = resuser.ContactNumber;
                                            res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            res_transaction.Amount = Convert.ToDecimal(amount) - res.ServiceCharges;
                                            res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            res_transaction.CurrentBalance = WalletBalance;
                                            res_transaction.CreatedBy = res.CreatedBy;
                                            res_transaction.CreatedByName = res.CreatedByName;
                                            res_transaction.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                                            res_transaction.Remarks = "Wallet Credited Successfully";
                                            res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit;
                                            res_transaction.Description = outobject.Particulars;
                                            res_transaction.Purpose = res.Remarks;
                                            res_transaction.Reference = auth_trans_ref_no;
                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                            res_transaction.Reference = transaction_uuid;
                                            res_transaction.IsApprovedByAdmin = true;
                                            res_transaction.IsActive = true;
                                            res_transaction.CardNumber = cardnumber;
                                            res_transaction.CardType = card_type_name;
                                            res_transaction.ExpiryDate = req_card_expiry_date;
                                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                            res_transaction.GatewayStatus = decision;
                                            res_transaction.ServiceCharge = res.ServiceCharges;
                                            res_transaction.NetAmount = res_transaction.Amount + res_transaction.ServiceCharge;
                                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                            res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NIS_Asia;
                                            if (res_transaction.Add())
                                            {
                                                //if(resuser.RefId!=0)
                                                //{
                                                //    Common.FirstTransactionCommisipon(resuser, res, res_transaction.TransactionUniqueId);
                                                //}

                                                outobject.Status = (int)AddDepositOrders.DepositStatus.Success;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders");


                                                string Title = "Card Payment";
                                                string Message = "Hello " + resuser.FirstName + "! Card Payment Transaction Completed Successfully By Transaction ID :" + transaction_uuid;
                                                Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit, res.MemberId, Title, Message);
                                                Common.AddLogs("Wallet Credited Successfully By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                ViewBag.Message = "Transaction Completed Successfully";
                                            }
                                        }
                                        else
                                        {
                                            Common.AddLogs("Transaction Already Updated By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                            ViewBag.Message = "Transaction Completed Successfully";
                                        }
                                    }
                                    else
                                    {
                                        Common.AddLogs(message + " By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                        ViewBag.Message = message;
                                    }


                                }
                                else
                                {
                                    Common.AddLogs("Deposit Order Not Updated By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);


                                    ViewBag.Message = "Transaction Not Updated";
                                }
                            }
                            else
                            {
                                Common.AddLogs("Transaction Not Found By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                ViewBag.Message = "Transaction Not Found";
                            }
                        }
                        else
                        {
                            Common.AddLogs("User Not Found By This TXNId:" + transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                            ViewBag.Message = "User Not Found";
                        }
                    }
                    else
                    {

                        ViewBag.Message = "Order Not Found";
                    }

                }
                else
                {
                    ViewBag.Message = "Not Valid Transaction";
                }
            }
            else
            {
                ViewBag.Message = "Transaction Reponse Not Valid";
            }

            if (!ViewBag.Message.ToString().ToLower().Contains("success"))
            {
                return RedirectToAction("Cancel", new { msg = ViewBag.Message });
            }
            return View();
        }

        public ActionResult CardPayment(decimal amount, string remarks, string particulars, string MemberId, decimal servicecharge, string PlatForm = "android")
        {
            string msg = "";
            Req_NIC_Card obj = new Req_NIC_Card();
            if (amount <= 0)
            {
                ViewBag.Message = "Amount should be greater than 0";
                return View(obj);
            }
            if (servicecharge < 0)
            {
                ViewBag.Message = "Service charge should be greater than or equals to 0";
                return View(obj);
            }
            AddUser outuser = new AddUser();
            GetUser inuser = new GetUser();
            inuser.MemberId = Convert.ToInt64(MemberId);
            AddUser resuser = RepCRUD<GetUser, AddUser>.GetRecord(nameof(Common.StoreProcedures.sp_Users_Get), inuser, outuser);
            if (resuser.Id > 0)
            {
                msg = Common.CheckTransactionLimit((int)VendorApi_CommonHelper.KhaltiAPIName.VISA_Master_Card, (int)AddTransactionLimit.TransactionTransferTypeEnum.Load_via_Card, resuser.MemberId, Convert.ToDecimal(amount) + servicecharge).ToLower();
                if (msg == "success")
                {
                    Guid g = Guid.NewGuid();

                    obj.access_key = Common.VISA_CardPaymentNICAccess_Key;
                    obj.profile_id = Common.VISA_CardPaymentNICprofile_id;
                    obj.transaction_uuid = g.ToString();
                    obj.signed_field_names = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,auth_trans_ref_no,transaction_type,reference_number,amount,currency,payment_method,bill_to_forename,bill_to_surname,bill_to_email,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_address_state,bill_to_address_country,bill_to_address_postal_code";
                    obj.unsigned_field_names = "card_type,card_number,card_expiry_date";
                    obj.signed_date_time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    obj.locale = "en";
                    obj.auth_trans_ref_no = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + Common.RandomNumber(1111, 9999);
                    obj.amount = amount + servicecharge;
                    obj.bill_to_forename = resuser.FirstName;
                    obj.bill_to_surname = resuser.LastName;
                    obj.bill_to_email = resuser.Email;
                    obj.bill_to_phone = resuser.ContactNumber;
                    obj.bill_to_address_line1 = "N/A";
                    obj.bill_to_address_city = "N/A";
                    obj.bill_to_address_state = "N/A";
                    obj.bill_to_address_postal_code = "N/A";
                    obj.bill_to_address_country = "NP";
                    obj.transaction_type = "sale";
                    obj.reference_number = MemberId.ToString();
                    obj.currency = "NPR";
                    obj.payment_method = "card";
                    obj.signature = Common.GenerateHashVISACardNIC("access_key=" + obj.access_key + ",profile_id=" + obj.profile_id + ",transaction_uuid=" + obj.transaction_uuid + ",signed_field_names=" + obj.signed_field_names + ",unsigned_field_names=" + obj.unsigned_field_names + ",signed_date_time=" + obj.signed_date_time + ",locale=" + obj.locale + ",auth_trans_ref_no=" + obj.auth_trans_ref_no + ",transaction_type=" + obj.transaction_type + ",reference_number=" + obj.reference_number + ",amount=" + obj.amount + ",currency=" + obj.currency + ",payment_method=" + obj.payment_method + ",bill_to_forename=" + obj.bill_to_forename + ",bill_to_surname=" + obj.bill_to_surname + ",bill_to_email=" + obj.bill_to_email + ",bill_to_phone=" + obj.bill_to_phone + ",bill_to_address_line1=" + obj.bill_to_address_line1 + ",bill_to_address_city=" + obj.bill_to_address_city + ",bill_to_address_state=" + obj.bill_to_address_state + ",bill_to_address_country=" + obj.bill_to_address_country + ",bill_to_address_postal_code=" + obj.bill_to_address_postal_code);
                    obj.card_type = "001";
                    obj.card_number = "";
                    obj.card_expiry_date = "";

                    AddDepositOrders outobjectver = new AddDepositOrders();
                    outobjectver.Amount = amount;
                    outobjectver.CreatedBy = 10000;
                    outobjectver.CreatedByName = "Admin";
                    outobjectver.TransactionId = obj.transaction_uuid;
                    outobjectver.RefferalsId = obj.auth_trans_ref_no;
                    outobjectver.MemberId = Convert.ToInt64(MemberId);
                    outobjectver.Type = (int)AddDepositOrders.DepositType.Card;
                    outobjectver.Remarks = particulars;
                    outobjectver.Particulars = "Visa Card Payment Initiate";
                    outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                    outobjectver.IsActive = true;
                    outobjectver.IsApprovedByAdmin = true;
                    outobjectver.ServiceCharges = servicecharge;
                    outobjectver.Platform = PlatForm;
                    outobjectver.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;

                    outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NIS_Asia;
                    outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NIS_Asia.ToString();

                    Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");
                    if (Id > 0)
                    {
                        Common.AddLogs("Add DepositOrders By This TXNId:" + obj.transaction_uuid, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                    }
                }
                else
                {
                    ViewBag.Message = msg;
                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                }
            }
            else
            {
                ViewBag.Message = "User Not Found ";
                Common.AddLogs("User Not Found ", false, Convert.ToInt32(AddLog.LogType.CardPayment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

            }
            //string result = RepNCHL.ConnectRequest(MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), "NPR", MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString(), 1000, "TEST", "TEST");
            //result = result.Replace("/connectipswebgw/", RepNCHL.NCHLApiUrl + "connectipswebgw/");
            ////Common.createSha256("data to be signed");
            return View(obj);
        }


        public ActionResult Success(string TXNID)
        {
            ViewBag.Message = Common.ConnectIPSSuccess(TXNID);
            return View();
        }

        public ActionResult BankSuccess(string MerchantTxnId, string GatewayTxnId)
        {

            //WalletTransactions res_transaction = new WalletTransactions();
            //res_transaction.VendorTransactionId = GatewayTxnId;
            //if (res_transaction.GetRecord())
            //{
            //    Response.Write("Already Received");   
            //}
            //else
            //{
            ViewBag.Message = Common.BankStatusCheck(MerchantTxnId);
            //    Response.Write("Received");
            //}
            if (!ViewBag.Message.ToString().ToLower().Contains("recieved"))
            {
                return RedirectToAction("Cancel", new { msg = ViewBag.Message });
            }
            return View();
        }

        public ActionResult Failure(string TXNID)
        {
            AddDepositOrders outobject = new AddDepositOrders();
            GetDepositOrders inobject = new GetDepositOrders();
            inobject.TransactionId = TXNID;
            AddDepositOrders res = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
            if (res.Id > 0)
            {
                AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                inuser.MemberId = Convert.ToInt64(res.MemberId);
                AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                if (resuser.Id > 0)
                {
                    Req_ConnectPaymentValidation req = new Req_ConnectPaymentValidation();
                    req.merchantId = RepNCHL.MERCHANTID;
                    req.appId = RepNCHL.APPID;
                    req.referenceId = TXNID;
                    req.txnAmt = res.Amount.ToString();
                    req.token = Common.GenerateConnectIPSToken("MERCHANTID=" + req.merchantId + ",APPID=" + req.appId + ",REFERENCEID=" + req.referenceId + ",TXNAMT=" + req.txnAmt);
                    string response = RepNCHL.PostMethod("connectipswebws/api/creditor/validatetxn", JsonConvert.SerializeObject(req));
                    if (!string.IsNullOrEmpty(response))
                    {
                        Res_ConnectPaymentValidation res1 = JsonConvert.DeserializeObject<Res_ConnectPaymentValidation>(response);
                        if (!string.IsNullOrEmpty(res1.referenceId))
                        {
                            if (res1.status == "SUCCESS")
                            {
                                outobject.Status = (int)AddDepositOrders.DepositStatus.Success;
                            }
                            else if (res1.status == "FAILED")
                            {
                                outobject.Status = (int)AddDepositOrders.DepositStatus.Failed;
                            }
                            else if (res1.status == "CANCELLED")
                            {
                                outobject.Status = (int)AddDepositOrders.DepositStatus.Cancelled;
                            }
                            else
                            {
                                outobject.Status = (int)AddDepositOrders.DepositStatus.Pending;
                            }
                            outobject.Particulars = res1.statusDesc;
                            outobject.ResponseCode = res1.status;
                            outobject.JsonResponse = response;
                            if (RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders"))
                            {
                                string Title = "Connect IPS";
                                string Message = "Hello " + resuser.FirstName + "! Connect IPS Transaction Failed By Transaction ID :" + TXNID;
                                Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_connectips, res.MemberId, Title, Message);
                                Common.AddLogs("Transaction Failed By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                ViewBag.Message = "Transaction Failed";
                            }
                            else
                            {
                                Common.AddLogs("Transaction Not Updated By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                ViewBag.Message = "Transaction Not Updated";
                            }
                        }
                        else
                        {
                            Common.AddLogs("Something went wrong try again later By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                            ViewBag.Message = "Something went wrong try again later";
                        }
                    }
                    else
                    {
                        Common.AddLogs("Response Not Found By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                        ViewBag.Message = "Response Not Found";
                    }
                }
                else
                {
                    Common.AddLogs("User Not Found By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                    ViewBag.Message = "User Not Found";
                }
            }
            else
            {
                ViewBag.Message = "Transaction not found";

            }

            return View();
        }

        public ActionResult Cancel(string msg)
        {

            ViewBag.Message = msg;
            return View();
        }

        public ActionResult IPSLinkBank(string MemberId, string ContactNumber, string code = "", string PlatForm = "Web")
        {
            AddRegisterLinkedBankNCHL obj = new AddRegisterLinkedBankNCHL();
            try
            {
                Common.AddLogs($"IPSLinkBank: Enter", false, (int)AddLog.LogType.DBLogs);
                if (string.IsNullOrEmpty(MemberId))
                {
                    ViewBag.Message = "MemberId Not Found";
                    Common.AddLogs($"IPSLinkBank: {ViewBag.Message}", false, (int)AddLog.LogType.DBLogs);
                    return View(obj);
                }
                else if (string.IsNullOrEmpty(ContactNumber))
                {
                    ViewBag.Message = "ContactNumber Not Found";
                    Common.AddLogs($"IPSLinkBank: {ViewBag.Message}", false, (int)AddLog.LogType.DBLogs);
                    return View(obj);
                }
                else
                {
                    ApiSetting objApiSettings = new ApiSetting();
                    using (var db = new MyPayEntities())
                    {
                        objApiSettings = db.ApiSettings.FirstOrDefault();

                        if ((objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.ALL) && (objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.NCHL))
                        {
                            return RedirectToAction($"CIPSRegisterMandateFailed", new { Message = Common.TemporaryServiceUnavailable });
                        }
                    }
                    AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                    GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                    inuser.MemberId = Convert.ToInt64(MemberId);
                    inuser.ContactNumber = Convert.ToString(ContactNumber);
                    inuser.CheckDelete = 0;
                    AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                    if (resuser.Id > 0)
                    {
                        if (resuser.IsKYCApproved != (int)AddUser.kyc.Verified || resuser.IsEmailVerified == false || resuser.Email.Trim() == "")
                        {
                            return RedirectToAction($"CIPSRegisterMandateFailed", new { Message = "Your Email and KYC must be verified to continue" });
                        }
                        else
                        {

                            string MobileNumber = resuser.ContactNumber;
                            string EmailID = resuser.Email;
                            obj.participantId = RepNCHL.participantId_LinkBank;
                            obj.identifier = Common.GenerateReferenceUniqueID();
                            obj.userIdentifier = MemberId;
                            obj.mobileNo = MobileNumber;
                            obj.email = EmailID;
                            obj.amount = "200000.00";
                            obj.debitType = "V";
                            obj.frequency = "7";
                            obj.mandateStartDate = System.DateTime.UtcNow.ToString("yyyy-MM-dd");
                            obj.mandateExpiryDate = System.DateTime.UtcNow.AddDays(178).ToString("yyyy-MM-dd");
                            obj.autoRenewal = true;
                            ViewBag.Message = "Success";

                            string TokenString = $"{obj.participantId},{obj.identifier},{obj.userIdentifier},{obj.mobileNo},{obj.email},{obj.amount},{obj.debitType},{obj.frequency},{obj.mandateStartDate},{obj.mandateExpiryDate}";
                            string SignatureText = Common.GenerateConnectIPSToken_LinkBank(TokenString);
                            obj.token = SignatureText;
                            obj.authToken = RepNCHL.gettoken_LinkBank();
                        }
                    }
                    else
                    {
                        Common.AddLogs("IPSLinkBank: MemberId Not Found", false, Convert.ToInt32(AddLog.LogType.DBLogs), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                        ViewBag.Message = "Member Not Found";
                    }
                }

            }
            catch (Exception ex)
            {
                Common.AddLogs($"IPSLinkBank: {ex.ToString()}", false, Convert.ToInt32(AddLog.LogType.DBLogs));
            }
            Common.AddLogs($"IPSLinkBank Payload: {JsonConvert.SerializeObject(obj)}", false, Convert.ToInt32(AddLog.LogType.DBLogs));
            return View(obj);
        }

        public ActionResult CIPSRegisterMandate(string participantId, string identifier, string userIdentifier)
        {
            AddRegisterLinkedBankNCHL obj = new AddRegisterLinkedBankNCHL();

            if (string.IsNullOrEmpty(userIdentifier))
            {
                ViewBag.Message = "userIdentifier Not Found";

                return View(obj);
            }
            else if (string.IsNullOrEmpty(participantId))
            {
                ViewBag.Message = "participantId Not Found";

                return View(obj);
            }
            else if (string.IsNullOrEmpty(identifier))
            {
                ViewBag.Message = "identifier Not Found";

                return View(obj);
            }
            else
            {
                Common.AddLogs($"CIPSRegisterMandate:: participantId:{participantId} identifier:{identifier} userIdentifier:{userIdentifier}", false, (int)AddLog.LogType.DBLogs);
                ViewBag.Message = "Success";
            }
            return View(obj);
        }
        public ActionResult CIPSRegisterMandateFailed(string Message = "FAILED")
        {
            ViewBag.Message = Message;
            return View();
        }

    }
}
