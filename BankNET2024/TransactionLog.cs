using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Transaction
    {
        public Transaction(DateTime transferTime, List<string> transactions)
        {
            TransferTime = transferTime;
            Transactions = transactions;
        }

        public DateTime TransferTime { get; set; }
        public List<string> Transactions { get; set; }
        public void TransactionHistory()
        {

        }

    }
}
