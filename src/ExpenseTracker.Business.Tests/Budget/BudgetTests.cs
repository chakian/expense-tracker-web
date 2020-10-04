using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests.Budget
{
    public class BudgetTests
    {
        private static DbContextOptions<ExpenseTrackerDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ExpenseTrackerDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Fact]
        public void Create_Budget_AssertValid()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();

            //act
            budgetBusiness.CreateNewBudget(name, userId);

            //assert
            Common.Entities.Budget actual = budgetBusiness.GetBudgetsOfUser(userId).First();
            Assert.NotEqual(0, actual.Id);
        }

        [Fact]
        public void Create_Budget_BudgetUserShouldBeCreated()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();

            //act
            budgetBusiness.CreateNewBudget(name, userId);

            //assert
            Persistence.DbModels.BudgetUser actual = new ExpenseTrackerDbContext(contextOptions).BudgetUsers.First();
            Assert.Equal(userId, actual.UserId);
        }
    }
}
