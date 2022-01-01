using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    public class CorporateAccountForRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CorporateName { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; } 
    }
}
