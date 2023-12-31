﻿using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Web.Mvc;

namespace Advanced_Web_Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _Cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                  config.Value.CloudName,
                  config.Value.ApiKey,
                  config.Value.ApiSecret
            );
            _Cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhoteAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _Cloudinary.UploadAsync(uploadParams);
            }
            
            return uploadResult;
        }

        public Task<DeletionResult> DeletePhotoAsync(string PublicId)
        {
            var deleteParams = new DeletionParams(PublicId);
            var deleteResult = _Cloudinary.DestroyAsync(deleteParams);
            return deleteResult;
        }
    }
}
