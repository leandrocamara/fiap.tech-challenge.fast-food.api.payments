using Application.Gateways;
using Entities.SeedWork;

namespace Application.UseCases.Payments;

public interface IUpdateStatusUseCase : IUseCase<UpdateStatusRequest, bool>;

public sealed class UpdateStatusUseCase(
    IPaymentGateway paymentGateway,
    IOrderGateway orderGateway) : IUpdateStatusUseCase
{
    public async Task<bool> Execute(UpdateStatusRequest request)
    {
        try
        {
            var payment = paymentGateway.GetById(request.Id);

            if (payment == null)
                throw new ApplicationException("Payment not found");

            payment.UpdateStatus(request.Paid);
            paymentGateway.Update(payment);

            await orderGateway.UpdateStatusOrder(payment);

            return true;
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover payment. Error: {e.Message}", e);
        }
    }
}

public record UpdateStatusRequest(Guid Id, bool Paid);