namespace BankNET2024;

public class Loan
{
    //Interest rate on loan
    public decimal InterestRate;
    public int Salary;
    public int LoanRequest;
    public int LoanAmount;

    //Method for admin to change interest rate
    public void UpdateInterestRate()
    {
        Console.WriteLine("Update the Interest Rate %");
        InterestRate = decimal.Parse(Console.ReadLine());
    }
    
    //Method for user to take loan, with restriction on how much they can take
    public void TakeLoan()
    {
        Console.WriteLine("Enter your monthly salary");
        Salary = int.Parse(Console.ReadLine());

        Console.WriteLine("How much loan");
        LoanRequest = int.Parse(Console.ReadLine());

        if ((Salary * 5) > LoanRequest)
        {
            LoanRequest = LoanAmount;
            Balance += LoanAmount;
        }
        else
        {
            Console.WriteLine("Loan denied. Apply for a smaller loan");
        }
}

}