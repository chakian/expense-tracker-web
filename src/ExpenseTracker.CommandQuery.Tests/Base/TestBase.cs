using ExpenseTracker.Persistence;
using Moq;

namespace ExpenseTracker.CommandQuery.Tests.Base
{
    public class TestBase
    {
        protected ExpenseTrackerDbContext GetMockContext()
        {
            return new Mock<ExpenseTrackerDbContext>().Object;
        }
    }
}
