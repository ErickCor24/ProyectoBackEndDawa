using BackEndDawa.Infrastructure;
using BackEndDawa.Services.Application;
using BackEndDawa.Services.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var AllowOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// -- Gget Connection string
var CONNECTION_STRING = builder.Configuration.GetConnectionString("DefaultConnection");
// -- Configurate connection
builder.Services.AddDbContext<ContextConnection>(options => options.UseSqlServer(CONNECTION_STRING));

// -- Add services to the container
builder.Services.AddScoped<ICompanyService, CompanyServiceImpl>();
builder.Services.AddScoped<IVehicleService, VehicleServiceImpl>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowOrigins,
        policy =>
        { 
            policy.AllowAnyHeader( )
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(AllowOrigins);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
