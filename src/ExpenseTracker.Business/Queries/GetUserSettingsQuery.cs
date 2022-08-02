using ExpenseTracker.Common.Contracts.Query.UserSetting;
using ExpenseTracker.Persistence;
using System.Linq;

namespace ExpenseTracker.Business.Queries
{
    public class GetUserSettingsQuery : BaseQuery<GetUserSettingsRequest, GetUserSettingsResponse>
    {
        public GetUserSettingsQuery(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(GetUserSettingsRequest request, GetUserSettingsResponse response)
        {
            var settingsDbo = context.UserSettings.SingleOrDefault(us => us.UserId == request.UserId);

            Common.Entities.UserSetting userSetting = null;

            if (settingsDbo != null)
            {
                userSetting = new Common.Entities.UserSetting()
                {
                    UserId = settingsDbo.UserId,
                    DefaultBudgetId = settingsDbo.DefaultBudgetId
                };
            }

            response.UserSetting = userSetting;
        }

        //TODO: Think about validation
    }
}
