﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;
namespace BasketTeam
{
    internal class Program
    {

        public static Connect conn = new Connect();

        public static void HozzaAdas()
        {
            Console.Write("Add meg a játékos nevét: ");
            string name = Console.ReadLine();

            Console.Write("Add meg a játékos magasságát (centiméter): ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Add meg a játékos súlyát (kilogramm): ");
            int weight = int.Parse(Console.ReadLine());

            conn.Connection.Open();

            string sql = "INSERT INTO Player (Name, Height, Weight, CreatedTime) VALUES (@Name, @Height, @Weight, NOW())";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Height", height);
            cmd.Parameters.AddWithValue("@Weight", weight);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            Console.WriteLine("Az új játékos sikeresen hozzá lett adva az adatbázishoz!");
        }

        public static void Torles()
        {
            Console.Write("Add meg a kívánt játékos azonosítóját akit törölni akar (Id): ");
            int id = int.Parse(Console.ReadLine());

            conn.Connection.Open();

            string sql = "DELETE FROM Player WHERE Id = @Id";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            Console.WriteLine("A játékos törölve az adatbázisból.");
        }

        public static void KiIras()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM Player";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\nJátékosok listája:");
            while (dr.Read())
            {
                Console.WriteLine($"Id: {dr.GetInt32(0)}, Név: {dr.GetString(1)}, Magasság: {dr.GetInt32(2)} cm, Súly: {dr.GetInt32(3)} kg, Hozzáadva: {dr.GetDateTime(4)}");
            }

            dr.Close();
            conn.Connection.Close();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nKosárlabda Csapat Kezelő");
                Console.WriteLine("1. Új játékos hozzáadása");
                Console.WriteLine("2. Játékos törlése");
                Console.WriteLine("3. Játékosok listázása");
                Console.WriteLine("4. Kilépés");
                Console.Write("Válassz egy opciót: ");

                string valasztas = Console.ReadLine();

                switch (valasztas)
                {
                    case "1":
                        HozzaAdas();
                        break;
                    case "2":
                        Torles();
                        break;
                    case "3":
                        KiIras();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        break;
                }
            }
        }
    }
}