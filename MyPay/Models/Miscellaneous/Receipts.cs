using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace MyPay.Models.Miscellaneous
{
    public class Receipts
    {
        public Int64 Id = 0;
        //public Int64 serviceId =0;
        public Int64 memberId = 0;
        //public string WalletTransactionId;
        public DateTime createdDate;
        public string table1JSONContent;
        public string contactNumber;
        public string fullname;
        public string TxnID;
        public string TxnType;
        public string PaidFor;
        public decimal Amount;
    }
}
