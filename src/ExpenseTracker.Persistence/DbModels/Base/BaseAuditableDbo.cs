using Microsoft.AspNetCore.Identity;
using System;

namespace ExpenseTracker.Persistence.DbModels
{
    public class BaseAuditableDbo<T> : BaseDbo<T>
    {
        public string InsertUserId { get; set; }
        public virtual IdentityUser InsertUser { get; set; }
        public DateTime InsertTime { get; set; }

        public string UpdateUserId { get; set; }
        public virtual IdentityUser UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }

    public class BaseAuditableDbo : BaseAuditableDbo<int>
    {
    }
}
