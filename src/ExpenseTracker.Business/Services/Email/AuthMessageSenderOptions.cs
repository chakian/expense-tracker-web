namespace ExpenseTracker.Business.Services.Email
{
    public class AuthMessageSenderOptions
    {
        public string MailServer { get; set; }
        public int MailServerPort { get; set; }
        public string MailAccount { get; set; }
        public string MailPasswrd { get; set; }
    }
}
