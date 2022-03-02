using ExpenseTracker.Business;
using ExpenseTracker.Common.Interfaces.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using System;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class UserSettingBusiness : BaseBusiness, IUserSettingBusiness
    {
        public UserSettingBusiness(ExpenseTrackerDbContext context) : base(context)
        {
        }

        public void CreateUserSettings(string userId, int budgetId)
        {
            UserSetting userSetting = new UserSetting()
            {
                UserId = userId,
                DefaultBudgetId = budgetId
            };

            userSetting.IsActive = true;
            userSetting.InsertUserId = userId;
            userSetting.InsertTime = DateTime.UtcNow;

            dbContext.Entry(userSetting).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        public void UpdateUserSettings(string userId, int budgetId)
        {
            var settingsDbo = dbContext.UserSettings.Single(us => us.UserId == userId);

            settingsDbo.DefaultBudgetId = budgetId;

            dbContext.Entry(settingsDbo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
