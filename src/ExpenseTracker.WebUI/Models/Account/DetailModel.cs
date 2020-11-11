using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Account
{
    public class DetailModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }
        public int Type { get; set; }
        public decimal Balance { get; set; }
    }
}
