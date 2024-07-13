using Application.Gateways;
using Entities.Payments;
using Entities.SeedWork;

namespace Application.UseCases.Payments;

public interface ICreatePaymentUseCase : IUseCase<CreatePaymentRequest, CreatePaymentResponse>;

public sealed class CreatePaymentUseCase(IPaymentGateway paymentGateway) : ICreatePaymentUseCase
{
    public async Task<CreatePaymentResponse> Execute(CreatePaymentRequest request)
    {
        try
        {
            var payment = new Payment(request.OrderId, request.Amount);
            var qrCode = await paymentGateway.GenerateQrCode(payment);

            payment.SetQrCode(qrCode);
            paymentGateway.Save(payment);

            var pngQrCode = await paymentGateway.ConvertToPngQrCode(payment);
            return new CreatePaymentResponse(payment.Id, pngQrCode);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
        }
    }
}

public record CreatePaymentRequest(Guid OrderId, decimal Amount);

public record CreatePaymentResponse(Guid Id, string QrCode);