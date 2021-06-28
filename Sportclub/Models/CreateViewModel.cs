using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sportclub.Models
{
    public class CreateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Voornaam { get; set; }
        [Required]
        [MaxLength(50)]
        public string Achternaam { get; set; }
        public string Geslacht { get; set; }
        [Required]
        public DateTime Geboortedatum { get; set; }
        [Required]
        public int LengteCm { get; set; }
        [Required]
        public double GewichtKg { get; set; }
        public IFormFile Photo { get; set; }
        public IEnumerable<SelectListItem> Geslachten { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem("Vrouw","Vrouw"),
            new SelectListItem("Man","Man"),
            new SelectListItem("Anders","Anders"),
        };
    }
}
