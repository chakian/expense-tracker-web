using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Account
{
    public class CreateModel
    {
        [Display(Name = "Ad")]
        [Required]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> TypeList { get; set; }
        public int Type { get; set; }
        public decimal Balance { get; set; }
    }
}
