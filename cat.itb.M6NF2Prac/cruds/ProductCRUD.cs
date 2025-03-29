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
    public class ProductCRUD
    {
        public Product SelectById(int id)
        {
            Product prod;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                prod = session.Get<Product>(id);
                session.Close();
            }
            return prod;
        }
        public List<Product> SelectAll()
        {
            List<Product> prods;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                prods = (from c in session.Query<Product>() select c).ToList();
                session.Close();
            }
            return prods;
        }
        public void Insert(Product prod)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(prod);
                    tx.Commit();
                    Console.WriteLine($"Producte {prod.Code} Insertat");
                    session.Close();
                }
            }
        }
        public void Update(Product prod)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(prod);
                        tx.Commit();
                        Console.WriteLine($"Producte {prod.Code} Actualitzat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error updating PRODUCT : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public void Delete(Product prod)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(prod);
                        tx.Commit();
                        Console.WriteLine($"Producte  {prod.Code} Eliminat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error deleting PRODUCT : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public Product? SelectByCodeADO(int code)
        {
            Product? prod = null;
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = "SELECT * FROM PRODUCT WHERE code = @code";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("code", code);

                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        prod = new Product()
                        {
                            Id = reader.GetInt32(0),
                            Code = reader.GetInt32(1),
                            Description = reader.GetString(2),
                            CurrentStock = reader.GetInt32(3),
                            MinStock = reader.GetInt32(4),
                            Price = reader.GetFloat(5),
                            Salesperson = new SalespersonCRUD().SelectById(reader.GetInt32(6))
                        };
                        Console.WriteLine($"Producte: {prod.Code} {prod.Description}");
                    }
                }
            }
            return prod;
        }
        public void UpdateADO(Product prod)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = "UPDATE PRODUCT SET price = @price WHERE id = @id";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("price", prod.Price);
                    cmd.Parameters.AddWithValue("id", prod.Id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Preu Actualitzat del Producte {prod.Code} {prod.Description}");
                }
            }
        }
        public IList<object[]> SelectByPriceHigherThan(float price)
        {
            IList<object[]> prods = new List<object[]>();
            using (var session = SessionFactoryStoreCloud.Open())
            {
                var query = session.QueryOver<Product>()
                    .Where(p => p.Price > price)
                    .SelectList(list => list.Select(p => p.Description).Select(p => p.Price));
                prods = query.List<object[]>();
            }
            return prods;
        }
    }
}
