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
            var context = CreateContext();
            UserSettingBusiness userSettingBusiness = new UserSettingBusiness(context);

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);

            //ACT
            userSettingBusiness.CreateUserSettings(userId, budgetId);
            context.SaveChanges();
            var actual = context.UserSettings.FirstOrDefault(us => us.UserId == userId);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(budgetId, actual.DefaultBudgetId);
        }

        [Fact]
        public void Get_UserSetting_Valid()
        {
            //ARRANGE
            var context = CreateContext();
            UserSettingBusiness userSettingBusiness = new UserSettingBusiness(context);

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);

            context.UserSettings.Add(new Persistence.DbModels.UserSetting()
            {
                UserId = userId,
                DefaultBudgetId = budgetId
            });
            context.SaveChanges();

            //ACT
            var actual = userSettingBusiness.GetUserSettings(userId);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(budgetId, actual.DefaultBudgetId);
        }
    }
}
