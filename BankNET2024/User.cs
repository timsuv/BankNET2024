namespace BankNET2024;

public class User(bool isAdmin, string userName, string passWord, string contactInformation = "")
{

    public bool IsAdmin { get; set; } = isAdmin;
    public string UserName { get; set; } = userName;
    public string PassWord { get; set; } = passWord;
    public string ContactInformation { get; set; } = contactInformation;
    public List<Account> Accounts { get; set; } = []; // Initierar kontolistan



}