using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;

namespace ExpenseTracker.Business.Commands
{
    public class CreateUserSettingsCommand : BaseCommand<CreateUserSettingsRequest, CreateUserSettingsResponse>
    {
        public CreateUserSettingsCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(CreateUserSettingsRequest request, CreateUserSettingsResponse response)
        {
            UserSetting userSetting = new UserSetting()
            {
                UserId = request.UserId,
                DefaultBudgetId = request.DefaultBudgetId
            };
            AddAuditDataForCreate(userSetting, request.UserId);
            
            context.Entry(userSetting).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        protected override CreateUserSettingsResponse Validate(CreateUserSettingsRequest request)
        {
            var response = new CreateUserSettingsResponse();
            //TODO: Validation
            return response;
        }
    }
}
