using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetBankTransfer
    {
        #region "Properties"

        //cipsBatchResponse
        private GetBakTransferBatch _cipsBatchResponse = new GetBakTransferBatch();
        public GetBakTransferBatch cipsBatchResponse
        {
            get { return _cipsBatchResponse; }
            set { _cipsBatchResponse = value; }
        }

        //cipsBatchResponse
        private List<GetBakTransferTxn> _cipsTxnResponseList = new List<GetBakTransferTxn>();
        public List<GetBakTransferTxn> cipsTxnResponseList
        {
            get { return _cipsTxnResponseList; }
            set { _cipsTxnResponseList = value; }
        }

        #endregion
    }

    public class GetBakTransferBatch
    {
        //cipsBatchResponse
        private string _responseCode = string.Empty;
        public string responseCode
        {
            get { return _responseCode; }
            set { _responseCode = value; }
        }

        //responseMessage
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }

        //batchId
        private string _batchId = string.Empty;
        public string batchId
        {
            get { return _batchId; }
            set { _batchId = value; }
        }

        //debitStatus
        private string _debitStatus = string.Empty;
        public string debitStatus
        {
            get { return _debitStatus; }
            set { _debitStatus = value; }
        }

        //id
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
    }

    public class GetBakTransferTxn
    {
        //cipsBatchResponse
        private string _responseCode = string.Empty;
        public string responseCode
        {
            get { return _responseCode; }
            set { _responseCode = value; }
        }

        //responseMessage
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }

        //instructionId
        private string _instructionId = string.Empty;
        public string instructionId
        {
            get { return _instructionId; }
            set { _instructionId = value; }
        }

        //debitStatus
        private string _creditStatus = string.Empty;
        public string creditStatus
        {
            get { return _creditStatus; }
            set { _creditStatus = value; }
        }

        //id
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
    }

     public class GetAllCipsTransctions
    {

        private CipsBatchDetail _cipsBatchDetail = new CipsBatchDetail();
        public CipsBatchDetail cipsBatchDetail
        {
            get { return _cipsBatchDetail; }
            set { _cipsBatchDetail = value; }
        }

        private List<GetTransactionBatchDetail> _cipsTransactionDetailList = new List<GetTransactionBatchDetail>();
        public List<GetTransactionBatchDetail> cipsTransactionDetailList
        {
            get { return _cipsTransactionDetailList; }
            set { _cipsTransactionDetailList = value; }
        }
    }
    // CIPS BATCH DETAILS
    public class CipsBatchDetail
    {
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _batchId = string.Empty;
        public string batchId
        {
            get { return _batchId; }
            set { _batchId = value; }
        }
        private string _recDate = string.Empty;
        public string recDate
        {
            get { return _recDate; }
            set { _recDate = value; }
        }
        private string _isoTxnId = string.Empty;
        public string isoTxnId
        {
            get { return _isoTxnId; }
            set { _isoTxnId = value; }
        }
        private string _batchAmount = string.Empty;
        public string batchAmount
        {
            get { return _batchAmount; }
            set { _batchAmount = value; }
        }
        private string _batchCount = string.Empty;
        public string batchCount
        {
            get { return _batchCount; }
            set { _batchCount = value; }
        }
        private string _batchChargeAmount = string.Empty;
        public string batchChargeAmount
        {
            get { return _batchChargeAmount; }
            set { _batchChargeAmount = value; }
        }
        private string _batchCrncy = string.Empty;
        public string batchCrncy
        {
            get { return _batchCrncy; }
            set { _batchCrncy = value; }
        }
        private string _categoryPurpose = string.Empty;
        public string categoryPurpose
        {
            get { return _categoryPurpose; }
            set { _categoryPurpose = value; }
        }
        private string _debtorAgent = string.Empty;
        public string debtorAgent
        {
            get { return _debtorAgent; }
            set { _debtorAgent = value; }
        }
        private string _debtorBranch = string.Empty;
        public string debtorBranch
        {
            get { return _debtorBranch; }
            set { _debtorBranch = value; }
        }
        private string _debtorName = string.Empty;
        public string debtorName
        {
            get { return _debtorName; }
            set { _debtorName = value; }
        }
        private string _debtorAccount = string.Empty;
        public string debtorAccount
        {
            get { return _debtorAccount; }
            set { _debtorAccount = value; }
        }
        private string _debtorIdType = string.Empty;
        public string debtorIdType
        {
            get { return _debtorIdType; }
            set { _debtorIdType = value; }
        }
        private string _debtorIdValue = string.Empty;
        public string debtorIdValue
        {
            get { return _debtorIdValue; }
            set { _debtorIdValue = value; }
        }
        private string _debtorAddress = string.Empty;
        public string debtorAddress
        {
            get { return _debtorAddress; }
            set { _debtorAddress = value; }
        }
        private string _debtorPhone = string.Empty;
        public string debtorPhone
        {
            get { return _debtorPhone; }
            set { _debtorPhone = value; }
        }
        private string _debtorMobile = string.Empty;
        public string debtorMobile
        {
            get { return _debtorMobile; }
            set { _debtorMobile = value; }
        }
        private string _debtorEmail = string.Empty;
        public string debtorEmail
        {
            get { return _debtorEmail; }
            set { _debtorEmail = value; }
        }
        private string _channelId = string.Empty;
        public string channelId
        {
            get { return _channelId; }
            set { _channelId = value; }
        }
        private string _debitStatus = string.Empty;
        public string debitStatus
        {
            get { return _debitStatus; }
            set { _debitStatus = value; }
        }
        private string _debitReasonCode = string.Empty;
        public string debitReasonCode
        {
            get { return _debitReasonCode; }
            set { _debitReasonCode = value; }
        }
        private string _ipsBatchId = string.Empty;
        public string ipsBatchId
        {
            get { return _ipsBatchId; }
            set { _ipsBatchId = value; }
        }
        private string _fileName = string.Empty;
        public string fileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private string _rcreTime = string.Empty;
        public string rcreTime
        {
            get { return _rcreTime; }
            set { _rcreTime = value; }
        }
        private string _rcreUserId = string.Empty;
        public string rcreUserId
        {
            get { return _rcreUserId; }
            set { _rcreUserId = value; }
        }
        private string _sessionSrlNo = string.Empty;
        public string sessionSrlNo
        {
            get { return _sessionSrlNo; }
            set { _sessionSrlNo = value; }
        }
        private string _settlementDate = string.Empty;
        public string settlementDate
        {
            get { return _settlementDate; }
            set { _settlementDate = value; }
        }
        private string _debitReasonDesc = string.Empty;
        public string debitReasonDesc
        {
            get { return _debitReasonDesc; }
            set { _debitReasonDesc = value; }
        }
        private string _txnResponse = string.Empty;
        public string txnResponse
        {
            get { return _txnResponse; }
            set { _txnResponse = value; }
        }

        private List<GetTransactionBatchDetail> _cipsTransactionDetailList = new List<GetTransactionBatchDetail>();
        public List<GetTransactionBatchDetail> cipsTransactionDetailList
        {
            get { return _cipsTransactionDetailList; }
            set { _cipsTransactionDetailList = value; }
        }

    }

    public class GetTransactionBatchDetail
    {
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _batchId = string.Empty;
        public string batchId
        {
            get { return _batchId; }
            set { _batchId = value; }
        }
        private string _isoTxnId = string.Empty;
        public string isoTxnId
        {
            get { return _isoTxnId; }
            set { _isoTxnId = value; }
        }
        private string _recDate = string.Empty;
        public string recDate
        {
            get { return _recDate; }
            set { _recDate = value; }
        }
        private string _instructionId = string.Empty;
        public string instructionId
        {
            get { return _instructionId; }
            set { _instructionId = value; }
        }
        private string _endToEndId = string.Empty;
        public string endToEndId
        {
            get { return _endToEndId; }
            set { _endToEndId = value; }
        }
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private string _chargeAmount = string.Empty;
        public string chargeAmount
        {
            get { return _chargeAmount; }
            set { _chargeAmount = value; }
        }
        private string _chargeLiability = string.Empty;
        public string chargeLiability
        {
            get { return _chargeLiability; }
            set { _chargeLiability = value; }
        }
        private string _purpose = string.Empty;
        public string purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
        private string _merchantId = string.Empty;
        public string merchantId
        {
            get { return _merchantId; }
            set { _merchantId = value; }
        }
        private string _appId = string.Empty;
        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
        }
        private string _appTxnId = string.Empty;
        public string appTxnId
        {
            get { return _appTxnId; }
            set { _appTxnId = value; }
        }
        private string _creditorAgent = string.Empty;
        public string creditorAgent
        {
            get { return _creditorAgent; }
            set { _creditorAgent = value; }
        }
        private string _creditorBranch = string.Empty;
        public string creditorBranch
        {
            get { return _creditorBranch; }
            set { _creditorBranch = value; }
        }
        private string _creditorName = string.Empty;
        public string creditorName
        {
            get { return _creditorName; }
            set { _creditorName = value; }
        }
        private string _creditorAccount = string.Empty;
        public string creditorAccount
        {
            get { return _creditorAccount; }
            set { _creditorAccount = value; }
        }
        private string _creditorIdType = string.Empty;
        public string creditorIdType
        {
            get { return _creditorIdType; }
            set { _creditorIdType = value; }
        }
        private string _creditorIdValue = string.Empty;
        public string creditorIdValue
        {
            get { return _creditorIdValue; }
            set { _creditorIdValue = value; }
        }
        private string _creditorAddress = string.Empty;
        public string creditorAddress
        {
            get { return _creditorAddress; }
            set { _creditorAddress = value; }
        }
        private string _creditorPhone = string.Empty;
        public string creditorPhone
        {
            get { return _creditorPhone; }
            set { _creditorPhone = value; }
        }
        private string _creditorMobile = string.Empty;
        public string creditorMobile
        {
            get { return _creditorMobile; }
            set { _creditorMobile = value; }
        }
        private string _creditorEmail = string.Empty;
        public string creditorEmail
        {
            get { return _creditorEmail; }
            set { _creditorEmail = value; }
        }
        private string _addenda1 = string.Empty;
        public string addenda1
        {
            get { return _addenda1; }
            set { _addenda1 = value; }
        }
        private string _addenda2 = string.Empty;
        public string addenda2
        {
            get { return _addenda2; }
            set { _addenda2 = value; }
        }
        private string _addenda3 = string.Empty;
        public string addenda3
        {
            get { return _addenda3; }
            set { _addenda3 = value; }
        }
        private string _addenda4 = string.Empty;
        public string addenda4
        {
            get { return _addenda4; }
            set { _addenda4 = value; }
        }
        private string _creditStatus = string.Empty;
        public string creditStatus
        {
            get { return _creditStatus; }
            set { _creditStatus = value; }
        }
        private string _reasonCode = string.Empty;
        public string reasonCode
        {
            get { return _reasonCode; }
            set { _reasonCode = value; }
        }
        private string _reversalStatus = string.Empty;
        public string reversalStatus
        {
            get { return _reversalStatus; }
            set { _reversalStatus = value; }
        }
        private string _refId = string.Empty;
        public string refId
        {
            get { return _refId; }
            set { _refId = value; }
        }
        private string _remarks = string.Empty;
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private string _particulars = string.Empty;
        public string particulars
        {
            get { return _particulars; }
            set { _particulars = value; }
        }
        private string _freeCode1 = string.Empty;
        public string freeCode1
        {
            get { return _freeCode1; }
            set { _freeCode1 = value; }
        }
        private string _freeCode2 = string.Empty;
        public string freeCode2
        {
            get { return _freeCode2; }
            set { _freeCode2 = value; }
        }
        private string _freeText1 = string.Empty;
        public string freeText1
        {
            get { return _freeText1; }
            set { _freeText1 = value; }
        }
        private string _freeText2 = string.Empty;
        public string freeText2
        {
            get { return _freeText2; }
            set { _freeText2 = value; }
        }
        private string _beneficiaryId = string.Empty;
        public string beneficiaryId
        {
            get { return _beneficiaryId; }
            set { _beneficiaryId = value; }
        }
        private string _beneficiaryName = string.Empty;
        public string beneficiaryName
        {
            get { return _beneficiaryName; }
            set { _beneficiaryName = value; }
        }
        private string _ipsBatchId = string.Empty;
        public string ipsBatchId
        {
            get { return _ipsBatchId; }
            set { _ipsBatchId = value; }
        }
        private string _rcreUserId = string.Empty;
        public string rcreUserId
        {
            get { return _rcreUserId; }
            set { _rcreUserId = value; }
        }
        private string _rcreTime = string.Empty;
        public string rcreTime
        {
            get { return _rcreTime; }
            set { _rcreTime = value; }
        }
        private string _reasonDesc = string.Empty;
        public string reasonDesc
        {
            get { return _reasonDesc; }
            set { _reasonDesc = value; }
        }
        private string _txnResponse = string.Empty;
        public string txnResponse
        {
            get { return _txnResponse; }
            set { _txnResponse = value; }
        }    

    }

    public class Req_CipsBatch
    {

        private string _batchId = string.Empty;
        public string batchId
        {
            get { return _batchId; }
            set { _batchId = value; }
        }
    }

    public class Req_CipsBankTransaction
    {

        private string _txnDateFrom = string.Empty;
        public string txnDateFrom
        {
            get { return _txnDateFrom; }
            set { _txnDateFrom = value; }
        }

        private string _txnDateTo = string.Empty;
        public string txnDateTo
        {
            get { return _txnDateTo; }
            set { _txnDateTo = value; }
        }
    }
}