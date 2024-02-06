using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using MyPay.API.Models.Request.NPS.Consumer;
using System.Drawing;

namespace MyPay.API.Models.NPS
{
    public class LodgeBill
    {
        public string batchId { get; set; }
        public string instructionId { get; set; }
        public string endToEndId { get; set; }
        public string appId { get; set; }
        public string vendorType { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
        public string creationDate { get; set; }
        public string transactionId { get; set; }
        public string memberId { get; set; }
        public string couponCode { get; set; }
        public string couponDiscount { get; set; }
        public string lodgeRequest { get; set; }
        public string lodgeResponse { get; set; }
        public string confirmRequest { get; set; }
        public string confirmResponse { get; set; }

        internal static string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        public bool create() {

            using (var connection = new SqlConnection(connectionString))
            {
                var storedProcedureName = "sp_NCHLServiceCreateUpdate";
                var values = new {
                    batchId = batchId,
                    instructionId = instructionId,
                    endToEndId = endToEndId,
                    appId = appId,
                    vendorType = vendorType,
                    amount = amount,
                    status = 0,
                    creationDate = DateTime.Now,
                    transactionId = transactionId,
                    memberId = memberId,
                    lodgeRequest = lodgeRequest,
                    lodgeResponse = lodgeResponse,
                    confirmRequest = confirmRequest,
                    confirmResponse = confirmResponse,
                    statementType = "Insert"
                };
                try
                {
                    var result = connection.Query(storedProcedureName, values, commandType: CommandType.StoredProcedure);
                }
                catch (SqlException sqlEx)
                {

                    //throw;
                }

               // connection.Query(storedProcedureName, values, commandType: CommandType.StoredProcedure);
                // results.ForEach(r => Console.WriteLine($"{r.OrderID} {r.Subtotal}"));

            }


            return false;
        }

        public bool update(string instructionId, string status, String lodgeReq = "", string lodgeResp = "", String confirmReq = "", string confirmResp = "")
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var storedProcedureName = "sp_NCHLServiceCreateUpdate";
                var values = new
                {
                    status = status,
                    instructionId = instructionId,
                    lodgeRequest = lodgeReq,
                    lodgeResponse = lodgeResp,
                    confirmRequest = confirmReq,
                    confirmResponse = confirmResp,
                    statementType = "Update"
                };
                try
                {
                    var result = connection.Query(storedProcedureName, values, commandType: CommandType.StoredProcedure);
                }
                catch (SqlException sqlEx)
                {

                    //throw;
                }
            }

                return false;
        }

        public LodgeBill getRecord(String instructionId)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                LodgeBill lodgeBill = new LodgeBill();
                var storedProcedureName = "sp_NCHLServiceCreateUpdate";
                var values = new
                {
                    instructionId = instructionId,
                    statementType = "Select"
                };
                try
                {
                    lodgeBill = connection.QueryFirstOrDefault<LodgeBill>(storedProcedureName, values, commandType: CommandType.StoredProcedure);
                    //var result = connection.Query<Customer>("GetCustomerByID", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (SqlException sqlEx)
                {

                    //throw;
                }
                return lodgeBill;
            }
        }
    }
}
