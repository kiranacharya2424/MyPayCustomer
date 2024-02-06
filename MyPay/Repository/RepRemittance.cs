using MyPay.Models.Add;
using MyPay.Models.Get;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Net.Http.Formatting;
using ServiceStack;
using System.IdentityModel.Tokens.Jwt;
using MyPay.Models.Common;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper; 
using MyPay.Models.RemittanceAPI.Add;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace MyPay.Repository
{
    public class RepRemittance
    {

        public static Int64 GetNewRemittanceId()
        {
            Int64 Id = 0;
            MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
            string Result = commonHelpers.GetScalarValueWithValue("SELECT TOP 1 max(MerchantMemberId) FROM RemittanceUser with(nolock)");
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
    }
}