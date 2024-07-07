using Entities.Payments;

namespace Application.Gateways;

public interface IOrderGateway
{
    Task UpdateStatusOrder(Guid orderId, PaymentStatus paymentStatus);
}