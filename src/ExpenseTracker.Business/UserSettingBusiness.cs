using ExpenseTracker.Persistence;
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

            if(settingsDbo == null)
            {

            }
            Common.Entities.UserSetting userSetting = new Common.Entities.UserSetting()
            {
                UserId = settingsDbo.UserId,
                DefaultBudgetId = settingsDbo.DefaultBudgetId
            };

            return userSetting;
        }
    }
}
