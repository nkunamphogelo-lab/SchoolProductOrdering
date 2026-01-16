using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SchoolProductOrdering.Data;
using SchoolProductOrdering.Models;

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
         Name = "Typek A4 Paper",
         Price = 95.00m,
         Description = "High-quality 80gsm white printing paper, 500 sheets.",
         ImagePath = "/image/products/Typek A4.jpg"
     },
     new Product
     {
         Name = "Butterfly 20 Pocket File",
         Price = 35.00m,
         Description = "A4 20 pocket display book for projects.",
         ImagePath = "/image/products/Butterfly A4 20 pocket file display book.jpg"
     },
     new Product
     {
         Name = "Coloured Pencils (10 Pack)",
         Price = 25.00m,
         Description = "KHOKI 10 pack assorted coloured pencils.",
         ImagePath = "/image/products/Coloured pencil 10 pack KHOKI.jpg"
     },
     new Product
     {
         Name = "2 Quire Counter Book",
         Price = 32.00m,
         Description = "192 pages feint and margin hardcover notebook.",
         ImagePath = "/image/products/Counter book 2 Quire 192 pages A4nfeint and margin.jpg"
     },
     new Product
     {
         Name = "Monami Crayons",
         Price = 45.00m,
         Description = "12 count multicolor crayons for art class.",
         ImagePath = "/image/products/crayons Monami 12 count Multicolor.jpg"
     },
     new Product
     {
         Name = "Electric Sharpener",
         Price = 120.00m,
         Description = "Deli electric pencil sharpener, black.",
         ImagePath = "/image/products/Deli electric pencil sharpener black.jpg"
     },
     new Product
     {
         Name = "Flip File A4 (10 Pocket)",
         Price = 22.00m,
         Description = "Standard 10 pocket display file.",
         ImagePath = "/image/products/Flip FILE A4 10 pocket display file.jpg"
     },
     new Product
     {
         Name = "Croxley Glue Sticks",
         Price = 55.00m,
         Description = "3 piece set of 36g glue sticks.",
         ImagePath = "/image/products/Glue stick Croxley 3 piece set 36g.jpg"
     },
     new Product
     {
         Name = "Marlin Pencils (12 Pack)",
         Price = 30.00m,
         Description = "Quality HB graphite pencils.",
         ImagePath = "/image/products/Marlin pencils 12 pack.jpg"
     },
     new Product
     {
         Name = "Staedtler Tradition Eraser Set",
         Price = 40.00m,
         Description = "Tradition eraser and sharpener set.",
         ImagePath = "/image/products/Staedtler tradition eraser and sharpener set.jpg"
     },
     new Product
     {
         Name = "Marlin Erasers (5 Pack)",
         Price = 15.00m,
         Description = "5 piece set of white erasers.",
         ImagePath = "/image/products/Marlin Eraser 5 piece set.jpg"
     },
     new Product
     {
         Name = "Computer Mouse",
         Price = 150.00m,
         Description = "Optical USB mouse for school computers.",
         ImagePath = "/image/products/Mouse.jpg"
     }


    // Repeat for all other stationery items...
); ;

            context.SaveChanges(); // Commit to SQL database
        } // Closes Initialize method
    } // Closes SeedData class
} // Closes namespace SchoolProductOrdering.Data