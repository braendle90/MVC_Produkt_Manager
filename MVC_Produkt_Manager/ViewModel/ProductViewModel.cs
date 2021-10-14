using MVC_Produkt_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Produkt_Manager.ViewModel
{
    public class ProductViewModel
    {

        public IEnumerable<Product> Products { get; set; }

        public string Search { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public Pagination Pagination { get; set; }


    }
}
