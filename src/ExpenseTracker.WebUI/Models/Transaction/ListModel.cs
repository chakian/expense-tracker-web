﻿using System;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class ListModel
    {
        public List<Transaction> TransactionList { get; set; }

        public class Transaction
        {
            public int Id { get; set; }
            public int BudgetId { get; set; }
            public DateTime Date { get; set; }
            public int AccountId { get; set; }
            //public int? TargetAccountId { get; set; }
            //public bool IsSplitTransaction { get; set; }
            public int? CategoryId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
    }
}
