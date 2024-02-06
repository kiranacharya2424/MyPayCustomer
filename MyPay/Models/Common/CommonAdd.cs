using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Common
{
    public class CommonAdd
    {

        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        //CreatedBy
        private Int64 _CreatedBy = 0;
        public Int64 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        //CreatedByName
        private string _CreatedByName = string.Empty;
        public string CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }

       
        //Sno
        private string _Sno = string.Empty;
        public string Sno
        {
            get { return _Sno; }
            set { _Sno = value; }
        }


        //CreatedDate
        private DateTime _CreatedDate = DateTime.UtcNow;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        //UpdatedDate
        private DateTime _UpdatedDate = DateTime.UtcNow;
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }
        //UpdatedBy
        private Int64 _UpdatedBy = 0;
        public Int64 UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }
        //UpdatedByName
        private string _UpdatedByName = string.Empty;
        public string UpdatedByName
        {
            get { return _UpdatedByName; }
            set { _UpdatedByName = value; }
        }

        //IsDeleted
        private bool _IsDeleted = false;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        //IsApprovedByAdmin
        private bool _IsApprovedByAdmin = true;
        public bool IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
        }

        //IsActive
        private bool _IsActive = true;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
     


    }
}