using Adapters.Controllers;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Payments;
using Application.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Extensions;

public static class AdaptersExtensions
{
    public static IServiceCollection AddAdaptersDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPaymentController, PaymentController>();

        services.AddScoped<IPaymentGateway, PaymentGateway>();
        services.AddScoped<IOrderGateway, OrderGateway>();

        return services;
    }
}