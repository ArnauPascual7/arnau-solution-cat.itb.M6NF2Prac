using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using NHibernate.Criterion;
using Order = cat.itb.M6NF2Prac.model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class OrderCRUD
    {
        public Order SelectById(int id)
        {
            Order ord;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                ord = session.Get<Order>(id);
                session.Close();
            }
            return ord;
        }
        public List<Order> SelectAll()
        {
            List<Order> ords;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                ords = (from c in session.Query<Order>() select c).ToList();
                session.Close();
            }
            return ords;
        }
        public void Insert(Order ord)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(ord);
                    tx.Commit();
                    Console.WriteLine($"Comanda: (Producte: {ord.Product.Id}, Client: {ord.Client.Id}, Data: {ord.OrderDate}) Insertat");
                    session.Close();
                }
            }
        }
        public void Update(Order ord)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(ord);
                        tx.Commit();
                        Console.WriteLine($"Comanda: (Producte: {ord.Product.Id}, Client: {ord.Client.Id}, Data: {ord.OrderDate})  Actualitzat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error updating ORDEPROD : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public void Delete(Order ord)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(ord);
                        tx.Commit();
                        Console.WriteLine($"Comanda: (Producte: {ord.Product.Id}, Client: {ord.Client.Id}, Data: {ord.OrderDate})  Eliminat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error deleting ORDERPROD : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public IList<Order> SelectByCostHigherThan(float cost, int amount)
        {
            IList<Order> ords = new List<Order>();
            using (var session = SessionFactoryStoreCloud.Open())
            {
                var query = session.CreateCriteria<Order>().Add(Restrictions.Gt("Cost", cost)).Add(Restrictions.Eq("Amount", amount));
                ords = query.List<Order>();
            }
            return ords;
        }
    }
}
