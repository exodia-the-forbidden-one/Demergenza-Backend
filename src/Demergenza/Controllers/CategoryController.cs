using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Application.Abstractions.Repositories.CategoryRepository;
using Demergenza.Application.DTOs.Category;
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
            Category? category = await _categoryRead.GetByIdAsync(Guid.Parse(id));
            return Ok(category);
        }

        [HttpGet]
        [Route("getallcategories")]
        public IActionResult GetAllCategories()
        {
            ICollection<Category> categories = _categoryRead.Select(c => new Category()
            {
                Id = c.Id,
                Menus = c.Menus,
                Name = c.Name,
                Image = c.Image
            }).ToList();
            return Ok(categories);
        }

        [Authorize]
        [HttpPost]
        [Route("addcategory")]
        public async Task<IActionResult> AddCategory([FromForm] AddCategory addacategory)
        {
            string newImageName = _imageService.SaveImage(addacategory.categoryImage);
            Admin? admin = await _adminRead.GetFirstAsync(admin => admin.Username == addacategory.AdminUsername);
            if (admin is null) return Unauthorized();
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = addacategory.CategoryName,
                Image = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}",
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
            bool isDeleted = _imageService.DeleteImageByName(Path.GetFileName(category.Image));
            return Ok(isDeleted);
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
                category.Image = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}";
                _imageService.DeleteImageByName(oldImageName);
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