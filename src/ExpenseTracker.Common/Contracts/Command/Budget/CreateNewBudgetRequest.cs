namespace ExpenseTracker.Common.Contracts.Command
{
    public class CreateNewBudgetRequest : BaseRequest
    {
        public string BudgetName { get; set; }
    }
}
