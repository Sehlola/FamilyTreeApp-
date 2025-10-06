using FamilyTreeApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add support for Blazor pages
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Connect to a local SQLite database file
builder.Services.AddDbContext<FamilyDbContext>(options =>
    options.UseSqlite("Data Source=familytree.db"));

// Add FamilyService so we can use it to get family data
builder.Services.AddScoped<FamilyService>();

var app = builder.Build();

// Show an error page if the app is not in development
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// Allow use of CSS, JS, and image files
app.UseStaticFiles();

// Enable page navigation in Blazor
app.UseRouting();

// Tell the app to use Blazor and a default page
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Run the app
app.Run();
