using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{
    sealed public class WalletTransactions
    {
        #region "Enums"
        public enum Signs
        {
            Credit = 1,
            Debit = 2
        }

        public enum WalletTypes
        {
            wallet = 0,
            Wallet = 1,
            Bank = 2,
            Card = 3,
            MPCoins = 4,
            FonePay = 5
        }

        public enum TransferTypes
        {
            Sender = 1,
            Reciever = 2
        }

        public enum Statuses
        {
            Success = 1,
            Pending = 2,
            Failed = 3,
            Queued = 4,
            Processing = 5,
            Expired = 6,
            Error = 7,
            Status_Error = 8,
            Refund = 9
        }


        public enum Types
        {
            Paused = 1,
            Cancelled = 2,
            Completed = 3,
            Rejected = 4,
            Reciept_Unverified = 5,
            Recieved = 6,
            Started = 7,
            Pending = 8,
            Amount_NotMatched = 9,
            Refund = 10
        }
        #endregion

        #region "Properties"
        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //VendorType
        private int _VendorType = 0;
        public int VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }
        private Int64 _MerchantMemberId = 0;
        public Int64 MerchantMemberId
        {
            get { return _MerchantMemberId; }
            set { _MerchantMemberId = value; }
        }
        private int _IsMerchantTxn = 0;
        public int IsMerchantTxn
        {
            get { return _IsMerchantTxn; }
            set { _IsMerchantTxn = value; }
        }
        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        //MerchantOrganization
        private string _MerchantOrganization = string.Empty;
        public string MerchantOrganization
        {
            get { return _MerchantOrganization; }
            set { _MerchantOrganization = value; }
        }
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //WalletImage
        private string _WalletImage = string.Empty;
        public string WalletImage
        {
            get { return _WalletImage; }
            set { _WalletImage = value; }
        }

        //MerchantId
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }
        //VendorTypeName
        private string _VendorTypeName = string.Empty;
        public string VendorTypeName
        {
            get { return _VendorTypeName; }
            set { _VendorTypeName = value; }
        }
        //Purpose
        private string _Purpose = string.Empty;
        public string Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; }
        }

        //CustomerID
        private string _CustomerID = string.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }


        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        //Sno
        private Int64 _Sno = 0;
        public Int64 Sno
        {
            get { return _Sno; }
            set { _Sno = value; }
        }

        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }

        }

        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }

        }
        //RecieverName
        private string _RecieverName = string.Empty;
        public string RecieverName
        {
            get { return _RecieverName; }
            set { _RecieverName = value; }
        }

        //UpdateBy
        private Int64 _UpdateBy = 0;
        public Int64 UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        //RecieverId
        private Int64 _RecieverId = 0;
        public Int64 RecieverId
        {
            get { return _RecieverId; }
            set { _RecieverId = value; }
        }

        //RecieverContactNumber
        private string _RecieverContactNumber = string.Empty;
        public string RecieverContactNumber
        {
            get { return _RecieverContactNumber; }
            set { _RecieverContactNumber = value; }
        }

        //CashBack
        private decimal _CashBack = 0;
        public decimal CashBack
        {
            get { return _CashBack; }
            set { _CashBack = value; }
        }

        //ServiceCharge
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }

        //RewardPoint
        private decimal _RewardPoint = 0;
        public decimal RewardPoint
        {
            get { return _RewardPoint; }
            set { _RewardPoint = value; }
        }

        //UpdateByName
        private string _UpdateByName = string.Empty;
        public string UpdateByName
        {
            get { return _UpdateByName; }
            set { _UpdateByName = value; }
        }

        //TransferType
        private int _TransferType = 0;
        public int TransferType
        {
            get { return _TransferType; }
            set { _TransferType = value; }
        }

        //GatewayStatus
        private string _GatewayStatus = string.Empty;
        public string GatewayStatus
        {
            get { return _GatewayStatus; }
            set { _GatewayStatus = value; }
        }

        //ResponseCode
        private string _ResponseCode = string.Empty;
        public string ResponseCode
        {
            get { return _ResponseCode; }
            set { _ResponseCode = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //VendorTransactionId
        private string _VendorTransactionId = string.Empty;
        public string VendorTransactionId
        {
            get { return _VendorTransactionId; }
            set { _VendorTransactionId = value; }
        }
        private string _UpdatedDatedt = string.Empty;
        public string UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }

        }
        //ParentTransactionId
        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //MemberName
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        //SignName
        private string _SignName = string.Empty;
        public string SignName
        {
            get { return _SignName; }
            set { _SignName = value; }
        }

        //TypeName
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        //Sign
        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        //Sign
        private int _recordTotal = 0;
        public int recordTotal
        {
            get { return _recordTotal; }
            set { _recordTotal = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //Type
        private string _TypeMultiple = string.Empty;
        public string TypeMultiple
        {
            get { return _TypeMultiple; }
            set { _TypeMultiple = value; }
        }
        //WalletType
        private int _WalletType = 0;
        public int WalletType
        {
            get { return _WalletType; }
            set { _WalletType = value; }
        }

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //CurrentBalance
        private decimal _CurrentBalance = 0;
        public decimal CurrentBalance
        {
            get { return _CurrentBalance; }
            set { _CurrentBalance = value; }
        }

        //PreviousBalance
        private decimal _PreviousBalance = 0;
        public decimal PreviousBalance
        {
            get { return _PreviousBalance; }
            set { _PreviousBalance = value; }
        }
        //CreatedBy
        private Int64 _CreatedBy = 0;
        public Int64 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        //CreatedByName
        private string _CreatedByName = string.Empty;
        public string CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }

        //Platform
        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        //DeviceCode
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        //StartDate 
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //CardNumber 
        private string _CardNumber = string.Empty;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }

        //ExpiryDate 
        private string _ExpiryDate = string.Empty;
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }

        //CardType 
        private string _CardType = string.Empty;
        public string CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }

        //EndDate 
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        //CreatedDate
        private DateTime _CreatedDate = DateTime.UtcNow;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        //UpdatedDate
        private DateTime _UpdatedDate = DateTime.UtcNow;
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        //IsDeleted
        private bool _IsDeleted = false;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        //IsApprovedByAdmin
        private bool _IsApprovedByAdmin = false;
        public bool IsApprovedByAdmin
        {
            get { return _IsApprovedByAdmin; }
            set { _IsApprovedByAdmin = value; }
        }

        //IsActive
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }

        //CheckDelete
        private int _CheckDelete = 2;// (int)clsData.BooleanValue.Both;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }

        //CheckCreatedDate
        private string _CheckCreatedDate = string.Empty;
        public string CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }


        //ThreeMonth
        private string _ThreeMonth = string.Empty;
        public string ThreeMonth
        {
            get { return _ThreeMonth; }
            set { _ThreeMonth = value; }
        }

        //SixMonth
        private string _SixMonth = string.Empty;
        public string SixMonth
        {
            get { return _SixMonth; }
            set { _SixMonth = value; }
        }

        //Year
        private string _Year = string.Empty;
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        //_NetAmount
        private decimal _NetAmount = 0;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
        //RecieverBankCode
        private string _RecieverBankCode = string.Empty;
        public string RecieverBankCode
        {
            get { return _RecieverBankCode; }
            set { _RecieverBankCode = value; }
        }

        //WalletTypeName
        private string _WalletTypeName = string.Empty;
        public string WalletTypeName
        {
            get { return _WalletTypeName; }
            set { _WalletTypeName = value; }
        }
        //RecieverAccountNo
        private string _RecieverAccountNo = string.Empty;
        public string RecieverAccountNo
        {
            get { return _RecieverAccountNo; }
            set { _RecieverAccountNo = value; }
        }
        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        //ReferCode
        private string _ReferCode = string.Empty;
        public string ReferCode
        {
            get { return _ReferCode; }
            set { _ReferCode = value; }
        }
        //RecieverBranch
        private string _RecieverBranch = string.Empty;
        public string RecieverBranch
        {
            get { return _RecieverBranch; }
            set { _RecieverBranch = value; }
        }
        //SenderBankCode
        private string _SenderBankCode = string.Empty;
        public string SenderBankCode
        {
            get { return _SenderBankCode; }
            set { _SenderBankCode = value; }
        }
        //SenderBankName
        private string _SenderBankName = string.Empty;
        public string SenderBankName
        {
            get { return _SenderBankName; }
            set { _SenderBankName = value; }
        }
        //RecieverBankName
        private string _RecieverBankName = string.Empty;
        public string RecieverBankName
        {
            get { return _RecieverBankName; }
            set { _RecieverBankName = value; }
        }


        //SenderBranchName
        private string _SenderBranchName = string.Empty;
        public string SenderBranchName
        {
            get { return _SenderBranchName; }
            set { _SenderBranchName = value; }
        }
        //RecieverBranchName
        private string _RecieverBranchName = string.Empty;
        public string RecieverBranchName
        {
            get { return _RecieverBranchName; }
            set { _RecieverBranchName = value; }
        }
        //SenderAccountNo
        private string _SenderAccountNo = string.Empty;
        public string SenderAccountNo
        {
            get { return _SenderAccountNo; }
            set { _SenderAccountNo = value; }
        }
        //SenderBranch
        private string _SenderBranch = string.Empty;
        public string SenderBranch
        {
            get { return _SenderBranch; }
            set { _SenderBranch = value; }
        }
        //BatchTransactionId
        private string _BatchTransactionId = string.Empty;
        public string BatchTransactionId
        {
            get { return _BatchTransactionId; }
            set { _BatchTransactionId = value; }
        }
        //TxnInstructionId
        private string _TxnInstructionId = string.Empty;
        public string TxnInstructionId
        {
            get { return _TxnInstructionId; }
            set { _TxnInstructionId = value; }
        }
        //Today
        private string _Today = string.Empty;
        public string Today
        {
            get { return _Today; }
            set { _Today = value; }
        }
        //Weekly
        private string _Weekly = string.Empty;
        public string Weekly
        {
            get { return _Weekly; }
            set { _Weekly = value; }
        }
        //Monthly
        private string _Monthly = string.Empty;
        public string Monthly
        {
            get { return _Monthly; }
            set { _Monthly = value; }
        }

        //VendorResponsePin
        private string _VendorResponsePin = string.Empty;
        public string VendorResponsePin
        {
            get { return _VendorResponsePin; }
            set { _VendorResponsePin = value; }
        }

        //VendorResponseSerial
        private string _VendorResponseSerial = string.Empty;
        public string VendorResponseSerial
        {
            get { return _VendorResponseSerial; }
            set { _VendorResponseSerial = value; }
        }

        //TotalCredit
        private decimal _TotalCredit = 0;
        public decimal TotalCredit
        {
            get { return _TotalCredit; }
            set { _TotalCredit = value; }
        }
        //TotalDebit
        private decimal _TotalDebit = 0;
        public decimal TotalDebit
        {
            get { return _TotalDebit; }
            set { _TotalDebit = value; }
        }
        //TotalServiceCharge
        private decimal _TotalServiceCharge = 0;
        public decimal TotalServiceCharge
        {
            get { return _TotalServiceCharge; }
            set { _TotalServiceCharge = value; }
        }
        //AmountSum
        private decimal _AmountSum = 0;
        public decimal AmountSum
        {
            get { return _AmountSum; }
            set { _AmountSum = value; }
        }
        //FilterTotalCount
        private Int32 _FilterTotalCount = 0;
        public Int32 FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }

        //ReceiverAmount
        private decimal _ReceiverAmount = 0;
        public decimal ReceiverAmount
        {
            get { return _ReceiverAmount; }
            set { _ReceiverAmount = value; }
        }

        //MPCoinsDebit
        private decimal _MPCoinsDebit = 0;
        public decimal MPCoinsDebit
        {
            get { return _MPCoinsDebit; }
            set { _MPCoinsDebit = value; }
        }

        //RewardPointBalance
        private decimal _RewardPointBalance = 0;
        public decimal RewardPointBalance
        {
            get { return _RewardPointBalance; }
            set { _RewardPointBalance = value; }
        }
        //PreviousRewardPointBalance
        private decimal _PreviousRewardPointBalance = 0;
        public decimal PreviousRewardPointBalance
        {
            get { return _PreviousRewardPointBalance; }
            set { _PreviousRewardPointBalance = value; }
        }

        //TransactionAmount
        private decimal _TransactionAmount = 0;
        public decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        //RefCode
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }
        //VendorJsonLookup
        private string _VendorJsonLookup = string.Empty;
        public string VendorJsonLookup
        {
            get { return _VendorJsonLookup; }
            set { _VendorJsonLookup = value; }
        }

        //TotalCoinsCredit
        private decimal _TotalCoinsCredit = 0;
        public decimal TotalCoinsCredit
        {
            get { return _TotalCoinsCredit; }
            set { _TotalCoinsCredit = value; }
        }
        //TotalCoinsDebit
        private decimal _TotalCoinsDebit = 0;
        public decimal TotalCoinsDebit
        {
            get { return _TotalCoinsDebit; }
            set { _TotalCoinsDebit = value; }
        }

        //WalletTypeMultiple
        private string _WalletTypeMultiple = string.Empty;
        public string WalletTypeMultiple
        {
            get { return _WalletTypeMultiple; }
            set { _WalletTypeMultiple = value; }
        }

        //CouponCode
        private string _CouponCode = string.Empty;
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        //CouponDiscount
        private decimal _CouponDiscount = 0;
        public decimal CouponDiscount
        {
            get { return _CouponDiscount; }
            set { _CouponDiscount = value; }
        }

        //IsFavourite
        private bool _IsFavourite = false;
        public bool IsFavourite
        {
            get { return _IsFavourite; }
            set { _IsFavourite = value; }
        }

        //CheckFavourite
        private int _CheckFavourite = 2;// (int)clsData.BooleanValue.Both;
        public int CheckFavourite
        {
            get { return _CheckFavourite; }
            set { _CheckFavourite = value; }
        }
         
        //AdditionalInfo1
        private string _AdditionalInfo1 = "";
        public string AdditionalInfo1
        {
            get { return _AdditionalInfo1; }
            set { _AdditionalInfo1 = value; }
        }

        //AdditionalInfo2
        private string _AdditionalInfo2 = "";
        public string AdditionalInfo2
        {
            get { return _AdditionalInfo2; }
            set { _AdditionalInfo2 = value; }
        }
        //TotalCouponDiscount
        private decimal _TotalCouponDiscount = 0;
        public decimal TotalCouponDiscount
        {
            get { return _TotalCouponDiscount; }
            set { _TotalCouponDiscount = value; }
        }
        #endregion

        #region "Add Delete Update Methods"
        public bool Add()
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = SetObject();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_WalletTransactions_AddNew_New", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool AddCashBack()
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = SetObjectV2(); //SetObject();
                //HT.
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_WalletTransactions_CashBack", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool Update()
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_WalletTransactions_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public bool Updates() //only for plasmatech(mypay_airlines)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_WalletTransactions_UpdateNew", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public Hashtable SetObject()
        {
            Hashtable Ht = new Hashtable();
            Ht.Add("MemberId", MemberId);
            Ht.Add("MemberName", MemberName);
            Ht.Add("Sign", Sign);
            //if (Type == 80)
            //{
            //    Ht.Add("Amount", TransactionAmount);
            //}
            //else
            //{
            //    Ht.Add("Amount", Amount);
            //}

            Ht.Add("Amount", Amount);
            //Ht.Add("Amount", Amount);
            Ht.Add("Description", Description);
            Ht.Add("Type", Type);
            Ht.Add("Reference", Reference);
            Ht.Add("Remarks", Remarks);
            Ht.Add("CurrentBalance", CurrentBalance);
            Ht.Add("TransactionUniqueId", TransactionUniqueId);
            Ht.Add("VendorTransactionId", VendorTransactionId);
            Ht.Add("ParentTransactionId", ParentTransactionId);
            Ht.Add("Status", Status);
            Ht.Add("CreatedDate", CreatedDate);
            Ht.Add("UpdatedDate", UpdatedDate);
            Ht.Add("IsDeleted", IsDeleted);
            Ht.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            Ht.Add("IsActive", IsActive);
            Ht.Add("CreatedBy", CreatedBy);
            Ht.Add("CreatedByName", CreatedByName);
            Ht.Add("CardNumber", CardNumber);
            Ht.Add("ExpiryDate", ExpiryDate);
            Ht.Add("CardType", CardType);
            Ht.Add("ContactNumber", ContactNumber);
            Ht.Add("RecieverName", RecieverName);
            Ht.Add("RecieverId", RecieverId);
            Ht.Add("RecieverContactNumber", RecieverContactNumber);
            Ht.Add("CashBack", CashBack);
            Ht.Add("ServiceCharge", ServiceCharge);
            Ht.Add("RewardPoint", RewardPoint);
            Ht.Add("GatewayStatus", GatewayStatus);
            Ht.Add("UpdateBy", UpdateBy);
            Ht.Add("UpdateByName", UpdateByName);
            Ht.Add("TransferType", TransferType);
            Ht.Add("Platform", Platform);
            Ht.Add("DeviceCode", DeviceCode);
            Ht.Add("NetAmount", NetAmount);
            Ht.Add("RecieverBankCode", RecieverBankCode);
            Ht.Add("RecieverAccountNo", RecieverAccountNo);
            Ht.Add("RecieverBranch", RecieverBranch);
            Ht.Add("SenderBankCode", SenderBankCode);
            Ht.Add("SenderAccountNo", SenderAccountNo);
            Ht.Add("SenderBranch", SenderBranch);
            Ht.Add("BatchTransactionId", BatchTransactionId);
            Ht.Add("TxnInstructionId", TxnInstructionId);
            Ht.Add("CustomerID", CustomerID);
            Ht.Add("IpAddress", IpAddress);
            Ht.Add("ResponseCode", ResponseCode);
            Ht.Add("WalletType", WalletType);
            Ht.Add("Purpose", Purpose);
            Ht.Add("SenderBankName", SenderBankName);
            Ht.Add("RecieverBankName", RecieverBankName);
            Ht.Add("SenderBranchName", SenderBranchName);
            Ht.Add("RecieverBranchName", RecieverBranchName);
            Ht.Add("VendorType", VendorType);
            Ht.Add("VendorResponsePin", VendorResponsePin);
            Ht.Add("VendorResponseSerial", VendorResponseSerial);
            Ht.Add("ReceiverAmount", ReceiverAmount);
            Ht.Add("RefCode", RefCode);
            Ht.Add("RewardPointBalance", RewardPointBalance);
            Ht.Add("TransactionAmount", TransactionAmount);
            Ht.Add("VendorJsonLookup", VendorJsonLookup);
            Ht.Add("MerchantMemberId", MerchantMemberId);
            Ht.Add("MerchantId", MerchantId);
            Ht.Add("MerchantOrganization", MerchantOrganization);
            Ht.Add("CouponCode", CouponCode);
            Ht.Add("CouponDiscount", CouponDiscount);
            Ht.Add("WalletImage", WalletImage);
            Ht.Add("AdditionalInfo1", AdditionalInfo1);
            Ht.Add("AdditionalInfo2", AdditionalInfo2); 
            return Ht;
        }


        public Hashtable SetObjectV2()
        {
            Hashtable Ht = new Hashtable();
            Ht.Add("MemberId", MemberId);
            Ht.Add("MemberName", MemberName);
            Ht.Add("Sign", Sign);
            if (Type == 80)
            {
                Ht.Add("Amount", TransactionAmount);
            }
            else
            {
                Ht.Add("Amount", Amount);
            }

            Ht.Add("Description", Description);
            Ht.Add("Type", Type);
            Ht.Add("Reference", Reference);
            Ht.Add("Remarks", Remarks);
            Ht.Add("CurrentBalance", CurrentBalance);
            Ht.Add("TransactionUniqueId", TransactionUniqueId);
            Ht.Add("VendorTransactionId", VendorTransactionId);
            Ht.Add("ParentTransactionId", ParentTransactionId);
            Ht.Add("Status", Status);
            Ht.Add("CreatedDate", CreatedDate);
            Ht.Add("UpdatedDate", UpdatedDate);
            Ht.Add("IsDeleted", IsDeleted);
            Ht.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            Ht.Add("IsActive", IsActive);
            Ht.Add("CreatedBy", CreatedBy);
            Ht.Add("CreatedByName", CreatedByName);
            Ht.Add("CardNumber", CardNumber);
            Ht.Add("ExpiryDate", ExpiryDate);
            Ht.Add("CardType", CardType);
            Ht.Add("ContactNumber", ContactNumber);
            Ht.Add("RecieverName", RecieverName);
            Ht.Add("RecieverId", RecieverId);
            Ht.Add("RecieverContactNumber", RecieverContactNumber);
            Ht.Add("CashBack", CashBack);
            Ht.Add("ServiceCharge", ServiceCharge);
            Ht.Add("RewardPoint", RewardPoint);
            Ht.Add("GatewayStatus", GatewayStatus);
            Ht.Add("UpdateBy", UpdateBy);
            Ht.Add("UpdateByName", UpdateByName);
            Ht.Add("TransferType", TransferType);
            Ht.Add("Platform", Platform);
            Ht.Add("DeviceCode", DeviceCode);
            Ht.Add("NetAmount", NetAmount);
            Ht.Add("RecieverBankCode", RecieverBankCode);
            Ht.Add("RecieverAccountNo", RecieverAccountNo);
            Ht.Add("RecieverBranch", RecieverBranch);
            Ht.Add("SenderBankCode", SenderBankCode);
            Ht.Add("SenderAccountNo", SenderAccountNo);
            Ht.Add("SenderBranch", SenderBranch);
            Ht.Add("BatchTransactionId", BatchTransactionId);
            Ht.Add("TxnInstructionId", TxnInstructionId);
            Ht.Add("CustomerID", CustomerID);
            Ht.Add("IpAddress", IpAddress);
            Ht.Add("ResponseCode", ResponseCode);
            Ht.Add("WalletType", WalletType);
            Ht.Add("Purpose", Purpose);
            Ht.Add("SenderBankName", SenderBankName);
            Ht.Add("RecieverBankName", RecieverBankName);
            Ht.Add("SenderBranchName", SenderBranchName);
            Ht.Add("RecieverBranchName", RecieverBranchName);
            Ht.Add("VendorType", VendorType);
            Ht.Add("VendorResponsePin", VendorResponsePin);
            Ht.Add("VendorResponseSerial", VendorResponseSerial);
            Ht.Add("ReceiverAmount", ReceiverAmount);
            Ht.Add("RefCode", RefCode);
            Ht.Add("RewardPointBalance", RewardPointBalance);
            Ht.Add("TransactionAmount", TransactionAmount);
            Ht.Add("VendorJsonLookup", VendorJsonLookup);
            Ht.Add("MerchantMemberId", MerchantMemberId);
            Ht.Add("MerchantId", MerchantId);
            Ht.Add("MerchantOrganization", MerchantOrganization);
            Ht.Add("CouponCode", CouponCode);
            Ht.Add("CouponDiscount", CouponDiscount);
            Ht.Add("WalletImage", WalletImage);
            Ht.Add("AdditionalInfo1", AdditionalInfo1);
            Ht.Add("AdditionalInfo2", AdditionalInfo2);
            return Ht;
        }



        #endregion

        #region "Get Methods"
        public DataTable GetList()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberId", MemberId);
                HT.Add("MemberName", MemberName);
                HT.Add("Amount", Amount);
                HT.Add("Sign", Sign);
                HT.Add("Type", Type);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("Status", Status);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Year", Year);
                HT.Add("ThreeMonth", ThreeMonth);
                HT.Add("SixMonth", SixMonth);
                HT.Add("WalletType", WalletType);
                HT.Add("RecieverId", RecieverId);
                HT.Add("TransferType", TransferType);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("RecieverName", RecieverName);
                HT.Add("RecieverContactNumber", RecieverContactNumber);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                HT.Add("RefCode", RefCode);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("CustomerID", CustomerID);
                HT.Add("VendorType", VendorType);
                //HT.Add("TypeMultiple", TypeMultiple);
                HT.Add("CouponCode", CouponCode);
                HT.Add("CheckFavourite", CheckFavourite);
                dt = obj.GetDataFromStoredProcedure("sp_WalletTransactions_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }
        public bool GetRecord()
        {
            DataTable dt = new DataTable();
            DataRecieved = false;
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberId", MemberId);
                HT.Add("MemberName", MemberName);
                HT.Add("Sign", Sign);
                HT.Add("Amount", Amount);
                HT.Add("Type", Type);
                HT.Add("Reference", Reference);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Status", Status);
                HT.Add("Take", 1);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("ThreeMonth", ThreeMonth);
                HT.Add("SixMonth", SixMonth);
                HT.Add("Year", Year);
                HT.Add("WalletType", WalletType);
                HT.Add("CouponCode", CouponCode);
                HT.Add("CheckFavourite", CheckFavourite);
                dt = obj.GetDataFromStoredProcedure("sp_WalletTransactions_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {

                    VendorResponseSerial = dt.Rows[0]["VendorResponseSerial"].ToString();
                    VendorResponsePin = dt.Rows[0]["VendorResponsePin"].ToString();
                    ResponseCode = dt.Rows[0]["ResponseCode"].ToString();
                    SenderBankName = dt.Rows[0]["SenderBankName"].ToString();
                    RecieverBankName = dt.Rows[0]["RecieverBankName"].ToString();
                    SenderBranchName = dt.Rows[0]["SenderBranchName"].ToString();
                    RecieverBranchName = dt.Rows[0]["RecieverBranchName"].ToString();
                    IpAddress = dt.Rows[0]["IpAddress"].ToString();
                    Purpose = dt.Rows[0]["Purpose"].ToString();
                    CardNumber = dt.Rows[0]["CardNumber"].ToString();
                    CustomerID = dt.Rows[0]["CustomerID"].ToString();
                    ExpiryDate = dt.Rows[0]["ExpiryDate"].ToString();
                    Platform = dt.Rows[0]["Platform"].ToString();
                    DeviceCode = dt.Rows[0]["DeviceCode"].ToString();
                    CardType = dt.Rows[0]["CardType"].ToString();
                    ContactNumber = dt.Rows[0]["ContactNumber"].ToString();
                    RecieverName = dt.Rows[0]["RecieverName"].ToString();
                    VendorType = Convert.ToInt32(dt.Rows[0]["VendorType"].ToString());
                    RecieverId = Convert.ToInt64(dt.Rows[0]["RecieverId"].ToString());
                    RecieverContactNumber = dt.Rows[0]["RecieverContactNumber"].ToString();
                    CashBack = Convert.ToDecimal(dt.Rows[0]["CashBack"].ToString());
                    ServiceCharge = Convert.ToDecimal(dt.Rows[0]["ServiceCharge"].ToString());
                    RewardPoint = Convert.ToDecimal(dt.Rows[0]["RewardPoint"].ToString());
                    GatewayStatus = dt.Rows[0]["GatewayStatus"].ToString();
                    UpdateBy = Convert.ToInt64(dt.Rows[0]["UpdateBy"].ToString());
                    UpdateByName = dt.Rows[0]["UpdateByName"].ToString();
                    TransferType = Convert.ToInt32(dt.Rows[0]["TransferType"].ToString());
                    WalletType = Convert.ToInt32(dt.Rows[0]["WalletType"].ToString());
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"].ToString());
                    MemberName = dt.Rows[0]["MemberName"].ToString();
                    Sign = Convert.ToInt32(dt.Rows[0]["Sign"].ToString());
                    Type = Convert.ToInt32(dt.Rows[0]["Type"].ToString());
                    Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    CurrentBalance = Convert.ToDecimal(dt.Rows[0]["CurrentBalance"].ToString());
                    Description = dt.Rows[0]["Description"].ToString();
                    Remarks = dt.Rows[0]["Remarks"].ToString();
                    Reference = dt.Rows[0]["Reference"].ToString();
                    TransactionUniqueId = dt.Rows[0]["TransactionUniqueId"].ToString();
                    VendorTransactionId = dt.Rows[0]["VendorTransactionId"].ToString();
                    ParentTransactionId = dt.Rows[0]["ParentTransactionId"].ToString();
                    Status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = dt.Rows[0]["CreatedByName"].ToString();
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    NetAmount = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());

                    RecieverBankCode = dt.Rows[0]["RecieverBankCode"].ToString();
                    RecieverAccountNo = dt.Rows[0]["RecieverAccountNo"].ToString();
                    RecieverBranch = dt.Rows[0]["RecieverBranch"].ToString();
                    SenderBankCode = dt.Rows[0]["SenderBankCode"].ToString();
                    SenderAccountNo = dt.Rows[0]["SenderAccountNo"].ToString();
                    SenderBranch = dt.Rows[0]["SenderBranch"].ToString();
                    BatchTransactionId = dt.Rows[0]["BatchTransactionId"].ToString();
                    TxnInstructionId = dt.Rows[0]["TxnInstructionId"].ToString();
                    VendorResponsePin = dt.Rows[0]["VendorResponsePin"].ToString();
                    VendorResponseSerial = dt.Rows[0]["VendorResponseSerial"].ToString();
                    ReceiverAmount = Convert.ToDecimal(dt.Rows[0]["ReceiverAmount"].ToString());
                    RefCode = dt.Rows[0]["RefCode"].ToString();
                    MPCoinsDebit = Convert.ToDecimal(dt.Rows[0]["MPCoinsDebit"].ToString());
                    RewardPointBalance = Convert.ToDecimal(dt.Rows[0]["RewardPointBalance"].ToString());
                    TransactionAmount = Convert.ToDecimal(dt.Rows[0]["TransactionAmount"].ToString());
                    VendorJsonLookup = (dt.Rows[0]["VendorJsonLookup"].ToString());
                    CouponCode = (dt.Rows[0]["CouponCode"].ToString());
                    MerchantMemberId = Convert.ToInt64(dt.Rows[0]["MerchantMemberId"].ToString());
                    MerchantId = Convert.ToString(dt.Rows[0]["MerchantId"].ToString());
                    MerchantOrganization = Convert.ToString(dt.Rows[0]["MerchantOrganization"].ToString());
                    WalletImage = Convert.ToString(dt.Rows[0]["WalletImage"].ToString());
                    IsFavourite = Convert.ToBoolean(dt.Rows[0]["IsFavourite"].ToString());
                    AdditionalInfo1 = Convert.ToString(dt.Rows[0]["AdditionalInfo1"].ToString());
                    AdditionalInfo2 = Convert.ToString(dt.Rows[0]["AdditionalInfo2"].ToString());
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool GetRecordCheckExists()
        {
            DataTable dt = new DataTable();
            DataRecieved = false;
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberId", MemberId);
                HT.Add("MemberName", MemberName);
                HT.Add("Sign", Sign);
                HT.Add("Amount", Amount);
                HT.Add("Type", Type);
                HT.Add("Reference", Reference);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Status", Status);
                HT.Add("Take", 1);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("ThreeMonth", ThreeMonth);
                HT.Add("SixMonth", SixMonth);
                HT.Add("Year", Year);
                HT.Add("WalletType", WalletType);
                HT.Add("CouponCode", CouponCode);
                dt = obj.GetDataFromStoredProcedure("sp_WalletTransactions_Get_CheckExists", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(dt.Rows[0]["Count"].ToString()) > 0)
                    {
                        DataRecieved = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("MemberId", MemberId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Type", Type);
                HT.Add("TypeMultiple", TypeMultiple);
                HT.Add("TransferType", TransferType);
                HT.Add("MemberName", MemberName);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Status", Status);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("RecieverName", RecieverName);
                HT.Add("RecieverContactNumber", RecieverContactNumber);
                HT.Add("WalletType", WalletType);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                HT.Add("Sign", Sign);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("VendorType", VendorType);
                HT.Add("CustomerID", CustomerID);
                HT.Add("RefCode", RefCode);
                HT.Add("WalletTypeMultiple", WalletTypeMultiple);
                HT.Add("CouponCode", CouponCode);
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantId", MerchantId);
                HT.Add("MerchantOrganization", MerchantOrganization);
                HT.Add("IsMerchantTxn", IsMerchantTxn);
                HT.Add("RoleId", RoleId);
                HT.Add("CheckFavourite", CheckFavourite);
                dt = obj.GetDataFromStoredProcedure("sp_WalletTransactions_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_WalletTransactions_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                        dt.Rows[0]["AmountSum"] = dtCounter.Rows[0]["AmountSum"].ToString();
                        dt.Rows[0]["TotalCredit"] = dtCounter.Rows[0]["TotalCredit"].ToString();
                        dt.Rows[0]["TotalDebit"] = dtCounter.Rows[0]["TotalDebit"].ToString();
                        dt.Rows[0]["TotalServiceCharge"] = dtCounter.Rows[0]["TotalServiceCharge"].ToString();
                        dt.Rows[0]["TotalCoinsCredit"] = dtCounter.Rows[0]["TotalCoinsCredit"].ToString();
                        dt.Rows[0]["TotalCoinsDebit"] = dtCounter.Rows[0]["TotalCoinsDebit"].ToString();
                        dt.Rows[0]["TotalCouponDiscount"] = dtCounter.Rows[0]["TotalCouponDiscount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }


        public int GetTransactionStatusEnum(string Status)
        {
            int TransactionStatusEnum = (int)Statuses.Pending;
            switch (Status.ToLower().Trim())
            {
                case "success":
                    TransactionStatusEnum = (int)Statuses.Success;
                    break;
                case "pending":
                    TransactionStatusEnum = (int)Statuses.Pending;
                    break;
                case "queued":
                    TransactionStatusEnum = (int)Statuses.Queued;
                    break;
                case "expired":
                    TransactionStatusEnum = (int)Statuses.Expired;
                    break;
                case "processing":
                    TransactionStatusEnum = (int)Statuses.Processing;
                    break;
                case "error":
                    TransactionStatusEnum = (int)Statuses.Error;
                    break;
                case "failed":
                    TransactionStatusEnum = (int)Statuses.Failed;
                    break;
                case "statuserror":
                    TransactionStatusEnum = (int)Statuses.Status_Error;
                    break;
            }
            return TransactionStatusEnum;
        }

        public DataTable GetAllServiceTxnList()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Type", Type);
                HT.Add("TypeMultiple", TypeMultiple);
                HT.Add("TransferType", TransferType);
                HT.Add("MemberName", MemberName);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Status", Status);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("RecieverName", RecieverName);
                HT.Add("RecieverContactNumber", RecieverContactNumber);
                HT.Add("WalletType", WalletType);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                HT.Add("Sign", Sign);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("VendorType", VendorType);
                HT.Add("CustomerID", CustomerID);
                HT.Add("RefCode", RefCode);
                dt = obj.GetDataFromStoredProcedure("sp_WalletTransactionsAllService_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public DataTable GetAllTxnDataDump()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Type", Type);
                HT.Add("TypeMultiple", TypeMultiple);
                HT.Add("TransferType", TransferType);
                HT.Add("MemberName", MemberName);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Status", Status);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("RecieverName", RecieverName);
                HT.Add("RecieverContactNumber", RecieverContactNumber);
                HT.Add("WalletType", WalletType);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                HT.Add("Sign", Sign);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("VendorType", VendorType);
                HT.Add("CustomerID", CustomerID);
                HT.Add("RefCode", RefCode);
                HT.Add("WalletTypeMultiple", WalletTypeMultiple);
                dt = obj.GetDataFromStoredProcedure("sp_WalletTransactionsAllTxn_Get", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        public DataTable GetEStatementList()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("Year", Year);
                HT.Add("Month", ThreeMonth);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                dt = obj.GetDataFromStoredProcedure("sp_WalletEStatement_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }
        public DataTable GetMerchantAllTxnDataDump()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Type", Type);
                HT.Add("TypeMultiple", TypeMultiple);
                HT.Add("TransferType", TransferType);
                HT.Add("MemberName", MemberName);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Status", Status);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("RecieverName", RecieverName);
                HT.Add("RecieverContactNumber", RecieverContactNumber);
                HT.Add("WalletType", WalletType);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                HT.Add("Sign", Sign);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("VendorType", VendorType);
                HT.Add("CustomerID", CustomerID);
                HT.Add("RefCode", RefCode);
                HT.Add("WalletTypeMultiple", WalletTypeMultiple);
                HT.Add("CouponCode", CouponCode);
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantId", MerchantId);
                HT.Add("MerchantOrganization", MerchantOrganization);
                HT.Add("IsMerchantTxn", IsMerchantTxn);
                HT.Add("RoleId", RoleId);
                dt = obj.GetDataFromStoredProcedure("sp_WalletTransactionsAllMerchantTxn_Get", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        #endregion
    }
}

public class DataTableResponse<TParent> where TParent : class
{
    public int draw;
    public int recordsFiltered;
    public int recordsTotal;
    public TParent data;
}
public class VendorJsonLookupItems
{
    public string PriorityNo { get; set; }
    public string ProductName { get; set; }
    public string Amount { get; set; }
    public string ShortDetails { get; set; }
    public string ProductCode { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; }
    public string PackageID { get; set; }
    public string Validity { get; set; }
}


public class VendorJsonLookupItemsElecticityNEA
{
    public string Consumer_Name { get; set; }
    public string Customer_Id { get; set; }
    public string SC_NO { get; set; }
    public string Counter { get; set; }
    public string Total_Due_Amount { get; set; }
    public List<ElecticityNEADueBills> Due_Bills { get; set; }

    //status
    private bool _Status = false;
    public bool Status
    {
        get { return _Status; }
        set { _Status = value; }
    }

    //Session_Id
    private string _Session_Id = "";
    public string Session_Id
    {
        get { return _Session_Id; }
        set { _Session_Id = value; }
    }

}
public class ElecticityNEADueBills
{
    public string Bill_Amount { get; set; }
    public string Bill_Date { get; set; }
    public string Days { get; set; }
    public string Payable_Amount { get; set; }
    public string Due_Bill_Of { get; set; }
    public string Status { get; set; }
}

public class VendorJsonLookupItemsKhanepani
{
    public string Customer_Name { get; set; }
    public string Customer_Id { get; set; }
    public string Address { get; set; }
    public string Reference_No { get; set; }
    public string Total_Due_Amount { get; set; }
    public List<KhanepaniDueBills> Due_Bills { get; set; }

    //status
    private bool _Status = false;
    public bool Status
    {
        get { return _Status; }
        set { _Status = value; }
    }

    //Session_Id
    private string _Session_Id = "";
    public string Session_Id
    {
        get { return _Session_Id; }
        set { _Session_Id = value; }
    }

}

public class KhanepaniDueBills
{
    public string Bill_Amount { get; set; }
    public string Bill_Date { get; set; }
    public string Days { get; set; }
    public string Payable_Amount { get; set; }
    public string Due_Bill_Of { get; set; }
    public string Status { get; set; }
}