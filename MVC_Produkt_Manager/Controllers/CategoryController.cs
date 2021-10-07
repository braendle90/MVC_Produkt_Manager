using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Produkt_Manager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_Produkt_Manager.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics;

namespace MVC_Produkt_Manager.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Category = await _context.Categorie.ToListAsync();

            return View(Category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteCategory = await _context.Categorie.FindAsync(id);

            if (deleteCategory == null)
            {
                return NotFound();
            }

            return View(deleteCategory);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var productListToRemove =   _context.Products.Where(x => x.Category.Id == id).ToList();
            var categorieToRemve = await _context.Categorie.FindAsync(id);

            foreach (var product in productListToRemove)
            {
                product.Category = null;
                //_context.Update(product);
            }


           _context.Products.UpdateRange(productListToRemove);
            _context.SaveChanges();


            _context.Categorie.Remove(categorieToRemve);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Category = await _context.Categorie.FirstOrDefaultAsync(x => x.Id == id);

            if (Category == null)
            {
                return NotFound();
            }


            return View(Category);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Category = await _context.Categorie.FindAsync(id);

            if (Category == null)
            {
                return NotFound();
            }


            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(IFormFile image,int id,[Bind("Id,CategoryName,Description,Image")] Category category)
        {

       


            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    //Set Key Name
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                    //Get url To Save
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageName);

                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }





                    category.Image = ImageName;
                }

                _context.Update(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("index");

            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Description,Image")] Category category)
        {

            if (ModelState.IsValid)
            {
      
                _context.Add(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("index");

            }
           
            
            

            return View(category);
        }

        public async Task<IActionResult> ListOfProducts (int id, int pg=1)
        {
            const int pageSize = 6;
            if (pg <1)
            {
                pg = 1;
            }

            var products = await _context.Products.Where(x => x.Category.Id == id).ToListAsync();
            int resCount = products.Count();
            var pagination = new Pagination(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = products.Skip(recSkip).Take(pagination.PageSize).ToList();
            this.ViewBag.Pagination = pagination;





            return View(model);

        } 

    }
}
