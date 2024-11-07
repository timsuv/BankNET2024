using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Admin : User
    {
        public Admin(string username, string password, string firstName, string lastName, string phoneNumber, decimal salary, List<Account> accounts) : base(username, password, firstName, lastName, phoneNumber, salary, accounts)
        {
        }
        
        
        
        
        //Private object of CurrencyChange class
        private CurrencyChange converter;
        
        //Methods for admin to update currency rate through CW, maybe add try catch or if/else later
        public void UpdateEuro()
        {
            Console.WriteLine("Enter EURO to SEK rate");
            decimal.TryParse(Console.ReadLine(), out decimal newRateEur);
            converter.UpdateEuro(newRateEur);
        }
        
        public void UpdateUsd()
        {
            Console.WriteLine("Enter USD to SEK rate");
            decimal.TryParse(Console.ReadLine(), out decimal newRateUsd);
            converter.UpdateUsd(newRateUsd);
        }

    }
}
