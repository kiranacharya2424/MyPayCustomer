using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetRoleOfMenus:CommonGet
    {
        #region "Properties"

        //RoleId
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //MenuId
        private int _MenuId = 0;
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }

        //CheckParentId
        private int _CheckParentId = -1;
        public int CheckParentId
        {
            get { return _CheckParentId; }
            set { _CheckParentId = value; }
        }

        #endregion

        public bool Delete()
        {
            bool DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MenuId", MenuId);
                HT.Add("ParentId", CheckParentId);
                HT.Add("RoleId", RoleId);
                HT.Add("IsDeleted", CheckDelete);
                HT.Add("IsActive", CheckActive);
                HT.Add("IsApprovedByAdmin", CheckApprovedByAdmin);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_MenusAssign_Delete", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
                //new ClsException(filename, "Delete", ex);
            }
            return DataRecieved;
        }
    }
}