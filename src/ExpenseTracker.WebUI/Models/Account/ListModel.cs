using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Account
{
    public class ListModel
    {
        public List<Account> AccountList { get; set; }

        public class Account
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Type { get; set; }
            public string TypeName { get; set; }

            public decimal Balance { get; set; }
        }
    }
}
