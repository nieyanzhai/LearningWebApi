using LearningWebApi.Service.ApiDataService;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace LearningWebApi.Service;

public static class DependencyInjection
{
    private const string BaseUrl = "https://localhost:7090";

    public static IServiceCollection AddSharedService(this IServiceCollection services)
    {
        var options = new RestClientOptions(BaseUrl);
        services.AddSingleton(new RestClient(new HttpClient(), options));
        services.AddSingleton<IDataService, DataService>();

        return services;
    }
}