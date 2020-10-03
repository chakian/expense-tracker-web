using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Persistence.DbModels
{
    public class Budget : BaseAuditableDbo
    {
        [Required]
        public string Name { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }
}
