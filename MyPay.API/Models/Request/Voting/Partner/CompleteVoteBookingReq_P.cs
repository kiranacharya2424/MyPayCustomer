using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Voting.Partner
{
    public class CompleteVoteBookingReq_P
    {
        public string orderId { get; set; }
        public string transactionId { get; set; }
        public string signature { get; set; }

      //  public string Reference { get; set; }

        public void createSignature(string key)
        {

          //  objectToSendToPartner = JsonConvert.DeserializeObject<CompleteVoteBookingReq_P>(JsonConvert.SerializeObject(this));

            string concatenatedReq = "";
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var property in typeof(CompleteVoteBookingReq_P).GetProperties())
            {
                if (property.Name.ToLower() != "signature")
                {
                    properties.Add(property);
                }
                
            }

            var propertiesList = properties.OrderBy(S => S.Name.ToLower());

            foreach (var item in propertiesList)
            {
                if (item.GetValue(this) != null)
                {
                    concatenatedReq = concatenatedReq + item.GetValue(this).ToString();
                }
            }
            this.signature = HMACSHA512(concatenatedReq, key);
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
