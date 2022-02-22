using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpenseTracker.Business.Base
{
    public class BaseBusiness
    {
        protected readonly ExpenseTrackerDbContext dbContext;
        public BaseBusiness(ExpenseTrackerDbContext _context)
        {
            dbContext = _context;
        }

        public BaseBusiness(DbContextOptions<ExpenseTrackerDbContext> _dbContextOptions)
        {
            dbContext = new ExpenseTrackerDbContext(_dbContextOptions);
        }

        protected T CreateNewAuditableObject<T>(string userId, bool isActive = true)
            where T : BaseAuditableDbo, new()
        {
            T dbo = new T();
            dbo.InsertUserId = userId;
            dbo.InsertTime = DateTime.UtcNow;
            dbo.IsActive = isActive;
            return dbo;
        }

        protected void UpdateAuditableObject<T>(T dbo, string userId, bool isActive = true)
            where T : BaseAuditableDbo
        {
            dbo.UpdateUserId = userId;
            dbo.UpdateTime = DateTime.UtcNow;
        }
    }
}
