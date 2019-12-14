using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class CommisionHandler : AbstractHandler
    {
        double commision;
        Account account;

        public CommisionHandler(double commision)
        {
            this.commision = commision;
        }

        public override object Handle(Account request)
        {
            request.money -= commision;
            return base.Handle(request);
        }
    }
}
