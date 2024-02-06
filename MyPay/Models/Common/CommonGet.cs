using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Common
{
    public class CommonGet
    {
        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }

        //CheckDelete
        private int _CheckDelete = 2;// (int)clsData.BooleanValue.Both;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }

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
         
    }
}