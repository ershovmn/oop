using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public abstract class Account
    {
        public abstract bool WithDraw(double money);
        public abstract bool TopUp(double money);
        public abstract bool Transfer(double money, Account account);

        public double money;
        public double percent;
        public double commision;

        public DateTime date;

        double limit;

        public Client client;

        public int accountType;

        public IHandler handler;

        public bool verif;
    }
}
