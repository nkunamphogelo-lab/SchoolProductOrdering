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

            // Inside Data/SeedData.cs
            context.Products.AddRange(
                new Product
                {
                    Name = "Laptop",
                    Price = 12000,
                    Description = "School laptop",
                    ImagePath = "images/products/School laptop.jpg"
                },
                new Product
                {
                    Name = "Headphones",
                    Price = 500,
                    Description = "Wireless headphones",
                    ImagePath = "images/products/Wireless headphones.jpg"
                },
                new Product
                {
                    Name = "Mouse",
                    Price = 200,
                    Description = "USB Mouse",
                    ImagePath = "images/products/USB Mouse.jpg"
                },
                new Product
                {
                    Name = "Mouse",
                    Price = 200,
                    Description = "USB Mouse",
                    ImagePath = "images/products/USB Mouse.jpg"
                },
                new Product
                {
                    Name = "Mouse",
                    Price = 200,
                    Description = "USB Mouse",
                    ImagePath = "images/products/USB Mouse.jpg"
                },
                new Product
                {
                    Name = "Typek A4",
                    Price = 100,
                    Description = "USB Mouse",
                    ImagePath = "images/products/Typek A4.jpg"
                },
                new Product
                {
                    Name = "Butterfly A4 20 pocket",
                    Price = 200,
                    Description = "  file display book",
                    ImagePath = "images/products/USB Mouse.jpg"
                }
            );

            context.SaveChanges();
        }
    }
}
 