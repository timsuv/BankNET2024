using BankNET2024;
using System.Transactions;

internal class LoanAccount : Account
{
    private List<Account> allAccounts;

    public LoanAccount(string accountNumber, decimal balance, decimal loanAmount, List<Account> accounts, decimal interestRate = 0.03m, string currency = "SEK")
        : base(accountNumber, balance, currency)
    {
        LoanAmount = loanAmount;
        InterestRate = interestRate;
        Balance = loanAmount;
        allAccounts = accounts;
        Task.Run(() => IncreaseLoan());
    }

    public decimal LoanAmount { get; set; }
    public decimal InterestRate { get; set; }

    private async Task IncreaseLoan()
    {
        Console.WriteLine("Ange kontonummer för det konto som ska användas för att betala lånet:");
        string selectedAccountNumber = Console.ReadLine();
        Account selectedAccount = GetAccountByNumber(selectedAccountNumber);

        if (selectedAccount == null)
        {
            Console.WriteLine("Kontot hittades inte.");
            return;
        }

        while (LoanAmount > 0)
        {
            decimal monthlyInterest = LoanAmount * InterestRate;
            decimal monthlyPayment = LoanAmount / 12 + monthlyInterest;

            Transactions.Add(new TransactionLog(DateTime.Now, $"Ränta: {monthlyInterest}"));
            await Task.Delay(2000);

            if (selectedAccount.Balance >= monthlyPayment)
            {
                selectedAccount.Balance -= monthlyPayment;
                LoanAmount -= monthlyPayment;
                selectedAccount.Transactions.Add(new TransactionLog(DateTime.Now, $"Månadsbetalning: {monthlyPayment}"));
            }
            else
            {
                Console.WriteLine("Otillräckligt saldo på valt konto för att göra månadsbetalningen.");
                break;
            }
        }
    }

    private Account GetAccountByNumber(string accountNumber)
    {
        foreach (var user in ManageBank._users)
        {
            var account = user.Accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
            if (account != null)
            {
                return account;
            }
        }
        return null;
    }


    public override string ToString()
    {
        return $"Kontonummer: {AccountNumber}, Saldo: {Balance:F} {Currency:F}, Kvar att betala: {LoanAmount:F} {Currency:F}, Ränta: {InterestRate:F}";
    }
}

//public void RequestLoan()
//{
//    if (LoanAmount <= Balance * LoanLimit)
//    {
//        Account.Balance += LoanAmount;  // Lägg till lånet i kontosaldot
//        Account.Transactions.Add(new TransactionLog(DateTime.Now, $"Lån: {LoanAmount}{Account.Currency}"));
//        Console.WriteLine($"Lånet {LoanAmount} {Account.Currency} beviljat. Nuvarande saldo: {Account.Balance} {Account.Currency}");
//    }
//    else
//    {
//        Console.WriteLine("Lånet ej beviljat! Övestrider låne gräns baserat på saldo.");
//    }
//}

//public async Task PayLoan(decimal amount)
//{
//    var monthlyInterest = LoanAmount * InterestRate;
//    var monthlyPayment = LoanAmount / 12 + monthlyInterest;


//    while (LoanAmount > 0)
//    {

//        if (amount > monthlyPayment)
//        {
//            LoanAmount -= amount;
//            Account.Balance -= amount;
//            Account.Transactions.Add(new TransactionLog(DateTime.Now, $"Återbetalning: {amount}{Account.Currency}"));
//            Console.WriteLine($"Återbetalning: {amount} {Account.Currency}. Nuvarande saldo: {Account.Balance} {Account.Currency}");
//        }
//        else
//        {
//            Console.WriteLine("Återbetalning ej betald.");
//        }
//        await Task.Delay(10000);

//    }

//    //if (amount > 0)
//    //{
//    //    Account.Balance -= amount;
//    //    Console.WriteLine($"{amount} {Account.Currency} betald. nuvarande saldo: {Account.Balance} {Account.Currency}");
//    //}
//    //else
//    //{
//    //    Console.WriteLine("Återbetalning ej betald.");
//    //}




