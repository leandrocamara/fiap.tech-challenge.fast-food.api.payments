using Adapters.Gateways.Orders;

namespace External.Clients;

public class OrderClient : IOrderClient
{
    public Task UpdateStatusOrder(UpdatePaymentStatusRequest request)
    {
        // TODO: Publish by SQS
        throw new NotImplementedException();
    }
}