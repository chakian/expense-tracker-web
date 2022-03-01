using ExpenseTracker.Common.Enums;
using ExpenseTracker.Persistence.DbModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class AccountTests : BudgetRelatedTestBase
    {
        [Fact]
        public void Get_AccountDetails_Valid()
        {
            //ARRANGE
            var context = CreateContext();
            AccountBusiness accountBusiness = new AccountBusiness(context);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);

            var account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = 0 };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
            int accountId = account.Id;

            //ACT
            Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);

            //ASSERT
            Assert.Equal(accountName, actual.Name);
        }

        //[Fact]
        //public void Update_Account_ChangeName_Valid()
        //{
        //    //ARRANGE
        //    var contextOptions = CreateNewContextOptions();
        //    var context = CreateContext(contextOptions);
        //    AccountBusiness accountBusiness = new AccountBusiness(context);

        //    string userId = Guid.NewGuid().ToString();
        //    string accountName = Guid.NewGuid().ToString();
        //    int budgetId = CreateBudget(contextOptions, userId);
        //    decimal balance = 100;
        //    int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, (int)AccountType.Cash, balance, userId);

        //    //ACT
        //    string newName = Guid.NewGuid().ToString();
        //    accountBusiness.UpdateAccount(accountId, newName, balance, userId);
        //    Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);

        //    //ASSERT
        //    // TODO: This sometimes may fail because of guid generation (?). It's better to check if the new name is not equal to prev.
        //    Assert.NotEqual(accountName, actual.Name);
        //    Assert.Equal(newName, actual.Name);
        //}

        //[Fact]
        //public void Update_AccountAsInactive_IsInactive()
        //{
        //    //ARRANGE
        //    var contextOptions = CreateNewContextOptions();
        //    var context = CreateContext(contextOptions);
        //    AccountBusiness accountBusiness = new AccountBusiness(context);

        //    string userId = Guid.NewGuid().ToString();
        //    string accountName = Guid.NewGuid().ToString();
        //    int budgetId = CreateBudget(contextOptions, userId);
        //    int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, 10, 0, userId);

        //    //ACT
        //    accountBusiness.UpdateAccountAsInactive(accountId, userId);
        //    Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);

        //    //ASSERT
        //    Assert.False(actual.IsActive);
        //}
    }
}
