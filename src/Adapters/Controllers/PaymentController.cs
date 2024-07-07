using Adapters.Controllers.Common;
using Application.UseCases.Payments;

namespace Adapters.Controllers;

public interface IPaymentController
{
    Task<Result> CreatePayment(CreatePaymentRequest request);
    Task<Result> UpdateStatus(UpdateStatusRequest request);
}

public class PaymentController(
    ICreatePaymentUseCase createPaymentUseCase,
    IUpdateStatusUseCase updateStatusUseCase) : BaseController, IPaymentController
{
    public async Task<Result> CreatePayment(CreatePaymentRequest request)
    {
        try
        {
            var response = await Execute(() => createPaymentUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> UpdateStatus(UpdateStatusRequest request)
    {
        try
        {
            var response = await Execute(() => updateStatusUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }
}