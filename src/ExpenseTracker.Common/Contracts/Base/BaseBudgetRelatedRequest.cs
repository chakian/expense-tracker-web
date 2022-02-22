namespace ExpenseTracker.Common.Contracts
{
    public class BaseBudgetRelatedRequest : BaseRequest
    {
        public int BudgetId { get; set; }
    }
}
