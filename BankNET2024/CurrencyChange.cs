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

        
        //Constructor to create instance in admin
        public CurrencyChange(decimal euro, decimal usd)
        {
            this.Euro = euro;
            this.Usd = usd;
        }

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

        public decimal getEuro()
        {
            return newRateEuro ;
        }

        public decimal getUsd()
        {
            return newRateUsd;
        }
        
        
    }
}
