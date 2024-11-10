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
                new User("Joel", "A", "O", "D", "ddd", [new Account("Acc10", 10000), new Account("Acc30", 20000)]), // Temp User
                new User("Tim", "A", "O", "D", "ddd", [new Account("Acc20", 1000), new SavingAccount("Save10", 10000)]), // Temp User
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
            Menu menu = new(["Withdraw", "Deposit", "Min info", "Transfer", "Mina Transaktioner", "Change Currency", "Exit"], "Bank menu");
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
                        tempAcc?.Deposit();
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
                        ChangeCurrency(tempUser);
                        Console.ReadLine();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                        
                    default:
                        break;
                }
            }
        }
        private void AdminMenu(IUser user)
        {
            var admin = (Admin)user;
            Menu menu = new(["Show all Users", "Delete User", "Change Currency value", "Show dict"], "Admin menu");
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
                    case 2:
                        admin.ChangeCurrencyRate();
                        break;
                    case 3:
                        foreach (var u in Admin.GetCurrencyDictionary())
                        {
                            Console.WriteLine($"{u.Key}: {u.Value}");
                        }
                        Console.ReadLine();
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
        private async Task Transfer2(User user)
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
                // Check if the destination account exists and if the amount is less than the balance of the source account
                if (toAccount != null && fromAccount != null && amount <= fromAccount.Balance)
                {
                    // Convert the amount to the destination account's currency
                    decimal convertedAmount = ChangeTransferAmount(amount, fromAccount, toAccount);

                    // Perform the transfer
                    toAccount.Balance += convertedAmount;
                    fromAccount.Balance -= amount;
                    if (convertedAmount != 0)
                    {
                        Console.WriteLine($"Omvandlad summa {convertedAmount}");
                        Console.WriteLine("Skickar...");
                        await Task.Delay(1000); // Simulate a delay
                                                // Log the transfer details
                        Console.WriteLine($"Pengarna skickades från {fromAccount.AccountNumber} ny balans: {fromAccount.Balance} till {toAccount.AccountNumber} ny balans: {toAccount.Balance}\n");

                        // Add transaction logs to both accounts
                        fromAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {amount} {fromAccount.Currency} till {toAccount.AccountNumber}"));
                        toAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {convertedAmount} {toAccount.Currency} från {fromAccount.AccountNumber}"));
                    }
                    else
                    {
                        Console.WriteLine("Något gick fel");
                    }

                    
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
        private decimal ChangeTransferAmount(decimal amount, Account fromAccount, Account toAccount)
        {
            var currencyDictionary = Admin.GetCurrencyDictionary();

            try 
            {
                // Kontrollera om valutorna finns i valutadictionaryn
                if (currencyDictionary.TryGetValue(fromAccount.Currency, out decimal fromExchangeRate) &&
                currencyDictionary.TryGetValue(toAccount.Currency, out decimal toExchangeRate))
                {
                    // Omvandla beloppet från källkontots valuta till målkontots valuta
                    decimal convertedAmount;
                    if (fromExchangeRate > toExchangeRate)
                    {
                        convertedAmount = amount * (fromExchangeRate / toExchangeRate);
                    }
                    else
                    {
                        convertedAmount = amount / (toExchangeRate / fromExchangeRate);
                    }
                    return convertedAmount;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Något gick fel {ex.Message}");
            }
            return 0;
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
                            Console.WriteLine($"Användare: {tempUser.Username}, Kontonummer: {account.AccountNumber}, Amount {account.Balance}");
                        }
                    }
                }
            }
        }
        private static void ShowTransferLog(Account account)
        {
            if (account != null)
            {
                Console.WriteLine($"Visar transaktionshistorik för konto {account.AccountNumber}");
                if (account.Transactions != null && account.Transactions.Count > 0)
                {
                    foreach (var transaction in account.Transactions)
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
        private void ChangeCurrency(User user)
        {
            var acc = user.GetAccount();

            if (acc != null)
            {
                Console.WriteLine("Vilken valuta vill du byta till?");
                var currencyDictionary = Admin.GetCurrencyDictionary();
                foreach (var currency in currencyDictionary)
                {
                    Console.WriteLine(currency.Key);
                }
                string newCurrency = Console.ReadLine().ToUpper();
                if (currencyDictionary.TryGetValue(newCurrency, out decimal newExchangeRate) &&
                    currencyDictionary.TryGetValue(acc.Currency, out decimal currentExchangeRate))
                {
                    if (currentExchangeRate > newExchangeRate)
                    {
                        acc.Balance *= (currentExchangeRate / newExchangeRate);
                    }
                    else
                    {
                        acc.Balance /= (newExchangeRate / currentExchangeRate);
                    }
                    acc.Currency = newCurrency;
                    Console.WriteLine($"Currency changed to {acc.Currency}. New balance: {acc.Balance:F2}  {acc.Currency:F}");
                }
                else
                {
                    Console.WriteLine("Ogiltig valuta");
                }


            }
        }
    }
}
