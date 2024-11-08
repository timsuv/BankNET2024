using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class SavingAccount : Account
    {
        public  decimal IntrestRate { get; set; }
        public SavingAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
        {

        }
    }
}
