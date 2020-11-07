using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Persistence.DbModels
{
    public class Account : BaseAuditableDbo
    {
        [Required]
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AccountType { get; set; }
    }
}
