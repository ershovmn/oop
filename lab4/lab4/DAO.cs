using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    abstract class DAO
    {
        public abstract Shop CreateShop(string name, string address);
        public abstract Product CreateProduct(string name);
        public abstract void NewBatch(string productName, int shopID, int price, int count);
        public abstract Shop FindCheapProduct(string productName);
        public abstract Dictionary<Product, int> TryBuy(int shopId, int sum);
        public abstract int Consignment(Dictionary<Product, int> values, int shopId);
        public abstract Shop CheapBatch(Dictionary<Product, int> sets);
    }
}
