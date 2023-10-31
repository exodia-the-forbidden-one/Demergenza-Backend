using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Application.Abstractions.Repositories.CategoryRepository;
using Demergenza.Domain.Entities.Menu;
using Microsoft.AspNetCore.Mvc;
using Demergenza.Domain.Entities.Models;
using Demergenza.Domain.Entities.Admin;
using System.Reflection;
using Demergenza.Application.Services;

namespace Demergenza.Controllers
{
    [ApiController]
    [Route("menu")]
    public class MenuController : Controller
    {
        private readonly ICategoryWriteRepository _categoryWrite;
        private readonly ICategoryReadRepository _categoryRead;
        private readonly IAdminReadRepository _adminRead;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly ImageService _imageService;
        public MenuController(ICategoryWriteRepository categoryWrite, ICategoryReadRepository categoryRead, IWebHostEnvironment hostEnvironment, IAdminReadRepository adminRead, ImageService imageService)
        {
            _categoryWrite = categoryWrite;
            _categoryRead = categoryRead;
            _hostEnviroment = hostEnvironment;
            _adminRead = adminRead;
            _imageService = imageService;
        }

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
            IQueryable<Category> categories = _categoryRead.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        [Route("addcategory")]
        public async Task<IActionResult> AddCategory([FromForm] AddCategoryModel addacategory)
        {
            string newImageName = _imageService.SaveImage(addacategory.categoryImage);

            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = addacategory.CategoryName,
                Image = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}",
                Admin = await _adminRead.GetFirstAsync(admin => admin.Username == addacategory.AdminUsername)
            };

            var isAdded = await _categoryWrite.AddAsync(category);
            await _categoryWrite.SaveAsync();

            return Ok(isAdded);
        }

        [HttpDelete]
        [Route("deletecategory/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            if (id is not null)
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
            return BadRequest(id);
        }

        [HttpPost]
        [Route("updatecategory/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromForm] UpdateCategoryModel categoryModel)
        {
            Category? category = await _categoryRead.GetByIdAsync(Guid.Parse(id));
            
            if (category is null) return BadRequest("invalid category id");

            if (categoryModel.CategoryImage is not null)
            {
                string oldImageName = Path.GetFileName(category.Image);
                string newImageName = _imageService.SaveImage(categoryModel.CategoryImage);
                category.Image = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}";
                _imageService.DeleteImageByName(oldImageName);
            }


            Admin? admin = await _adminRead.GetFirstAsync(admin => admin.Username == categoryModel.AdminUsername);
            category.Admin = admin ?? category.Admin;
            category.Name = categoryModel.CategoryName ?? category.Name;

            await _categoryWrite.UpdateAsync(category);

            return Ok();
        }
    }
}


