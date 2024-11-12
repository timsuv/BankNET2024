namespace BankNET2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ManageBank manageBank = new(); // Create a new instance of the ManageBank class

            await manageBank.LogIn(); // Call the LogIn method of the ManageBank class
        }
    }
}
 