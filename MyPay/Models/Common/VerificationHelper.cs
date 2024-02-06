using Dapper;
using MyPay.Models.Add;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyPay.Models.Common
{
    public class VerificationHelper
    {

        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        public string checkAmountValid(string amount) {
            string result = "success";
            if (!string.IsNullOrEmpty(amount))
            {
                decimal Num;
                bool isNum = decimal.TryParse(amount, out Num);
                if (!isNum)
                {
                    result = "Please enter valid amount.";
                }
                else if (Convert.ToDecimal(amount) <= 0)
                {
                    result = "Please enter amount greater than zero.";
                }
            }
            else
            {
                result = "Please enter amount.";
            }
            return result;
        }

        public string checkDeviceID(string deviceID) {
            string result = "success";
            if (string.IsNullOrEmpty(deviceID))
            {
                result = "Please enter DeviceId";
            }
            return result;
        }

        public string checkIfUTokenIsValid()
        {
            string result = "success";
            if (HttpContext.Current.Request.Headers.GetValues("UToken") != null)
            {
                string secretkey = HttpContext.Current.Request.Headers.GetValues("UToken").First();

                if (string.IsNullOrEmpty(secretkey))
                {
                    result = "UToken not found";
                }
            }
            return result;
        }

        public string checkIfUserIsValid(string JWTToken, string deviceID, ref MyUser user) { 
            string result = "success";

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM users where deviceID = @deviceID and JWTToken = @JWTToken";
                var parameters = new { deviceID = deviceID, JWTToken = JWTToken };

                var userResult = connection.QueryFirst <MyUser>(sql,parameters);
                //if (userResult == null)
                //{
                //    return "User not found";
                //    //Common.not
                //}
                if(userResult.Id <= 0)
                {
                    return Common.SessionExpired;
                }

                if (user.IsActive == false)
                {
                    return Common.InactiveUserMessage;
                }

                user = userResult;
            }
            return result;
        }

        public string getUser(string secretkey, string DeviceId, ref AddUserLoginWithPin user)
        {
            string result = "success";
            AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
            GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
            inobjectUser.JwtToken = secretkey;
            inobjectUser.DeviceId = DeviceId;
            user = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
            if (user.Id > 0)
            {
                return "success";
            }
            else {
                return Common.SessionExpired;
            }
        }
        public string checkIfTokenIsValid(string contactNumber) {
            string result = "success";

            string originalFileName = Common.EncryptString(contactNumber).Replace("/", "@@@");
            string TokenUpdatedTime = string.Empty;
            string filepath = HttpContext.Current.Server.MapPath("/UserCodes/" + originalFileName + ".txt");
            if (!System.IO.File.Exists(filepath))
            {
                result = Common.Invalidusertoken;
            }
           
            TokenUpdatedTime = (File.ReadAllText(filepath));
            if (!string.IsNullOrEmpty(TokenUpdatedTime) && DateTime.UtcNow < Convert.ToDateTime(TokenUpdatedTime))
            {
                //if (!notcheckMemberId && memId.ToString() != resUser.MemberId.ToString())
                //{
                //    result = Common.Invalidusertoken;
                //}

            }
                return result;
        }

        public string checkPin(string mpin, AddUserLoginWithPin user ) {
            string result = "";
            int AttemptedCount = 0;
            DateTime AttemptedDatetime = DateTime.UtcNow;
            string originalFileNameForPin = Common.EncryptString(Convert.ToString(user.MemberId)).Replace("/", "@@@");
            string filepathForPin = HttpContext.Current.Server.MapPath("/LoginAttempt/" + originalFileNameForPin + ".txt");
            if (System.IO.File.Exists(filepathForPin))
            {
                string JsonForPin = (File.ReadAllText(filepathForPin));
                if (!string.IsNullOrEmpty(JsonForPin))
                {
                    CommonLoginAttempt onCommonLoginAttempt = JsonConvert.DeserializeObject<CommonLoginAttempt>(JsonForPin);
                    AttemptedCount = Convert.ToInt32(onCommonLoginAttempt.AttemptedCount);
                    AttemptedDatetime = Convert.ToDateTime(onCommonLoginAttempt.AttemptedDatetime);
                    if (AttemptedCount >= 25)
                    {
                        if ((System.DateTime.UtcNow - AttemptedDatetime).TotalHours < 2)
                        {
                            return "Your account is blocked for 2 hours due to wrong pin attempts. Please try again later.";
                        }
                        else if (string.IsNullOrEmpty(mpin) || Common.Decryption(mpin) != Common.DecryptString(user.Pin))
                        {
                            InvalidPinUserUpdate(user.MemberId);
                            return Common.SessionExpired;
                        }
                            AttemptedCount = 0;
                            string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                            System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);
                        
                    }
                }
            }
            if (result == "")
            {
                if (string.IsNullOrEmpty(user.Pin))
                {
                    result = Common.SetYourPin;
                }
                else if (string.IsNullOrEmpty(mpin) || Common.Decryption(mpin) != Common.DecryptString(user.Pin))
                {
                    if (!System.IO.File.Exists(filepathForPin))
                    {
                        var LoginAttemptFile = System.IO.File.Create(filepathForPin);
                        LoginAttemptFile.Close();
                        LoginAttemptFile.Dispose();
                    }
                    else
                    {
                        string JsonForPin = (File.ReadAllText(filepathForPin));
                        if (!string.IsNullOrEmpty(JsonForPin))
                        {
                            CommonLoginAttempt onCommonLoginAttempt = JsonConvert.DeserializeObject<CommonLoginAttempt>(JsonForPin);
                            AttemptedCount = Convert.ToInt32(onCommonLoginAttempt.AttemptedCount) + 1;
                        }
                    }
                    if (AttemptedCount <= 5)
                    {
                        string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                        System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);
                    }
                    return Common.Invalidpin;
                   // Common.AddLogs($"Transaction {Common.Invalidpin} : {VendorApiTypeName} For Rs. {amount} ", false, (int)AddLog.LogType.User, resUser.MemberId, resUser.FirstName, true);
                }
                else
                {
                    AttemptedCount = 0;
                    string NewJsonForPin = "{\"AttemptedDatetime\":\"" + DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"AttemptedCount\":\"" + AttemptedCount + "\"}";
                    System.IO.File.WriteAllText(filepathForPin, NewJsonForPin);
                }
            }
            return "success";
        }

        public bool InvalidPinUserUpdate(Int64 MemberId)
        {
            bool data = false;
            try
            {
                if (MemberId != 0)
                {
                    CommonHelpers obj = new CommonHelpers();
                    string Result = "";
                    string str = $"exec sp_Users_InvalidPin_Update '{MemberId}'";
                    Result = obj.GetScalarValueWithValue(str);
                    if (!string.IsNullOrEmpty(Result) && Result != "0")
                    {
                        data = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public WalletTransactions.WalletTypes checkPaymentIstrumentType(int instrument) {
            return (WalletTransactions.WalletTypes) instrument;
        }

        public bool checkIfPaymentInstrumentIsAllowedForTransaction(int instrument) {
            return true;
        }


        public string validateScratchCoupon(string couponCode) {
           // Common.ValidateCoupon(resUser.MemberId, CouponCode, Type, ref resGetCouponsScratched, VendorApiType);
           string result = "success";

            return result;
        }

        public AddCalculateServiceChargeAndCashback calculateServiceCharge(string memberID, string amount, string vendorAPIType) {
            AddCalculateServiceChargeAndCashback serviceChargeAndCashback = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(memberID, amount, vendorAPIType);
            return serviceChargeAndCashback;
        }

        public string getCouponDiscount(string couponCode, string amount, string memberID, AddCalculateServiceChargeAndCashback serviceChargeAndCashBack, WalletTransactions.WalletTypes walletType, decimal walletBalance, decimal discountAmount) {
         

            if(walletType == WalletTransactions.WalletTypes.MPCoins || walletType == WalletTransactions.WalletTypes.Bank ) {
                return "Coupons cannot be applied on Bank or MyPay Coins Payment";
            }

            using (var connection = new SqlConnection(connectionString))
            {
                string result = "success";
                var sql = "SELECT * FROM CouponsScratched where memberID = @memberID and couponCode = @couponCode";
                var parameters = new { memberID = memberID, couponCode = couponCode };

                var couponData = connection.QueryFirst<CouponDetails>(sql, parameters);
                if (couponData == null)
                {
                    return "coupon not found";
                }


                decimal CouponDeduct = (Convert.ToDecimal(amount) * couponData.couponPercentage) / 100;
                if (couponData.minumumAmount > 0 && CouponDeduct < couponData.minumumAmount)
                {
                    CouponDeduct = couponData.minumumAmount;
                }
                else if (couponData.maximumAmount > 0 && CouponDeduct > couponData.maximumAmount)
                {
                    CouponDeduct = couponData.maximumAmount;
                }
                decimal finalamount = Convert.ToDecimal(amount) + serviceChargeAndCashBack.ServiceCharge - (CouponDeduct);
                if (((walletType == 0 || walletType == WalletTransactions.WalletTypes.Wallet)) && walletBalance < finalamount)
                {
                    result = Common.InsufficientBalance;
                }
                else
                {
                    result = "";
                }

                return result;
            }
        }
    }
}


public class CouponDetails { 
    public int couponType { get; set; }
    public int memberID { get; set; }
    public string couponCode { get; set; }
    public int serviceID { get; set; }
    public bool isUsed { get; set; }
    public bool isExpired { get; set; }
    public bool isActive { get; set; }
    public decimal couponAmount { get; set; }
    public decimal couponPercentage { get; set; }
    public decimal minumumAmount { get; set; }
    public decimal maximumAmount { get; set; }
}


public class MyUser { 
    public long Id { get; set; }
    public string UserId { get; set; }

    public string Password { get; set; }

    public bool IsActive { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }


    public string LastName { get; set; }

    public long MemberId { get; set; }

    public string ContactNumber { get; set; }

    public int IsKYCApproved { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal TotalRewardPoints { get; set; }

}