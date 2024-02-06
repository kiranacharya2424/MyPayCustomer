using MyPay.API.Models.Request.Voting.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.NPS.Consumer
{
    public class NCHLConsumerConfirmPayment: ICommonVotingProps
    {
        public string instructionId { get; set; }

        public string paymentMethodId { get; set; }
        public int vendorAPIType { get; set; }
        public string UniqueCustomerId { get; set; }
        public string SecretKey { get; set; }
        public string Mpin { get; set; }
        public string Hash { get; set; }
        public string MemberId { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceId { get; set; }
        public string Version { get; set; }
        public string PlatForm { get; set; }
        public long TimeStamp { get; set; }
        public string Token { get; set; }
        public decimal Amount { get; set; }

        public string PaymentMode { get; set; }



        public string BankTransactionID { get; set; }

        public string Reference { get; set; }

        public string transactionId { get; set; }
    }


}
