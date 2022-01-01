using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public bool Status { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
        public ICollection<CorporateAccount> CorporateAccounts { get; set; }
        public ICollection<AdminAccount> AdminAccounts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
