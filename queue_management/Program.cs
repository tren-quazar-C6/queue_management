using Microsoft.EntityFrameworkCore;
using queue_management.Data;
using queue_management.Services;

var builder = WebApplication.CreateBuilder(args);

// ========================
// SERVICES
// ========================

// MVC
builder.Services.AddControllersWithViews();

// DB CONTEXT
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MySqlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// SERVICES
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TurnService>();

var app = builder.Build();

// ========================
// PIPELINE (ORDEN IMPORTA MUCHO)
// ========================

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS + static files
app.UseHttpsRedirection();
app.UseStaticFiles(); // 🔥 ESTO ES CRÍTICO

// Routing
app.UseRouting();

// Auth (aunque no lo uses aún, debe ir aquí)
app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Run app
app.Run();