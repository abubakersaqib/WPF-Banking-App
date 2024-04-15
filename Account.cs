using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBS_Bank
{
    internal class Account
    {
        public string FirstName { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostalAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AccountType { get; set; } = "";
        public int AccountNumber { get; set; }
        public int SortCode { get; set; }
        public int InitialBalance { get; set; }
        public int OverdraftLimit { get; set; }

        public Account(int accountNumber, string firstName, string surname, string email, string phone, string postalAddress, string permanentAddress,
                  string city, string country, string accountType, int sortCode,
                  int initialBalance, int overdraftLimit)
        {
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Phone = phone;
            PostalAddress = postalAddress;
            PermanentAddress = permanentAddress;
            City = city;
            Country = country;
            AccountType = accountType;
            AccountNumber = accountNumber;
            SortCode = sortCode;
            InitialBalance = initialBalance;
            OverdraftLimit = overdraftLimit;
        }
        public Account(int accountNumber, string email, string phone, string postalAddress, string permanentAddress, string city, string country, int overdraftLimit)
        {
            AccountNumber = accountNumber;
            Email = email;
            Phone = phone;
            PostalAddress = postalAddress;
            PermanentAddress = permanentAddress;
            City = city;
            Country = country;
            OverdraftLimit = overdraftLimit;

        }


    }

}
