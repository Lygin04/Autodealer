// Подбор автомобилей
using Autodealer.Data;
using Autodealer.Extensions;
using Autodealer.GraphQL.GraphQLSchema;
using Autodealer.GraphQL.GraphQLTypes;
using Autodealer.Repositories;
using Autodealer.Repositories.Interfaces;
using Autodealer.Services;
using Autodealer.Services.Caching;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetConnectionString("Redis");
    option.InstanceName = "Cars_";
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey("O4enbSecretKeyO4enbSecretKeyO4enbSecretKey"u8.ToArray())
        };
    });

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<ProducerService>();
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<IEngineRepository, EngineRepository>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IRedisCacheService, RedisCacheService>();

builder.Services.AddTransient<AppQuery>();
builder.Services.AddTransient<CarType>();
builder.Services.AddTransient<AppSchema>();

builder.Services.AddGraphQL(b => b
    .AddSystemTextJson()
    .AddErrorInfoProvider(opt => 
    {
        opt.ExposeExceptionDetails = builder.Environment.IsDevelopment();
        opt.ExposeExtensions = true;
        opt.ExposeCode = builder.Environment.IsDevelopment();
    })
    .ConfigureExecutionOptions(opt => 
    {
        opt.EnableMetrics = true;
        opt.ThrowOnUnhandledException = builder.Environment.IsDevelopment();
    })
    .AddSchema<AppSchema>()
    .AddGraphTypes(typeof(AppSchema).Assembly)
);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuth();

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: "Autodealer", serviceVersion: "1.0.0");

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Autodealer"))
    .WithMetrics(meterProviderBuilder =>
    {
        meterProviderBuilder
            .SetResourceBuilder(resourceBuilder)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddPrometheusExporter();
    })
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            //.AddOtlpExporter()
            .AddJaegerExporter(o =>
            {
                o.AgentHost = builder.Configuration["JAEGER_HOST"] ?? "host.docker.internal";
                o.AgentPort = 6831;
            });
    })
    .UseOtlpExporter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL("/graphql");

app.UseGraphQLGraphiQL("/ui/graphql");

app.MapPrometheusScrapingEndpoint();

app.MapControllers();

app.Run();