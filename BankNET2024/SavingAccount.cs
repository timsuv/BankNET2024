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
        public decimal InterestRate { get; private set; }
        public decimal SavingBalance { get; set; }

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
        }
        
    }
    
}
