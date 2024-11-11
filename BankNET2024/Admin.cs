using System;
using System.Collections.Generic;

namespace BankNET2024
{
    public class Admin : IUser
    {
        private static int _employeeCounter = 0; // Start value for employee ID   
        private static Dictionary<string, decimal> _currencyDictionary = new()
        {
            { "SEK", 1 },
            { "USD", 10.50m }, // Amerikansk dollar
            { "EUR", 11.00m }, // Euro
            { "GBP", 12.70m }, // Brittiskt pund
            { "NOK", 1.00m },  // Norsk krona
            { "DKK", 1.50m },  // Dansk krona
            { "JPY", 0.075m }, // Japansk yen
            { "CNY", 1.40m },  // Kinesisk yuan
            { "INR", 0.13m },  // Indisk rupie
            { "CHF", 11.70m }, // Schweizisk franc
            { "CAD", 8.00m }
        };

        public Admin(string username, string password, string firstName, string lastName)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            EmployeeID = GenerateEmployeeID();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID { get; private set; }

        private string GenerateEmployeeID() // Generate uniq employee ID
        {
            return $"EMP{_employeeCounter++}";
        }

        // Returnerar valutadictionary som är statiskt
        public static Dictionary<string, decimal> GetCurrencyDictionary()
        {
            return _currencyDictionary;
        }

        // Metod för att ändra valutakurs i den statiska dictionaryn
        public void ChangeCurrencyRate()
        {
            Console.WriteLine("Vilken valuta vill du ändra värdet på?");
            string currency = Console.ReadLine().ToUpper();    
            Console.WriteLine("Ange det nya värdet: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal rate))
            {
                if (_currencyDictionary.ContainsKey(currency))
                {
                    _currencyDictionary[currency] = rate;
                    Console.WriteLine($"Växelkursen för {currency} har ändrats till {rate}.");
                }
                else
                {
                    Console.WriteLine($"Valutan'{currency}' finns inte bland valutorna.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt värde.");
            }
            
        }

        public override string ToString()
        {
            return $"Admin Info: Användarnamn: {Username}, Förnamn: {FirstName}, Efternamn: {LastName}, Employee ID: {EmployeeID}";
        }
    }
}
