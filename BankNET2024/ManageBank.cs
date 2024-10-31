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
        Dictionary<string, string> users = new()
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

            // Skriv ut ASCII-konsten
            Console.WriteLine(bankArt);
        }
        public void TempLogIn()
        {
            var attempts = 3;
            var password = "";
            var userNamn = "";

            while (attempts != 0)
            {
                Console.Write("Skriv in användarnamn: ");
                userNamn = Console.ReadLine();

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

                if (ValidLogIn(userNamn, password))
                {
                    UserMainMenu();
                    break;
                }
                else
                {
                    Console.WriteLine("Try again");
                }

                attempts--;
            }
            Console.WriteLine("SLut på försökt hejdå");
        }
        public bool ValidLogIn(string userName, string password)
        {
            if (users.ContainsKey(userName) && users[userName] == password)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        private void UserMainMenu()
        {
            List<string> options = ["Metod 1", "Metod 1", "Metod 1", "Metod 1"];
            Menu menu = new(options, "menu");

            menu.MenuRun();
        }


    }
}
