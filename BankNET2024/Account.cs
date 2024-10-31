using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public abstract class Account
    {
        protected Account(string accountNumber, float balance, string name, string contactInfo)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Name = name;
            ContactInformation = contactInfo;
        }

        public string AccountNumber { get; set; }
        public float Balance { get; set; }

        public string Name { get; set; }
        public string ContactInformation { get; set; }


        public Dictionary<string, string> LoginDetails { get; set; }

        public void Deposit()
        {

        }
        public void Withdraw()
        {

        }

        public void DisplayAccount()
        {
            Console.WriteLine("");
        }
        public void Transfer()
        {

        }
        public void TransferToUser()
        {

        }
    }

}
