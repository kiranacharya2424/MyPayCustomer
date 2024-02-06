using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_API_Get_Payments_Requests : CommonResponse
    {

        private List<AddUserSavedPayments> _data = new List<AddUserSavedPayments>();
        public List<AddUserSavedPayments> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}