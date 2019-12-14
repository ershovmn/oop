using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class CreditAccount : Account
    {
        public CreditAccount(Client client, double percent, double commision, AccountDecorator D)
        {
            this.client = client;
            if(D != null)
            {
                D.Decorate(this);
            }
            this.percent = percent;
            this.commision = commision;
            this.accountType = 0;
        }

        public override bool WithDraw(double money)
        {
            handler.Handle(this);
            if (money > 100 && !verif) return false;
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
            if (account.client != this.client) return false;
            account.money += money;
            this.money -= money;
            return true && verif;
        }
    }
}
