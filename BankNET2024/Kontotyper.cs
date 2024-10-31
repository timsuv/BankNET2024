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
        public string AccountNumber {  get; set; }
        public float Balance { get; set; }
        public string Currency {  get; set; }

        public AccountType(string accountnumber, float balance, string currency) 
        
        {
            AccountNumber = accountnumber;
            Balance = balance;
            Currency = currency;
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



        // Sparkonto (årlig ränta)



    }
}
