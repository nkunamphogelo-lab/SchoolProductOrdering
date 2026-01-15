var builder = WebApplication.CreateBuilder(args);

// 1. Add Session services
builder.Services.AddRazorPages();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ... other middleware ...

app.UseRouting();

// 2. Enable Session - This MUST be between UseRouting and UseMapRazorPages
app.UseSession();

app.UseAuthorization();
app.MapRazorPages();
app.Run();