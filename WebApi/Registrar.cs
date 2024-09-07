using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using WebApi.Settings;
using RabbitMQ.Abstractions;
using RabbitMQ.Implementations;

namespace WebApi
{
    /// <summary>
    /// Регистратор сервиса.
    /// </summary>
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettings = configuration.Get<ApplicationSettings>();
            var rabbitMqSettings = configuration.GetSection("RabbitSettings").Get<RabbitSettings>();

            services.AddSingleton(applicationSettings)
                    .AddSingleton(rabbitMqSettings)
                    .AddSingleton((IConfigurationRoot)configuration)
                    .InstallServices()
                    .ConfigureContext(applicationSettings.ConnectionString)
                    .InstallRepositories()
                    .InstallRabbitMQ();
            
            return services;
        }
        
        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IUserService, UserService>()
                .AddTransient<IGroupService, GroupService>()
                .AddTransient<IUserGroupService, UserGroupService>()
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<IUserRoleService, UserRoleService>();
            return serviceCollection;
        }
        
        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IGroupReposotory, GroupRepository>()
                .AddTransient<IUserGroupRepository, UserGroupRepository>()
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddTransient<IUserRoleRepository, UserRoleRepository>();
            return serviceCollection;
        }

        private static IServiceCollection InstallRabbitMQ(this IServiceCollection serviceCollection)
        {
            serviceCollection                
                .AddTransient<IRabbitMqProducer, RabbitMqProducer>();
            return serviceCollection;
        }
    }
}