using Data;
using Logic;
using Logic.Helper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repository;
using System.Text;

namespace Backend_Feleves
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register the DbContext
            builder.Services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BackendFeleves2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                options.UseLazyLoadingProxies();
            });

            // Register the Repository with the correct generic type
            builder.Services.AddTransient(typeof(Repository<>));

            builder.Services.AddTransient<DtoProvider>();
            builder.Services.AddTransient<CommentLogic>();
            builder.Services.AddTransient<UserLogic>();
            builder.Services.AddTransient<PictureLogic>();
            builder.Services.AddTransient<SalesItemLogic>();
            builder.Services.AddTransient<CourseLogic>();
            builder.Services.AddTransient<ContentLogic>();
            builder.Services.AddTransient<VideoLogic>();

            builder.Services.AddIdentity<User, IdentityRole>(
                    option =>
                    {
                        option.Password.RequireDigit = false;
                        option.Password.RequiredLength = 6;
                        option.Password.RequireNonAlphanumeric = false;
                        option.Password.RequireUppercase = false;
                        option.Password.RequireLowercase = false;
                    }
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MainDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "artlounge.com",
                    ValidIssuer = "artlounge.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Nagyonhossz�titkos�t�kulcsNagyonhossz�titkos�t�kulcsNagyonhossz�titkos�t�kulcsNagyonhossz�titkos�t�kulcsNagyonhossz�titkos�t�kulcsNagyonhossz�titkos�t�kulcs"))
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
