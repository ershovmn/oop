using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new ClientBuilder().AddName("Mikhail", "Ershov").Build();

            Account account = CreatorAccount.Factory(client, 1);

            account.TopUp(100);

            AbstractHandler percentHandler = new PercentHandler(10);
            AbstractHandler percentHandler1 = new CommisionHandler(15);
            AbstractHandler percentHandler2 = new CommisionHandler(15);

            percentHandler.SetNext(percentHandler1).SetNext(percentHandler2);

            percentHandler.Handle(account);

            //percentHandler.Handle(percentHandler1.Handle(null));
        }
    }
}
