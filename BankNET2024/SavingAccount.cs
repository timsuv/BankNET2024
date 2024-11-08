using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class SavingAccount : Account
    {
        //Savings account, needs an interest rate, savings amount, savings account number
        private decimal InterestRate = 0.033m;
        public decimal SavingBalance { get; set; }
        public string savingsAccountNumber { get; set; }

        //Constructor
        public SavingAccount(string accountNumber, decimal balance, decimal savingBalance) : base(accountNumber, balance)
        {
            this.SavingBalance = savingBalance;
        }
        
        //Methods to deposit (& calculate interest), withdraw & display balance
        public void Deposit(decimal amount)
        {
            amount += SavingBalance;
            Console.WriteLine($"The interest on this deposit of {amount} SEK is" + (InterestRate * amount) + " SEK");
        }

        public void Withdraw(decimal amount)
        {
            amount -= SavingBalance;
        }
            
        public void DisplayBalance()
        {
            Console.WriteLine($"Savings balance: {SavingBalance} SEK");
            Console.WriteLine($"The accrued interest is " + (InterestRate * SavingBalance));
        }
        
    }
    
}
