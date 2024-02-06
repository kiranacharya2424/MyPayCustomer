using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;

namespace MyPay.Repository
{
    public static class RepServiceCharge
    {
        public static string GetServiceCharge(string MemberId, string TotalAmount, string ServiceId, string platform, string devicecode, bool ismobile, ref AddCalculateServiceChargeAndCashback objOut)
        {
            try
            {
                string msg = string.Empty;
                if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
                {
                    msg = "Please enter MemberId.";
                }
                else if (string.IsNullOrEmpty(TotalAmount))
                {
                    msg = "Please enter TotalAmount.";
                }
                else if (string.IsNullOrEmpty(ServiceId) || ServiceId == "0")
                {
                    msg = "Please enter ServiceId.";
                }
                else if (!string.IsNullOrEmpty(MemberId))
                {
                    Int64 Num;
                    bool isNum = Int64.TryParse(MemberId, out Num);
                    if (!isNum)
                    {
                        msg = "Please enter valid MemberId.";
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    if (!string.IsNullOrEmpty(TotalAmount))
                    {
                        decimal Num;
                        bool isNum = decimal.TryParse(TotalAmount, out Num);
                        if (!isNum)
                        {
                            msg = "Please enter valid TotalAmount.";
                        }
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    if (!string.IsNullOrEmpty(ServiceId))
                    {
                        Int64 Num;
                        bool isNum = Int64.TryParse(ServiceId, out Num);
                        if (!isNum)
                        {
                            msg = "Please enter valid ServiceId.";
                        }
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    //AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                    //GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                    //inobject.MemberId = Convert.ToInt32(MemberId);
                    //AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                    //if (res != null && res.Id != 0)
                    if (true)
                    {
                        AddProviderLogoList outobjectService = new AddProviderLogoList();
                        GetProviderLogoList inobjectService = new GetProviderLogoList();
                        outobjectService.ProviderTypeId = ServiceId;
                        AddProviderLogoList resService = RepCRUD<GetProviderLogoList, AddProviderLogoList>.GetRecord(Common.StoreProcedures.sp_ProviderLogoList_Get, inobjectService, outobjectService);
                        if (resService != null && resService.Id != 0)
                        {
                            objOut = Common.CalculateNetAmountWithServiceCharge(MemberId, TotalAmount, ServiceId);
                            if (objOut != null)
                            {
                                msg = "success";
                            }
                            else
                            {
                                msg = "Cashback Not Found.";
                            }
                        }
                        else
                        {
                            msg = "Invalid Service ID.";
                        }
                    }
                    else
                    {
                        msg = "User not found with this MemberId.";
                    }
                }
                return msg;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return e.Message;
                throw;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}