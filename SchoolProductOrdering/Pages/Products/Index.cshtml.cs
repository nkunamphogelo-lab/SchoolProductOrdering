using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // IMPORTANT: Needed for Session
using SchoolProductOrdering.Data;
using SchoolProductOrdering.Models;

namespace SchoolProductOrdering.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly SchoolProductOrdering.Data.ApplicationDbContext _context;

        public IndexModel(SchoolProductOrdering.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }

        // NEW: This method handles the "Add to Cart" button click
        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            // 1. Get current cart count from Session
            int currentCount = HttpContext.Session.GetInt32("CartCount") ?? 0;

            // 2. Increase it by 1
            currentCount++;

            // 3. Save it back to Session
            HttpContext.Session.SetInt32("CartCount", currentCount);

            // 4. Refresh the page to show the updated counter in the Navbar
            return RedirectToPage();
        }
    }
}