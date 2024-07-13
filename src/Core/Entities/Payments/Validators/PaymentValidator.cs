using Entities.SeedWork;

namespace Entities.Payments.Validators;

internal sealed class PaymentValidator : IValidator<Payment>
{
    public bool IsValid(Payment payment, out string error)
    {
        // TODO: Implement
        error = string.Empty;
        return true;
    }
}