using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Admin : User
    {
        public Admin(string username, string password, string firstName, string lastName, string phoneNumber, decimal salary, List<Account> accounts) : base(username, password, firstName, lastName, phoneNumber, salary, accounts)
        {
        }
    }
}
