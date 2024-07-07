namespace Entities.Payments;

public readonly struct PaymentStatus
{
    private EPaymentStatus Value { get; }

    public bool IsPaid() => Value is EPaymentStatus.Paid;

    public static PaymentStatus Pending() => new(EPaymentStatus.Pending);
    public static PaymentStatus Paid() => new(EPaymentStatus.Paid);
    public static PaymentStatus Failed() => new(EPaymentStatus.Failed);

    public static implicit operator short(PaymentStatus status) => (short)status.Value;
    public static implicit operator PaymentStatus(short value) => new((EPaymentStatus)value);
    public static implicit operator string(PaymentStatus status) => status.ToString();

    public override string ToString() => Value.ToString();

    private PaymentStatus(EPaymentStatus status) => Value = status;

    private enum EPaymentStatus : short
    {
        Pending = 1,
        Paid = 2,
        Failed = 3
    }
}