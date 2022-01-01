using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class CorporateAccount
    {
        [Key]
        public int CorporateAccountId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [StringLength(50)]
        public string CorporateName { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string WebSite { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
