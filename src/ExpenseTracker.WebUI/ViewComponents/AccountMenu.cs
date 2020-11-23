using ExpenseTracker.Business;
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
        AccountBusiness accountBusiness;
        public AccountMenu(DbContextOptions<ExpenseTrackerDbContext> contextOptions)
        {
            accountBusiness = new AccountBusiness(contextOptions);
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
            List<Account> list = accountBusiness.GetAccountsOfBudget(budgetId);
            return list;
        }
    }
}
