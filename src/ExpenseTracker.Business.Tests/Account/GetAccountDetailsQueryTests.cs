using ExpenseTracker.Business.Queries;
using ExpenseTracker.Common.Contracts.Query;
using ExpenseTracker.Persistence.DbModels;
using System;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class GetAccountDetailsQueryTests : BudgetRelatedTestBase
    {
        [Fact]
        public void Get_AccountDetails_Valid()
        {
            //ARRANGE
            var context = CreateContext();

            string userId = Guid.NewGuid().ToString();
            string accountName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);

            var account = new Account { BudgetId = budgetId, Name = accountName, AccountType = 10, Balance = 0 };
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
            int accountId = account.Id;

            var request = new GetAccountDetailsRequest() { AccountId = accountId, UserId = userId };
            var query = new GetAccountDetailsQuery(context);

            //ACT
            var actual = query.Retrieve(request).Account;

            //ASSERT
            Assert.Equal(accountName, actual.Name);
        }
    }
}
