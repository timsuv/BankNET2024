using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    internal class Loan
    {
        public decimal InterestRate {  get; set; }
        public decimal LoanLimit { get; set; } = 5;
        public Account Account { get; set; }

   
        public Loan(decimal interestRate, decimal loanLimit, Account account)
        {
            InterestRate = interestRate;
            LoanLimit = loanLimit;
            Account = account;
        }

        public bool CanTakeLoan(decimal requestedLoanAmount)
        {
            decimal maxLoanAmount = Account.Balance * LoanLimit;
            return requestedLoanAmount <= maxLoanAmount;
        }

        public bool RequestLoan(decimal loanAmount)
        {
            if (CanTakeLoan(loanAmount))
            {
                Account.Balance += loanAmount;  // Lägg till lånet i kontosaldot
                Console.WriteLine($"Lånet {loanAmount} {Account.Currency} beviljat. Nuvarande saldo: {Account.Balance} {Account.Currency}");
                return true;
            }
            else
            {
                Console.WriteLine("Lånet ej beviljat! Övestrider låne gräns baserat på saldo.");
                return false;
            }
        }

        public void PayLoan(decimal amount)
        {
            if (amount > 0)
            {
                Account.Balance -= amount;
                Console.WriteLine($"{amount} {Account.Currency} betald. nuvarande saldo: {Account.Balance} {Account.Currency}");
            }
            else
            {
                Console.WriteLine("Återbetalning ej betald.");
            }

        }

    }


}
