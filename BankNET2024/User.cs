namespace BankNET2024;

public class User
{
    public bool IsAdmin;
    public string AccountNumber;
    public string PassWord;
    public User(bool isAdmin, string accountNumber, string passWord)
    {
        IsAdmin = isAdmin;
        AccountNumber = accountNumber;
        PassWord = passWord;
    }

}