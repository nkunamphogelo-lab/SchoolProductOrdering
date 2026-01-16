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

        // 1. Property to track the selected category from the URL
        [BindProperty(SupportsGet = true)]
        public string? SelectedCategory { get; set; }

        public async Task OnGetAsync()
        {
            // Start with all products as a queryable list
            var query = db_context.Products.AsQueryable();

            // 2. Filter logic: check if the name or description contains the category keyword
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                query = query.Where(p => p.Name.Contains(SelectedCategory) ||
                                         p.Description.Contains(SelectedCategory));
            }

            // Execute the query and get the list
            Products = await query.ToListAsync();

            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                // Session tracking logic remains active
            }
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var product = await db_context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var cartJson = HttpContext.Session.GetString("CartItems");
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();

            // Check if item already exists to increment quantity instead of adding a new row
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

            // Update the navbar count to show total items, not just unique rows
            HttpContext.Session.SetInt32("CartCount", cart.Sum(i => i.Quantity));

            return RedirectToPage();
        }
    }
}