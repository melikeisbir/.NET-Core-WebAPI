using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class KullaniciRole: IdentityUserRole<string>
    {
        public int Id { get; set; }
    }

}
