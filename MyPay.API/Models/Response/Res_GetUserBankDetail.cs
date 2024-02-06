using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_GetUserBankDetail: CommonResponse
    {
        //UserBankDetail
        private List<AddUserBankDetail> _data = new List<AddUserBankDetail>();
        public List<AddUserBankDetail> data
        {
            get { return _data; }
            set { _data = value; }
        }

        //BankTransferType
        private int _BankTransferType = (int)AddUserBankDetail.UserBankType.NPS;
        public int BankTransferType
        {
            get { return _BankTransferType; }
            set { _BankTransferType = value; }
        }

        //IsEmailVerified
        private bool _IsEmailVerified = false;
        public bool IsEmailVerified
        {
            get { return _IsEmailVerified; }
            set { _IsEmailVerified = value; }
        }

    }
}