using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Demat
{
    public class Res_Vendor_API_Demat_Requests : CommonResponse
    {
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }


        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // state
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }


        // detailsstring
        private string _detail;
        public string detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
        //Extradata
        private List<DematExtra_Data> _Extrdata = new List<DematExtra_Data>();
        public List<DematExtra_Data> extra_Data
        {

            get { return _Extrdata; }

            set { _Extrdata = value; }
        }

    }
    public class DematExtra_Data
    {


    }
}
