using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolProductOrdering.Models;
using System.Text.Json;

namespace SchoolProductOrdering.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalAmount { get; set; }

        public void OnGet()
        {
            LoadCart();
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
                TotalAmount = CartItems.Sum(item => item.Price * item.Quantity);
            }
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(cart));
            HttpContext.Session.SetInt32("CartCount", cart.Sum(i => i.Quantity));
        }

        // Logic to completely remove an item
        public IActionResult OnPostRemove(int productId)
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
                cart.RemoveAll(x => x.ProductId == productId);
                SaveCart(cart);
            }
            return RedirectToPage();
        }

        // Logic to increase or decrease quantity
        public IActionResult OnPostUpdateQuantity(int productId, int change)
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
                var item = cart.FirstOrDefault(x => x.ProductId == productId);

                if (item != null)
                {
                    item.Quantity += change;
                    // If quantity reaches 0, remove it
                    if (item.Quantity <= 0) cart.Remove(item);
                }
                SaveCart(cart);
            }
            return RedirectToPage();
        }
    }
}