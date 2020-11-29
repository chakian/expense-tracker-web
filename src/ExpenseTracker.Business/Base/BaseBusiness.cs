using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using System;

namespace ExpenseTracker.Business.Base
{
    public class BaseBusiness
    {
        protected readonly ExpenseTrackerDbContext context;
        public BaseBusiness(ExpenseTrackerDbContext _context)
        {
            context = _context;
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
