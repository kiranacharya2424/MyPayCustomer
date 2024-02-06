using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddMarque : CommonAdd
    {


        public enum enumMarqueFor
        {   
            All = 1,
            Home =2

        }
        #region "Properties"

        //Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //MarqueFor
        private enumMarqueFor _EnumMarqueFor = 0;

        public enumMarqueFor EnumMarqueFor
        {
            get { return _EnumMarqueFor; }
            set { _EnumMarqueFor = value; }
        }
        private int _MarqueFor = 1;
        public int MarqueFor
        {
            get { return _MarqueFor; }
            set { _MarqueFor = value; }
        }
        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private int _Priority = 0;
           
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        //private bool _IsActive = false;
        //public bool IsActive
        //{
        //    get { return _IsActive; }
        //    set { _IsActive = value; }
        //}
        //UpdatedByName
        private string _UpdatedByName = string.Empty;
        public string UpdatedByName
        {
            get { return _UpdatedByName; }
            set { _UpdatedByName = value; }
        }

        private string _Link = string.Empty;
        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        //UpdatedDateDt
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        #endregion
    }
}