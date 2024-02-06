using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Security.Policy;

namespace MyPay.API.Controllers
{
    public class GenerateNCHLTokenController : ApiController
    {
        [HttpPost]
        [Route("api/createNCHLHash")]

        public HttpResponseMessage createNCHLHash(HashRequest req)
        {
            var hash = new HashResult();

            string result = GenerateConnectIPSToken(req.Text);
           
                hash.Hash = result;
                var response = Request.CreateResponse<HashResult>(System.Net.HttpStatusCode.OK, hash);
                return response;
            
        }


        internal string GenerateConnectIPSToken(string stringToHash)
        {
            try
            {
                if (Common.ApplicationEnvironment.IsProduction)
                {
                    X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile), RepNCHL.PFXPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                    RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;

                    RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
                    privateKey1.ImportParameters(privateKey.ExportParameters(true));
                    byte[] data = Encoding.UTF8.GetBytes(stringToHash);
                    byte[] signature = privateKey1.SignData(data, "SHA256");
                    string signaturresult = Convert.ToBase64String(signature);
                    return signaturresult;

                }
                else {
                    X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile_LinkBank), "123", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                    RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;

                    RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
                    privateKey1.ImportParameters(privateKey.ExportParameters(true));
                    byte[] data = Encoding.UTF8.GetBytes(stringToHash);
                    byte[] signature = privateKey1.SignData(data, "SHA256");
                    string signaturresult = Convert.ToBase64String(signature);
                    return signaturresult;
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName);
                return string.Empty;
            }
        }
    }

    public class HashResult { 
        public string Hash { get; set; }
        public bool result { get; set; }
    }

    public class HashRequest { 
        public string Text { get; set; }
    }
}
