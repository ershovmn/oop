using System;

namespace lab3 {
    class Program {
        static void Main(string[] args) {
            HandlerFileINI handlerFileINI = new HandlerFileINI("test.in");
            //handlerFileINI.PrintAll();
            var res = handlerFileINI.GetValueDouble("ADC_DEV", "BufferLenSeconds");
            Console.WriteLine($"{res}");
        }
    }
}
