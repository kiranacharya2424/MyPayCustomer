﻿using MyPay.Models.Add;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_GetCipsLookupResponse: CommonResponse
    {
        //UserBankDetail
        private CipsBatchDetail _data = new CipsBatchDetail();
        public CipsBatchDetail data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}