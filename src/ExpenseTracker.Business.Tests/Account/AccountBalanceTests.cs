using ExpenseTracker.Business.Helpers;
using ExpenseTracker.Persistence.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class AccountBalanceTests : BudgetRelatedTestBase
    {
        [Theory]
        [InlineData(1000, 100, false, 900)]
        [InlineData(1000, 100, true, 1100)]
        public void UpdateBalance_ForNewTransaction_Ok(decimal startBalance, decimal transactionAmount, bool isIncome, decimal expectedEndBalance)
        {
            //ARRANGE
            var context = CreateContext();

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);

            var account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = startBalance };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
            int accountId = account.Id;

            var balanceHelper = new AccountBalanceHelper(context);

            //ACT
            balanceHelper.UpdateAccountBalancesForNewTransaction(accountId, null, transactionAmount, isIncome, userId);
            var actual = context.Accounts.Single(account => account.Id == accountId);

            //ASSERT
            Assert.Equal(expectedEndBalance, actual.Balance);
        }

        /*
        TESTING SCENARIOS
        Old Src  | Old Targt | New Src | New Targt | Old Amt | New Amt | Old Inc? | New Inc?
        A        | B         | A       | B         | 10      | 20      | False    | False
        A        | B         | A       | B         | 10      | 10      | False    | True
        A        | B         | A       | C         | 10      | 10      | False    | False
        A        | B         | C       | B         | 10      | 10      | False    | False
        A        | B         | C       | D         | 10      | 10      | False    | False
        A        | B         | B       | A         | 10      | 10      | False    | False
        A        | B         | B       | C         | 10      | 10      | False    | False
        A        | B         | C       | A         | 10      | 10      | False    | False
        */
        [Theory]
        [InlineData(0, 1, 0, 1, 10, 20, false, false, 100, new string[] { "80", "120", "100", "100" })]
        [InlineData(0, 1, 0, 1, 10, 10, false, true, 100, new string[] { "110", "90", "100", "100" })]
        [InlineData(0, 1, 0, 2, 10, 10, false, false, 100, new string[] { "90", "100", "110", "100" })]
        [InlineData(0, 1, 2, 1, 10, 10, false, false, 100, new string[] { "100", "110", "90", "100" })]
        [InlineData(0, 1, 2, 3, 10, 10, false, false, 100, new string[] { "100", "100", "90", "110" })]
        [InlineData(0, 1, 1, 0, 10, 10, false, false, 100, new string[] { "110", "90", "100", "100" })]
        [InlineData(0, 1, 1, 2, 10, 10, false, false, 100, new string[] { "100", "90", "110", "100" })]
        [InlineData(0, 1, 2, 0, 10, 10, false, false, 100, new string[] { "110", "100", "90", "100" })]
        public void UpdateBalance_ForUpdatedTransaction(int oldSource, int oldTarget,
            int source, int target,
            decimal initialAmount, decimal updatedAmount,
            bool initialIsIncome, bool updatedIsIncome,
            decimal initialBalance,
            string[] expectedBalancesString)
        {
            //ARRANGE
            var context = CreateContext();

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);
            List<int> accountIds = new List<int>();

            var account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = initialBalance };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            accountIds.Add(account.Id);
            account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = initialBalance };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            accountIds.Add(account.Id);
            account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = initialBalance };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            accountIds.Add(account.Id);
            account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = initialBalance };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            accountIds.Add(account.Id);
            context.SaveChanges();

            decimal[] expectedBalances = new decimal[expectedBalancesString.Length];
            for(int i = 0; i < expectedBalancesString.Length; i++)
            {
                expectedBalances[i] = decimal.Parse(expectedBalancesString[i]);
            }

            var balanceHelper = new AccountBalanceHelper(context);

            balanceHelper.UpdateAccountBalancesForNewTransaction(accountIds[oldSource], accountIds[oldTarget], initialAmount, initialIsIncome, userId);

            // ACT
            balanceHelper.UpdateAccountBalancesForEditedTransaction(accountIds[source], accountIds[target], updatedAmount, updatedIsIncome, accountIds[oldSource], accountIds[oldTarget], initialAmount, initialIsIncome, userId);
            var actualSource = context.Accounts.Single(account => account.Id == accountIds[source]);
            var actualTarget = context.Accounts.Single(account => account.Id == accountIds[target]);

            // ASSERT
            Assert.Equal(expectedBalances[source], actualSource.Balance);
            Assert.Equal(expectedBalances[target], actualTarget.Balance);
        }

        //[Fact]
        //public void Update_Account_IncreaseBalance_Valid()
        //{
        //    //ARRANGE
        //    var contextOptions = CreateNewContextOptions();
        //    var context = CreateContext(contextOptions);
        //    AccountBusiness accountBusiness = new AccountBusiness(context);
        //    TransactionBusiness transactionBusiness = new TransactionBusiness(context);

        //    string userId = Guid.NewGuid().ToString();
        //    string accountName = Guid.NewGuid().ToString();
        //    int budgetId = CreateBudget(contextOptions, userId);
        //    decimal initialBalance = 100;
        //    int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, (int)AccountType.Cash, initialBalance, userId);

        //    int year = DateTime.Now.Year;
        //    int month = DateTime.Now.Month;
        //    decimal newBalance = 200;
        //    decimal expectedAmount = 100;

        //    //ACT
        //    accountBusiness.UpdateAccount(accountId, accountName, newBalance, userId);
        //    Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);
        //    var actualTransaction = transactionBusiness.GetTransactionsOfBudgetForPeriod(budgetId, new DateTime(year, month, 1, 0, 0, 0, 0), new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999))[0];

        //    //ASSERT
        //    // TODO: This sometimes may fail because . It's better to check if the new name is not equal to prev.
        //    Assert.Equal(accountName, actual.Name);
        //    Assert.Equal(accountId, actualTransaction.AccountId);
        //    Assert.Equal(expectedAmount, actualTransaction.Amount);
        //    Assert.True(actualTransaction.IsIncome);
        //}

        //[Fact]
        //public void Update_Account_DecreaseBalance_Valid()
        //{
        //    //ARRANGE
        //    var contextOptions = CreateNewContextOptions();
        //    var context = CreateContext(contextOptions);
        //    AccountBusiness accountBusiness = new AccountBusiness(context);
        //    TransactionBusiness transactionBusiness = new TransactionBusiness(context);

        //    string userId = Guid.NewGuid().ToString();
        //    string accountName = Guid.NewGuid().ToString();
        //    int budgetId = CreateBudget(contextOptions, userId);
        //    decimal initialBalance = 200;
        //    int accountId = accountBusiness.CreateNewAccount(budgetId, accountName, (int)AccountType.Cash, initialBalance, userId);

        //    int year = DateTime.Now.Year;
        //    int month = DateTime.Now.Month;
        //    decimal newBalance = 100;
        //    decimal expectedAmount = 100;

        //    //ACT
        //    accountBusiness.UpdateAccount(accountId, accountName, newBalance, userId);
        //    Common.Entities.Account actual = accountBusiness.GetAccountDetails(accountId);
        //    var actualTransaction = transactionBusiness.GetTransactionsOfBudgetForPeriod(budgetId, new DateTime(year, month, 1, 0, 0, 0, 0), new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999))[0];

        //    //ASSERT
        //    // TODO: This sometimes may fail because . It's better to check if the new name is not equal to prev.
        //    Assert.Equal(accountName, actual.Name);
        //    Assert.Equal(accountId, actualTransaction.AccountId);
        //    Assert.Equal(expectedAmount, actualTransaction.Amount);
        //    Assert.False(actualTransaction.IsIncome);
        //}
    }
}
