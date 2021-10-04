using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Produkt_Manager.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Produkt Name")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Artikelnummer")]
        public string ArtNr { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Hersteller")]
        public string Brand { get; set; }

        [StringLength(160)]
        [Display(Name = "Bild")]
        public string Image { get; set; }

    
        public Category Category { get; set; }
    }
}
