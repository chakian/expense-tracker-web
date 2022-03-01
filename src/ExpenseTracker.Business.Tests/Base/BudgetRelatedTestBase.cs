using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using System;

namespace ExpenseTracker.Business.Tests
{
    public class BudgetRelatedTestBase : TestBase
    {
        protected int CreateBudget(ExpenseTrackerDbContext context, string userId, string budgetName = null)
        {
            if (budgetName == null) budgetName = Guid.NewGuid().ToString();
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            CreateNewBudgetRequest createNewBudgetRequest = new CreateNewBudgetRequest()
            {
                BudgetName = budgetName,
                UserId = userId
            };
            int budgetId = budgetBusiness.CreateNewBudget(createNewBudgetRequest).Id;
            context.SaveChanges();
            return budgetId;
        }
    }
}
