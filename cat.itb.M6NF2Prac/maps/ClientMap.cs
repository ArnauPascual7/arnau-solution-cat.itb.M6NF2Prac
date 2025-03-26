using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Table("CLIENT");
            Id(x => x.Id);
            Map(x => x.Code).Column("code");
            Map(x => x.Name).Column("name");
            Map(x => x.Credit).Column("credit");
            HasMany(x => x.Orders).AsSet()
                .KeyColumn("client")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join();
        }
    }
}
