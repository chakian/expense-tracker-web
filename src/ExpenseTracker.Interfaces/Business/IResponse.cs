namespace ExpenseTracker.Interfaces.Business
{
    public interface IResponse
    {
        void WriteExceptionMessage(Exception exception, bool clearAll = true);
        bool HasErrors();
        //public void AddMessage(string message, bool isError);
    }
}
