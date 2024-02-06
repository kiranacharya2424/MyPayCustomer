using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class FlightSwitchController : BaseAdminSessionController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            FlightSwitchSetting objflightSwitchSetting = new FlightSwitchSetting();
            using (var db = new MyPayEntities())
            {
                objflightSwitchSetting = db.FlightSwitchSettings.FirstOrDefault();
            }
            ViewBag.FlightSwitchType = objflightSwitchSetting.FlightSwitchType;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(string FlightSwitchType)
        {
            int dbresult = 0;
            FlightSwitchSetting objflightSwitchSetting = new FlightSwitchSetting();
            using (var db = new MyPayEntities())
            {
                objflightSwitchSetting = db.FlightSwitchSettings.FirstOrDefault();
                objflightSwitchSetting.FlightSwitchType = Convert.ToInt32(FlightSwitchType);
                dbresult = db.SaveChanges();

                if (dbresult == 1)
                {
                    AddApiFlightSwitchSettings outobject = new AddApiFlightSwitchSettings();
                    outobject.FlightSwitchType = Convert.ToInt32(FlightSwitchType);
                    outobject.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    outobject.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    outobject.CreatedByName = Session["AdminUserName"].ToString();
                    outobject.UpdatedByName = Session["AdminUserName"].ToString();

                    Int64 Id = RepCRUD<AddApiFlightSwitchSettings, GetApiFlightSwitchSettings>.Insert(outobject, "FlightSwitchSettings");
                    Common.AddLogs("Updated Flight Settings by(AdminMemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Maintenance, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());

                    ViewBag.SuccessMessage = "Updated Successfully";
                }
                else
                {
                    ViewBag.Message = "Already Exists.";
                }
            }
            ViewBag.FlightSwitchType = objflightSwitchSetting.FlightSwitchType;
            return View();
        }
    }
}
