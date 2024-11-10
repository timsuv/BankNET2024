using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class SavingAccount : Account
    {
        // Interest rate for the savings account
        private decimal _intrestRate = 0.03m;

        // Constructor to initialize the savings account with account number and balance
        public SavingAccount(string accountNumber, decimal balance) : base(accountNumber, balance)
        {
            // Start a task to periodically increase the balance with interest
            Task.Run(() => IncreaseBalance());
        }

        // Override the Withdraw method to prevent withdrawals from the savings account
        public override void Withdraw()
        {
            Console.WriteLine("Can't withdraw from this account");
        }

        // Method to periodically increase the balance with interest
        public async Task IncreaseBalance()
        {
            while (true)
            {
                // Calculate the interest
                decimal intrest = Balance * _intrestRate;

                // Add the interest to the balance
                Balance += intrest;

                // Log the interest addition transaction
                Transactions.Add(new TransactionLog(DateTime.Now, $"Interest added: {intrest}"));

                // Wait for 10 seconds before adding interest again
                await Task.Delay(10000);
            }
        }
    }
}
