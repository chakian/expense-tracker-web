using ExpenseTracker.Common.Interfaces.Business;
using ExpenseTracker.Persistence;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class UserSettingBusiness : BaseBusiness, IUserSettingBusiness
    {
        public UserSettingBusiness(ExpenseTrackerDbContext context) : base(context)
        {
        }

        public void UpdateUserSettings(string userId, int budgetId)
        {
            var settingsDbo = dbContext.UserSettings.Single(us => us.UserId == userId);

            settingsDbo.DefaultBudgetId = budgetId;

            dbContext.Entry(settingsDbo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
