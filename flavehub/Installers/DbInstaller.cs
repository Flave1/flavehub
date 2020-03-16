using AutoMapper;
using flavehub.Data;
using flavehub.Domain;
using flavehub.Repository.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace flavehub.Installers
{
    public class DbInstaller : IInstaller
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                   options.UseSqlServer(
                       configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            
            services.AddScoped<IPostServices, PostServices>(); 
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICategoryServices, CategoryService>();
            services.AddAutoMapper(typeof(Startup)); 
            services.AddMediatR(typeof(Startup));
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod(); ;
                });
            });
        }
    }
}
