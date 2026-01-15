using Microsoft.EntityFrameworkCore;
using SchoolProductOrdering.Data;
using SchoolProductOrdering.Models; // THIS FIXES THE 'PRODUCT' ERROR

var builder = WebApplication.CreateBuilder(args);

// Register the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Session services
builder.Services.AddRazorPages();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// --- SEED PRODUCTS DATA ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    context.Database.EnsureCreated(); // Ensures table exists

    if (!context.Products.Any()) // Only adds if table is empty
    {
        context.Products.AddRange(
            new Product
            {
                Name = "Scientific Calculator",
                Price = 350.00m,
                Description = "Advanced functions for math and science students."
            },
            new Product
            {
                Name = "A4 Hardcover Notebook",
                Price = 45.00m,
                Description = "192 pages, feint and margin ruled."
            },
            new Product
            {
                Name = "HB Pencils (12 Pack)",
                Price = 30.00m,
                Description = "High-quality graphite pencils."
            }
        );
        context.SaveChanges(); // Saves to SQL Server
    }
}

// Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Required for Cart
app.UseAuthorization();
app.MapRazorPages();

app.Run();