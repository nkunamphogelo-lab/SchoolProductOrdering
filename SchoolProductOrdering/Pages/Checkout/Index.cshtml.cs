using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolProductOrdering.Models;
using System.Text.Json;

namespace SchoolProductOrdering.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        // 1. Property to hold the items for the checkout list
        public List<CartItem> CartItems { get; set; } = new();

        // 2. Property to calculate the total price for the order summary
        public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);

        public void OnGet()
        {
            // 3. Retrieve the cart items from the Session memory
            var cartJson = HttpContext.Session.GetString("CartItems");

            if (!string.IsNullOrEmpty(cartJson))
            {
                // Convert the JSON text back into a C# list of CartItems
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
            }
        }

        // 4. Method to clear the cart after payment or if the user clicks "Clear"
        public IActionResult OnPostUpdateQuantity(int productId, int increment)
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (string.IsNullOrEmpty(cartJson)) return RedirectToPage();

            var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson)!;
            var item = cart.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                // Increase or decrease the quantity
                item.Quantity += increment;

                // If quantity drops to 0, remove the item entirely
                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
            }

            // Save the updated list back to the session
            HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(cart));
            HttpContext.Session.SetInt32("CartCount", cart.Sum(i => i.Quantity));

            return RedirectToPage();
        }
    }
}