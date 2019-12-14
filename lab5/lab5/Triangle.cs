using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab5
{
    [Serializable]
    public class Triangle
    {
        public Point A { get; set; } = new Point();
        public Point B { get; set; } = new Point();
        public Point C { get; set; } = new Point();

        public void Serialize(string name)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Triangle));
            FileStream file = new FileStream(name, FileMode.OpenOrCreate);
            xmlSerializer.Serialize(file, this);
            file.Close();
        }

        public static Triangle Deserialize(string name)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Triangle));
            FileStream file = new FileStream(name, FileMode.OpenOrCreate);
            Triangle triangle = (Triangle)xmlSerializer.Deserialize(file);
            file.Close();
            return triangle;
        }

        public void SaveToBinary(string name)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = new FileStream(name, FileMode.OpenOrCreate);
            binaryFormatter.Serialize(file, this);
            file.Close();
        }

        public static Triangle loadFromBinary(string name)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = new FileStream(name, FileMode.OpenOrCreate);
            Triangle triangle = (Triangle)binaryFormatter.Deserialize(file);
            file.Close();
            return triangle;
        }

        public void saveToDB(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string command = $"INSERT INTO Triangle (A_x, A_y, B_x, B_y, C_x, C_y) VALUES ({A.X}, {A.Y}, {B.X}, {B.Y}, {C.X}, {C.Y})";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }


        public static Triangle loadFromDB(string connectionString, int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string command = $"SELECT * FROM Triangle WHERE TriangleID = {id}";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                sqlDataReader.Read();
                var result = new Triangle
                {
                    A = new Point
                    {
                        X = (int)sqlDataReader.GetValue(1),
                        Y = (int)sqlDataReader.GetValue(2)
                    },
                    B = new Point
                    {
                        X = (int)sqlDataReader.GetValue(3),
                        Y = (int)sqlDataReader.GetValue(4)
                    },
                    C = new Point
                    {
                        X = (int)sqlDataReader.GetValue(5),
                        Y = (int)sqlDataReader.GetValue(6)
                    }
                };
                sqlDataReader.Close();
                connection.Close();
                return result;
            }
            return null;
        }

    }
}
