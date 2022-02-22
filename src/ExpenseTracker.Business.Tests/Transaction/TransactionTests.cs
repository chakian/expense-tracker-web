using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using System;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class TransactionTests : BudgetRelatedTestBase
    {
        [Fact]
        public void Add_Transaction_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            var context = CreateContext(contextOptions);
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);
            AccountBusiness accountBusiness = new AccountBusiness(context);

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            int accountId = accountBusiness.CreateNewAccount(budgetId, Guid.NewGuid().ToString(), 10, 0, userId);
            int categoryId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            decimal amount = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string description = Guid.NewGuid().ToString();
            var request = new CreateTransactionRequest()
            {
                BudgetId = budgetId,
                Date = DateTime.Now,
                AccountId = accountId,
                CategoryId = categoryId,
                Amount = amount,
                IsIncome = false,
                Description = description
            };

            //ACT
            var actualTx = transactionBusiness.CreateTransaction(request);
            var actual = new ExpenseTrackerDbContext(contextOptions).Transactions.FirstOrDefault(t => t.Id == actualTx.Id);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(description, actual.Description);
        }

        [Fact]
        public void Get_TransactionsOfBudget_Empty()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            var context = CreateContext(contextOptions);
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);

            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            DateTime startDate = new DateTime(2020, 11, 1);
            DateTime endDate = new DateTime(2020, 11, 30);

            //ACT
            var actual = transactionBusiness.GetTransactionsOfBudgetForPeriod(budgetId, startDate, endDate);

            //ASSERT
            Assert.Empty(actual);
        }

        //TODO: Create real account and category for this to work
        [Fact]
        public void Get_TransactionsOfBudget_NotEmpty()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            var dbctx = new ExpenseTrackerDbContext(contextOptions);
            TransactionBusiness transactionBusiness = new TransactionBusiness(dbctx);

            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            decimal amount = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string description = Guid.NewGuid().ToString();

            var currentDate = DateTime.Now;
            dbctx.Transactions.Add(new Persistence.DbModels.Transaction()
            {
                BudgetId = budgetId,
                Amount = amount,
                Description = description,
                Date = currentDate,
                IsActive = true
            });
            dbctx.SaveChanges();

            var beginningDate = currentDate.AddMinutes(-1);
            var endDate = currentDate.AddMinutes(1);

            //ACT
            var actual = transactionBusiness.GetTransactionsOfBudgetForPeriod(budgetId, beginningDate, endDate);

            //ASSERT
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Get_TransactionDetails_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            var context = CreateContext(contextOptions);
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);

            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            decimal amount = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string description = Guid.NewGuid().ToString();

            var dbctx = new ExpenseTrackerDbContext(contextOptions);

            var acc = new Persistence.DbModels.Account()
            {
                Name = "test"
            };
            dbctx.Accounts.Add(acc);
            var cat = new Persistence.DbModels.Category()
            {
                Name = "test"
            };
            dbctx.Categories.Add(cat);
            dbctx.SaveChanges();

            var tx = new Persistence.DbModels.Transaction()
            {
                BudgetId = budgetId,
                Amount = amount,
                Description = description,
                Account = acc,
                Category = cat,
                IsActive = true
            };
            dbctx.Transactions.Add(tx);
            dbctx.SaveChanges();

            var txId = tx.Id;

            //ACT
            var actual = transactionBusiness.GetTransactionDetails(txId);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(description, actual.Description);
        }

        [Fact]
        public void Delete_Transaction_ExpectNull()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            var context = CreateContext(contextOptions);
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);
            AccountBusiness accountBusiness = new AccountBusiness(context);

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            int accountId = accountBusiness.CreateNewAccount(budgetId, Guid.NewGuid().ToString(), 10, 0, userId);
            decimal amount = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string description = Guid.NewGuid().ToString();

            var dbctx = new ExpenseTrackerDbContext(contextOptions);
            var tx = new Persistence.DbModels.Transaction()
            {
                BudgetId = budgetId,
                Amount = amount,
                Description = description,
                AccountId = accountId,
                IsActive = true
            };
            dbctx.Transactions.Add(tx);
            dbctx.SaveChanges();

            var txId = tx.Id;

            //ACT
            transactionBusiness.DeleteTransaction(txId, userId);
            var actual = dbctx.Transactions.FirstOrDefault(t => t.Id == txId);

            //ASSERT
            Assert.Null(actual);
        }
    }
}
