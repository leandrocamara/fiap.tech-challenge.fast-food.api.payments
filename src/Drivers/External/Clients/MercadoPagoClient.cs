using System.Globalization;
using Adapters.Gateways.Payments;
using Entities.Payments;
using QRCoder;

namespace External.Clients;

internal class MercadoPagoClient : IPaymentClient
{
    public Task<string> GenerateQrCode(Payment payment)
    {
        var qrCode = @$"
            PaymentId={payment.Id};
            OrderId={payment.OrderId};
            Amount={payment.Amount.ToString(CultureInfo.InvariantCulture)};
            Type={nameof(MercadoPagoClient)}";

        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(qrCode, QRCodeGenerator.ECCLevel.Q);
        var pngQrCode = new PngByteQRCode(qrCodeData);

        return Task.FromResult(Convert.ToBase64String(pngQrCode.GetGraphic(20)));
    }
}