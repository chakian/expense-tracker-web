using ExpenseTracker.Common.Contracts;

namespace ExpenseTracker.CommandQuery.Interfaces
{
    public interface ICommand<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        public TResponse HandleCommand(TRequest request);
    }
}
