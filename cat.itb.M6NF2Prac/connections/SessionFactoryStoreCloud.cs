using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.connections
{
    public class SessionFactoryStoreCloud
    {
        private static string ConnectionString =
            "Server=postgresql-arnaupascual.alwaysdata.net;" +
            "Port=5432;" +
            "Database=arnaupascual_store;" +
            "User Id=arnaupascual;" +
            "Password=7e8@itb;";
        private static ISessionFactory? session;

        public static ISessionFactory CreateSession()
        {
            if (session != null)
                return session;

            IPersistenceConfigurer configDB = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);

            var configMap = Fluently
                .Configure()
                .Database(configDB)
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<Program>());

            session = configMap.BuildSessionFactory();

            return session;
        }

        public static ISession Open()
        {
            return CreateSession().OpenSession();
        }
    }
}
