using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab4
{
    class Batch
    {
        public int ShopID;
        public string productName;
        public int count;
        public int price;

        public Batch(int ShopID, string name, int count, int price)
        {
            this.ShopID = ShopID;
            productName = name;
            this.count = count;
            this.price = price;
        }
    }

    class CsvAdapter : DAO
    {
        private List<Shop> shops = new List<Shop>();
        private List<Product> products = new List<Product>();
        private List<Batch> batchs = new List<Batch>(); 
        private string fileShop = "shop.csv";
        private string fileProduct = "product.csv";
        private string fileBatch = "batch.csv";
 
        public CsvAdapter()
        {
            string[] lines = File.ReadAllLines(fileShop);

            foreach(var item in lines)
            {
                string[] array = item.Split(';');
                shops.Add(new Shop(Convert.ToInt32(array[0]), array[1], array[2]));
            }

            lines = File.ReadAllLines(fileProduct);

            foreach(var item in lines)
            {
                products.Add(new Product(item.Split(';')[0]));
            }

            lines = File.ReadAllLines(fileBatch);

            foreach(var item in lines)
            {
                string[] array = item.Split(';');
                batchs.Add(new Batch(Convert.ToInt32(array[0]), array[1], Convert.ToInt32(array[2]), Convert.ToInt32(array[3])));
            }
        }

        private void Save()
        {
            string[] lines = new string[shops.Count];

            for(int i = 0; i < shops.Count; i++)
            {
                lines[i] = $"{shops[i].shopID};{shops[i].shopName};{shops[i].shopAddress}";
            }

            File.WriteAllLines(fileShop, lines);

            lines = new string[products.Count];

            for (int i = 0; i < products.Count; i++)
            {
                lines[i] = $"{products[i].Name}";
            }

            File.WriteAllLines(fileProduct, lines);

            lines = new string[batchs.Count];

            for (int i = 0; i < batchs.Count; i++)
            {
                lines[i] = $"{batchs[i].ShopID};{batchs[i].productName};{batchs[i].count};{batchs[i].price}";
            }

            File.WriteAllLines(fileBatch, lines);
        }

        public override Shop CreateShop(string name, string address)
        {
            if (!shops.Any())
            {
                shops.Add(new Shop(0, name, address));
            }
            else
            {
                shops.Add(new Shop(shops.Last().shopID + 1, name, address));
            }
            this.Save();
            return shops.Last();
        }

        public override Product CreateProduct(string name)
        {
            if( products.FindIndex(x => x.Name == name) < 0)
            {
                products.Add(new Product(name));
            }
            this.Save();
            return products.Find(x => x.Name == name);
        }

        public override void NewBatch(string productName, int shopID, int price, int count)
        {
            if(batchs.FindIndex(x => (x.productName == productName && x.ShopID == shopID)) < 0)
            {
                batchs.Add(new Batch(shopID, productName, count, price));
            }
            else
            {
                Batch batch = batchs.Find(x => (x.productName == productName && x.ShopID == shopID));
                batch.price = price;
                batch.count += count;
            }

            this.Save();
        }

        public override Shop FindCheapProduct(string productName)
        {
            int minSum = -1;
            int shopId = -1;
            foreach(var item in batchs.FindAll(x => x.productName == productName))
            {
                if(item.price < minSum || minSum < 0)
                {
                    minSum = item.price;
                    shopId = item.ShopID;
                }
            }

            return shops.SingleOrDefault(x => x.shopID == shopId);
        }

        public override Dictionary<Product, int> TryBuy(int shopId, int sum)
        {
            Dictionary<Product, int> product = new Dictionary<Product, int>();
            foreach(var item in batchs.FindAll(x => x.ShopID == shopId))
            {
                int count1 = (int)Math.Floor(Convert.ToDouble(sum) / item.price);
                int count = count1 > item.count ? item.count : count1;
                product.Add(new Product(item.productName), count);
            }
            return product;
        }

        public override int Consignment(Dictionary<Product, int> values, int shopId)
        {
            int sum = 0;

            foreach(var item in values)
            {
                if(batchs.FindIndex(x => (x.productName == item.Key.Name && x.ShopID == shopId)) >= 0)
                {
                    Batch batch = batchs.Find(x => (x.productName == item.Key.Name && x.ShopID == shopId));
                    if (batch.count < item.Value) return -1;
                    sum += item.Value * batch.price;
                }
                else
                {
                    return -1;
                }
            }

            foreach(var item in values)
            {
                batchs.Find(x => (x.productName == item.Key.Name && x.ShopID == shopId)).count -= item.Value;
            }

            Console.WriteLine("hi");

            this.Save();

            return sum;
        }

        public override Shop CheapBatch(Dictionary<Product, int> sets)
        {
            int minSum = -1;
            Shop shop = null;

            foreach (var item in shops)
            {
                int sum = CheckSum(sets, item.shopID);
                //Console.WriteLine($"{sum} {item.shopID}");
                if(sum > 0 && (sum < minSum || minSum < 0))
                {
                    minSum = sum;
                    shop = item;
                }
            }
            return shop;
        }

        private int CheckSum(Dictionary<Product, int> values, int shopId)
        {
            int sum = 0;

            foreach (var item in values)
            {
                if (batchs.FindIndex(x => (x.productName == item.Key.Name && x.ShopID == shopId)) >= 0)
                {
                    Batch batch = batchs.Find(x => (x.productName == item.Key.Name && x.ShopID == shopId));
                    //Console.WriteLine($"{batch.ShopID} {batch.productName} {batch.count} {batch.count} {item.Key.Name}");
                    if (batch.count < item.Value) return -1;
                    sum += item.Value * batch.price;
                }
                else
                {
                    return -1;
                }
            }

            return sum;
        }
    }
}
