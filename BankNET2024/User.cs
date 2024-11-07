using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class User
    {
        public User(string username, string password, string firstName, string lastName, string phoneNumber, List<Account> accounts)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Accounts = accounts;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Account> Accounts { get; set; }

        //  public bool IsLocked { get; set; } kommer senare
        //public int Attempts { get; set; }

        public void CreateOwnAccout()
        {

        }
        public void DisplayAccounts()
        {
            //visar infos om alla accounts med bara nummer och balance
            Console.WriteLine("\nHär kommer listan med alla konto:");
            foreach (var account in Accounts)
            {
                Console.WriteLine($"Konto: {account.AccountNumber} Saldo: {account.Balance}");
            }
        }
    }


}
