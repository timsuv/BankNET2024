using BankApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    internal class ManageBank
    {
        public static List<IUser>? Users { get; set; } =
            [
                new User("Joel", "A", "O", "D", "ddd", new List<Account> { new Account("Acc10", 10000), new Account("Save001", 20000) }), // Temp User
                new User("Tim", "A", "O", "D", "ddd", new List<Account> { new Account("Acc20", 1000) }), // Temp User
                new Admin("Ossy", "C", "Ossy", "A") // Admin
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
        public async Task LogIn()
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

                await Task.Delay(1000); // Simulera en liten fördröjningsprocess

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
                    else if (tempUser is User)
                    {
                        await UserMenu(tempUser);
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
        private async Task UserMenu(IUser user)
        {
            var tempUser = (User)user;
            Menu menu = new(["Withdraw", "Deposit", "Min info", "Transfer", "Mina Transaktioner", "Exit"], "Bank menu");
            while (true)
            {
                switch (menu.MenuRun())
                {
                    case 0:
                        var account = tempUser.GetAccount();
                        if (account != null)
                        {
                            account.Withdraw();
                        }
                        else
                        {
                            Console.WriteLine("Ingen giltig konto hittades.");
                        }
                        break;
                    case 1:
                        var tempAcc = tempUser.GetAccount();
                        if (tempAcc != null)
                        {
                            tempAcc.Deposit();
                        }
                        break;
                    case 2:
                        Console.WriteLine(tempUser);
                        tempUser.DisplayAccounts();
                        Console.ReadLine();
                        break;
                    case 3:
                        await Transfer(tempUser);
                        Console.ReadLine();
                        break;
                    case 4:
                        ShowTransferLog(tempUser.GetAccount());
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
        private void AdminMenu(IUser user)
        {
            Menu menu = new(["Show all Users", "Delete User"], "Admin menu");

            while (true)
            {
                switch (menu.MenuRun())
                {
                    case 0:
                        if (Users != null)
                        {
                            foreach (var u in Users)
                            {
                                Console.WriteLine(u);
                            }
                        }
                        GetAllAccountNumbers();
                        Console.ReadLine();
                        break;
                    case 1:
                        DeleteUser();
                        break;
                    default:
                        break;
                }
            }
        }
        private void DeleteUser()
        {
            Console.WriteLine("Ange användarnamn: ");
            string? userName = Console.ReadLine();
            var userToDelete = Users?.Find(u => u.Username == userName);

            if (userToDelete != null && userToDelete is not Admin)
            {
                Users?.Remove(userToDelete);
                Console.WriteLine("Användaren togs bort.");
            }
            else
            {
                Console.WriteLine("Något gick fel.");
            }
            Console.ReadLine();
        }
        private async Task Transfer(User user)
        {
            // Get the account from which the money will be transferred
            var fromAccount = user.GetAccount();

            // Prompt the user to enter the account number to which the money will be transferred
            Console.WriteLine("Till vilket konto: ");
            string? inputToAccount = Console.ReadLine();

            // Find the user and account that matches the entered account number
            var toUser = Users?.OfType<User>().FirstOrDefault(u => u.Accounts.Any(a => a.AccountNumber == inputToAccount));
            var toAccount = toUser?.Accounts.FirstOrDefault(a => a.AccountNumber == inputToAccount);

            // Prompt the user to enter the amount of money to transfer
            Console.WriteLine("Hur mycket pengar: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                // Check if the destination account exists and if the amount is less than the balance of the destination account
                if (toAccount != null && amount < toAccount.Balance)
                {
                    toAccount.Balance += amount;
                    fromAccount.Balance -= amount;

                    Console.WriteLine("Skickar...");
                    await Task.Delay(1000); // Simulate a delay

                    // Log the transfer details
                    Console.WriteLine($"Pengarna skickdes från {fromAccount.AccountNumber} ny balans; {fromAccount.Balance} till {toAccount.AccountNumber} ny balans {toAccount.Balance}\n");

                    // Add transaction logs to both accounts
                    fromAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {amount} till {toAccount.AccountNumber}"));
                    toAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {amount} från {fromAccount.AccountNumber}"));
                }
                else
                {
                    // Display an error message if something went wrong
                    Console.WriteLine("Nåt gick fel");
                }
            }
            else
            {
                // Display an error message if the entered amount is invalid
                Console.WriteLine("Ogiltigt belopp.");
            }
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
        private static void ShowTransferLog(Account account1)
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
