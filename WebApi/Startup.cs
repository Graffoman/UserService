using AutoMapper;
using Newtonsoft.Json;
using RabbitMQ.Implementations;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Mapping;
using WebApi.Settings;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InstallAutomapper(services);
            services.AddServices(Configuration);
            services.AddControllers()
                    .AddNewtonsoftJson(options => options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto)
                    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddMassTransit(x => {
                x.UsingRabbitMq((context, cfg) =>
                {
                    ConfigureRmq(cfg, Configuration);
                });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();   
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHealthChecks("/health");
            app.UseHealthChecks("/db_ef_healthcheck", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("db_ef_healthcheck") 
            });

            app.UseRouting();

            //app.UseAuthorization();    
          


            if (!env.IsProduction())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
               
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        private static IServiceCollection InstallAutomapper(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
            return services;
        }
        
        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoleMappingsProfile>();
                cfg.AddProfile<PermissionMappingsProfile>();                
                cfg.AddProfile<GroupMappingsProfile>();
                cfg.AddProfile<UserGroupMappingsProfile>();
                cfg.AddProfile<UserRoleMappingsProfile>();
                cfg.AddProfile<UserMappingsProfile>();
                
                cfg.AddProfile<Services.Implementations.Mapping.RoleMappingProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.PermissionMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.GroupMappingProfile>();                
                cfg.AddProfile<Services.Implementations.Mapping.UserGroupMappingProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.UserRoleMappingProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.UserMappingsProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }

        /// <summary>
        /// Конфигурирование RMQ.
        /// </summary>
        /// <param name="configurator"> Конфигуратор RMQ. </param>
        /// <param name="configuration"> Конфигурация приложения. </param>
        private static void ConfigureRmq(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
        {
            var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;
            configurator.Host(rmqSettings.Host,
                rmqSettings.VHost,
                h =>
                {
                    h.Username(rmqSettings.Login);
                    h.Password(rmqSettings.Password);
                });
        }
    }
}