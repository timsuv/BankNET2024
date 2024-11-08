namespace BankNET2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //New Account for testing
            Account JoshuaAccount = new Account("10101", 200000000);

            //New List
            List<Account> joshuaAccounts = new List<Account>();

            //New User
            User user1 = new User("Joshua", "1234", "Joshua", "Ng", "0707070707", joshuaAccounts);
            
            //Apply for Loan, Withdraw & Display amount
            LoanAccount user1Loan = new LoanAccount(JoshuaAccount.AccountNumber, JoshuaAccount.Balance);
            user1Loan.ApplyForLoan(400.00m);
            user1Loan.DisplayLoanAmount();
            user1Loan.Withdraw(200.00m);
            user1Loan.DisplayLoanAmount();

            //Create Saving account to test
            SavingAccount user1Saving = new SavingAccount(JoshuaAccount.AccountNumber, JoshuaAccount.Balance, 0);
            
            //Deposit into saving and check balance, withdraw then check again
            user1Saving.Deposit(100);
            user1Saving.DisplayBalance();
            user1Saving.Withdraw(50);
            user1Saving.DisplayBalance();
            
            //Test exchange, default is set at 0 so answer should be €0 $0
            JoshuaAccount.Exchange();
            
            //List for admin
            List<Account> adminList = new List<Account>(); 
            //Create test admin to update exchange rate
            Admin admin = new Admin("admin", "password", "Ad","Min","07070722", adminList);

            admin.UpdateEuro(2.00m);
            admin.UpdateUsd(2.44m);
            
            //Test exchange 
            JoshuaAccount.Exchange();
            



        }
    }
}
 