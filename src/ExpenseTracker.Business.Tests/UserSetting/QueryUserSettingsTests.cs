using ExpenseTracker.Business.Queries;
using ExpenseTracker.Common.Contracts.Query.UserSetting;
using ExpenseTracker.Common.Interfaces.Business;
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
            string userId = Guid.NewGuid().ToString();
            int expectedBudgetId = 1;

            var userSettingBusinessMock = new Mock<IUserSettingBusiness>();
            userSettingBusinessMock.Setup(bus => bus.GetUserSettings(It.IsAny<string>())).Returns(new Common.Entities.UserSetting()
            {
                DefaultBudgetId = expectedBudgetId,
                IsActive = true,
                UserId = userId
            });

            var query = new GetUserSettingsQuery(GetMockContext(), userSettingBusinessMock.Object);
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
            string userId = Guid.NewGuid().ToString();

            var userSettingBusinessMock = new Mock<IUserSettingBusiness>();
            Common.Entities.UserSetting userSetting = null;
            userSettingBusinessMock.Setup(bus => bus.GetUserSettings(It.IsAny<string>())).Returns(userSetting);

            var query = new GetUserSettingsQuery(GetMockContext(), userSettingBusinessMock.Object);
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
