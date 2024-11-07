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

            new User("A", "A", "O", "D", "ddd", [new Account("Def", 10000), new Account("s", 20000)]), // Temp User
            new User("C", "A", "O", "D", "ddd", [new Account("def", 1000)]), // Temp User
            new Admin("Ad", "C")

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
                        AdminMenu(tempUser);
                    }
                    else if(tempUser is User)
                    {
                        UserMenu(tempUser);
                    }
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
        private void UserMenu(IUser user)
        {
            var tempUser = (User)user;

            List<string> options = ["Withdraw", "Deposit", "Min info" , "Min Transfer"];

            Menu menu = new(options, "Bank menu");

            while (true)
            {

                switch (menu.MenuRun())
                {
                    case 0:
                        var account = tempUser.GetAccount();
                        if (account != null)
                        {
                            account.TempWithdraw();
                        }
                        else
                        {
                            Console.WriteLine("Ingen giltig konto hittades.");
                        }
                        break;
                    case 1:
                        var tempAcc = tempUser.GetAccount();
                        if(tempAcc != null)
                        {
                            tempAcc.Deposit2();
                        }

                        break;
                    case 2:
                        Console.WriteLine(tempUser);
                        tempUser.DisplayAccounts();
                        Console.ReadLine();
                        break;
                    case 3:
                        GetAllAccountNumbers();
                        Console.ReadLine();
                        break;

                    default:
                        break;
                }
            }
        }
        private void AdminMenu(IUser user)
        {

        }

        private void Transfer()
        {
           
        }
        private void GetAllAccountNumbers()
        {
            Console.WriteLine("\nAlla kontonummer över alla användare:");
            if (Users != null)
            {
                foreach (var user in Users)
                {
                    if (user is User tempUser)
                    {
                        foreach (var account in tempUser.Accounts)
                        {
                            Console.WriteLine($"Användare: {tempUser.Username}, Kontonummer: {account.AccountNumber}");
                        }
                    }
                }
            }
        }
        private bool ValidLogIn(string? userName, string password)
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
