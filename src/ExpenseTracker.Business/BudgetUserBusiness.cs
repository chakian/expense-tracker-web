using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Business
{
    public class BudgetUserBusiness
    {
        private readonly ExpenseTrackerDbContext _context;

        public BudgetUserBusiness(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _context = new ExpenseTrackerDbContext(options);
        }
        public BudgetUserBusiness(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public void AddUserForBudget(Budget budget, string userId)
        {
            BudgetUser budgetUser = new BudgetUser()
            {
                Budget = budget,
                UserId = userId,
                Role = 1
            };
            budgetUser.InsertUserId = userId;
            budgetUser.InsertTime = DateTime.UtcNow;
            budgetUser.IsActive = true;

            _context.BudgetUsers.Add(budgetUser);
        }
    }
}
