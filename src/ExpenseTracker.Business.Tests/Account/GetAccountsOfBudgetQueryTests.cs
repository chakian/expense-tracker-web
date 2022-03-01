using ExpenseTracker.Business.Queries;
using ExpenseTracker.Common.Contracts.Query;
using ExpenseTracker.Persistence.DbModels;
using System;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class GetAccountsOfBudgetQueryTests : BudgetRelatedTestBase
    {
        [Fact]
        public void GetAccountsOfBudget_NotEmpty()
        {
            //ARRANGE
            var context = CreateContext();
            var query = new GetAccountsOfBudgetQuery(context);

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);

            var account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = 0, IsActive = true };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();

            var request = new GetAccountsOfBudgetRequest()
            {
                BudgetId = budgetId,
            };

            //ACT
            var actual = query.Retrieve(request);

            //ASSERT
            Assert.NotEmpty(actual.AccountList);
        }
    }
}
