using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerAccountId { get; set; }
        public virtual CustomerAccount CustomerAccount { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Count { get; set; }
        [StringLength(30)]
        public string OrderStatus { get; set; }
    }
}
