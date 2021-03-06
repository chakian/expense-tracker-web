﻿namespace ExpenseTracker.Common.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public string Name { get; set; }
        public int AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
