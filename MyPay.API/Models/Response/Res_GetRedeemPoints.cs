using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_GetRedeemPoints:CommonResponse
    {
        //TotalRewardPoints
        private string _TotalRewardPoints = string.Empty;
        public string TotalRewardPoints
        {
            get { return _TotalRewardPoints; }
            set { _TotalRewardPoints = value; }
        }

        // RedeemPoints
        private List<AddRedeemPoints> _data = new List<AddRedeemPoints>();
        public List<AddRedeemPoints> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}