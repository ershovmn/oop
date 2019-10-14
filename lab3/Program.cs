using System;

namespace lab3 {
    class Program {
        static void Main(string[] args) {
            HandlerFileINI handlerFileINI = new HandlerFileINI("test.ini");
            //handlerFileINI.PrintAll();
            var res = handlerFileINI.GetValueDouble("COMMON", "LogNCMD");
            Console.WriteLine($"{res}");
        }
    }
}
