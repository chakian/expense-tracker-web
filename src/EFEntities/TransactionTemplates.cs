﻿using System;
using System.Collections.Generic;

namespace EFEntities
{
    public partial class TransactionTemplates
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int? SourceAccountId { get; set; }
        public int? TargetAccountId { get; set; }
        public int BudgetId { get; set; }
        public string UserId { get; set; }

        public virtual Budgets Budget { get; set; }
        public virtual Categories Category { get; set; }
        public virtual Users InsertUser { get; set; }
        public virtual Accounts SourceAccount { get; set; }
        public virtual Accounts TargetAccount { get; set; }
        public virtual Users UpdateUser { get; set; }
        public virtual Users User { get; set; }
    }
}
