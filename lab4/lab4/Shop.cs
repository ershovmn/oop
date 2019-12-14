using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Shop
    {
        public int shopID;
        public string shopName;
        public string shopAddress;

        public Shop(int shopID, string shopName, string shopAddress)
        {
            this.shopID = shopID;
            this.shopName = shopName;
            this.shopAddress = shopAddress;
        }
    }
}
