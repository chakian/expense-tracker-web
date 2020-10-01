namespace ExpenseTracker.Persistence.DbModels
{
    public class BaseDbo<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
    }
}
