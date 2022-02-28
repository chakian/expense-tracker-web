namespace ExpenseTracker.Common.Contracts.Query.UserSetting
{
    public class GetUserSettingsResponse : BaseResponse
    {
        public Entities.UserSetting UserSetting { get; set; }
    }
}
