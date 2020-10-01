using EFEntities;
using System;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class TempBusiness
    {
        public void GetUsers()
        {
            ExpenseTrackerContext context = new ExpenseTrackerContext();
            var users = context.Users.ToList();
            Console.WriteLine(users.ToString());
        }

        public void GetCurrencies()
        {
            ExpenseTrackerContext context = new ExpenseTrackerContext();
            var currencies = context.Currencies.ToList();
            Console.WriteLine(currencies.ToString());
        }
    }
}
