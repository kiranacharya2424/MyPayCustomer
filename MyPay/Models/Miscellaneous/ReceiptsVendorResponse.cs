using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.Miscellaneous
{
    public class ReceiptsVendorResponse
    {
        public Int64 Id = 0;
        public Int64 serviceId =0;
        public Int64 memberId = 0;
       // public string WalletTransactionId;
        public DateTime CreatedDate;
        public string ReqJSONContent;
        public string ResJSONContent;
        public string ContactNumber;
        public string FullName;
        public string TxnID;
        public string TxnType;
        public string PaidFor;
        public decimal Amount;
        public string SessionId;
        public string ReferenceId;
        public string table1JSONContent;
        public string table2JSONContent;
    }

    public class  CableDownloadVendorResponse
    {
        public string ReferenceId { get; set; }
        public string UserName { get; set; }
        public string BasePriceRed { get; set; }
        public string TotalAmountRed { get; set; }
        public string VatTaxRed { get; set; }
        public string Message { get; set; }
        public Tickets Tickets { get; set; }


    }
    public class Tickets
    {


        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string QRCode { get; set; }
        public string BarCode { get; set; }
        public string ValidUntil { get; set; }
        public double Price { get; set; }
        public string TicketNumber { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public int CheckDelete { get; set; }
        public int CheckActive { get; set; }
        public int CheckApprovedByAdmin { get; set; }
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }

    public class MerchantDetail
    {

        public String MerchantUniqueId { get; set; }
        public String apiurl { get; set; }
        public string OrganizationName { get; set; }
        public string apiKey { get; set; }
        public string APIUser { get; set; }
        public string Password { get; set; }    
        public string MID { get; set; }
        public string API_Password { get; set; }
    }

    public class walletTransactionDetail
    {

        public string TransactionId { get; set; }
        public string CashBack { get; set; }
        public string ServiceCharge { get; set; }
       
    }
}

