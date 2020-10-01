using EFEntities.Base;

namespace EFEntities
{
    public partial class BudgetUsers : AuditableEntity
    {
        public int BudgetUserId { get; set; }
        public int BudgetId { get; set; }
        public string UserId { get; set; }
        public bool? CanDelete { get; set; }
        public bool? CanRead { get; set; }
        public bool? CanWrite { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsOwner { get; set; }

        public virtual Budgets Budget { get; set; }
        public virtual Users User { get; set; }
    }
}
