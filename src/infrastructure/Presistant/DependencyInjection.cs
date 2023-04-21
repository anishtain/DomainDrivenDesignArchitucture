using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.bases;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.roles;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.models.schemas.clients.users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            services.AddScoped<IUserStore, identities.UserStore>();
            services.AddScoped<IRoleStore, identities.RoleStore>();

            services.Configure<commons.sharedDatas.TokenConfig>(opt => 
            {
                opt.Audience = configuration["Secret:TokenConfig:Audience"];
                opt.Secret = configuration["Secret:TokenConfig:Secret"];
                opt.Issuer = configuration["Secret:TokenConfig:Issuer"];
            });

            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SqlIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["Security:TokenConfig:Audience"],
                        ValidIssuer = configuration["Security:TokenConfig:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:TokenConfig:Secret"]))
                    };

                });

        }
        else
        {
            services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(SqlRepository<,>));
        }


        return services;
    }
}
