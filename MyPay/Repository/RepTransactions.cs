using log4net;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Interop;

namespace MyPay.Repository
{
    public static class RepTransactions
    {
        private static ILog log = LogManager.GetLogger(typeof(RepTransactions));
        public static string CheckRecipientDetail(Int64 memberid, string recipientphone, string platform, string devicecode, bool ismobile)
        {
            try
            {
                if (memberid == 0)
                {
                    return "Please enter memberid.";
                }
                else if (string.IsNullOrEmpty(recipientphone))
                {
                    return "Please enter Recipient's contact number.";
                }
                else
                {
                    AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                    inobject.MemberId = memberid;
                    AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                    if (res != null && res.MemberId != 0)
                    {
                        if (!string.IsNullOrEmpty(res.Pin) && res.Pin != "0")
                        {
                            AddUserLoginWithPin outobject_rec = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobject_rec = new GetUserLoginWithPin();
                            inobject_rec.ContactNumber = recipientphone;
                            AddUserLoginWithPin res_rec = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject_rec, outobject_rec);
                            if (res_rec != null && res_rec.Id != 0)
                            {
                                //if (string.IsNullOrEmpty(res_rec.FirstName) || res_rec.IsPhoneVerified == false ||   res_rec.IsKYCApproved != (int)AddUser.kyc.Verified || res_rec.Pin == 0 || string.IsNullOrEmpty(res_rec.Password))
                                //{
                                //    return "Please ask recipient to complete his/her profile.";
                                //}
                                //else
                                if (res.MemberId == res_rec.MemberId)
                                {
                                    return "Receiver account is same as sender account. Please enter different contact no.";
                                }
                                else
                                {
                                    return "success";
                                }
                            }
                            else
                            {
                                return "This number is not registered on MyPay";
                            }
                        }
                        else
                        {
                            return "Please set your pin first.";
                        }
                    }
                    else
                    {
                        return "User not found with this memberid.";
                    }
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
                return e.Message;
                throw;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string TransferByPhone(string CouponCode, ref string TransactionUniqueID, ref string SenderTransactionUniqueID, AddUserLoginWithPin res, string CustomerId, Int64 memberid, string recipientphone, string amount,
            string pin, string remarks, string referenceno, string platform, string devicecode, bool ismobile, string UserInput,
            string authenticationToken, ref AddVendor_API_Requests objVendor_API_Requests, Int32 VendorApiType = 0, Int64 MerchantMemberId = 0, string MerchantID = "", string MerchantOrganizationName = "")
        {
            string result = "";
            remarks = Common.HTMLToPlainText(remarks);
            
            if (VendorApiType == 0)
            {
                VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone;
            }
            try
            {
                if (memberid == 0)
                {
                    result = "Please enter memberid.";
                    return result;
                }
                else if (string.IsNullOrEmpty(recipientphone))
                {
                    result = "Please enter Recipient's contact number.";
                    return result;
                }
                else if (!string.IsNullOrEmpty(recipientphone))
                {
                    Int64 Num;
                    bool isNum = Int64.TryParse(recipientphone, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid phone number.";
                        return result;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (string.IsNullOrEmpty(amount))
                    {
                        result = "Please enter Amount.";
                        return result;
                    }
                    else if (!string.IsNullOrEmpty(amount))
                    {
                        decimal Num;
                        bool isNum = decimal.TryParse(amount, out Num);
                        if (!isNum)
                        {
                            result = "Please enter valid Amount.";
                            return result;
                        }
                        else if (Convert.ToDecimal(amount) <= 0)
                        {
                            result = "Please enter valid Amount.";
                            return result;
                        }
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        if (string.IsNullOrEmpty(pin) && VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                        {
                            result = "Please enter pin.";
                            return result;
                        }
                        else if (!string.IsNullOrEmpty(pin) && VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                        {
                            Int64 Num;
                            bool isNum = Int64.TryParse(pin, out Num);
                            if (!isNum)
                            {
                                result = "Please enter valid pin.";
                                return result;
                            }
                        }

                        if (string.IsNullOrEmpty(result))
                        {
                            if (string.IsNullOrEmpty(remarks))
                            {
                                result = "Please enter remarks.";
                                return result;
                            }
                            else if (string.IsNullOrEmpty(referenceno) || referenceno == "0")
                            {
                                result = "Please enter reference no.";
                                return result;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        string msg = string.Empty;
                        AddRequestFund resReqID = new AddRequestFund();
                        if (!string.IsNullOrEmpty(CustomerId) && VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                        {
                            AddRequestFund outobjectReqID = new AddRequestFund();
                            GetRequestFund inobjectReqID = new GetRequestFund();
                            inobjectReqID.MemberId = Convert.ToInt64(memberid);
                            if (Convert.ToInt64(CustomerId) != 0)
                            {
                                inobjectReqID.Id = Convert.ToInt64(CustomerId);
                            }
                            else
                            {
                                inobjectReqID.Id = -1;
                            }
                            resReqID = RepCRUD<GetRequestFund, AddRequestFund>.GetRecord(Common.StoreProcedures.sp_RequestFund_Get, inobjectReqID, outobjectReqID);
                            if (resReqID != null && resReqID.Id != 0 && resReqID.RequestStatus != (int)AddRequestFund.RequestStatuses.Pending)
                            {
                                result = "This request is already updated.";
                            }
                        }
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                        else
                        {
                            if (res != null && res.Id != 0)
                            {
                                string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").ToString();
                                if ((!string.IsNullOrEmpty(res.Pin) && res.Pin != "0") || VendorApiType != (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                                {
                                    //if (res.IsKYCApproved == (int)AddUser.kyc.Verified)
                                    //{
                                    if ((Common.DecryptString(res.Pin) == pin) || VendorApiType != (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                                    {
                                        //if (res.IsKYCApproved != (int)AddUser.kyc.Verified)
                                        //{
                                        //    result = Common.GetKycMessage(res, Convert.ToDecimal(amount));
                                        //    return result;
                                        //}

                                        AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(memberid.ToString(), amount, ((int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone).ToString());
                                        if ((res.TotalAmount >= Convert.ToDecimal(objOut.NetAmount)) || VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load || VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Remittance_Transactions)
                                        {
                                            // Check Sender Limit
                                            if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                                            {
                                                msg = Common.CheckTransactionLimit((int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone, (int)AddTransactionLimit.TransactionTransferTypeEnum.Wallet_To_Wallet_Transfer, res.MemberId, objOut.NetAmount, (int)WalletTransactions.Signs.Debit).ToLower();
                                            }
                                            else
                                            {
                                                msg = "success";
                                            }
                                            if (msg != "success")
                                            {
                                                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                                msg = textInfo.ToTitleCase(msg);
                                                return msg;
                                            }
                                            else
                                            {
                                                decimal WalletBalance = Convert.ToDecimal(res.TotalAmount);
                                                AddUserLoginWithPin outobject_rec = new AddUserLoginWithPin();
                                                GetUserLoginWithPin inobject_rec = new GetUserLoginWithPin();
                                                inobject_rec.ContactNumber = recipientphone;
                                                AddUserLoginWithPin res_rec = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject_rec, outobject_rec);
                                                if (res_rec != null && res_rec.Id != 0)
                                                {
                                                    if (res.MemberId == res_rec.MemberId)
                                                    {
                                                        msg = "Receiver account is same as sender account. Please enter different contact no.";
                                                    }
                                                    else
                                                    {
                                                        // Check Receiver Limit
                                                        if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                                                        {
                                                            msg = Common.CheckTransactionLimit((int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone, (int)AddTransactionLimit.TransactionTransferTypeEnum.Wallet_To_Wallet_Transfer, res_rec.MemberId, objOut.NetAmount, (int)WalletTransactions.Signs.Credit).ToLower();
                                                        }
                                                        else
                                                        {
                                                            msg = "success";
                                                        }
                                                        if (msg != "success" && res_rec.RoleId == (int)AddUser.UserRoles.User)
                                                        {
                                                            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                                            msg = textInfo.ToTitleCase(msg);
                                                            return msg;
                                                        }
                                                        else
                                                        {
                                                            if (res_rec.RoleId == (int)AddUser.UserRoles.Merchant)
                                                            {
                                                                VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_QR_Payments;
                                                            }
                                                            WalletBalance = Convert.ToDecimal(Convert.ToDecimal(res.TotalAmount) - Convert.ToDecimal(objOut.NetAmount));
                                                            decimal RecipientWalletBalance = Convert.ToDecimal(Convert.ToDecimal(res_rec.TotalAmount) + Convert.ToDecimal(amount));

                                                            TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                                            WalletTransactions res_transaction_sender = new WalletTransactions();
                                                            res_transaction_sender.MemberId = res.MemberId;
                                                            res_transaction_sender.ContactNumber = res.ContactNumber;
                                                            res_transaction_sender.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                            res_transaction_sender.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                                            res_transaction_sender.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                            res_transaction_sender.RecieverId = res_rec.MemberId;
                                                            res_transaction_sender.RecieverName = res_rec.FirstName + " " + res_rec.MiddleName + " " + res_rec.LastName;
                                                            res_transaction_sender.RecieverContactNumber = res_rec.ContactNumber;
                                                            res_transaction_sender.UpdateBy = res.MemberId;
                                                            res_transaction_sender.UpdateByName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                            res_transaction_sender.Amount = Convert.ToDecimal(amount);
                                                            res_transaction_sender.CurrentBalance = WalletBalance;
                                                            if (string.IsNullOrEmpty(authenticationToken))
                                                            {
                                                                res_transaction_sender.CreatedBy = Common.CreatedBy;
                                                                res_transaction_sender.CreatedByName = Common.CreatedByName;
                                                            }
                                                            else
                                                            {
                                                                res_transaction_sender.CreatedBy = Common.GetCreatedById(authenticationToken);
                                                                res_transaction_sender.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                                            }
                                                            res_transaction_sender.TransactionUniqueId = TransactionUniqueID;
                                                            res_transaction_sender.Remarks = remarks;
                                                            res_transaction_sender.Description = remarks;
                                                            res_transaction_sender.Status = (int)WalletTransactions.Statuses.Success;
                                                            res_transaction_sender.Reference = referenceno;
                                                            res_transaction_sender.IsApprovedByAdmin = true;
                                                            res_transaction_sender.IsActive = true;
                                                            res_transaction_sender.Platform = platform;
                                                            res_transaction_sender.DeviceCode = devicecode;
                                                            res_transaction_sender.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
                                                            res_transaction_sender.Type = VendorApiType;
                                                            res_transaction_sender.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                            res_transaction_sender.CustomerID = res_rec.ContactNumber;
                                                            res_transaction_sender.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                            res_transaction_sender.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                                            res_transaction_sender.ServiceCharge = objOut.ServiceCharge;
                                                            res_transaction_sender.CashBack = objOut.CashbackAmount;
                                                            if ((VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Load) ||
                                                                (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load) ||
                                                                (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Remittance_Transactions) ||
                                                                (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_QR_Payments))
                                                            {
                                                                res_transaction_sender.MerchantMemberId = MerchantMemberId;
                                                                res_transaction_sender.MerchantId = MerchantID;
                                                                res_transaction_sender.MerchantOrganization = MerchantOrganizationName;
                                                            }
                                                            bool IsTransactionSaved = false;
                                                            if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load || VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Remittance_Transactions)
                                                            {
                                                                IsTransactionSaved = true;
                                                            }
                                                            else
                                                            {
                                                                IsTransactionSaved = res_transaction_sender.Add();
                                                            }

                                                            if (IsTransactionSaved)
                                                            {
                                                                SenderTransactionUniqueID = res_transaction_sender.TransactionUniqueId;
                                                                string TitleSender = "Funds Transferred";
                                                                string MessageSender = $"Amount of Rs.{Convert.ToDecimal(amount).ToString("0.00")} is successfully transferred to {res_rec.FirstName} ( Ph: {res_rec.ContactNumber})";
                                                                Models.Common.Common.SendNotification(authenticationToken, VendorApiType, memberid, TitleSender, MessageSender);

                                                                Models.Common.Common.AddLogs("Fund Transfer by phone is successfully Debited.", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(res.MemberId), res.Email, true, platform, devicecode, (int)AddLog.LogActivityEnum.Fund_Transfer);


                                                                TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                                                WalletTransactions res_transaction_receiver = new WalletTransactions();
                                                                res_transaction_receiver.MemberId = res_rec.MemberId;
                                                                res_transaction_receiver.ContactNumber = res_rec.ContactNumber;
                                                                res_transaction_receiver.MemberName = res_rec.FirstName + " " + res_rec.MiddleName + " " + res_rec.LastName;
                                                                res_transaction_receiver.TransferType = (int)WalletTransactions.TransferTypes.Reciever;
                                                                res_transaction_receiver.Amount = Convert.ToDecimal(amount);
                                                                res_transaction_receiver.CurrentBalance = RecipientWalletBalance;
                                                                res_transaction_receiver.CreatedBy = res_rec.MemberId;
                                                                res_transaction_receiver.CreatedByName = res_rec.FirstName + " " + res_rec.MiddleName + " " + res_rec.LastName;
                                                                res_transaction_receiver.TransactionUniqueId = TransactionUniqueID;
                                                                res_transaction_receiver.Remarks = remarks;
                                                                res_transaction_receiver.Description = remarks;
                                                                res_transaction_receiver.Status = (int)WalletTransactions.Statuses.Success;
                                                                res_transaction_receiver.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                                                res_transaction_receiver.RecieverId = res.MemberId;
                                                                res_transaction_receiver.RecieverName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                                if (!string.IsNullOrEmpty(MerchantOrganizationName))
                                                                {
                                                                    res_transaction_receiver.RecieverName = MerchantOrganizationName;
                                                                }
                                                                res_transaction_receiver.RecieverContactNumber = res.ContactNumber;
                                                                res_transaction_receiver.UpdateBy = res.MemberId;
                                                                res_transaction_receiver.UpdateByName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                                res_transaction_receiver.Reference = referenceno;
                                                                res_transaction_receiver.IsApprovedByAdmin = true;
                                                                res_transaction_receiver.IsActive = true;
                                                                res_transaction_receiver.Platform = platform;
                                                                res_transaction_receiver.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                                                res_transaction_receiver.Type = VendorApiType;
                                                                res_transaction_receiver.CustomerID = res.ContactNumber;
                                                                if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load)
                                                                {
                                                                    res_transaction_receiver.CustomerID = MerchantOrganizationName;
                                                                }
                                                                res_transaction_receiver.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                                res_transaction_receiver.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                                                if ((VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Load) ||
                                                                    (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load) ||
                                                                    (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_QR_Payments))
                                                                {
                                                                    res_transaction_receiver.MerchantMemberId = MerchantMemberId;
                                                                    res_transaction_receiver.MerchantId = MerchantID;
                                                                    res_transaction_receiver.MerchantOrganization = MerchantOrganizationName;
                                                                }
                                                                IsTransactionSaved = res_transaction_receiver.Add();
                                                                if (IsTransactionSaved)
                                                                {
                                                                    string TitleReceiver = "Received Funds Successfully";
                                                                    string MessageReceiver = res.FirstName + $"( Ph: {res.ContactNumber}) has successfully transferred the Rs.{Convert.ToDecimal(amount).ToString("0.00")}";
                                                                    if ((VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Load) ||
                                                                    (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load) ||
                                                                    (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_QR_Payments))
                                                                    {
                                                                        MessageReceiver = MerchantOrganizationName + $" has successfully transferred the Rs.{Convert.ToDecimal(amount).ToString("0.00")}";
                                                                    }
                                                                    Models.Common.Common.SendNotification(authenticationToken, VendorApiType, res_rec.MemberId, TitleReceiver, MessageReceiver);

                                                                    Models.Common.Common.AddLogs("Fund Transfer by phone is successfully Credited.", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(res_rec.MemberId), res.Email, true, platform, devicecode, (int)AddLog.LogActivityEnum.Fund_Transfer);

                                                                    #region SendEmailConfirmation
                                                                    string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/P2PTransaction.html"));
                                                                    string body = mystring;
                                                                    if (!string.IsNullOrEmpty(res.FirstName))
                                                                    {
                                                                        body = body.Replace("##Sender##", res.FirstName);
                                                                    }
                                                                    else
                                                                    {
                                                                        body = body.Replace("##Sender##", res.ContactNumber);
                                                                    }
                                                                    body = body.Replace("##Amount##", (res_transaction_receiver.Amount).ToString("0.00"));
                                                                    if (res_transaction_receiver.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                                                                    {
                                                                        body = body.Replace("##TransactionId##", res_transaction_receiver.Reference);
                                                                    }
                                                                    else
                                                                    {
                                                                        body = body.Replace("##TransactionId##", TransactionUniqueID);
                                                                    }
                                                                    body = body.Replace("##ConsumerTransactionId##", res_transaction_receiver.Reference);
                                                                    body = body.Replace("##Date##", Common.fnGetdatetimeFromInput(res_transaction_receiver.CreatedDate));
                                                                    body = body.Replace("##Type##", "Wallet to Wallet Transfer");
                                                                    body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
                                                                    body = body.Replace("##Cashback##", objOut.CashbackAmount.ToString("0.00"));
                                                                    body = body.Replace("##ServiceCharge##", objOut.ServiceCharge.ToString("0.00"));
                                                                    body = body.Replace("##Purpose##", res_transaction_receiver.Description);

                                                                    string Subject = MyPay.Models.Common.Common.WebsiteName + " - Fund Transfer Successfull";
                                                                    string bodyrecipirnt = body;
                                                                    if (!string.IsNullOrEmpty(res.Email))
                                                                    {

                                                                        body = body.Replace("##UserName##", res.FirstName);
                                                                        if (VendorApiType != (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load)
                                                                        {
                                                                            Common.SendAsyncMail(res.Email, Subject, body);
                                                                        }
                                                                    }
                                                                    if (!string.IsNullOrEmpty(res_rec.Email))
                                                                    {
                                                                        bodyrecipirnt = bodyrecipirnt.Replace("##UserName##", res_rec.FirstName);
                                                                        Common.SendAsyncMail(res_rec.Email, Subject, bodyrecipirnt);
                                                                    }
                                                                    #endregion

                                                                    if (resReqID != null && resReqID.Id != 0 && resReqID.RequestStatus == (int)AddRequestFund.RequestStatuses.Pending &&
                                                                        VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone)
                                                                    {
                                                                        resReqID.RequestStatus = (int)AddRequestFund.RequestStatuses.Accepted;
                                                                        resReqID.IpAddress = Common.GetUserIP();
                                                                        resReqID.CreatedBy = Common.GetCreatedById(authenticationToken);
                                                                        resReqID.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                                                        bool UpdateStatus = RepCRUD<AddRequestFund, GetRequestFund>.Update(resReqID, "requestfund");

                                                                    }
                                                                    msg = "success";
                                                                }
                                                                else
                                                                {
                                                                    msg = "Receiver transaction not saved";
                                                                }

                                                            }
                                                            else
                                                            {
                                                                msg = "Transaction Failed";
                                                            }
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    msg = "This number is not registered on MyPay";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = Common.InsufficientBalance;
                                        }
                                    }
                                    else
                                    {
                                        msg = Common.Invalidpin;
                                        Common.AddLogs($"Transaction {msg} : {VendorApiTypeName} For Rs. {amount} ", false, (int)AddLog.LogType.User, res.MemberId, res.FirstName, true);
                                    }
                                    //}
                                    //else
                                    //{
                                    //    result = "Kyc not completed.";
                                    //    return result;
                                    //}
                                }
                                else
                                {
                                    msg = "Please set your pin first.";
                                }
                            }
                            else
                            {
                                msg = "User not found with this memberid.";
                            }
                            string MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            VendorApi_CommonHelper.RequestedToken = string.Empty;
                            
                        }
                        return msg;
                    }
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
                result = e.Message;
                return result;
                throw;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
            return result;
        }


        public static string WalletUpdateFromAdmin(Int64 memberid, string amount, string Referenceno, string TransactionSign, ref string UserMessage, string AdminRemarks, string TxnId)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- WalletUpdateFromAdmin started" + Environment.NewLine);
            // Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- WalletUpdateFromAdmin started" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

            string result = "";
            Int32 VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.WalletUpdate_By_Admin;
            if (TransactionSign == "3")
            {
                VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Hold_By_Admin;
                TransactionSign = Convert.ToString((int)WalletTransactions.Signs.Debit);
            }
            if (TransactionSign == "4")
            {
                if (TxnId == "")
                {
                    result = "Please enter Transaction id.";
                    return result;
                }
                VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Amount_Release_From_Admin;
                TransactionSign = Convert.ToString((int)WalletTransactions.Signs.Credit);
            }
            try
            {
                if (memberid == 0)
                {
                    result = "Please enter Memberid.";
                    return result;
                }
                else if (string.IsNullOrEmpty(amount))
                {
                    result = "Please enter Amount.";
                    return result;
                }
                else if (string.IsNullOrEmpty(TransactionSign) || (TransactionSign != "1" && TransactionSign != "2"))
                {
                    result = "Please select Transaction Type.";
                    return result;
                }
                else if (!string.IsNullOrEmpty(amount))
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(amount, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid Amount.";
                        return result;
                    }
                    else if (Convert.ToDecimal(amount) <= 0)
                    {
                        result = "Please enter valid Amount.";
                        return result;
                    }
                }
                else if (string.IsNullOrEmpty(AdminRemarks))
                {
                    result = "Please enter Remarks.";
                    return result;
                }

                if (string.IsNullOrEmpty(result))
                {
                    log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- WalletUpdateFromAdmin all validation passed" + Environment.NewLine);
                    //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- WalletUpdateFromAdmin all validation passed", false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                    string platform = "web";
                    string authenticationToken = string.Empty;
                    string devicecode = HttpContext.Current.Request.Browser.Type;
                    string TransactionUniqueID = string.Empty;
                    AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                    inobject.MemberId = memberid;
                    AddUserLoginWithPin res_rec = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                    if (res_rec != null && res_rec.Id != 0)
                    {
                        log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- sp_Users_Get executed" + Environment.NewLine);
                        //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- sp_Users_Get executed -- TransactionSign: {TransactionSign}" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                        Int32 Sign = Convert.ToInt32(TransactionSign);
                        string remarks = "Wallet update from admin";
                        decimal RecipientWalletBalance = Convert.ToDecimal(res_rec.TotalAmount);
                        if (Sign == (int)WalletTransactions.Signs.Debit)
                        {
                            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- RecipientWalletBalance" + Environment.NewLine);
                            //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- RecipientWalletBalance" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                            RecipientWalletBalance = Convert.ToDecimal(Convert.ToDecimal(res_rec.TotalAmount) - Convert.ToDecimal(amount));
                            if (RecipientWalletBalance < 0)
                            {
                                result = "Insufficient Wallet Amount";
                                return result;
                            }
                            else
                            {
                                remarks = WalletTransactions.Signs.Debit + " wallet from admin ";
                            }
                            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- remarks updated with RecipientWalletBalance {RecipientWalletBalance}" + Environment.NewLine);
                            //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- remarks updated with RecipientWalletBalance {RecipientWalletBalance}" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                        }
                        else if (Sign == (int)WalletTransactions.Signs.Credit)
                        {
                            RecipientWalletBalance = Convert.ToDecimal(Convert.ToDecimal(res_rec.TotalAmount) + Convert.ToDecimal(amount));
                            remarks = WalletTransactions.Signs.Credit + " wallet from admin ";
                        }
                        if (!string.IsNullOrEmpty(AdminRemarks))
                        {
                            remarks = AdminRemarks;
                        }
                        TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                        WalletTransactions res_transaction = new WalletTransactions();
                        res_transaction.MemberId = res_rec.MemberId;
                        res_transaction.ContactNumber = res_rec.ContactNumber;
                        res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Reciever;
                        res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                        res_transaction.MemberName = res_rec.FirstName + " " + res_rec.MiddleName + " " + res_rec.LastName;
                        res_transaction.RecieverId = res_rec.MemberId;
                        res_transaction.RecieverName = res_rec.FirstName + " " + res_rec.MiddleName + " " + res_rec.LastName;
                        res_transaction.RecieverContactNumber = res_rec.ContactNumber;
                        res_transaction.UpdateBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                        res_transaction.UpdateByName = HttpContext.Current.Session["AdminUserName"].ToString();
                        res_transaction.Amount = Convert.ToDecimal(amount);
                        res_transaction.CurrentBalance = RecipientWalletBalance;
                        res_transaction.CreatedBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                        res_transaction.CreatedByName = HttpContext.Current.Session["AdminUserName"].ToString();
                        res_transaction.TransactionUniqueId = TransactionUniqueID;
                        res_transaction.Remarks = AdminRemarks;
                        res_transaction.Description = remarks;
                        res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                        res_transaction.Reference = Referenceno;
                        res_transaction.IsApprovedByAdmin = true;
                        res_transaction.IsActive = true;
                        res_transaction.Platform = platform;
                        res_transaction.DeviceCode = devicecode;
                        res_transaction.Sign = Sign;
                        res_transaction.Type = VendorApiType;
                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                        res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                        if (TxnId != "")
                        {
                            res_transaction.ParentTransactionId = TxnId;
                        }
                        bool IsTransactionSaved = res_transaction.Add();
                        log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- Transaction IsTransactionSaved returned" + Environment.NewLine);
                        //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- Transaction IsTransactionSaved returned" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                        if (IsTransactionSaved)
                        {
                            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- Transaction saved" + Environment.NewLine);
                            //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- Transaction saved" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                            string Title = "Wallet Updated";
                            string Message = remarks + " for Rs " + Convert.ToDecimal(amount).ToString("0.00");

                            Models.Common.Common.SendNotification(authenticationToken, VendorApiType, memberid, Title, Message);
                            Models.Common.Common.AddLogs(Message, false, Convert.ToInt32(AddLog.LogType.Wallet), Convert.ToInt64(res_rec.MemberId), res_rec.Email, false, platform, devicecode, (int)AddLog.LogActivityEnum.Admin_Wallet_Update);
                            UserMessage = $"Rs.{amount} {@Enum.GetName(typeof(WalletTransactions.Signs), Sign).ToString()}  User Wallet Successfully.";
                            result = "success";
                        }
                        else
                        {
                            result = "Transaction failed for wallet updated from admin for MemberID " + memberid.ToString();
                        }
                    }
                    else
                    {
                        result = "User not found for MemberID " + memberid.ToString();
                    }

                }
                return result;
            }
            catch (DbEntityValidationException e)
            {
                result = e.Message;
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result = result + " " + ve.PropertyName + " " + ve.ErrorMessage;
                    }
                }
                Common.AddLogs($"{System.DateTime.Now.ToString()} inside DbEntityValidationException -- " + result, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Common.AddLogs($"{System.DateTime.Now.ToString()} inside Exception -- " + result, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                return result;
            }
        }


        public static string BankTransferByLinkedAccount(AddUserLoginWithPin resuser, string VendorType, string mpin, string authenticationToken, int type, string BankId, string Description, decimal Amount, Int64 MemberId, string Platform, string DeviceCode, ref string ReturnTransactionId, ref AddVendor_API_Requests objVendor_API_Requests, ref string API_NAME)
        {
            if (string.IsNullOrEmpty(BankId))
            {
                return "Please Enter BankId";
            }
            else if (type == 0)
            {
                return "Please Enter Type";
            }
            else if (string.IsNullOrEmpty(Description))
            {
                return "Please Enter Purpose";
            }
            else if (Amount == 0)
            {
                return "Please Enter Amount";
            }
            else if (MemberId == 0)
            {
                return "Please Enter MemberId";
            }
            AddCalculateServiceChargeAndCashback objOut = new AddCalculateServiceChargeAndCashback();

            if (type == 1)
            {
                objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(MemberId.ToString(), Amount.ToString(), ((int)VendorApi_CommonHelper.KhaltiAPIName.Deposit_By_Linked_Bank).ToString());
            }
            else if (!string.IsNullOrEmpty(VendorType))
            {
                objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(MemberId.ToString(), Amount.ToString(), VendorType);
            }
            Common.AddLogs("Linked Bank Service Charge Calculate", false, 1, 10000, "Admin", false, Platform, DeviceCode);
            string msg = Common.CheckTransactionLimit((type == 1 ? (int)VendorApi_CommonHelper.KhaltiAPIName.Deposit_By_Linked_Bank : (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank), (int)AddTransactionLimit.TransactionTransferTypeEnum.Pay_And_Deposit_From_Linked_Bank, resuser.MemberId, objOut.NetAmount).ToLower();
            if (msg != "success")
            {
                Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                return msg;
            }
            AddUserBankDetail outuobject = new AddUserBankDetail();
            GetUserBankDetail inuobject = new GetUserBankDetail();
            inuobject.Id = Convert.ToInt64(BankId);
            inuobject.MemberId = MemberId;
            inuobject.CheckDelete = 0;
            AddUserBankDetail resobject = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(nameof(Common.StoreProcedures.sp_UserBankDetail_Get), inuobject, outuobject);

            if (resobject.Id > 0)
            {
                if (string.IsNullOrEmpty(resobject.BankCode))
                {
                    return "Please Select Bank";
                }
                else if (string.IsNullOrEmpty(resobject.AccountNumber))
                {
                    return ("Account Number Not Exist");
                }
                else if (string.IsNullOrEmpty(resobject.Name))
                {
                    return ("Account Name Not Exist");
                }
                else
                {
                    ApiSetting objApiSettings = new ApiSetting();
                    using (var db = new MyPayEntities())
                    {
                        objApiSettings = db.ApiSettings.FirstOrDefault();

                        if (resobject.BankTransferType == (int)AddUserBankDetail.UserBankType.NPS)
                        {
                            if ((objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.ALL) && (objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.NPS))
                            {
                                return Common.TemporaryServiceUnavailable;
                            }
                        }
                        else if (resobject.BankTransferType == (int)AddUserBankDetail.UserBankType.NCHL)
                        {
                            if ((objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.ALL) && (objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.NCHL))
                            {
                                return Common.TemporaryServiceUnavailable;
                            }
                        }
                    }
                    decimal finalamount = Amount + objOut.ServiceCharge;
                    string TransactionId = new CommonHelpers().GenerateUniqueId_Limit20();
                    string ReferenceId = new CommonHelpers().GenerateUniqueId();
                    AddDepositOrders outobjectver = new AddDepositOrders();
                    outobjectver.Amount = Convert.ToDecimal(finalamount);
                    outobjectver.CreatedBy = Common.GetCreatedById(authenticationToken);
                    outobjectver.CreatedByName = Common.GetCreatedByName(authenticationToken);
                    outobjectver.TransactionId = TransactionId;
                    outobjectver.RefferalsId = TransactionId;
                    outobjectver.MemberId = Convert.ToInt64(resuser.MemberId);
                    if (type == 1)
                    {
                        outobjectver.Type = (int)AddDepositOrders.DepositType.Linked_Bank_Deposit;
                    }
                    else
                    {
                        outobjectver.Type = (int)AddDepositOrders.DepositType.Linked_Bank_Transaction;
                    }

                    outobjectver.Remarks = Description;
                    outobjectver.Particulars = "Pending Transaction";
                    outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                    outobjectver.IsActive = true;
                    outobjectver.IsApprovedByAdmin = true;
                    outobjectver.ServiceCharges = objOut.ServiceCharge;
                    outobjectver.Platform = Platform;
                    outobjectver.DeviceCode = DeviceCode;
                    if (resobject.BankTransferType == (int)AddUserBankDetail.UserBankType.NPS)
                    {

                        outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NPS;
                        outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NPS.ToString();

                    }
                    else
                    {

                        outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;
                        outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NCHL.ToString();

                    }
                    Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");
                    if (Id > 0)
                    {
                        Common.AddLogs("Linked Bank Generate Request", false, 1, 10000, "Admin", false, Platform, DeviceCode);
                        if (resobject.BankTransferType == (int)AddUserBankDetail.UserBankType.NPS)
                        {
                            API_NAME = "api/LoadWalletWithToken";

                            GetLoadWalletWithToken bank = RepNps.LoadWalletWithToken(API_NAME, TransactionId, resobject.BranchName, resobject.BankCode, finalamount.ToString(), Description, ref objVendor_API_Requests);
                            Common.AddLogs(JsonConvert.SerializeObject(bank), false, 1, 10000, "Admin", false, Platform, DeviceCode);
                            if (!string.IsNullOrEmpty(bank.code))
                            {
                                AddDepositOrders outobject = new AddDepositOrders();
                                GetDepositOrders inobject = new GetDepositOrders();
                                inobject.Id = Id;
                                AddDepositOrders resDeposit = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
                                if (resDeposit.Id > 0)
                                {
                                    if (bank.errors.Count > 0)
                                    {
                                        if (bank.data != null && !string.IsNullOrEmpty(bank.data.GatewayTransactionId))
                                        {
                                            resDeposit.RefferalsId = bank.data.GatewayTransactionId;
                                        }
                                        resDeposit.Particulars = bank.errors[0].error_message;
                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                        resDeposit.ResponseCode = bank.errors[0].error_code;
                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

                                        Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);


                                        return resDeposit.Particulars;
                                    }
                                    if (bank.code == "0" && (bank.message.ToLower().Contains("success")) && bank.data != null && bank.data.GatewayTransactionId != null && bank.data.GatewayTransactionId != "")
                                    {
                                        decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) + (Convert.ToDecimal(Amount)));

                                        if (type == 1)
                                        {
                                            Common.AddLogs("Linked Bank Transaction Creation Start", false, 1, 10000, "Admin", false, Platform, DeviceCode);
                                            WalletTransactions res_transaction = new WalletTransactions();
                                            res_transaction.VendorTransactionId = bank.data.GatewayTransactionId;
                                            if (!res_transaction.GetRecordCheckExists())
                                            {
                                                res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                                                res_transaction.ContactNumber = resuser.ContactNumber;
                                                res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                                res_transaction.Amount = Convert.ToDecimal(Amount);
                                                res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
                                                res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                                res_transaction.CurrentBalance = WalletBalance;
                                                res_transaction.CreatedBy = Common.GetCreatedById(authenticationToken);
                                                res_transaction.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                                res_transaction.TransactionUniqueId = TransactionId;
                                                res_transaction.VendorTransactionId = bank.data.GatewayTransactionId;
                                                res_transaction.Reference = bank.data.MerchantTxnId;
                                                //res_transaction.BatchTransactionId = res.cipsBatchResponse.batchId;
                                                //res_transaction.TxnInstructionId = res.cipsTxnResponseList[0].instructionId;
                                                res_transaction.Remarks = "Wallet Credit Successfully";
                                                res_transaction.GatewayStatus = bank.message;
                                                res_transaction.ResponseCode = bank.code;
                                                res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Deposit_By_Linked_Bank;
                                                res_transaction.Description = Description;
                                                res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                                res_transaction.IsApprovedByAdmin = true;
                                                res_transaction.IsActive = true;
                                                res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                                res_transaction.RecieverName = Common.ConnectIPs_AccountNumber;
                                                res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                res_transaction.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
                                                res_transaction.RecieverBankCode = Common.ConnectIps_BankId;
                                                res_transaction.RecieverBranch = Common.ConnectIPs_BranchName;
                                                res_transaction.SenderAccountNo = resobject.AccountNumber;
                                                res_transaction.SenderBankCode = resobject.BankCode;
                                                res_transaction.SenderBranch = resobject.BranchId;
                                                res_transaction.SenderBankName = resobject.BankName;
                                                res_transaction.SenderBranchName = resobject.BranchId;
                                                res_transaction.ServiceCharge = objOut.ServiceCharge;
                                                res_transaction.NetAmount = Convert.ToDecimal(Amount) + objOut.ServiceCharge;
                                                res_transaction.Purpose = Description;
                                                res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NPS;
                                                res_transaction.Platform = Platform;
                                                res_transaction.DeviceCode = DeviceCode;
                                                res_transaction.RecieverBankName = Common.ConnectIPs_BankName;
                                                res_transaction.RecieverBranchName = Common.ConnectIPs_BranchName;
                                                res_transaction.Platform = Platform;
                                                res_transaction.DeviceCode = DeviceCode;

                                                resDeposit.RefferalsId = bank.data.GatewayTransactionId;
                                                resDeposit.Particulars = bank.message;
                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                                resDeposit.ResponseCode = bank.code;
                                                resDeposit.Status = (int)AddDepositOrders.DepositStatus.Success;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                                if (res_transaction.Add())
                                                {

                                                    //if (resuser.RefId != 0)
                                                    //{
                                                    //    Common.FirstTransactionCommisipon(resuser, resDeposit, res_transaction.TransactionUniqueId);
                                                    //}
                                                    ReturnTransactionId = res_transaction.TransactionUniqueId;
                                                    Common.AddLogs("Payment Successfully Deposit", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                                                    log.Info($"{System.DateTime.Now.ToString()} RepTransactions BankTransferByLinkedAccount complete" + Environment.NewLine);

                                                    return "Success";
                                                }
                                                else
                                                {

                                                    Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                    return "Something Went Wrong Payment Not Sent";
                                                }
                                            }
                                            else
                                            {
                                                Common.AddLogs(JsonConvert.SerializeObject(bank), true, 1, 10000, "Admin", false, Platform, DeviceCode);
                                                Common.AddLogs("Transaction Sent Already", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                return "Transaction Sent Already";
                                            }
                                        }
                                        else
                                        {
                                            AddBankTransactions bankoutuobject = new AddBankTransactions();
                                            GetBankTransactions bankinuobject = new GetBankTransactions();
                                            bankinuobject.VendorTransactionId = bank.data.GatewayTransactionId;
                                            AddBankTransactions res_BankTransactions = RepCRUD<GetBankTransactions, AddBankTransactions>.GetRecord(nameof(Common.StoreProcedures.sp_BankTransactions_Get), bankinuobject, bankoutuobject);
                                            if (res_BankTransactions.Id == 0)
                                            {

                                                res_BankTransactions.MemberId = resuser.MemberId;
                                                res_BankTransactions.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                                res_BankTransactions.Amount = Convert.ToDecimal(Amount) + objOut.ServiceCharge;
                                                res_BankTransactions.VendorTransactionId = bank.data.GatewayTransactionId;
                                                res_BankTransactions.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                                                res_BankTransactions.CurrentBalance = WalletBalance;
                                                res_BankTransactions.CreatedBy = Common.GetCreatedById(authenticationToken);
                                                res_BankTransactions.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                                res_BankTransactions.TransactionUniqueId = TransactionId;
                                                res_BankTransactions.Remarks = "Payment Successfully Received";
                                                res_BankTransactions.Purpose = Description;
                                                res_BankTransactions.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank;
                                                res_BankTransactions.Description = Description;
                                                res_BankTransactions.Status = (int)WalletTransactions.Statuses.Pending;
                                                res_BankTransactions.Reference = "";
                                                res_BankTransactions.IsApprovedByAdmin = true;
                                                //res_BankTransactions.BatchId = res.cipsBatchResponse.batchId;
                                                //res_BankTransactions.InstructionId = res.cipsTxnResponseList[0].instructionId;
                                                res_BankTransactions.CreditStatus = bank.message;
                                                res_BankTransactions.DebitStatus = bank.message;
                                                res_BankTransactions.GatewayStatus = bank.message;
                                                res_BankTransactions.IsActive = true;
                                                res_BankTransactions.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Debit);
                                                res_BankTransactions.ResponseCode = bank.code;
                                                res_BankTransactions.RecieverName = Common.ConnectIps_AccountName;
                                                res_BankTransactions.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
                                                res_BankTransactions.RecieverBankCode = Common.ConnectIps_BankId;
                                                res_BankTransactions.RecieverBranch = Common.ConnectIPs_BranchId;
                                                res_BankTransactions.RecieverBankName = Common.ConnectIPs_BankName;
                                                res_BankTransactions.RecieverBranchName = Common.ConnectIPs_BranchName;
                                                res_BankTransactions.SenderAccountNo = resobject.AccountNumber;
                                                res_BankTransactions.SenderBankCode = resobject.BankCode;
                                                res_BankTransactions.SenderBranch = resobject.BranchId;
                                                res_BankTransactions.SenderBankName = resobject.BankName;
                                                res_BankTransactions.SenderBranchName = resobject.BranchId;
                                                res_BankTransactions.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                res_BankTransactions.ServiceCharge = objOut.ServiceCharge;
                                                res_BankTransactions.NetAmount = Convert.ToDecimal(Amount) + objOut.ServiceCharge;
                                                res_BankTransactions.Sno = res_BankTransactions.GetBankTransactionSno();
                                                res_BankTransactions.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NPS;

                                                resDeposit.RefferalsId = bank.data.GatewayTransactionId;
                                                resDeposit.Particulars = bank.message;
                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                                resDeposit.ResponseCode = bank.code;
                                                resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

                                                Int64 BankDepositId = RepCRUD<AddBankTransactions, GetBankTransactions>.Insert(res_BankTransactions, "banktransactions");
                                                if (BankDepositId > 0)
                                                {

                                                    //if (resuser.RefId != 0)
                                                    //{
                                                    //    Common.FirstTransactionCommisipon(resuser, resDeposit, res_BankTransactions.TransactionUniqueId);
                                                    //}
                                                    ReturnTransactionId = res_BankTransactions.TransactionUniqueId;
                                                    Common.AddLogs("Payment Successfully Recieved", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                                                    log.Info($"{System.DateTime.Now.ToString()} RepTransactions BankTransferByLinkedAccount complete" + Environment.NewLine);

                                                    return "Success";
                                                }
                                                else
                                                {

                                                    Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                    return "Something Went Wrong Payment Not Sent";
                                                }
                                            }
                                            else
                                            {
                                                Common.AddLogs("Transaction Sent Already", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                return "Transaction Sent Already";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (bank.data != null && bank.data.GatewayTransactionId != null && bank.data.GatewayTransactionId != "")
                                        {
                                            resDeposit.RefferalsId = bank.data.GatewayTransactionId;
                                        }
                                        resDeposit.Particulars = bank.message;
                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                        resDeposit.ResponseCode = bank.code;
                                        if (bank.code == "1")
                                        {
                                            resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                        }
                                        else
                                        {
                                            resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                        }

                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                        Common.AddLogs(bank.message, false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                        return bank.message;
                                    }
                                }
                                else
                                {
                                    return "Request Not Found";
                                }

                            }
                            else
                            {
                                return "Data Not Found";
                            }
                        }
                        else
                        {
                            API_NAME = "tokenization/stagepayment";

                            GetLoadWalletWithTokenNCHL bank = RepNCHL.LoadWalletWithToken(API_NAME, resuser, ReferenceId, TransactionId, resobject.BranchName, resobject.BankCode, finalamount.ToString(), Description, ref objVendor_API_Requests);

                            Common.AddLogs(JsonConvert.SerializeObject(bank), false, 1, 10000, "Admin", false, Platform, DeviceCode);
                            if (!string.IsNullOrEmpty(bank.responseCode))
                            {
                                AddDepositOrders outobject = new AddDepositOrders();
                                GetDepositOrders inobject = new GetDepositOrders();
                                inobject.Id = Id;
                                AddDepositOrders resDeposit = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
                                if (resDeposit.Id > 0)
                                {
                                    if (bank != null && bank.responseErrors != null && bank.responseErrors.Count > 0)
                                    {
                                        if (string.IsNullOrEmpty(bank.instructionId))
                                        {
                                            resDeposit.RefferalsId = bank.instructionId;
                                        }
                                        resDeposit.Particulars = bank.responseErrors[0].ToString();
                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                        resDeposit.ResponseCode = bank.responseErrors[0].ToString();
                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

                                        Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                                        return resDeposit.Particulars;
                                    }
                                    if (bank.responseCode == "000" && (bank.debitStatus == "000" || bank.creditStatus == "000"))
                                    {
                                        decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) + (Convert.ToDecimal(Amount)));

                                        if (type == 1)
                                        {
                                            Common.AddLogs("Linked Bank Transaction Creation Start", false, 1, 10000, "Admin", false, Platform, DeviceCode);
                                            WalletTransactions res_transaction = new WalletTransactions();
                                            res_transaction.VendorTransactionId = bank.txnId.ToString();
                                            if (!res_transaction.GetRecordCheckExists())
                                            {
                                                res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                                                res_transaction.ContactNumber = resuser.ContactNumber;
                                                res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                                res_transaction.Amount = Convert.ToDecimal(Amount);
                                                res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
                                                res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                                res_transaction.CurrentBalance = WalletBalance;
                                                res_transaction.CreatedBy = Common.GetCreatedById(authenticationToken);
                                                res_transaction.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                                res_transaction.TransactionUniqueId = TransactionId;
                                                res_transaction.VendorTransactionId = bank.txnId.ToString();
                                                res_transaction.Reference = bank.refId;
                                                //res_transaction.BatchTransactionId = res.cipsBatchResponse.batchId;
                                                //res_transaction.TxnInstructionId = res.cipsTxnResponseList[0].instructionId;
                                                res_transaction.Remarks = "Wallet Credit Successfully";
                                                res_transaction.GatewayStatus = bank.responseMessage;
                                                res_transaction.ResponseCode = bank.responseCode;
                                                res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Deposit_By_Linked_Bank;
                                                res_transaction.Description = Description;
                                                res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                                res_transaction.IsApprovedByAdmin = true;
                                                res_transaction.IsActive = true;
                                                res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                                res_transaction.RecieverName = Common.ConnectIPs_AccountNumber;
                                                res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                res_transaction.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
                                                res_transaction.RecieverBankCode = Common.ConnectIps_BankId;
                                                res_transaction.RecieverBranch = Common.ConnectIPs_BranchName;
                                                res_transaction.SenderAccountNo = resobject.AccountNumber;
                                                res_transaction.SenderBankCode = resobject.BankCode;
                                                res_transaction.SenderBranch = resobject.BranchId;
                                                res_transaction.SenderBankName = resobject.BankName;
                                                res_transaction.SenderBranchName = resobject.BranchId;
                                                res_transaction.ServiceCharge = objOut.ServiceCharge;
                                                res_transaction.NetAmount = Convert.ToDecimal(Amount) + objOut.ServiceCharge;
                                                res_transaction.Purpose = Description;
                                                res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;
                                                res_transaction.Platform = Platform;
                                                res_transaction.DeviceCode = DeviceCode;
                                                res_transaction.RecieverBankName = Common.ConnectIPs_BankName;
                                                res_transaction.RecieverBranchName = Common.ConnectIPs_BranchName;

                                                resDeposit.RefferalsId = bank.instructionId;
                                                resDeposit.Particulars = bank.responseMessage;
                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                                resDeposit.ResponseCode = bank.responseCode;
                                                resDeposit.Status = (int)AddDepositOrders.DepositStatus.Success;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                                if (res_transaction.Add())
                                                {

                                                    //if (resuser.RefId != 0)
                                                    //{
                                                    //    Common.FirstTransactionCommisipon(resuser, resDeposit, res_transaction.TransactionUniqueId);
                                                    //}
                                                    ReturnTransactionId = res_transaction.TransactionUniqueId;
                                                    Common.AddLogs("Payment Successfully Deposit", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                                                    log.Info($"{System.DateTime.Now.ToString()} RepTransactions BankTransferByLinkedAccount complete" + Environment.NewLine);

                                                    return "Success";
                                                }
                                                else
                                                {

                                                    Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                    return "Something Went Wrong Payment Not Sent";
                                                }
                                            }
                                            else
                                            {
                                                Common.AddLogs(JsonConvert.SerializeObject(bank), true, 1, 10000, "Admin", false, Platform, DeviceCode);
                                                Common.AddLogs("Transaction Sent Already", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                return "Transaction Sent Already";
                                            }
                                        }
                                        else
                                        {
                                            AddBankTransactions bankoutuobject = new AddBankTransactions();
                                            GetBankTransactions bankinuobject = new GetBankTransactions();
                                            bankinuobject.VendorTransactionId = bank.txnId.ToString();
                                            AddBankTransactions res_BankTransactions = RepCRUD<GetBankTransactions, AddBankTransactions>.GetRecord(nameof(Common.StoreProcedures.sp_BankTransactions_Get), bankinuobject, bankoutuobject);
                                            if (res_BankTransactions.Id == 0)
                                            {

                                                res_BankTransactions.MemberId = resuser.MemberId;
                                                res_BankTransactions.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                                res_BankTransactions.Amount = Convert.ToDecimal(Amount) + objOut.ServiceCharge;
                                                res_BankTransactions.VendorTransactionId = bank.txnId.ToString();
                                                res_BankTransactions.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                                                res_BankTransactions.CurrentBalance = WalletBalance;
                                                res_BankTransactions.CreatedBy = Common.GetCreatedById(authenticationToken);
                                                res_BankTransactions.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                                res_BankTransactions.TransactionUniqueId = TransactionId;
                                                res_BankTransactions.Remarks = "Payment Successfully Received";
                                                res_BankTransactions.Purpose = Description;
                                                res_BankTransactions.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank;
                                                res_BankTransactions.Description = Description;
                                                res_BankTransactions.Status = (int)WalletTransactions.Statuses.Pending;
                                                res_BankTransactions.Reference = bank.instructionId;
                                                res_BankTransactions.IsApprovedByAdmin = true;
                                                //res_BankTransactions.BatchId = res.cipsBatchResponse.batchId;
                                                //res_BankTransactions.InstructionId = res.cipsTxnResponseList[0].instructionId;
                                                res_BankTransactions.CreditStatus = bank.creditStatus;
                                                res_BankTransactions.DebitStatus = bank.debitStatus;
                                                res_BankTransactions.GatewayStatus = bank.responseMessage;
                                                res_BankTransactions.IsActive = true;
                                                res_BankTransactions.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Debit);
                                                res_BankTransactions.ResponseCode = bank.responseCode;
                                                res_BankTransactions.RecieverName = Common.ConnectIps_AccountName;
                                                res_BankTransactions.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
                                                res_BankTransactions.RecieverBankCode = Common.ConnectIps_BankId;
                                                res_BankTransactions.RecieverBranch = Common.ConnectIPs_BranchId;
                                                res_BankTransactions.RecieverBankName = Common.ConnectIPs_BankName;
                                                res_BankTransactions.RecieverBranchName = Common.ConnectIPs_BranchName;
                                                res_BankTransactions.SenderAccountNo = resobject.AccountNumber;
                                                res_BankTransactions.SenderBankCode = resobject.BankCode;
                                                res_BankTransactions.SenderBranch = resobject.BranchId;
                                                res_BankTransactions.SenderBankName = resobject.BankName;
                                                res_BankTransactions.SenderBranchName = resobject.BranchId;
                                                res_BankTransactions.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                res_BankTransactions.ServiceCharge = objOut.ServiceCharge;
                                                res_BankTransactions.NetAmount = Convert.ToDecimal(Amount) + objOut.ServiceCharge;
                                                res_BankTransactions.Sno = res_BankTransactions.GetBankTransactionSno();
                                                res_BankTransactions.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;

                                                resDeposit.RefferalsId = bank.instructionId;
                                                resDeposit.Particulars = bank.responseMessage;
                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                                resDeposit.ResponseCode = bank.responseCode;
                                                resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

                                                Int64 BankDepositId = RepCRUD<AddBankTransactions, GetBankTransactions>.Insert(res_BankTransactions, "banktransactions");
                                                if (BankDepositId > 0)
                                                {

                                                    //if (resuser.RefId != 0)
                                                    //{
                                                    //    Common.FirstTransactionCommisipon(resuser, resDeposit, res_BankTransactions.TransactionUniqueId);
                                                    //}
                                                    ReturnTransactionId = res_BankTransactions.TransactionUniqueId;
                                                    Common.AddLogs("Payment Successfully Recieved", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                                                    log.Info($"{System.DateTime.Now.ToString()} RepTransactions BankTransferByLinkedAccount complete" + Environment.NewLine);

                                                    return "Success";
                                                }
                                                else
                                                {

                                                    Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                    return "Something Went Wrong Payment Not Sent";
                                                }
                                            }
                                            else
                                            {
                                                Common.AddLogs("Transaction Sent Already", false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);

                                                return "Transaction Sent Already";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(bank.instructionId))
                                        {
                                            resDeposit.RefferalsId = bank.instructionId;
                                        }
                                        resDeposit.Particulars = bank.responseMessage;
                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(bank);
                                        resDeposit.ResponseCode = bank.responseCode;
                                        if (bank.responseCode != "000" && bank.responseCode != "ENTR")
                                        {
                                            resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                        }
                                        else
                                        {
                                            resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                        }

                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                        Common.AddLogs(bank.responseMessage, false, Convert.ToInt32(AddLog.LogType.Linked_BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                                        string DebitCreditDescription = string.Empty;
                                        if (bank.debitDescription.ToUpper() != "SUCCESS" && !string.IsNullOrEmpty(bank.debitDescription))
                                        {
                                            DebitCreditDescription = bank.debitDescription;
                                        }
                                        else
                                        {
                                            DebitCreditDescription = bank.responseMessage;
                                        }
                                        return DebitCreditDescription;
                                    }
                                }
                                else
                                {
                                    return "Request Not Found";
                                }

                            }
                            else
                            {
                                return "Data Not Found";
                            }
                        }

                    }
                    else
                    {
                        return "Something Went Wrong Try Again Later!";
                    }
                }
            }
            else
            {
                return "Bank Not Found";
            }
        }


        public static string Request_TransferByPhone(Int64 MemberId, string recipientphone, string amount, string pin, string remarks, string referenceno, string platform, string devicecode, bool ismobile, string authenticationToken)
        {
            string result = "";
            try
            {
                if (MemberId == 0)
                {
                    result = "Please enter MemberId.";
                    return result;
                }
                else if (string.IsNullOrEmpty(recipientphone))
                {
                    result = "Please enter sender contact number.";
                    return result;
                }
                else if (!string.IsNullOrEmpty(recipientphone))
                {
                    Int64 Num;
                    bool isNum = Int64.TryParse(recipientphone, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid phone number.";
                        return result;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (string.IsNullOrEmpty(amount))
                    {
                        result = "Please enter Amount.";
                        return result;
                    }
                    else if (!string.IsNullOrEmpty(amount))
                    {
                        decimal Num;
                        bool isNum = decimal.TryParse(amount, out Num);
                        if (!isNum)
                        {
                            result = "Please enter valid Amount.";
                            return result;
                        }
                        else if (Convert.ToDecimal(amount) <= 0)
                        {
                            result = "Please enter valid Amount.";
                            return result;
                        }
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        if (string.IsNullOrEmpty(pin))
                        {
                            result = "Please enter pin.";
                            return result;
                        }
                        else if (!string.IsNullOrEmpty(pin))
                        {
                            Int64 Num;
                            bool isNum = Int64.TryParse(pin, out Num);
                            if (!isNum)
                            {
                                result = "Please enter valid pin.";
                                return result;
                            }
                        }

                        if (string.IsNullOrEmpty(result))
                        {
                            if (string.IsNullOrEmpty(remarks))
                            {
                                result = "Please enter remarks.";
                                return result;
                            }
                            else if (string.IsNullOrEmpty(referenceno) && referenceno == "0")
                            {
                                result = "Please enter reference no.";
                                return result;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                        GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                        inobject.MemberId = MemberId;
                        AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                        if (res != null && res.MemberId != 0)
                        {
                            if (!string.IsNullOrEmpty(res.Pin) && res.Pin != "0")
                            {
                                //if (res.IsKYCApproved == (int)AddUser.kyc.Verified)
                                //{
                                if (Common.DecryptString(res.Pin) == pin)
                                {
                                    int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone;
                                    decimal WalletBalance = Convert.ToDecimal(res.TotalAmount);
                                    AddUserLoginWithPin outobject_rec = new AddUserLoginWithPin();
                                    GetUserLoginWithPin inobject_rec = new GetUserLoginWithPin();
                                    inobject_rec.ContactNumber = recipientphone;
                                    AddUserLoginWithPin res_rec = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject_rec, outobject_rec);
                                    if (res_rec != null && res_rec.MemberId != 0)
                                    {

                                        //if (string.IsNullOrEmpty(res_rec.FirstName) || res_rec.IsPhoneVerified == false  || res_rec.IsKYCApproved != (int)AddUser.kyc.Verified || res_rec.Pin == 0 || string.IsNullOrEmpty(res_rec.Password))
                                        //{
                                        //    return "Please ask sender to complete his/her profile.";
                                        //}
                                        //else
                                        //{ 
                                        AddRequestFund outobject_req = new AddRequestFund();
                                        GetRequestFund inobject_req = new GetRequestFund();
                                        outobject_req.SenderMemberId = MemberId;
                                        outobject_req.MemberId = res_rec.MemberId;
                                        outobject_req.Amount = Convert.ToDecimal(amount);
                                        outobject_req.IpAddress = Common.GetUserIP();
                                        outobject_req.CreatedBy = Common.GetCreatedById(authenticationToken);
                                        outobject_req.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                        Int64 i = RepCRUD<AddRequestFund, GetRequestFund>.Insert(outobject_req, "requestfund");
                                        if (i > 0)
                                        {
                                            string Title = "Request for Fund transfer ";
                                            string Message = $"You got request from {res.FirstName}, {res.ContactNumber} of amount Rs. {Convert.ToDecimal(amount).ToString("0.00")} ";
                                            Models.Common.Common.SendNotification(authenticationToken, VendorApiType, res_rec.MemberId, Title, Message);

                                            Message = $"You have sent request to {res_rec.FirstName}, {res_rec.ContactNumber} of amount Rs. {Convert.ToDecimal(amount).ToString("0.00")} ";
                                            Models.Common.Common.SendNotification(authenticationToken, VendorApiType, res.MemberId, Title, Message);

                                            string LogMessage = $"Fund transfer by phone request submitted successfully from {res.FirstName}, {res.ContactNumber} to {res_rec.FirstName}, {res_rec.ContactNumber} of amount Rs. {Convert.ToDecimal(amount).ToString("0.00")}";
                                            Models.Common.Common.AddLogs(LogMessage, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, true, platform, devicecode);
                                            result = "success";
                                        }
                                        else
                                        {
                                            string LogMessage = $"Fund transfer by phone request failed from {res.FirstName}, {res.ContactNumber} to {res_rec.FirstName}, {res_rec.ContactNumber} of amount Rs. {Convert.ToDecimal(amount).ToString("0.00")}";
                                            Models.Common.Common.AddLogs(LogMessage, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), res.Email, true, platform, devicecode);
                                            result = "Request Failed";
                                            return result;
                                        }
                                        //}
                                    }
                                    else
                                    {
                                        result = "This number is not registered on MyPay";
                                        return result;
                                    }

                                }
                                else
                                {
                                    result = Common.Invalidpin;
                                    return result;
                                }
                                //}
                                //else
                                //{
                                //    result = "Kyc not completed.";
                                //    return result;
                                //}
                            }
                            else
                            {
                                result = "Please set your pin first.";
                                return result;
                            }
                        }
                        else
                        {
                            result = "User not found with this memberid.";
                            return result;
                        }
                    }
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
                result = e.Message;
                return result;
                throw;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
            return result;
        }

        public static string MerchantWalletUpdateFromAdmin(AddMerchant resMerchant, string amount, string Referenceno, string TransactionSign, ref string UserMessage, string AdminRemarks, string TxnId)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit --Merchant WalletUpdateFromAdmin started" + Environment.NewLine);

            string result = "";
            Int32 VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Account_CreditDebit;
            try
            {
                if (string.IsNullOrEmpty(amount))
                {
                    result = "Please enter Amount.";
                    return result;
                }
                else if (string.IsNullOrEmpty(TransactionSign) || (TransactionSign != "1" && TransactionSign != "2"))
                {
                    result = "Please select Transaction Type.";
                    return result;
                }
                else if (!string.IsNullOrEmpty(amount))
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(amount, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid Amount.";
                        return result;
                    }
                    else if (Convert.ToDecimal(amount) <= 0)
                    {
                        result = "Please enter valid Amount.";
                        return result;
                    }
                }
                else if (string.IsNullOrEmpty(AdminRemarks))
                {
                    result = "Please enter Remarks.";
                    return result;
                }

                if (string.IsNullOrEmpty(result))
                {
                    log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit --Merchant WalletUpdateFromAdmin all validation passed" + Environment.NewLine);

                    string platform = "web";
                    string authenticationToken = string.Empty;
                    string devicecode = HttpContext.Current.Request.Browser.Type;
                    string TransactionUniqueID = string.Empty;

                    Int32 Sign = Convert.ToInt32(TransactionSign);
                    string remarks = "Wallet update from admin";
                    decimal RecipientWalletBalance = Convert.ToDecimal(resMerchant.MerchantTotalAmount);
                    if (Sign == (int)WalletTransactions.Signs.Debit)
                    {
                        log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- Merchant WalletBalance" + Environment.NewLine);

                        RecipientWalletBalance = Convert.ToDecimal(Convert.ToDecimal(resMerchant.MerchantTotalAmount) - Convert.ToDecimal(amount));
                        if (RecipientWalletBalance < 0)
                        {
                            result = "Insufficient Wallet Amount";
                            return result;
                        }
                        else
                        {
                            remarks = WalletTransactions.Signs.Debit + " merchant wallet from admin ";
                        }
                        log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- remarks updated with Merchant WalletBalance {RecipientWalletBalance}" + Environment.NewLine);

                    }
                    else if (Sign == (int)WalletTransactions.Signs.Credit)
                    {
                        RecipientWalletBalance = Convert.ToDecimal(Convert.ToDecimal(resMerchant.MerchantTotalAmount) + Convert.ToDecimal(amount));
                        remarks = WalletTransactions.Signs.Credit + " merchant wallet from admin ";
                    }
                    resMerchant.MerchantTotalAmount = RecipientWalletBalance;
                    bool IsUpdated = RepCRUD<AddMerchant, GetMerchant>.Update(resMerchant, "Merchant");
                    if (!string.IsNullOrEmpty(AdminRemarks))
                    {
                        remarks = AdminRemarks;
                    }
                    TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                    AddMerchantOrders outobject_req = new AddMerchantOrders();
                    outobject_req.MerchantId = resMerchant.MerchantUniqueId;
                    outobject_req.MerchantName = resMerchant.FirstName + " " + resMerchant.LastName;
                    outobject_req.OrganizationName = resMerchant.OrganizationName;
                    outobject_req.OrderId = TransactionUniqueID;
                    outobject_req.TransactionId = TransactionUniqueID;
                    outobject_req.MemberId = resMerchant.UserMemberId;
                    outobject_req.MemberName = resMerchant.FirstName + " " + resMerchant.LastName;
                    outobject_req.Status = 1;
                    outobject_req.Remarks = AdminRemarks;
                    outobject_req.Amount = Convert.ToDecimal(amount);
                    outobject_req.NetAmount = Convert.ToDecimal(amount);
                    outobject_req.IsActive = true;
                    outobject_req.IsApprovedByAdmin = true;
                    outobject_req.IsDeleted = false;
                    //outobject_req.CreatedBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                    //outobject_req.CreatedByName = HttpContext.Current.Session["AdminUserName"].ToString();

                    outobject_req.CreatedBy = Common.CreatedBy;
                    outobject_req.CreatedByName = Common.CreatedByName;
                    outobject_req.Type = VendorApiType;
                    outobject_req.Platform = "Web";
                    outobject_req.DeviceCode = "";
                    // outobject_req.OrderToken = resMerchant.ContactNo;
                    outobject_req.UpdatedBy = Convert.ToInt64(10000);
                    outobject_req.UpdatedByName = "user100005";
                    //outobject_req.UpdatedBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                    //outobject_req.UpdatedByName = HttpContext.Current.Session["AdminUserName"].ToString();
                    // outobject_req.OrderOTP = resMerchant.ContactNo;
                    outobject_req.Sign = Sign;
                    outobject_req.CurrentBalance = RecipientWalletBalance;

                    GetMerchantOrders inobject_req = new GetMerchantOrders();

                    Int64 id = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Insert(outobject_req, "MerchantOrders");


                    log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit Merchant -- Transaction IsTransactionSaved returned" + Environment.NewLine);

                    if (id > 0)
                    {
                        log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit Merchant -- Transaction saved" + Environment.NewLine);


                        string Message = remarks + " for Rs " + Convert.ToDecimal(amount).ToString("0.00");

                        Models.Common.Common.AddLogs(Message, false, Convert.ToInt32(AddLog.LogType.Merchant), Convert.ToInt64(resMerchant.MerchantMemberId), resMerchant.EmailID, false, platform, devicecode, (int)AddLog.LogActivityEnum.Admin_Wallet_Update);
                        UserMessage = $"Rs.{amount} {@Enum.GetName(typeof(WalletTransactions.Signs), Sign).ToString()}  User Wallet Successfully.";
                        result = "success";
                    }
                    else
                    {
                        result = "Transaction failed for merchant wallet updated from admin for MemberID " + resMerchant.MerchantUniqueId;
                    }
                }



                return result;
            }
            catch (DbEntityValidationException e)
            {
                result = e.Message;
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result = result + " " + ve.PropertyName + " " + ve.ErrorMessage;
                    }
                }
                Common.AddLogs($"{System.DateTime.Now.ToString()} inside DbEntityValidationException -- " + result, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Common.AddLogs($"{System.DateTime.Now.ToString()} inside Exception -- " + result, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                return result;
            }
        }
        public static string Get_TransferByPhone(AddUserLoginWithPin res, string Take, string Skip, Int64 MemberId, string platform, string devicecode, bool ismobile, ref List<AddRequestFund> objRef)
        {
            string result = "";
            try
            {

                if (string.IsNullOrEmpty(Take))
                {
                    Take = "10";
                }
                if (string.IsNullOrEmpty(Skip))
                {
                    Skip = "0";
                }

                if (MemberId == 0)
                {
                    result = "Please enter MemberId.";
                    return result;
                }
                else if (string.IsNullOrEmpty(platform))
                {
                    result = "Please enter platform.";
                    return result;
                }
                else if (string.IsNullOrEmpty(devicecode))
                {
                    result = "Please enter devicecode.";
                    return result;
                }
                else
                {
                    //AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                    //GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                    //inobject.MemberId = MemberId;
                    //AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                    if (res != null && res.MemberId != 0)
                    {
                        AddRequestFund outobjectRequestFund = new AddRequestFund();
                        GetRequestFund inobjectRequestFund = new GetRequestFund();
                        inobjectRequestFund.MemberId = MemberId;
                        inobjectRequestFund.Type = 1;
                        inobjectRequestFund.Take = Convert.ToInt32(Take);
                        inobjectRequestFund.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
                        List<AddRequestFund> resRequestFund = RepCRUD<GetRequestFund, AddRequestFund>.GetRecordList(Common.StoreProcedures.sp_RequestFund_Get, inobjectRequestFund, outobjectRequestFund);
                        if (resRequestFund != null && resRequestFund.Count != 0)
                        {
                            objRef = resRequestFund;
                            result = "success";
                            return result;
                        }
                        else
                        {
                            result = "No Request Found.";
                            return result;
                        }
                    }
                    else
                    {
                        result = "User not found with this MemberId.";
                        return result;
                    }
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
                result = e.Message;
                return result;
                throw;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
        }

        public static string TransferByPhoneRejectRequest(string MemberId, string CustomerId, string platform, string devicecode, bool ismobile, string authenticationToken)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
                {
                    result = "Please enter MemberId.";
                    return result;
                }
                else if (!string.IsNullOrEmpty(MemberId))
                {
                    Int64 Num;
                    bool isNum = Int64.TryParse(MemberId, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid MemberId.";
                        return result;
                    }
                }
                if (string.IsNullOrEmpty(CustomerId) || CustomerId == "0")
                {
                    result = "Please enter UniqueCustomerId.";
                    return result;
                }
                else if (!string.IsNullOrEmpty(CustomerId))
                {
                    Int64 Num;
                    bool isNum = Int64.TryParse(CustomerId, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid UniqueCustomerId.";
                        return result;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    AddRequestFund outobject = new AddRequestFund();
                    GetRequestFund inobject = new GetRequestFund();
                    inobject.Id = Convert.ToInt64(CustomerId);
                    AddRequestFund res = RepCRUD<GetRequestFund, AddRequestFund>.GetRecord(Common.StoreProcedures.sp_RequestFund_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {

                        res.RequestStatus = (int)AddRequestFund.RequestStatuses.Rejected;
                        res.IpAddress = Common.GetUserIP();
                        res.CreatedBy = Common.GetCreatedById(authenticationToken);
                        res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                        bool UpdateStatus = RepCRUD<AddRequestFund, GetRequestFund>.Update(res, "requestfund");
                        if (UpdateStatus)
                        {
                            Models.Common.Common.AddLogs("Fund transfer by phone request rejected successfully", false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", true, platform, devicecode);
                            result = "success";
                        }
                        else
                        {
                            result = "Request Failed";
                            return result;
                        }
                    }
                    else
                    {
                        result = "Request not found with this CustomerId.";
                        return result;
                    }
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
                result = e.Message;
                return result;
                throw;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
            return result;
        }

        public static string MPCoinsUpdateFromAdmin(Int64 memberid, string amount, string Referenceno, string TransactionSign, ref string UserMessage, string AdminRemarks)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- MPCoinsUpdateFromAdmin started" + Environment.NewLine);
            // Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- WalletUpdateFromAdmin started" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
            Int32 VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.MpCoinsUpdate_By_Admin;
            string result = "";
            if (TransactionSign == "3")
            {
                TransactionSign = Convert.ToString((int)WalletTransactions.Signs.Debit);
            }
            if (TransactionSign == "4")
            {
                TransactionSign = Convert.ToString((int)WalletTransactions.Signs.Credit);
            }
            try
            {
                if (memberid == 0)
                {
                    result = "Please enter Memberid.";
                    return result;
                }
                else if (string.IsNullOrEmpty(amount))
                {
                    result = "Please enter Amount.";
                    return result;
                }
                else if (string.IsNullOrEmpty(TransactionSign) || (TransactionSign != "1" && TransactionSign != "2"))
                {
                    result = "Please select Transaction Type.";
                    return result;
                }
                else if (!string.IsNullOrEmpty(amount))
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(amount, out Num);
                    if (!isNum)
                    {
                        result = "Please enter valid Amount.";
                        return result;
                    }
                    else if (Convert.ToDecimal(amount) <= 0)
                    {
                        result = "Please enter valid Amount.";
                        return result;
                    }
                }
                else if (string.IsNullOrEmpty(AdminRemarks))
                {
                    result = "Please enter Remarks.";
                    return result;
                }

                if (string.IsNullOrEmpty(result))
                {
                    log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- MPCoinsUpdateFromAdmin all validation passed" + Environment.NewLine);
                    //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- WalletUpdateFromAdmin all validation passed", false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                    string platform = "web";
                    string authenticationToken = string.Empty;
                    string devicecode = HttpContext.Current.Request.Browser.Type;
                    string TransactionUniqueID = string.Empty;
                    AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                    inobject.MemberId = memberid;
                    AddUserLoginWithPin res_rec = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                    if (res_rec != null && res_rec.Id != 0)
                    {
                        log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- sp_Users_Get executed" + Environment.NewLine);
                        //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- sp_Users_Get executed -- TransactionSign: {TransactionSign}" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                        Int32 Sign = Convert.ToInt32(TransactionSign);
                        string remarks = "MP Coins update from admin";
                        decimal RecipientMPCoinsBalance = Convert.ToDecimal(res_rec.TotalRewardPoints);
                        if (Sign == (int)WalletTransactions.Signs.Debit)
                        {
                            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- RecipientMPCoinsBalance" + Environment.NewLine);
                            //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- RecipientWalletBalance" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                            RecipientMPCoinsBalance = Convert.ToDecimal(Convert.ToDecimal(res_rec.TotalRewardPoints) - Convert.ToDecimal(amount));
                            if (RecipientMPCoinsBalance < 0)
                            {
                                result = "Insufficient MP Coins";
                                return result;
                            }
                            else
                            {
                                remarks = WalletTransactions.Signs.Debit + " MP Coins from admin ";
                            }
                            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- remarks updated with RecipientMPCoinsBalance {RecipientMPCoinsBalance}" + Environment.NewLine);
                            //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- remarks updated with RecipientWalletBalance {RecipientWalletBalance}" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                        }
                        else if (Sign == (int)WalletTransactions.Signs.Credit)
                        {
                            RecipientMPCoinsBalance = Convert.ToDecimal(Convert.ToDecimal(res_rec.TotalRewardPoints) + Convert.ToDecimal(amount));
                            remarks = WalletTransactions.Signs.Credit + " MP Coins from admin ";
                        }
                        if (!string.IsNullOrEmpty(AdminRemarks))
                        {
                            remarks = AdminRemarks;
                        }
                        TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                        RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                        res_rewardpoint.MemberId = res_rec.MemberId;
                        res_rewardpoint.MemberName = res_rec.FirstName + " " + res_rec.MiddleName + " " + res_rec.LastName;
                        res_rewardpoint.Amount = Convert.ToDecimal(amount);
                        res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                        res_rewardpoint.ParentTransactionId = String.Empty;
                        res_rewardpoint.CurrentBalance = Convert.ToDecimal(RecipientMPCoinsBalance);
                        res_rewardpoint.CreatedBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                        res_rewardpoint.CreatedByName = HttpContext.Current.Session["AdminUserName"].ToString();
                        res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                        res_rewardpoint.Remarks = remarks;
                        res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.MPCoinsUpdate_By_Admin;
                        res_rewardpoint.Description = AdminRemarks;
                        res_rewardpoint.Status = 1;
                        res_rewardpoint.Reference = Referenceno;
                        res_rewardpoint.IsApprovedByAdmin = true;
                        res_rewardpoint.IsActive = true;
                        res_rewardpoint.Sign = Sign;
                        bool IsRewardPointSaved = res_rewardpoint.Add();
                        log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- RewardPointTransactions IsRewardPointSaved returned" + Environment.NewLine);
                        //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- Transaction IsTransactionSaved returned" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                        if (IsRewardPointSaved)
                        {
                            log.Info($"{System.DateTime.Now.ToString()} inside CreditDebit -- RewardPointTransactions saved" + Environment.NewLine);
                            //Common.AddLogs($"{System.DateTime.Now.ToString()} inside CreditDebit -- Transaction saved" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);

                            string Title = "MyPay Coins  Updated";
                            string Message = remarks + " for pts. " + Convert.ToDecimal(amount).ToString("0.00");

                            Models.Common.Common.SendNotification(authenticationToken, VendorApiType, memberid, Title, Message);
                            Models.Common.Common.AddLogs(Message, false, Convert.ToInt32(AddLog.LogType.RewardPoints), Convert.ToInt64(res_rec.MemberId), res_rec.Email, false, platform, devicecode, (int)AddLog.LogActivityEnum.Admin_RewardPoints_Update);
                            UserMessage = $"Pts.{amount} {@Enum.GetName(typeof(RewardPointTransactions.Signs), Sign).ToString()}  User MP Coins Successfully.";
                            result = "success";
                        }
                        else
                        {
                            result = "Reward Point Transaction failed for MP Coins updated from admin for MemberID " + memberid.ToString();
                        }
                    }
                    else
                    {
                        result = "User not found for MemberID " + memberid.ToString();
                    }
                }
                return result;
            }
            catch (DbEntityValidationException e)
            {
                result = e.Message;
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        result = result + " " + ve.PropertyName + " " + ve.ErrorMessage;
                    }
                }
                Common.AddLogs($"{System.DateTime.Now.ToString()} inside DbEntityValidationException -- " + result, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Common.AddLogs($"{System.DateTime.Now.ToString()} inside Exception -- " + result, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                return result;
            }
        }

        public static string GetApiTransactions(string searchtext, string datefiltertype, string DateFrom, string DateTo, string MemberId, string Take, string Skip, string Type, ref List<AddTransaction> list)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Take))
            {
                Take = "0";
            }
            if (string.IsNullOrEmpty(Skip))
            {
                Skip = "0";
            }
            if (string.IsNullOrEmpty(Type))
            {
                Type = "0";
            }
            msg = ValidateUserInputs_GetTransactions(MemberId, Take, Skip, Type);
            if (string.IsNullOrEmpty(msg))
            {
                //AddUser outobject = new AddUser();
                //GetUser inobject = new GetUser();
                //inobject.MemberId = Convert.ToInt64(MemberId);
                //AddUser resGetRecord = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject, outobject);
                //if (resGetRecord.Id == 0)
                //{
                //    msg = Common.CommonMessage.MemberId_Not_Found;
                //}
                //else
                {
                    AddTransaction outobjecttrans = new AddTransaction();
                    GetTransaction inobjectTrans = new GetTransaction();
                    inobjectTrans.Take = Convert.ToInt32(Take);
                    inobjectTrans.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
                    inobjectTrans.MemberId = Convert.ToInt64(MemberId);
                    inobjectTrans.Type = Convert.ToInt32(Type);
                    inobjectTrans.StartDate = DateFrom;
                    inobjectTrans.EndDate = DateTo;
                    inobjectTrans.CheckDelete = 0;
                    inobjectTrans.CheckActive = 1;
                    if (datefiltertype == "3")
                    {
                        inobjectTrans.ThreeMonth = "yes";
                    }
                    else if (datefiltertype == "6")
                    {
                        inobjectTrans.SixMonth = "yes";
                    }
                    else if (datefiltertype == "12")
                    {
                        inobjectTrans.Year = "yes";
                    }

                    if (!string.IsNullOrEmpty(searchtext))
                    {
                        //Int32 Num;
                        //CommonHelpers commonHelpers = new CommonHelpers();
                        //string Result = commonHelpers.GetScalarValueWithValue($"SELECT TOP 1 ProviderTypeId FROM ProviderLogosList with(nolock) WHERE IsActive=1 AND ProviderTypeId != 0 AND ( ServiceAPIName LIKE '%{searchtext}%' OR  ProviderServiceName LIKE '%{searchtext}%' )");
                        //bool isNum = Int32.TryParse(Result, out Num);
                        //if (isNum)
                        //{
                        //    inobjectTrans.Type = Convert.ToInt32(Result);
                        //}
                        //else
                        //{
                        //    inobjectTrans.SearchText = searchtext;
                        //}
                    }
                    //*********************************************************************
                    //********************* FOR PENDING TRANSACTIONS *********************
                    //*********************************************************************
                    list = RepCRUD<GetTransaction, AddTransaction>.GetRecordList(Common.StoreProcedures.sp_WalletTransactions_Get, inobjectTrans, outobjecttrans);
                    if (list.Count > 0)
                    {
                        if (Common.ApplicationEnvironment.IsProduction)
                        {
                            list.ForEach(z => z.WalletImage = (!string.IsNullOrEmpty(z.WalletImage) ? (Common.LiveSiteUrl + "/Images/MerchantImages/" + z.WalletImage) : ""));
                        }
                        else
                        {
                            list.ForEach(z => z.WalletImage = (!string.IsNullOrEmpty(z.WalletImage) ? (Common.TestSiteUrl + "/Images/MerchantImages/" + z.WalletImage) : ""));
                        }
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            list = list.Where(c => c.ProviderName.Contains(searchtext) || c.CustomerID.Contains(searchtext) || c.RecieverName.Contains(searchtext) || c.RecieverContactNumber.Contains(searchtext)).ToList();
                        }
                        //for (int i = 0; i < list.Count; i++)
                        //{
                        //    if(list[i].Type == "49" && string.IsNullOrEmpty( list[i].SenderAccountNo) )
                        //    {
                        //        list[i].SenderAccountNo = "XXX-XXX-XXXX";
                        //    }
                        //}
                        msg = Common.CommonMessage.success;
                    }
                    else
                    {
                        list.Clear();
                        msg = Common.CommonMessage.Data_Not_Found;
                    }
                }
            }
            return msg;
        }

        public static string GetApiTransactionsDetails(string MemberId, string Take, string Skip, string TransactionId, ref AddTransaction list)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
            {
                msg = "Please enter MemberId.";
            }
            else if (string.IsNullOrEmpty(TransactionId) || TransactionId == "0")
            {
                msg = "Please enter TransactionId.";
            }
            if (string.IsNullOrEmpty(Take))
            {
                Take = "0";
            }
            if (string.IsNullOrEmpty(Skip))
            {
                Skip = "0";
            }
            if (string.IsNullOrEmpty(msg))
            {
                AddTransaction outobjecttrans = new AddTransaction();
                GetTransaction inobjectTrans = new GetTransaction();
                inobjectTrans.Take = Convert.ToInt32(Take);
                inobjectTrans.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
                inobjectTrans.MemberId = Convert.ToInt64(MemberId);
                inobjectTrans.TransactionUniqueId = TransactionId;
                inobjectTrans.CheckDelete = 0;
                inobjectTrans.CheckActive = 1;
                list = RepCRUD<GetTransaction, AddTransaction>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Get, inobjectTrans, outobjecttrans);
                if (list != null)
                {
                    msg = Common.CommonMessage.success;
                }
                else
                {
                    msg = Common.CommonMessage.Data_Not_Found;
                }
            }
            return msg;
        }

        public static string ValidateUserInputs_GetTransactions(string MemberId, string Take, string Skip, string Type)
        {

            // *********************************************************************************//
            // ****** Validate API User Input Before Sending Further for Processing  ********** //
            // ********************************************************************************//
            string msg = string.Empty;
            if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
            {
                msg = "Please enter MemberId.";
            }
            else if (!string.IsNullOrEmpty(MemberId))
            {
                Int64 Num;
                bool isNum = Int64.TryParse(MemberId, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid MemberId.";
                }
            }
            if (string.IsNullOrEmpty(msg))
            {
                if (string.IsNullOrEmpty(Type))
                {
                    msg = "Please enter Type.";
                }
                else
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(Type, out Num);
                    if (!isNum)
                    {
                        msg = "Please enter valid Type.";
                    }
                }
            }
            return msg;
        }

        public static string GetRecentApiTransactions(string MemberId, string Type, ref List<AddRecentPayments> list)
        {
            string msg = string.Empty;

            msg = ValidateUserInputs_GetTransaction(MemberId, Type);
            if (string.IsNullOrEmpty(msg))
            {

                GetRecentTransaction objRecentTransaction = new GetRecentTransaction();
                objRecentTransaction.Type = Convert.ToInt32(Type);
                objRecentTransaction.MemberId = Convert.ToInt64(MemberId);
                objRecentTransaction.CheckDelete = 0;
                objRecentTransaction.CheckActive = 1;
                objRecentTransaction.RoleId = (int)AddUser.UserRoles.User;
                objRecentTransaction.GetRecord();
                DataTable dt = objRecentTransaction.GetRecord();
                if (dt.Rows.Count > 0)
                {
                    msg = "success";
                }
                List<AddRecentPayments> AddTransactionsList = new List<AddRecentPayments>();
                foreach (DataRow row in dt.Rows)
                {
                    if (!AddTransactionsList.Exists(c => c.TransactionAmount == row["TransactionAmount"].ToString() && c.CustomerID == row["CustomerID"].ToString()))
                    {
                        AddRecentPayments addRecentPayments = new AddRecentPayments();
                        addRecentPayments.Amount = row["Amount"].ToString();
                        addRecentPayments.TransactionAmount = row["TransactionAmount"].ToString();
                        addRecentPayments.IsFavourite = Convert.ToBoolean(row["IsFavourite"].ToString());
                        addRecentPayments.CustomerID = row["CustomerID"].ToString();
                        addRecentPayments.Type = Convert.ToInt32(row["Type"].ToString());
                        addRecentPayments.TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["Type"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").Replace("FONEPAY", " ").ToString();
                        addRecentPayments.ContactNumber = row["CustomerID"].ToString();
                        addRecentPayments.CreatedDate = Convert.ToDateTime(row["createdDate"].ToString()); //row["createdDate"].ToString();
                        AddTransactionsList.Add(addRecentPayments);
                    }
                }
                list = AddTransactionsList;
            }
            return msg;

        }

        public static string SaveRecentApiTransactions(Int64 MemberId, Int32 Type, string TransactionId)
        {
            string msg = string.Empty;
            if ((new CommonHelpers()).SaveRecentTransaction(MemberId, Type, TransactionId))
            {
                msg = "success";
            }
            else
            {
                msg = "Not added";
            }
            return msg;
        }

        public static string ValidateUserInputs_GetTransaction(string MemberId, string Type)
        {

            // *********************************************************************************//
            // **** Validate API User Input Before Sending Further for Processing  ******** //
            // ********************************************************************************//
            string msg = string.Empty;
            if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
            {
                msg = "Please enter MemberId.";
            }
            else if (!string.IsNullOrEmpty(MemberId))
            {
                Int64 Num;
                bool isNum = Int64.TryParse(MemberId, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid MemberId.";
                }
            }
            if (string.IsNullOrEmpty(msg))
            {
                if (string.IsNullOrEmpty(Type))
                {
                    msg = "Please enter Type.";
                }
                else
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(Type, out Num);
                    if (!isNum)
                    {
                        msg = "Please enter valid Type.";
                    }
                }
            }
            return msg;
        }
        public static IEnumerable<AddTransaction> GetAllTransactions(GetTransaction inobject)
        {
            AddTransaction outobject = new AddTransaction();
            inobject.CheckDelete = 0;
            List<AddTransaction> list = RepCRUD<GetTransaction, AddTransaction>.GetRecordList(Models.Common.Common.StoreProcedures.sp_WalletTransactions_Get, inobject, outobject);
            return list;
        }
    }
}