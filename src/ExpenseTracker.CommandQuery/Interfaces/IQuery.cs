using ExpenseTracker.Common.Contracts;

namespace ExpenseTracker.CommandQuery.Interfaces
{
    public interface IQuery
    {
        public IResponse HandleQuery(IRequest request);
    }
}
