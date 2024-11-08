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

            new User("Joel", "A", "O", "D", "ddd", [new Account("Acc10", 10000), new Account("Save001", 20000)]), // Temp User
            new User("Tim", "A", "O", "D", "ddd", [new Account("Acc20", 1000)]), // Temp User
            new Admin("Ossy", "C") // Admin

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

            List<string> options = ["Withdraw", "Deposit", "Min info" , "Transfer", "Mina Transaktioner"];

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
                        Transfer(tempUser);
                        Console.ReadLine();
                        break;
                    case 4:
                        ShowTransferLog(tempUser.GetAccount());
                        break;

                    default:
                        break;
                }
            }
        }
        private void AdminMenu(IUser user)
        {

        }

        private void Transfer(User user)
        {
            GetAllAccountNumbers();
            Console.WriteLine("-------------------------");
            var fromAccount = user.GetAccount();

            Console.WriteLine("Till vilket konto: ");
            string? inputToAccount = Console.ReadLine();

            var toUser = Users?.OfType<User>().FirstOrDefault(u => u.Accounts.Any(a => a.AccountNumber == inputToAccount));
            var toAccount = toUser?.Accounts.FirstOrDefault(a => a.AccountNumber == inputToAccount);

            Console.WriteLine("Hur mycket pengar: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            toAccount.Balance += amount;
            fromAccount.Balance -= amount;

            Console.WriteLine($"Pengarna skickdes från {fromAccount} till {toAccount}\n");

            fromAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {amount} till {toAccount.AccountNumber}"));
            toAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {amount} från {fromAccount.AccountNumber}")); 

            GetAllAccountNumbers();


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
                            Console.WriteLine($"Användare: {tempUser.Username}, Kontonummer: {account.AccountNumber}, Amount");
                        }
                    }
                }
            }
        }
        private void ShowTransferLog(Account account1)
        {
            if (account1 != null)
            {
                Console.WriteLine($"Visar transaktionshistorik för konto {account1.AccountNumber}");
                if (account1.Transactions != null && account1.Transactions.Count > 0)
                {
                    foreach (var transaction in account1.Transactions)
                    {
                        transaction.DisplayTransactionHistory();
                    }
                }
                else
                {
                    Console.WriteLine("Inga transaktioner hittades.");
                }
            }
            else
            {
                Console.WriteLine("Ingen giltig konto hittades.");
            }
            Console.ReadKey();
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
