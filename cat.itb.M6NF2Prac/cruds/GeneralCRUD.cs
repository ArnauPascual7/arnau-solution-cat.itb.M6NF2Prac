using cat.itb.M6NF2Prac.connections;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class GeneralCRUD
    {
        public void DropTables(List<String> tables)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn };

                foreach (string table in tables)
                {
                    cmd.CommandText = $"DROP TABLE IF EXISTS {table} CASCADE";
                    cmd.ExecuteNonQuery();

                    Console.WriteLine($"Taula {table} Eliminada");
                }
            }
        }
        public void RunScriptSQL()
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                string script = File.ReadAllText(@"..\..\..\files\store.sql");
                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn, CommandText = script
                };
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Script Executat");
        }
        public void RestoreDb()
        {
            List<string> tables = ["CLIENT", "ORDERPROD", "PRODUCT", "PROVIDER", "SALESPERSON"];
            DropTables(tables);
            RunScriptSQL();
        }
    }
}
