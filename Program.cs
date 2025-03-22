using Microsoft.EntityFrameworkCore;
using RecipeSharingWebsiteAPI.Data; // Add this line to include the namespace where RecipeSharingWebsiteContext is defined

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Add this to register MVC services

// Register the DbContext for database connection (update with your context and connection)
builder.Services.AddDbContext<RecipeSharingWebsiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
