using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddTicketCategory:CommonAdd
    {
        #region "Properties"
        
        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //LogoImage
        private string _LogoImage = string.Empty;
        public string LogoImage
        {
            get { return _LogoImage; }
            set { _LogoImage = value; }
        }

        //CategoryName
        private string _CategoryName = string.Empty;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        //Position
        private int _Position = 0;
        public int Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        #endregion
    }
}