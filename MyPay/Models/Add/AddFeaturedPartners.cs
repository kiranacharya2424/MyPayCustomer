using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Wordprocessing;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace MyPay.Models.Add
{
    public class AddFeaturedPartners : CommonAdd
    {
        //bool DataRecieved = false;

        #region "Properties" 
        public string Message { get; set; }

        //Running
        public int Id { get; set; }
        [Required]
        public string OrganizationName { get; set; }

        public string Image { get; set; }
        [Required]
        public string WebsiteUrl { get; set; }
        [Required]
        public string SortOrder { get; set; }


        #endregion

        #region "Add Methods" 
        public CommonDbResponse Add()
        {
            CommonDbResponse obj1 = new CommonDbResponse();

            CommonHelpers obj = new CommonHelpers();
            System.Collections.Hashtable HT = SetObject();
            obj1 = obj.ExecuteProcedureGetValue("sp_InsertFeaturedPartners", HT);


            return obj1;
        }

        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("Id", Id);
            HT.Add("OrganizationName", OrganizationName);
            HT.Add("Image", Image);
            HT.Add("WebsiteUrl", WebsiteUrl);
            HT.Add("SortOrder", SortOrder);

            return HT;
        }
        #endregion


    }
}