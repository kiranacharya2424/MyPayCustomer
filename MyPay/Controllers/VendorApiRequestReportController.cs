using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.NepalPayQR;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Windows.Interop;
using System.Xml;
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;

namespace MyPay.Controllers
{
    public class VendorApiRequestReportController : BaseAdminSessionController
    {
        [Authorize]
        // GET: VendorApiRequestReport
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetVendorApiRequestLists()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("Date");
            columns.Add("CreatedByName");
            columns.Add("TransactionId");
            columns.Add("VendorTransactionId");
            columns.Add("MemberId");
            columns.Add("MemberName");
            columns.Add("VendorType");
            columns.Add("VendorStatus");
            columns.Add("PlatForm");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            string MemberId = context.Request.Form["MemberId"];
            string fromdate = context.Request.Form["fromdate"];
            string todate = context.Request.Form["todate"];
            string Name = context.Request.Form["Name"];
            string Res_Khalti_Id = context.Request.Form["Res_Khalti_Id"];
            string TransactionUniqueId = context.Request.Form["TransactionUniqueId"];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddVendor_API_Requests> apirequest = new List<AddVendor_API_Requests>();

            AddVendor_API_Requests w = new AddVendor_API_Requests();
            if (!string.IsNullOrEmpty(MemberId))
            {
                w.MemberId = Convert.ToInt64(MemberId);
            }
            w.StartDate = fromdate;
            w.EndDate = todate;
            w.MemberName = Name;
            w.Res_Khalti_Id = Res_Khalti_Id;
            w.TransactionUniqueId = TransactionUniqueId;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            apirequest = (from DataRow row in dt.Rows

                          select new AddVendor_API_Requests
                          {
                              MemberId = Convert.ToInt64(row["MemberId"]),
                              MemberName = row["MemberName"].ToString(),
                              CreatedByName = row["CreatedByName"].ToString(),
                              TransactionUniqueId = row["TransactionUniqueId"].ToString(),
                              Req_ReferenceNo = row["Req_ReferenceNo"].ToString(),
                              Res_Khalti_Id = row["Res_Khalti_Id"].ToString(),
                              Res_Khalti_State = row["Res_Khalti_State"].ToString(),
                              TypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(row["VendorApiType"])).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString(),
                              CreatedDatedt = row["IndiaDate"].ToString(),
                              PlatForm = row["PlatForm"].ToString(),
                              DeviceCode = row["DeviceCode"].ToString(),
                              IpAddress = row["IpAddress"].ToString()
                          }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddVendor_API_Requests>> objDataTableResponse = new DataTableResponse<List<AddVendor_API_Requests>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = apirequest
            };
            var json = Json(objDataTableResponse);

            return Json(objDataTableResponse);

        }


        public string GetVendorApijsonDetails(string transactionid, string type)
        {
            string result = "";
            try
            {
                AddVendor_API_Requests outobject = new AddVendor_API_Requests();
                GetVendor_API_Requests inobject = new GetVendor_API_Requests();
                inobject.TransactionUniqueId = transactionid;
                AddVendor_API_Requests res = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Models.Common.Common.StoreProcedures.sp_VendorAPIRequest_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    if (type == "mypayreq")
                    {
                        result = res.Req_Input;
                    }
                    else if (type == "mypayres")
                    {
                        result = res.Res_Output;
                    }
                    else if (type == "vendorreq")
                    {
                        result = res.Req_Khalti_Input;
                    }
                    else if (type == "vendorres")
                    {
                        result = res.Res_Khalti_Output;
                    }
                    else
                    {
                        result = "";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        public string GetVendorTransactionStatus(string TransactionUniqueId)
        {
            string result = "";
            GetVendor_API_TransactionLookup objRes = new GetVendor_API_TransactionLookup();
            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
           
            try
            {
                WalletTransactions objtrans = new WalletTransactions();
                objtrans.TransactionUniqueId = TransactionUniqueId;
                if (objtrans.GetRecord())
                {
                    string ProviderEnumName = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), objtrans.Type).ToString();
                    bool IsUtility = false;
                    if (ProviderEnumName.ToLower().IndexOf("khalti") >= 0)
                    {
                        IsUtility = true;
                    }
                    if (IsUtility)
                    {
                        // *************** CHECK IF ANY OTHER TRANSACTION WITH SAME REFERENCE NO. AS SUCCESS ********************//
                        WalletTransactions objtransChkSuccess = new WalletTransactions();
                        objtransChkSuccess.Reference = objtrans.Reference;
                        objtransChkSuccess.Type = objtrans.Type;
                        objtransChkSuccess.MemberId = objtrans.MemberId;
                        objtransChkSuccess.Status = (int)WalletTransactions.Statuses.Success;
                        if (objtransChkSuccess.GetRecord())
                        {
                            result = $"Already success TransactionID: {objtransChkSuccess.TransactionUniqueId} found with Reference no {objtrans.Reference}.";
                        }
                        else
                        {
                            string authenticationToken = string.Empty;
                            string UserInput = string.Empty;
                            string Version = "1.0";
                            string DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                            string PlatForm = "Web";
                            result = RepKhalti.RequestTransactionLookup(objtrans.TransactionUniqueId, objtrans.Reference,
                                objtrans.Type.ToString(), objtrans.MemberId.ToString(), authenticationToken, UserInput,
                                Version, DeviceCode, PlatForm, ref objRes, ref objVendor_API_Requests);
                        }
                    }
                    else
                    {
                        result = "Selected Transaction is not done from Vendor. Please check status for vendor transactions only";
                    }
                }
                else
                {
                    result = "Transaction Not Found";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            objRes.message = result;
            return JsonConvert.SerializeObject(objRes);
        }


        public string GetNepalPayQRStatus(string TransactionUniqueId)
        {
            string result = "";
            GetVendor_API_TransactionLookup objRes = new GetVendor_API_TransactionLookup();
            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
            string Rep_State = "failed";
            string instructionId = string.Empty;
            string validationtraceid = string.Empty;
            string issuerId = string.Empty;
            string merchantid = string.Empty;
            string nQrTxnId = string.Empty;
            string AuthorizationKey = string.Empty;
            string MemberId = string.Empty;
            string MerchantName = string.Empty;
            string amt = string.Empty;
            try
            {
                WalletTransactions objtrans = new WalletTransactions();
                objtrans.TransactionUniqueId = TransactionUniqueId;
              
               
                if (objtrans.GetRecord())
                {
                    string ProviderEnumName = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), objtrans.Type).ToString();
                    bool IsUtility = false;
                    if (ProviderEnumName.ToLower()== "nepalpay_qr_payments")
                    {
                        IsUtility = true;
                    }
                    if (IsUtility)
                    {
                        // *************** CHECK IF ANY OTHER TRANSACTION WITH SAME REFERENCE NO. AS SUCCESS ********************//
                        WalletTransactions objtransChkSuccess = new WalletTransactions();
                        objtransChkSuccess.Reference = objtrans.Reference;
                        objtransChkSuccess.Type = objtrans.Type;
                        objtransChkSuccess.MemberId = objtrans.MemberId;
                        objtransChkSuccess.Status = (int)WalletTransactions.Statuses.Success;
                        if (objtransChkSuccess.GetRecord())
                        {
                            result = $"Already success TransactionID: {objtransChkSuccess.TransactionUniqueId} found with Reference no {objtrans.Reference}.";
                        }
                        else
                        {
                            string authenticationToken = "admin";
                            string UserInput = string.Empty;
                            string Version = "1.0";
                            string DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                            string PlatForm = "Web";

                           
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                          
                            Hashtable HT2 = new Hashtable();
                            HT2.Add("flag", "gettxndata");
                            HT2.Add("TransactionId", TransactionUniqueId);                  
                            DataTable dt2 = new DataTable();
                            string RefundCode = "0";
                            string Refundmessage = "";
                            dt2 = commonHelpers.GetDataFromStoredProcedure("sp_NepalPayQR_Detail", HT2);   //--Check refund amount exceed more than actual amount or not --//
                            if (dt2.Rows.Count > 0)
                            {
                                DataRow row = dt2.Rows[0];
                                instructionId = !string.IsNullOrEmpty(row["TxnInstructionId"].ToString()) ? row["TxnInstructionId"].ToString() : "";
                                validationtraceid=!string.IsNullOrEmpty(row["validationTraceId"].ToString()) ? row["validationTraceId"].ToString() : "";
                                issuerId = !string.IsNullOrEmpty(row["issuerId"].ToString()) ? row["issuerId"].ToString() : "";
                                merchantid = !string.IsNullOrEmpty(row["merchantpan"].ToString()) ? row["merchantpan"].ToString() : "";
                                nQrTxnId = !string.IsNullOrEmpty(row["CustomerID"].ToString()) ? row["CustomerID"].ToString() : "";
                                MemberId = !string.IsNullOrEmpty(row["MemberId"].ToString()) ? row["MemberId"].ToString() : "";
                                MerchantName= !string.IsNullOrEmpty(row["merchantName"].ToString()) ? row["merchantName"].ToString() : "";
                                amt = !string.IsNullOrEmpty(row["Amount"].ToString()) ? row["Amount"].ToString() : "";


                            }
                            else
                            {
                                
                            }

                            GetDataFromNepalQRPay getroutesdetail = RepKhalti.AuthenticationNepalPayQR("", "", "", Convert.ToInt64(MemberId), "Refund", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments));
                            NepalQRAuth model = JsonConvert.DeserializeObject<NepalQRAuth>(Convert.ToString(getroutesdetail.message));

                            GetDataFromNepalQRPay result1 = RepKhalti.GetRequestCheckstatus(objtrans.TransactionUniqueId, objtrans.Reference,
                                objtrans.Type.ToString(), objtrans.MemberId.ToString(), authenticationToken, UserInput,
                                Version, DeviceCode, PlatForm, ref objRes, ref objVendor_API_Requests ,
                                instructionId,  validationtraceid, issuerId,  merchantid,  nQrTxnId,  202 ,model.access_token);
                            
                            var dataresponse = JsonConvert.DeserializeObject<NepalQRCheckStatusResponse>(result1.message);
                            
                            
                            
                                //var dataresponse1 = JsonConvert.DeserializeObject<responsecheckstatus>(Convert.ToString( dataresponse.responseBody));
                                if (dataresponse.responseCode=="200")
                                {
                                string jsonString = JsonConvert.SerializeObject(dataresponse.responseBody);
                                List<responsecheckstatus> dataresponsestatus = JsonConvert.DeserializeObject<List<responsecheckstatus>>(jsonString);

                                nQrTxnId = dataresponsestatus[0].nQrTxnId;
                                if ((dataresponsestatus[0].debitStatus == "000") && (dataresponsestatus[0].creditStatus.ToUpper() == "DEFER" || dataresponsestatus[0].creditStatus == "000" || dataresponsestatus[0].creditStatus == "999"))
                                {
                                    Rep_State = "success";
                                    result = JsonConvert.SerializeObject(dataresponsestatus[0]) ;
                                    string Result1 = commonHelpers.GetScalarValueWithValue(" update  NepalPayQR  set UpdatedDate = GetDate(),transactionStatus = '" + Rep_State + "', WalletTransactionId = '" + TransactionUniqueId + "' , nQrTxnId='" + dataresponsestatus[0].nQrTxnId + "',sessionSrlNo='" + dataresponsestatus[0].sessionSrlNo + "'," +
                                          "creditStatus='" + dataresponsestatus[0].creditStatus + "',debitStatus='" + dataresponsestatus[0].debitStatus + "',NepalPayQRTxnDatetime='" + dataresponsestatus[0].recDate + "'  " +
                                          "   where  instructionId='" + instructionId + "'");


                                }


                            }
                            //else if (dataresponse.responseCode == "E010")
                            //{
                            //    Rep_State = "Transaction Id not found in third party service";
                            //    result = dataresponse.responseDescription;
                            //    string Result2 = commonHelpers.GetScalarValueWithValue(" update  NepalPayQR  set UpdatedDate = GetDate(),transactionStatus = '" + Rep_State + "', WalletTransactionId = '" + TransactionUniqueId + "' where  instructionId='" + instructionId + "'");

                            //}
                            else
                            {
                                Rep_State = "failed";
                                result = dataresponse.responseDescription;

                                string Result2 = commonHelpers.GetScalarValueWithValue(" update  NepalPayQR  set UpdatedDate = GetDate(),transactionStatus = '" + Rep_State + "', WalletTransactionId = '" + TransactionUniqueId + "' where  instructionId='" + instructionId + "'");

                            }

                        }
                    }
                    else
                    {
                        result = "Selected Transaction is not done from NepalPay QR. Please check status for NepalPay QR transactions only";
                    }
                }
                else
                {
                    result = "Transaction Not Found";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            

            objRes.message = result;
            var jsonresult1 = new
            {
                result = objRes.message,
                message = Rep_State,
                nQrTxnId = nQrTxnId,
                instructionId= instructionId,
                validationtraceid = validationtraceid,
                amt = amt,
                MerchantName = MerchantName

            };

            // Convert the anonymous object to JSON
            string jsonResult = JsonConvert.SerializeObject(jsonresult1);

            // Return JsonResult with the JSON string
            return JsonConvert.SerializeObject(jsonresult1);
            //return JsonConvert.SerializeObject(objRes);
        }

    }
}