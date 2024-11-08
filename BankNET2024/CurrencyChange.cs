using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class CurrencyChange
    {
        
        //Variables and methods here of static type 
        //Declare Euro and Usd 
        public static decimal Euro { get; private set; } 
        public static decimal Usd { get; private set; }
        
        
        //Update Euro & Usd methods
        public static void UpdateEuro(decimal newRateEuro)
        {
            Euro = newRateEuro;
        }
        
        public static void UpdateUsd(decimal newRateUsd)
        {
            Usd = newRateUsd;
        }
        
        //Method to return latest Euro & Usd rates for account
        public static decimal getEuro()
        {
            return Euro ;
        }
        
        public static decimal getUsd()
        {
            return Usd;
        }
        
    }
}
