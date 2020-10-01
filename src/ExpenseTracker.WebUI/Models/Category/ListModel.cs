using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Category
{
    public class ListModel
    {
        public List<Category> CategoryList { get; set; }

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public decimal Balance { get; set; }
        }
    }
}
