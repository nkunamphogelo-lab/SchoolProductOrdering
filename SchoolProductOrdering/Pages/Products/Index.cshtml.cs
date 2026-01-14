using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        // This list holds the products fetched from the database
        public IList<Product> Product { get; set; } = default!;

        // This is the GET method that runs when you open the page
        public async Task OnGetAsync()
        {
            // This pulls the seeded products (Laptop, Mouse, etc.) into the list
            Product = await _context.Products.ToListAsync();
        }
    }
}