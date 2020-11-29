using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExpenseTracker.Business.Tests
{
    public class TestBase
    {
        protected DbContextOptions<ExpenseTrackerDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ExpenseTrackerDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected ExpenseTrackerDbContext CreateContext(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            return new ExpenseTrackerDbContext(options);
        }

        protected ExpenseTrackerDbContext CreateContext()
        {
            DbContextOptions<ExpenseTrackerDbContext> options = CreateNewContextOptions();
            return new ExpenseTrackerDbContext(options);
        }
    }
}
