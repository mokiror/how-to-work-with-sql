using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace sql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //файл
            string dbname = "demo.db";
            string connectionString = 
                $"Data Source = {dbname}";
            //запрос создаёт базу
            string init_query =
                "CREATE TABLE IF NOT EXISTS " +
                "Users (Id INTEGER, Name TEXT, Age INTEGER );";

            //заполняем базу
            string insert_query =
                "INSERT INTO Users (Id, Name, Age) " +
                "VALUES (1, 'Rei', 14);";

            //запрос на вывод
            string select_query =
                "SELECT * FROM Users;";

            //соединение 2
            SQLiteConnection connection =
                new SQLiteConnection(connectionString);

            connection.Open();
            Console.WriteLine("Соединение установлено");

            //соединение1
            SQLiteCommand command01 = 
                new SQLiteCommand(init_query, connection);
            command01.ExecuteNonQuery();
            Console.WriteLine($"Выполнили некий запрос {init_query}");

            SQLiteCommand command02 =
               new SQLiteCommand(insert_query, connection);
            command02.ExecuteNonQuery();
            Console.WriteLine($"Выполнили некий запрос {insert_query}");

            SQLiteCommand command03 =
               new SQLiteCommand(select_query, connection);
            SQLiteDataReader reader = command03.ExecuteReader(); //метод вернет совокупность данных для читки
            Console.WriteLine($"Выполнили некий запрос {select_query}");
            Console.WriteLine(
                $"{reader.GetName(0)}\t" +
                $"{reader.GetName(1)}\t" +
                $"{reader.GetName(2)}\t");

            //чтобы вывелось содержимое таблицы
            while (reader.Read())
            {
                Console.WriteLine(
               $"{reader.GetValue(0)}\t" +
               $"{reader.GetValue(1)}\t" +
               $"{reader.GetValue(2)}\t");
            }

            connection.Close();
            Console.WriteLine("Соединение разорванно");

            Console.ReadKey();
        }
    }
}
