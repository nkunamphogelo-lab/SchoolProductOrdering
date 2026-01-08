using Microsoft.EntityFrameworkCore;
using SchoolProductOrdering.Models;

namespace SchoolProductOrdering.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Products.Any())
                return;

            context.Products.AddRange(
                new Product { Name = "Laptop", Price = 12000, Description = "School laptop" },
                new Product { Name = "Headphones", Price = 500, Description = "Wireless headphones" },
                new Product { Name = "Mouse", Price = 200, Description = "USB Mouse" }
            );

            context.SaveChanges();
        }
    }
}
