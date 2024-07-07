using System.Globalization;
using Adapters.Gateways.Payments;
using Entities.Payments;

namespace External.Clients;

internal class MercadoPagoClient : IPaymentClient
{
    public Task<string> GenerateQrCode(Payment payment)
    {
        return Task.FromResult(@$"
            PaymentId={payment.Id};
            OrderId={payment.OrderId};
            Amount={payment.Amount.ToString(CultureInfo.InvariantCulture)};
            Type={nameof(MercadoPagoClient)}");
    }
}