namespace BankNET2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //New Account
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
            
            
            
            
            
        }
    }
}
 