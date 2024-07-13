using Adapters.Gateways.Payments;
using Entities.Payments;

namespace External.Persistence.Repositories;

public sealed class PaymentRepository(PaymentsContext context) : BaseRepository<Payment>(context), IPaymentRepository
{
}