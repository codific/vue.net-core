using AutoMapper;
using Core.DataAccess;
using Core.MappingProfiles;
using Entities.Identity;
using GSK.DAL;
using GSK.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EntityContext>(options => options.UseInMemoryDatabase(databaseName: "VueNetDatabase"));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<EntityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 15;
            });

            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(config =>
            {
                config.AddProfile<IdentityMappingProfile>();
            })));

            services.ConfigureServices();
            services.ConfigureDataAccess<EntityContext, ApplicationUnitOfWork>();
            services.AddScoped<IStandardRepository, StandardRepository>();

            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            services.AddMvc();
            services.AddHttpContextAccessor();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

        }
    }
}
