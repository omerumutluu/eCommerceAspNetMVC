using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int CorporateAccountId { get; set; }
        public virtual CorporateAccount CorporateAccount { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        [StringLength(150)]
        public string ProductThumbnail { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
