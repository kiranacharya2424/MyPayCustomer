using MyPay.Models.Common;
using System;

namespace MyPay.Models.Add
{
    public class AddApiSettingsHistory : CommonAdd
    {
        //BankTransferType
        private Int32 _BankTransferType = 0;
        public Int32 BankTransferType

        {
            get { return _BankTransferType; }
            set { _BankTransferType = value; }
        }
        //MPCoinsDateSettings
        private DateTime _MPCoinsDateSettings = System.DateTime.UtcNow;
        public DateTime MPCoinsDateSettings

        {
            get { return _MPCoinsDateSettings; }
            set { _MPCoinsDateSettings = value; }
        }
        //MPCoinsDateSettingsDT
        private string _MPCoinsDateSettingsDT = string.Empty;
        public string MPCoinsDateSettingsDT

        {
            get { return _MPCoinsDateSettingsDT; }
            set { _MPCoinsDateSettingsDT = value; }
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
        //BankTransferType
        private Int32 _LinkBankTransferType = 0;
        public Int32 LinkBankTransferType

        {
            get { return _LinkBankTransferType; }
            set { _LinkBankTransferType = value; }
        }
    }
}