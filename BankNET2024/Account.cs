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

        public void Deposit2()
        {
            Console.WriteLine("\nAnge mängden pengar. ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount < 0)
            {
                Balance += amount;
                var log = $"Mängden pengar inlagd: {amount} på {AccountNumber}";
                Console.WriteLine(log);
                Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
            }
        }
        public void Deposit(List<Account> accounts)
        {
            Console.WriteLine("\nVilket konto vill du dra ut från??");
            string accountNumber = Console.ReadLine();

            Account toAccount = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (toAccount != null)
            {
                Console.WriteLine("\nAnge mängden pengar. ");
                decimal amount = decimal.Parse(Console.ReadLine());

                if (amount < 0)
                {
                    Balance += amount;
                    toAccount.Balance += amount;
                    var log = $"Mängden pengar inlagd: {amount} på {AccountNumber}";
                    Console.WriteLine(log);
                    Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
                }

            }
            else
            {
                Console.WriteLine("Uttag misslyckades. Detta bankkonto existerar inte.");
            }
        }
        public void Withdraw(List<Account> accounts)
        {
            Console.WriteLine("\nVilket konto vill du dra ut från??");
            string accountNumber = Console.ReadLine();

            Account toAccount = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (toAccount != null)
            {
                try
                {
                    Console.WriteLine("\nAnge mängden pengar. ");
                    decimal amount = decimal.Parse(Console.ReadLine());

                    if (Balance >= amount && amount < 0)
                    {
                        Balance -= amount;
                        toAccount.Balance += amount;
                        var log = $"Mängden pengar uttagen: {amount} från {AccountNumber}";
                        Console.WriteLine(log);
                        Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
                    }
                    else
                    {
                        Console.WriteLine("Uttag misslyckades. Inte tillräckligt med pengar.");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message) ;
                }
                
            }
            else
            {
                Console.WriteLine("Uttag misslyckades. Detta bankkonto existerar inte.");
            }
        }
        public void TempWithdraw()
        {
            Console.WriteLine("\nAnge mängden pengar. ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Balance -= amount;
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
