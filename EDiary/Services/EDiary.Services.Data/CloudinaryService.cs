namespace EDiary.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using EDiary.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                await pictureFile.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "EDiary",
                    File = new FileDescription(fileName, ms),
                };

                uploadResult = await this.cloudinaryUtility.UploadAsync(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
