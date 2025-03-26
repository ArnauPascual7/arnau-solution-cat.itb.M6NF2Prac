using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Client Client { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual int Amount { get; set; }
        public virtual DateTime DeliveryDate { get; set; }
        public virtual float Cost { get; set; }
    }
}
