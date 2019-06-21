using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class DataAccessConfigurationExtensions
    {
        public static IServiceCollection ConfigureDataAccess<TContext, TUnitOfWork>(this IServiceCollection services)
            where TContext : DbContext, IDatabaseContext
            where TUnitOfWork : class, IUnitOfWork
        {
            services.AddSingleton<IUowProvider, UowProvider>();
            services.AddTransient<IDatabaseContext, TContext>();
            services.AddTransient<IUnitOfWork, TUnitOfWork>();

            return services;
        }
    }
}
