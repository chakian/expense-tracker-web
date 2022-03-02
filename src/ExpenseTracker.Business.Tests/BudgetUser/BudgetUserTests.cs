using ExpenseTracker.Business.Commands;
using ExpenseTracker.Common.Contracts.Command;
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

            string userId = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(context, userId);
            var budget = context.Budgets.First(b => b.Id == budgetId);
            string secondUserId = Guid.NewGuid().ToString();

            AddUserToBudgetCommand addUserToBudgetCommand = new AddUserToBudgetCommand(context);

            //ACT
            addUserToBudgetCommand.Execute(new AddUserToBudgetRequest() { UserId = secondUserId, BudgetId = budget.Id });
            var actual = context.BudgetUsers.FirstOrDefault(us => us.BudgetId==budgetId && us.UserId == secondUserId);

            //ASSERT
            Assert.Null(actual);
        }
    }
}
