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
        // Pràctica
        /// <summary>
        /// Pràctica Exercici 5
        /// </summary>
        /// <param name="spers"></param>
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
        // Pràctica
        /// <summary>
        /// Pràctica Exercici 7
        /// </summary>
        /// <param name="surname"></param>
        /// <returns></returns>
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
        // Examen
        public Salesperson SelectByIdADO(int id)
        {
            Salesperson sper = new Salesperson();
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = "SELECT * FROM SALESPERSON WHERE id = @id";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("id", id);

                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sper = new Salesperson()
                        {
                            Id = reader.GetInt32(0),
                            Surname = reader.GetString(1),
                            Job = reader.GetString(2),
                            StartDate = reader.GetDateTime(3),
                            Salary = reader.GetFloat(4),
                            Commission = reader.GetFloat(5),
                            Dep = reader.GetString(6)
                        };
                    }
                }
            }
            return sper;
        }
        // Examen
        /// <summary>
        /// Examen Exercici 3
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public IList<Salesperson> SelectByJobADO(string job)
        {
            IList<Salesperson> spers = new List<Salesperson>();
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = "SELECT * FROM SALESPERSON WHERE job = @job";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("job", job);

                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        spers.Add(new Salesperson()
                        {
                            Id = reader.GetInt32(0),
                            Surname = reader.GetString(1),
                            Job = reader.GetString(2),
                            StartDate = reader.GetDateTime(3),
                            Salary = reader.GetFloat(4),
                            Commission = reader.IsDBNull(5) ? null : reader.GetFloat(5),
                            Dep = reader.GetString(6)
                        });
                    }
                }
            }
            return spers;
        }
        // Examen
        /// <summary>
        /// Examen Exercici 3
        /// </summary>
        /// <param name="sper"></param>
        public void UpdateADO(Salesperson sper)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn })
                {
                    string query = "UPDATE SALESPERSON SET salary = @salary WHERE id = @id";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("salary", sper.Salary);
                    cmd.Parameters.AddWithValue("id", sper.Id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Salary Actualitzat del Venedor {sper.Surname}");

                    cmd.Parameters.Clear();

                    query = "UPDATE SALESPERSON SET commission = @comm WHERE id = @id";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("comm", sper.Commission ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("id", sper.Id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Comissió Actualitzada del Venedor {sper.Surname}");
                }
            }
        }
        // Examen
        /// <summary>
        /// Examen Exercici 6
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public IList<Salesperson> SelectByJob(string job)
        {
            IList<Salesperson> spers = new List<Salesperson>();
            using (var session = SessionFactoryStoreCloud.Open())
            {
                IQuery query = session.CreateQuery($"select c from Salesperson c where c.Job like '{job}'");
                spers = query.List<Salesperson>();
            }
            return spers;
        }
        // Examen
        /// <summary>
        /// Examen Exercici 8
        /// </summary>
        /// <param name="salary"></param>
        /// <param name="dep"></param>
        /// <returns></returns>
        public IList<Salesperson> SelectByJobSalaryLowerThan(float salary, string dep)
        {
            IList<Salesperson> spers = new List<Salesperson>();
            using (var session = SessionFactoryStoreCloud.Open())
            {
                var query = session.Query<Salesperson>()
                    .Where(sp => sp.Salary < salary && sp.Dep == dep);
                spers = query.ToList<Salesperson>();
            }
            return spers;
        }
    }
}
