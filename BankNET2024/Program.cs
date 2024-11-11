namespace BankNET2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ManageBank manageBank = new();

            await manageBank.LogIn();
        }
    }
}
 