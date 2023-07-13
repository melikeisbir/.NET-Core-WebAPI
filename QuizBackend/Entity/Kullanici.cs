using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public Role Role { get; set; }
    }
    public enum Role
    {
        Admin,   //  Role (Rol) enumunu temsil ediyor. Bu enum, iki farklı role sahip olabileceğiniz bir sistemde
                 //  rol tanımlarını temsil ediyor: Admin ve User.
        User
    }
}
