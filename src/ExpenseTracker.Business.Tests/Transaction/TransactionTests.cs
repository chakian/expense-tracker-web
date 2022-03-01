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
            var context = CreateContext();
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
            var actual = context.Transactions.FirstOrDefault(t => t.Id == actualTx.Id);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(description, actual.Description);
        }

        [Fact]
        public void Get_TransactionsOfBudget_Empty()
        {
            //ARRANGE
            var context = CreateContext();
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
            var context = CreateContext();
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);

            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            decimal amount = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string description = Guid.NewGuid().ToString();

            context.Accounts.Add(new Persistence.DbModels.Account()
            {
                BudgetId = budgetId,
                Name = "test"
            });
            context.SaveChanges();

            context.Categories.Add(new Persistence.DbModels.Category()
            {
                BudgetId = budgetId,
                Name = "testCat"
            });
            context.SaveChanges();

            var accountId = context.Accounts.First().Id;
            var categoryId = context.Categories.First().Id;

            var currentDate = DateTime.Now;
            context.Transactions.Add(new Persistence.DbModels.Transaction()
            {
                BudgetId = budgetId,
                AccountId = accountId,
                CategoryId = categoryId,
                Amount = amount,
                Description = description,
                Date = currentDate,
                IsActive = true
            });
            context.SaveChanges();

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
            var context = CreateContext();
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);

            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            decimal amount = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string description = Guid.NewGuid().ToString();

            var acc = new Persistence.DbModels.Account()
            {
                Name = "test"
            };
            context.Accounts.Add(acc);
            var cat = new Persistence.DbModels.Category()
            {
                Name = "test"
            };
            context.Categories.Add(cat);
            context.SaveChanges();

            var tx = new Persistence.DbModels.Transaction()
            {
                BudgetId = budgetId,
                Amount = amount,
                Description = description,
                Account = acc,
                Category = cat,
                IsActive = true
            };
            context.Transactions.Add(tx);
            context.SaveChanges();

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
            var context = CreateContext();
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);
            AccountBusiness accountBusiness = new AccountBusiness(context);

            string userId = Guid.NewGuid().ToString();
            int budgetId = new Random(DateTime.Now.Millisecond).Next(0, 100);
            int accountId = accountBusiness.CreateNewAccount(budgetId, Guid.NewGuid().ToString(), 10, 0, userId);
            decimal amount = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string description = Guid.NewGuid().ToString();

            var tx = new Persistence.DbModels.Transaction()
            {
                BudgetId = budgetId,
                Amount = amount,
                Description = description,
                AccountId = accountId,
                IsActive = true
            };
            context.Transactions.Add(tx);
            context.SaveChanges();

            var txId = tx.Id;

            //ACT
            transactionBusiness.DeleteTransaction(txId, userId);
            var actual = context.Transactions.FirstOrDefault(t => t.Id == txId);

            //ASSERT
            Assert.Null(actual);
        }
    }
}
