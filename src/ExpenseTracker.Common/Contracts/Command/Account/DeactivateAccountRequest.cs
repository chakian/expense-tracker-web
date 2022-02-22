namespace ExpenseTracker.Common.Contracts.Command
{
    public class DeactivateAccountRequest : BaseBudgetRelatedRequest
    {
        public int AccountId { get; set; }
    }
}
