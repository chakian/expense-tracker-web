using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Budget
{
    public class UpdateModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }
    }
}
