using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementProcessor.ObjectInfo
{
    public class OperationResultInfo
    {

        public OperationResultInfo()
        {
        }
        public OperationResultInfo(bool Status, string ErrorMessage, string ErrorCode, object ReturnObject, bool Condition)
        {
            _Status = Status;
            _ErrorMessage = ErrorMessage;
            _ErrorCode = ErrorCode;
            _ReturnObject = ReturnObject;
            _Condition = Condition;

        }

        private bool _Status;
        private string _ErrorMessage;
        private string _ErrorCode;
        private object _ReturnObject;
        private bool _Condition;
        public bool Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        public bool Condition
        {
            get
            {
                return _Condition;
            }
            set
            {
                _Condition = value;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
            }
        }

        public string ErrorCode
        {
            get
            {
                return _ErrorCode;
            }
            set
            {
                _ErrorCode = value;
            }
        }

        public object ReturnObject
        {
            get
            {
                return _ReturnObject;
            }
            set
            {
                _ReturnObject = value;
            }
        }
    }


    public class Statement
    {
        public string SlNos;
        public string Descriptions;
        public string Dates;
        public string TxnSrlNos;
        public string CheckNos;
        public string CrDrs;
        public string TransactionAmounts;
        public string BalanceAmounts;
        public string TxnMemos;
        public string TxnCategorys;
        public string TxnPstDates;
        public string TxnValueDates;
        public string Transaction;
        public string Externaltransaction;
        public Statement()
        {
        }
        public Statement(string Description, string SlNo, string Date, string TxnSrlNo, string CheckNo, string CrDr, string TransactionAmount, string BalanceAmount, string TxnMemo, string TxnCategory, string TxnPstDate, string TxnValueDate, string Transactions, string ExternalTransactions)
        {
            this.Descriptions = Description;
            this.SlNos = SlNo;
            this.Dates = Date;
            this.TxnSrlNos = TxnSrlNo;
            this.CheckNos = CheckNo;
            this.CrDrs = CrDr;
            this.TransactionAmounts = TransactionAmount;
            this.BalanceAmounts = BalanceAmount;
            this.TxnMemos = TxnMemo;
            this.TxnCategorys = TxnCategory;
            this.TxnPstDates = TxnPstDate;
            this.TxnValueDates = TxnValueDate;
            this.Transaction = Transactions;
            this.Externaltransaction = ExternalTransactions;
        }
        public string SlNo { get; set; }
        public string Date { get; set; }
        public string TxnSrlNo { get; set; }
        public string CheckNo { get; set; }
        public string CrDr { get; set; }
        public string TransactionAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string TxnMemo { get; set; }
        public string TxnCategory { get; set; }
        public string TxnPstDate { get; set; }
        public string TxnValueDate { get; set; }
        public string Description { get; set; }

        public string Transactions { get; set; }
        public string ExternalTransactions { get; set; }

    }
    public class Documents
    {

        public string DocumentName;
        public Documents()
        {
        }
        public Documents(string _DocumentName)
        {
            this.DocumentName = _DocumentName;

        }
        public string _DocumentName { get; set; }


    }
}