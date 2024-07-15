using Adapters.Gateways.Payments;
using Entities.Payments;
using Microsoft.EntityFrameworkCore;

namespace External.Persistence.Repositories;

public sealed class PaymentRepository(PaymentsContext context) : BaseRepository<Payment>(context), IPaymentRepository
{
    public Task<Payment?> GetByOrderId(Guid orderId) =>
        context.Payments.FirstOrDefaultAsync(payment => payment.OrderId.Equals(orderId));
}