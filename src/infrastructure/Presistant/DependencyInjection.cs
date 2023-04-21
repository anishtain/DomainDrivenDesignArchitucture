using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.bases;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant;

public static class DependencyInjection
{
    public static IServiceCollection AddPresistante(this IServiceCollection services, IConfiguration configuration, bool isIdentity)
    {
        string connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(connectionString));

        if (isIdentity)
        {
            services.AddScoped<IUnitOfWork, SqldentityUnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(SqlIdentityRepository<,>));
        }
        else
        {
            services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(SqlRepository<,>));
        }

        return services;
    }
}
