namespace ExpenseTracker.Common.Contracts.Command
{
    public class UpdateBudgetRequest : BaseRequest
    {
        public int BudgetId { get; set; }
        public string Name { get; set; }
    }
}
