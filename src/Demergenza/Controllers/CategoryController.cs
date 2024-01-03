using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Application.Abstractions.Repositories.CategoryRepository;
using Demergenza.Application.DTOs.Category;
using Demergenza.Application.DTOs.Menu;
using Demergenza.Application.Services;
using Demergenza.Domain.Entities.Admin;
using Demergenza.Domain.Entities.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demergenza.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryWriteRepository _categoryWrite;
        private readonly ICategoryReadRepository _categoryRead;
        private readonly IAdminReadRepository _adminRead;
        private readonly ImageService _imageService;

        public CategoryController(ICategoryWriteRepository categoryWrite,
            ICategoryReadRepository categoryRead, ImageService imageService, IAdminReadRepository adminRead)
        {
            _categoryWrite = categoryWrite;
            _categoryRead = categoryRead;
            _adminRead = adminRead;
            _imageService = imageService;
        }

        [Authorize]
        [HttpGet]
        [Route("getcategorybyid/{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] string id)
        {
            Category category = await _categoryRead.GetByIdAsync(Guid.Parse(id));
            return Ok(new CategoryResponse()
            {
                Id = category.Id,
                Admin = category.Admin,
                Image = category.Image,
                Menus = category.Menus.Select(m => new MenuResponse()
                {
                    Name = m.Name,
                    Image = m.Name,
                    Date = m.Date,
                    Ingredients = m.Ingredients,
                    Price = m.Price,
                    Id = m.Id
                }).ToList(),
                Name = category.Name
            });
        }

        [HttpGet]
        [Route("getallcategories")]
        public IActionResult GetAllCategories()
        {
            IQueryable<CategoryResponse> categories = _categoryRead.GetAll().Select(c => new CategoryResponse()
            {
                Id = c.Id,
                Image = c.Image,
                Menus = c.Menus.Select(m => new MenuResponse()
                {
                    Name = m.Name,
                    Image = m.Name,
                    Date = m.Date,
                    Ingredients = m.Ingredients,
                    Price = m.Price,
                    Id = m.Id
                }).ToList(),
                Name = c.Name
            });
            return Ok(categories);
        }

        [Authorize]
        [HttpPost]
        [Route("addcategory")]
        public async Task<IActionResult> AddCategory([FromForm] AddCategory addCategory)
        {
            string newImageName = _imageService.SaveImage(addCategory.categoryImage);
            Admin? admin = await _adminRead.GetFirstAsync(admin => admin.Username == addCategory.AdminUsername);
            if (admin is null) return Unauthorized();
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = addCategory.CategoryName,
                Image = newImageName,
                Admin = admin,
                Date = DateTime.UtcNow
            };

            var isAdded = await _categoryWrite.AddAsync(category);
            await _categoryWrite.SaveAsync();

            return Ok(isAdded);
        }

        [Authorize]
        [HttpDelete]
        [Route("deletecategory/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            Category? category = await _categoryRead.GetByIdAsync(Guid.Parse(id));
            if (category is null)
            {
                return BadRequest("Specified category doesnt exist");
            }

            await _categoryWrite.RemoveAsync(Guid.Parse(id));
            await _categoryWrite.SaveAsync();
            if (category.Image is not null)
                _imageService.DeleteImageByName(Path.GetFileName(category.Image));

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("updatecategory/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string id,
            [FromForm] UpdateCategory categoryModel)
        {
            Category? category = await _categoryRead.GetByIdAsync(Guid.Parse(id));

            if (category is null) return BadRequest("invalid category id");

            if (categoryModel.CategoryImage is not null)
            {
                string? oldImageName = Path.GetFileName(category.Image);
                string newImageName = _imageService.SaveImage(categoryModel.CategoryImage);
                category.Image = newImageName;
                if (oldImageName is not null)
                {
                    _imageService.DeleteImageByName(oldImageName);
                }
            }

            Admin? admin = await _adminRead.GetFirstAsync(admin => admin.Username == categoryModel.AdminUsername);
            category.Admin = admin ?? category.Admin;
            category.Name = categoryModel.CategoryName ?? category.Name;
            category.Date = DateTime.UtcNow;

            await _categoryWrite.UpdateAsync(category);

            return Ok();
        }
    }
}