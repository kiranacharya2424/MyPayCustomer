using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MyPay.Repository
{
    public static class RepMerchants
    {
        public static string RequestMerchantOrderGenerate(string API_KEY, string ReturnUrl, string OrderId, string MerchantId, string Amount, string UserName, string Pwd, string Platform, string DeviceCode, string UserInput, ref string UniqueTransactionId, ref string RedirectURL, ref string OrderToken, ref AddVendor_API_Requests objVendor_API_Requests,string UserMobileNumber="")
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(OrderId))
            {
                msg = "Please enter OrderId.";
            }
            else if (OrderId.Length < 6)
            {
                msg = "Minimum OrderId Length Is 6 Digits";
            }
            else if (string.IsNullOrEmpty(UserName))
            {
                msg = "Please enter UserName.";
            }
            else if (string.IsNullOrEmpty(Pwd))
            {
                msg = "Please enter Password.";
            }
            else if (string.IsNullOrEmpty(Amount) || Amount == "0")
            {
                msg = "Please enter Amount.";
            }
            else if (!string.IsNullOrEmpty(Amount))
            {
                decimal Num;
                bool isNum = decimal.TryParse(Amount, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid Amount.";
                }
                else if (Convert.ToDecimal(Amount) <= 0)
                {
                    msg = "Please enter valid Amount.";
                }
            }
            if (string.IsNullOrEmpty(msg) && Convert.ToDecimal(Amount) <= 0)
            {
                msg = "Please enter valid Amount.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                if ((string.IsNullOrEmpty(MerchantId) || MerchantId == "0"))
                {
                    msg = "Please enter MerchantId.";
                }
                else
                {

                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        RedirectURL = res.CancelURL;
                        if (res.apikey != API_KEY)
                        {
                            msg = "Unauthorized API request";
                        }
                        else if (res.IsDeleted)
                        {
                            msg = "Merchant not found";
                        }
                        else if (res.IsActive == false)
                        {
                            msg = "Merchant Inactive";
                        }
                        else if (!Common.GetMerchantIPValidate(res.MerchantUniqueId))
                        {
                            msg = "Invalid IP Address.";
                        }
                        else if (res.API_User != UserName)
                        {
                            msg = "Unauthorized User request";
                        }
                        //else if (Common.DecryptionFromKey(res.API_Password, res.secretkey) != Pwd)
                        //{
                        //    msg = "Unauthorized User Credentials";
                        //}
                        else
                        {
                            string DecryptedKeyValue = Common.DecryptionFromKey(API_KEY, res.secretkey);
                            string[] strDecryptedArray = DecryptedKeyValue.Split(':');
                            if (string.IsNullOrEmpty(msg) && strDecryptedArray.Length >= 3)
                            {
                                string MerchantIDDecrypted = strDecryptedArray[2];
                                if (MerchantIDDecrypted == MerchantId)
                                {
                                    AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                    GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                    inobjectOrders.MerchantId = MerchantId;
                                    inobjectOrders.OrderId = OrderId;
                                    AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                    if (resOrders == null || resOrders.Id == 0)
                                    {
                                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                                        AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceChargeMerchant(MerchantId, Amount, VendorApiType.ToString());

                                        UniqueTransactionId = new CommonHelpers().GenerateUniqueId();
                                        OrderToken = Common.EncryptionFromKey(OrderId + ":" + MerchantId + ":" + UniqueTransactionId + ":", res.secretkey);
                                        AddMerchantOrders ObjMerchantOrders = new AddMerchantOrders();
                                        ObjMerchantOrders.CommissionAmount = Convert.ToDecimal(objOut.MerchantCommissionTotal);
                                        ObjMerchantOrders.DiscountAmount = Convert.ToDecimal(objOut.DiscountAmount);
                                        ObjMerchantOrders.CashbackAmount = Convert.ToDecimal(objOut.CashbackAmount);
                                        ObjMerchantOrders.MerchantContributionPercentage = Convert.ToDecimal(objOut.PercentageCommissionMerchant);
                                        ObjMerchantOrders.ServiceCharges = objOut.ServiceCharge;
                                        ObjMerchantOrders.Amount = Convert.ToDecimal(Amount);
                                        ObjMerchantOrders.OrderId = OrderId;
                                        ObjMerchantOrders.MerchantId = MerchantId;
                                        ObjMerchantOrders.OrganizationName = res.OrganizationName;
                                        ObjMerchantOrders.MerchantContactNo = res.ContactNo;
                                        ObjMerchantOrders.MerchantName = res.FirstName + " " + res.LastName;
                                        ObjMerchantOrders.MerchantId = MerchantId;
                                        ObjMerchantOrders.TransactionId = UniqueTransactionId;
                                        ObjMerchantOrders.IsActive = true;
                                        ObjMerchantOrders.IsDeleted = false;
                                        ObjMerchantOrders.IsApprovedByAdmin = true;
                                        ObjMerchantOrders.OrderToken = OrderToken;
                                        ObjMerchantOrders.Platform = Platform;
                                        ObjMerchantOrders.DeviceCode = DeviceCode;
                                        ObjMerchantOrders.Remarks = "Order Awaiting User Login and Payment";
                                        ObjMerchantOrders.Type = VendorApiType;
                                        ObjMerchantOrders.MemberId=objVendor_API_Requests.MemberId;
                                        ObjMerchantOrders.MemberName= objVendor_API_Requests.MemberName;
                                        ObjMerchantOrders.TrackerId = objVendor_API_Requests.TransactionUniqueId;
                                        ObjMerchantOrders.CreatedBy = res.Id;
                                        ObjMerchantOrders.CreatedByName = res.UserName;
                                        ObjMerchantOrders.UpdatedBy = res.Id;
                                        ObjMerchantOrders.MemberContactNumber = UserMobileNumber;
                                        ObjMerchantOrders.UpdatedByName = res.UserName;
                                        //if (objOut.MerchantCommissionTotal > 0)
                                        //{
                                        //    ObjMerchantOrders.NetAmount = Convert.ToDecimal(objOut.NetAmount) - Convert.ToDecimal(objOut.MerchantCommissionTotal);
                                        //}
                                        //else
                                        //{
                                            ObjMerchantOrders.NetAmount = Convert.ToDecimal(objOut.NetAmount);
                                        //}
                                        ObjMerchantOrders.Sign = (int)AddMerchantOrders.MerchantOrderSign.Credit;
                                        ObjMerchantOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Incomplete;
                                        ObjMerchantOrders.ReturnUrl = ReturnUrl;
                                       
                                        Int64 i = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Insert(ObjMerchantOrders, "merchantorders");
                                        if (i > 0)
                                        {
                                            msg = "success";
                                            RedirectURL = res.SuccessURL;
                                            Common.AddLogs($"Mypay Merchant Orders Transaction Generated Successfully on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                        else
                                        {
                                            msg = "Order generation failed";
                                            RedirectURL = res.CancelURL;
                                            Common.AddLogs($"Mypay Merchant Orders Transaction Failed on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                    }
                                    else
                                    {
                                        msg = "This Order Is Already Generated";
                                    }
                                }
                                else
                                {
                                    msg = "Unauthorized User MerchantId";
                                }
                            }
                            else
                            {
                                msg = "Invalid Api key length";
                            }
                        }
                    }
                    else
                    {
                        msg = "Invalid MerchantId";
                    }
                }
            }

            return msg;
        }


        public static string RequestMerchantOrderStatusCheck(string API_KEY, string TransactionId, string GatewayTransactionId, ref AddMerchantOrders result)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(API_KEY))
            {
                msg = "Please enter API_KEY.";
            }
            else if ((string.IsNullOrEmpty(GatewayTransactionId) && (string.IsNullOrEmpty(TransactionId))))
            {
                msg = "Please enter MerchantTransactionId.";
            }
            else
            {
                AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                if (!string.IsNullOrEmpty(TransactionId))
                {
                    inobjectOrders.TransactionId = TransactionId;
                }
                else
                {
                    inobjectOrders.TrackerId = GatewayTransactionId;
                }
                AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                if (resOrders != null && resOrders.Id != 0)
                {
                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = resOrders.MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        if (res.apikey == API_KEY)
                        {
                            result = resOrders;
                            msg = "success";
                            Common.AddLogs($"Mypay Merchant Orders Transaction Check status on {Common.fnGetdatetime()} for OrderID: {resOrders.OrderId}", false, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                        }
                        else
                        {
                            msg = "Invalid API_KEY";
                        }
                    }
                    else
                    {
                        msg = "Merchant Not Found";
                    }
                }
                else
                {
                    msg = "Order Not Found";
                }

            }
            return msg;
        }

        public static string RequestMerchantWalletTransaction(string API_KEY, string ContactNumber, string MerchantId, string Amount, string UserName, string Pwd, int VendorApiType, string PlatForm, string DeviceCode, string Remarks, string Reference, string AuthTokenString, string UserInput, string Signature, ref string UniqueTransactionId, ref string SenderTransactionId, ref string RedirectURL, ref string OrderToken, ref AddVendor_API_Requests objVendor_API_Requests)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(ContactNumber))
            {
                msg = "Please enter ContactNumber.";
            }
            else if (string.IsNullOrEmpty(UserName))
            {
                msg = "Please enter UserName.";
            }
            else if (string.IsNullOrEmpty(Pwd))
            {
                msg = "Please enter Password.";
            }
            else if (string.IsNullOrEmpty(Remarks))
            {
                msg = "Please enter Remarks.";
            }
            else if (string.IsNullOrEmpty(Reference))
            {
                msg = "Please enter Reference.";
            }
            else if (string.IsNullOrEmpty(Amount) || Amount == "0")
            {
                msg = "Please enter Amount.";
            }
            else if (!string.IsNullOrEmpty(Amount))
            {
                decimal Num;
                bool isNum = decimal.TryParse(Amount, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid Amount.";
                }
                else if (Convert.ToDecimal(Amount) <= 0)
                {
                    msg = "Please enter valid Amount.";
                }
            }
            if (string.IsNullOrEmpty(msg) && Convert.ToDecimal(Amount) <= 0)
            {
                msg = "Please enter valid Amount.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                if ((string.IsNullOrEmpty(MerchantId) || MerchantId == "0"))
                {
                    msg = "Please enter MerchantId.";
                }
                else
                {

                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        RedirectURL = res.CancelURL;
                        if (res.apikey != API_KEY)
                        {
                            msg = "Unauthorized API request";
                        }
                        else if (res.IsDeleted)
                        {
                            msg = "Merchant not found";
                        }
                        else if (res.IsActive == false)
                        {
                            msg = "Merchant Inactive";
                        }
                        else if (!Common.GetMerchantIPValidate(res.MerchantUniqueId))
                        {
                            msg = "Invalid IP Address.";
                        }
                        else if (res.API_User != UserName)
                        {
                            msg = "Unauthorized User request";
                        }
                        else if (Common.DecryptionFromKey(res.API_Password, res.secretkey) != Pwd)
                        {
                            msg = "Unauthorized User Credentials";
                        }
                        else
                        {
                            if (RepMerchants.VerifySignature($"KeyPair:{UserName}:{Pwd}", MerchantId, AuthTokenString) == false)
                            {
                                msg = "Unauthorized User Credentials Data";
                            }
                            else if (RepMerchants.VerifySignature(UserInput, MerchantId, Signature) == false)
                            {
                                msg = "Bad Data sent for API Request";
                            }
                            //else if ((VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load && (res.MerchantType != (int)AddMerchant.MerChantType.Bank)))
                            //{
                            //    msg = "Direct load APIs are available only for Banking Merchants.";
                            //}
                            //else if ((VendorApiType != (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load && (res.MerchantType == (int)AddMerchant.MerChantType.Bank)))
                            //{
                            //    msg = "This APIs Is not available for Banking Merchants.";
                            //}

                            else if ((VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load && (res.MerchantType != (int)AddMerchant.MerChantType.Bank)) || (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Remittance_Transactions && (res.MerchantType != (int)AddMerchant.MerChantType.Remittance)))
                            {
                                msg = "Direct load APIs are available only for Banking and Remit Merchants.";
                            }
                            else if ((VendorApiType != (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load && (res.MerchantType == (int)AddMerchant.MerChantType.Bank)) || (VendorApiType != (int)VendorApi_CommonHelper.KhaltiAPIName.Remittance_Transactions && (res.MerchantType == (int)AddMerchant.MerChantType.Remittance)))
                            {
                                msg = "This APIs Is not available for Banking and Remit Merchants.";
                            }
                            else
                            {
                                string DecryptedKeyValue = Common.DecryptionFromKey(API_KEY, res.secretkey);
                                string[] strDecryptedArray = DecryptedKeyValue.Split(':');
                                if (string.IsNullOrEmpty(msg) && strDecryptedArray.Length >= 3)
                                {
                                    string MerchantIDDecrypted = strDecryptedArray[2];
                                    if (MerchantIDDecrypted == MerchantId)
                                    {
                                        AddUserLoginWithPin outobject_User = new AddUserLoginWithPin();
                                        GetUserLoginWithPin inobject_User = new GetUserLoginWithPin();
                                        inobject_User.MemberId = res.UserMemberId;
                                        AddUserLoginWithPin res_User = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject_User, outobject_User);

                                        if (res_User != null && res_User.Id != 0 && res.UserMemberId != 0)
                                        {
                                            if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load)
                                            {
                                                msg = Common.CheckTransactionLimit(VendorApiType, (int)AddTransactionLimit.TransactionTransferTypeEnum.Load_Wallet_From_Bank, res.UserMemberId, Convert.ToDecimal(Amount)).ToLower();
                                            }
                                            else
                                            {
                                                msg = "success";
                                            }
                                            if (msg == "success")
                                            {
                                                UniqueTransactionId = new CommonHelpers().GenerateUniqueId();
                                                string Pin = Common.DecryptString(res_User.Pin);
                                                string CustomerId = res.OrganizationName + " (" + res.MerchantUniqueId + ")";
                                                string MerchantID = res.MerchantUniqueId;
                                                string MerchantOrganizationName = res.OrganizationName;
                                                bool IsDuplicateReference = new CommonHelpers().GetMerchantDuplicateReferenceTransaction(res.MerchantMemberId, Reference);
                                                if (IsDuplicateReference)
                                                {
                                                    msg = "Merchant Wallet Transaction Failed with Duplicate Reference " + Reference;
                                                    Common.AddLogs($"{msg} on {Common.fnGetdatetime()} ", true, Convert.ToInt32(AddLog.LogType.Merchant), Common.CreatedBy, "", false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                }
                                                else
                                                {
                                                    string CouponCode = string.Empty;
                                                    msg = RepTransactions.TransferByPhone(CouponCode, ref UniqueTransactionId, ref SenderTransactionId, res_User, CustomerId, res_User.MemberId, ContactNumber, Amount, Pin, Remarks, Reference, PlatForm, DeviceCode, true, UserInput, "", ref objVendor_API_Requests, VendorApiType, res.MerchantMemberId, MerchantID, MerchantOrganizationName);

                                                    if (msg.ToLower() == "success")
                                                    {
                                                        Common.AddLogs($"Mypay Merchant Wallet Transaction Completed Successfully on {Common.fnGetdatetime()} with TransactionId: {UniqueTransactionId}", true, Convert.ToInt32(AddLog.LogType.Merchant), Common.CreatedBy, "", false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                    }
                                                    else
                                                    {
                                                        msg = "Merchant Transaction Failed: " + msg;
                                                        Common.AddLogs($"Mypay Merchant Wallet Transaction Failed on {Common.fnGetdatetime()} ", true, Convert.ToInt32(AddLog.LogType.Merchant), Common.CreatedBy, "", false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = "Invalid User";
                                        }
                                    }
                                    else
                                    {
                                        msg = "Unauthorized User MerchantId";
                                    }
                                }
                                else
                                {
                                    msg = "Invalid Api key length";
                                }
                            }
                        }
                    }
                    else
                    {
                        msg = "Invalid MerchantId";
                    }

                }
            }
            return msg;
        }

        public static string RequestMerchantAccountValidation(string API_KEY, string Reference, Int32 VendorApiType, string ContactNumber, string MerchantId, string UserName, string Pwd, string PlatForm, string DeviceCode, string AuthTokenString, string UserInput, string Signature, ref AddVendor_API_Requests objVendor_API_Requests, ref string AccountStatus, ref string FullName)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(ContactNumber))
            {
                msg = "Please enter ContactNumber.";
            }
            else if (string.IsNullOrEmpty(Reference))
            {
                msg = "Please enter Reference.";
            }
            else if (string.IsNullOrEmpty(UserName))
            {
                msg = "Please enter UserName.";
            }
            else if (string.IsNullOrEmpty(Pwd))
            {
                msg = "Please enter Password.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                if ((string.IsNullOrEmpty(MerchantId) || MerchantId == "0"))
                {
                    msg = "Please enter MerchantId.";
                }
                else
                {

                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        if (res.apikey != API_KEY)
                        {
                            msg = "Unauthorized API request";
                        }
                        else if (res.IsDeleted)
                        {
                            msg = "Merchant not found";
                        }
                        else if (res.IsActive == false)
                        {
                            msg = "Merchant Inactive";
                        }
                        else if (!Common.GetMerchantIPValidate(res.MerchantUniqueId))
                        {
                            msg = "Invalid IP Address.";
                        }
                        else if (res.API_User != UserName)
                        {
                            msg = "Unauthorized User request";
                        }
                        else if (Common.DecryptionFromKey(res.API_Password, res.secretkey) != Pwd)
                        {
                            msg = "Unauthorized User Credentials";
                        }
                        else
                        {
                            if (RepMerchants.VerifySignature($"KeyPair:{UserName}:{Pwd}", MerchantId, AuthTokenString) == false)
                            {
                                msg = "Unauthorized User Credentials Data";
                            }
                            else if (RepMerchants.VerifySignature(UserInput, MerchantId, Signature) == false)
                            {
                                msg = "Bad Data sent for API Request";
                            }
                            else
                            {
                                string DecryptedKeyValue = Common.DecryptionFromKey(API_KEY, res.secretkey);
                                string[] strDecryptedArray = DecryptedKeyValue.Split(':');
                                if (string.IsNullOrEmpty(msg) && strDecryptedArray.Length >= 3)
                                {
                                    string MerchantIDDecrypted = strDecryptedArray[2];
                                    if (MerchantIDDecrypted == MerchantId)
                                    {
                                        objVendor_API_Requests.MemberId = res.UserMemberId;
                                        objVendor_API_Requests.MemberName = res.FirstName + " " + res.LastName;
                                        objVendor_API_Requests.CreatedBy = res.UserMemberId;
                                        objVendor_API_Requests.CreatedByName = res.FirstName + " " + res.LastName;
                                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                                        bool IsDuplicateReference = new CommonHelpers().GetDuplicateReferenceParameterFromMerchant(res.UserMemberId, Reference, VendorApiType);

                                        if (IsDuplicateReference)
                                        {
                                            msg = "Merchant Direct Account Validation Failed with Duplicate Reference " + Reference;
                                            Common.AddLogs($"{msg} on {Common.fnGetdatetime()} ", true, Convert.ToInt32(AddLog.LogType.Merchant), Common.CreatedBy, "", false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                        else
                                        {
                                            AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                                            GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                                            inobjectUser.ContactNumber = ContactNumber;
                                            inobjectUser.CheckDelete = 0;
                                            AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);

                                            if (resUser != null && resUser.MemberId != 0)
                                            {
                                                FullName = resUser.FirstName + " " + resUser.LastName;
                                                if (resUser.IsActive)
                                                {
                                                    msg = "success";
                                                    AccountStatus = "Active";
                                                }
                                                else
                                                {
                                                    msg = "Account Inactive";
                                                    AccountStatus = "Inactive";
                                                }
                                            }
                                            else
                                            {
                                                msg = "Account Information not found";
                                                AccountStatus = "Not Found";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = "Unauthorized User MerchantId";
                                    }
                                }
                                else
                                {
                                    msg = "Invalid Api key length";
                                }
                            }
                        }
                    }
                    else
                    {
                        msg = "Invalid MerchantId";
                    }

                }
            }
            return msg;
        }

        public static string RequestMerchantWalletTransactionCheckStatus(string API_KEY, string Reference, string TransactionReference, Int32 VendorApiType, string TransactionId, string MerchantId, string UserName, string Pwd, string PlatForm, string DeviceCode, string AuthTokenString, string UserInput, string Signature, ref AddVendor_API_Requests objVendor_API_Requests, ref string TransactionStatus, ref Int32 StatusCode, ref string TransactionUniqueID)
        {
            string msg = string.Empty;
            //if (string.IsNullOrEmpty(TransactionId))
            //{
            //    msg = "Please enter TransactionId.";
            //}
            //else 
            if (string.IsNullOrEmpty(Reference))
            {
                msg = "Please enter Reference.";
            }
            else if (string.IsNullOrEmpty(TransactionReference))
            {
                msg = "Please enter TransactionReference.";
            }
            else if (string.IsNullOrEmpty(UserName))
            {
                msg = "Please enter UserName.";
            }
            else if (string.IsNullOrEmpty(Pwd))
            {
                msg = "Please enter Password.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                if ((string.IsNullOrEmpty(MerchantId) || MerchantId == "0"))
                {
                    msg = "Please enter MerchantId.";
                }
                else
                {

                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        if (res.apikey != API_KEY)
                        {
                            msg = "Unauthorized API request";
                        }
                        else if (res.IsDeleted)
                        {
                            msg = "Merchant not found";
                        }
                        else if (res.IsActive == false)
                        {
                            msg = "Merchant Inactive";
                        }
                        else if (!Common.GetMerchantIPValidate(res.MerchantUniqueId))
                        {
                            msg = "Invalid IP Address.";
                        }
                        else if (res.API_User != UserName)
                        {
                            msg = "Unauthorized User request";
                        }
                        else if (Common.DecryptionFromKey(res.API_Password, res.secretkey) != Pwd)
                        {
                            msg = "Unauthorized User Credentials";
                        }
                        else
                        {
                            if (RepMerchants.VerifySignature($"KeyPair:{UserName}:{Pwd}", MerchantId, AuthTokenString) == false)
                            {
                                msg = "Unauthorized User Credentials Data";
                            }
                            else if (RepMerchants.VerifySignature(UserInput, MerchantId, Signature) == false)
                            {
                                msg = "Bad Data sent for API Request";
                            }
                            else
                            {
                                string DecryptedKeyValue = Common.DecryptionFromKey(API_KEY, res.secretkey);
                                string[] strDecryptedArray = DecryptedKeyValue.Split(':');
                                if (string.IsNullOrEmpty(msg) && strDecryptedArray.Length >= 3)
                                {
                                    string MerchantIDDecrypted = strDecryptedArray[2];
                                    if (MerchantIDDecrypted == MerchantId)
                                    {
                                        objVendor_API_Requests.MemberId = res.UserMemberId;
                                        objVendor_API_Requests.MemberName = res.FirstName + " " + res.LastName;
                                        objVendor_API_Requests.CreatedBy = res.UserMemberId;
                                        objVendor_API_Requests.CreatedByName = res.FirstName + " " + res.LastName;
                                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                                        bool IsDuplicateReference = new CommonHelpers().GetDuplicateReferenceParameterFromMerchant(res.UserMemberId, Reference, VendorApiType);

                                        if (IsDuplicateReference)
                                        {
                                            msg = "Merchant Direct load check status Failed with Duplicate Reference " + Reference;
                                            Common.AddLogs($"{msg} on {Common.fnGetdatetime()} ", true, Convert.ToInt32(AddLog.LogType.Merchant), Common.CreatedBy, "", false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                        else
                                        {
                                            WalletTransactions txn = new WalletTransactions();
                                            txn.Reference = TransactionReference;
                                            if (!string.IsNullOrEmpty(TransactionId))
                                            {
                                                txn.TransactionUniqueId = TransactionId;
                                            }
                                            if (txn.GetRecord())
                                            {
                                                TransactionUniqueID = txn.TransactionUniqueId;
                                                TransactionStatus = @Enum.GetName(typeof(WalletTransactions.Statuses), Convert.ToInt64(txn.Status)).ToString();
                                                StatusCode = txn.Status;
                                                msg = "Success";
                                            }
                                            else
                                            {
                                                msg = "Transaction not found";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = "Unauthorized User MerchantId";
                                    }
                                }
                                else
                                {
                                    msg = "Invalid Api key length";
                                }
                            }
                        }
                    }
                    else
                    {
                        msg = "Invalid MerchantId";
                    }

                }
            }
            return msg;
        }

        public static Int64 GetNewMerchantId()
        {
            Int64 Id = 0;
            MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
            string Result = commonHelpers.GetScalarValueWithValue("SELECT TOP 1 max(MerchantMemberId) FROM merchant with(nolock)");
            if (!string.IsNullOrEmpty(Result) && Result != "0")
            {
                Id = Convert.ToInt64(Result) + 1;
            }
            else
            {
                Id = MyPay.Models.Common.Common.StartingNumber;
            }
            return Id;
        }
        public static bool RSAKeyCheck(string PlainText = "This is really sent by me, really!")
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048); // Generate a new 2048 bit RSA key

            RSAParameters publicKey = rsa.ExportParameters(false);
            RSAParameters privateKey = rsa.ExportParameters(true);

            String privateKeyString = Cryptograph.ExportPrivateKey(rsa);
            String publicKeyString = Cryptograph.ExportPublicKey(rsa);

            //string encryptedText = Cryptograph.Encrypt(PlainText, publicKey.ToPublicKeyXml());

            //Console.WriteLine("This is the encrypted Text:" + "\n " + encryptedText);

            //string decryptedText = Cryptograph.Decrypt(encryptedText, privateKey.ToPrivateKeyXml());

            //Console.WriteLine("This is the decrypted text: " + decryptedText);

            RSACryptoServiceProvider publicKeyParameter = Cryptograph.ImportPublicKey(publicKeyString);
            RSACryptoServiceProvider privateKeyParameter = Cryptograph.ImportPrivateKey(privateKeyString);
            string messageToSign = PlainText;

            string signedMessage = Cryptograph.SignData(messageToSign, privateKey);

            //// Is this message really, really, REALLY sent by me?
            bool success = Cryptograph.VerifyData(messageToSign, signedMessage, publicKey);
            return success;
        }
        public static List<string> GenerateKeyPair_Merchant()
        {
            int keyBits = 2048;
            List<string> KeyPair = new List<string>();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keyBits); // Generate a new 2048 bit RSA key
            String privateKeyString = Cryptograph.ExportPrivateKey(rsa);
            String publicKeyString = Cryptograph.ExportPublicKey(rsa);
            KeyPair.Add(privateKeyString);
            KeyPair.Add(publicKeyString);

            return KeyPair;
        }
        public static string GenerateSignature(string messageToSign, string MerchantUniqueId)
        {
            string privateKeyString = (new CommonHelpers()).GetMerchantPrivateKey(MerchantUniqueId).Replace("\\n", Environment.NewLine);
            if (string.IsNullOrEmpty(privateKeyString))
            {
                return "";
            }
            RSACryptoServiceProvider privateKeyParameter = Cryptograph.ImportPrivateKey(privateKeyString);
            RSAParameters privateKey = privateKeyParameter.ExportParameters(true);
            string signedMessage = Cryptograph.SignData(messageToSign, privateKey);
            return signedMessage;
        }
        public static bool VerifySignature(string messageToSign, string MerchantUniqueId, string signedMessage)
        {
            string publicKeyString = (new CommonHelpers()).GetMerchantPublicKey(MerchantUniqueId).Replace("\\n", Environment.NewLine);
            if (string.IsNullOrEmpty(publicKeyString))
            {
                return false;
            }
            RSACryptoServiceProvider publicKeyParameter = Cryptograph.ImportPublicKey(publicKeyString);

            RSAParameters publicKey = publicKeyParameter.ExportParameters(false);
            bool success = Cryptograph.VerifyData(messageToSign, signedMessage, publicKey);
            return success;
        }

        public static string BankWithdrawalMerchant(string Amount, ref bool IsWithdrawalApproveByAdmin, AddMerchant model, AddUserLoginWithPin resUser, int WithdrawalRequestTypeID, decimal ServiceCharge, string userBankName, string userBankBankCode, string userBankAccountNumber, ref string error_message, ref string Particulars, ref string Json_Response, ref int BankStatus, string Description, AddMerchantOrders resOrders, string TransactionId, string ProcessId)
        {
            string msg;
            bool resOrdersFlag = false;
            GetFundAccountValidation valid = RepNps.FundAccountValidation(userBankName, userBankAccountNumber, userBankBankCode);
            string AccountValidationJson = JsonConvert.SerializeObject(valid);
            Common.AddLogs("Merchant Bank Account Validation JSON (MerchantUniqueId:" + model.MerchantUniqueId + ") INPUT : AccountName=" + userBankName + " Accountnumber=" + userBankAccountNumber + " BankCode = " + userBankBankCode + " OUTPUT: " + AccountValidationJson, true, (int)AddLog.LogType.Merchant);
            if (valid != null && !string.IsNullOrEmpty(valid.code) && (valid.code == "0" || (valid.data != null && Convert.ToInt32(valid.data.NameMatchPercentage) >= 80)))
            {
                GetFundTransferRequest fundres = new GetFundTransferRequest();
                fundres = RepNps.FundTransferRequest(userBankName, userBankAccountNumber, userBankBankCode, TransactionId, ProcessId, Amount.ToString(), Description);
                Json_Response = JsonConvert.SerializeObject(fundres);
                Common.AddLogs("Merchant Bank Withdrawal JSON (MerchantUniqueId:" + model.MerchantUniqueId + ")  : " + Json_Response, true, (int)AddLog.LogType.Merchant);
                if (fundres != null && !string.IsNullOrEmpty(fundres.code))
                {
                    AddMerchantOrders resDeposit = resOrders;
                    if (resDeposit.Id > 0)
                    {
                        if (fundres.errors.Count > 0)
                        {
                            error_message = fundres.errors[0].error_message;
                            Particulars = error_message;
                            resDeposit.Particulars = Particulars;
                            Json_Response = JsonConvert.SerializeObject(fundres);
                            BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                            resDeposit.Status = BankStatus;
                            RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resDeposit, "merchantorders");
                            Common.AddLogs(error_message, false, Convert.ToInt32(AddLog.LogType.Merchant), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                            msg = error_message;
                        }
                        else if ((fundres.code == "0" && fundres.data.TransactionStatus.ToLower() == "success") || (fundres.code == "2" || fundres.message.ToLower() == "pending"))
                        {
                            Particulars = fundres.data.TransactionStatus;
                            Json_Response = JsonConvert.SerializeObject(fundres);
                            if (fundres.code == "2" || fundres.message.ToLower() == "pending")
                            {
                                BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Pending;
                            }
                            else
                            {
                                BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Success;
                            }
                            if (WithdrawalRequestTypeID == (int)AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType.WalletBalance)
                            {
                                // ********  UPDATE TRANSACTION STATUS ************
                                WalletTransactions res_transaction = new WalletTransactions();
                                res_transaction.TransactionUniqueId = TransactionId;
                                if (!string.IsNullOrEmpty(TransactionId) && res_transaction.GetRecord())
                                {
                                    res_transaction.VendorTransactionId = fundres.data.TransactionId;
                                    res_transaction.BatchTransactionId = fundres.data.MerchantTxnId;
                                    res_transaction.GatewayStatus = fundres.data.TransactionStatus;
                                    res_transaction.ResponseCode = fundres.code;
                                    res_transaction.Status = BankStatus;
                                    if (fundres.code == "2" || fundres.message.ToLower() == "pending")
                                    {
                                        res_transaction.Remarks = "Merchant Withdraw Pending with Bank Transaction ID: " + fundres.data.TransactionId;
                                        res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
                                    }
                                    else
                                    {
                                        res_transaction.Remarks = "Merchant Withdraw Completed Successfully with Bank Transaction ID: " + fundres.data.TransactionId;
                                        res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                    }
                                    res_transaction.Update();
                                }
                                else
                                {
                                    msg = "Transaction Not Found";
                                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                }
                            }
                            // ********  UPDATE ORDER STATUS ************
                            if (fundres.code == "2" || fundres.message.ToLower() == "pending")
                            {
                                resOrders.Remarks = "Merchant Withdrawal Pending with Bank Transaction ID: " + fundres.data.TransactionId;
                            }
                            else
                            {
                                resOrders.Remarks = "Merchant Withdrawal Completed Successfully";
                            }
                            resOrders.UpdatedBy = model.UserMemberId;
                            resOrders.UpdatedByName = model.UserName;
                            resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Success;
                            resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                            if (resOrdersFlag)
                            {
                                msg = "success";
                                IsWithdrawalApproveByAdmin = true;
                                Common.AddLogs($"Payment Successfully Sent For Merchant Withdrawal (MerchantID: {resOrders.MerchantId} OrderID: {resOrders.OrderId})", false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                SentPaymentSuccessEmail(model, Convert.ToDecimal(Amount), TransactionId, Convert.ToDateTime(Common.fnGetdatetime()), Description);
                            }
                            else
                            {
                                msg = $"Withdrawal Order Update Failed For Merchant Withdrawal (MerchantID: {resOrders.MerchantId} OrderID: {resOrders.OrderId})";
                                Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                            }
                        }
                        else
                        {
                            BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                            resDeposit.Status = BankStatus;
                            Particulars = fundres.data.TransactionStatus;
                            resDeposit.Particulars = Particulars;
                            RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resDeposit, "merchantorders");

                            msg = "Withdrawal Transaction Has Been Failed.Due To Some Reasons. Please Try Again!";
                            Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                        }
                    }
                    else
                    {
                        msg = "Withdrawal Request Not Found";
                        Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                    }
                }
                else
                {
                    BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                    resOrders.Status = BankStatus;
                    Particulars = fundres.data.TransactionStatus;
                    resOrders.Particulars = Particulars;
                    Json_Response = JsonConvert.SerializeObject(fundres);
                    RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                    msg = "Merchant Withdrawal Data Not Found";
                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                }
            }
            else
            {
                BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                resOrders.Status = BankStatus;
                Particulars = "Merchant Withdrawal - Invalid Bank Details";
                resOrders.Particulars = Particulars;
                Json_Response = AccountValidationJson;
                RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                msg = "Merchant Withdrawal - Invalid Bank Details";
                Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
            }
            if (BankStatus != (int)AddMerchantOrders.MerchantOrderStatus.Success && BankStatus != (int)AddMerchantOrders.MerchantOrderStatus.Pending)
            {
                msg = RefundMerchantWithdrawal(Amount, model, resUser, WithdrawalRequestTypeID, ServiceCharge, resOrders, TransactionId);
            }
            else
            {
                Common.AddLogs("Withdrawal Bank Transaction " + Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), BankStatus).ToString(), false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
            }

            return msg;
        }

        public static string RefundMerchantWithdrawal(string Amount, AddMerchant model, AddUserLoginWithPin resUser, int WithdrawalRequestTypeID, decimal ServiceCharge, AddMerchantOrders resOrders, string TransactionId, string Remarks = "")
        {
            string msg = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(TransactionId))
                {
                    if (WithdrawalRequestTypeID == (int)AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType.WalletBalance)
                    {
                        // Merchant Wallet Refund
                        bool IsRefunded = MerchantWalletRefund(ref msg, TransactionId, resUser, Remarks);
                        if (IsRefunded)
                        {
                            Common.AddLogs($"Merchant Wallet Refund Successfully Completed For TransactionId:{TransactionId}", false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                        }
                        else
                        {
                            Common.AddLogs($"Merchant Wallet Refund Failed For TransactionId:{TransactionId}", false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                        }
                    }
                    if (WithdrawalRequestTypeID == (int)AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType.MerchantBalance)
                    {
                        AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                        GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                        inobjectOrders.MerchantId = model.MerchantUniqueId;
                        inobjectOrders.ParentTransactionId = resOrders.TransactionId;
                        AddMerchantOrders resOrdersCheck = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                        if (resOrdersCheck != null && resOrdersCheck.Id == 0)
                        {
                            // Merchant Refund
                            model.MerchantTotalAmount = model.MerchantTotalAmount + (Convert.ToDecimal(Amount) + Convert.ToDecimal(ServiceCharge));
                            bool resWalletUpdate = RepCRUD<AddMerchant, GetMerchant>.Update(model, "merchant");
                            if (resWalletUpdate)
                            {
                                AddMerchantOrders resOrdersRefund = new AddMerchantOrders();
                                resOrdersRefund = resOrders;
                                resOrdersRefund.CurrentBalance = model.MerchantTotalAmount;
                                resOrdersRefund.Sign = (int)AddMerchantOrders.MerchantOrderSign.Credit;
                                resOrdersRefund.Status = (int)AddMerchantOrders.MerchantOrderStatus.Refund;
                                resOrdersRefund.Remarks = $"Withdrawal Amount Refunded for OrderID: {resOrdersRefund.OrderId}";
                                resOrdersRefund.CreatedDate = DateTime.UtcNow;
                                resOrdersRefund.UpdatedDate = DateTime.UtcNow;
                                resOrdersRefund.TransactionId = new CommonHelpers().GenerateUniqueId();
                                resOrdersRefund.ParentTransactionId = resOrders.TransactionId;
                                resOrdersRefund.OrderId = "refund-" + new CommonHelpers().GenerateUniqueId();
                                Int64 RefundOrderID = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Insert(resOrdersRefund, "merchantorders");
                                if (RefundOrderID > 0)
                                {
                                    msg = "success";
                                    Common.AddLogs($"Withdrawal Refunded For TransactionID:{resOrders.TransactionId} For Merchant Withdrawal (MerchantID: {resOrders.MerchantId} )", false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                }
                                else
                                {
                                    msg = $"Refund Withdrawal Order Not Created for TransactionID:{resOrders.TransactionId}";
                                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                }
                            }
                            else
                            {
                                msg = $"Withdrawal Amount Refund Failed For Merchant Withdrawal With TransactionID:{resOrders.TransactionId}  (MerchantID: {resOrders.MerchantId} ).";
                                Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                            }
                        }
                        else
                        {
                            msg = $"Withdrawal Amount Already Refunded With TransactionID:{resOrdersCheck.TransactionId}  (MerchantID: {resOrders.MerchantId} ).";
                            Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                        }
                    }
                }
                else
                {
                    msg = "Transaction Not Found";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        private static bool MerchantWalletRefund(ref string msg, string TransactionId, AddUserLoginWithPin resuser, string Remarks = "")
        {
            bool IsRefunded = false;
            if (string.IsNullOrEmpty(TransactionId))
            {
                msg = $"ParentTransactionId Not Found For Refund Of TransactionId:{TransactionId}";
                Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
            }
            WalletTransactions res_transaction = new WalletTransactions();
            res_transaction.ParentTransactionId = TransactionId;
            if (!res_transaction.GetRecordCheckExists())
            {
                msg = VendorApi_CommonHelper.RefundMerchantBankTransfer("", TransactionId, (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Wallet_Withdrawal, Convert.ToString((int)WalletTransactions.WalletTypes.Bank), resuser, "", "", Remarks);
                if (msg.ToLower() == "success")
                {
                    IsRefunded = true;
                }
            }
            return IsRefunded;
        }

        private static void SentPaymentSuccessEmail(AddMerchant model, decimal Amount, string TransactionUniqueId, DateTime CreatedDate, string Description)
        {
            #region SendEmailConfirmation
            string mystring = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Templates/BankTransfer.html"));
            string body = mystring;
            body = body.Replace("##Amount##", (Amount).ToString("0.00"));
            body = body.Replace("##TransactionId##", TransactionUniqueId);
            body = body.Replace("##Date##", CreatedDate.ToString("dd-MMM-yyyy hh:mm:ss tt"));
            body = body.Replace("##Type##", "Bank Transfer");
            body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
            body = body.Replace("##Cashback##", "0");
            body = body.Replace("##ServiceCharge##", "0");
            //body = body.Replace("##Cashback##", objOut.CashbackAmount.ToString("0.00"));
            //body = body.Replace("##ServiceCharge##", objOut.ServiceCharge.ToString("0.00"));
            body = body.Replace("##Purpose##", Description);
            string Subject = MyPay.Models.Common.Common.WebsiteName + " - Bank Transfer Successfull";
            if (!string.IsNullOrEmpty(model.EmailID))
            {
                body = body.Replace("##UserName##", model.FirstName);
                Common.SendAsyncMail(model.EmailID, Subject, body);
            }
            #endregion
        }

        public static string CheckMerchantRemittanceCredentials(string API_KEY, string MerchantId, string Amount, string UserName, string Pwd, int VendorApiType, string PlatForm, string DeviceCode, string Remarks, string Reference, string AuthTokenString, string UserInput, string Signature, ref string UniqueTransactionId, ref string SenderTransactionId, ref string RedirectURL, ref string OrderToken, ref AddVendor_API_Requests objVendor_API_Requests)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                msg = "Please enter UserName.";
            }
            else if (string.IsNullOrEmpty(Pwd))
            {
                msg = "Please enter Password.";
            }
            else if (string.IsNullOrEmpty(Remarks))
            {
                msg = "Please enter Remarks.";
            }
            else if (string.IsNullOrEmpty(Reference))
            {
                msg = "Please enter Reference.";
            }
            else if (string.IsNullOrEmpty(Amount) || Amount == "0")
            {
                msg = "Please enter Amount.";
            }
            else if (!string.IsNullOrEmpty(Amount))
            {
                decimal Num;
                bool isNum = decimal.TryParse(Amount, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid Amount.";
                }
                else if (Convert.ToDecimal(Amount) <= 0)
                {
                    msg = "Please enter valid Amount.";
                }
            }
            if (string.IsNullOrEmpty(msg) && Convert.ToDecimal(Amount) <= 0)
            {
                msg = "Please enter valid Amount.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                if ((string.IsNullOrEmpty(MerchantId) || MerchantId == "0"))
                {
                    msg = "Please enter MerchantId.";
                }
                else
                {

                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        RedirectURL = res.CancelURL;
                        if (res.apikey != API_KEY)
                        {
                            msg = "Unauthorized API request";
                        }
                        else if (res.IsDeleted)
                        {
                            msg = "Merchant not found";
                        }
                        else if (res.IsActive == false)
                        {
                            msg = "Merchant Inactive";
                        }
                        else if (!Common.GetMerchantIPValidate(res.MerchantUniqueId))
                        {
                            msg = "Invalid IP Address.";
                        }
                        else if (res.API_User != UserName)
                        {
                            msg = "Unauthorized User request";
                        }
                        else if (Common.DecryptionFromKey(res.API_Password, res.secretkey) != Pwd)
                        {
                            msg = "Unauthorized User Credentials";
                        }
                        else
                        {
                            if (RepMerchants.VerifySignature($"KeyPair:{UserName}:{Pwd}", MerchantId, AuthTokenString) == false)
                            {
                                msg = "Unauthorized User Credentials Data";
                            }
                            else if (RepMerchants.VerifySignature(UserInput, MerchantId, Signature) == false)
                            {
                                msg = "Bad Data sent for API Request";
                            }                            
                            else
                            {
                                string DecryptedKeyValue = Common.DecryptionFromKey(API_KEY, res.secretkey);
                                string[] strDecryptedArray = DecryptedKeyValue.Split(':');
                                if (string.IsNullOrEmpty(msg) && strDecryptedArray.Length >= 3)
                                {
                                    string MerchantIDDecrypted = strDecryptedArray[2];
                                    if (MerchantIDDecrypted == MerchantId)
                                    {
                                        AddUserLoginWithPin outobject_User = new AddUserLoginWithPin();
                                        GetUserLoginWithPin inobject_User = new GetUserLoginWithPin();
                                        inobject_User.MemberId = res.UserMemberId;
                                        AddUserLoginWithPin res_User = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject_User, outobject_User);

                                        if (res_User != null && res_User.Id != 0 && res.UserMemberId != 0)
                                        {
                                            msg = "success";

                                        }
                                        else
                                        {
                                            msg = "Invalid User";
                                        }
                                    }
                                    else
                                    {
                                        msg = "Unauthorized User MerchantId";
                                    }
                                }
                                else
                                {
                                    msg = "Invalid Api key length";
                                }
                            }
                        }
                    }
                    else
                    {
                        msg = "Invalid MerchantId";
                    }

                }
            }
            return msg;
        }

        public static string RequestMerchantOrderGenerate_BUS(string API_KEY, string ReturnUrl, string OrderId, string MerchantId, string Amount, string UserName, string Pwd, string Platform, string DeviceCode, string UserInput, ref string UniqueTransactionId, ref string RedirectURL, ref string OrderToken, ref AddVendor_API_Requests objVendor_API_Requests,long MemId,string MemName,string MemContactNo,string commissionamount)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(OrderId))
            {
                msg = "Please enter OrderId.";
            }
            else if (OrderId.Length < 6)
            {
                msg = "Minimum OrderId Length Is 6 Digits";
            }
            else if (string.IsNullOrEmpty(UserName))
            {
                msg = "Please enter UserName.";
            }
            else if (string.IsNullOrEmpty(Pwd))
            {
                msg = "Please enter Password.";
            }
            else if (string.IsNullOrEmpty(Amount) || Amount == "0")
            {
                msg = "Please enter Amount.";
            }
            else if (!string.IsNullOrEmpty(Amount))
            {
                decimal Num;
                bool isNum = decimal.TryParse(Amount, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid Amount.";
                }
                else if (Convert.ToDecimal(Amount) <= 0)
                {
                    msg = "Please enter valid Amount.";
                }
            }
            if (string.IsNullOrEmpty(msg) && Convert.ToDecimal(Amount) <= 0)
            {
                msg = "Please enter valid Amount.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                if ((string.IsNullOrEmpty(MerchantId) || MerchantId == "0"))
                {
                    msg = "Please enter MerchantId.";
                }
                else
                {

                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        RedirectURL = res.CancelURL;
                        if (res.apikey != API_KEY)
                        {
                            msg = "Unauthorized API request";
                        }
                        else if (res.IsDeleted)
                        {
                            msg = "Merchant not found";
                        }
                        else if (res.IsActive == false)
                        {
                            msg = "Merchant Inactive";
                        }
                        //else if (!Common.GetMerchantIPValidate(res.MerchantUniqueId))
                        //{
                        //    msg = "Invalid IP Address.";
                        //}
                        else if (res.API_User != UserName)
                        {
                            msg = "Unauthorized User request";
                        }
                        else if (Common.DecryptionFromKey(res.API_Password, res.secretkey) != Common.DecryptionFromKey(Pwd, res.secretkey))
                        {
                            msg = "Unauthorized User Credentials";
                        }
                        else
                        {
                            string DecryptedKeyValue = Common.DecryptionFromKey(API_KEY, res.secretkey);
                            string[] strDecryptedArray = DecryptedKeyValue.Split(':');
                            if (string.IsNullOrEmpty(msg) && strDecryptedArray.Length >= 3)
                            {
                                string MerchantIDDecrypted = strDecryptedArray[2];
                                if (MerchantIDDecrypted == MerchantId)
                                {
                                    AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                    GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                    inobjectOrders.MerchantId = MerchantId;
                                    inobjectOrders.OrderId = OrderId;
                                    AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                    if (resOrders == null || resOrders.Id == 0)
                                    {
                                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Settlement;
                                        AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceChargeMerchant(MerchantId, Amount, VendorApiType.ToString());

                                        UniqueTransactionId = new CommonHelpers().GenerateUniqueId();
                                        OrderToken = Common.EncryptionFromKey(OrderId + ":" + MerchantId + ":" + UniqueTransactionId + ":", res.secretkey);
                                        AddMerchantOrders ObjMerchantOrders = new AddMerchantOrders();
                                        ObjMerchantOrders.Amount = Convert.ToDecimal(Amount);
                                        if (UserName.ToLower() == "bussewa")
                                        {
                                            ObjMerchantOrders.CommissionAmount = Convert.ToDecimal(objOut.MerchantCommissionTotal);
                                        }
                                        else
                                        {
                                            ObjMerchantOrders.CommissionAmount =Convert.ToDecimal( commissionamount);  // tourist bus (we get commision from third party in fetchbus api )
                                        }
                                       // ObjMerchantOrders.CommissionAmount = Convert.ToDecimal(objOut.MerchantCommissionTotal);
                                        ObjMerchantOrders.DiscountAmount = Convert.ToDecimal(objOut.DiscountAmount);
                                        ObjMerchantOrders.CashbackAmount = Convert.ToDecimal(objOut.CashbackAmount);
                                        ObjMerchantOrders.MerchantContributionPercentage = Convert.ToDecimal(objOut.PercentageCommissionMerchant);
                                        ObjMerchantOrders.ServiceCharges = objOut.ServiceCharge;
                                        ObjMerchantOrders.NetAmount = Convert.ToDecimal(objOut.NetAmount);
                                        //ObjMerchantOrders.CurrentBalance = Convert.ToDecimal(res.MerchantTotalAmount);
                                        ObjMerchantOrders.OrderId = OrderId;
                                        ObjMerchantOrders.MerchantId = MerchantId;
                                        ObjMerchantOrders.OrganizationName = res.OrganizationName;
                                        ObjMerchantOrders.MerchantContactNo = res.ContactNo;
                                        ObjMerchantOrders.MerchantName = res.FirstName + " " + res.LastName;
                                        ObjMerchantOrders.MemberId = MemId;
                                        ObjMerchantOrders.MemberName =MemName;
                                        ObjMerchantOrders.MemberContactNumber = MemContactNo;
                                        ObjMerchantOrders.TransactionId = UniqueTransactionId;
                                        ObjMerchantOrders.IsActive = true;
                                        ObjMerchantOrders.IsDeleted = false;
                                        ObjMerchantOrders.IsApprovedByAdmin = true;
                                        ObjMerchantOrders.OrderToken = OrderToken;
                                        ObjMerchantOrders.Platform = Platform;
                                        ObjMerchantOrders.DeviceCode = DeviceCode;
                                        ObjMerchantOrders.Remarks = "Order Awaiting Payment";
                                        ObjMerchantOrders.Type = VendorApiType;
                                        ObjMerchantOrders.CreatedBy = res.Id;
                                        ObjMerchantOrders.CreatedByName = res.UserName;
                                        ObjMerchantOrders.UpdatedBy = res.Id;
                                        ObjMerchantOrders.UpdatedByName = res.UserName;
                                        ObjMerchantOrders.Sign = (int)AddMerchantOrders.MerchantOrderSign.Credit;
                                        ObjMerchantOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Incomplete;
                                        ObjMerchantOrders.ReturnUrl = ReturnUrl;
                                        Int64 i = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Insert(ObjMerchantOrders, "merchantorders");
                                        if (i > 0)
                                        {
                                            msg = "success";
                                            RedirectURL = res.SuccessURL;
                                            Common.AddLogs($"Mypay Merchant Orders Transaction Generated Successfully on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                        else
                                        {
                                            msg = "Order generation failed";
                                            RedirectURL = res.CancelURL;
                                            Common.AddLogs($"Mypay Merchant Orders Transaction Failed on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                    }
                                    else
                                    {
                                        msg = "This Order Is Already Generated";
                                    }
                                }
                                else
                                {
                                    msg = "Unauthorized User MerchantId";
                                }
                            }
                            else
                            {
                                msg = "Invalid Api key length";
                            }
                        }
                    }
                    else
                    {
                        msg = "Invalid MerchantId";
                    }
                }
            }

            return msg;
        }
        public static string RequestMerchantOrderGenerateCableCar(string API_KEY, string ReturnUrl, string OrderId, string MerchantId, string Amount, string UserName, string Pwd, string Platform, string DeviceCode, string UserInput, ref string UniqueTransactionId, ref string RedirectURL, ref string OrderToken, ref AddVendor_API_Requests objVendor_API_Requests, string UserMobileNumber = "")
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(OrderId))
            {
                msg = "Please enter OrderId.";
            }
            else if (OrderId.Length < 6)
            {
                msg = "Minimum OrderId Length Is 6 Digits";
            }
            else if (string.IsNullOrEmpty(UserName))
            {
                msg = "Please enter UserName.";
            }
            else if (string.IsNullOrEmpty(Pwd))
            {
                msg = "Please enter Password.";
            }
            else if (string.IsNullOrEmpty(Amount) || Amount == "0")
            {
                msg = "Please enter Amount.";
            }
            else if (!string.IsNullOrEmpty(Amount))
            {
                decimal Num;
                bool isNum = decimal.TryParse(Amount, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid Amount.";
                }
                else if (Convert.ToDecimal(Amount) <= 0)
                {
                    msg = "Please enter valid Amount.";
                }
            }
            if (string.IsNullOrEmpty(msg) && Convert.ToDecimal(Amount) <= 0)
            {
                msg = "Please enter valid Amount.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                if ((string.IsNullOrEmpty(MerchantId) || MerchantId == "0"))
                {
                    msg = "Please enter MerchantId.";
                }
                else
                {

                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = MerchantId;
                    inobject.CheckActive = 1;
                    inobject.CheckDelete = 0;
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        RedirectURL = res.CancelURL;
                        if (res.apikey != API_KEY)
                        {
                            msg = "Unauthorized API request";
                        }
                        else if (res.IsDeleted)
                        {
                            msg = "Merchant not found";
                        }
                        else if (res.IsActive == false)
                        {
                            msg = "Merchant Inactive";
                        }
                        //else if (!Common.GetMerchantIPValidate(res.MerchantUniqueId))
                        //{
                        //    msg = "Invalid IP Address.";
                        //}
                        else if (res.API_User != UserName)
                        {
                            msg = "Unauthorized User request";
                        }
                        //else if (Common.DecryptionFromKey(res.API_Password, res.secretkey) != Pwd)
                        //{
                        //    msg = "Unauthorized User Credentials";
                        //}
                        else
                        {
                            string DecryptedKeyValue = Common.DecryptionFromKey(API_KEY, res.secretkey);
                            string[] strDecryptedArray = DecryptedKeyValue.Split(':');
                            if (string.IsNullOrEmpty(msg) && strDecryptedArray.Length >= 3)
                            {
                                string MerchantIDDecrypted = strDecryptedArray[2];
                                if (MerchantIDDecrypted == MerchantId)
                                {
                                    AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                    GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                    inobjectOrders.MerchantId = MerchantId;
                                    inobjectOrders.OrderId = OrderId;
                                    AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                    if (resOrders == null || resOrders.Id == 0)
                                    {
                                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                                        AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceChargeMerchant(MerchantId, Amount, VendorApiType.ToString());

                                        UniqueTransactionId = new CommonHelpers().GenerateUniqueId();
                                        OrderToken = Common.EncryptionFromKey(OrderId + ":" + MerchantId + ":" + UniqueTransactionId + ":", res.secretkey);
                                        AddMerchantOrders ObjMerchantOrders = new AddMerchantOrders();
                                        ObjMerchantOrders.CommissionAmount = Convert.ToDecimal(objOut.MerchantCommissionTotal);
                                        ObjMerchantOrders.DiscountAmount = Convert.ToDecimal(objOut.DiscountAmount);
                                        ObjMerchantOrders.CashbackAmount = Convert.ToDecimal(objOut.CashbackAmount);
                                        ObjMerchantOrders.MerchantContributionPercentage = Convert.ToDecimal(objOut.PercentageCommissionMerchant);
                                        ObjMerchantOrders.ServiceCharges = objOut.ServiceCharge;
                                        ObjMerchantOrders.Amount = Convert.ToDecimal(Amount);
                                        ObjMerchantOrders.OrderId = OrderId;
                                        ObjMerchantOrders.MerchantId = MerchantId;
                                        ObjMerchantOrders.OrganizationName = res.OrganizationName;
                                        ObjMerchantOrders.MerchantContactNo = res.ContactNo;
                                        ObjMerchantOrders.MerchantName = res.FirstName + " " + res.LastName;
                                        ObjMerchantOrders.MerchantId = MerchantId;
                                        ObjMerchantOrders.TransactionId = UniqueTransactionId;
                                        ObjMerchantOrders.IsActive = true;
                                        ObjMerchantOrders.IsDeleted = false;
                                        ObjMerchantOrders.IsApprovedByAdmin = true;
                                        ObjMerchantOrders.OrderToken = OrderToken;
                                        ObjMerchantOrders.Platform = Platform;
                                        ObjMerchantOrders.DeviceCode = DeviceCode;
                                        ObjMerchantOrders.Remarks = "Order Awaiting User Login and Payment";
                                        ObjMerchantOrders.Type = VendorApiType;
                                        ObjMerchantOrders.MemberId = objVendor_API_Requests.MemberId;
                                        ObjMerchantOrders.MemberName = objVendor_API_Requests.MemberName;
                                        ObjMerchantOrders.TrackerId = objVendor_API_Requests.TransactionUniqueId;
                                        ObjMerchantOrders.CreatedBy = res.Id;
                                        ObjMerchantOrders.CreatedByName = res.UserName;
                                        ObjMerchantOrders.UpdatedBy = res.Id;
                                        ObjMerchantOrders.MemberContactNumber = UserMobileNumber;
                                        ObjMerchantOrders.UpdatedByName = res.UserName;
                                        //if (objOut.MerchantCommissionTotal > 0)
                                        //{
                                        //    ObjMerchantOrders.NetAmount = Convert.ToDecimal(objOut.NetAmount) - Convert.ToDecimal(objOut.MerchantCommissionTotal);
                                        //}
                                        //else
                                        //{
                                        ObjMerchantOrders.NetAmount = Convert.ToDecimal(objOut.NetAmount);
                                        //}
                                        ObjMerchantOrders.Sign = (int)AddMerchantOrders.MerchantOrderSign.Credit;
                                        ObjMerchantOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Incomplete;
                                        ObjMerchantOrders.ReturnUrl = ReturnUrl;

                                        Int64 i = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Insert(ObjMerchantOrders, "merchantorders");
                                        if (i > 0)
                                        {
                                            msg = "success";
                                            RedirectURL = res.SuccessURL;
                                            Common.AddLogs($"Mypay Merchant Orders Transaction Generated Successfully on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                        else
                                        {
                                            msg = "Order generation failed";
                                            RedirectURL = res.CancelURL;
                                            Common.AddLogs($"Mypay Merchant Orders Transaction Failed on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), res.UserMemberId, res.FirstName + " " + res.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                        }
                                    }
                                    else
                                    {
                                        msg = "This Order Is Already Generated";
                                    }
                                }
                                else
                                {
                                    msg = "Unauthorized User MerchantId";
                                }
                            }
                            else
                            {
                                msg = "Invalid Api key length";
                            }
                        }
                    }
                    else
                    {
                        msg = "Invalid MerchantId";
                    }
                }
            }

            return msg;
        }

        }
}
