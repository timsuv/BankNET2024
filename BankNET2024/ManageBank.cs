using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    internal class ManageBank
    {
        readonly List<string> tempPassword = [
            "pass",
            "1K1"
            ];

        Dictionary<string, string> användare = new Dictionary<string, string>
        {
            { "a", "l" }
        };

        public ManageBank()
        {
            string bankArt = @"
  ____            _    
 |  _ \          | |   
 | |_) | __ _ ___| | __
 |  _ < / _` / __| |/ /
 | |_) | (_| \__ \   < 
 |____/ \__,_|___/_|\_\";

            var attempts = 3;
            // Skriv ut ASCII-konsten
            Console.WriteLine(bankArt);
        }
        public void TempLogIn()
        {
            var attempts = 3;
            var password = "";

            while (attempts != 0)
            {


                Console.Write("Skriv in lösenordet: ");
                password = string.Empty; // Återställ lösenordet för varje inmatning

                // Läs in tangenttryckningar utan att visa dem
                while (true)
                {
                    var key = Console.ReadKey(intercept: true); // Intercept: true döljer inmatningen

                    if (key.Key == ConsoleKey.Enter) // Avsluta när Enter trycks
                        break;

                    password += key.KeyChar; // Lägg till tecknet i lösenordet
                    Console.Write("*"); // Visa en asterisk istället
                }

                Console.WriteLine(); // Ny rad efter lösenordet har skrivits in

                if (tempPassword.Any(p => p == password))
                {
                    Console.WriteLine("Works");
                    break;
                }
                else
                {
                    Console.WriteLine("Försök igen");
                }
                attempts--;
            }
            Console.WriteLine("Slut på försök, appen stänger");
        }
        public void LogIn()
        {
            var attempts = 3;
            var password = "";

            while (attempts != 0)
            {
                Console.Write("Skriv in lösenordet: ");
                password = string.Empty; // Återställ lösenordet för varje inmatning

                // Läs in tangenttryckningar utan att visa dem
                while (true)
                {
                    var key = Console.ReadKey(intercept: true); // Intercept: true döljer inmatningen

                    if (key.Key == ConsoleKey.Enter) // Avsluta när Enter trycks
                        break;

                    password += key.KeyChar; // Lägg till tecknet i lösenordet
                    Console.Write("*"); // Visa en asterisk istället
                }

                Console.WriteLine(); // Ny rad efter lösenordet har skrivits in

                if (tempPassword.Any(p => p == password))
                {
                    Console.WriteLine("Works");
                    break;
                }
                else
                {
                    Console.WriteLine("Försök igen");
                }
                attempts--;
            }
            Console.WriteLine("Slut på försök, appen stänger");
        }
        public string Username()
        {
            Console.WriteLine("Skriv användarname: ");
            return Console.ReadLine();
        }
    }
}
