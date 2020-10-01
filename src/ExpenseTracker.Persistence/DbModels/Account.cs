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

        //TODO: Think this through, you're drunk. Just Kidding. Think about it though
        //public int AccountTypeId { get; set; }
    }
}
