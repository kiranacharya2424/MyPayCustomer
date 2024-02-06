using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MyPay.Repository
{
    public static class RepDealsAndOffers
    {
        public static string GetApiDealsandOffers(AddUserLoginWithPin resGetRecord, string Take, string Skip, string Type, ref List<AddDealsandOffers> list)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Take))
            {
                Take = "0";
            }
            if (string.IsNullOrEmpty(Skip))
            {
                Skip = "0";
            }
            if (string.IsNullOrEmpty(Type))
            {
                Type = "0";
            }
            AddDealsandOffers outobjecttrans = new AddDealsandOffers();
            GetAPIDealsAndOffers inobjectTrans = new GetAPIDealsAndOffers();
            inobjectTrans.Take = Convert.ToInt32(Take);
            inobjectTrans.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
            inobjectTrans.CheckDelete = 0;
            inobjectTrans.CheckActive = 1;
            inobjectTrans.MemberId = resGetRecord.MemberId; 
            List<AddDealsandOffers> list_deals = RepCRUD<GetAPIDealsAndOffers, AddDealsandOffers>.GetRecordList(Common.StoreProcedures.sp_APIDealsandOffers_Get, inobjectTrans, outobjecttrans);
            list = list_deals.OrderByDescending(c => Convert.ToDateTime(c.CreatedDate)).ToList(); ;
            if (list.Count > 0)
            {
                string ImagePrefix = Common.LiveSiteUrl;
                if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                {
                    ImagePrefix = (Common.LiveSiteUrl);
                }
                else
                {
                    ImagePrefix = (Common.TestSiteUrl);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (!string.IsNullOrEmpty(list[i].Image))
                    {
                        list[i].Image = (ImagePrefix + "Images/DealsandOffers/" + list[i].Image.ToString());
                    }
                }
                msg = Common.CommonMessage.success;
            }
            else
            {
                list.Clear();
                msg = Common.CommonMessage.Data_Not_Found;
            }
            return msg;
        }

    }
}