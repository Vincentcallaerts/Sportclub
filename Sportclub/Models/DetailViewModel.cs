using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportclub.Models
{
    public class DetailViewModel
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Geslacht { get; set; }
        public int LengteCm { get; set; }
        public double GewichtKg { get; set; }
        public string ImgPath { get; set; }
    }
}
