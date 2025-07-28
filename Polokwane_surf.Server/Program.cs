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

// Configure CORS to allow requests from local React dev and deployed Netlify app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlify", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",                       // React local dev (Vite)
            "https://polokwanewebsite.netlify.app"        // Live Netlify site
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowNetlify");

app.UseAuthorization();

app.MapControllers();

app.Run();
