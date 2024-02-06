using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iText.Html2pdf;
using System.IO;

namespace MyPay.Controllers
{
    public class MerchantProfileController : BaseMerchantSessionController
    {
        // GET: MerchantProfile
        public ActionResult Index()
        {
            AddMerchant model = new AddMerchant();
            if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Convert.ToString(Session["MerchantUniqueId"])))
            {
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    model = res;
                }
                else
                {
                    ViewBag.Message = "Something went wrong to fetch your profile, please try after some time.";
                }
            }
            return View(model);
        }

        // GET: MerchantBankDetail
        public ActionResult MerchantBankDetail()
        {
            AddMerchantBankDetail model = new AddMerchantBankDetail();
            if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Convert.ToString(Session["MerchantUniqueId"])))
            {
                AddMerchantBankDetail outobject = new AddMerchantBankDetail();
                GetMerchantBankDetail inobject = new GetMerchantBankDetail();
                inobject.MerchantId = Session["MerchantUniqueId"].ToString();
                AddMerchantBankDetail res = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    model = res;
                }
                else
                {
                    ViewBag.Message = "Bank Details Not Found.";
                }
            }
            return View(model);
        }


        public ActionResult MerchantQRCode()
        {
            AddMerchant model = new AddMerchant();
            if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Convert.ToString(Session["MerchantUniqueId"])))
            {
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    model = res;
                    ViewBag.QRCode = Common.GetQRMemberID_Encrypted(Convert.ToString(res.UserMemberId));
                }
                string HOST = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                ViewBag.Host = HOST;
            }
            return View(model);
        }
        public ActionResult MerchantQRCodeExport(string MerchantContact)
        {
            AddMerchant model = new AddMerchant();
            if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Convert.ToString(Session["MerchantUniqueId"])))
            {
                RepMechantsCommon objRepMechantsCommon = new RepMechantsCommon();
                objRepMechantsCommon.ExportToPDF(MerchantContact);
            }

            return View(model);
        }


    }
}