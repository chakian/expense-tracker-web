using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Persistence.DbModels
{
    public class Budget : BaseAuditableDbo
    {
        [Required]
        public string Name { get; set; }

        public string OwnerUserId { get; set; }
        public virtual IdentityUser OwnerUser { get; set; }
    }
}
