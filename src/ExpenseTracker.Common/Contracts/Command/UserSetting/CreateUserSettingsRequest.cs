namespace ExpenseTracker.Common.Contracts.Command
{
    public class CreateUserSettingsRequest : BaseRequest
    {
        public int DefaultBudgetId { get; set; }
    }
}
