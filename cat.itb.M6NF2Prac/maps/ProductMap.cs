using cat.itb.M6NF2Prac.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.maps
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("PRODUCT");
            Id(x => x.Id);
            Map(x => x.Code).Column("code");
            Map(x => x.Description).Column("description");
            Map(x => x.CurrentStock).Column("currentstock");
            Map(x => x.MinStock).Column("minstock");
            Map(x => x.Price).Column("price");
            References(x => x.Salesperson)
                .Column("salesp")
                .Not.LazyLoad()
                .Fetch.Join();
            HasMany(x => x.Orders).AsSet()
                .KeyColumn("product")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join();
            HasOne(x => x.Provider)
                .PropertyRef(nameof(Provider.Product))
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join();
        }
    }
}
