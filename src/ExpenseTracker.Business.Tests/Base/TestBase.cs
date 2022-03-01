using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace ExpenseTracker.Business.Tests
{
    public class TestBase
    {
        private DbContextOptions<ExpenseTrackerDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ExpenseTrackerDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected ExpenseTrackerDbContext CreateContext()
        {
            DbContextOptions<ExpenseTrackerDbContext> options = CreateNewContextOptions();
            return new ExpenseTrackerDbContext(options);
        }

        protected ExpenseTrackerDbContext GetMockContext()
        {
            return new Mock<ExpenseTrackerDbContext>().Object;
        }
    }
}
