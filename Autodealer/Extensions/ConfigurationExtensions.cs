using Microsoft.OpenApi.Models;

namespace Autodealer.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Добавляет поддержку Swagger с авторизацией в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddSwaggerWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Autodealer API",
                Description = "Сервис продажи машин",
                Version = "1.0.0.0"
            });

            var schema = new OpenApiSecurityScheme
            {
                Description = "Использование заголовка Authorization со схемой Bearer.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", schema);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { schema, new[] { "Bearer" } }
            });
        });
    }
}