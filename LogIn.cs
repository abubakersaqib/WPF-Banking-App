using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBS_Bank
{
    internal class LogIn
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LogIn(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
