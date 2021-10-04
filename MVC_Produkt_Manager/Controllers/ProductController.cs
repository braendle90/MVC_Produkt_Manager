using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Produkt_Manager.Data;
using MVC_Produkt_Manager.Models;
using MVC_Produkt_Manager.ViewModel;

namespace MVC_Produkt_Manager.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(int index)
        {

            var products = await _context.Products
                .Include(x => x.Category)
                .ToListAsync();

            var fiveProducts = products.GetRange(0,5);

            return View(fiveProducts);
        }

        [HttpGet]
        [ActionName("IndexRange")]
        public async Task<IActionResult> IndexRange(int item, int index )
        {
            

            int indexFrom = 0;
            int indexTo = index * 25;


            var products = await _context.Products
              .Include(x => x.Category)
              .ToListAsync();

            var fiveProducts = products.GetRange(indexFrom, indexTo);

            return View("Index",fiveProducts);

        }
        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {

            var category = await _context.Categorie.ToListAsync();
            var viewModel = new ProductCategoryViewModel();
            viewModel.CategoryList = new List<Category>();
            foreach (var item in category)
            {
                viewModel.CategoryList.Add(item);
            }

            return View(viewModel);

        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,CategoryName,ProductName,ProductDescription,ArtNr,Brand,Image")] ProductCategoryViewModel productViewModel,int idCat)
        {
            if (ModelState.IsValid)
            {


                Category category = await _context.Categorie.FindAsync(productViewModel.CategoryId);

              var product = new Product
                {
                    Id = productViewModel.Id,
                    ProductName = productViewModel.ProductName,
                    Description = productViewModel.ProductDescription,
                    ArtNr = productViewModel.ArtNr,
                    Brand = productViewModel.Brand,
                    Image = productViewModel.ProductImage,
                    Category = category                   
                };



                await _context.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Products.FindAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,ProductName,Description,ArtNr,Brand,Image")] ProductCategoryViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewModelExists(productViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productViewModel = await _context.Products.FindAsync(id);
            _context.Products.Remove(productViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }


        [HttpPost]
        public async Task<IActionResult> fillModal(int id)
        {
            Product modal = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);


            return Ok(modal);
        }
    }
}
