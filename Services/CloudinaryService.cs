using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Laborlance_API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Laborlance_API.Services
{
    public class CloudinaryService
    {
        private Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            Account acc = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }

            return uploadResult;

        }
        public async Task<DeletionResult> DeleteImagesAsync(List<string> publicIds)
        {
            var deletionResult = new DeletionResult();
            foreach(var id in publicIds)
            {
                var deletionParams = new DeletionParams(id);
                deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            }
            return deletionResult;
        }
    }
}