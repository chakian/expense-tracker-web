using ExpenseTracker.Common.Enums;

namespace ExpenseTracker.Common.Extensions
{
    public static class AccountTypeHelper
    {
        public static string GetAccountTypeName(int accountTypeCode)
        {
            switch (accountTypeCode)
            {
                case (int)AccountType.Cash:
                    return "Nakit";
                case (int)AccountType.BankAccount:
                    return "Banka";
                case (int)AccountType.CreditCard:
                    return "Kredi Kartı";
                case (int)AccountType.CreditAccount:
                    return "Kredi Hesabı";
                case (int)AccountType.SavingAccount:
                    return "Birikim";
                default:
                    return "-";
            }
        }
    }
}
