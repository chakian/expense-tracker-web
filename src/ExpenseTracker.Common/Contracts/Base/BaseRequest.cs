namespace ExpenseTracker.Common.Contracts
{
    public class BaseRequest : IRequest
    {
        public string UserId { get; set; }
    }

    public interface IRequest
    {
    }
}
