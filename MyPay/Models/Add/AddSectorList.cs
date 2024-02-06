using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddAirlinesList
    {
        #region "Properties"
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _AirlineName = string.Empty;
        public string AirlineName
        {
            get { return _AirlineName; }
            set { _AirlineName = value; }
        }
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        
        #endregion


    }
}