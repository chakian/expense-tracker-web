using ExpenseTracker.Common.Contracts;

namespace ExpenseTracker.CommandQuery.Interfaces
{
    public interface IQuery<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        public TResponse HandleQuery(TRequest request);
    }
}
