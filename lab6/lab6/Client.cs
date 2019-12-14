using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Client
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; set; }
        public int PassportNumber { get; set; }

        List<Account> accounts;

        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Client(string firstName, string lastName, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public Client(string firstName, string lastName, int number)
        {
            FirstName = firstName;
            LastName = lastName;
            PassportNumber = number;
        }

        public Client(string firstName, string lastName, string address, int number)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PassportNumber = number;
        }
    }
}
