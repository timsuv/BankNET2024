using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class CurrencyChange
    {
        //Declare Euro and Usd 
        public decimal Euro { get; set; } 
        public decimal Usd { get; set; }
        
        //Override constructor
        public CurrencyChange()
        {
        }

        //Update Euro & Usd methods
        public decimal newRateEuro;
        public decimal newRateUsd;
        public void UpdateEuro(decimal newRateEuro)
        {
            Euro = newRateEuro;
        }
        
        public void UpdateUsd(decimal newRateUsd)
        {
            Usd = newRateUsd;
        }
        
        //Method to return latest Euro & Usd rates for account
        public decimal getEuro()
        {
            return Euro ;
        }
        
        public decimal getUsd()
        {
            return Usd;
        }
        
    }
}
