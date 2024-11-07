using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
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
        //public Account(decimal balance)
        //{
        //    AccountNumber = GenerateUniqueAccountNumber();
        //    Balance = balance;
        //}
        //private string GenerateUniqueAccountNumber()
        //{
        //    return Guid.NewGuid().ToString(); // Ger ett unikt kontonummer som t.ex. "3f84c023-b2d4-4c90-98f0-13fd4b9d2bcd"
        //}

        public void Deposit2()
        {
            Console.WriteLine("\nAnge mängden pengar. ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount > 0)
            {
                Balance += amount;
                var log = $"Mängden pengar inlagd: {amount} på {AccountNumber}";
                Console.WriteLine(log);
                Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
                Console.Read();
            }
        }
        public void TempWithdraw()
        {
            Console.WriteLine("\nAnge mängden pengar. ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Balance -= amount;

            var log = $"Mängden pengar uttagen: {amount} från {AccountNumber}";
            Console.WriteLine(log);
            Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
        }
        public void TempTransfer()
        {

        }
        public async Task Transfer(List<Account> accounts)
        {
            Console.WriteLine("\nWhich account do you want to transfer to?");
            string? accountNumber = Console.ReadLine();

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
        public override string ToString()
        {
            return $"Account number: {AccountNumber}, BAlance {Balance}";
        }


    }
}
