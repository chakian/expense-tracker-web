using ExpenseTracker.Business;
using ExpenseTracker.Business.Queries;
using ExpenseTracker.Common.Contracts.Query;
using ExpenseTracker.Common.Entities;
using ExpenseTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.ViewComponents
{
    [ViewComponent]
    public class AccountMenu : ViewComponent
    {
        private readonly ExpenseTrackerDbContext context;
        public AccountMenu(ExpenseTrackerDbContext context)
        {
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {
            var tempBudgetId = TempData["budgetId"];
            int budgetId = 0;
            if (tempBudgetId != null)
            {
                int.TryParse(tempBudgetId.ToString(), out budgetId);
            }
            
            var list = GetAccounts(budgetId);
            return View(list);
        }

        private List<Account> GetAccounts(int budgetId)
        {
            var request = new GetAccountsOfBudgetRequest() { BudgetId = budgetId };
            var query = new GetAccountsOfBudgetQuery(context);
            var list = query.Retrieve(request).AccountList;

            return list;
        }
    }
}
