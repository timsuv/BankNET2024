using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class SavingAccount : Account
    {
        private decimal IntrestRate = 0.3m;
        public SavingAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
        {
            Task.Run(() => IncreaseBalance());
        }
        public override void Withdraw()
        {
            Console.WriteLine("Cant withdraw from this account");
        }
        public async Task IncreaseBalance()
        {
            while (true)
            {
                decimal intrest = Balance * IntrestRate;
                Balance += intrest;
                Transactions.Add(new TransactionLog(DateTime.Now, $"Interest added: {intrest}"));
                await Task.Delay(10000);
            }
        }
    }
}
