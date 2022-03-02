namespace ExpenseTracker.Common.Contracts.Command
{
    public class UpdateUserSettingsDefaultBudgetRequest : BaseRequest
    {
        public int DefaultBudgetId { get; set; }
    }
}
