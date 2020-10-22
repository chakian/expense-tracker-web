using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Account
{
    public class CreateModel
    {
        [Display(Name = "Ad")]
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}
