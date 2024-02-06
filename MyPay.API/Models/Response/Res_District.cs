using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.District
{
    public class Res_District : CommonResponse
    {

        //Res_District
        private List<AddDistrict> _data = new List<AddDistrict>();
        public List<AddDistrict> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}