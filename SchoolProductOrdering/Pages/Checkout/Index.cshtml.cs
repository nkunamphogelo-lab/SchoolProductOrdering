using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolProductOrdering.Models;
using System.Text.Json;

namespace SchoolProductOrdering.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new();

        public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);

        // 1. Added property to tell the HTML to show the success message
        public bool IsSuccess { get; set; } = false;

        public void OnGet(bool success = false)
        {
            // If the URL has ?success=true, we show the thank you message
            IsSuccess = success;

            var cartJson = HttpContext.Session.GetString("CartItems");

            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
            }
        }

        // 2. Updated UpdateQuantity to handle the Trash Can as well
        public IActionResult OnPostUpdateQuantity(int productId, int increment)
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (string.IsNullOrEmpty(cartJson)) return RedirectToPage();

            var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson)!;
            var item = cart.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                item.Quantity += increment;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
            }

            HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(cart));
            HttpContext.Session.SetInt32("CartCount", cart.Sum(i => i.Quantity));

            return RedirectToPage();
        }

        // 3. New Method: Process Payment and Clear Cart
        public IActionResult OnPostProcessPayment()
        {
            // Here you would normally save the order to a Database

            // Clear the session data
            HttpContext.Session.Remove("CartItems");
            HttpContext.Session.SetInt32("CartCount", 0);

            // Redirect back to the GET method with a success flag
            return RedirectToPage(new { success = true });
        }

        // 4. Method to manually clear the cart
        public IActionResult OnPostClearCart()
        {
            HttpContext.Session.Remove("CartItems");
            HttpContext.Session.SetInt32("CartCount", 0);
            return RedirectToPage();
        }
    }
}