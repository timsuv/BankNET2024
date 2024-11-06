using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class SavingAccount : Account
    {
        public SavingAccount(string accountNumber, decimal balance, string name, string contactInfo, decimal amount, string password) : base(accountNumber, balance, name, contactInfo, amount, password)
        {

        }
    }
}
