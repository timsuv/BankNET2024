using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class LoanAccount : Account
    {
        public decimal LoanAmount;
        
        public LoanAccount(string accountNumber, decimal balance, decimal loanAmount) : base(accountNumber, balance)
        {
            this.LoanAmount = loanAmount;
        }

        public void ApplyForLoan(decimal balance, decimal loanAmount)
        {
            this.LoanAmount = loanAmount;
            
            if (LoanAmount < (balance * 5))
            {
                loanAmount += balance;
            }
            else
            {
                Console.WriteLine("Requested loan amount is too high, please reapply with a lower amount");
            }
        }

        public void DisplayLoanAmount()
        {
            Console.WriteLine($"Loan amount is {LoanAmount}");
        }
        
        
    }
}
