using MasterChef.Domain.Resources;
using MasterChef.Infra.Helpers.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers.Config;

public static class InvalidModelStateResponseFactory
{
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var errors = context.ModelState.GetErrorMessages();
        var response = new ErrorResource(messages: errors);

        return new BadRequestObjectResult(response);
    }
}