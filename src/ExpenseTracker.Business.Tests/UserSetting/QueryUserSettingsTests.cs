using ExpenseTracker.Business.Queries;
using ExpenseTracker.Common.Contracts.Query.UserSetting;
using ExpenseTracker.Persistence.DbModels;
using Moq;
using System;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class QueryUserSettingsTests : TestBase
    {
        [Fact]
        public void Get_UserSetting_Valid()
        {
            //ARRANGE
            var context = CreateContext();

            string userId = Guid.NewGuid().ToString();
            int expectedBudgetId = 1;

            var userSettings = new UserSetting()
            {
                DefaultBudgetId = expectedBudgetId,
                IsActive = true,
                UserId = userId
            };
            context.Entry(userSettings).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();

            var query = new GetUserSettingsQuery(context);
            var request = new GetUserSettingsRequest()
            {
                UserId = userId,
            };

            //ACT
            var actual = query.Retrieve(request);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(expectedBudgetId, actual.UserSetting.DefaultBudgetId);
        }

        [Fact]
        public void Get_UserSetting_Null()
        {
            //ARRANGE
            var context = CreateContext();

            string userId = Guid.NewGuid().ToString();

            var query = new GetUserSettingsQuery(context);
            var request = new GetUserSettingsRequest()
            {
                UserId = userId,
            };

            //ACT
            var actual = query.Retrieve(request);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Null(actual.UserSetting);
        }
    }
}
