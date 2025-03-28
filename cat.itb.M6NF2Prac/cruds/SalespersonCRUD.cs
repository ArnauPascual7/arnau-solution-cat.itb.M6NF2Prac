using cat.itb.M6NF2Prac.connections;
using cat.itb.M6NF2Prac.model;
using NHibernate;
using NHibernate.SqlCommand;
using Npgsql;
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
        public void InsertADO(List<Salesperson> spers)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = $"INSERT INTO SALESPERSON (surname, job, startdate, salary, commission, dep) VALUES (@surname, @job, @startdate, @salary, @commission, @dep)";
                    cmd.CommandText = query;
                    foreach (Salesperson sper in spers)
                    {
                        cmd.Parameters.AddWithValue("surname", sper.Surname ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("job", sper.Job ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("startdate", sper.StartDate);
                        cmd.Parameters.AddWithValue("salary", sper.Salary);
                        cmd.Parameters.AddWithValue("commission", sper.Commission ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("dep", sper.Dep ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();

                        Console.WriteLine($"Venedor {sper.Surname} Insertat");
                        cmd.Parameters.Clear();
                    }
                    Console.WriteLine("Venedors inserits correctament");
                }
            }
        }
        public Salesperson? SelectBySurname(string surname)
        {
            Salesperson? sper = null;
            using (var session = SessionFactoryStoreCloud.Open())
            {
                IQuery query = session.CreateQuery($"select c from Salesperson c where c.Surname like '{surname}'");
                sper = query.UniqueResult<Salesperson>();
            }
            return sper;
        }
    }
}
