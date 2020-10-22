using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Category
{
    public class CreateModel
    {
        [Display(Name = "Ad")]
        public string Name { get; set; }
        //public int ParentCategoryId { get; set; }
    }
}
