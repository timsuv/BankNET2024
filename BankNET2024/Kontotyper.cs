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
        public string AccountNumber;
        public decimal TotalBalance;
        public string Currency;

        public AccountType(string accountnumber, decimal totalbalance, string currency) 
        
        {
            AccountNumber = accountnumber;
            TotalBalance = totalbalance;
            Currency = currency;
        }

        // insättning
        public void Deposit(Decimal amount)
        {
            TotalBalance += amount;
        }

        // Uttag från konto, returnerar false om uttaget överstiger saldot
        public bool Withdraw(decimal amount)
        {
            if (TotalBalance >= amount)
            {
                TotalBalance -= amount;
                return true;
            }
            return false;
        }




        // Visar  kontoinformation



        // Sparkonto som har en årlig ränta



    }
}
