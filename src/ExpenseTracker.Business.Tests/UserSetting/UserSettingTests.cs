using ExpenseTracker.Persistence;
using System;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class UserSettingTests : TestBase
    {
        [Fact]
        public void Create_UserSetting_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            var dbcntx = CreateContext(contextOptions);
            UserSettingBusiness userSettingBusiness = new UserSettingBusiness(dbcntx);

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);

            //ACT
            userSettingBusiness.CreateUserSettings(userId, budgetId);
            var actual = new ExpenseTrackerDbContext(contextOptions).UserSettings.FirstOrDefault(us => us.UserId == userId);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(budgetId, actual.DefaultBudgetId);
        }

        [Fact]
        public void Get_UserSetting_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            var dbcntx = CreateContext(contextOptions);
            UserSettingBusiness userSettingBusiness = new UserSettingBusiness(dbcntx);

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);

            var dbctx = new ExpenseTrackerDbContext(contextOptions);
            dbctx.UserSettings.Add(new Persistence.DbModels.UserSetting()
            {
                UserId = userId,
                DefaultBudgetId = budgetId
            });
            dbctx.SaveChanges();

            //ACT
            var actual = userSettingBusiness.GetUserSettings(userId);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(budgetId, actual.DefaultBudgetId);
        }
    }
}
