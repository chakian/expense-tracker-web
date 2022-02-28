using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class BudgetTests : TestBase
    {
        [Fact]
        public void Create_Budget_Valid()
        {
            //arrange
            var context = CreateContext();
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            CreateNewBudgetRequest createNewBudgetRequest = new CreateNewBudgetRequest()
            {
                BudgetName = name,
                UserId = userId
            };

            //act
            int budgetId = budgetBusiness.CreateNewBudget(createNewBudgetRequest).Id;
            context.SaveChanges();

            //assert
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);
            Assert.NotEqual(0, actual.Id);
            Assert.True(actual.IsActive);
        }

        [Fact]
        public void Get_BudgetsOfUser_Empty()
        {
            //arrange
            var context = CreateContext();
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            CreateNewBudgetRequest createNewBudgetRequest = new CreateNewBudgetRequest()
            {
                BudgetName = name,
                UserId = userId
            };
            budgetBusiness.CreateNewBudget(createNewBudgetRequest);
            context.SaveChanges();

            //act
            List<Common.Entities.Budget> actual = budgetBusiness.GetBudgetsOfUser(userId);
            
            //assert
            Assert.Empty(actual);
        }

        [Fact]
        public void Get_BudgetsOfUser_AllDeleted_ShouldBeEmpty()
        {
            //arrange
            var context = CreateContext();
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            CreateNewBudgetRequest createNewBudgetRequest = new CreateNewBudgetRequest()
            {
                BudgetName = name,
                UserId = userId
            };
            int budgetId = budgetBusiness.CreateNewBudget(createNewBudgetRequest).Id;
            context.SaveChanges();
            DeactivateBudgetRequest deactivateBudgetRequest = new DeactivateBudgetRequest()
            {
                BudgetId = budgetId,
                UserId = userId
            };

            //act
            budgetBusiness.UpdateBudgetAsInactive(deactivateBudgetRequest);
            context.SaveChanges();
            List<Common.Entities.Budget> actual = budgetBusiness.GetBudgetsOfUser(userId);

            //assert
            Assert.Empty(actual);
        }

        [Fact]
        public void Get_BudgetDetails_Valid()
        {
            //arrange
            var context = CreateContext();
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            string budgetName = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            CreateNewBudgetRequest createNewBudgetRequest = new CreateNewBudgetRequest()
            {
                BudgetName = budgetName,
                UserId = userId
            };
            int budgetId = budgetBusiness.CreateNewBudget(createNewBudgetRequest).Id;
            context.SaveChanges();

            //act
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);

            //assert
            Assert.Equal(budgetName, actual.Name);
        }

        [Fact]
        public void Update_Budget_Valid()
        {
            //arrange
            var context = CreateContext();
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            CreateNewBudgetRequest createNewBudgetRequest = new CreateNewBudgetRequest()
            {
                BudgetName = name,
                UserId = userId
            };
            int budgetId = budgetBusiness.CreateNewBudget(createNewBudgetRequest).Id;
            context.SaveChanges();

            //act
            string newName = Guid.NewGuid().ToString();
            while(newName == name)
            {
                newName = Guid.NewGuid().ToString();
            }
            UpdateBudgetRequest updateBudgetRequest = new UpdateBudgetRequest()
            {
                BudgetId = budgetId,
                Name = newName,
                UserId = userId
            };
            budgetBusiness.UpdateBudget(updateBudgetRequest);
            context.SaveChanges();

            //assert
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);
            Assert.Equal(newName, actual.Name);
        }

        [Fact]
        public void Update_BudgetAsInactive_IsInactive()
        {
            //arrange
            var context = CreateContext();
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            string name = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            CreateNewBudgetRequest createNewBudgetRequest = new CreateNewBudgetRequest()
            {
                BudgetName = name,
                UserId = userId
            };
            int budgetId = budgetBusiness.CreateNewBudget(createNewBudgetRequest).Id;
            DeactivateBudgetRequest deactivateBudgetRequest = new DeactivateBudgetRequest()
            {
                BudgetId = budgetId,
                UserId = userId
            };

            //act
            budgetBusiness.UpdateBudgetAsInactive(deactivateBudgetRequest);

            //assert
            Common.Entities.Budget actual = budgetBusiness.GetBudgetDetails(budgetId);
            Assert.False(actual.IsActive);
        }

        //Update_Budget_FailRoleNotAuthorized
        //Update_BudgetAsInactive_FailRoleNotAuthorized
    }
}
