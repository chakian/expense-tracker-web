namespace ExpenseTracker.Interfaces.Business
{
    public interface IQuery<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse
    {
        public TResponse Retrieve(TRequest request);
    }
}
