using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPay.Models.VendorAPI.Get.Insurance.IMEGeneral;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_Commit_Insurance_IMEGeneral_Requests : CommonProp
    {
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        // CustomerCode
        private string _CustomerCode = string.Empty;
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        //SessionId
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private string _PolicyType = string.Empty;
        public string PolicyType
        {
            get { return _PolicyType; }
            set { _PolicyType = value; }
        }

        private string _InsuranceType = string.Empty;
        public string InsuranceType
        {
            get { return _InsuranceType; }
            set { _InsuranceType = value; }
        }


        private string _Branch = string.Empty;
        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }


        private string _FullName = string.Empty;
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }


        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }


        private string _MobileNumber = string.Empty;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }


        private string _PolicyDescription = string.Empty;
        public string PolicyDescription
        {
            get { return _PolicyDescription; }
            set { _PolicyDescription = value; }
        }


        private string _DebitNoteNo = string.Empty;
        public string DebitNoteNo
        {
            get { return _DebitNoteNo; }
            set { _DebitNoteNo = value; }
        }


        private string _BillNo = string.Empty;
        public string BillNo
        {
            get { return _BillNo; }
            set { _BillNo = value; }
        }


        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //InsuranceSlug
        private string _InsuranceSlug = string.Empty;
        public string InsuranceSlug
        {
            get { return _InsuranceSlug; }
            set { _InsuranceSlug = value; }
        }
    }
}
