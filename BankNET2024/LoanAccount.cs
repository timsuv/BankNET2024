using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankNET2024
{
    internal class LoanAccount : Account
    {
        public LoanAccount(string accountNumber, decimal balance, decimal loanAmount, decimal interestRate = 0.03m, string currency = "SEK")
            : base(accountNumber, balance, currency)
        {
            LoanAmount = loanAmount;
            InterestRate = interestRate;
            Balance = loanAmount;
            Task.Run(() => IncreaseLoan());
        }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
     
        private async Task IncreaseLoan(Account account)
        {
            while (Balance > 0)
            {
               decimal monthlyInterest = LoanAmount * InterestRate;
                decimal monthlyPayment = LoanAmount / 12 + monthlyInterest;


                Transactions.Add(new TransactionLog(DateTime.Now, $"Ränta: {monthlyInterest}"));
                await Task.Delay(2000);
                
                Balance -= monthlyPayment;
                
            }
        }


        //public void RequestLoan()
        //{
        //    if (LoanAmount <= Balance * LoanLimit)
        //    {
        //        Account.Balance += LoanAmount;  // Lägg till lånet i kontosaldot
        //        Account.Transactions.Add(new TransactionLog(DateTime.Now, $"Lån: {LoanAmount}{Account.Currency}"));
        //        Console.WriteLine($"Lånet {LoanAmount} {Account.Currency} beviljat. Nuvarande saldo: {Account.Balance} {Account.Currency}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Lånet ej beviljat! Övestrider låne gräns baserat på saldo.");
        //    }
        //}

        //public async Task PayLoan(decimal amount)
        //{
        //    var monthlyInterest = LoanAmount * InterestRate;
        //    var monthlyPayment = LoanAmount / 12 + monthlyInterest;


        //    while (LoanAmount > 0)
        //    {

        //        if (amount > monthlyPayment)
        //        {
        //            LoanAmount -= amount;
        //            Account.Balance -= amount;
        //            Account.Transactions.Add(new TransactionLog(DateTime.Now, $"Återbetalning: {amount}{Account.Currency}"));
        //            Console.WriteLine($"Återbetalning: {amount} {Account.Currency}. Nuvarande saldo: {Account.Balance} {Account.Currency}");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Återbetalning ej betald.");
        //        }
        //        await Task.Delay(10000);

        //    }

        //    //if (amount > 0)
        //    //{
        //    //    Account.Balance -= amount;
        //    //    Console.WriteLine($"{amount} {Account.Currency} betald. nuvarande saldo: {Account.Balance} {Account.Currency}");
        //    //}
        //    //else
        //    //{
        //    //    Console.WriteLine("Återbetalning ej betald.");
        //    //}


        public override string ToString()
        {
            return $"Kontonummer: {AccountNumber}, Saldo: {Balance:F} {Currency:F}, Kvar att betala: {LoanAmount:F} {Currency:F}, Ränta: {InterestRate:F}";
        }
    }
}


