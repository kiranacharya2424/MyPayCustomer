//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using MyPay.Models.Add;
//using Newtonsoft.Json;

//namespace MyPay.API.Models.Request.Voting.Partner
//{
//    public  class VotingAPISetting
//    {
//        public  string voting_BaseURL { get; set; }

//        public  string key { get; set; }

//        public  string user { get; set; }

//        public  string pass { get; set; }

//        public  string voting_ContestList { get; set; }

//        public  string voting_SubContestList { get; set; }

//        public  string voting_subContestDetails { get; set; }

//        public  string voting_validateCoupon { get; set; }

//        public  string voting_bookVotes { get; set; }

//        public  string voting_CompleteVotesBooking { get; set; }

//        public  string Voting_BaseURL_Staging { get; set; }

//        public  string Key_Staging { get; set; }

//        public  string User_Staging { get; set; }

//        public  string Pass_Staging { get; set; }
//        public VotingAPISetting loadSettings()
//        {
//            var settingsFile = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/voting.json"));
//            //System.IO.File.ReadAllText("\voting.json");
//            VotingAPISetting settings = Newtonsoft.Json.JsonConvert.DeserializeObject<VotingAPISetting>(settingsFile);

//            //string fullPath = System.Web.Hosting.HostingEnvironment.MapPath("~/voting.json");
//            //if (System.IO.File.Exists(fullPath))
//            //{
//            //    return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
//            //}
//            //else
//            //{
//            //    errorMessage = "";
//            //    return Json(new { fileName = "", errorMessage = errorMessage });
//            //}

//            return settings;
//        }
//    }
//}



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using Newtonsoft.Json;

namespace MyPay
{
    public static class VotingAPISetting
    {
        public static string voting_BaseURL { get; set; }

        public static string key { get; set; }

        public static string user { get; set; }

        public static string pass { get; set; }
    }
}
