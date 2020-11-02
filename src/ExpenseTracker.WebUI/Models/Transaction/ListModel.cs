using System;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class ListModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        public List<Transaction> TransactionList { get; set; }

        public class Transaction
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string AccountName { get; set; }
            //public int? TargetAccountId { get; set; }
            //public bool IsSplitTransaction { get; set; }
            public string CategoryName { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
    }
}
