using Adapters.Gateways.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients;

public class OrderClient(ISendEndpointProvider sendEndpointProvider, ILogger<OrderClient> logger) : IOrderClient
{
    private const string QueueName = "payment-updated";

    public async Task UpdateStatusOrder(UpdatePaymentStatusRequest request)
    {
        logger.LogInformation("Publishing message: {Text}", JsonConvert.SerializeObject(request));

        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueName}"));

        await endpoint.Send(request);
    }
}