using Entities.Payments;

namespace Adapters.Gateways.Payments;

public interface IPaymentClient
{
    Task<string> GenerateQrCode(Payment payment);
}