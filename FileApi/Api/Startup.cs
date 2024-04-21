using System.Reflection;
using AspNetCore.Yandex.ObjectStorage.Extensions;
using AutoMapper;
using Core.Dapper.Connection;
using Core.Middlewares;
using Core.Migration.Extensions;
using Dal;
using Logic;
using Microsoft.OpenApi.Models;
using FileMappingProfile = Api.Controllers.File.Profiles.FileMappingProfile;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                    policy =>
                    {
                        policy.WithOrigins("https://console.yandex.cloud/");
                    });
            });
            
            services.AddSingleton<DapperContext>();
            services.AddMigrationRunner(_configuration, typeof(Dal.Migrations.InitialMigration).Assembly);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Logic.Profiles.File.FileMappingProfile());
                mc.AddProfile(new FileMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddYandexObjectStorage(_configuration);

            services.AddLogicService();
            services.AddDalService();
        
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                // Указываем путь к XML-файлу комментариев
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseMiddleware<ErrorHandlingMiddleware>();
        
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}