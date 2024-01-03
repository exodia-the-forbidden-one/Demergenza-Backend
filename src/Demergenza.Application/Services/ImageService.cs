using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Demergenza.Application.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string DataImagesPath;
        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            DataImagesPath = Path.Combine(_hostEnvironment.WebRootPath, "data-images");
        }

        /// <summary>
        /// Saves an uploaded image to a data-images directory.
        /// </summary>
        /// <param name="formFile">The uploaded image file as an IFormFile object.</param>
        /// <returns>The unique name of the saved image file.</returns>
        public string SaveImage(IFormFile formFile)
        {
            string fileExtension = Path.GetExtension(formFile.FileName);
            string newImageName = Guid.NewGuid().ToString() + fileExtension;
            string path = Path.Combine(DataImagesPath, newImageName);

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                formFile.CopyTo(fileStream);
            }
            return newImageName;
        }

        public bool DeleteImageByName(string imageName)
        {
            {
                File.Delete(Path.Combine(DataImagesPath, imageName));
                return true;
            }
        }
    }
}