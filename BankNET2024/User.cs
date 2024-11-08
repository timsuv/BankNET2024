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
        public decimal Salary { get; set; }
        public List<Account> Accounts { get; set; }

        //  public bool IsLocked { get; set; } kommer senare
        //public int Attempts { get; set; }

        
        
        
        
        public void AddAccount()
        {
            //Option to select what type of account to add (either add savings account, or apply for loan)
            //If savings, add savings account to List<Account> accounts
            //If savings, go through check with balance then add
        }
        
        public void DisplayAccounts()
        {
            //visar infos om alla accounts med bara nummer och balance
            //CW List or dictionary from user constructor of type Accounts
        }
    }


}
