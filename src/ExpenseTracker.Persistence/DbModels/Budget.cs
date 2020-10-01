using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Persistence.DbModels
{
    public class Budget : BaseAuditableDbo
    {
        [Required]
        public string Name { get; set; }
    }
}
