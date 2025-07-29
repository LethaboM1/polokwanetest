using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polokwane_surf.Server.Data;
using Polokwane_surf.Server.Service;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// Add Controllers & Services
builder.Services.AddControllers();
builder.Services.AddScoped<EmailService>();

// Configure CORS to allow requests from local dev and Netlify
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlify", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",                     // local React dev
            "https://polokwanewebsite.netlify.app"      // Netlify production
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
        // .AllowCredentials(); // Uncomment if using cookies/auth
    });
});

// Swagger for development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Log Origin headers for CORS debug (optional)
app.Use(async (context, next) =>
{
    var origin = context.Request.Headers["Origin"].ToString();
    if (!string.IsNullOrEmpty(origin))
    {
        Console.WriteLine($"Incoming request from: {origin}");
    }
    await next();
});

app.UseHttpsRedirection();

app.UseCors("AllowNetlify");   

app.UseAuthorization();

app.MapControllers();

app.Run();
