using Microsoft.EntityFrameworkCore;
using SchoolProductOrdering.Models;
using SchoolProductOrdering.Data;

namespace SchoolProductOrdering.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // If there is already data, don't add more
            if (context.Products.Any())
                return;

            context.Products.AddRange(
                new Product
                {
                    Name = "Scientific Calculator",
                    Price = 350.00m,
                    Description = "Advanced math calculator for high school and university.",
                    ImagePath = "/images/products/calculator.jpg"
                },
                new Product
                {
                    Name = "A4 Typek Paper (500 Sheets)",
                    Price = 95.00m,
                    Description = "High-quality 80gsm white printing paper.",
                    ImagePath = "/images/products/typek-a4.jpg"
                },
                new Product
                {
                    Name = "Butterfly A4 Display Book",
                    Price = 45.00m,
                    Description = "20-pocket file for organizing school assignments.",
                    ImagePath = "/images/products/display-book.jpg"
                },
                new Product
                {
                    Name = "HB Pencils (12 Pack)",
                    Price = 35.00m,
                    Description = "Standard graphite pencils for writing and sketching.",
                    ImagePath = "/images/products/pencils.jpg"
                },
                new Product
                {
                    Name = "Hardcover Notebook (A4)",
                    Price = 40.00m,
                    Description = "192-page feint and margin ruled notebook.",
                    ImagePath = "/images/products/notebook.jpg"
                },
                new Product
                {
                    Name = "Bic Ballpoint Pens (Blue 10pk)",
                    Price = 30.00m,
                    Description = "Smooth writing pens for daily school work.",
                    ImagePath = "/images/products/pens.jpg"
                }
            );

            context.SaveChanges(); // Commit to SQL database
        } // Closes Initialize method
    } // Closes SeedData class
} // Closes namespace SchoolProductOrdering.Data