using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Account
    {
        protected Account(string accountNumber, decimal balance, string name, string contactInfo, decimal amount)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Name = name;
            Amount = amount;
        }

        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }



        public void Deposit()
        {
            Balance += Amount;
        }
        public void Withdraw()
        {
            Balance -= Amount;
        }

        public void DisplayAccount()
        {
            Console.WriteLine("");
        }
        //public async Task Transfer(string toAccountNumber, decimal amount)
        //{
        //    var toAccount = Accounts.FirstOrDefault(a => a.AccountNumber == toAccountNumber);
        //    if (toAccount != null && Balance >= amount)
        //    {
        //        Balance -= amount;
        //        toAccount.Balance += amount;
        //        Console.WriteLine($"Transferred {amount} from {AccountNumber} to {toAccountNumber}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Transfer failed. Check account balance or account number.");
        //    }
        //}

        public async Task TransferToUser(Account toAccount, decimal amount)
        {
            if (toAccount != null && Balance >= amount)
            {
                Balance -= amount;
                toAccount.Balance += amount;
                Console.WriteLine($"Transferred {amount} from {AccountNumber} to {toAccount.AccountNumber}");
            }
            else
            {
                Console.WriteLine("Transfer failed. Check account balance or account details.");
            }
        }
        

    }
}
