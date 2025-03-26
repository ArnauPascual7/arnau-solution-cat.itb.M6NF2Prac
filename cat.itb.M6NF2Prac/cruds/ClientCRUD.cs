using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class ClientCRUD
    {
        public Client SelectById(int id)
        {
            Client clie;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                clie = session.Get<Client>(id);
                session.Close();
            }
            return clie;
        }
        public List<Client> SelectAll()
        {
            List<Client> clies;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                clies = (from c in session.Query<Client>() select c).ToList();
                session.Close();
            }
            return clies;
        }
        public void Insert(Client clie)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(clie);
                    tx.Commit();
                    Console.WriteLine($"Client {clie.Code} {clie.Name} Insertat");
                    session.Close();
                }
            }
        }
        public void Update(Client clie)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(clie);
                        tx.Commit();
                        Console.WriteLine($"Client {clie.Code} {clie.Name} Actualitzat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error updating CLIENT : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public void Delete(Client emp)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(emp);
                        tx.Commit();
                        Console.WriteLine($"Client {emp.Code} {emp.Name} Eliminat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error deleting CLIENT : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public void InsertADO(Client clie)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = $"INSERT INTO CLIENT (code, name, credit) VALUES ({clie.Code}, '{clie.Name}', {clie.Credit})";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                    Console.WriteLine($"Client {clie.Code} {clie.Name} Insertat");
                }
            }
        }
        public Client? SelectByNameADO(string name)
        {
            Client? clie = null;
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = "SELECT * FROM CLIENT WHERE name ILIKE @name";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("name", name);
                    
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        clie = new Client()
                        {
                            Id = reader.GetInt32(0),
                            Code = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Credit = reader.GetFloat(3),
                        };
                    }
                }
            }
            return clie;
        }
    }
}
