using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.model
{
    public class Order
    {
        public int Id { get; set; }
        // TODO: Product & Client
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public float Cost { get; set; }
    }
}
