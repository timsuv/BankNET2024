using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Userr: IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Accountt> Accounts { get; set; }

        public Userr(string username, string password, string firstName, string lastName, string phoneNumber, List<Accountt> accounts)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Accounts = accounts;

            Accounts.Add(new Accountt("kkk", 1000));
        }

        public void CreateOwnAccout()
        {
            Accounts.Add(new Accountt("111", 0));
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
        public override string ToString()
        {
           
            return $"Username: {Username}, Password: ****, FirstName: {FirstName}, LastName: {LastName}, " +
           $"PhoneNumber: {PhoneNumber}";
        }
    }


}
