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
        }
    }
}
