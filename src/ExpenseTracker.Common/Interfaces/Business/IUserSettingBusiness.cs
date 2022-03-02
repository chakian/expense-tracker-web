using ExpenseTracker.Common.Interfaces.Business.Base;

namespace ExpenseTracker.Common.Interfaces.Business
{
    public interface IUserSettingBusiness : IBaseBusiness
    {
        void CreateUserSettings(string userId, int budgetId);
        void UpdateUserSettings(string userId, int budgetId);
    }
}
