using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController<T> : BaseController<T>
    {
        protected readonly UserManager<IdentityUser> _userManager;

        public BaseAuthenticatedController(ILogger<T> logger)
            : base(logger)
        {
        }
        public BaseAuthenticatedController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options)
            : base(logger, options)
        {
        }

        public BaseAuthenticatedController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options)
        {
            _userManager = userManager;

            //TODO: This should be done on register action. Find a way to do it there, later.
            //bool hasDefaultBudget = new BudgetCheckBusiness().DoesUserHaveDefaultBudget();
            //if (hasDefaultBudget == false)
            //{
            //    int firstBudgetId = DoesUserHaveAnyBudget();
            //    if (firstBudgetId > 0)
            //    {
            //        SetDefaultBudgetForUser();
            //    }
            //    else
            //    {
            //        firstBudgetId = CreateDefaultBudgetForUser();
            //        SetDefaultBudgetForUser();
            //    }
            //}
        }

        protected string UserId
        {
            get
            {
                return _userManager.GetUserId(User);
            }
        }
    }
}
