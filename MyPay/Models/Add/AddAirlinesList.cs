using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddSectorList
    {
        #region "Properties"
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _SectorCode = string.Empty;
        public string SectorCode
        {
            get { return _SectorCode; }
            set { _SectorCode = value; }
        }
        private string _SectorName = string.Empty;
        public string SectorName
        {
            get { return _SectorName; }
            set { _SectorName = value; }
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