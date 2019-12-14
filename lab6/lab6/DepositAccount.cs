using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class DepositAccount : Account
    {
        public DepositAccount(Client client, DateTime endDate, double percent, AccountDecorator D)
        {
            this.client = client;
            if(D != null)
            {
                D.Decorate(this);
            }
            this.percent = percent;
            this.accountType = 2;
            this.date = endDate;
        }

        public override bool WithDraw(double money)
        {
            handler.Handle(this);
            if (DateTime.Now < this.date) return false;
            this.money -= money;
            return true;
        }

        public override bool TopUp(double money)
        {
            handler.Handle(this);
            this.money += money;
            return true;
        }

        public override bool Transfer(double money, Account account)
        {
            handler.Handle(this);
            if (account.client != this.client || DateTime.Now < this.date) return false;
            account.money += money;
            this.money -= money;
            return true && verif;
        }
    }
}
