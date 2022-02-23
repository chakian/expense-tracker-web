using ExpenseTracker.CommandQuery.Queries;
using ExpenseTracker.CommandQuery.Tests.Base;
using ExpenseTracker.Common.Contracts.Query.UserSetting;
using ExpenseTracker.Common.Interfaces.Business;
using ExpenseTracker.Persistence;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ExpenseTracker.CommandQuery.Tests.Query.UserSetting
{
    public class QueryUserSettingsTests : TestBase
    {
        [Fact]
        public void Get_UserSetting_Valid()
        {
            //ARRANGE
            string userId = Guid.NewGuid().ToString();
            int expectedBudgetId = 1;

            var userSettingBusinessMock = new Mock<IUserSettingBusiness>();
            userSettingBusinessMock.Setup(bus => bus.GetUserSettings(It.IsAny<string>())).Returns(new Common.Entities.UserSetting()
            {
                DefaultBudgetId = expectedBudgetId,
                IsActive = true,
                UserId = userId
            });

            var query = new QueryUserSettings(GetMockContext(), userSettingBusinessMock.Object);
            var request = new GetUserSettingsRequest()
            {
                UserId = userId,
            };

            //ACT
            var actual = query.HandleQuery(request);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(expectedBudgetId, actual.DefaultBudgetId);
        }

        [Fact]
        public void Get_UserSetting_Null()
        {
            //ARRANGE
            string userId = Guid.NewGuid().ToString();
            int expectedBudgetId = 0;

            var userSettingBusinessMock = new Mock<IUserSettingBusiness>();
            Common.Entities.UserSetting userSetting = null;
            userSettingBusinessMock.Setup(bus => bus.GetUserSettings(It.IsAny<string>())).Returns(userSetting);

            var query = new QueryUserSettings(GetMockContext(), userSettingBusinessMock.Object);
            var request = new GetUserSettingsRequest()
            {
                UserId = userId,
            };

            //ACT
            var actual = query.HandleQuery(request);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(expectedBudgetId, actual.DefaultBudgetId);
        }
    }
}
