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

        public bool IsSuccess { get; set; } = false;

        // 1. Property to store the selected payment method
        [BindProperty]
        public string SelectedPaymentMethod { get; set; } = "Credit Card";

        // 2. Property to display the payment method on the success screen
        public string? ConfirmedMethod { get; set; }

        public void OnGet(bool success = false, string? method = null)
        {
            IsSuccess = success;
            ConfirmedMethod = method;

            var cartJson = HttpContext.Session.GetString("CartItems");

            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
            }
        }

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

        // 3. Updated to capture the payment method before clearing the cart
        public IActionResult OnPostProcessPayment()
        {
            // Store the method used for the order
            string methodUsed = SelectedPaymentMethod;

            // Clear the session data
            HttpContext.Session.Remove("CartItems");
            HttpContext.Session.SetInt32("CartCount", 0);

            // Redirect with both the success flag AND the payment method used
            return RedirectToPage(new { success = true, method = methodUsed });
        }

        public IActionResult OnPostClearCart()
        {
            HttpContext.Session.Remove("CartItems");
            HttpContext.Session.SetInt32("CartCount", 0);
            return RedirectToPage();
        }
    }
}