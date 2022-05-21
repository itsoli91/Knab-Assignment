using Knab.Identity.Api.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Knab.Identity.Api.Controllers;

[Route("api")]
public class ResourceController : Controller
{
    private readonly UserManager<KnabUser> _userManager;

    public ResourceController(UserManager<KnabUser> userManager)
    {
        _userManager = userManager;
    }

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("message")]
    public async Task<IActionResult> GetMessage()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return BadRequest();

        return Content($"{user.UserName} has been successfully authenticated.");
    }
}