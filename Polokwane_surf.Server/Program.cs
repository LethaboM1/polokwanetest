using Microsoft.EntityFrameworkCore;
using Polokwane_surf.Server.Data;
using Polokwane_surf.Server.Service;

var builder = WebApplication.CreateBuilder(args);

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

// Configure CORS to allow your frontend origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlify", policy =>
    {
        policy.WithOrigins(
            "https://polokwanewebsite.netlify.app",  //  live frontend
            "http://localhost:5173"                   // Local dev 
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
      
    });
});

// Add Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: Use CORS before Authorization and MapControllers
app.UseCors("AllowNetlify");

app.UseAuthorization();

app.MapControllers();

app.Run();
