using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
       


        }

        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public decimal Amount { get; set; }
        public string Password { get; set; }


        public void Deposit(List<Account> accounts)
        {
            Console.WriteLine("\nVilket konto vill du sätta in på?");
            string accountNumber = Console.ReadLine();

            Account toAccount = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (toAccount != null)
            {
                Console.WriteLine("\nAnge mängden pengar: ");
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                {
                    Balance += amount;
                    toAccount.Balance += amount;
                    var log = $"Mängden pengar inlagd: {amount} på {AccountNumber}";
                    Console.WriteLine(log);
                    Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
                }
                else
                {
                    Console.WriteLine("Ogiltig mängd. Ange en positiv siffra.");
                }
            }
            else
            {
                Console.WriteLine("Insättning misslyckades. Detta bankkonto existerar inte.");
            }
        }

        public void Withdraw(List<Account> accounts)
        {
            Console.WriteLine("\nVilket konto vill du dra ut från?");
            string accountNumber = Console.ReadLine();

            Account toAccount = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (toAccount != null)
            {
                Console.WriteLine("\nAnge mängden pengar: ");
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                {
                    if (Balance >= amount)
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
                else
                {
                    Console.WriteLine("Ogiltig mängd. Ange en positiv siffra.");
                }
            }
            else
            {
                Console.WriteLine("Uttag misslyckades. Detta bankkonto existerar inte.");
            }
        }


        public void DisplayAccount(User user)
        {
            Console.WriteLine("");
        }
        public async Task Transfer(List<Account> accounts)
        {
            Console.WriteLine("\nVilket konto vill du sätta in på?");
            string accountNumber = Console.ReadLine();

            Account toAccount = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (toAccount != null)
            {
                Console.WriteLine("\nAnge mängden pengar: ");
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                {
                    if (Balance >= amount)
                    {
                        Balance -= amount;
                        toAccount.Balance += amount;
                        var log = $"Överförde {amount} från {AccountNumber} till {toAccount.AccountNumber}";
                        Console.WriteLine(log);
                        Transaction transaction = new Transaction(DateTime.Now, new List<string> { log });
                    }
                    else
                    {
                        Console.WriteLine("Uttag misslyckades. Inte tillräckligt med pengar.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig mängd. Ange en positiv siffra");
                }
            }
            else
            {
                Console.WriteLine("Uttag misslyckades. Detta bankkonto existerar inte.");
            }
        }



       

    }
}
