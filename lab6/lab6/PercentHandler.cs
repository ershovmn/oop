using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class PercentHandler : AbstractHandler
    {
        double percent;
        Account account;

        public PercentHandler(double percent)
        {
            this.percent = percent;
        }

        public override object Handle(Account request)
        {
            request.money -= request.money * percent / 100;
            return base.Handle(request);
        }
    }
}
