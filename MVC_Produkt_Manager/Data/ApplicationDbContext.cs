using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Produkt_Manager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MVC_Produkt_Manager.ViewModel;

namespace MVC_Produkt_Manager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       public DbSet<Product> Products { get; set; }
       public DbSet<Category> Categorie { get; set; }



    
    }
}
