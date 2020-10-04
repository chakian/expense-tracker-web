using System;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class AccountTests : TestBase
    {
        [Fact]
        public void Create_Account_Valid()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            AccountBusiness accountBusiness = new AccountBusiness(contextOptions);
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            string budgetName = Guid.NewGuid().ToString();
            int budgetId = budgetBusiness.CreateNewBudget(budgetName, userId);

            //act
            int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, userId);

            //assert
            Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);
            Assert.NotEqual(0, actual.Id);
            Assert.True(actual.IsActive);
        }
    }
}
