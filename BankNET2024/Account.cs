using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Account
    {
        public Account(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;

        }

        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }



        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }


        public void Transfer(Account AccountSender, Account AccountReceiver, decimal amount)//mellan egan konto t.ex sparkonto
        {
            //var toAccount = Accounts.FirstOrDefault(a => a.AccountNumber == toAccountNumber);
            //if (toAccount != null && Balance >= amount)
            //{
            //    Balance -= amount;
            //    toAccount.Balance += amount;
            //    Console.WriteLine($"Transferred {amount} from {AccountNumber} to {toAccountNumber}");
            //}
            //else
            //{
            //    Console.WriteLine("Transfer failed. Check account balance or account number.");
            //}
        }
        public void Exchange()
        {
            //visa växling valuta men inte mycket mer
        }




    }
}
