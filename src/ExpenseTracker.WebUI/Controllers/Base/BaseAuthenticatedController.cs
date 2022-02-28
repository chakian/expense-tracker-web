using ExpenseTracker.Business;
using ExpenseTracker.CommandQuery.Command;
using ExpenseTracker.CommandQuery.Queries;
using ExpenseTracker.Common.Entities;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController<T> : BaseController<T>
    {
        protected readonly UserManager<IdentityUser> _userManager;
        private QueryUserSettings _queryUserSettings;

        [Obsolete]
        public BaseAuthenticatedController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options)
        {
            _userManager = userManager;
        }

        public BaseAuthenticatedController(ILogger<T> logger, 
            ExpenseTrackerDbContext context, 
            UserManager<IdentityUser> userManager,
            QueryUserSettings queryUserSettings)
            : base(logger, context)
        {
            _userManager = userManager;
            _queryUserSettings = queryUserSettings;
        }

        #region Initiate UserSettings
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //TODO: This should be done on register action. Find a way to do it there, later.
            // Get User Settings
            var userSettings = GetUserSetting();
            // If User Settings doesn't exist create it
            if (userSettings == null)
            {
                CreateUserSetting();
                userSettings = GetUserSetting();
                BudgetId = userSettings.DefaultBudgetId;
            }
            else
            {
                BudgetId = userSettings.DefaultBudgetId;
            }

            TempData["budgetId"] = BudgetId;
        }
        private UserSetting GetUserSetting()
        {
            UserSettingBusiness userSettingBusiness = new UserSettingBusiness(_dbContext);
            _queryUserSettings = new QueryUserSettings(_dbContext, userSettingBusiness);
            return _queryUserSettings.HandleQuery(new Common.Contracts.Query.UserSetting.GetUserSettingsRequest()
            {
                UserId = UserId
            }).UserSetting;
        }
        private void CreateUserSetting()
        {
            UserSettingBusiness userSettingBusiness = new UserSettingBusiness(_dbContext);
            int budgetId = FindFirstBudgetId();
            userSettingBusiness.CreateUserSettings(UserId, budgetId);
        }
        private int FindFirstBudgetId()
        {
            //TODO: Temp solution
            BudgetBusiness budgetBusiness = new BudgetBusiness(new ExpenseTrackerDbContext(_dbContextOptions));
            var firstBudget = budgetBusiness.GetBudgetsOfUser(UserId).FirstOrDefault();
            if (firstBudget != null)
            {
                return firstBudget.Id;
            }
            else
            {
                return CreateBudgetForUser();
            }
        }
        private int CreateBudgetForUser()
        {
            CreateNewBudgetCommand createNewBudgetCommand = new CreateNewBudgetCommand(_dbContextOptions);
            createNewBudgetCommand.HandleCommand(new Common.Contracts.Command.CreateNewBudgetRequest()
            {
                BudgetName = "Default Budget",
                UserId = UserId
            });
            BudgetBusiness budgetBusiness = new BudgetBusiness(new ExpenseTrackerDbContext(_dbContextOptions));
            return budgetBusiness.GetBudgetsOfUser(UserId).First().Id;
        }
        #endregion Initiate UserSettings

        protected string UserId
        {
            get
            {
                return _userManager.GetUserId(User);
            }
        }

        protected int BudgetId { get; private set; }
    }
}
