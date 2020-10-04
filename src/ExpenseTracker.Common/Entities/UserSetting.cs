namespace ExpenseTracker.Common.Entities
{
    public class UserSetting
    {
        public string UserId { get; set; }
        public int DefaultBudgetId { get; set; }
        public bool IsActive { get; set; }
    }
}
