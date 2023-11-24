using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Application.Abstractions.Repositories.CategoryRepository;
using Demergenza.Domain.Entities.Menu;
using Microsoft.AspNetCore.Mvc;
using Demergenza.Domain.Entities.Models;
using Demergenza.Domain.Entities.Admin;
using Demergenza.Application.Services;
using Demergenza.Application.Abstractions.Repositories.MenuRepository;
using Microsoft.AspNetCore.Authorization;
using Demergenza.Domain.Entities.Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Demergenza.Controllers
{
    [ApiController]
    [Route("api/menu")]
    public class MenuController : Controller
    {
        private readonly IAdminReadRepository _adminRead;
        private readonly ImageService _imageService;
        private readonly IMenuWriteRepository _menuWrite;
        private readonly IMenuReadRepository _menuRead;
        private readonly ICategoryReadRepository _categoryRead;

        public MenuController(
            IAdminReadRepository adminRead, ImageService imageService,
            IMenuWriteRepository menuWrite, IMenuReadRepository menuRead, ICategoryReadRepository categoryRead)
        {
            _adminRead = adminRead;
            _imageService = imageService;
            _menuWrite = menuWrite;
            _menuRead = menuRead;
            _categoryRead = categoryRead;
        }

        [HttpGet]
        [Route("getallmenusbycategoryname/{categoryName}")]
        public async Task<IActionResult> GetMenusByCategoryName([FromRoute] string categoryName)
        {
            var category = await _categoryRead.GetWhere(c => c.Name == categoryName).Select(c => new Category()
            {
                Id = c.Id,
                Menus = c.Menus,
                Name = c.Name,
                Image = c.Image
            }).FirstAsync();


            if (category == null) return NoContent();
            List<MenuResponse> menus = category.Menus.Select(menu => new MenuResponse()
            {
                Name = menu.Name,
                Ingredients = menu.Ingredients,
                Image = menu.Image,
                Price = menu.Price,
                Date = menu.Date
            }).OrderBy(m => m.Date).ToList();
            return Ok(menus);
        }

        [Authorize]
        [HttpPost]
        [Route("addmenu")]
        public async Task<IActionResult> AddMenu([FromForm] AddMenuModel addMenuModel)
        {


            string? imageName = addMenuModel.MenuImage != null ? _imageService.SaveImage(addMenuModel.MenuImage) : null;

            Admin? admin = await _adminRead.GetFirstAsync(admin => admin.Username == addMenuModel.AdminUserName);
            if (admin is null) return Unauthorized("specified admin not found");
            Category? category =
                await _categoryRead.GetFirstAsync(category => category.Id == Guid.Parse(addMenuModel.CategoryId));
            if (category is null) return BadRequest("specified category not found");
            Menu menu = new Menu()
            {
                Name = addMenuModel.MenuName,
                Price = addMenuModel.MenuPrice,
                Ingredients = addMenuModel.MenuIngredients,
                Admin = admin,
                CategoryId = category.Id,
                Date = DateTime.UtcNow
            };
            if (addMenuModel.MenuImage != null) menu.Image = $"{Request.Scheme}://{Request.Host}/data-images/{imageName}";
            bool isAdded = await _menuWrite.AddAsync(menu);
            return Ok(isAdded);
        }

        [Authorize]
        [HttpDelete]
        [Route("deletemenu/{id}")]
        public async Task<IActionResult> DeleteMenu([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid parsedId)) return BadRequest("invalid id type");
            Menu? menu = await _menuRead.GetByIdAsync(parsedId);
            if (menu is null)
            {
                return BadRequest("Specified category doesnt exist");
            }

            await _menuWrite.RemoveAsync(Guid.Parse(id));
            await _menuWrite.SaveAsync();
            bool isDeleted = _imageService.DeleteImageByName(Path.GetFileName(menu.Image));
            return Ok(isDeleted);
        }

        [Authorize]
        [HttpPost]
        [Route("updatemenu")]
        public async Task<IActionResult> UpdateMenu([FromForm] UpdateMenuModel menuModel)
        {
            Console.WriteLine(menuModel.Id);
            Menu? menu = await _menuRead.GetFirstAsync(m => m.Id == Guid.Parse(menuModel.Id));
            if (menu == null) return BadRequest("invalid id");
            Console.WriteLine(menuModel.Id);
            var admin = _adminRead.Select(a => a.Id).First();
            menu.AdminId = admin;
            menu.Name = menuModel.MenuName;
            menu.Price = menuModel.MenuPrice;
            menu.Ingredients = menuModel.MenuIngredients;
            menu.Date = DateTime.UtcNow;

            if (menuModel.MenuImage is not null)
            {
                string? oldImageName = Path.GetFileName(menu.Image);
                string newImageName = _imageService.SaveImage(menuModel.MenuImage);
                menu.Image = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}";
                if (oldImageName != null) _imageService.DeleteImageByName(oldImageName);
            }
            await _menuWrite.UpdateAsync(menu);

            return Ok(menu);
        }
    }
}