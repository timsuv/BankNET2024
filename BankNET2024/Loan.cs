using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Loan
    {
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int Duration { get; set; }
        public Account AccountToPay { get; set; }
        public Loan(decimal amount, decimal interestRate, int duration, Account accountToPay)
        {
            Amount = amount;
            InterestRate = interestRate;
            Duration = duration;
            AccountToPay = accountToPay;
        }

    }
}
