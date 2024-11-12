using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankNET2024
{
    internal class LoanAccount : Account
    {
        public LoanAccount(string accountNumber, decimal balance, decimal loanAmount, decimal interestRate = 0.03m, string currency = "SEK")
            : base(accountNumber, balance, currency)
        {
            LoanAmount = loanAmount;
            InterestRate = interestRate;
            Balance = loanAmount;
            Task.Run(() => IncreaseLoan());
        }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
     
        private async Task IncreaseLoan()
        {
            
            while (LoanAmount > 0)
            {
                await Task.Delay(30000);
                decimal monthlyInterest = LoanAmount * InterestRate;
                LoanAmount += monthlyInterest;

            }
            if (LoanAmount < 0)
            {
                LoanAmount = 0;
            }
        }
        public override string ToString()
        {
            return $"Kontonummer: {AccountNumber}, Saldo: {Balance:F} {Currency:F}, Kvar att betala: {LoanAmount:F} {Currency:F}, Ränta: {InterestRate:F}";
        }
    }
}


