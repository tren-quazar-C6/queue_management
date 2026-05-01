using Microsoft.EntityFrameworkCore;
using queue_management.Data;
using queue_management.Services;

var builder = WebApplication.CreateBuilder(args);

// ========================
// SERVICES
// ========================

// MVC + API Controllers
// AddControllersWithViews registra MVC (TurnController, UserController, HomeController)
// AddControllers agrega soporte para [ApiController] (AttendanceController, QueueController - Parte 4)
builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        opts.JsonSerializerOptions.PropertyNamingPolicy =
            System.Text.Json.JsonNamingPolicy.CamelCase;
    });

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
app.UseStaticFiles();

// Routing
app.UseRouting();

// Auth (aunque no lo uses aun, debe ir aqui)
app.UseAuthorization();

// Routes MVC tradicionales (vistas Razor)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Routes API REST - Parte 4: AttendanceController y QueueController
// Usan atributo [Route("api/...")] definido directamente en cada controller
app.MapControllers();

// Run app
app.Run();
