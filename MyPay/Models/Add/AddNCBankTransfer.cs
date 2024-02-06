using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{

    public class AddNCBankTransfer
    {
        //cipsBatchDetail
        private AddNCBatchDetail _cipsBatchDetail = new AddNCBatchDetail();
        public AddNCBatchDetail cipsBatchDetail
        {
            get { return _cipsBatchDetail; }
            set { _cipsBatchDetail = value; }
        }

        //cipsTransactionDetailList
        private List<AddNCTransactionDetail> _cipsTransactionDetailList = new List<AddNCTransactionDetail>();
        public List<AddNCTransactionDetail> cipsTransactionDetailList
        {
            get { return _cipsTransactionDetailList; }
            set { _cipsTransactionDetailList = value; }
        }

        //token
        private string _token = "";
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
    public class AddNCBatchDetail
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

    public class AddNCTransactionDetail
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

        //creditorAgent 
        private string _creditorAgent = "";
        public string creditorAgent
        {
            get { return _creditorAgent; }
            set { _creditorAgent = value; }
        }

        //creditorBranch
        private string _creditorBranch = "";
        public string creditorBranch
        {
            get { return _creditorBranch; }
            set { _creditorBranch = value; }
        }

        //creditorName
        private string _creditorName = "";
        public string creditorName
        {
            get { return _creditorName; }
            set { _creditorName = value; }
        }

        //creditorAccount
        private string _creditorAccount = "";
        public string creditorAccount
        {
            get { return _creditorAccount; }
            set { _creditorAccount = value; }
        }

        //creditorType
        private string _creditorType = "";
        public string creditorType
        {
            get { return _creditorType; }
            set { _creditorType = value; }
        }

        //creditorValue
        private string _creditorValue = "";
        public string creditorValue
        {
            get { return _creditorValue; }
            set { _creditorValue = value; }
        }

        //creditorAddress
        private string _creditorAddress = "";
        public string creditorAddress
        {
            get { return _creditorAddress; }
            set { _creditorAddress = value; }
        }

        //creditorPhone
        private string _creditorPhone = "";
        public string creditorPhone
        {
            get { return _creditorPhone; }
            set { _creditorPhone = value; }
        }

        //creditorMobile
        private string _creditorMobile = "";
        public string creditorMobile
        {
            get { return _creditorMobile; }
            set { _creditorMobile = value; }
        }

        //creditorEmail
        private string _creditorEmail = "";
        public string creditorEmail
        {
            get { return _creditorEmail; }
            set { _creditorEmail = value; }
        }

        //addenda1
        private int _addenda1 = 0;
        public int addenda1
        {
            get { return _addenda1; }
            set { _addenda1 = value; }
        }

        //addenda2
        private string _addenda2 = "";
        public string addenda2
        {
            get { return _addenda2; }
            set { _addenda2 = value; }
        }

        //addenda3
        private string _addenda3 = "";
        public string addenda3
        {
            get { return _addenda3; }
            set { _addenda3 = value; }
        }

        //addenda4
        private string _addenda4 = "";
        public string addenda4
        {
            get { return _addenda4; }
            set { _addenda4 = value; }
        }

        //freeCode1
        private string _freeCode1 = "";
        public string freeCode1
        {
            get { return _freeCode1; }
            set { _freeCode1 = value; }
        }

        //freeCode2
        private string _freeCode2 = "";
        public string freeCode2
        {
            get { return _freeCode2; }
            set { _freeCode2 = value; }
        }

        //freeText1
        private string _freeText1 = "";
        public string freeText1
        {
            get { return _freeText1; }
            set { _freeText1 = value; }
        }

        //freeText2
        private string _freeText2 = "";
        public string freeText2
        {
            get { return _freeText2; }
            set { _freeText2 = value; }
        }

        //remarks
        private string _remarks = "";
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

    }
}