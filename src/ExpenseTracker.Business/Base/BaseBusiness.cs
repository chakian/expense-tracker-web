using ExpenseTracker.Common.Interfaces.Business.Base;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpenseTracker.Business.Base
{
    public class BaseBusiness : IBaseBusiness
    {
        protected readonly ExpenseTrackerDbContext dbContext;
        protected BaseBusiness(ExpenseTrackerDbContext _context)
        {
            dbContext = _context;
        }

        [Obsolete("This constructor will be removed when all business calls are done via command/query module")]
        protected BaseBusiness(DbContextOptions<ExpenseTrackerDbContext> _dbContextOptions)
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
