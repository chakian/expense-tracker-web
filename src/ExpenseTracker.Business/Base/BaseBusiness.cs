using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Base
{
    public class BaseBusiness
    {
        protected readonly ExpenseTrackerDbContext context;
        public BaseBusiness(DbContextOptions<ExpenseTrackerDbContext> _options)
        {
            context = new ExpenseTrackerDbContext(_options);
        }
    }
}
