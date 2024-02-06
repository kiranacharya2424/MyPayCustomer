using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.Get
{
    public class GetApiFlightSwitchSettings : CommonGet
    {
        public int FlightSwitchType { get; set; }
        public int IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public long UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
    }
}
