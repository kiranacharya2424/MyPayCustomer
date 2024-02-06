using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Epf_Contributor_Detail_Requests : CommonProp
    {
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        public CipsBatchContributorDetail CipsBatch { get; set; }
        public CipsTransactionContributorDetail CipsTransaction { get; set; }
        public class CipsBatchContributorDetail
        {

            private string _BatchId = string.Empty;
            public string BatchId
            {
                get => _BatchId;
                set => _BatchId = value;
            }
            private int _BatchAmount = 0;
            public int BatchAmount
            {
                get => _BatchAmount;
                set => _BatchAmount = value;
            }
            private int _BatchCount = 0;
            public int BatchCount
            {
                get => _BatchCount;
                set => _BatchCount = value;
            }
            private string _BatchCrncy = string.Empty;
            public string BatchCrncy
            {
                get => _BatchCrncy;
                set => _BatchCrncy = value;
            }
            private string _CategoryPurpose = string.Empty;
            public string CategoryPurpose
            {
                get => _CategoryPurpose;
                set => _CategoryPurpose = value;
            }
            private string _DebtorAgent = string.Empty;
            public string DebtorAgent
            {
                get => _DebtorAgent;
                set => _DebtorAgent = value;
            }
            private string _DebtorBranch = string.Empty;
            public string DebtorBranch
            {
                get => _DebtorBranch;
                set => _DebtorBranch = value;
            }
            private string _DebtorName = string.Empty;
            public string DebtorName
            {
                get => _DebtorName;
                set => _DebtorName = value;
            }
            private string _DebtorAccount = string.Empty;
            public string DebtorAccount
            {
                get => _DebtorAccount;
                set => _DebtorAccount = value;
            }
            private string _DebtorIdType = string.Empty;
            public string DebtorIdType
            {
                get => _DebtorIdType;
                set => _DebtorIdType = value;
            }
            private string _DebtorIdValue = string.Empty;
            public string DebtorIdValue
            {
                get => _DebtorIdValue;
                set => _DebtorIdValue = value;
            }
            private string _DebtorAddress = string.Empty;
            public string DebtorAddress
            {
                get => _DebtorAddress;
                set => _DebtorAddress = value;
            }
            private string _DebtorPhone = string.Empty;
            public string DebtorPhone
            {
                get => _DebtorPhone;
                set => _DebtorPhone = value;
            }
            private string _DebtorMobile = string.Empty;
            public string DebtorMobile
            {
                get => _DebtorMobile;
                set => _DebtorMobile = value;
            }
            private string _DebtorEmail = string.Empty;
            public string DebtorEmail
            {
                get => _DebtorEmail;
                set => _DebtorEmail = value;
            }
        }
        public class CipsTransactionContributorDetail
        {

            private string _InstructionId = string.Empty;
            public string InstructionId
            {
                get => _InstructionId;
                set => _InstructionId = value;
            }
            private string _EndToEndId = string.Empty;
            public string EndToEndId
            {
                get => _EndToEndId;
                set => _EndToEndId = value;
            }
            private int _Amount = 0;
            public int Amount
            {
                get => _Amount;
                set => _Amount = value;
            }
            private string _AppId = string.Empty;
            public string AppId
            {
                get => _AppId;
                set => _AppId = value;
            }
            private string _RefId = string.Empty;
            public string RefId
            {
                get => _RefId;
                set => _RefId = value;
            }
            private string _FreeText1 = string.Empty;
            public string FreeText1
            {
                get => _FreeText1;
                set => _FreeText1 = value;
            }
            private string _Addenda3 = string.Empty;
            public string Addenda3
            {
                get => _Addenda3;
                set => _Addenda3 = value;
            }
        }

    }
}