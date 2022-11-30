using BelajarWebApi.Data;
using BelajarWebApi.Models;
using BelajarWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// seperti bagian tubuh, tangan , kaki , kepala

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = securityKey
        };
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IIdentityService, IdentityService>();
builder.Services.AddSingleton<IGhibliApi, GhibliApi>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// seperti cara makan

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//DataSeeder.Seed(app.Services);

app.Run();
