using LearningWebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
using OData.Swagger.Services;


namespace LearningWebApi.Api;

public class Program
{
    public static void Main(string[] args)
    {
        // configure services
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);

        // configure middleware
        var app = builder.Build();
        ConfigureMiddleware(app);

        app.Run();
    }

    // Add services to the container.
    private static void ConfigureServices(IServiceCollection services)
    {
        // Add Infrastructure
        services.AddInfrastructure();
        
        // Add Cores
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        // Add OData
        services.AddControllers().AddOData(options => options.Select());


        // Add Swagger Versioning
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            // options.AssumeDefaultVersionWhenUnspecified = true;
            // options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo {Title = "LearningWebApi.Api", Version = "v1"});
            options.SwaggerDoc("v2", new OpenApiInfo {Title = "LearningWebApi.Api", Version = "v2"});
        });
        
        // Add Swagger Support for OData
        services.AddOdataSwaggerSupport();
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LearningWebApi.Api v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "LearningWebApi.Api v2");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        app.MapControllers();
    }
}