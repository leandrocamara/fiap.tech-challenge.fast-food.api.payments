using Adapters.Gateways.Orders;
using Adapters.Gateways.Payments;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using External.Clients;
using External.Persistence;
using External.Persistence.Migrations;
using External.Persistence.Repositories;
using External.Settings;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace External.Extensions;

public static class ExternalExtensions
{
    public static IServiceCollection AddExternalDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PaymentsContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUnitOfWork, PaymentsContext>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddHttpClient();
        services.AddScoped<IOrderClient, OrderClient>();
        services.AddScoped<IPaymentClient, MercadoPagoClient>();

        SetupAmazonSqs(services, configuration);

        return services;
    }

    private static void SetupAmazonSqs(IServiceCollection services, IConfiguration configuration)
    {
        var settings = GetAmazonSqsSettings(configuration);
        
        services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(
            new SessionAWSCredentials(settings.AccessKey, settings.SecretKey, settings.SessionToken),
            new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region) }));
    }

    public static IHealthChecksBuilder AddSqsHealthCheck(
        this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        var settings = GetAmazonSqsSettings(configuration);

        return builder.AddSqs(options =>
        {
            options.Credentials = new SessionAWSCredentials(
                settings.AccessKey,
                settings.SecretKey,
                settings.SessionToken);
            options.RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region);
        }, name: "sqs_health_check", tags: new[] { "sqs", "healthcheck" });
    }

    private static AmazonSqsSettings GetAmazonSqsSettings(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(AmazonSqsSettings)).Get<AmazonSqsSettings>();

        if (settings is null)
            throw new ArgumentException($"{nameof(AmazonSqsSettings)} not found.");

        return settings;
    }

    public static void CreateDatabase(this IApplicationBuilder _, IConfiguration configuration)
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("Default"))
                .ScanIn(typeof(Initial).Assembly).For.Migrations().For.EmbeddedResources())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using (serviceProvider.CreateScope())
        {
            serviceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
        }
    }
}