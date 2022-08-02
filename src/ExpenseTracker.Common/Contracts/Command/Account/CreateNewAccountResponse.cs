namespace ExpenseTracker.Common.Contracts.Command
{
    public class CreateNewAccountResponse : BaseResponse
    {
        public int CreatedAccountId { get; set; }
    }
}