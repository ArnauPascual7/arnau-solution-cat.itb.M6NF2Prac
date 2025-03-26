using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("ORDERPROD");
            Id(x => x.Id);
            Map(x => x.OrderDate).Column("orderdate");
            Map(x => x.Amount).Column("amount");
            Map(x => x.DeliveryDate).Column("deliverydate");
            Map(x => x.Cost).Column("cost");
            References(x => x.Product)
                .Column("product")
                .Not.LazyLoad()
                .Fetch.Join();
            References(x => x.Client)
                .Column("client")
                .Not.LazyLoad()
                .Fetch.Join();
        }
    }
}
