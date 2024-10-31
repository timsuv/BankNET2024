using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class AccountType
    {
        //Basklasser
        public string AccountNumber { get; set; }
        public float Balance { get; set; }
        public string Currency { get; set; }
        public float IntrestRate { get; set; }

        public AccountType(string accountnumber, float balance, string currency, float intrestrate)

        {
            AccountNumber = accountnumber;
            Balance = balance;
            Currency = currency;
            IntrestRate = intrestrate;
        }

        // insättning
        public void Deposit(float amount)
        {
            Balance += amount;
        }

        // Uttag från konto, säger till om uttaget överstiger saldot
        public bool Withdraw(float amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }


        // Visar  kontoinformation
        public void ShowAccountDetails()
        {
            Console.WriteLine($"Konto: {AccountNumber}, Balance: {Balance} {Currency}");

        }
    }
    public class SavingsAccount : AccountType
    {
        public float InterestRate { get; set; }

        //konstructor
        public SavingsAccount(string accountNumber, float balance, string currency, float interestRate) : base(accountNumber, balance, currency, interestRate)
        {
            InterestRate = interestRate;
        }

        // Beräknar årliga räntan baserat på saldo
        public float CalculateAnnualInterest()
        {
            return Balance * (InterestRate / 100);
        }

        public void ShowLoanDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Ränta: {InterestRate}%");
        }















        // Sparkonto (årlig ränta)
        
        //Lånekonto
        
    }
}

