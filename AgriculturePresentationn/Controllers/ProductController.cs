using DataAccessLayer.Contexts;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriculturePresentationn.Controllers
{
    public class ProductController : Controller
    {
        private readonly AgricultureContext _context;

        public ProductController(AgricultureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .OrderByDescending(x => x.ProductId)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product p)
        {
            if (string.IsNullOrWhiteSpace(p.Name))
            {
                ModelState.AddModelError("Name", "Ürün adı boş geçilemez.");
            }

            if (p.Value < 0)
            {
                ModelState.AddModelError("Value", "Değer negatif olamaz.");
            }

            if (!ModelState.IsValid)
                return View(p);

            _context.Products.Add(p);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product p)
        {
            if (string.IsNullOrWhiteSpace(p.Name))
                ModelState.AddModelError("Name", "Ürün adı boş geçilemez.");

            if (p.Value < 0)
                ModelState.AddModelError("Value", "Değer negatif olamaz.");

            if (!ModelState.IsValid)
                return View(p);

            _context.Products.Update(p);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
