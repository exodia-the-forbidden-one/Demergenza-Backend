using Demergenza.Application.Abstractions.Repositories.AdminRepository;
using Demergenza.Application.Abstractions.Repositories.CategoryRepository;
using Microsoft.AspNetCore.Hosting;
using Demergenza.Domain.Entities.Admin;
using Demergenza.Domain.Entities.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demergenza.Domain.Entities.Models;

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
        public MenuController(ICategoryWriteRepository categoryWrite, ICategoryReadRepository categoryRead, IWebHostEnvironment hostEnvironment, IAdminReadRepository adminRead)
        {
            _categoryWrite = categoryWrite;
            _categoryRead = categoryRead;
            _hostEnviroment = hostEnvironment;
            _adminRead = adminRead;
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
        public async Task<IActionResult> AddCategory([FromForm] AddCategoryModel addCategory)
        {

            string[] filenameAndExtension = addCategory.categoryImage.FileName.Split('.');
            string newImageName = Guid.NewGuid().ToString() + "." + filenameAndExtension[1];
            string path = Path.Combine(_hostEnviroment.WebRootPath, "data-images", newImageName);

            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = addCategory.CategoryName,
                Image = $"{Request.Scheme}://{Request.Host}/data-images/{newImageName}",
                Admin = await _adminRead.GetFirstAsync(admin => admin.Username == addCategory.AdminUsername)
            };

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                addCategory.categoryImage.CopyTo(fileStream);
            }

            var x = await _categoryWrite.AddAsync(category);
            await _categoryWrite.SaveAsync();

            return Ok(new
            {
                Ok = x
            });
        }

        [HttpDelete]
        [Route("deletecategory/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string id)
        {
            Console.WriteLine(id);
            if (id is not null)
            {
                await _categoryWrite.RemoveAsync(Guid.Parse(id));
                await _categoryWrite.SaveAsync();
                return Ok(true);
            }
            return BadRequest(id);
        }

    }
}


