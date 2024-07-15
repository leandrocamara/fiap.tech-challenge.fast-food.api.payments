using Entities.Payments;

namespace Adapters.Gateways.Payments;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByOrderId(Guid orderId);
}