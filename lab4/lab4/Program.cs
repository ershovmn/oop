using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        private const string Name = "super cheap";

        static void Main(string[] args)
        {
            string InputType = Properties.Settings.Default.InputType;

            DAO adapter = null;

            //InputType = "CSV";
            if (InputType == "DB")
            {
                adapter = new MsSqlAdapter();
            }
            if (InputType == "CSV")
            {
                adapter = new CsvAdapter();
            }

            var shop = adapter.CreateShop("QQQ", "hello");
            var shopId = shop.shopID;

            Console.WriteLine("Creeate shop");

            adapter.CreateProduct(Name);

            Console.WriteLine("Create product");
            
            adapter.NewBatch(Name, shopId, 1, 12);

            Console.WriteLine("Create batch");
            
            var resTryBuy = adapter.TryBuy(0, 14);
            
            Dictionary<Product, int> test = new Dictionary<Product, int>();
            test.Add(new Product(Name), 3);
            Console.WriteLine($"Min price {adapter.Consignment(test, shopId)}");

            Shop shop1 = adapter.CheapBatch(test);

            Console.WriteLine($"ShopId cheap batch {shop1.shopID}");

            Console.WriteLine($"ShopID cheap product {adapter.FindCheapProduct(Name)?.shopID.ToString() ?? "Item not found"}");

            Console.ReadKey();
        }
    }
}
