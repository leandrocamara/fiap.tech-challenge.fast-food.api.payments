using Application.Gateways;
using Entities.Payments;

namespace Adapters.Gateways.Orders;

public class OrderGateway(IOrderClient orderClient) : IOrderGateway
{
    public Task UpdateStatusOrder(Guid orderId, PaymentStatus paymentStatus)
    {
        var orderStatus = paymentStatus.IsPaid()
            ? EOrderStatus.Paid
            : EOrderStatus.Failed;

        throw new NotImplementedException();
    }

    public enum EOrderStatus : short
    {
        Paid = 1,
        Failed = 2
    }
}