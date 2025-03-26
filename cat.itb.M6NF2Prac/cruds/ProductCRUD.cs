using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
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
    }
}
