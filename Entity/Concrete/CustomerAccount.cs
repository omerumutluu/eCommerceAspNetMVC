using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class CustomerAccount
    {
        [Key]
        public int CustomerAccountId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public string Address { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Role { get; set; }
        public bool Status { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
