using ExpenseTracker.Business;
using ExpenseTracker.Common.Entities;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController<T> : BaseController<T>
    {
        protected readonly UserManager<IdentityUser> _userManager;

        public BaseAuthenticatedController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options)
        {
            _userManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //TODO: This should be done on register action. Find a way to do it there, later.
            UserSettingBusiness userSettingBusiness = new UserSettingBusiness(_dbContextOptions);
            var userSettings = userSettingBusiness.GetUserSettings(UserId);
            bool hasDefaultBudget = userSettings.DefaultBudgetId > 0;
            if (hasDefaultBudget == false)
            {
                BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);
                var firstBudget = budgetBusiness.GetBudgetsOfUser(UserId).FirstOrDefault();
                if (firstBudget != null)
                {
                    //SetDefaultBudgetForUser
                    userSettingBusiness.UpdateUserSettings(UserId, firstBudget.Id);
                    BudgetId = firstBudget.Id;
                }
                else
                {
                    budgetBusiness.CreateNewBudget("Default Budget", UserId);
                    var budget = budgetBusiness.GetBudgetsOfUser(UserId).FirstOrDefault();
                    //SetDefaultBudgetForUser
                    userSettingBusiness.UpdateUserSettings(UserId, budget.Id);
                    BudgetId = budget.Id;
                }
            }
            else
            {
                BudgetId = userSettings.DefaultBudgetId;
            }
        }

        protected string UserId
        {
            get
            {
                return _userManager.GetUserId(User);
            }
        }

        private int defaultBudgetId;
        protected int BudgetId
        {
            get { return defaultBudgetId; }
            private set
            {
                defaultBudgetId = value;
            }
        }
    }
}
