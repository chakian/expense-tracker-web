using ExpenseTracker.Business.Commands;
using ExpenseTracker.Common.Contracts.Command;
using System;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class CreateNewAccountCommandTests : BudgetRelatedTestBase
    {
        [Fact]
        public void Create_Account_Valid()
        {
            //ARRANGE
            var context = CreateContext();
            var command = new CreateNewAccountCommand(context);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);

            var commandRequest = new CreateNewAccountRequest()
            {
                BudgetId = budgetId,
                AccountName = accountName,
                AccountType = 10,
                AccountBalance = 0,
                UserId = userId,
            };

            //ACT
            int accountId = command.Execute(commandRequest).CreatedAccountId;
            var actual = context.Accounts.Single(x => x.Id == accountId);

            //ASSERT
            Assert.NotEqual(0, actual.Id);
            Assert.True(actual.IsActive);
        }
    }
}
