using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity
{
    public class Konu
    {
        public int Id { get; set; }
        public int DersId { get; set; }
        [JsonIgnore] // JSON serileştirmesi sırasında belirli bir özelliğin yoksayılmasını sağlayan bir işaretleyicidir.
                     
        public virtual Ders Ders { get; set; }
        public string KonuAdi { get; set; }
    }
}
