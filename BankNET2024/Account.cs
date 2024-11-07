using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Account
    {
        public static List<Account> Accounts = new List<Account>();
        protected Account(string accountNumber, decimal balance, string name, string contactInfo, decimal amount, string password)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Name = name;
            ContactInformation = contactInfo;
            Amount = amount;
            Password = password;



        }

        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public decimal Amount { get; set; }
        public string Password { get; set; }


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
        public async Task TransferToUser(List<Account> accounts)
        {
            Console.WriteLine("\nWhich account do you want to transfer to?");
            string accountNumber = Console.ReadLine();

            Account toAccount = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (toAccount != null)
            {
                Console.WriteLine("\nEnter the amount to transfer:");
                decimal amount = decimal.Parse(Console.ReadLine());

                if (Balance >= amount)
                {
                    Balance -= amount;
                    toAccount.Balance += amount;
                    var log = $"Transferred {amount} from {AccountNumber} to {toAccount.AccountNumber}";
                    Console.WriteLine(log);
                    Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
                }
                else
                {
                    Console.WriteLine("Transfer failed. Insufficient balance.");
                }
            }
            else
            {
                Console.WriteLine("Transfer failed. Invalid account number.");
            }
        }

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
        private void CreateAccount()
        {
            var newAccount = new Account(
                accountNumber: "1",
                balance: 50036.32M,
                name: "Bob Åberg",
                contactInfo: "bob@gmail.com, 0728539675",
                amount: 0M,
                password: "apa1"
            );

            var newAccount2 = new Account(
            accountNumber: "22",
            balance: 40036.32M,
            name: "Emil Lönneberga",
            contactInfo: "Emil.233@gmail.com, 0721939396",
            amount: 0M,
            password: "apa2"
        );
            var newAccount3 = new Account(
                accountNumber: "333",
                balance: 30036.32M,
                name: "Anders Strömberg",
                contactInfo: "Anders.213@gmail.com, 0721602756",
                amount: 0M,
                password: "apa3"
);

            Accounts.Add(newAccount);
            Accounts.Add(newAccount2);
            Accounts.Add(newAccount3);
        }

    }
}
