using MyPay.Models.Common;

using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;

using System.Web;

namespace MyPay.Models.Add

{

    #region "Enums"
    public enum NRBReportsType
    {
        Annexture10_1_5 = 5,
        Annexture10_1_6 = 6,
        Annexture10_1_8 = 8,
    }
    #endregion
    #region "Properties"
    public class AddNRBReports : CommonAdd
    {

        private NRBReportsType _TypeEnum = 0;
        public NRBReportsType TypeEnum
        {
            get { return _TypeEnum; }
            set { _TypeEnum = value; }
        }
        private int _TypeEnumValue = 0;
        public int TypeEnumValue
        {
            get { return _TypeEnumValue; }
            set { _TypeEnumValue = value; }
        }
        private string _FormOfTransaction = string.Empty;
        public string FormOfTransaction { get { return _FormOfTransaction; } set { _FormOfTransaction = value; } }


        private Int64 _ZeroToThousandCount = 0;
        public Int64 ZeroToThousandCount { get { return _ZeroToThousandCount; } set { _ZeroToThousandCount = value; } }


        private decimal _ZeroToThousandSum = 0;
        public decimal ZeroToThousandSum { get { return _ZeroToThousandSum; } set { _ZeroToThousandSum = value; } }


        private Int64 _ThousandToFiveThousandCount = 0;
        public Int64 ThousandToFiveThousandCount { get { return _ThousandToFiveThousandCount; } set { _ThousandToFiveThousandCount = value; } }


        private decimal _ThousandToFiveThousandSum = 0;
        public decimal ThousandToFiveThousandSum { get { return _ThousandToFiveThousandSum; } set { _ThousandToFiveThousandSum = value; } }


        private Int64 _FiveThousandToTenThousandCount = 0;
        public Int64 FiveThousandToTenThousandCount { get { return _FiveThousandToTenThousandCount; } set { _FiveThousandToTenThousandCount = value; } }
        
        
        private decimal _FiveThousandToTenThousandSum = 0;
        public decimal FiveThousandToTenThousandSum { get { return _FiveThousandToTenThousandSum; } set { _FiveThousandToTenThousandSum = value; } }
        
        
        private Int64 _TenThousandToTwentyThousandCount = 0;
        public Int64 TenThousandToTwentyThousandCount { get { return _TenThousandToTwentyThousandCount; } set { _TenThousandToTwentyThousandCount = value; } }
       
        
        private decimal _TenThousandToTwentyThousandSum = 0;
        public decimal TenThousandToTwentyThousandSum { get { return _TenThousandToTwentyThousandSum; } set { _TenThousandToTwentyThousandSum = value; } }


        private Int64 _TwentyThousandToTwentyFiveThousandCount = 0;
        public Int64  TwentyThousandToTwentyFiveThousandCount { get { return _TwentyThousandToTwentyFiveThousandCount; } set { _TwentyThousandToTwentyFiveThousandCount = value; } }


        private decimal _TwentyThousandToTwentyFiveThousandSum = 0;
        public decimal TwentyThousandToTwentyFiveThousandSum { get { return _TwentyThousandToTwentyFiveThousandSum; } set { _TwentyThousandToTwentyFiveThousandSum = value; } }


        private Int64 _TwentyFiveThousandAboveCount = 0;
        public Int64 TwentyFiveThousandAboveCount { get { return _TwentyFiveThousandAboveCount; } set { _TwentyFiveThousandAboveCount = value; } }
       
        
        private decimal _TwentyFiveThousandAboveSum = 0;
        public decimal TwentyFiveThousandAboveSum { get { return _TwentyFiveThousandAboveSum; } set { _TwentyFiveThousandAboveSum = value; } }


        private Int64 _Success = 0;
        public Int64 Success { get { return _Success; } set { _Success = value; } }


        private Int64 _Failed = 0;
        public Int64 Failed { get { return _Failed; } set { _Failed = value; } }


        private string _Wallet = string.Empty;
        public string Wallet { get { return _Wallet; } set { _Wallet = value; } }


        private string _Gender = string.Empty;
        public string Gender { get { return _Gender; } set { _Gender = value; } }


        private Int64 _TotalNumber = 0;
        public Int64 TotalNumber { get { return _TotalNumber; } set { _TotalNumber = value; } }


        private decimal _TotalWalletSum = 0;
        public decimal TotalWalletSum { get { return _TotalWalletSum; } set { _TotalWalletSum = value; } }


    }

    #endregion
}