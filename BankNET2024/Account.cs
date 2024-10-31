using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public abstract class Account
    {
        public string AccountNumber { get; set; }
        public float ActualBalance { get; set; }

        public string Name { get; set; }
        public int MyProperty { get; set; }
        public float SavingAccount { get; set; }



    }
}
