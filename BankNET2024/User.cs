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
            DisplayAccounts();
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
            if (choice != 1 && choice != 2) //Kollar så att användaren valt något av alternativen
            { 
                Console.WriteLine("Ogiltigt val. Ange 1 eller 2.");
            }
            else
            {
                Console.WriteLine("Hur mycket vill du sätta in på kontot?");
                decimal.TryParse(Console.ReadLine(), out decimal initialBalance);
                if (initialBalance > 0 && initialBalance < 1000000) //Sätter en maxgräns på insättningsbeloppet
                {
                    string accountNumber = Guid.NewGuid().ToString(); //Använder guid för att skapa ett unikt kontonummer
                    if (choice == 1) //Ifall användaren valt vanligt konto
                    {
                        Account newAccount = new(accountNumber, initialBalance);
                        Console.WriteLine($"Nu har ett nytt konto skapats med kontonummer {accountNumber} och saldo {initialBalance}.");
                        Accounts.Add(newAccount);//Lägger till kontot i kontolistan i User
                    }
                    else//Annars skapas ett sparkonto
                    {
                        Account newAccount = new SavingAccount(accountNumber, initialBalance);
                        Console.WriteLine($"Nu har ett nytt sparkonto skapats med kontonummer {accountNumber} och saldo {initialBalance}.");
                        Accounts.Add(newAccount);
                    } 
                }
                else
                {
                    Console.WriteLine("Ogiltigt belopp. Insättningen måste vara mellan 0 och 1 000 000.");
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
                var currencyDictionary = Admin.GetCurrencyDictionary(); // hämtar valutorna från Admin
                foreach (var currency in currencyDictionary) // skriver ut valutorna
                {
                    Console.WriteLine(currency.Key);
                }
                string? newCurrency = Console.ReadLine().ToUpper();
                if (currencyDictionary.TryGetValue(newCurrency, out decimal newExchangeRate) &&
                    currencyDictionary.TryGetValue(acc.Currency, out decimal currentExchangeRate))
                {
                    acc.Balance *= (currentExchangeRate / newExchangeRate);
                    acc.Currency = newCurrency;
                    Console.WriteLine($"Currency changed to {acc.Currency}. New balance: {acc.Balance:F2}  {acc.Currency:F2}");
                }
                else
                {
                    Console.WriteLine("Ogiltig valuta");
                }


            }
        }
        public void TakeLoan()
        {
            // Check if a loan account already exists
            if (Accounts.Any(a => a is LoanAccount))
            {
                Console.WriteLine("Du har redan ett lån.");
                return;
            }

            Console.WriteLine("Hur mycket vill du låna: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal loanAmount))
            {
                if (loanAmount <= Accounts.Sum(a => a.Balance) * 5 && loanAmount > 0)
                {
                    LoanAccount loanAccount = new(Guid.NewGuid().ToString(), 0, loanAmount);
                    Accounts.Add(loanAccount);
                    Console.WriteLine($"Lånekonto skapat med lånebelopp {loanAmount}.");
                }
                else
                {
                    Console.WriteLine("Ogiltigt lånebelopp.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning.");
            }
         
        }
        public void PayLoan()
        {
            var payAcc = GetAccount();

            var loanAccount = Accounts.FirstOrDefault(a => a is LoanAccount);
            if (loanAccount != null && loanAccount is LoanAccount account)
            {
                Console.WriteLine("Hur mycket vill du betala: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal payment))
                {
                    if (payment <= payAcc.Balance)
                    {
                        payAcc.Balance -= payment;
                        account.LoanAmount -= payment;

                        Console.WriteLine($"Du har betalat {payment} och har nu {account.LoanAmount} kvar att betala.");
                    }
                   
                }
            }
            else
            {
                Console.WriteLine("Du har inget lån att betala.");
            }
        }
        public override string ToString()
        {
           return $"Användarnamn: {Username}, Lösenord: ****, Förnamn: {FirstName}, Efternamn: {LastName}, " +
           $"Telefonnummer: {PhoneNumber}";
        }
    }
}
