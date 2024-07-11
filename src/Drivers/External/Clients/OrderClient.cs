using Adapters.Gateways.Orders;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients;

public class OrderClient(IAmazonSQS sqsClient, ILogger<OrderClient> logger) : IOrderClient
{
    private const string QueueName = "payment-updated";

    public Task UpdateStatusOrder(UpdatePaymentStatusRequest request)
    {
        var message = JsonConvert.SerializeObject(request);
        logger.LogInformation("Publishing message: {Text}", message);

        return sqsClient.SendMessageAsync(new SendMessageRequest { QueueUrl = QueueName, MessageBody = message });
    }
}