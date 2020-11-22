using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Category
{
    public class CreateModel
    {
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Üst Kategori")]
        public int? ParentCategoryId { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
    }
}
