using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPay.Controllers
{
    public class ReferShareLinkController : Controller
    {
        public ActionResult Index(string rfc)
        {
            if (!string.IsNullOrEmpty(rfc))
            {
                AddUser outobject = new AddUser();
                GetUser inobject = new GetUser();
                inobject.RefCode = rfc;
                AddUser res = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddShareReferLink outobjectShareLnk = new AddShareReferLink();
                    GetShareReferLink inobjectShareLnk = new GetShareReferLink();
                    inobjectShareLnk.RefCode = rfc;
                    inobjectShareLnk.CheckActive = 1;
                    inobjectShareLnk.CheckDelete = 0;
                    inobjectShareLnk.IPAddress = Common.GetUserIP();
                    inobjectShareLnk.CheckOpened = 0;
                    AddShareReferLink resShareReferLink = RepCRUD<GetShareReferLink, AddShareReferLink>.GetRecord("sp_ShareReferLink_Get", inobjectShareLnk, outobjectShareLnk);

                    if (resShareReferLink == null || resShareReferLink.Id == 0)
                    {
                        AddShareReferLink outobjectShareLnkNew = new AddShareReferLink();
                        outobjectShareLnkNew.Id = 0;
                        outobjectShareLnkNew.RefCode = rfc;
                        outobjectShareLnkNew.IPAddress = Common.GetUserIP();
                        outobjectShareLnkNew.IsDeleted = false;
                        outobjectShareLnkNew.IsActive = true;
                        outobjectShareLnkNew.IsOpened = false;
                        outobjectShareLnkNew.MemberId = res.MemberId;
                        outobjectShareLnkNew.PhoneNumber = res.ContactNumber;
                        outobjectShareLnkNew.CreatedDate = System.DateTime.UtcNow;
                        outobjectShareLnkNew.UpdatedDate = System.DateTime.UtcNow;
                        outobjectShareLnkNew.CreatedBy = Common.CreatedBy;
                        outobjectShareLnkNew.CreatedByName = Common.CreatedByName;
                        outobjectShareLnkNew.UpdatedBy = Common.CreatedBy;
                        //outobjectShareLnkNew.SharedLinkURL = Common.LiveSiteUrl + $"ReferShareLink?refcode={res.RefCode}&IPAddress={Common.GetUserIP()}&Platform={platform}";

                        Int64 Id = RepCRUD<AddShareReferLink, GetShareReferLink>.Insert(outobjectShareLnkNew, "sharereferlink");
                        var client = Request.ServerVariables["HTTP_USER_AGENT"];
                        if (Id > 0)
                        {
                            Common.AddLogs($"Refer link open from IPAddress: {Common.GetUserIP()} (RefCode:{rfc})", false, (int)AddLog.LogType.User);
                        }
                        //resShareReferLink.UpdatedDate = System.DateTime.UtcNow;
                        //resShareReferLink.IsOpened = true;
                        //bool IsUpdated = RepCRUD<AddShareReferLink, GetShareReferLink>.Update(resShareReferLink, "sharereferlink");
                        //if (IsUpdated)
                        //{
                        //    Common.AddLogs($"Refer link created successfully from IPAddress: {Common.GetUserIP()} (RefCode:{RefCode})", false, (int)AddLog.LogType.User);
                        //}
                    }
                    else
                    {
                        resShareReferLink.UpdatedDate = System.DateTime.UtcNow;
                        bool isUpdated = RepCRUD<AddShareReferLink, GetShareReferLink>.Update(resShareReferLink, "sharereferlink");
                        Common.AddLogs($"Refer link reopen from IPAddress: {Common.GetUserIP()} (RefCode:{rfc})", false, (int)AddLog.LogType.User);
                    }
                }
            }
            return View();
        }
    }
}
