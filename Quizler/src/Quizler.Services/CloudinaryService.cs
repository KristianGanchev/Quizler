﻿namespace Quizler.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public string UploadPictureAsync(string pictureFile, string fileName)
        {
            //byte[] destinationData;

            //using (var ms = new MemoryStream())
            //{
            //    await pictureFile.CopyToAsync(ms);
            //    destinationData = ms.ToArray();
            //}

            UploadResult uploadResult = null;

            //using (var ms = new MemoryStream(destinationData))
            //{
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "quizzes_images",
                    File = new FileDescription(fileName, pictureFile),
                    
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
           // }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
