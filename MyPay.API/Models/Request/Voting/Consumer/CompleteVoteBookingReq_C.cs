using MyPay.API.Models.Request.Voting.Partner;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Voting.Consumer
{
    public class CompleteVoteBookingReq_C : CompleteVoteBookingReq_P, ICommonVotingProps
    {
        public string DeviceCode { get; set; }
        public string DeviceId { get; set; }
        public string Version { get; set; }
        public string PlatForm { get; set; }
        public long TimeStamp { get; set; }
        public string Token { get; set; }
        public string UniqueCustomerId { get; set; }
        public string SecretKey { get; set; } = "";
        public string Mpin { get; set; } = "";
        public string Hash { get; set; } = "";
        public string MemberId { get; set; }

        public string PaymentMerchantId { get; set; }

        public int PaymentMethodId { get; set; }


        public string PaymentMode { get; set; }

        public string BankTransactionID { get; set; }

        public string Reference { get; set; }

        public void createSignature(string key, ref CompleteVoteBookingReq_P objectToSendToPartner)
        {

            objectToSendToPartner = JsonConvert.DeserializeObject<CompleteVoteBookingReq_P>(JsonConvert.SerializeObject(this));

            string concatenatedReq = "";
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var property in typeof(CompleteVoteBookingReq_P).GetProperties())
            {
                properties.Add(property);
            }

            var propertiesList = properties.OrderBy(S => S.Name.ToLower());

            foreach (var item in propertiesList)
            {
                if (item.GetValue(objectToSendToPartner) != null)
                {
                    concatenatedReq = concatenatedReq + item.GetValue(this).ToString();
                }
            }
            objectToSendToPartner.signature = HMACSHA512(concatenatedReq, key);
        }

        internal static string HMACSHA512(string text, string secretKey)
        {
            var hash = new StringBuilder();
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString().ToUpper();
        }

    }
}
