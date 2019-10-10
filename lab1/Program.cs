using System;
using System.Collections.Generic;

namespace sharp
{
    class Program {
        static void Main(string[] args) {
            RaionalFraction a1 = new RaionalFraction(1, 2);
            RaionalFraction a2 = new RaionalFraction(1, 3);
            RaionalFraction a3 = new RaionalFraction(1, 4);
            RaionalFraction a4 = new RaionalFraction(1, 5);
            RaionalFraction a5 = new RaionalFraction(1, 6);
            RaionalFraction a6 = new RaionalFraction(1, 7);
            SetFractions sf = new SetFractions(new[] {a1, a2, a3, a4, a5, a6});
            sf.Max().Print();
            sf.Min().Print();
            Console.WriteLine($"{sf.CountMoreThanFraction(a4)}");
            Console.WriteLine($"{sf.CountMoreThanFraction(a4)}");
        }
    }
}
