using ExpenseTracker.Common.Interfaces.Business.Base;

namespace ExpenseTracker.Common.Interfaces.Business
{
    public interface IUserSettingBusiness : IBaseBusiness
    {
        void UpdateUserSettings(string userId, int budgetId);
    }
}
