using Microsoft.EntityFrameworkCore;
using SchoolProductOrdering.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. ADD CONTROLLER SERVICES (Required for MVC)
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 2. RUN SEED DATA
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// 3. FIX THE ROUTING MAP
// This tells the app to look for the Home Controller first when it starts
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();