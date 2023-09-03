using ADloginAPI.Controllers;
using ADloginAPI.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Register services here
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Set the default to your cookie authentication scheme
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Google"; // Specify your desired external authentication scheme here

})
    //.AddCookie(options =>
    //{
    //    options.LoginPath = "/auth/login";
    //})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        // Configure your cookie authentication options here
        options.LoginPath = "/auth/login";
    })
   .AddGoogle(options =>
   {
       options.ClientId = "350765916364-viv3al52jr829vi0dp8okig9m53k5daj.apps.googleusercontent.com";
       options.ClientSecret = "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";
   });
   
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
