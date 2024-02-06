using MyPay.API.Models.Request.Voting.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.NPS.Consumer
{
    public class NCHLConsumerLodgeReq: ICommonVotingProps
    {
        public string chitNo { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string fiscalYear { get; set; }
        public double amount { get; set; } = 0.0;
        public string refID { get; set; }
        public string billNo { get; set; }
        public string PANNo { get; set; }
        public string Name { get; set; }
        public string remarks { get; set; }
        public int vendorType { get; set; }
        public int requestNo { get; set; }
        public string branchID { get; set; }
        public string paymentType { get; set; }
        public string policyNo { get; set; }
        public string bob { get; set; }
        public string passportNo { get; set; }
        public string lotNo { get; set; }
        public string serviceProvider { get; set; }
        public string contactNo { get; set; }
        public string voucherNo { get; set; }
        public string loanType { get; set; }
        public string contributorName { get; set; }
        public string ucin { get; set; }
        public string submissionNo { get; set; }
        public string employerNo { get; set; }
        public string organizationName { get; set; }
        public string transactionID { get; set; }
        public string officeCode { get; set; }
        public string oldSchemeNo { get; set; }
        public string newSchemeNo { get; set; }
        public string citPentionNo { get; set; }
        public string ebpNo { get; set; }
        public int vendorAPIType { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceId { get; set; }
        public string Version { get; set; }
        public string PlatForm { get; set; }
        public long TimeStamp { get; set; }
        public string Token { get; set; }
        public string UniqueCustomerId { get; set; }
        public string SecretKey { get; set; }
        public string Mpin { get; set; }
        public string Hash { get; set; }
        public string MemberId { get; set; }
        //"Amount":"1000","Code":"BOK-77-APP-1","BankName":"Bank Of Kathmandu Credit Card - NPR","CardNumber":"0000000000000000","Name":"theone Sharma","ServiceCharge":"10.00",
        public string Code { get; set; }
        public string BankName { get; set; }
            public string CardNumber { get; set; }
        public double Amount { get; set; } = 0.0;

        //public string CardType { get; set; }
    }


}
