using Application.Gateways;
using Entities.Payments;

namespace Adapters.Gateways.Payments;

public class PaymentGateway(
    IPaymentRepository repository,
    IPaymentClient client) : IPaymentGateway
{
    public void Save(Payment payment) => repository.Add(payment);

    public Payment? GetById(Guid id) => repository.GetById(id);

    public void Update(Payment payment) => repository.Update(payment);

    public Task<string> GenerateQrCode(Payment payment) => client.GenerateQrCode(payment);
}