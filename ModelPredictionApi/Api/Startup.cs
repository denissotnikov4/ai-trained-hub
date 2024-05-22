using System.Reflection;
using Api.Controllers.Classify.Profiles;
using Api.Controllers.Detect.Profiles;
using Api.Controllers.Pose.Profiles;
using Api.Controllers.Segment.Profiles;
using AutoMapper;
using Core.Middlewares;
using FileLib.Extension;
using Logic;
using Microsoft.OpenApi.Models;

namespace Api;

/// <summary>
/// Класс Startup содержит конфигурацию и настройки для приложения.
/// </summary>
public class Startup
{
    /// <summary>
    /// Метод для настройки сервисов приложения
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public void ConfigureServices(IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<DetectProfile>();
            mc.AddProfile<SegmentProfile>();
            mc.AddProfile<PoseProfile>();
            mc.AddProfile<ClassifyProfile>();
        });

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
        
        services.AddLogicService();
        services.AddFileService();
        
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
 
    /// <summary>
    /// Метод для настройки конвейера обработки HTTP-запросов
    /// </summary>
    /// <param name="app">Объект приложения</param>
    /// <param name="env">Среда хостинга</param>
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