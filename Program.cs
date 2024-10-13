using Microsoft.EntityFrameworkCore;
using ApiEmpresa.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? cadena = builder.Configuration.GetConnectionString("DefaultConnection") ?? "OtraCadena";
builder.Services.AddControllers();
builder.Services.AddDbContext<Conexiones>(opt =>
      opt.UseMySQL(cadena));
    //opt.UseSqlServer(cadena)); //esto es para SQL Server
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();