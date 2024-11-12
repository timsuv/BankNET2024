using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    internal interface IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Username { get; set; }
        public string Password { get; set; }
        List<Account> Accounts { get; set; } 
    }
}
