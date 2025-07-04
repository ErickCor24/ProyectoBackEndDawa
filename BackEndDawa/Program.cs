using BackEndDawa.Infrastructure;
using BackEndDawa.Models;
using BackEndDawa.Services.Application;
using BackEndDawa.Services.Ports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var AllowOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// -- Gget Connection string
var CONNECTION_STRING = builder.Configuration.GetConnectionString("DefaultConnection");
// -- Configurate connection
builder.Services.AddDbContext<ContextConnection>(options => options.UseSqlServer(CONNECTION_STRING));

// -- Add services to the container
builder.Services.AddScoped<ICompany, CompanyServiceImpl>();
builder.Services.AddScoped<IUserCompany, UserCompanySericeImpl>();

builder.Services.AddSingleton<ServiceUtility>();


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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------------------ JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings!.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
//-----------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowOrigins);

app.UseHttpsRedirection();

//
app.UseAuthentication();
//

app.UseAuthorization();

app.MapControllers();

app.Run();
