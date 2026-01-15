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

        // Holds the list of products for the catalog
        public List<Product> Products { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Fetch all products from the database
            Products = await db_context.Products.ToListAsync();

            // This ensures the page "remembers" the cart when it loads
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                // We don't need to do anything with the list here, 
                // but this ensures the Session is active.
            }
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var product = await db_context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            // Retrieve current cart
            var cartJson = HttpContext.Session.GetString("CartItems");
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();

            // Add the item to the list
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            });

            // Save the list back to Session
            HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(cart));

            // Update the little red number in the Navbar
            HttpContext.Session.SetInt32("CartCount", cart.Count);

            return RedirectToPage();
        }
    }
}