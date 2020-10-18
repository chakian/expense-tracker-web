using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Dashboard
{
    public class IndexModel
    {
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        //public decimal BudgetedAmount { get; set; }
        public decimal RecordedAmount { get; set; }
        //public decimal RemainingAmount { get; set; }
        public List<Category> SubCategories { get; set; }
    }
}
