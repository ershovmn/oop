using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle() {
                A = new Point(0, 0),
                B = new Point(1, 2),
                C = new Point(10, 0)
            };

            Console.WriteLine($"{triangle.B.X} {triangle.B.Y}");
            triangle.saveToDB(@"Data Source = LAPTOP-ERSOVMN\SQLEXPRESS02; Initial Catalog = tehnarenok; Integrated Security = True; MultipleActiveResultSets = True");
            triangle.Serialize("input.xml");
            triangle.SaveToBinary("test");
            Triangle tr = Triangle.Deserialize("input.xml");
            Triangle trFromDb = Triangle.loadFromDB(@"Data Source = LAPTOP-ERSOVMN\SQLEXPRESS02; Initial Catalog = tehnarenok; Integrated Security = True; MultipleActiveResultSets = True", 1);
            Triangle trFromBinary = Triangle.loadFromBinary("test");
            triangle.Serialize("i.xml");
            Console.WriteLine($"{tr.B.X} {tr.B.Y}");
            Console.WriteLine($"{triangle.B.X} {triangle.B.Y}");
            Console.ReadKey();
        }
    }
}
