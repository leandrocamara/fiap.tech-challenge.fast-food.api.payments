using Entities.Payments.Validators;
using Entities.SeedWork;

namespace Entities.Payments;

public sealed class Payment : Entity, IAggregatedRoot
{
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string QrCode { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Payment(Guid orderId, decimal amount)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Amount = amount;
        Status = PaymentStatus.Pending();
        QrCode = string.Empty;
        CreatedAt = UpdatedAt = DateTime.UtcNow;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    public void SetQrCode(string qrCode) => QrCode = qrCode;

    public void UpdateStatus(bool paid)
    {
        UpdatedAt = DateTime.UtcNow;
        Status = paid
            ? PaymentStatus.Paid()
            : PaymentStatus.Failed();
    }

    private static readonly IValidator<Payment> Validator = new PaymentValidator();

    // Required for EF
    private Payment()
    {
    }
}