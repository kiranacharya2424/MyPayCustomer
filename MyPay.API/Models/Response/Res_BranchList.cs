using MyPay.Models.Add;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.State
{
    public class Res_BranchList : CommonResponse
    {
        //AddState
        private List<GetNCBranchList> _data = new List<GetNCBranchList>();
        public List<GetNCBranchList> data
        {
            get { return _data; }
            set { _data = value; }
        }

    }
}