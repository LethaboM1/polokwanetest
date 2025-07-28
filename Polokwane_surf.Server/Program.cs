//using Microsoft.EntityFrameworkCore;
//using Polokwane_surf.Server.Data;
//using Polokwane_surf.Server.Service;


//var builder = WebApplication.CreateBuilder(args);

//// Define the CORS policy name
//var MyAllowSpecificOrigins = "AllowReactApp";

//// Add services to the container
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Configure SMTP settings for sending email
////builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));
//builder.Services.AddScoped<EmailService>();

//// Configure Entity Framework with MySQL
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(8, 0, 36))
//    )
//);

//// Configure CORS to allow the frontend origin
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});



//var app = builder.Build();

//// Use CORS before routing and authorization


//// Serve static files (for React build or development fallback)
//app.UseDefaultFiles();
//app.UseStaticFiles();

//// Swagger only in development
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.UseDeveloperExceptionPage();
//}

//app.UseHttpsRedirection();
//app.UseCors("AllowAll");
//app.UseAuthorization();

//app.MapControllers();

//// Fallback for SPA routing
//app.MapFallbackToFile("/index.html");

//app.Run();
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polokwane_surf.Server.Data;
using Polokwane_surf.Server.Service;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add DbContext (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// Controllers & Services
builder.Services.AddControllers();
builder.Services.AddScoped<EmailService>();

// Local CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://localhost:49381")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
