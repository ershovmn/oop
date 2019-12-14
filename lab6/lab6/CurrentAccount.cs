using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class CurrentAccount : Account
    {
        public CurrentAccount(Client client, double percent, AccountDecorator D)
        {
            this.client = client;
            if(D != null)
            {
                D.Decorate(this);
            }
            this.percent = percent;
            this.accountType = 1;
        }

        public override bool WithDraw(double money)
        {
            handler.Handle(this);
            if(money > 100 && !verif) { return false; } 
            if (this.money - money < 0) return false;
            this.money -= money;
            return true;
        }

        public override bool TopUp(double money)
        {
            this.money += money;
            return true;
        }

        public override bool Transfer(double money, Account account)
        {
            handler.Handle(this);
            if (account.client != this.client || this.money - money < 0) return false;
            account.money += money;
            this.money -= money;
            return true;
        }
    }
}
