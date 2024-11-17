using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserCore.Enums;
using UserCore.Infrastructure;
using UserCore.Interfaces.Auth;

namespace UserCore.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentication(this IServiceCollection services, IOptions<JwtOptions> jwtOptions)
    {
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
            opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = [new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Value.SecretKey))]
                };

                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["hot-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });

        
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Permission", policy =>
            {
                policy.Requirements.Add(new PermissionRequirement(new Permission[] { /* указать права */ }));
            });
        });
    }
    
    public static IEndpointConventionBuilder RequirePermission<TBuilder>(this TBuilder builder,
        params Permission[] permissions) where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(permissions)));
    }
}