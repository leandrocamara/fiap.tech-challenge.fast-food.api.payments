using Adapters.Controllers.Common;
using Application.UseCases.Payments;

namespace Adapters.Controllers;

public interface IPaymentController
{
    Task<Result> CreatePayment(CreatePaymentRequest request);
    Task<Result> GetPaymentByOrderId(Guid orderId);
    Task<Result> UpdateStatus(UpdateStatusRequest request);
}

public class PaymentController(
    ICreatePaymentUseCase createPaymentUseCase,
    IGetPaymentByOrderIdUseCase getPaymentByOrderIdUseCase,
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

    public async Task<Result> GetPaymentByOrderId(Guid orderId)
    {
        try
        {
            var response = await Execute(() => getPaymentByOrderIdUseCase.Execute(orderId));

            return response is null
                ? Result.NotFound()
                : Result.Success(response);
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