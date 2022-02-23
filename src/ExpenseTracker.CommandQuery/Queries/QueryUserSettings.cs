using ExpenseTracker.Common.Contracts.Query.UserSetting;
using ExpenseTracker.Common.Interfaces.Business;
using ExpenseTracker.Persistence;

namespace ExpenseTracker.CommandQuery.Queries
{
    public class QueryUserSettings : BaseQuery<GetUserSettingsRequest, GetUserSettingsResponse>
    {
        private readonly IUserSettingBusiness _userSettingBusiness;
        public QueryUserSettings(ExpenseTrackerDbContext context, IUserSettingBusiness userSettingBusiness) : base(context)
        {
            _userSettingBusiness = userSettingBusiness;
        }

        protected override GetUserSettingsResponse HandleInternal(GetUserSettingsRequest request)
        {
            var settings = _userSettingBusiness.GetUserSettings(request.UserId);

            var response = new GetUserSettingsResponse()
            {
                DefaultBudgetId = settings.DefaultBudgetId
            };

            return response;
        }
    }
}
