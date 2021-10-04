using MVC_Produkt_Manager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Produkt_Manager.ViewModel
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Product Beschreibung")]
        public string ProductDescription { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Artikelnummer")]
        public string ArtNr { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Hersteller")]
        public string Brand { get; set; }
        [StringLength(160)]
        [Display(Name = "Productbild")]
        public string ProductImage { get; set; }
  
        [StringLength(60)]
        [Display(Name = "Kategorie Name")]
        public string CategoryName { get; set; }

        [StringLength(200)]
        [Display(Name = "Kategorie Beschreibung")]
        public string CategoryDescription { get; set; }
        [StringLength(160)]
        [Display(Name = "Kategoriebild")]
        public string CategoryImage { get; set; }

        public Category Category { get; set; }


        public List<Category> CategoryList { get; set; } = new List<Category>();



    }
}
