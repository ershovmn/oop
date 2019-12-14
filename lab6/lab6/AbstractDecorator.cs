using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class AccountDecorator
    {
        public void Decorate(Account account)
        {
            if(account.client.PassportNumber != 0 || account.client.Address != "")
            {
                account.verif = true;
            }
        }
    }
}
