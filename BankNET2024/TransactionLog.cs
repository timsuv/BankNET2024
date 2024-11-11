using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class TransactionLog
    {
        public TransactionLog(DateTime transferTime, string log)
        {
            TransferTime = transferTime;
            Log = log;
        }

        public DateTime TransferTime { get; set; }
        public string Log { get; set; }
        public void DisplayTransactionHistory()
        {
            Console.WriteLine($"{TransferTime}: {Log}");
        }
        public override string ToString()
        {
            return $"Datum: {TransferTime}, Beskrivning: {Log}";
        }

    }
}
