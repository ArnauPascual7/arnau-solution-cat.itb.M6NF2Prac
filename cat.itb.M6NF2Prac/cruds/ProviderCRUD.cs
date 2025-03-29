using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using NHibernate.Criterion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class ProviderCRUD
    {
        public Provider SelectById(int id)
        {
            Provider prov;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                prov = session.Get<Provider>(id);
                session.Close();
            }
            return prov;
        }
        public List<Provider> SelectAll()
        {
            List<Provider> provs;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                provs = (from c in session.Query<Provider>() select c).ToList();
                session.Close();
            }
            return provs;
        }
        public void Insert(Provider prov)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(prov);
                    tx.Commit();
                    Console.WriteLine($"Proveïdor {prov.Name} Insertat");
                    session.Close();
                }
            }
        }
        public void Update(Provider prov)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(prov);
                        tx.Commit();
                        Console.WriteLine($"Proveïdor {prov.Name} Actualitzat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error updating PROVIDER : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public void Delete(Provider prov)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(prov);
                        tx.Commit();
                        Console.WriteLine($"Proveïdor  {prov.Name} Eliminat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error deleting PROVIDER : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public IList<Provider> SelectCreditLowerThanADO(float credit)
        {
            List<Provider> provs = new List<Provider>();
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = $"SELECT * FROM PROVIDER WHERE credit < {credit}";
                    cmd.CommandText = query;

                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        provs.Add(new Provider()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            City = reader.GetString(3),
                            StCode = reader.GetString(4),
                            ZipCode = reader.GetString(5),
                            Area = reader.GetInt32(6),
                            Phone = reader.GetString(7),
                            Product = new ProductCRUD().SelectById(reader.GetInt32(8)),
                            Amount = reader.GetInt32(9),
                            Credit = reader.GetInt32(10),
                            Remark = reader.GetString(11)
                        });
                    }
                }
            }
            return provs;
        }
        public Provider? SelectLowestAmount()
        {
            Provider? prov = null;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                QueryOver<Provider> lowestAmount = QueryOver.Of<Provider>().SelectList(p => p.SelectMin(a => a.Amount));
                var query = session.QueryOver<Provider>().WithSubquery.WhereProperty(p => p.Amount).Eq(lowestAmount).SingleOrDefault();
                prov = query;
            }
            return prov;
        }
        public IList<Provider> SelectByCity(string city)
        {
            IList<Provider> provs = new List<Provider>();
            using (var session = SessionFactoryStoreCloud.Open())
            {
                var query = session.CreateQuery($"select c from Provider c where c.City like '{city}'");
                provs = query.List<Provider>();
            }
            return provs;
        }
    }
}
