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
        List<User> users = new List<User>
        {
            new User(true, "001", "admin"),
            new User(false, "002", "12345"),
            new User(false,"003", "54321")
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
            var tempUser = users.Find(u => u.UserName == userName);
            if (tempUser != null && tempUser.PassWord == password)
            {

                return true;
            }
            return false;
        }
        private void UserMainMenu()
        {
            List<string> options = ["Metod 1", "Metod 1", "Metod 1", "Metod 1", "Exit"];
            Menu menu = new(options, "menu");

            menu.MenuRun();
        }
//        private void CreateAccount()
//        {
//            var newAccount = new Account(
//                accountNumber: "1",
//                balance: 50036.32M,
//                name: "Bob Åberg",
//                contactInfo: "bob@gmail.com, 0728539675",
//                amount: 0M,
//                password: "apa1"
//            );

//            var newAccount2 = new Account(
//            accountNumber: "22",
//            balance: 40036.32M,
//            name: "Emil Lönneberga",
//            contactInfo: "Emil.233@gmail.com, 0721939396",
//            amount: 0M,
//            password: "apa2"
//        );
//            var newAccount3 = new Account(
//                accountNumber: "333",
//                balance: 30036.32M,
//                name: "Anders Strömberg",
//                contactInfo: "Anders.213@gmail.com, 0721602756",
//                amount: 0M,
//                password: "apa3"
//);

//            Accounts.Add(newAccount);
//            Accounts.Add(newAccount2);
//            Accounts.Add(newAccount3);
//        }

    }
}
