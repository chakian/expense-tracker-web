using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
        public void Create_Budget_Valid()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();

            //act
            int budgetId = budgetBusiness.CreateNewBudget(name, userId);

            //assert
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);
            Assert.NotEqual(0, actual.Id);
            Assert.True(actual.IsActive);
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

        [Fact]
        public void Get_BudgetsOfUser_NotEmpty()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            budgetBusiness.CreateNewBudget(name, userId);

            //act
            List<Common.Entities.Budget> actual = budgetBusiness.GetBudgetsOfUser(userId);
            
            //assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Get_BudgetDetails_Valid()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            string budgetName = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            int budgetId = budgetBusiness.CreateNewBudget(budgetName, userId);

            //act
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);

            //assert
            Assert.Equal(budgetName, actual.Name);
        }

        [Fact]
        public void Update_Budget_Valid()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();

            //act
            int budgetId = budgetBusiness.CreateNewBudget(name, userId);
            string newName = Guid.NewGuid().ToString();
            while(newName == name)
            {
                newName = Guid.NewGuid().ToString();
            }
            budgetBusiness.UpdateBudget(budgetId, newName, userId);

            //assert
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);
            Assert.Equal(newName, actual.Name);
        }

        [Fact]
        public void Update_BudgetAsInactive_IsInactive()
        {
            //arrange
            var contextOptions = CreateNewContextOptions();
            BudgetBusiness budgetBusiness = new BudgetBusiness(contextOptions);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();

            //act
            int budgetId = budgetBusiness.CreateNewBudget(name, userId);
            budgetBusiness.UpdateBudgetAsInactive(budgetId, userId);

            //assert
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);
            Assert.False(actual.IsActive);
        }
    }
}
