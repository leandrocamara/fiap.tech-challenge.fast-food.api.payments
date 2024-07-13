namespace Adapters.Gateways.Orders;

public interface IOrderClient
{
    Task UpdateStatusOrder(UpdatePaymentStatusRequest request);
}

public record UpdatePaymentStatusRequest(Guid OrderId, bool Paid);