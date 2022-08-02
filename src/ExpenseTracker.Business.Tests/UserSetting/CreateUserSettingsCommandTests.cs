using ExpenseTracker.Business.Commands;
using ExpenseTracker.Common.Contracts.Command;
using System;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class CreateUserSettingsCommandTests : TestBase
    {
        [Fact]
        public void Create_UserSetting_Valid()
        {
            //ARRANGE
            var context = CreateContext();

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);

            var command = new CreateUserSettingsCommand(context);
            var request = new CreateUserSettingsRequest() { DefaultBudgetId = budgetId, UserId = userId };

            //ACT
            command.Execute(request);
            var actual = context.UserSettings.FirstOrDefault(us => us.UserId == userId);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(budgetId, actual.DefaultBudgetId);
        }
    }
}
