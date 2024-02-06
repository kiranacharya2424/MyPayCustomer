using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_ConnectIPSLookupBatch:CommonProp
    {
        //BATCHID 
        private string _BATCHID = string.Empty;
        public string BATCHID
        {
            get { return _BATCHID; }
            set { _BATCHID = value; }
        }     
         
        //INSTRUCTIONID
        private string _INSTRUCTIONID = string.Empty;
        public string INSTRUCTIONID
        {
            get { return _INSTRUCTIONID; }
            set { _INSTRUCTIONID = value; }
        }
        //MEMBERID
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
    }
}