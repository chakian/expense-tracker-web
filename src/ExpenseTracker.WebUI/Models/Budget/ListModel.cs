using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Budget
{
    public class ListModel
    {
        public List<Budget> BudgetList { get; set; }

        public class Budget
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
