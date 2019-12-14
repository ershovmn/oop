using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace lab4
{
    class MsSqlAdapter : DAO
    {
        private SqlConnection connection;

        public MsSqlAdapter()
        {
            connection = new SqlConnection(@"Data Source = LAPTOP-ERSOVMN\SQLEXPRESS02; Initial Catalog = lab4; Integrated Security = True; MultipleActiveResultSets = True");
            connection.Open();
        }

        public override Shop CreateShop(string name, string address)
        {
            string command = $"insert into Shop (ShopName, ShopAddress) values (N'{name}', N'{address}')";
            SqlCommand sqlCommand = new SqlCommand(command, this.connection);
            sqlCommand.ExecuteNonQuery();
            command = $"select top 1 * from Shop order by ShopID desc";
            sqlCommand = new SqlCommand(command, this.connection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            return new Shop((int)sqlDataReader.GetValue(0), name, address);
        }

        public override Product CreateProduct(string name)
        {
            string command = $"insert into Product (ProductName) values (N'{name}')";
            SqlCommand sqlCommand = new SqlCommand(command, this.connection);
            sqlCommand.ExecuteNonQuery();
            return new Product(name);
        }

        public override void NewBatch(string productName, int shopID, int price, int count)
        {
            string command1 = $"select * from Batch where ShopID = {shopID} and ProductName = N'{productName}'";
            SqlCommand sqlCommand1 = new SqlCommand(command1, this.connection);
            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();

            bool isExsist = sqlDataReader.HasRows;

            //sqlDataReader.Read();
            //Console.WriteLine($"{sqlDataReader.GetValue(0)} {sqlDataReader.GetValue(1)} {sqlDataReader.GetValue(2)} {sqlDataReader.GetValue(3)}");
            //Console.ReadKey();

            if(isExsist)
            {
                sqlDataReader.Read();
                int oldCount = sqlDataReader.GetInt32(3);
                count += oldCount;
                string command = $"update Batch set Count = {count}, Price = {price} where ShopID = {shopID} and ProductName = N'{productName}'";
                SqlCommand sqlCommand = new SqlCommand(command, this.connection);
                sqlCommand.ExecuteNonQuery();
            }
            else
            {
                string command = $"insert into Batch (ShopID, ProductName, Count, Price) values ({shopID}, N'{productName}', {count}, {price})";
                SqlCommand sqlCommand = new SqlCommand(command, this.connection);
                sqlCommand.ExecuteNonQuery();
            }
            return;
        }

        public override Shop FindCheapProduct(string productName)
        {
            string command = $"select TOP 1 ShopID from Batch where ProductName = N'{productName}' order by price";
            SqlCommand sqlCommand = new SqlCommand(command, this.connection);
            int? shopID = (int?)sqlCommand.ExecuteScalar();
            return shopID.HasValue ? GetShop(shopID.Value) : null;
        }

        public override Dictionary<Product, int> TryBuy(int shopId, int sum)
        {
            string command = $"select ProductName, Price, Count FROM Batch where ShopID = {shopId}";
            SqlCommand sqlCommand = new SqlCommand(command, this.connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Dictionary<Product, int> product = new Dictionary<Product, int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int count = (int)(Math.Floor(sum / Convert.ToDouble(reader.GetValue(1))));
                    int productCount = (int)reader.GetValue(2);
                    int Count = count > productCount ? productCount : count;
                    if (Count == 0)
                    {
                        continue;
                    }
                    product.Add(new Product(reader.GetString(0)), Count);
                    //command = $"update Batch set Count = {productCount - Count} where ShopID = {shopId} and ProductName = N'{reader.GetString(0)}'";
                    //sqlCommand = new SqlCommand(command, this.connection);
                    //sqlCommand.ExecuteNonQuery();
                }
            }
            return product;
        }

        public override int Consignment(Dictionary<Product, int> values, int shopId)
        {
            int countProducts = 0;
            int sum = 0;
            string command = $"select ProductName, Price, Count FROM Batch where ShopID = {shopId}";
            SqlCommand sqlCommand = new SqlCommand(command, this.connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string productName = reader.GetString(0);
                    int count = reader.GetInt32(2);
                    int price = reader.GetInt32(1);
                    foreach (var item in values)
                    {
                        if (item.Key.Name == productName)
                        {
                            if (count < item.Value) return -1;
                            countProducts += 1;
                            sum += item.Value * price;
                        }
                    }
                }

                if(countProducts == values.Count)
                {
                    foreach(var item in values)
                    {
                        string command1 = $"update Batch set Count=Count-{item.Value} where ShopID = {shopId} and ProductName = N'{item.Key.Name}'";
                        SqlCommand sqlCommand1 = new SqlCommand(command1, this.connection);
                        sqlCommand1.ExecuteNonQuery();
                    }
                    return sum;
                }
            }
            return -1;
        }

        public override Shop CheapBatch(Dictionary<Product, int> sets)
        {
            string commandShop = $"select ShopID from Shop";
            SqlCommand sqlCmdShop = new SqlCommand(commandShop, connection);
            SqlDataReader sqlDRShop = sqlCmdShop.ExecuteReader();
            if (!sqlDRShop.HasRows) return null;

            int minSum = -1;
            int shopId = -1;

            while (sqlDRShop.Read())
            {
                int sum = CheckSum(sets, sqlDRShop.GetInt32(0));
                //Console.WriteLine($"{sum} {sqlDRShop.GetInt32(0)}");
                if (sum > 0 && (sum < minSum || minSum < 0))
                {
                    minSum = sum;
                    shopId = sqlDRShop.GetInt32(0);
                }
            }

            if(minSum > 0)
            {
                return GetShop(shopId);
            }

            return null;
        }

        private Shop GetShop(int shopID)
        {
            string command = $"select * from Shop where ShopID = {shopID}";
            SqlCommand sqlCommand = new SqlCommand(command, this.connection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                sqlDataReader.Read();
                return new Shop(sqlDataReader.GetInt32(0), sqlDataReader.GetString(1), sqlDataReader.GetString(2));
            }
            return null;
        }

        private int CheckSum(Dictionary<Product, int> values, int shopId)
        {
            int countProducts = 0;
            int sum = 0;
            string command = $"select ProductName, Price, Count FROM Batch where ShopID = {shopId}";
            SqlCommand sqlCommand = new SqlCommand(command, this.connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string productName = reader.GetString(0);
                    int count = reader.GetInt32(2);
                    int price = reader.GetInt32(1);
                    foreach (var item in values)
                    {
                        if (item.Key.Name == productName)
                        {
                            if (count < item.Value) return -1;
                            countProducts += 1;
                            sum += item.Value * price;
                        }
                    }
                }

                if (countProducts == values.Count)
                {
                    return sum;
                }
            }
            return -1;
        }
    }
}
