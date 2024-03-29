﻿using ExpenseTracker.Business;
using ExpenseTracker.Business.Commands;
using ExpenseTracker.Business.Queries;
using ExpenseTracker.Common.Contracts.Command;
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
        private GetUserSettingsQuery _queryUserSettings;

        [Obsolete]
        public BaseAuthenticatedController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options)
        {
            _userManager = userManager;
        }

        public BaseAuthenticatedController(ILogger<T> logger, 
            ExpenseTrackerDbContext context, 
            UserManager<IdentityUser> userManager,
            GetUserSettingsQuery queryUserSettings)
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
            _queryUserSettings = new GetUserSettingsQuery(_dbContext);
            return _queryUserSettings.Retrieve(new Common.Contracts.Query.UserSetting.GetUserSettingsRequest()
            {
                UserId = UserId
            }).UserSetting;
        }
        private void CreateUserSetting()
        {
            var command = new CreateUserSettingsCommand(_dbContext);
            var budgetId = FindFirstBudgetId();
            var request = new CreateUserSettingsRequest() { DefaultBudgetId = budgetId, UserId = UserId };
            
            command.Execute(request);
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
            CreateNewBudgetCommand createNewBudgetCommand = new CreateNewBudgetCommand(_dbContext);
            var createResponse = createNewBudgetCommand.Execute(new CreateNewBudgetRequest() { BudgetName = "Default Budget", UserId = UserId });

            AddUserToBudgetCommand addUserToBudgetCommand = new AddUserToBudgetCommand(_dbContext);
            addUserToBudgetCommand.Execute(new AddUserToBudgetRequest() { UserId = UserId, BudgetId = createResponse.CreatedBudgetId });

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
