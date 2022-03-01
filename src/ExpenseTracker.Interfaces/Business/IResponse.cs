namespace ExpenseTracker.Interfaces.Business
{
    public interface IResponse
    {
        public void WriteExceptionMessage(Exception exception, bool clearAll = true);
        //public void AddMessage(string message, bool isError);
    }
}
