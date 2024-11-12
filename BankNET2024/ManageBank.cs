using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    internal class ManageBank
    {
        private static List<IUser>? _users =
        [
                new User("User1", "1", "Sussie", "Ekeberg", "0761772149", [new Account("Acc10", 10000), new Account("Acc30", 20000)]), // Temp User
                new User("User2", "2", "Lars", "Larsson", "0731235647", [new Account("Acc20", 1000), new SavingAccount("Save10", 10000)]), // Temp User
                new Admin("Admin", "3", "Ossy", "A") // Admin
        ];
        public void Bankart()
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
        public ManageBank()
        {
            Bankart();
        }
        static string MaskInput()
        {
            SecureString password = new SecureString();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (!char.IsControl(key.KeyChar))
                {
                    password.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.RemoveAt(password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            return new System.Net.NetworkCredential(string.Empty, password).Password;

        }
        public async Task LogIn()
        {
            var attempts = 3;
            string? userName;

            while (attempts != 0) // Loop until the attempts are exhausted
            {
                Console.Write("Ange användarnamn: "); // Prompt the user to enter the username
                userName = Console.ReadLine();

                Console.Write("Ange lösenordet: "); // Prompt the user to enter the password
                string password = MaskInput();



                Console.WriteLine(); // New line after the password is entered

                if (ValidLogIn(userName, password))
                {

                    var tempUser = _users?.FirstOrDefault(user => user.Username == userName && user.Password == password); // Get the user object
                    Console.WriteLine("Loggning pågår ....");
                    await Task.Delay(2000);
                    if (tempUser is Admin) // Check if the user is an admin or user
                    {
                        await AdminMenu(tempUser);
                    }
                    else if (tempUser is User)
                    {
                        await UserMenu(tempUser);
                    }
                    break;
                }
                else
                {
                    Console.Clear();
                    Bankart();
                    attempts--; // Decrement the attempts
                    Console.WriteLine($"Försök igen, försök kvar: {attempts}");
                }

            }
            Console.WriteLine("INGA FÖRSÖK KVAR"); // Display a message when the attempts are exhausted
            Environment.Exit(0);
        }

        private async Task UserMenu(IUser user)
        {
            var tempUser = (User)user; // Cast the user object to a User object
            Menu menu = new(["Uttag", "Insättning", "Min info", "Överförning", "Mina Transaktioner", "Byta valuta", "Skapa ny konto", "Ta ett lån", "Betala lånet", "Logga ut", "Avsluta"], "Bank meny"); // Create a menu object
            while (true)
            {
                switch (menu.MenuRun()) // Run the menu
                {
                    case 0:
                        var account = tempUser.GetAccount(); // Get the account
                        if (account != null)
                        {
                            account.Withdraw();
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Ingen giltig konto hittades.");
                        }
                        break;
                    case 1:
                        var tempAcc = tempUser.GetAccount();
                        tempAcc?.Deposit();
                        Console.ReadLine();
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
                        ShowTransferLog(tempUser?.GetAccount());
                        break;
                    case 5:
                        tempUser.ChangeCurrency();
                        Console.ReadLine();
                        break;
                    case 6:
                        tempUser.CreateNewAccount();
                        break;
                    case 7:
                        tempUser.TakeLoan();
                        Console.ReadLine();
                        break;
                    case 8:
                        tempUser.PayLoan();
                        break;
                    case 9:
                        await LogOut(user);
                        break;
                    case 10:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
        private async Task AdminMenu(IUser user)
        {
            var admin = (Admin)user;
            Menu menu = new(["Visa alla användrare", "Radera enn användare", "Byta valutas värde", "Visa alla valutor", "Logga ut"], "Admin meny");
            while (true)
            {
                switch (menu.MenuRun())
                {
                    case 0:
                        if (_users != null)
                        {
                            foreach (var u in _users)
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
                        Console.ReadLine();
                        break;
                    case 3:
                        foreach (var u in Admin.GetCurrencyDictionary())
                        {
                            Console.WriteLine($"{u.Key}: {u.Value}");
                        }
                        Console.ReadLine();
                        break;
                    case 4:
                        await LogOut(admin);
                        return;

                    default:
                        break;
                }
            }
        }
        private void DeleteUser()
        {
            Console.WriteLine("Ange användarnamn: ");
            string? userName = Console.ReadLine();
            var userToDelete = _users?.Find(u => u.Username == userName);

            if (userToDelete != null && userToDelete is not Admin)
            {
                _users?.Remove(userToDelete);
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
            Console.WriteLine("Ange kontot du vill skicka pengarna till: ");
            string? inputToAccount = Console.ReadLine();

            // Find the user and account that matches the entered account number
            var toUser = _users?.OfType<User>().FirstOrDefault(u => u.Accounts.Any(a => a.AccountNumber == inputToAccount));
            var toAccount = toUser?.Accounts.FirstOrDefault(a => a.AccountNumber == inputToAccount);

            // Prompt the user to enter the amount of money to transfer
            Console.WriteLine("Ange summan du vill skicka: ");
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
                        Console.WriteLine($"Pengarna skickades från {fromAccount.AccountNumber}, Ny balans: {fromAccount.Balance:F} {fromAccount.Currency} till {toAccount.AccountNumber}\n");

                        // Add transaction logs to both accounts
                        fromAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {amount:F} {fromAccount.Currency} till {toAccount.AccountNumber}"));
                        toAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Överföring: {convertedAmount:F} {toAccount.Currency} från {fromAccount.AccountNumber}"));
                    }
                    else
                    {
                        Console.WriteLine("Något gick fel");
                    }

                    
                }
                else
                {
                    // Display an error message if something went wrong
                    Console.WriteLine("Något gick fel");
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
            if (_users != null)
            {
                foreach (var user in _users)
                {
                    if (user is User tempUser)
                    {
                        foreach (var account in tempUser.Accounts)
                        {
                            Console.WriteLine($"Användare: {tempUser.Username}, Kontonummer: {account.AccountNumber}, Belopp {account.Balance}");
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
            var tempUser = _users?.Find(u => u.Username == userName);
            if (tempUser != null && tempUser.Password == password)
            {
                return true;
            }
            return false;
        }
        public async Task LogOut(IUser? user)
        {
            Console.WriteLine("Loggar ut...");
            await Task.Delay(2000);
            Console.Clear();
            Bankart();
            user = null;


            await LogIn();
        }

    }
}
