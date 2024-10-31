using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Transaction
    {
        public string ID { get; set; }
        public float BalanceAmount { get; set; }
        public DateTime TransferTime { get; set; }
        public List<string> Transactions { get; set; }
        public void TransactionHistory()
        {

        }

    }
}
