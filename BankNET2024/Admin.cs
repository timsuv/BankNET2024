using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNET2024
{
    public class Admin : IUser
    {
        private static int employeeCounter = 1000; // Start value for employee ID   

        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
            EmployeeID = GenerateEmployeeID();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeID { get; private set; }
        

        private string GenerateEmployeeID()
        {
            return $"EMP{employeeCounter++}";
        }

    }
}
