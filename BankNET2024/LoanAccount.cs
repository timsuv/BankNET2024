using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class LoanAccount : Account
    {
        public LoanAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
        {
        }
    }
}
