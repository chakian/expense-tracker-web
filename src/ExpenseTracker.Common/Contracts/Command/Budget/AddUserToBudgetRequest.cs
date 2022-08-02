namespace ExpenseTracker.Common.Contracts.Command
{
    public class AddUserToBudgetRequest : BaseRequest
    {
        public int BudgetId { get; set; }
    }
}
