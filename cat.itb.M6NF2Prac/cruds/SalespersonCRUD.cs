using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.cruds
{
    public class SalespersonCRUD
    {
        public Salesperson SelectById(int id)
        {
            Salesperson sper;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                sper = session.Get<Salesperson>(id);
                session.Close();
            }
            return sper;
        }
        public List<Salesperson> SelectAll()
        {
            List<Salesperson> spers;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                spers = (from c in session.Query<Salesperson>() select c).ToList();
                session.Close();
            }
            return spers;
        }
        public void Insert(Salesperson sper)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(sper);
                    tx.Commit();
                    Console.WriteLine($"Persona de Vendes {sper.Surname} Insertat");
                    session.Close();
                }
            }
        }
        public void Update(Salesperson sper)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(sper);
                        tx.Commit();
                        Console.WriteLine($"Persona de Vendes  {sper.Surname} Actualitzat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error updating SALESPERSON : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
        public void Delete(Salesperson sper)
        {
            using (var session = SessionFactoryStoreCloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(sper);
                        tx.Commit();
                        Console.WriteLine($"Persona de Vendes  {sper.Surname} Eliminat");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        throw new Exception("Error deleting SALESPERSON : " + ex.Message);
                    }
                }
                session.Close();
            }
        }
    }
}
