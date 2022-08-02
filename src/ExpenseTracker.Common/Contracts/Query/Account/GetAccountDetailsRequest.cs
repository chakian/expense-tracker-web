namespace ExpenseTracker.Common.Contracts.Query
{
    public class GetAccountDetailsRequest : BaseRequest
    {
        public int AccountId { get; set; }
    }
}
