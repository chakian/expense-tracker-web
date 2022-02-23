namespace ExpenseTracker.Common.Contracts.Query.UserSetting
{
    public class GetUserSettingsResponse : BaseResponse
    {
        public int DefaultBudgetId { get; set; }
    }
}
