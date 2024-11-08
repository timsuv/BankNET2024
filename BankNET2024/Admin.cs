using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Admin : User
    {
        public Admin(string username, string password, string firstName, string lastName, string phoneNumber, List<Account> accounts) : base(username, password, firstName, lastName, phoneNumber, accounts)
        {
        }
        
        
        
        
        
        //Methods for admin to update currency rate through CW, maybe add try catch or if/else later
        public void UpdateEuro(decimal dailyRateEuro)
        { 
            CurrencyChange.UpdateEuro(dailyRateEuro);
        }
        public void UpdateUsd(decimal dailyRateUsd)
        {
            CurrencyChange.UpdateUsd(dailyRateUsd);
        }

    }
}
