using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public static class CreatorAccount
    {
        public static Account Factory(Client client, double? percent = null, double? commision = null, DateTime? endDate = null, double? limit = null)
        {
            if(!commision.HasValue && endDate.HasValue && !limit.HasValue)
            {
                return new DepositAccount(client, endDate.Value, percent.Value, new AccountDecorator());
            }
            
            if(limit.HasValue && !percent.HasValue && !endDate.HasValue && commision.HasValue)
            {
                return new CreditAccount(client, percent.Value, commision.Value, new AccountDecorator());
            }

            if(percent.HasValue && !limit.HasValue && !commision.HasValue && !endDate.HasValue)
            {
                return new CurrentAccount(client, percent.Value, new AccountDecorator());
            }

            throw new ArgumentException("");
        }
    }
}
