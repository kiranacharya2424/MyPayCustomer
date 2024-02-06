using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddAirlineCommission : CommonAdd
    {
        #region "Enums"

        public enum CommissionStatus
        {
            Success = 1,
            Failure = 2,
            Cancelled = 3
        }

        public enum CommissionType
        {
            Service = 1,
            Other = 2
        }

        public enum KycTypes
        {
            All = 0,
            Verified = 1 
        }

        public enum GenderTypes
        {
            All = 0,
            Male = 1,
            Female = 2,
            Other=3
        }

        public enum ScheduleStatuses
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }

        #endregion
        #region "Properties"
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Kyc Type")]
        private int _KycType = 0;
        public int KycType
        {
            get { return _KycType; }
            set { _KycType = value; }
        }
         
        private string  _KycTypeName = string.Empty;
        public string KycTypeName
        {
            get { return _KycTypeName; }
            set { _KycTypeName = value; }
        }
        private KycTypes _KycTypeEnum = 0;
        public KycTypes KycTypeEnum
        {
            get { return _KycTypeEnum; }
            set { _KycTypeEnum = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type")]
        private int _GenderType = 0;
        public int GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type Name")]
        private string _GenderTypeName = string.Empty;
        public string GenderTypeName
        {
            get { return _GenderTypeName; }
            set { _GenderTypeName = value; }
        }
        private GenderTypes _GenderTypeEnum = 0;
        public GenderTypes GenderTypeEnum
        {
            get { return _GenderTypeEnum; }
            set { _GenderTypeEnum = value; }
        }
        private int _FromSectorId = 0;
        public int FromSectorId
        {
            get { return _FromSectorId; }
            set { _FromSectorId = value; }
        }
        private string _FromSectorName = string.Empty;
        public string FromSectorName
        {
            get { return _FromSectorName; }
            set { _FromSectorName = value; }
        }
        private int _ToSectorId = 0;
        public int ToSectorId
        {
            get { return _ToSectorId; }
            set { _ToSectorId = value; }
        }
        private string _ToSectorName = string.Empty;
        public string ToSectorName
        {
            get { return _ToSectorName; }
            set { _ToSectorName = value; }
        }
        private int _AirlineId = 0;
        public int AirlineId
        {
            get { return _AirlineId; }
            set { _AirlineId = value; }
        }
        private string _AirlineName = string.Empty;
        public string AirlineName
        {
            get { return _AirlineName; }
            set { _AirlineName = value; }
        }
        private int _AirlineClassId = 0;
        public int AirlineClassId
        {
            get { return _AirlineClassId; }
            set { _AirlineClassId = value; }
        }
        private string _ClassName = string.Empty;
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        //FixedCommission
        [Required(ErrorMessage = "Fixed Commission  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Fixed Commission value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Fixed Amount")]
        private decimal _FixedCommission = 0;
        public decimal FixedCommission
        {
            get { return _FixedCommission; }
            set { _FixedCommission = value; }
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
        //PercentageCommission
        [Required(ErrorMessage = "Percentage  Commission  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Percentage Commission value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Cashback Percentage")]
        private decimal _Cashback_Percentage = 0;
        public decimal Cashback_Percentage
        {
            get { return _Cashback_Percentage; }
            set { _Cashback_Percentage = value; }
        }
        //PercentageRewardPoints
        [Required(ErrorMessage = "Percentage  RewardPoints  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter MP-Coins(%) Credit value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage RewardPoints")]
        private decimal _MPCoinsCredit = 0;
        public decimal MPCoinsCredit
        {
            get { return _MPCoinsCredit; }
            set { _MPCoinsCredit = value; }
        }
        //PercentageRewardPointsDebit
        [Required(ErrorMessage = "Percentage  RewardPoints  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter MP-Coins(%) Debit value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage RewardPoints")]
        private decimal _MPCoinsDebit = 0;
        public decimal MPCoinsDebit
        {
            get { return _MPCoinsDebit; }
            set { _MPCoinsDebit = value; }
        }
        //MaximumAllowed
        [Required(ErrorMessage = "Maximum Allowed Amount is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Maximum Allowed value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Max Allowed Amount")]
        private decimal _MaximumCashbackAllowed = 0;
        public decimal MaximumCashbackAllowed
        {
            get { return _MaximumCashbackAllowed; }
            set { _MaximumCashbackAllowed = value; }
        }

        //MinimumAllowed
        [Required(ErrorMessage = "Minimum Allowed Amount is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Minimum Allowed value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Min Allowed Amount")]
        private decimal _MinimumCashbackAllowed = 0;
        public decimal MinimumCashbackAllowed
        {
            get { return _MinimumCashbackAllowed; }
            set { _MinimumCashbackAllowed = value; }
        }
        //ServiceCharge
        [Required(ErrorMessage = "Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Service Charge")]
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        //ServiceCharge
        [Required(ErrorMessage = "Minimum Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Min Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Minimum Service Charge")]
        private decimal _MinServiceCharge = 0;
        public decimal MinServiceCharge
        {
            get { return _MinServiceCharge; }
            set { _MinServiceCharge = value; }
        }
        //ServiceCharge
        [Required(ErrorMessage = "Maximum Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Max Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Maximum Service Charge")]
        private decimal _MaxServiceCharge = 0;
        public decimal MaxServiceCharge
        {
            get { return _MaxServiceCharge; }
            set { _MaxServiceCharge = value; }
        }
        //IsServiceChargeFlat
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Service Charge Flat")]
        private bool _IsCashbackPerTicket = false;
        public bool IsCashbackPerTicket
        {
            get { return _IsCashbackPerTicket; }
            set { _IsCashbackPerTicket = value; }
        }
        
        //FromDate
        [Required(ErrorMessage = "From Date is  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter From Date")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "From Date")]
        private DateTime _FromDate = System.DateTime.UtcNow;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        //ToDate
        [Required(ErrorMessage = "To Date is  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter To Date")]
        [Display(Name = "To Date")]
        private DateTime _ToDate = System.DateTime.UtcNow;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        //ScheduleStatus
        private string _ScheduleStatus = String.Empty;
        public string ScheduleStatus
        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }
    
        //ServiceName
        private string _ServiceName = String.Empty;
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }
        private string _FromDateDT = string.Empty;
        public string FromDateDT
        {
            get { return _FromDateDT; }
            set { _FromDateDT = value; }
        }
        private string _ToDateDT = string.Empty;
        public string ToDateDT
        {
            get { return _ToDateDT; }
            set { _ToDateDT = value; }
        }
        private string _UpdateIndiaDate = string.Empty;
        public string UpdateIndiaDate
        {
            get { return _UpdateIndiaDate; }
            set { _UpdateIndiaDate = value; }
        }
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //EnumScheduleStatus
        private ScheduleStatuses _EnumScheduleStatus = 0;
        public ScheduleStatuses EnumScheduleStatus
        {
            get { return _EnumScheduleStatus; }
            set { _EnumScheduleStatus = value; }
        }
        #endregion
        public CommonDBResonse AddAirline()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_AddAirlineCommision", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("FromSectorId", FromSectorId);
            HT.Add("ToSectorId", ToSectorId);
            HT.Add("AirlineId", AirlineId);
            HT.Add("AirlineClassId", AirlineClassId);
            HT.Add("IsActive", IsActive);
            HT.Add("IsDeleted", IsDeleted);
            HT.Add("Cashback_Percentage", Cashback_Percentage);
            HT.Add("MPCoinsDebit", MPCoinsDebit);
            HT.Add("MPCoinsCredit", MPCoinsCredit);
            HT.Add("MinimumCashbackAllowed", MinimumCashbackAllowed);
            HT.Add("MaximumCashbackAllowed", MaximumCashbackAllowed);
            HT.Add("ServiceCharge", ServiceCharge);
            HT.Add("MinServiceCharge", MinServiceCharge);
            HT.Add("MaxServiceCharge", MaxServiceCharge);
            HT.Add("IsCashbackPerTicket", IsCashbackPerTicket);
            HT.Add("GenderType", GenderType);
            HT.Add("KycType", KycType);
            HT.Add("FromDate", FromDate);
            HT.Add("ToDate", ToDate);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("Flag", "I");     
            return HT;
        }
        #region GetMethods

        public float CountCommissionCheck(Int64 Id)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(Id) from Airline_Commissions with(nolock) where Id != '" + Id.ToString() + " and IsActive=1 and IsDeleted=0 and cast(FromDate as date) < cast(getdate() as date )  and cast(ToDate as date) > cast(getdate() as date ) ";
                //string str = "select count(Id) from Commission where Id != '" + Id.ToString() + "' and  ServiceId='" + serviceId + "'  and ((MinimumAmount between " + MinimumAmount + " and " + MaximumAmount + " ) or ((maximumamount between " + MinimumAmount + " and " + MaximumAmount + " ) and IsActive=1 and IsDeleted=0))";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (float.Parse(Result));
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public List<(string, string)> CountAirlineClass(Int64 AirlineId)
        {
            int data = 0;
            List<(string,string)> aaa = new List<(string, string)>();

            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(Class) from Airline_Class with(nolock) where AirlineId = '" + AirlineId.ToString() + "'";
                string list = "select Class, Id as ClassId from Airline_Class with(nolock) where AirlineId = '" + AirlineId.ToString() + "' ";
                Result = obj.GetScalarValueWithValue(str);
                aaa = obj.GetAirlineScalarValueWithValue(list);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (int.Parse(Result));
                }
            }
            catch (Exception ex)
            {

            }
            return aaa;
        }
        #endregion
    }
}