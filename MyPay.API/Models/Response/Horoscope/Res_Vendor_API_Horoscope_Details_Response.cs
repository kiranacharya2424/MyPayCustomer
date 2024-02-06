using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Horoscope
{
    public class Res_Vendor_API_Horoscope_Details_Response : CommonResponse
    {
        public class NepaliDateDetails
        {
            public string english_day_value { get; set; }
            public string english_day_name { get; set; }
            public string english_month_value { get; set; }
            public string english_month_name { get; set; }
            public string english_year { get; set; }
            public string nepali_day_value { get; set; }
            public string nepali_day_name { get; set; }
            public string nepali_month_value { get; set; }
            public string nepali_month_value_string { get; set; }
            public string nepali_month_name { get; set; }
            public string nepali_year { get; set; }
            public string nepali_year_value { get; set; }
            public string english_date { get; set; }
            public string nepali_date { get; set; }
            public string english_long_date { get; set; }
            public string nepali_long_date { get; set; }
        }

        public class CurrentWeekRange
        {
            public DateTime weekStartDateEnglish { get; set; }
            public DateTime weekEndDateEnglish { get; set; }
            public string weekStartDateNepali { get; set; }
            public string weekEndDateNepali { get; set; }
            public string weekRangeEnglish { get; set; }
            public string weekRangeNepali { get; set; }
        }

            public string userName { get; set; }
            public string selectedLanguageCode { get; set; }
            public string selectedFrequency { get; set; }
            public bool isHoroscopeSelected { get; set; }
            public int selectedHoroscopeId { get; set; }
            public DateTime currentDate { get; set; }
            public NepaliDateDetails currentNepaliDateDetails { get; set; }
            public CurrentWeekRange currentWeekRange { get; set; }
            public object myHoroscope { get; set; }
            public object[] dailyHoroscopes { get; set; }
            public object weeklyHoroscopes { get; set; }
            public object monthlyEnglishHoroscopes { get; set; }
            public object monthlyNepaliHoroscopes { get; set; }
            public object yearlyEnglishHoroscopes { get; set; }
            public object yearlyNepaliHoroscopes { get; set; }
            public string Message { get; set; }
       
    }
}