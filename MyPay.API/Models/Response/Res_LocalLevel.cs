using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;

namespace MyPay.API.Models
{
    public class Res_LocalLevel : CommonResponse
    {
        //Res_LocalLevel
        private List<AddLocalLevel> _data = new List<AddLocalLevel>();
        public List<AddLocalLevel> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
