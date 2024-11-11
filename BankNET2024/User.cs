using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class User: IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Account> Accounts { get; set; }

        public User(string username, string password, string firstName, string lastName, string phoneNumber, List<Account> accounts)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Accounts = accounts;

        }
        public Account? GetAccount()
        {
            Console.WriteLine("Vilket konto: ");
            string? account = Console.ReadLine();

            var foundAccount = Accounts.FirstOrDefault(a => a.AccountNumber == account);
            if (foundAccount == null)
            {
                Console.WriteLine("Konto hittades inte.");
            }
            return foundAccount;
        }
        public void CreateNewAccount()
        {
            Console.WriteLine("Vad för sorts konto vill du skapa?\n1. Vanligt konto\n2. Sparkonto");
            int.TryParse(Console.ReadLine(), out int choice);
            if (choice != 1 && choice != 2)
            { 
                Console.WriteLine("Ogiltigt val. Ange 1 eller 2.");
            }
            else
            {
                Console.WriteLine("Hur mycket vill du sätta in på kontot?");
                decimal.TryParse(Console.ReadLine(), out decimal initialBalance);
                if (initialBalance > 0 && initialBalance < 1000000)
                {
                    string accountNumber = Guid.NewGuid().ToString();
                    if (choice == 1)
                    {
                        Account newAccount = new Account(accountNumber, initialBalance);
                        Console.WriteLine($"Nu har ett nytt konto skapats med kontonummer {accountNumber} och saldo {initialBalance}.");
                        this.Accounts.Add(newAccount);
                    }
                    else
                    {
                        Account newAccount = new SavingAccount(accountNumber, initialBalance);
                        Console.WriteLine($"Nu har ett nytt sparkonto skapats med kontonummer {accountNumber} och saldo {initialBalance}.");
                        this.Accounts.Add(newAccount);
                    } 

                }
                else
                {
                    Console.WriteLine("Ogiltig mängd. Ange ett positivt belopp.");
                }
            }
        }
        public void DisplayAccounts()
        {
            //visar infos om alla accounts med bara nummer och balance
            if (Accounts != null)
            {
                foreach (var account in Accounts)
                {
                    Console.WriteLine(account);
                }
            }
        }
        public void ChangeCurrency()
        {
            var acc = GetAccount();

            if (acc != null)
            {
                Console.WriteLine("Vilken valuta vill du byta till?");
                var currencyDictionary = Admin.GetCurrencyDictionary();
                foreach (var currency in currencyDictionary)
                {
                    Console.WriteLine(currency.Key);
                }
                string? newCurrency = Console.ReadLine().ToUpper();
                if (currencyDictionary.TryGetValue(newCurrency, out decimal newExchangeRate) &&
                    currencyDictionary.TryGetValue(acc.Currency, out decimal currentExchangeRate))
                {
                    if (currentExchangeRate > newExchangeRate)
                    {
                        acc.Balance *= (currentExchangeRate / newExchangeRate);
                    }
                    else
                    {
                        acc.Balance /= (newExchangeRate / currentExchangeRate);
                    }
                    acc.Currency = newCurrency;
                    Console.WriteLine($"Currency changed to {acc.Currency}. New balance: {acc.Balance:F2}  {acc.Currency:F}");
                }
                else
                {
                    Console.WriteLine("Ogiltig valuta");
                }


            }
        }
        public override string ToString()
        {
           return $"Användarnamn: {Username}, Lösenord: ****, Förnamn: {FirstName}, Efternamn: {LastName}, " +
           $"Telefonnummer: {PhoneNumber}";
        }
    }
}
