using AutoMapper;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WebApi.Mapping;

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
                app.UseSwagger();
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
    }
}