
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using Newtonsoft.Json;

namespace MyPay
{
    public static class VotingAPISetting_API
    {
        public static string voting_BaseURL { get; set; }

        public static string key { get; set; }

        public static string user { get; set; }

        public static string pass { get; set; }
    }
}