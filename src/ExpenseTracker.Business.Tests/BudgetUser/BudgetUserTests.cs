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
            var context = CreateContext();
            BudgetUserBusiness budgetUserBusiness = new BudgetUserBusiness(context);

            string userId = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);
            var budget = context.Budgets.First(b => b.Id == budgetId);
            string secondUserId = Guid.NewGuid().ToString();

            //ACT
            budgetUserBusiness.AddUserForBudget(budget, secondUserId);
            var actual = context.BudgetUsers.FirstOrDefault(us => us.BudgetId==budgetId && us.UserId == secondUserId);

            //ASSERT
            Assert.Null(actual);
        }
    }
}
