using System;

namespace EFEntities
{
    public partial class BudgetUsers
    {
        public int BudgetUserId { get; set; }
        public bool IsActive { get; set; }
        public string InsertUserId { get; set; }
        public DateTime InsertTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int BudgetId { get; set; }
        public string UserId { get; set; }
        public bool? CanDelete { get; set; }
        public bool? CanRead { get; set; }
        public bool? CanWrite { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsOwner { get; set; }

        public virtual Budgets Budget { get; set; }
        public virtual Users InsertUser { get; set; }
        public virtual Users UpdateUser { get; set; }
        public virtual Users User { get; set; }
    }
}
