using Demergenza.Application.Abstractions.Repositories.AboutUsRepository;
using Demergenza.Domain.Entities.PageContents;
using Microsoft.AspNetCore.Mvc;

namespace Demergenza.Controllers;

[Controller]
[Route("api/get-content")]
public class GetContentController : Controller
{
    private IAboutUsReadRepository _aboutUsRead;

    public GetContentController(IAboutUsReadRepository aboutUsRead)
    {
        _aboutUsRead = aboutUsRead;
    }

    [HttpGet]
    [Route("about-us")]
    public async Task<IActionResult> GetAboutUs()
    {
        AboutUs? aboutUs = await _aboutUsRead.GetFirstAsync();
        return Ok(aboutUs);
    }
}