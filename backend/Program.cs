using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SignlR_Web_ApI.DataServices;
using SignlR_Web_ApI.Helper;
using SignlR_Web_ApI.Hubs;
using SignlR_Web_ApI.Models;
using SignlR_Web_ApI.Repo.EntityFrameWork.Data;
using SignlR_Web_ApI.Services;
namespace SignlR_Web_ApI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddSignalR(); // as service to enable signalR in our application
        
        // Custom services
        // automatically map Model JWT to section 'JWT'
        builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
        
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("connectionStr")
            ));
        builder.Services.AddScoped<DbContext, AppDbContext>();
        builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddScoped<IJwtServices, JwtServices>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("reactApp", builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials() // Required for SignalR
                    .WithOrigins("http://localhost:5173"); // CORS configuration for the client application
                //http://localhost:5173
            });
        });
        builder.Services.AddAuthentication(
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters() {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:IssuerIp"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:AudienceIP"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecrityKey"])),
                ValidateLifetime = true,
            };
        });
        builder.Services.AddSingleton<ShardDb>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("reactApp");

        app.MapHub<ChatHub>("/Chat").RequireCors("reactApp"); // this is the endpoint that the application use to listen through up socket 
        
        app.Run();
    }
}