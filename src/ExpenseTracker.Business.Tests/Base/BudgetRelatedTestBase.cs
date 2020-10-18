using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpenseTracker.Business.Tests
{
    public class BudgetRelatedTestBase : TestBase
    {
        protected int CreateBudget(DbContextOptions<ExpenseTrackerDbContext> contextOptions, string userId, string budgetName = null)
        {
            if (budgetName == null) budgetName = Guid.NewGuid().ToString();

            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            int budgetId = budgetBusiness.CreateNewBudget(budgetName, userId);
            return budgetId;
        }
    }
}
