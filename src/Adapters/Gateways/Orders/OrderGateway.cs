using Application.Gateways;
using Entities.Payments;

namespace Adapters.Gateways.Orders;

public class OrderGateway(IOrderClient orderClient) : IOrderGateway
{
    public Task UpdateStatusOrder(Payment payment)
    {
        var request = new UpdatePaymentStatusRequest(payment.OrderId, payment.Status.IsPaid());
        return orderClient.UpdateStatusOrder(request);
    }
}