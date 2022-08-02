using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class CategoryBusiness
    {
        private readonly ExpenseTrackerDbContext _context;
        public CategoryBusiness(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _context = new ExpenseTrackerDbContext(options);
        }
        public CategoryBusiness(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public int CreateNewCategory(int budgetId, string name, int? parentCategoryId, string userId)
        {
            Category category = new Category()
            {
                BudgetId = budgetId,
                Name = name,
                ParentCategoryId = parentCategoryId
            };
            category.InsertUserId = userId;
            category.InsertTime = DateTime.UtcNow;
            category.IsActive = true;

            _context.Categories.Add(category);

            _context.SaveChanges();

            return category.Id;
        }

        //private Category GetById() { }

        public List<Common.Entities.Category> GetCategoriesOfBudget(int budgetId)
        {
            List<Common.Entities.Category> CategoryList = new List<Common.Entities.Category>();
            _context.Categories.AsQueryable().Where(b => b.BudgetId == budgetId && b.IsActive).ToList().ForEach(b =>
            {
                CategoryList.Add(new Common.Entities.Category()
                {
                    Id = b.Id,
                    BudgetId = b.BudgetId,
                    Name = b.Name
                });
            });
            return CategoryList;
        }

        public Common.Entities.Category GetCategoryDetails(int id)
        {
            var categoryDbo = _context.Categories.AsQueryable().SingleOrDefault(b => b.Id == id);
            if (categoryDbo != null)
            {
                return new Common.Entities.Category()
                {
                    Id = categoryDbo.Id,
                    BudgetId = categoryDbo.BudgetId,
                    Name = categoryDbo.Name,
                    IsActive = categoryDbo.IsActive
                };
            }
            else
            {
                return new Common.Entities.Category();
            }
        }

        public void UpdateCategory(int categoryId, string name, string userId)
        {
            Category category = _context.Categories.Find(categoryId);

            if (category != null)
            {
                category.Name = name;

                category.UpdateUserId = userId;
                category.UpdateTime = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }

        public void UpdateCategoryAsInactive(int categoryId, string userId)
        {
            Category Category = _context.Categories.Find(categoryId);

            if (Category != null)
            {
                Category.IsActive = false;
                Category.UpdateUserId = userId;
                Category.UpdateTime = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }
    }
}
