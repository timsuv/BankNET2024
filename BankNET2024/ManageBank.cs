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
        public List<IUser>? Users { get; set; } = [

            new Userr("A", "A", "O", "D", "ddd", []), // Temp User
            new Admin("A", "C")

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
            string? userName;

            while (attempts != 0)
            {
                Console.Write("Skriv in användarnamn: ");
                userName = Console.ReadLine();

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

                if (ValidLogIn(userName, password))
                {
                    var tempUser = Users?.FirstOrDefault(user => user.Username == userName && user.Password == password);

                    if (tempUser is Admin)
                    {
                        Console.WriteLine("ADMIN WORK");
                    }
                    else if(tempUser is Userr)
                    {
                        Console.WriteLine("IS USER");
                    }

                   // MainMenu(tempUser);
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
        private void MainMenu(IUser user) // Temp- need methods and check if its admin 
        {
            List<string> options = ["Transfer", "Withdraw", "Insert", "Create Accountt", "Take a loan", "Min information"];

            Menu menu = new(options, "Bank menu");

            switch (menu.MenuRun())
            {
                case 5:
                    Console.WriteLine(user);
                    break;
                    
                default:
                    break;
            }

        }
        private void UserMenu(IUser user)
        {

        }
        private void AdminMenu(IUser user)
        {

        }

        public bool ValidLogIn(string? userName, string password)
        {
            var tempUser = Users?.Find(u => u.Username == userName);
            if (tempUser != null && tempUser.Password == password)
            {
                return true;
            }
            return false;
        }
    }
}
