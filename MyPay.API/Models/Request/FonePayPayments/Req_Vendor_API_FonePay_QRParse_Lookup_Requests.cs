using System; 
namespace MyPay.API.Models
{
    public class Req_Vendor_API_FonePay_QRParse_Lookup_Requests : CommonProp
    {
        private string _qrRequestMessage = String.Empty;
        public string qrRequestMessage
        {
            get { return _qrRequestMessage; }
            set { _qrRequestMessage = value; }
        }
    }
}