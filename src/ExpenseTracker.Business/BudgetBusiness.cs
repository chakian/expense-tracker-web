using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class BudgetBusiness
    {
        private readonly ExpenseTrackerDbContext _context;
        public BudgetBusiness(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _context = new ExpenseTrackerDbContext(options);
        }

        public int CreateNewBudget(string name, string userId)
        {
            Budget budget = new Budget()
            {
                Name = name
            };
            budget.InsertUserId = userId;
            budget.InsertTime = DateTime.UtcNow;
            budget.IsActive = true;

            _context.Budgets.Add(budget);

            BudgetUserBusiness budgetUserBusiness = new BudgetUserBusiness(_context);
            budgetUserBusiness.AddUserForBudget(budget, userId);

            _context.SaveChanges();

            return budget.Id;
        }

        public List<Common.Entities.Budget> GetBudgetsOfUser(string userId)
        {
            var budgetUserList = _context.BudgetUsers.Where(bu => bu.UserId == userId && bu.IsActive).Select(bu => bu.BudgetId).ToList();
            var budgetDboList = _context.Budgets.Where(b => budgetUserList.Contains(b.Id) && b.IsActive).ToList();
            List<Common.Entities.Budget> budgetList = new List<Common.Entities.Budget>();
            budgetDboList.ForEach(b =>
            {
                budgetList.Add(new Common.Entities.Budget()
                {
                    Id = b.Id,
                    Name = b.Name,
                    IsActive = b.IsActive
                });
            });
            return budgetList;
        }

        public Common.Entities.Budget GetBudgetDetails(int id)
        {
            var budgetDbo = _context.Budgets.SingleOrDefault(b => b.Id == id);
            if (budgetDbo != null)
            {
                return new Common.Entities.Budget()
                {
                    Id = budgetDbo.Id,
                    Name = budgetDbo.Name,
                    IsActive = budgetDbo.IsActive
                };
            }
            else
            {
                return new Common.Entities.Budget();
            }
        }

        public void UpdateBudget(int budgetId, string name, string userId)
        {
            Budget budget = _context.Budgets.Find(budgetId);

            if (budget != null)
            {
                budget.Name = name;

                budget.UpdateUserId = userId;
                budget.UpdateTime = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }

        public void UpdateBudgetAsInactive(int budgetId, string userId)
        {
            Budget budget = _context.Budgets.Find(budgetId);

            if (budget != null)
            {
                budget.IsActive = false;
                budget.UpdateUserId = userId;
                budget.UpdateTime = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }
    }
}
