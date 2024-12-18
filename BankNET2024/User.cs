﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class User : IUser
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
            Console.WriteLine();
            DisplayAccounts();
            Console.WriteLine("\nAnge vilket konto du vill hitta: ");
            string? account = Console.ReadLine();

            var foundAccount = Accounts.FirstOrDefault(a => a.AccountNumber.ToLower() == account.ToLower());
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
                        Console.WriteLine($"Nu har ett nytt konto skapats med kontonummer {accountNumber} och saldo {initialBalance} {newAccount.Currency}.");
                        Accounts.Add(newAccount);//Lägger till kontot i kontolistan i User
                    }
                    else//Annars skapas ett sparkonto
                    {
                        Account newAccount = new SavingAccount(accountNumber, initialBalance);
                        Console.WriteLine($"Nu har ett nytt sparkonto skapats med kontonummer {accountNumber} och saldo {initialBalance} {newAccount.Currency}.");
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
            Console.WriteLine("Lista över dina konto:");
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
                Console.WriteLine("\nVilken valuta vill du byta till?");
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
                    Console.WriteLine($"\nValuta ändrad till {acc.Currency}. Nytt Saldo: {acc.Balance:F2}  {acc.Currency:F2}");
                }
                else
                {
                    Console.WriteLine("Ogiltig valuta");
                }


            }
        }
        public void TakeLoan()
        {
            // Find an existing loan account, if any
            var existingLoanAccount = Accounts.OfType<LoanAccount>().FirstOrDefault();

            Console.WriteLine("\nHur mycket vill du låna? ");
            if (decimal.TryParse(Console.ReadLine(), out decimal loanAmount))
            {
                if (loanAmount <= Accounts.Sum(a => a.Balance) * 5 && loanAmount > 0)
                {
                    if (existingLoanAccount != null)
                    {

                        if (existingLoanAccount.LoanAmount == 0)
                        {
                            existingLoanAccount.LoanAmount = loanAmount;
                            Console.WriteLine($"\nBefintligt lånekonto uppdaterat med lånebelopp {loanAmount}.");
                        }
                        else
                        {
                            Console.WriteLine("\nDu har redan ett lån som inte är betalt.");
                        }
                    }
                    else
                    {
                        LoanAccount newLoanAccount = new(Guid.NewGuid().ToString(), 0, loanAmount);
                        Accounts.Add(newLoanAccount);
                        Console.WriteLine($"\nNytt lånekonto skapat med lånebelopp {loanAmount} {newLoanAccount.Currency}.");
                    }
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
            var loanAccount = Accounts.FirstOrDefault(a => a is LoanAccount);
            if (loanAccount != null && loanAccount is LoanAccount account && account.LoanAmount > 0)
            {
                var payAcc = GetAccount();
                if (payAcc != null)
                {
                    Console.WriteLine("\nHur mycket vill du betala: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal payment))
                    {
                        if (payment <= payAcc.Balance && payment <= account.LoanAmount)
                        {
                            payAcc.Balance -= payment;
                            account.LoanAmount -= payment;
                            if (account.LoanAmount == 0)
                            {
                                Console.WriteLine("\nDu har betalat av hela ditt lån");
                                payAcc.Transactions.Add(new TransactionLog(DateTime.Now, $"Lånet avbetalt: {payment} {account.Currency}"));
                            }
                            else
                                Console.WriteLine($"\nDu har betalat {payment} och har nu {account.LoanAmount} {account.Currency} kvar att betala.");
                            payAcc.Transactions.Add(new TransactionLog(DateTime.Now, $"Lånebetalning: {payment} {account.Currency}"));
                        }
                        else if (payment > payAcc.Balance)
                        {
                            Console.WriteLine("\nDu har inte tillräckligt med pengar på kontot");
                        }
                        else
                        {
                            Console.WriteLine("\nDu försökte betala mer än du har lånat, försök igen.");
                        }

                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("\nDu har inget lån att betala.");
            }
        }

        public override string ToString()
        {
            return $"Förnamn: {FirstName}, Efternamn: {LastName}, " +
            $"Telefonnummer: {PhoneNumber}";
        }
    }
}
