namespace ExpenseTracker.WebUI.Models.Category
{
    public class UpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}
