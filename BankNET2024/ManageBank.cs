using BankApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    internal class ManageBank
    {
        public List<User>? Users { get; set; } = [

            new User("A", "A", "O", "D", "ddd", 10000, new List<Account>()) // Temp User

            ];
        public ManageBank()
        {
            string bankArt = @"
███▄▄▄▄      ▄████████     ███     ▀█████████▄     ▄████████ ███▄▄▄▄      ▄█   ▄█▄ 
███▀▀▀██▄   ███    ███ ▀█████████▄   ███    ███   ███    ███ ███▀▀▀██▄   ███ ▄███▀ 
███   ███   ███    █▀     ▀███▀▀██   ███    ███   ███    ███ ███   ███   ███▐██▀   
███   ███  ▄███▄▄▄         ███   ▀  ▄███▄▄▄██▀    ███    ███ ███   ███  ▄█████▀    
███   ███ ▀▀███▀▀▀         ███     ▀▀███▀▀▀██▄  ▀███████████ ███   ███ ▀▀█████▄    
███   ███   ███    █▄      ███       ███    ██▄   ███    ███ ███   ███   ███▐██▄   
███   ███   ███    ███     ███       ███    ███   ███    ███ ███   ███   ███ ▀███▄ 
 ▀█   █▀    ██████████    ▄████▀   ▄█████████▀    ███    █▀   ▀█   █▀    ███   ▀█▀ 
                                                                         ▀         
";

            Console.WriteLine(bankArt);

        }
        public void LogIn()
        {
            var attempts = 3;
            string password;
            string? userNamn;

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
                    MainMenu();
                    break;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Try again, försök kvar: {attempts}");
                }
                if (attempts == 0)
                {
                    Console.WriteLine("SLUT PÅ FÖRSÖK");
                    Environment.Exit(0);
                }
            }
        }
        private void MainMenu() // Temp- need methods and check if its admin 
        {
            List<string> options = ["Transfer", "Withdraw", "Insert", "Create Account", "Take a loan", "Min information"];

            Menu menu = new(options, "Bank menu");

            menu.MenuRun();

        }
        public bool ValidLogIn(string? userName, string password)
        {
            var tempUser = Users.Find(u => u.Username == userName);
            if (tempUser != null && tempUser.Password == password)
            {
                return true;
            }
            return false;
        }
    }
}
