using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class ForeignAccount : Account
    {
        public Currency Currency { get; set; }
        public ForeignAccount(string accountNumber, decimal balance, Currency currency) : base(accountNumber, balance)
        {
            Currency = currency;

        }
        public void DisplayCurrencies()
        {
            foreach (Currency currency in Enum.GetValues(typeof(Currency)))
            {
                Console.WriteLine(currency);
            }
        }
        public override string ToString()
        {
            return $"Account number: {AccountNumber}, Balance: {Balance:C2}, Currency {Currency} ";
        }


    }
}
