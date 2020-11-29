namespace ExpenseTracker.Common.Contracts.Command
{
    public class DeactivateBudgetRequest : BaseRequest
    {
        public int BudgetId { get; set; }
    }
}
