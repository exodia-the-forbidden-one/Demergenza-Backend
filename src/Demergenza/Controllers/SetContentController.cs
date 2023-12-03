using Demergenza.Application.Abstractions.Repositories.AboutUsRepository;
using Demergenza.Application.DTOs.AboutUs;
using Demergenza.Application.Services;
using Demergenza.Domain.Entities.PageContents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demergenza.Controllers;

[Authorize]
[Controller]
[Route("api/set-content")]
public class SetContentController : Controller
{
    private readonly IAboutUsReadRepository _aboutUsReadRepository;
    private readonly IAboutUsWriteRepository _aboutUsWriteRepository;
    private readonly ImageService _imageService;

    public SetContentController(IAboutUsReadRepository aboutUsReadRepository,
        IAboutUsWriteRepository aboutUsWriteRepository, ImageService imageService)
    {
        _aboutUsReadRepository = aboutUsReadRepository;
        _aboutUsWriteRepository = aboutUsWriteRepository;
        _imageService = imageService;
    }

    [HttpPost]
    [Route("about-us")]
    public async Task<IActionResult> AboutUs([FromForm] SetAboutUs setAboutUs)
    {
        AboutUs? aboutUs;

        aboutUs = await _aboutUsReadRepository.GetFirstAsync();
        if (aboutUs is not null)
        {
            aboutUs.TextContent = setAboutUs.TextContent;
            if (setAboutUs.Image is not null)
            {
                string? oldImageName = Path.GetFileName(aboutUs.ImagePath);
                string newImageName = _imageService.SaveImage(setAboutUs.Image);
                aboutUs.ImagePath = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}";
                if (oldImageName != null) _imageService.DeleteImageByName(oldImageName);
            }

            await _aboutUsWriteRepository.UpdateAsync(aboutUs);
        }
        else
        {
            string imagePath = "";
            if (setAboutUs.Image is not null)
            {
                string newImageName = _imageService.SaveImage(setAboutUs.Image);
                imagePath = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}";
            }

            aboutUs = new AboutUs()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                ImagePath = imagePath,
                TextContent = setAboutUs.TextContent
            };
            await _aboutUsWriteRepository.AddAsync(aboutUs);
        }

        return Ok(true);
    }
}