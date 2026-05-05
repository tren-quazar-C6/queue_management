using Microsoft.EntityFrameworkCore;
using queue_management.Data;
using queue_management.Hubs;   // ← NUEVO: importa el Hub
using queue_management.Services;

var builder = WebApplication.CreateBuilder(args);

// ========================
// SERVICES
// ========================

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

// ========================
// SIGNALR
// ← NUEVO: registra SignalR en el contenedor de DI.
// SignalR ya viene incluido en el SDK de ASP.NET Core (.NET 6+),
// no necesitas instalar ningún NuGet extra.
// ========================
builder.Services.AddSignalR();

var app = builder.Build();

// ========================
// PIPELINE
// ========================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Routes MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Routes API REST
app.MapControllers();

// ========================
// SIGNALR ENDPOINT
// ← NUEVO: expone la URL del hub para que los clientes JS puedan conectarse.
// El cliente usará: new signalR.HubConnectionBuilder().withUrl("/queueHub")
// ========================
app.MapHub<QueueHub>("/queueHub");

app.Run();