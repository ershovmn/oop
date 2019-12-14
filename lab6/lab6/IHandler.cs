using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(Account request);
    }
}
