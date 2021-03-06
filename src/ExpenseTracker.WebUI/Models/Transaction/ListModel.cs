﻿using System;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class ListModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CurrentMonth { get; set; }
        public string CurrentMonthName { get; set; }
        public int CurrentYear { get; set; }
        public int CurrentAccountId { get; set; }

        public List<Transaction> TransactionList { get; set; }

        public class Transaction
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string AccountName { get; set; }
            public string TargetAccountName { get; set; }
            public bool IsSplitTransaction { get; set; }
            public string CategoryName { get; set; }
            public decimal Amount { get; set; }
            public bool IsIncome { get; set; }
            public string Description { get; set; }
        }
    }
}
