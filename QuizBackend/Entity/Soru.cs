using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entity
{
    public class Soru
    {
        public int Id { get; set; }
        public int KonuId { get; set; }
        public string SoruText { get; set; }
        public string CevapA { get; set; }
        public string CevapB { get; set; }
        public string CevapC { get; set; }
        public string CevapD { get; set; }
        public string CevapE { get; set; }
        public string DogruCevap { get; set; }
        [JsonIgnore]
        public virtual Konu Konu { get; set; } // Virtual eklenmesinin sebebi daha az yer kaplaması için
    }
}
