﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder
                => optionsBuilder
                    .UseNpgsql(connectionString));

                    #region health checks
                    
                    services.AddHealthChecks()
                        .AddDbContextCheck<DatabaseContext>(
                            tags: new[] { "db_ef_healthcheck" },
                            customTestQuery: async (context, token) =>
                            {
                                return await context.Users.AnyAsync(token);
                            });

                    #endregion
                    return services;
        }
    }
}