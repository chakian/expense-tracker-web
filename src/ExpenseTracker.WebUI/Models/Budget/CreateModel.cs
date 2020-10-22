using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Budget
{
    public class CreateModel
    {
        [Display(Name = "Ad")]
        public string Name { get; set; }
    }
}
