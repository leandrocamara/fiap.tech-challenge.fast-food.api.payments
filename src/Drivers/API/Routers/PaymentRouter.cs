using Adapters.Controllers;
using Application.UseCases.Payments;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Routers;

[ApiController]
[Route("api/payments")]
public class PaymentRouter(IPaymentController controller) : BaseRouter
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "", typeof(CreatePaymentResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePayment(CreatePaymentRequest request)
    {
        var result = await controller.CreatePayment(request);
        return HttpResponse(result);
    }

    [HttpPut("{id}/status")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStatusPayment(UpdateStatusRequest request)
    {
        var result = await controller.UpdateStatus(request);
        return HttpResponse(result);
    }
}