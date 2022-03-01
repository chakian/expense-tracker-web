using ExpenseTracker.Common.Contracts.Query.UserSetting;
using ExpenseTracker.Common.Interfaces.Business;
using ExpenseTracker.Persistence;

namespace ExpenseTracker.Business.Queries
{
    public class GetUserSettingsQuery : BaseQuery<GetUserSettingsRequest, GetUserSettingsResponse>
    {
        private readonly IUserSettingBusiness _userSettingBusiness;
        public GetUserSettingsQuery(ExpenseTrackerDbContext context, IUserSettingBusiness userSettingBusiness) : base(context)
        {
            _userSettingBusiness = userSettingBusiness;
        }

        protected override GetUserSettingsResponse HandleInternal(GetUserSettingsRequest request)
        {
            var settings = _userSettingBusiness.GetUserSettings(request.UserId);

            var response = new GetUserSettingsResponse()
            {
                UserSetting = settings,
            };

            return response;
        }
    }
}
