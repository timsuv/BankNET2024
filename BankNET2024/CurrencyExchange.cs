using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public static class CurrencyExchange 
    {
        //Exchange rate for different USD & EUR
        public decimal USD;
        public decimal EUR;
        
        // Method for admin to update currency rate
        public void UpdateRate()
        {
            Console.WriteLine("Update USD rate");
            USD = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Update EUR rate");
            EUR = decimal.Parse(Console.ReadLine());
        }
        
        //Method to allow user to view balance in different account rates
        public void Exchange()
        {
            Console.WriteLine($"Your account balance is {Balance} SEK");
            Console.WriteLine($"Your account balance in USD is {Balance * USD} USD");
            Console.WriteLine($"Your account balance in EUR is {Balance * EUR} EUR");
        }
        
    }
}
