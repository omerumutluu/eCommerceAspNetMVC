using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [StringLength(60)]
        public string BrandName { get; set; }
        public List<Product> products { get; set; }
        public bool Status { get; set; }
    }
}
