using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_Produkt_Manager.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Produkt_Manager.Models
{
    public class Category
    {
            
        public int  Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Kategorie")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }

        [StringLength(160)]
        [Display(Name = "Kategorie Bild")]
        public string Image { get; set; }

    }
}
