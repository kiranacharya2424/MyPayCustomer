using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.VotingDB
{
    public class VotingOrder
    {
        public long Id { get; set; }
        public long memberID { get; set; }
        public string memberName { get; set; }
        public string memberContactNumber { get; set; }
        public long contestID { get; set; }
        public int subcontestID { get; set; }
        public string deviceCode { get; set; }
        public long votingCandidateUniqueId { get; set; }
        public string votingCadidateName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal PricePerVote { get; set; }
        public int NoOfVotes { get; set; }
        public decimal amount { get; set; }
        public bool isPaidVote { get; set; }
        public bool isPackageUsed { get; set; }
        public long votingPackageID { get; set; }
        public decimal serviceCharge { get; set; }
        public string couponCode { get; set; }
        public long createdBy { get; set; }
        public string createdByName { get; set; }
        public DateTime createdDate { get; set; }
        public long UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool isDeleted { get; set; }
        public string platform { get; set; }
        public long orderId { get; set; }
        public string merchantID { get; set; }
        public string TransactionUniqueID { get; set; }
        public string paymentMethodId { get; set; }
    }
    
}
