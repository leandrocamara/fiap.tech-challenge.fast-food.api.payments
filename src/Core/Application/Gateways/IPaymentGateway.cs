﻿using Entities.Payments;

namespace Application.Gateways
{
    public interface IPaymentGateway
    {
        void Save(Payment payment);
        Payment? GetById(Guid id);
        void Update(Payment payment);
        Task<string> GenerateQrCode(Payment payment);
        Task<string> ConvertToPngQrCode(Payment payment);
    }
}
