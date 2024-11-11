namespace BankNET2024
{
    public class User : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Account> Accounts { get; set; }
        //private int _loans = 5;

        public User(string username, string password, string firstName, string lastName, string phoneNumber, List<Account> accounts)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Accounts = accounts;

        }
        public Account? GetAccount()
        {
            Console.WriteLine("Vilket konto: ");
            string? account = Console.ReadLine();

            var foundAccount = Accounts.FirstOrDefault(a => a.AccountNumber == account);
            if (foundAccount == null)
            {
                Console.WriteLine("Konto hittades inte.");
            }
            return foundAccount;
        }
        public void ChangeCurrency()
        {
            Account? account = GetAccount();
            if (account != null)
            {
                Console.WriteLine("Vilken valuta vill du byta till? (SEK, USD, EUR)");
                string currency = Console.ReadLine();
                if (currency == "SEK" || currency == "USD" || currency == "EUR")
                {
                    account.Currency = currency;
                    Console.WriteLine($"Valutan har ändrats till {currency}.");
                }
                else
                {
                    Console.WriteLine("Ogiltig valuta.");
                }
            }
        }
        public void CreateNewAccount()
        {
            Console.WriteLine("Vad för slags konto vill du skapa?\n1. Vanligt konto\n2. Sparkonto");
            int.TryParse(Console.ReadLine(), out int choice);
            if (choice != 1 && choice != 2)
            {
                Console.WriteLine("Ogiltigt val. Ange 1 eller 2.");
            }
            else
            {
                Console.WriteLine("Hur mycket vill du sätta in på kontot?");
                decimal.TryParse(Console.ReadLine(), out decimal initialBalance);
                if (initialBalance > 0 && initialBalance < 1000000)
                {
                    string accountNumber = Guid.NewGuid().ToString();
                    if (choice == 1)
                    {
                        Account newAccount = new Account(accountNumber, initialBalance);
                        Console.WriteLine($"Nu har ett nytt konto skapats med kontonummer {accountNumber} och saldo {initialBalance}.");
                        this.Accounts.Add(newAccount);
                    }
                    else
                    {
                        Account newAccount = new SavingAccount(accountNumber, initialBalance);
                        Console.WriteLine($"Nu har ett nytt sparkonto skapats med kontonummer {accountNumber} och saldo {initialBalance}.");
                        this.Accounts.Add(newAccount);
                    }

                }
                else
                {
                    Console.WriteLine("Ogiltig mängd. Ange ett positivt belopp.");
                }
            }
        }
        public void DisplayAccounts()
        {
            //visar infos om alla accounts med bara nummer och balance
            if (Accounts != null)
            {
                foreach (var account in Accounts)
                {
                    Console.WriteLine(account);
                }
            }
        }
        public override string ToString()
        {
            return $"Username: {Username}, Password: ****, FirstName: {FirstName}, LastName: {LastName}, " +
            $"PhoneNumber: {PhoneNumber}";
        }
    }
}
