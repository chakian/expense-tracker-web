using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Category
{
    public class DeleteModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }
    }
}
