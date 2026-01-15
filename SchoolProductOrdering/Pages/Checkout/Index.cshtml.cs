using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolProductOrdering.Models;
using System.Text.Json;

namespace SchoolProductOrdering.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new();

        // This property calculates the price for the HTML
        public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);

        public void OnGet()
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
            }
        }

        // This method clears the cart when the button is clicked
        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            // Get current cart or create new
            var cartJson = HttpContext.Session.GetString("CartItems");
            List<CartItem> cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson)!;

            // Add item
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            });

            // Save back to Session
            HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(cart));
            HttpContext.Session.SetInt32("CartCount", cart.Count);

            return RedirectToPage();
        }
    }
} 
