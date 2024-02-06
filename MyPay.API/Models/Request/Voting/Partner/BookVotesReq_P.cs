//using iText.Html2pdf.Attach.Impl.Layout.Form.Renderer;
using MyPay.API.Models.Response.Voting.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Voting.Partner

{
    public class BookVotesReq_P
    {
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerMobile { get; set; }
        public string contestId { get; set; }
        public string subContestId { get; set; }
        public string contestantId { get; set; }
        public string pricePerVote { get; set; }
        public string totalVote { get; set; }
        public string totalAmount { get; set; }
       // public string isPaidVote { get ; set; }

        private string _isPaidVote;

        public string isPaidVote
        {
            get {
                if (totalAmount == "0" || totalAmount is null)
                {
                    return "false";
                }
                return "true";
            }
            set { _isPaidVote = value; }
        }

        // public string isPackageUsed { get; set; }

        private string _isPackageUsed;

        public string isPackageUsed
        {
            get
            {
                if (packageId == "0" || packageId is null)
                {
                    return "false";
                }
                return "true";
            }
            set { _isPackageUsed = value; }
        }
        public string packageId { get; set; } = "0";
        public string remarks { get; set; } = "";
        public string serviceCharge { get; set; } = "0";
        public string couponCode { get; set; } = "";
        public string signature { get; set; }


        public void createSignature(string key)
        {
            string concatenatedReq = "";
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var property in typeof(BookVotesReq_P).GetProperties())
            {
                properties.Add(property);
            }

           var propertiesList = properties.OrderBy(S => S.Name.ToLower());

            foreach (var item in propertiesList) {
                if(item.GetValue(this) != null)
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
