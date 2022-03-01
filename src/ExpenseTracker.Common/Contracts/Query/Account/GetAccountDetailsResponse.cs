namespace ExpenseTracker.Common.Contracts.Query
{
    public class GetAccountDetailsResponse : BaseResponse
    {
        public Entities.Account Account { get; set; }
    }
}
