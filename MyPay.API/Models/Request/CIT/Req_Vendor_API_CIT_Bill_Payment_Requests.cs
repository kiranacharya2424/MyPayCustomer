using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_CIT_Bill_Payment_Requests : CommonProp
    {
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private CITBatchDetail _CITBatchDetail = new CITBatchDetail();
        public CITBatchDetail CITBatchDetail
        {
            get { return _CITBatchDetail; }
            set { _CITBatchDetail = value; }
        }
        private CITTransactionDetail _CITTransactionDetail = new CITTransactionDetail();
        public CITTransactionDetail CITTransactionDetail
        {
            get { return _CITTransactionDetail; }
            set { _CITTransactionDetail = value; }
        }
    }
    public class CITBatchDetail
    {
        //batchId
        private string _batchId = "";
        public string batchId
        {
            get { return _batchId; }
            set { _batchId = value; }
        }

        //batchAmount
        private string _batchAmount = string.Empty;
        public string batchAmount
        {
            get { return _batchAmount; }
            set { _batchAmount = value; }
        }

        //batchCount
        private int _batchCount = 0;
        public int batchCount
        {
            get { return _batchCount; }
            set { _batchCount = value; }
        }

        //batchCrncy
        private string _batchCrncy = "";
        public string batchCrncy
        {
            get { return _batchCrncy; }
            set { _batchCrncy = value; }
        }

        //categoryPurpose
        private string _categoryPurpose = "";
        public string categoryPurpose
        {
            get { return _categoryPurpose; }
            set { _categoryPurpose = value; }
        }

        //debtorAgent
        private string _debtorAgent = "";
        public string debtorAgent
        {
            get { return _debtorAgent; }
            set { _debtorAgent = value; }
        }

        //debtorBranch
        private string _debtorBranch = "";
        public string debtorBranch
        {
            get { return _debtorBranch; }
            set { _debtorBranch = value; }
        }

        //debtorName
        private string _debtorName = "";
        public string debtorName
        {
            get { return _debtorName; }
            set { _debtorName = value; }
        }

        //debtorAccount
        private string _debtorAccount = "";
        public string debtorAccount
        {
            get { return _debtorAccount; }
            set { _debtorAccount = value; }
        }

        //debtorIdType
        private string _debtorIdType = "";
        public string debtorIdType
        {
            get { return _debtorIdType; }
            set { _debtorIdType = value; }
        }

        //debtorIdValue
        private string _debtorIdValue = "";
        public string debtorIdValue
        {
            get { return _debtorIdValue; }
            set { _debtorIdValue = value; }
        }

        //debtorAddress
        private string _debtorAddress = "";
        public string debtorAddress
        {
            get { return _debtorAddress; }
            set { _debtorAddress = value; }
        }

        //debtorPhone
        private string _debtorPhone = "";
        public string debtorPhone
        {
            get { return _debtorPhone; }
            set { _debtorPhone = value; }
        }

        //debtorMobile
        private string _debtorMobile = "";
        public string debtorMobile
        {
            get { return _debtorMobile; }
            set { _debtorMobile = value; }
        }

        //debtorEmail
        private string _debtorEmail = "";
        public string debtorEmail
        {
            get { return _debtorEmail; }
            set { _debtorEmail = value; }
        }


    }
    public class CITTransactionDetail
    {
        //instructionId
        private string _instructionId = "";
        public string instructionId
        {
            get { return _instructionId; }
            set { _instructionId = value; }
        }

        //endToEndId 
        private string _endToEndId = "";
        public string endToEndId
        {
            get { return _endToEndId; }
            set { _endToEndId = value; }
        }

        //amount 
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        //appId 
        private string _appId = string.Empty;
        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        //refId 
        private string _refId = string.Empty;
        public string refId
        {
            get { return _refId; }
            set { _refId = value; }
        }

        //remarks
        private string _remarks = "";
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        //freeCode1
        private string _freeCode1 = "";
        public string freeCode1
        {
            get { return _freeCode1; }
            set { _freeCode1 = value; }
        }

    }
}