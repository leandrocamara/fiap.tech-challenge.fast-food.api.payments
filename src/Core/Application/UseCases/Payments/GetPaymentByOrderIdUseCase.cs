using Application.Gateways;
using Entities.SeedWork;

namespace Application.UseCases.Payments;

public interface IGetPaymentByOrderIdUseCase : IUseCase<Guid, GetPaymentByOrderIdResponse?>;

public sealed class GetPaymentByOrderIdUseCase(IPaymentGateway paymentGateway) : IGetPaymentByOrderIdUseCase
{
    public async Task<GetPaymentByOrderIdResponse?> Execute(Guid orderId)
    {
        try
        {
            var payment = await paymentGateway.GetByOrderId(orderId);

            if (payment is null) return null;

            return new GetPaymentByOrderIdResponse(
                payment.Id, payment.OrderId,
                payment.Amount, payment.Status, payment.CreatedAt);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover the payment. Error: {e.Message}", e);
        }
    }
}

public record GetPaymentByOrderIdResponse(Guid Id, Guid OrderId, decimal Amount, string Status, DateTime CreatedAt);