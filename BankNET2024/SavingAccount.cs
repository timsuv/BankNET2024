using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class SavingAccount : Account
    {
        public SavingAccount(string accountNumber, float balance, string name, string contactInfo) : base(accountNumber, balance, name, contactInfo)
        {

        }
    }
}
