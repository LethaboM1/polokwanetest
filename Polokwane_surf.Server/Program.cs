using Microsoft.EntityFrameworkCore;
using Polokwane_surf.Server.Data;
using Polokwane_surf.Server.Service;

var port = Environment.GetEnvironmentVariable("PORT") ?? "7059";

var builder = WebApplication.CreateBuilder(args);

// Bind to HTTP on all interfaces with dynamic port (for Render)
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Configure MySQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// Add EmailService and Controllers
builder.Services.AddScoped<EmailService>();
builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure CORS to allow your frontend origins to work
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlifyAndLocalhost", policy =>
    {
        policy.WithOrigins(
            "https://polokwanewebsite.netlify.app",  // live frontend URL
            "https://localhost:49381",               // your React dev server URL (note the port)
            "http://localhost:5173"                   // any other local dev URL you use
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Enable HTTPS redirection and swagger only locally
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowNetlifyAndLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
