using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class UserSettingBusiness
    {
        private readonly ExpenseTrackerDbContext _context;
        public UserSettingBusiness(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _context = new ExpenseTrackerDbContext(options);
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

            _context.UserSettings.Add(userSetting);
            _context.SaveChanges();
        }

        public void UpdateUserSettings(string userId, int budgetId)
        {
            var settingsDbo = _context.UserSettings.SingleOrDefault(us => us.UserId == userId);

            settingsDbo.DefaultBudgetId = budgetId;

            settingsDbo.UpdateUserId = userId;
            settingsDbo.UpdateTime = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public Common.Entities.UserSetting GetUserSettings(string userId)
        {
            var settingsDbo = _context.UserSettings.SingleOrDefault(us => us.UserId == userId);

            Common.Entities.UserSetting userSetting = null;

            if (settingsDbo != null)
            {
                userSetting = new Common.Entities.UserSetting()
                {
                    UserId = settingsDbo.UserId,
                    DefaultBudgetId = settingsDbo.DefaultBudgetId
                };
            }

            return userSetting;
        }
    }
}
