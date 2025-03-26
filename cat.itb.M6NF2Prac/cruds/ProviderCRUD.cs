using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
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
                        Console.WriteLine($"Proveïdor  {prov.Name} Actualitzat");
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
    }
}
