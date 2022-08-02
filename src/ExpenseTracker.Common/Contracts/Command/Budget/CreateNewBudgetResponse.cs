namespace ExpenseTracker.Common.Contracts.Command
{
    public class CreateNewBudgetResponse : BaseResponse
    {
        public int CreatedBudgetId { get; set; }
    }
}
