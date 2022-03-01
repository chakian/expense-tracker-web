namespace ExpenseTracker.Interfaces.Business
{
    public interface ICommand<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        public TResponse Execute(TRequest request);
    }
}
