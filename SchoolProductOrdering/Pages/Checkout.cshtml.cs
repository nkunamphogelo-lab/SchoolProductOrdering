using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolProductOrdering.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace SchoolProductOrdering.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "First name is required")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = string.Empty;

        [BindProperty]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Cellphone number is required")]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Delivery location is required")]
        public string DeliveryLocation { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public void OnGet()
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
                TotalAmount = cart.Sum(item => item.Price * item.Quantity);
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Clear the cart
            HttpContext.Session.Remove("CartItems");
            HttpContext.Session.Remove("CartCount");

            // Redirect to the success page
            return RedirectToPage("/OrderSuccess");
        }
    }
}