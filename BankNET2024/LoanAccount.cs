using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class LoanAccount : Account
    {
        public decimal LoanAmount { get; set; }
        public decimal LoanBalance { get; set; }
        public decimal InterestRate = 2;
        public decimal WithdrawAmount { get; set; }
        
        public LoanAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
        {
        }
        
        public void ApplyForLoan(decimal loanAmount)
        {
            this.LoanAmount = loanAmount;
            
            if (LoanAmount < (Balance * 5))
            {
                LoanAmount += LoanBalance;
                Console.WriteLine($"Requested loan amount of {LoanAmount} was successful with an interest rate of {InterestRate}, or " + (InterestRate * LoanAmount));
            }
            else
            {
                Console.WriteLine("Requested loan amount is too high, please reapply with a lower amount");
            }
        }
        
        public void DisplayLoanAmount()
        {
            Console.WriteLine($"Loan balance is {LoanBalance}");
        }

        public void Withdraw(decimal withdrawAmount)
        {
            this.WithdrawAmount = withdrawAmount;

            if (WithdrawAmount < LoanBalance)
            {
                WithdrawAmount -= LoanBalance;
            }
            else
            {
                Console.WriteLine("Requested withdrawal amount is too high, please try again with a lower amount");
            }
        }
        
        
    }
}
