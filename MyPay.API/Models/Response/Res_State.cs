using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.State
{
    public class Res_State : CommonResponse
    {
        //AddState
        private List<AddState> _data = new List<AddState>();
        public List<AddState> data
        {
            get { return _data; }
            set { _data = value; }
        }

    }
}