using MVC_Produkt_Manager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Produkt_Manager.ViewModel
{
    public class ProductCategoryPaginationViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Produkt")]
        public Product Products { get; set; }

        [Display(Name = "Category")]
        public Category Categorys { get; set; }

        public int ActualPage { get; set; }
        public int itemsPerPage { get; set; }

    }
}
