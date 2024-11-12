using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankNET2024
{
    public class Account(string accountNumber, decimal balance, string currency = "SEK")
    {
        public string AccountNumber { get; set; } = accountNumber;
        public decimal Balance { get; set; } = balance;
        public string Currency { get; set; } = currency;
        public List<TransactionLog> Transactions { get; set; } = [];
       
            public void Deposit()
            {
                decimal amount = Amount();

                if (amount <= 0)  // Check if the amount is invalid
                {
                    return;  // Exit the method early if invalid input
                }

                Balance += amount;
                Console.WriteLine($"Mängden pengar inlagd: {amount:F} {Currency} på {AccountNumber}");
                Transactions.Add(new TransactionLog(DateTime.Now, $"Insättning: {amount}"));
                Console.ReadLine();
            }
        public virtual void Withdraw()
        {
            decimal amount = Amount();

            if (amount <= 0)  
            {
                return;  
            }

            if (amount > Balance)  // Ensure the withdrawal doesn't exceed balance
            {
                Console.WriteLine("Otillräckligt saldo för uttag.");
                return;
            }

            Balance -= amount;
            Console.WriteLine($"Mängden pengar uttagen: {amount:F} {Currency} från {AccountNumber}");
            Transactions.Add(new TransactionLog(DateTime.Now, $"Uttag: {amount:F} {Currency}"));
            Console.ReadLine();
        }
        private decimal Amount()
        {
            Console.WriteLine("\nAnge mängden pengar: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                return amount;
            }
            else
            {
                Console.WriteLine("Ogiltig mängd pengar.");
                return -1;  // Return a sentinel value to indicate invalid input
            }
        }

        public override string ToString()
        {
            return $"Kontonummer: {AccountNumber}, Saldo: {Balance:F} {Currency}";
        }
       
        
    }
}
