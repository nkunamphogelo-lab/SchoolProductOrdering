using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolProductOrdering.Models;
using SchoolProductOrdering.Data;
using System.Text.Json;

namespace SchoolProductOrdering.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db_context;

        public IndexModel(ApplicationDbContext context)
        {
            db_context = context;
        }

        public List<Product> Products { get; set; } = new();

        // These properties were missing, which caused the red squiggles
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public string? CurrentFilter { get; set; }

        public async Task OnGetAsync()
        {
            CurrentFilter = SearchString;

            // Start with your original database table name
            var query = db_context.Products.AsQueryable();

            // Filter logic for the search bar
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(p => p.Name.Contains(SearchString) ||
                                         p.Description.Contains(SearchString));
            }

            Products = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var product = await db_context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var cartJson = HttpContext.Session.GetString("CartItems");
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(cart));
            HttpContext.Session.SetInt32("CartCount", cart.Sum(i => i.Quantity));

            return RedirectToPage();
        }
    }
}