using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using System.Linq;

namespace ExpenseTracker.Business.Commands
{
    public class UpdateUserSettingsDefaultBudgetCommand : BaseCommand<UpdateUserSettingsDefaultBudgetRequest, UpdateUserSettingsDefaultBudgetResponse>
    {
        public UpdateUserSettingsDefaultBudgetCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(UpdateUserSettingsDefaultBudgetRequest request, UpdateUserSettingsDefaultBudgetResponse response)
        {
            var settingsDbo = context.UserSettings.Single(us => us.UserId == request.UserId);

            settingsDbo.DefaultBudgetId = request.DefaultBudgetId;

            context.Entry(settingsDbo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        protected override UpdateUserSettingsDefaultBudgetResponse Validate(UpdateUserSettingsDefaultBudgetRequest request)
        {
            var response = new UpdateUserSettingsDefaultBudgetResponse();
            //TODO: Validation
            return response;
        }
    }
}
