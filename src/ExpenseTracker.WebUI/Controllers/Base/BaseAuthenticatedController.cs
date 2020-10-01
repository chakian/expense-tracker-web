using ExpenseTracker.Business.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController : BaseAuthenticatedController
    {
        public BaseAuthenticatedController()
        {
            //TODO: This should be done on register action. Find a way to do it there, later.
            bool hasDefaultBudget = new BudgetCheckBusiness().DoesUserHaveDefaultBudget();
            if (hasDefaultBudget == false)
            {
                int firstBudgetId = DoesUserHaveAnyBudget();
                if(firstBudgetId > 0)
                {
                    SetDefaultBudgetForUser();
                }
                else
                {
                    firstBudgetId = CreateDefaultBudgetForUser();
                    SetDefaultBudgetForUser();
                }
            }
        }
    }
}
