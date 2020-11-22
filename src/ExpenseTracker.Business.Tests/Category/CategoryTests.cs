using System;
using System.Collections.Generic;
using Xunit;

namespace ExpenseTracker.Business.Tests
{
    public class CategoryTests : BudgetRelatedTestBase
    {
        [Fact]
        public void Create_Category_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            CategoryBusiness categoryBusiness = new CategoryBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string categoryName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);

            //ACT
            int categoryId = categoryBusiness.CreateNewCategory(budgetId, categoryName, null, userId);
            Common.Entities.Category actual = categoryBusiness.GetCategoryDetails(categoryId);

            //ASSERT
            Assert.NotEqual(0, actual.Id);
            Assert.True(actual.IsActive);
        }

        [Fact]
        public void Get_CategoryDetails_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            CategoryBusiness categoryBusiness = new CategoryBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string categoryName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            int categoryId = categoryBusiness.CreateNewCategory(budgetId, categoryName, null, userId);

            //ACT
            Common.Entities.Category actual = categoryBusiness.GetCategoryDetails(categoryId);

            //ASSERT
            Assert.Equal(categoryName, actual.Name);
        }

        [Fact]
        public void Get_CategoriesOfBudget_NotEmpty()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            CategoryBusiness categoryBusiness = new CategoryBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string categoryName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            categoryBusiness.CreateNewCategory(budgetId, categoryName, null, userId);

            //ACT
            List<Common.Entities.Category> actual = categoryBusiness.GetCategoriesOfBudget(budgetId);

            //ASSERT
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Update_Category_Valid()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            CategoryBusiness categoryBusiness = new CategoryBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string categoryName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            int categoryId = categoryBusiness.CreateNewCategory(budgetId, categoryName, null, userId);

            //ACT
            string newName = Guid.NewGuid().ToString();
            categoryBusiness.UpdateCategory(categoryId, newName, userId);
            Common.Entities.Category actual = categoryBusiness.GetCategoryDetails(categoryId);

            //ASSERT
            // TODO: This sometimes may fail. It's better to check if the new name is not equal to prev.
            Assert.NotEqual(categoryName, actual.Name);
            Assert.Equal(newName, actual.Name);
        }

        [Fact]
        public void Update_CategoryAsInactive_IsInactive()
        {
            //ARRANGE
            var contextOptions = CreateNewContextOptions();
            CategoryBusiness categoryBusiness = new CategoryBusiness(contextOptions);

            string userId = Guid.NewGuid().ToString();
            string categoryName = Guid.NewGuid().ToString();
            int budgetId = CreateBudget(contextOptions, userId);
            int categoryId = categoryBusiness.CreateNewCategory(budgetId, categoryName, null, userId);

            //ACT
            categoryBusiness.UpdateCategoryAsInactive(categoryId, userId);
            Common.Entities.Category actual = categoryBusiness.GetCategoryDetails(categoryId);

            //ASSERT
            Assert.False(actual.IsActive);
        }
    }
}
