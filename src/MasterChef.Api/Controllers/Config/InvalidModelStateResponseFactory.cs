using MasterChef.Domain.Resources;
using MasterChef.Infra.Helpers.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers.Config;

/// <summary>
/// 
/// </summary>
public static class InvalidModelStateResponseFactory
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var errors = context.ModelState.GetErrorMessages();
        var response = new ErrorResource(messages: errors);

        return new BadRequestObjectResult(response);
    }
}