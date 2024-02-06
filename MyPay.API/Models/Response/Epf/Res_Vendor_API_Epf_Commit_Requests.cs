using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Events
{
    public class Res_Vendor_API_Epf_Commit_Requests : CommonResponse
    {
        public Data _Data { get; set; }
        private string responseDescription;
        public string ResponseDescription
        {
            get => responseDescription;
            set => responseDescription = value;
        }
        private string responseCode;
        public string ResponseCode
        {
            get => responseCode;
            set => responseCode = value;
        }

        public class Data
        {
            private DateTime datetime;
            public DateTime Datetime
            {
                get => datetime;
                set => datetime = value;
            }
            private string code;
            public string Code
            {
                get => code;
                set => code = value;
            }
            private string description;
            public string Description
            {
                get => description;
                set => description = value;
            }
            private List<DepositDetail> depositDetail;
            public List<DepositDetail> DepositDetail
            {
                get => depositDetail;
                set => depositDetail = value;
            }
            private object instructionId;
            public object InstructionId
            {
                get => instructionId;
                set => instructionId = value;
            }
            private List<object> contributors;
            public List<object> Contributors
            {
                get => contributors;
                set => contributors = value;
            }
            private string version;
            public string Version
            {
                get => version;
                set => version = value;
            }
            private string timestamp;
            public string Timestamp
            {
                get => timestamp;
                set => timestamp = value;
            }
            private string status;
            public string Status
            {
                get => status;
                set => status = value;
            }
        }

        public class DepositDetail
        {
            private string depositDesc;
            public string DepositDesc
            {
                get => depositDesc;
                set => depositDesc = value;
            }
            private string depositType;
            public string DepositType
            {
                get => depositType;
                set => depositType = value;
            }
        }
    }
}