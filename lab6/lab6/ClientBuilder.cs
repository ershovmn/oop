using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class ClientBuilder
    {
        private string firstName;
        private string lastName;
        private string address = null;
        private int number = -1;


        public Client Build()
        {
            if (address == null && number == -1 ) return new Client(firstName, lastName);
            if (address == null) return new Client(firstName, lastName, number);
            if (number == -1) return new Client(firstName, lastName, address);
            return new Client(firstName, lastName, address, number);
        }

        public ClientBuilder AddName(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            return this;
        }

        public ClientBuilder AddAddress(string address)
        {
            address = address;
            return this;
        }

        public ClientBuilder AddPassportNumber(int passportName)
        {
            number = passportName;
            return this;
        }
    }
}
