using ExpenseTracker.Persistence;
using System;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class BudgetUserTests : BudgetRelatedTestBase
    {
        [Fact]
        public void Add_UserForBudget_ExpectNotAdded()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            BudgetUserBusiness budgetUserBusiness = new BudgetUserBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            var budget = new ExpenseTrackerDbContext(contextOptions).Budgets.First(b => b.Id == budgetId);
            string secondUserId = Guid.NewGuid().ToString();

            //ACT
            budgetUserBusiness.AddUserForBudget(budget, secondUserId);
            var actual = new ExpenseTrackerDbContext(contextOptions).BudgetUsers.FirstOrDefault(us => us.BudgetId==budgetId && us.UserId == secondUserId);

            //ASSERT
            Assert.Null(actual);
        }
    }
}
