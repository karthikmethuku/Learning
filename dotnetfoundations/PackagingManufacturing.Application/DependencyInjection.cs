using Microsoft.Extensions.DependencyInjection;
using PackagingManufacturing.Application.Services;

namespace PackagingManufacturing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProductService>();
        services.AddScoped<MaterialService>();
        services.AddScoped<ProductionOrderService>();
        services.AddScoped<WorkOrderService>();
        services.AddScoped<ProductionLineService>();
        return services;
    }
}
