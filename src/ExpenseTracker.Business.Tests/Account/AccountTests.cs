using ExpenseTracker.Common.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class AccountTests : BudgetRelatedTestBase
    {
        [Fact]
        public void Create_Account_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            AccountBusiness accountBusiness = new AccountBusiness(contextOptions);
            
            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);

            //ACT
            int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, 10, 0, userId);
            Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);

            //ASSERT
            Assert.NotEqual(0, actual.Id);
            Assert.True(actual.IsActive);
        }

        [Fact]
        public void Get_AccountDetails_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            AccountBusiness accountBusiness = new AccountBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, 10, 0, userId);

            //ACT
            Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);

            //ASSERT
            Assert.Equal(accountName, actual.Name);
        }

        [Fact]
        public void Get_AccountsOfBudget_NotEmpty()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            AccountBusiness accountBusiness = new AccountBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            accountBusiness.CreateNewAccount(budgetId, accountName, 10, 0, userId);

            //ACT
            List<Common.Entities.Account> actual = accountBusiness.GetAccountsOfBudget(budgetId);

            //ASSERT
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Update_Account_ChangeName_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            AccountBusiness accountBusiness = new AccountBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            decimal balance = 100;
            int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, (int)AccountType.Cash, balance, userId);

            //ACT
            string newName = Guid.NewGuid().ToString();
            accountBusiness.UpdateAccount(accountId, newName, balance, userId);
            Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);

            //ASSERT
            // TODO: This sometimes may fail because of guid generation (?). It's better to check if the new name is not equal to prev.
            Assert.NotEqual(accountName, actual.Name);
            Assert.Equal(newName, actual.Name);
        }

        [Fact]
        public void Update_AccountAsInactive_IsInactive()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            AccountBusiness accountBusiness = new AccountBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, 10, 0, userId);

            //ACT
            accountBusiness.UpdateAccountAsInactive(accountId, userId);
            Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);

            //ASSERT
            Assert.False(actual.IsActive);
        }
    }
}
